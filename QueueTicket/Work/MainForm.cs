using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Tobasa
{
    public partial class MainForm : Form
    {
        #region Member variables / class

        /// Struct to save label data -label that need tobe resized automatically
        /// See labelRecordList,RecordLabelSize(),OnLabelResize()
        /// Set label Resize event handler to OnLabelResize()
        private struct LabelRecord
        {
            public LabelRecord(Label label)
            {
                this.label = label;
                this.initialWidth = label.Width;
                this.initialHeight = label.Height;
                this.initialFontSize = label.Font.Size;
            }

            public Label label;
            public int initialWidth;
            public int initialHeight;
            public float initialFontSize;
        }

        /// Array runnning text bottom
        ArrayList runningTextList = new ArrayList();

        /// array to list LabelRecord
        ArrayList labelRecordList;

        /// Ticket printout  informations
        private class TicketPrint
        {
            public Font HeaderFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);
            public Font DefaultFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            public Font NomorFont = new Font("Arial", 45, FontStyle.Bold, GraphicsUnit.Point);
            
            public string Header = "";
            public string TimeStamp = "";
            public string Footer = "";
            public string TextAntrian = "Nomor Antrian";
            public string Nomor = "0";
        }

        private delegate void ProcessMessageCallback(DataReceivedEventArgs arg, string text);
        private delegate void ProcessErrorCallBack(NotifyEventArgs e);

        private TCPClient client = null;
        private TicketPrint printJob = new TicketPrint();
        private bool isFullScreen = false;

        ///  our images
        private Bitmap dispLogoImg = null;
        private Bitmap displayHeaderBg = null;
        private Bitmap dispHeaderImg = null;
        private Bitmap post0BtnImgOn = null;
        private Bitmap post0BtnImgOff = null;
        private Bitmap post1BtnImgOn = null;
        private Bitmap post1BtnImgOff = null;
        private Bitmap post2BtnImgOn = null;
        private Bitmap post2BtnImgOff = null;
        private Bitmap post3BtnImgOn = null;
        private Bitmap post3BtnImgOff = null;
        private Bitmap post4BtnImgOn = null;
        private Bitmap post4BtnImgOff = null;
        
        #endregion

        #region Constructor/Destructor

        public MainForm()
        {
            isFullScreen = false;
            labelRecordList = new ArrayList();
 
            InitializeComponent();
            // we want to receive key event
            KeyPreview = true;
           
            InitImages();
            InitTexts();
                        
            AdaptDivMenuLayout();

            RecordLabelSize();

            if (Properties.Settings.Default.StartDisplayFullScreen)
                ToggleFullScreen();

            /// Start TCP client
            StartClient();
        }

        #endregion

        #region TCP Connection stuffs

        void TCPClient_Notified(NotifyEventArgs e)
        {
            ProcessError(e);
        }

        public void ProcessError(NotifyEventArgs e)
        {
            if (this.InvokeRequired)
            {
                ProcessErrorCallBack d = new ProcessErrorCallBack(ProcessError);
                this.Invoke(d, new object[] { e });
            }
            else
                MessageBox.Show(this, e.Message, e.Summary, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CloseConnection()
        {
            if (client != null && client.Session != null)
                client.Session.Close();
        }

        public void StartClient()
        {
            string dispServerHost = Properties.Settings.Default.QueueServerHost;
            int dispServerPort = Properties.Settings.Default.QueueServerPort;
            string stationName = Properties.Settings.Default.StationName;
            string stationPost = Properties.Settings.Default.StationPost;
            string userName = Properties.Settings.Default.QueueUserName;
            string password = Properties.Settings.Default.QueuePassword;

            client = new TCPClient(dispServerHost, dispServerPort);
            client.Notified += new Action<NotifyEventArgs>(TCPClient_Notified);

            client.Start();

            if (client.Connected)
            {
                client.Session.DataReceived += new DataReceived(NetSession_DataReceived);

                string salt = Properties.Settings.Default.SecuritySalt;
                string clearPwd = Util.DecryptPassword(password, salt);
                string passwordHash = Util.GetPasswordHash(clearPwd, userName);

                // Send LOGIN message + our station name to server
                string message = String.Empty;
                message = "LOGIN" + Msg.Separator + "TICKET" + Msg.Separator + stationName + Msg.Separator + stationPost + Msg.Separator + userName + Msg.Separator + passwordHash;
                client.Send(message);
            }
        }

        #endregion

        #region Autoresize Form's Label stuffs

        /// Registered form labels are automatically resized when Form size resized

        /// Save initial data from labels that need to be resized
        /// Every label in the list will have its Resize event handled by OnLabelResize
        /// which will use this data
        private void RecordLabelSize()
        {
            labelRecordList.Add(new LabelRecord(lblPnl0));
            labelRecordList.Add(new LabelRecord(lblPnl1));
            labelRecordList.Add(new LabelRecord(lblPnl2));
            labelRecordList.Add(new LabelRecord(lblPnl3));
            labelRecordList.Add(new LabelRecord(lblPnl4));
            labelRecordList.Add(new LabelRecord(runTextBottom));
        }

        private void ResizeLabel(LabelRecord lbl)
        {
            SuspendLayout();
            // Get the proportionality of the resize
            float proportionalNewWidth = (float)lbl.label.Width / lbl.initialWidth;
            float proportionalNewHeight = (float)lbl.label.Height / lbl.initialHeight;

            // Calculate the current font size
            lbl.label.Font = new Font(lbl.label.Font.FontFamily, lbl.initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               lbl.label.Font.Style);


            Image img = lbl.label.Image;
            if (img != null)
            {
                var pic = new Bitmap(img, lbl.label.Width, lbl.label.Height);
                lbl.label.Image = pic;
            }

            ResumeLayout();
        }

        private void OnLabelResize(object sender, EventArgs e)
        {
            foreach (LabelRecord lbl in labelRecordList)
            {
                if (lbl.label == (Label)sender)
                {
                    ResizeLabel(lbl);
                    break;
                }
            }
        }

        #endregion

        #region Form appearance stuffs

        void InitImages()
        {
            // Header images
            if (File.Exists(Properties.Settings.Default.DisplayHeaderBg))
                displayHeaderBg = new Bitmap(Properties.Settings.Default.DisplayHeaderBg);
            else
                displayHeaderBg = Properties.Resources.DisplayHeaderBg;

            if (File.Exists(Properties.Settings.Default.DisplayLogoImg))
                dispLogoImg = new Bitmap(Properties.Settings.Default.DisplayLogoImg);
            else
                dispLogoImg = Properties.Resources.QueueLogo150;

            if (File.Exists(Properties.Settings.Default.DisplayHeaderImg))
                dispHeaderImg = new Bitmap(Properties.Settings.Default.DisplayHeaderImg);
            else
                dispHeaderImg = Properties.Resources.DisplayHeaderImg;

            // POST#0 on
            if (File.Exists(Properties.Settings.Default.Post0BtnImgOn))
                post0BtnImgOn = new Bitmap(Properties.Settings.Default.Post0BtnImgOn);
            else
                post0BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#0 off
            if (File.Exists(Properties.Settings.Default.Post0BtnImgOff))
                post0BtnImgOff = new Bitmap(Properties.Settings.Default.Post0BtnImgOff);
            else
                post0BtnImgOff = Properties.Resources.ButtonGreenOff;

            // POST#1 on
            if (File.Exists(Properties.Settings.Default.Post1BtnImgOn))
                post1BtnImgOn = new Bitmap(Properties.Settings.Default.Post1BtnImgOn);
            else
                post1BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#1 off
            if (File.Exists(Properties.Settings.Default.Post1BtnImgOff))
                post1BtnImgOff = new Bitmap(Properties.Settings.Default.Post1BtnImgOff);
            else
                post1BtnImgOff = Properties.Resources.ButtonGreenOff;

            // POST#2 on
            if (File.Exists(Properties.Settings.Default.Post2BtnImgOn))
                post2BtnImgOn = new Bitmap(Properties.Settings.Default.Post2BtnImgOn);
            else
                post2BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#2 off
            if (File.Exists(Properties.Settings.Default.Post2BtnImgOff))
                post2BtnImgOff = new Bitmap(Properties.Settings.Default.Post2BtnImgOff);
            else
                post2BtnImgOff = Properties.Resources.ButtonGreenOff;

            // POST#3 on
            if (File.Exists(Properties.Settings.Default.Post3BtnImgOn))
                post3BtnImgOn = new Bitmap(Properties.Settings.Default.Post3BtnImgOn);
            else
                post3BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#3 off
            if (File.Exists(Properties.Settings.Default.Post3BtnImgOff))
                post3BtnImgOff = new Bitmap(Properties.Settings.Default.Post3BtnImgOff);
            else
                post3BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#4 on
            if (File.Exists(Properties.Settings.Default.Post4BtnImgOn))
                post4BtnImgOn = new Bitmap(Properties.Settings.Default.Post4BtnImgOn);
            else
                post4BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#4 off
            if (File.Exists(Properties.Settings.Default.Post4BtnImgOff))
                post4BtnImgOff = new Bitmap(Properties.Settings.Default.Post4BtnImgOff);
            else
                post4BtnImgOff = Properties.Resources.ButtonGreenOff;

            picBtnPnl0.Enabled = Properties.Settings.Default.Post0Enabled;
            picBtnPnl1.Enabled = Properties.Settings.Default.Post1Enabled;
            picBtnPnl2.Enabled = Properties.Settings.Default.Post2Enabled;
            picBtnPnl3.Enabled = Properties.Settings.Default.Post3Enabled;
            picBtnPnl4.Enabled = Properties.Settings.Default.Post4Enabled;

            if (!picBtnPnl0.Enabled)
            {
                post0BtnImgOn = Util.MakeGrayscale3(post0BtnImgOn);
                post0BtnImgOff = Util.MakeGrayscale3(post0BtnImgOff);
            }

            if (!picBtnPnl1.Enabled)
            {
                post1BtnImgOn = Util.MakeGrayscale3(post1BtnImgOn);
                post1BtnImgOff = Util.MakeGrayscale3(post1BtnImgOff);
            }

            if (!picBtnPnl2.Enabled)
            {
                post2BtnImgOn = Util.MakeGrayscale3(post2BtnImgOn);
                post2BtnImgOff = Util.MakeGrayscale3(post2BtnImgOff);
            }

            if (!picBtnPnl3.Enabled)
            {
                post3BtnImgOn = Util.MakeGrayscale3(post3BtnImgOn);
                post3BtnImgOff = Util.MakeGrayscale3(post3BtnImgOff);
            }

            if (!picBtnPnl4.Enabled)
            {
                post4BtnImgOn = Util.MakeGrayscale3(post4BtnImgOn);
                post4BtnImgOff = Util.MakeGrayscale3(post4BtnImgOff);
            }

            pnlHeader.BackgroundImage = displayHeaderBg;
            picLogo.Image = dispLogoImg;
            picHeader.Image = dispHeaderImg;
            picBtnPnl0.Image = post0BtnImgOff;
            picBtnPnl1.Image = post1BtnImgOff;
            picBtnPnl2.Image = post2BtnImgOff;
            picBtnPnl3.Image = post3BtnImgOff;
            picBtnPnl4.Image = post4BtnImgOff;
        }

        void AddRunningText(string text)
        {
            runningTextList.Add(text);
            runTextBottom.Text = "";
            foreach (string txt in runningTextList)
            {
                if (runTextBottom.Text != "")
                    runTextBottom.Text += "  ::  ";
                runTextBottom.Text += txt;
            }
        }

        void InitTexts()
        {
            lblPnl0.Text = Properties.Settings.Default.Post0Caption;
            lblPnl1.Text = Properties.Settings.Default.Post1Caption;
            lblPnl2.Text = Properties.Settings.Default.Post2Caption;
            lblPnl3.Text = Properties.Settings.Default.Post3Caption;
            lblPnl4.Text = Properties.Settings.Default.Post4Caption;

            AddRunningText(Properties.Settings.Default.RunningText0);
            AddRunningText(Properties.Settings.Default.RunningText1);
        }

        public void ToggleFullScreen()
        {
            if (this.isFullScreen == false)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.isFullScreen = true;
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.isFullScreen = false;
            }
        }

        private void AdaptDivMenuLayout()
        {
            bool visible3 = Properties.Settings.Default.Post3Visible;
            divPost3.Visible = visible3;

            bool visible4 = Properties.Settings.Default.Post4Visible;
            divPost4.Visible = visible4;

            if (visible3 && visible4)
            {
                foreach (RowStyle style in divMenu.RowStyles)
                {
                    style.SizeType = SizeType.Percent;
                    style.Height = 20F;
                }
            }
            else if (!visible3 && !visible4)
            {
                divMenu.RowStyles[0].Height = 33.33F;
                divMenu.RowStyles[1].Height = 33.33F;
                divMenu.RowStyles[2].Height = 33.33F;
                divMenu.RowStyles[3].Height = 0F;
                divMenu.RowStyles[4].Height = 0F;
            }
            else if (!visible3 && visible4)
            {
                divMenu.RowStyles[0].Height = 25F;
                divMenu.RowStyles[1].Height = 25F;
                divMenu.RowStyles[2].Height = 25F;
                divMenu.RowStyles[3].Height = 0F;
                divMenu.RowStyles[4].Height = 25F;
            }
            else if (visible3 && !visible4)
            {
                divMenu.RowStyles[0].Height = 25F;
                divMenu.RowStyles[1].Height = 25F;
                divMenu.RowStyles[2].Height = 25F;
                divMenu.RowStyles[3].Height = 25F;
                divMenu.RowStyles[4].Height = 0F;
            }
        }

        #endregion

        #region QueueServer message handler

        /// cross thread safe handler
        private void ProcessMessage(DataReceivedEventArgs arg, string text)
        {
            /// InvokeRequired required compares the thread ID of the 
            /// calling thread to the thread ID of the creating thread. 
            /// If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                ProcessMessageCallback d = new ProcessMessageCallback(ProcessMessage);
                this.Invoke(d, new object[] { arg, text });
            }
            else
            {
                if (text.StartsWith("TICKET"))
                    HandleMessage(arg, text);
                else if (text.StartsWith("LOGIN"))
                {
                    string _response = text;
                    if (_response == Msg.LOGIN_OK )
                    {
                        Logger.Log("QueueTicket : Successfully logged in");
                    }
                    else
                    {
                        string reason = _response.Substring(10);
                        string msg = "QueueTicket : Could not logged in to server, \r\nReason: " + reason;
                        Logger.Log(msg);
                        MessageBox.Show(this, msg, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseConnection();
                    }
                }
                else
                {
                    string logmsg = String.Format("Unhandled session message from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                }
            }
        }

        private void NetSession_DataReceived(DataReceivedEventArgs arg)
        {
            string text = "";
            /*
            /// Deserialize the message
            object message = Message.Deserialize(arg.Data);

            /// Handle the message
            StringMessage stringMessage = message as StringMessage;
            if (stringMessage != null)
                text = stringMessage.Message;
            */

            string stringMessage = Encoding.UTF8.GetString(arg.Data);
            if (stringMessage != null)
            {
                text = stringMessage;
                ProcessMessage(arg, text);
            }

        }

        private void HandleMessage(DataReceivedEventArgs arg, string text)
        {
            if (text == Msg.TICKET_SET_NEWNUMBER_NULL )
                MessageBox.Show("Cannot create new number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (text.StartsWith( Msg.TICKET_SET_NEWNUMBER ))
                HandleTicketSetNextNumber(arg, text);
        }

        private void HandleTicketSetNextNumber(DataReceivedEventArgs arg, string text)
        {
            string _prefix, _number, _post, _timestamp, _header;
            _prefix = _number = _post = _timestamp = _header = "";

            string[] words = text.Split(Msg.Separator.ToCharArray());
            if (words.Length == 6)
            {
                _prefix = words[2];
                _number = words[3];
                _post = words[4];
                _timestamp = words[5];

                if (true == Properties.Settings.Default.PrintTicket)
                {
                    short _prnCopies = 0;
                    // Get Print Copies value
                    if (_post == Properties.Settings.Default.Post0Post)
                    {
                        _prnCopies = Properties.Settings.Default.Post0PrintCopies;
                        _header = Properties.Settings.Default.Post0PrintHeader;
                    }
                    else if (_post == Properties.Settings.Default.Post1Post)
                    {
                        _prnCopies = Properties.Settings.Default.Post1PrintCopies;
                        _header = Properties.Settings.Default.Post1PrintHeader;
                    }
                    else if (_post == Properties.Settings.Default.Post2Post)
                    {
                        _prnCopies = Properties.Settings.Default.Post2PrintCopies;
                        _header = Properties.Settings.Default.Post2PrintHeader;
                    }
                    else if (_post == Properties.Settings.Default.Post3Post)
                    {
                        _prnCopies = Properties.Settings.Default.Post3PrintCopies;
                        _header = Properties.Settings.Default.Post3PrintHeader;
                    }
                    else if (_post == Properties.Settings.Default.Post4Post)
                    {
                        _prnCopies = Properties.Settings.Default.Post4PrintCopies;
                        _header = Properties.Settings.Default.Post4PrintHeader;
                    }

                    int i = 0;
                    while (i < _prnCopies)
                    {
                        PrintTicket(_prefix + _number, _post, _timestamp, _header);
                        i++;
                    }
                }
            }
            else
            {
                string logmsg = String.Format("Invalid TICKET:SET_NEWNUMBER from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                Logger.Log(logmsg);
                return;
            }
        }

        #endregion

        #region Ticket Printing stuffs

        private void PrintTicket(string number, string post, string timestamp, string header="")
        {
            string msg = number + "\r\n" + post + "\r\n" + timestamp;
            Logger.Log("QueueTicket : PrintTicket : " + msg);
            
            if (header == "")
                printJob.Header = post;
            else
                printJob.Header = header;

            printJob.Nomor = number;
            printJob.TimeStamp = timestamp;
            printJob.Footer = Properties.Settings.Default.PrintFooter;

            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(PrintPageHandler);
                pd.DefaultPageSettings.Landscape = false;
                // pd.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                pd.DocumentName = "Ticket_" + post + "_" + number; 

                // Create a new instance of Margins with 1-inch margins.
                // i-inch = 100
                Margins margins = new Margins(30,30,30,30);
                pd.DefaultPageSettings.Margins = margins;
               
                // Create 3-inch x 3-inch paper size
                PaperSize ppprSize = new PaperSize("TicketSize", 300, 300);
                pd.DefaultPageSettings.PaperSize = ppprSize;
                //pd.PrinterSettings.Copies = _prnCopies;
                //pd.PrinterSettings.Collate =  (_prnCopies > 1);

                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// The PrintPage event is raised for each page to be printed. 
        private void PrintPageHandler(object sender, PrintPageEventArgs ev)
        {
            float yPos = 0;
            int leftMargin = ev.MarginBounds.Left;
            int topMargin = ev.MarginBounds.Top;
            int width = ev.MarginBounds.Width;
            int height = ev.MarginBounds.Height;

            // Create a StringFormat object with the each line of text, and the block 
            // of text centered on the page.
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            //stringFormat.LineAlignment = StringAlignment.Center;

            StringFormat stringFormat2 = new StringFormat(stringFormat);
            StringFormat stringFormat3 = new StringFormat(stringFormat);
            StringFormat stringFormat4 = new StringFormat(stringFormat);
            StringFormat stringFormat5 = new StringFormat(stringFormat);

            yPos = topMargin;
            Rectangle rect1 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(printJob.Header, printJob.HeaderFont, Brushes.Black, rect1, stringFormat);

            //yPos += printJob.HeaderFont.GetHeight(ev.Graphics);
            yPos += ev.Graphics.MeasureString(printJob.Header, printJob.HeaderFont, width).Height;
            Rectangle rect2 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(printJob.TimeStamp, printJob.DefaultFont, Brushes.Black, rect2, stringFormat2);

            //yPos += printJob.DefaultFont.GetHeight(ev.Graphics);
            yPos += ev.Graphics.MeasureString(printJob.TimeStamp, printJob.DefaultFont, width).Height;
            Rectangle rect3 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(printJob.TextAntrian, printJob.DefaultFont, Brushes.Black, rect3, stringFormat3);

            //yPos += printJob.DefaultFont.GetHeight(ev.Graphics);
            yPos += ev.Graphics.MeasureString(printJob.TextAntrian, printJob.DefaultFont, width).Height;
            Rectangle rect4 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(printJob.Nomor, printJob.NomorFont, Brushes.Black, rect4, stringFormat4);

            //yPos += printJob.NomorFont.GetHeight(ev.Graphics);
            yPos += ev.Graphics.MeasureString(printJob.Nomor, printJob.NomorFont, width).Height;
            Rectangle rect5 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(printJob.Footer, printJob.DefaultFont, Brushes.Black, rect5, stringFormat5);
        }

        #endregion

        #region Form event handlers

        ///  Send TICKET_CREATE_NEWNUMBER message to QueueServer
        private void CreateNewNumber(string text)
        {
            if (client.Connected)
            {
                string message = "TICKET" + Msg.Separator + "CREATE_NEWNUMBER" + Msg.Separator;
                message += Properties.Settings.Default.StationName + Msg.Separator;

                string post0 = Properties.Settings.Default.Post0Post;
                string post1 = Properties.Settings.Default.Post1Post;
                string post2 = Properties.Settings.Default.Post2Post;
                string post3 = Properties.Settings.Default.Post3Post;
                string post4 = Properties.Settings.Default.Post4Post;


                if (text == "POST#0")
                    message += post0;
                else if (text == "POST#1")
                    message += post1;
                else if (text == "POST#2")
                    message += post2;
                else if (text == "POST#3")
                    message += post3;
                else if (text == "POST#4")
                    message += post4;

                client.Send(message);
            }
            else
            {
                MessageBox.Show(this, "Could not connect to server\r\nPlease restart application", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.StartDisplayFullScreen = isFullScreen;
            Properties.Settings.Default.Save();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F:
                case Keys.Return:
                {   
                    ToggleFullScreen();
                    break;
                }
                case Keys.Escape:
                {
                    this.Close();
                    break;
                }
                case Keys.O:  // Ctrl+O
                {
                    
                    if (e.Control)
                    {
                        OptionForm form = new OptionForm();
                        form.ShowDialog();
                    }
                    break;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
            SaveSettings();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;

            if (picBox == picBtnPnl0)
                picBox.Image = post0BtnImgOn;
            else if (picBox == picBtnPnl1)
                picBox.Image = post1BtnImgOn;
            else if (picBox == picBtnPnl2)
                picBox.Image = post2BtnImgOn;
            else if (picBox == picBtnPnl3)
                picBox.Image = post3BtnImgOn;
            else if (picBox == picBtnPnl4)
                picBox.Image = post4BtnImgOn;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;

            if (picBox == picBtnPnl0)
            {
                picBox.Image = post0BtnImgOff;
                CreateNewNumber("POST#0");
            }
            else if (picBox == picBtnPnl1)
            {
                picBox.Image = post1BtnImgOff;
                CreateNewNumber("POST#1");
            }
            else if (picBox == picBtnPnl2)
            {
                picBox.Image = post2BtnImgOff;
                CreateNewNumber("POST#2");
            }
            else if (picBox == picBtnPnl3)
            {
                picBox.Image = post3BtnImgOff;
                CreateNewNumber("POST#3");
            }
            else if (picBox == picBtnPnl4)
            {
                picBox.Image = post4BtnImgOff;
                CreateNewNumber("POST#4");
            }
        }

        #endregion
    }
}
