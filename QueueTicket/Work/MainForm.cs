#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2015-2024  Jefri Sibarani

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Tobasa
{
    public partial class MainForm : Form
    {
        #region Member variables / class

        private delegate void NetSessionDataReceivedCb(DataReceivedEventArgs arg);
        private delegate void TCPClientNotifiedCb(NotifyEventArgs e);
        private delegate void TCPClientClosedCb(TCPClient e);

        Properties.Settings _settings = Properties.Settings.Default;
        private TCPClient _client = null;
        private TicketPrint _printJob = new TicketPrint();
        private bool _isFullScreen = false;

        // Struct to save label data -label that need tobe resized automatically
        // See labelRecordList,RecordLabelSize(),OnLabelResize()
        // Set label Resize event handler to OnLabelResize()
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

        // Array runnning text bottom
        ArrayList runningTextList = new ArrayList();

        // array to list LabelRecord
        ArrayList labelRecordList;

        // Ticket printout  informations
        private class TicketPrint
        {
            public Font HeaderFont  = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);
            public Font DefaultFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            public Font NomorFont   = new Font("Arial", 45, FontStyle.Bold, GraphicsUnit.Point);

            public string Header = "";
            public string TimeStamp = "";
            public string Footer = "";
            public string TextAntrian = "Nomor Antrian";
            public string Nomor = "0";
        }

        //  our images
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
        private Bitmap post5BtnImgOn = null;
        private Bitmap post5BtnImgOff = null;
        private Bitmap post6BtnImgOn = null;
        private Bitmap post6BtnImgOff = null;
        private Bitmap post7BtnImgOn = null;
        private Bitmap post7BtnImgOff = null;
        private Bitmap post8BtnImgOn = null;
        private Bitmap post8BtnImgOff = null;
        private Bitmap post9BtnImgOn = null;
        private Bitmap post9BtnImgOff = null;

        #endregion

        #region Constructor/Destructor

        public MainForm()
        {
            _isFullScreen = false;
            labelRecordList = new ArrayList();

            InitializeComponent();
            // we want to receive key event
            KeyPreview = true;

            InitImages();
            InitTexts();
            InitButtonText();
            AdaptDivMenuLayout();
            AdaptLeftRightMenuLayout();
            RecordLabelSize();

            if (_settings.StartDisplayFullScreen)
                ToggleFullScreen();

            // Start TCP client
            StartClient();
        }

        #endregion

        #region TCP Connection stuffs

        private void TCPClientNotified(NotifyEventArgs arg)
        {
            if (this.InvokeRequired)
            {
                TCPClientNotifiedCb dlg = new TCPClientNotifiedCb(TCPClientNotified);
                this.Invoke(dlg, new object[] { arg });
            }
            else
            {
                if (arg.Type == NotifyType.NOTIFY_ERR)
                {
                    MessageBox.Show(arg.Message, arg.Summary, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Logger.Log(arg);
            }
        }

        private void TCPClientConnected(TCPClient client)
        {
            if (client.Connected)
            {
                string clearPwd = Util.DecryptPassword(_settings.QueuePassword, _settings.SecuritySalt);
                string passwordHash = Util.GetPasswordHash(clearPwd, _settings.QueueUserName);

                // Log in to the server
                // SYS|LOGIN|REQ|[Module!Post!Station!Username!Password]
                string message =
                    Msg.SysLogin.Text +
                    Msg.Separator + "REQ" +
                    Msg.Separator + "TICKET" +
                    Msg.CompDelimiter + _settings.StationPost +
                    Msg.CompDelimiter + _settings.StationName +
                    Msg.CompDelimiter + _settings.QueueUserName +
                    Msg.CompDelimiter + passwordHash;

                client.Send(message);
            }
        }

        private void TCPClientClosed(TCPClient client)
        {
            if (this.InvokeRequired)
            {
                TCPClientClosedCb dlg = new TCPClientClosedCb(TCPClientClosed);
                this.Invoke(dlg, new object[] { client });
            }
            else
            {
                //lblStatus.Text = "Not Connected to Server";
            }
        }

        public void StartClient()
        {
            _client = null;
            _client = new TCPClient(_settings.QueueServerHost, _settings.QueueServerPort);
            _client.Notified += new Action<NotifyEventArgs>(TCPClientNotified);
            _client.OnDataReceived += new DataReceived(NetSessionDataReceived);
            _client.OnConnected += new ConnectionConnected(TCPClientConnected);

            _client.Start();
        }
        private void CloseConnection()
        {
            if (_client != null)
                _client.Stop();
        }

        private void RestartClient()
        {
            Logger.Log("QueueTicket", "Restarting TCPClient");
            CloseConnection();
            StartClient();
        }

        #endregion

        #region Autoresize Form's Label stuffs

        // Registered form labels are automatically resized when Form size resized
        // Save initial data from labels that need to be resized
        // Every label in the list will have its Resize event handled by OnLabelResize
        // which will use this data
        private void RecordLabelSize()
        {
            labelRecordList.Add(new LabelRecord(lblPnl0));
            labelRecordList.Add(new LabelRecord(lblPnl1));
            labelRecordList.Add(new LabelRecord(lblPnl2));
            labelRecordList.Add(new LabelRecord(lblPnl3));
            labelRecordList.Add(new LabelRecord(lblPnl4));
            labelRecordList.Add(new LabelRecord(lblPnl5));
            labelRecordList.Add(new LabelRecord(lblPnl6));
            labelRecordList.Add(new LabelRecord(lblPnl7));
            labelRecordList.Add(new LabelRecord(lblPnl8));
            labelRecordList.Add(new LabelRecord(lblPnl9));
            //labelRecordList.Add(new LabelRecord(runningTextBottom));
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
            if (File.Exists(_settings.DisplayHeaderBg))
                displayHeaderBg = new Bitmap(_settings.DisplayHeaderBg);
            else
                displayHeaderBg = Properties.Resources.DisplayHeaderBg;

            if (File.Exists(_settings.DisplayLogoImg))
                dispLogoImg = new Bitmap(_settings.DisplayLogoImg);
            else
                dispLogoImg = Properties.Resources.QueueLogo150;

            if (File.Exists(_settings.DisplayHeaderImg))
                dispHeaderImg = new Bitmap(_settings.DisplayHeaderImg);
            else
                dispHeaderImg = Properties.Resources.DisplayHeaderImg;


            // POST#0 on
            if (File.Exists(_settings.Post0BtnImgOn))
                post0BtnImgOn = new Bitmap(_settings.Post0BtnImgOn);
            else
                post0BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#0 off
            if (File.Exists(_settings.Post0BtnImgOff))
                post0BtnImgOff = new Bitmap(_settings.Post0BtnImgOff);
            else
                post0BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#1 on
            if (File.Exists(_settings.Post1BtnImgOn))
                post1BtnImgOn = new Bitmap(_settings.Post1BtnImgOn);
            else
                post1BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#1 off
            if (File.Exists(_settings.Post1BtnImgOff))
                post1BtnImgOff = new Bitmap(_settings.Post1BtnImgOff);
            else
                post1BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#2 on
            if (File.Exists(_settings.Post2BtnImgOn))
                post2BtnImgOn = new Bitmap(_settings.Post2BtnImgOn);
            else
                post2BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#2 off
            if (File.Exists(_settings.Post2BtnImgOff))
                post2BtnImgOff = new Bitmap(_settings.Post2BtnImgOff);
            else
                post2BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#3 on
            if (File.Exists(_settings.Post3BtnImgOn))
                post3BtnImgOn = new Bitmap(_settings.Post3BtnImgOn);
            else
                post3BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#3 off
            if (File.Exists(_settings.Post3BtnImgOff))
                post3BtnImgOff = new Bitmap(_settings.Post3BtnImgOff);
            else
                post3BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#4 on
            if (File.Exists(_settings.Post4BtnImgOn))
                post4BtnImgOn = new Bitmap(_settings.Post4BtnImgOn);
            else
                post4BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#4 off
            if (File.Exists(_settings.Post4BtnImgOff))
                post4BtnImgOff = new Bitmap(_settings.Post4BtnImgOff);
            else
                post4BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#5 on
            if (File.Exists(_settings.Post5BtnImgOn))
                post5BtnImgOn = new Bitmap(_settings.Post5BtnImgOn);
            else
                post5BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#5 off
            if (File.Exists(_settings.Post5BtnImgOff))
                post5BtnImgOff = new Bitmap(_settings.Post5BtnImgOff);
            else
                post5BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#6 on
            if (File.Exists(_settings.Post6BtnImgOn))
                post6BtnImgOn = new Bitmap(_settings.Post6BtnImgOn);
            else
                post6BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#6 off
            if (File.Exists(_settings.Post6BtnImgOff))
                post6BtnImgOff = new Bitmap(_settings.Post6BtnImgOff);
            else
                post6BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#7 on
            if (File.Exists(_settings.Post7BtnImgOn))
                post7BtnImgOn = new Bitmap(_settings.Post7BtnImgOn);
            else
                post7BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#7 off
            if (File.Exists(_settings.Post7BtnImgOff))
                post7BtnImgOff = new Bitmap(_settings.Post7BtnImgOff);
            else
                post7BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#8 on
            if (File.Exists(_settings.Post8BtnImgOn))
                post8BtnImgOn = new Bitmap(_settings.Post8BtnImgOn);
            else
                post8BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#8 off
            if (File.Exists(_settings.Post8BtnImgOff))
                post8BtnImgOff = new Bitmap(_settings.Post8BtnImgOff);
            else
                post8BtnImgOff = Properties.Resources.ButtonGreenOff;


            // POST#9 on
            if (File.Exists(_settings.Post9BtnImgOn))
                post9BtnImgOn = new Bitmap(_settings.Post9BtnImgOn);
            else
                post9BtnImgOn = Properties.Resources.ButtonGreenOn;
            // POST#9 off
            if (File.Exists(_settings.Post9BtnImgOff))
                post9BtnImgOff = new Bitmap(_settings.Post9BtnImgOff);
            else
                post9BtnImgOff = Properties.Resources.ButtonGreenOff;


            picBtnPnl0.Enabled = _settings.Post0Enabled;
            picBtnPnl1.Enabled = _settings.Post1Enabled;
            picBtnPnl2.Enabled = _settings.Post2Enabled;
            picBtnPnl3.Enabled = _settings.Post3Enabled;
            picBtnPnl4.Enabled = _settings.Post4Enabled;
            picBtnPnl5.Enabled = _settings.Post5Enabled;
            picBtnPnl6.Enabled = _settings.Post6Enabled;
            picBtnPnl7.Enabled = _settings.Post7Enabled;
            picBtnPnl8.Enabled = _settings.Post8Enabled;
            picBtnPnl9.Enabled = _settings.Post9Enabled;

            if (!picBtnPnl0.Enabled)
            {
                post0BtnImgOn  = Util.MakeGrayscale3(post0BtnImgOn);
                post0BtnImgOff = Util.MakeGrayscale3(post0BtnImgOff);
            }

            if (!picBtnPnl1.Enabled)
            {
                post1BtnImgOn  = Util.MakeGrayscale3(post1BtnImgOn);
                post1BtnImgOff = Util.MakeGrayscale3(post1BtnImgOff);
            }

            if (!picBtnPnl2.Enabled)
            {
                post2BtnImgOn  = Util.MakeGrayscale3(post2BtnImgOn);
                post2BtnImgOff = Util.MakeGrayscale3(post2BtnImgOff);
            }

            if (!picBtnPnl3.Enabled)
            {
                post3BtnImgOn  = Util.MakeGrayscale3(post3BtnImgOn);
                post3BtnImgOff = Util.MakeGrayscale3(post3BtnImgOff);
            }

            if (!picBtnPnl4.Enabled)
            {
                post4BtnImgOn  = Util.MakeGrayscale3(post4BtnImgOn);
                post4BtnImgOff = Util.MakeGrayscale3(post4BtnImgOff);
            }

            if (!picBtnPnl5.Enabled)
            {
                post5BtnImgOn  = Util.MakeGrayscale3(post5BtnImgOn);
                post5BtnImgOff = Util.MakeGrayscale3(post5BtnImgOff);
            }

            if (!picBtnPnl6.Enabled)
            {
                post6BtnImgOn  = Util.MakeGrayscale3(post6BtnImgOn);
                post6BtnImgOff = Util.MakeGrayscale3(post6BtnImgOff);
            }

            if (!picBtnPnl7.Enabled)
            {
                post7BtnImgOn  = Util.MakeGrayscale3(post7BtnImgOn);
                post7BtnImgOff = Util.MakeGrayscale3(post7BtnImgOff);
            }

            if (!picBtnPnl8.Enabled)
            {
                post8BtnImgOn  = Util.MakeGrayscale3(post8BtnImgOn);
                post8BtnImgOff = Util.MakeGrayscale3(post8BtnImgOff);
            }

            if (!picBtnPnl9.Enabled)
            {
                post9BtnImgOn  = Util.MakeGrayscale3(post9BtnImgOn);
                post9BtnImgOff = Util.MakeGrayscale3(post9BtnImgOff);
            }

            pnlHeader.BackgroundImage = displayHeaderBg;
            picLogo.Image = dispLogoImg;
            picHeader.Image = dispHeaderImg;

            picBtnPnl0.Image = post0BtnImgOff;
            picBtnPnl1.Image = post1BtnImgOff;
            picBtnPnl2.Image = post2BtnImgOff;
            picBtnPnl3.Image = post3BtnImgOff;
            picBtnPnl4.Image = post4BtnImgOff;
            picBtnPnl5.Image = post5BtnImgOff;
            picBtnPnl6.Image = post6BtnImgOff;
            picBtnPnl7.Image = post7BtnImgOff;
            picBtnPnl8.Image = post8BtnImgOff;
            picBtnPnl9.Image = post9BtnImgOff;
        }

        void InitButtonText()
        {
            picBtnPnl0.Text = _settings.Post0Name;
            picBtnPnl1.Text = _settings.Post1Name;
            picBtnPnl2.Text = _settings.Post2Name;
            picBtnPnl3.Text = _settings.Post3Name;
            picBtnPnl4.Text = _settings.Post4Name;
            picBtnPnl5.Text = _settings.Post5Name;
            picBtnPnl6.Text = _settings.Post6Name;
            picBtnPnl7.Text = _settings.Post7Name;
            picBtnPnl8.Text = _settings.Post8Name;
            picBtnPnl9.Text = _settings.Post9Name;
        }

        void AddRunningText(string text)
        {
            runningTextList.Add(text);
                runningTextBottom.Text = "";
            foreach (string txt in runningTextList)
            {
                if (runningTextBottom.Text != "")
                    runningTextBottom.Text += "  ::  ";

                runningTextBottom.Text += txt;
            }
        }

        void InitTexts()
        {
            lblPnl0.Text = _settings.Post0Caption;
            lblPnl1.Text = _settings.Post1Caption;
            lblPnl2.Text = _settings.Post2Caption;
            lblPnl3.Text = _settings.Post3Caption;
            lblPnl4.Text = _settings.Post4Caption;
            lblPnl5.Text = _settings.Post5Caption;
            lblPnl6.Text = _settings.Post6Caption;
            lblPnl7.Text = _settings.Post7Caption;
            lblPnl8.Text = _settings.Post8Caption;
            lblPnl9.Text = _settings.Post9Caption;

            AddRunningText(_settings.RunningText0);
            AddRunningText(_settings.RunningText1);

            // set main menu label content alignment
            var contentAligment = ContentAlignment.MiddleRight;
            if (_settings.MainMenuLabelAlignment == "Left")
                contentAligment = ContentAlignment.MiddleLeft;
            if (_settings.MainMenuLabelAlignment == "Middle")
                contentAligment = ContentAlignment.MiddleCenter;
            if (_settings.MainMenuLabelAlignment == "Right")
                contentAligment = ContentAlignment.MiddleRight;

            lblPnl0.TextAlign = contentAligment;
            lblPnl1.TextAlign = contentAligment;
            lblPnl2.TextAlign = contentAligment;
            lblPnl3.TextAlign = contentAligment;
            lblPnl4.TextAlign = contentAligment;
            lblPnl5.TextAlign = contentAligment;
            lblPnl6.TextAlign = contentAligment;
            lblPnl7.TextAlign = contentAligment;
            lblPnl8.TextAlign = contentAligment;
            lblPnl9.TextAlign = contentAligment;

            float fontSize = (float) _settings.MainMenuLabelFontSize;
            if (fontSize > 26) fontSize = 26;
            if (fontSize < 0) fontSize = 0;

            lblPnl0.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl1.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl2.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl3.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl4.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl5.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl6.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl7.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl8.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPnl9.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        }

        public void ToggleFullScreen()
        {
            if (this._isFullScreen == false)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this._isFullScreen = true;
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this._isFullScreen = false;
            }
        }

        private void AdaptDivMenuLayout()
        {
            // Left Part
            bool visible3 = _settings.Post3Visible;
            divPost3.Visible = visible3;
            bool visible4 = _settings.Post4Visible;
            divPost4.Visible = visible4;
            
            if (visible3 && visible4)
            {
                foreach (RowStyle style in divMenuLeft.RowStyles)
                {
                    style.SizeType = SizeType.Percent;
                    style.Height = 20F;
                }
            }
            else if (!visible3 && !visible4)
            {
                divMenuLeft.RowStyles[0].Height = 33.33F;
                divMenuLeft.RowStyles[1].Height = 33.33F;
                divMenuLeft.RowStyles[2].Height = 33.33F;
                divMenuLeft.RowStyles[3].Height = 0F;
                divMenuLeft.RowStyles[4].Height = 0F;
            }
            else if (!visible3 && visible4)
            {
                divMenuLeft.RowStyles[0].Height = 25F;
                divMenuLeft.RowStyles[1].Height = 25F;
                divMenuLeft.RowStyles[2].Height = 25F;
                divMenuLeft.RowStyles[3].Height = 0F;
                divMenuLeft.RowStyles[4].Height = 25F;
            }
            else if (visible3 && !visible4)
            {
                divMenuLeft.RowStyles[0].Height = 25F;
                divMenuLeft.RowStyles[1].Height = 25F;
                divMenuLeft.RowStyles[2].Height = 25F;
                divMenuLeft.RowStyles[3].Height = 25F;
                divMenuLeft.RowStyles[4].Height = 0F;
            }


            // Right Part
            bool visible8 = _settings.Post8Visible;
            divPost8.Visible = visible8;
            bool visible9 = _settings.Post9Visible;
            divPost9.Visible = visible9;

            if (visible8 && visible9)
            {
                foreach (RowStyle style in divMenuRight.RowStyles)
                {
                    style.SizeType = SizeType.Percent;
                    style.Height = 20F;
                }
            }
            else if (!visible8 && !visible9)
            {
                divMenuRight.RowStyles[0].Height = 33.33F;
                divMenuRight.RowStyles[1].Height = 33.33F;
                divMenuRight.RowStyles[2].Height = 33.33F;
                divMenuRight.RowStyles[3].Height = 0F;
                divMenuRight.RowStyles[4].Height = 0F;
            }
            else if (!visible8 && visible9)
            {
                divMenuRight.RowStyles[0].Height = 25F;
                divMenuRight.RowStyles[1].Height = 25F;
                divMenuRight.RowStyles[2].Height = 25F;
                divMenuRight.RowStyles[3].Height = 0F;
                divMenuRight.RowStyles[4].Height = 25F;
            }
            else if (visible8 && !visible9)
            {
                divMenuRight.RowStyles[0].Height = 25F;
                divMenuRight.RowStyles[1].Height = 25F;
                divMenuRight.RowStyles[2].Height = 25F;
                divMenuRight.RowStyles[3].Height = 25F;
                divMenuRight.RowStyles[4].Height = 0F;
            }
        }

        private void AdaptLeftRightMenuLayout()
        {
            bool leftMenuVisible = _settings.ShowLeftMenu;
            bool rightMenuVisible = _settings.ShowRightMenu;

            if( leftMenuVisible && !rightMenuVisible)
            {
                divMenu.ColumnStyles[0].Width = 100F;
                divMenu.ColumnStyles[1].Width = 0F;
            }
            else if  (!leftMenuVisible && rightMenuVisible )
            {
                divMenu.ColumnStyles[0].Width = 0F;
                divMenu.ColumnStyles[1].Width = 100F;
            }
            else
            {
                divMenu.ColumnStyles[0].Width = 50F;
                divMenu.ColumnStyles[1].Width = 50F;
            }
        }

        #endregion

        #region QueueServer message handler

        // cross thread safe handler
        private void NetSessionDataReceived(DataReceivedEventArgs arg)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                NetSessionDataReceivedCb d = new NetSessionDataReceivedCb(NetSessionDataReceived);
                this.Invoke(d, new object[] { arg });
            }
            else
            {
                if (arg.DataString.StartsWith("SYS") || arg.DataString.StartsWith("TICKET"))
                    HandleMessage(arg);
                else
                {
                    string logmsg = String.Format("[QueueTicket] Unhandled session message from: {0}", arg.RemoteInfo);
                    Logger.Log(logmsg);
                }
            }
        }

        private void HandleMessage(DataReceivedEventArgs arg)
        {
            try
            {
                Message qmessage = new Message(arg);

                Logger.Log("[QueueTicket] Processing " + qmessage.MessageType.String + " from " + arg.Session.RemoteInfo);

                // Handle SysLogin
                if (qmessage.MessageType == Msg.SysLogin && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string result = qmessage.PayloadValues["result"];
                    string data   = qmessage.PayloadValues["data"];

                    if (result == "OK")
                    {
                        Logger.Log("[QueueTicket] Successfully logged in");
                    }
                    else
                    {
                        string reason = data;
                        string msg = "[QueueTicket] Could not logged in to server, \r\nReason: " + reason;

                        Logger.Log(msg);
                        MessageBox.Show(this, msg, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        CloseConnection();
                    }
                }
                // Handle TicketCreate
                else if (qmessage.MessageType == Msg.TicketCreate)
                {
                    // extract payload
                    string postprefix = qmessage.PayloadValues["postprefix"];
                    string number     = qmessage.PayloadValues["number"];
                    string post       = qmessage.PayloadValues["post"];
                    string timestamp  = qmessage.PayloadValues["timestamp"];

                    if (string.IsNullOrWhiteSpace(number))
                        MessageBox.Show("Could not create new number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        PrintNewTicket(postprefix, number, post, timestamp);
                }
                // Handle SysNotify
                else if (qmessage.MessageType == Msg.SysNotify)
                {
                    // extract payload
                    string notifyTyp = qmessage.PayloadValues["type"];
                    string notifyMsg = qmessage.PayloadValues["message"];

                    if (notifyTyp == "ERROR")
                        MessageBox.Show(notifyMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        // WARNING, INFO
                        MessageBox.Show(notifyMsg, notifyTyp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Logger.Log(string.Format("[QueueTicket] Unhandled message from: {0} - MSG: {1} ", arg.RemoteInfo, qmessage.RawMessage));
                }
            }
            catch (Exception ex)
            {
                Logger.Log("QueueTicket", ex);
            }
        }

        private void PrintNewTicket(string prefix, string number, string post, string timestamp)
        {
            string header = "";

            if (_settings.PrintTicket)
            {
                short prnCopies = 0;
                
                if (post == _settings.Post0Post)
                {
                    prnCopies = _settings.Post0PrintCopies;
                    header = _settings.Post0PrintHeader;
                }
                else if (post == _settings.Post1Post)
                {
                    prnCopies = _settings.Post1PrintCopies;
                    header = _settings.Post1PrintHeader;
                }
                else if (post == _settings.Post2Post)
                {
                    prnCopies = _settings.Post2PrintCopies;
                    header = _settings.Post2PrintHeader;
                }
                else if (post == _settings.Post3Post)
                {
                    prnCopies = _settings.Post3PrintCopies;
                    header = _settings.Post3PrintHeader;
                }
                else if (post == _settings.Post4Post)
                {
                    prnCopies = _settings.Post4PrintCopies;
                    header = _settings.Post4PrintHeader;
                }
                else if (post == _settings.Post5Post)
                {
                    prnCopies = _settings.Post5PrintCopies;
                    header = _settings.Post5PrintHeader;
                }
                else if (post == _settings.Post6Post)
                {
                    prnCopies = _settings.Post6PrintCopies;
                    header = _settings.Post6PrintHeader;
                }
                else if (post == _settings.Post7Post)
                {
                    prnCopies = _settings.Post7PrintCopies;
                    header = _settings.Post7PrintHeader;
                }
                else if (post == _settings.Post8Post)
                {
                    prnCopies = _settings.Post8PrintCopies;
                    header = _settings.Post8PrintHeader;
                }
                else if (post == _settings.Post9Post)
                {
                    prnCopies = _settings.Post9PrintCopies;
                    header = _settings.Post9PrintHeader;
                }

                int i = 0;
                while (i < prnCopies)
                {
                    DrawAndPrintNewTicket(prefix + number, post, timestamp, header);
                    i++;
                }
            }

        }

        private void RequestNewTicket(string postname)
        {
            if (_client != null)
            {
                string message =
                        Msg.TicketCreate.Text + 
                        Msg.Separator + "REQ" +
                        Msg.Separator + "Identifier" +
                        Msg.Separator + postname +
                        Msg.CompDelimiter + _settings.StationName;

                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }

        #endregion

        #region Ticket Printing stuffs

        private void DrawAndPrintNewTicket(string number, string post, string timestamp, string header = "")
        {
            string msg = number + "\r\n" + post + "\r\n" + timestamp;
            Logger.Log("[QueueTicket]  PrintTicket : " + msg);

            if (header == "")
                _printJob.Header = post;
            else
                _printJob.Header = header;

            _printJob.Nomor = number;
            _printJob.TimeStamp = timestamp;
            _printJob.Footer = _settings.PrintFooter;

            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(PrintPageHandler);
                pd.DefaultPageSettings.Landscape = false;
                // pd.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                pd.DocumentName = "Ticket_" + post + "_" + number;

                // Create a new instance of Margins with 1-inch margins.
                // i-inch = 100
                Margins margins = new Margins(30, 30, 30, 30);
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

        // The PrintPage event is raised for each page to be printed. 
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
            ev.Graphics.DrawString(_printJob.Header, _printJob.HeaderFont, Brushes.Black, rect1, stringFormat);

            //yPos += printJob.HeaderFont.GetHeight(ev.Graphics);
            yPos += ev.Graphics.MeasureString(_printJob.Header, _printJob.HeaderFont, width).Height;
            Rectangle rect2 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(_printJob.TimeStamp, _printJob.DefaultFont, Brushes.Black, rect2, stringFormat2);

            //yPos += printJob.DefaultFont.GetHeight(ev.Graphics);
            yPos += ev.Graphics.MeasureString(_printJob.TimeStamp, _printJob.DefaultFont, width).Height;
            Rectangle rect3 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(_printJob.TextAntrian, _printJob.DefaultFont, Brushes.Black, rect3, stringFormat3);

            //yPos += printJob.DefaultFont.GetHeight(ev.Graphics);
            yPos += ev.Graphics.MeasureString(_printJob.TextAntrian, _printJob.DefaultFont, width).Height;
            Rectangle rect4 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(_printJob.Nomor, _printJob.NomorFont, Brushes.Black, rect4, stringFormat4);

            //yPos += printJob.NomorFont.GetHeight(ev.Graphics);
            yPos += ev.Graphics.MeasureString(_printJob.Nomor, _printJob.NomorFont, width).Height;
            Rectangle rect5 = new Rectangle(leftMargin, (int)yPos, width, height);
            ev.Graphics.DrawString(_printJob.Footer, _printJob.DefaultFont, Brushes.Black, rect5, stringFormat5);
        }

        #endregion

        #region Form event handlers

        // Send TICKET_CREATE_NEWNUMBER message to QueueServer
        private void CreateNewNumber(string text)
        {
            string postname = "";

            string post0 = _settings.Post0Post;
            string post1 = _settings.Post1Post;
            string post2 = _settings.Post2Post;
            string post3 = _settings.Post3Post;
            string post4 = _settings.Post4Post;
            string post5 = _settings.Post5Post;
            string post6 = _settings.Post6Post;
            string post7 = _settings.Post7Post;
            string post8 = _settings.Post8Post;
            string post9 = _settings.Post9Post;

            if (text == "POST0")
                postname = post0;
            else if (text == "POST1")
                postname = post1;
            else if (text == "POST2")
                postname = post2;
            else if (text == "POST3")
                postname = post3;
            else if (text == "POST4")
                postname = post4;
            else if (text == "POST5")
                postname = post5;
            else if (text == "POST6")
                postname = post6;
            else if (text == "POST7")
                postname = post7;
            else if (text == "POST8")
                postname = post8;
            else if (text == "POST9")
                postname = post9;

            RequestNewTicket(postname);
        }

        private void SaveSettings()
        {
            _settings.StartDisplayFullScreen = _isFullScreen;
            _settings.Save();
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

        private void OnFormClosing(object sender, FormClosingEventArgs e)
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
            else if (picBox == picBtnPnl5)
                picBox.Image = post5BtnImgOn;
            else if (picBox == picBtnPnl6)
                picBox.Image = post6BtnImgOn;
            else if (picBox == picBtnPnl7)
                picBox.Image = post7BtnImgOn;
            else if (picBox == picBtnPnl8)
                picBox.Image = post8BtnImgOn;
            else if (picBox == picBtnPnl9)
                picBox.Image = post9BtnImgOn;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;

            if (picBox == picBtnPnl0)
            {
                picBox.Image = post0BtnImgOff;
                CreateNewNumber("POST0");
            }
            else if (picBox == picBtnPnl1)
            {
                picBox.Image = post1BtnImgOff;
                CreateNewNumber("POST1");
            }
            else if (picBox == picBtnPnl2)
            {
                picBox.Image = post2BtnImgOff;
                CreateNewNumber("POST2");
            }
            else if (picBox == picBtnPnl3)
            {
                picBox.Image = post3BtnImgOff;
                CreateNewNumber("POST3");
            }
            else if (picBox == picBtnPnl4)
            {
                picBox.Image = post4BtnImgOff;
                CreateNewNumber("POST4");
            }
            else if (picBox == picBtnPnl5)
            {
                picBox.Image = post5BtnImgOff;
                CreateNewNumber("POST5");
            }
            else if (picBox == picBtnPnl6)
            {
                picBox.Image = post6BtnImgOff;
                CreateNewNumber("POST6");
            }
            else if (picBox == picBtnPnl7)
            {
                picBox.Image = post7BtnImgOff;
                CreateNewNumber("POST7");
            }
            else if (picBox == picBtnPnl8)
            {
                picBox.Image = post8BtnImgOff;
                CreateNewNumber("POST8");
            }
            else if (picBox == picBtnPnl9)
            {
                picBox.Image = post9BtnImgOff;
                CreateNewNumber("POST9");
            }
        }

        private void DrawButtonLabel(PaintEventArgs e, String text)
        {
            // NOTE: https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-align-drawn-text

            int fontSize = _settings.ButtonLabelFontSize;
            if (fontSize > 30)
                fontSize = 30;
            if (fontSize < 0)
                fontSize = 0;

            using (Font myFont = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point))
            {
                // Create a StringFormat object with the each line of text, and the block
                // of text centered on the page.
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center; 
                stringFormat.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(text, myFont, Brushes.Gray, e.ClipRectangle, stringFormat);
            }
        }

        private void OnButtonPaint(object sender, PaintEventArgs e)
        {

            if (_settings.DrawLabelOnButtons)
            {
                PictureBox picBox = (PictureBox)sender;

                if (picBox == picBtnPnl0)
                {
                    DrawButtonLabel(e, _settings.Post0Name);
                }
                else if (picBox == picBtnPnl1)
                {
                    DrawButtonLabel(e, _settings.Post1Name);
                }
                else if (picBox == picBtnPnl2)
                {
                    DrawButtonLabel(e, _settings.Post2Name);
                }
                else if (picBox == picBtnPnl3)
                {
                    DrawButtonLabel(e, _settings.Post3Name);
                }
                else if (picBox == picBtnPnl4)
                {
                    DrawButtonLabel(e, _settings.Post4Name);
                }
                else if (picBox == picBtnPnl5)
                {
                    DrawButtonLabel(e, _settings.Post5Name);
                }
                else if (picBox == picBtnPnl6)
                {
                    DrawButtonLabel(e, _settings.Post6Name);
                }
                else if (picBox == picBtnPnl7)
                {
                    DrawButtonLabel(e, _settings.Post7Name);
                }
                else if (picBox == picBtnPnl8)
                {
                    DrawButtonLabel(e, _settings.Post8Name);
                }
                else if (picBox == picBtnPnl9)
                {
                    DrawButtonLabel(e, _settings.Post9Name);
                }
            }
        }
    }
    
    #endregion

}
