using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

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

        /// array to list LabelRecord
        ArrayList labelRecordList;

        private delegate void ProcessMessageCallback(DataReceivedEventArgs arg, string text);
        private delegate void ProcessErrorCallBack(NotifyEventArgs e);

        private TCPClient client = null;
        private string curNumber = "";

        #endregion

        #region Constructor / Destructor
        public MainForm()
        {
            labelRecordList = new ArrayList();

            InitializeComponent();
            /// we want to receive key event
            KeyPreview = true;

            lblStation.Text = Properties.Settings.Default.StationName;
            lblPost.Text = GetStationPostCaption(Properties.Settings.Default.StationPost);
            tbServer.Text = Properties.Settings.Default.QueueServerHost;
            tbPort.Text = Properties.Settings.Default.QueueServerPort.ToString();

            string fullStationName = Properties.Settings.Default.StationName;
            string counterNo = fullStationName.Substring(fullStationName.IndexOf('#') + 1);
            string stationName = fullStationName.Substring(0, fullStationName.IndexOf('#'));

            tbStation.Text = stationName;
            cbCounterNo.Text = counterNo;
            lblNumber.Text = "";
            curNumber = "";

            /// Populate cbPost
            //string[] cbPostItems = new string[Properties.Settings.Default.UIPostList.Count];
            //Properties.Settings.Default.UIPostList.CopyTo(cbPostItems, 0);
            string[] cbPostItems = UIPostListToArray();
            cbPost.Items.Clear();
            cbPost.Items.AddRange(cbPostItems);
            cbPost.Text = Properties.Settings.Default.StationPost;

            RecordLabelSize();

            Database.Me.Connect(GetConnectionString());

            /// In basic mode, Hide "Tab Penyerahan Obat" and "Tab Selesai"
            if (Properties.Settings.Default.BasicQueueMode)
            {
                mainTab.TabPages.Remove(tabProcess);
                mainTab.TabPages.Remove(tabFinish);
            }
            else
            {
                if (Properties.Settings.Default.StationPost == Properties.Settings.Default.UpdateDisplayJobStatusPost)
                {
                    InitGridJobs();
                    InitGridJobsFin();
                }
                else
                {
                    btnRefresh.Enabled = false;
                    btnRefreshFin.Enabled = false;
                }
            }

            if (Properties.Settings.Default.ReleaseMode)
            {
            }
           
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

        public void StartClient()
        {
            client = null;

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

                /// Send LOGIN message + our station name to server
                string message = String.Empty;
                message = "LOGIN" + Msg.Separator + "CALLER" + Msg.Separator + stationName + Msg.Separator + stationPost + Msg.Separator + userName + Msg.Separator + passwordHash;
                client.Send(message);
            }
        }

        private void CloseConnection()
        {
            if (client.Connected)
                client.Stop();
        }

        #endregion

        #region Autoresize Form's Label stuffs

        /// Registered form labels are automatically resized when Form size resized
        
        /// Save initial data from labels that need to be resized
        /// Every label in the list will have its Resize event handled by OnLabelResize
        /// which will use this data
        private void RecordLabelSize()
        {
            labelRecordList.Add(new LabelRecord(capStation));
            labelRecordList.Add(new LabelRecord(capNumber));
            labelRecordList.Add(new LabelRecord(capNext));
            labelRecordList.Add(new LabelRecord(lblStation));

            labelRecordList.Add(new LabelRecord(lblPost));
            labelRecordList.Add(new LabelRecord(lblQueueCount));
            labelRecordList.Add(new LabelRecord(capCurrNumber));
            labelRecordList.Add(new LabelRecord(lblNumber));
        }

        private void ResizeLabel(LabelRecord lbl)
        {
            SuspendLayout();
            /// Get the proportionality of the resize
            float proportionalNewWidth = (float)lbl.label.Width / lbl.initialWidth;
            float proportionalNewHeight = (float)lbl.label.Height / lbl.initialHeight;

            /// Calculate the current font size
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

        #region QueueServer message handler

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
                if (text.StartsWith("CALLER"))
                    ProcessMessageCaller(arg, text);
                else if (text.StartsWith("LOGIN"))
                {
                    string _response = text;
                    if (_response == Msg.LOGIN_OK)
                    {
                        lblStatus.Text = "Connected to Server : " + client.Session.RemoteInfo + " - Post :" + Properties.Settings.Default.StationPost + "  Station:" + Properties.Settings.Default.StationName;
                        Logger.Log("QueueCaller : Successfully logged in");
                    }
                    else
                    {
                        string reason = _response.Substring(10);
                        string msg = "QueueCaller : Could not logged in to server, \r\nReason: " + reason;
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

        private void ProcessMessageCaller(DataReceivedEventArgs arg, string text)
        {
            if (text == Msg.CALLER_SET_NEXTNUMBER_NULL )
            {
                MessageBox.Show("Queue empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblQueueCount.Text = "0";
            }
            else if (text.StartsWith( Msg.CALLER_SET_NEXTNUMBER ))
                ProcessMessageCallerSetNextNumber(arg,text);
        }

        private void ProcessMessageCallerSetNextNumber(DataReceivedEventArgs arg, string text)
        {
            string _prefix, _number,_numberCount;
            _prefix = _number = _numberCount = "";

            string[] words = text.Split(Msg.Separator.ToCharArray());
            if (words.Length == 5)
            {
                _prefix = words[2];
                _number = words[3];
                _numberCount = words[4];

                curNumber = _number;
                lblNumber.Text = _prefix + _number;
                lblQueueCount.Text = _numberCount;
            }
            else
            {
                string logmsg = String.Format("Invalid CALLER:SET_NEXTNUMBER from: {0} - MSG: {1} ",arg.RemoteInfo,text);
                Logger.Log(logmsg);
                return;
            }
        }

        #endregion

        #region Form event handlers

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
            Properties.Settings.Default.Save();
        }

        private void OnNextNumber(object sender, EventArgs e)
        {
            if (client.Connected)
            {

                string message = "CALLER" + Msg.Separator + "GET_NEXTNUMBER" + Msg.Separator;
                      message += Properties.Settings.Default.StationName;
                      message += Msg.Separator + Properties.Settings.Default.StationPost;

                client.Send(message);
            }
            else
                MessageBox.Show(this, "Could not connect to server\r\nPlease restart application", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnCallNumber(object sender, EventArgs e)
        {
            if (curNumber == "0" || curNumber == "")
            {
                MessageBox.Show(this, "No number to call", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (client.Connected)
            {
                string message = "CALLER" + Msg.Separator + "RECALL_NUMBER" + Msg.Separator;
                
                message += curNumber;
                message += Msg.Separator + Properties.Settings.Default.StationName;
                message += Msg.Separator + Properties.Settings.Default.StationPost;

                client.Send(message);
            }
            else
                MessageBox.Show(this, "Could not connect to server\r\nPlease restart application", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnAbout(object sender, EventArgs e)
        {
            Form about = new AboutBox();
            about.ShowDialog();
        }

        private void OnSaveSettings(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to save changes and reconnect?"
                                                   , "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string staName = tbStation.Text + "#" + cbCounterNo.Text;
                Properties.Settings.Default.StationName = staName;
                Properties.Settings.Default.StationPost = cbPost.Text;
                Properties.Settings.Default.QueueServerHost = tbServer.Text;
                Properties.Settings.Default.QueueServerPort = Convert.ToInt32(tbPort.Text);

                CloseConnection();
                StartClient();

                lblStation.Text = staName;
                lblPost.Text = GetStationPostCaption(cbPost.Text);

                curNumber = "";
                lblNumber.Text = "";
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9:
                    {
                        btnCall.PerformClick();
                        break;
                    }
                case Keys.F12:
                    {
                        OnNextNumber(btnNext, EventArgs.Empty);
                        break;
                    }
            }
        }

        #endregion

        #region Connection  string

        private string GetConnectionString()
        {
            string partialConnStr = Properties.Settings.Default.ConnectionString;
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            builder.ConnectionString = partialConnStr;

            string encryptedPwd = Properties.Settings.Default.ConnectionStringPassword;
            string salt = Properties.Settings.Default.SecuritySalt;
            string clearPwd = Util.DecryptPassword(encryptedPwd, salt);

            builder.Add("Password", clearPwd);

            return builder.ToString();
        }

        #endregion

        #region Tab job finished

        private void InitGridJobs()
        {
            if (Database.Me.Connected)
            {
               string sql =  @"SELECT id,number,status,station,post,source,date,starttime,calltime,call2time,endtime FROM jobs
                              WHERE status = 'PROCESS'  AND date = (SELECT CAST(getdate() AS date))
                              AND post = ? ORDER BY starttime ASC";

                try
                {
                    Database.Me.OpenConnection();
                    OleDbDataAdapter sda = new OleDbDataAdapter(sql, Database.Me.Connection);
                    sda.SelectCommand.Parameters.Add("?", OleDbType.VarChar, 32).Value = Properties.Settings.Default.StationPost;

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gridJobs.DataSource = dt;

                    DataGridView gridView = gridJobs;
                    DataGridViewColumn column = null;
                    column = gridView.Columns[0];
                    column.Width = 30;
                    column.HeaderText = "Id";
                    column = gridView.Columns[1];
                    column.Width = 60;
                    column.HeaderText = "Number";
                    column = gridView.Columns[2];
                    column.Width = 75;
                    column.HeaderText = "Status";
                    column = gridView.Columns[3];
                    column.Width = 1;
                    column.HeaderText = "Station";
                    column = gridView.Columns[4];
                    column.Width = 1;
                    column.HeaderText = "Post";
                    column = gridView.Columns[5];
                    column.Width = 1;
                    column.HeaderText = "Source";
                    column = gridView.Columns[6];
                    column.Width = 1;
                    column.HeaderText = "Date";
                    column = gridView.Columns[7];
                    column.HeaderText = "Start Time";
                    column = gridView.Columns[8];
                    column.HeaderText = "Call Time";
                    column = gridView.Columns[9];
                    column.HeaderText = "Finish Time";
                    column = gridView.Columns[10];
                    column.HeaderText = "End Time";
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueCaller : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueCaller : Exception : " + e.Message);
                }
            }
        }

        private void InitGridJobsFin()
        {
            if (Database.Me.Connected)
            {
                string sql = @"SELECT id,number,status,station,post,source,date,starttime,calltime,call2time,endtime FROM jobs
                              WHERE status = 'FINISHED'  AND date = (SELECT CAST(getdate() AS date))
                              AND post = ? ORDER BY starttime ASC";

                try
                {
                    Database.Me.OpenConnection();
                    OleDbDataAdapter sda = new OleDbDataAdapter(sql, Database.Me.Connection);
                    sda.SelectCommand.Parameters.Add("?", OleDbType.VarChar, 32).Value = Properties.Settings.Default.StationPost;
                    
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gridJobsFin.DataSource = dt;

                    DataGridView gridView = gridJobsFin;
                    DataGridViewColumn column = null;
                    column = gridView.Columns[0];
                    column.Width = 30;
                    column.HeaderText = "Id";
                    column = gridView.Columns[1];
                    column.Width = 60;
                    column.HeaderText = "Number";
                    column = gridView.Columns[2];
                    column.Width = 75;
                    column.HeaderText = "Status";
                    column = gridView.Columns[3];
                    column.Width = 1;
                    column.HeaderText = "Station";
                    column = gridView.Columns[4];
                    column.Width = 1;
                    column.HeaderText = "Post";
                    column = gridView.Columns[5];
                    column.Width = 1;
                    column.HeaderText = "Source";
                    column = gridView.Columns[6];
                    column.Width = 1;
                    column.HeaderText = "Date";
                    column = gridView.Columns[7];
                    column.HeaderText = "Start Time";
                    column = gridView.Columns[8];
                    column.HeaderText = "Call Time";
                    column = gridView.Columns[9];
                    column.HeaderText = "Finish Time";
                    column = gridView.Columns[10];
                    column.HeaderText = "End Time";
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueCaller : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueCaller : Exception : " + e.Message);
                }
            }
        }

        private void OnGridJobsCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Database.Me.Connected)
            {
                if (e.RowIndex < 0)
                    return;

                string _id = Convert.ToString(gridJobs.Rows[e.RowIndex].Cells[0].Value);
                if (_id == "")
                    return;

                string msg = "Proses untuk pemgambilan obat?";

                if (MessageBox.Show(msg, "Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sql = @"UPDATE jobs SET status = 'FINISHED',call2time=getdate() WHERE id = ?";
                    try
                    {
                        Database.Me.OpenConnection();
                        using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                        {
                            Int32 number = 0;

                            if (Int32.TryParse(_id, out number))
                            {
                                cmd.Parameters.Add("?", OleDbType.Integer).Value = number;
                                cmd.ExecuteNonQuery();
                                
                                InitGridJobs();
                            }
                        }

                        // Update Job status on Display
                        UpdateDisplayJobStatus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void OnGridJobsFinCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Database.Me.Connected)
            {
                if (e.RowIndex < 0)
                    return;

                string _id = Convert.ToString(gridJobsFin.Rows[e.RowIndex].Cells[0].Value);
                if (_id == "")
                    return;

                string msg = "Tutup nomor antrian ini?";

                if (MessageBox.Show(msg, "Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sql = @"UPDATE jobs SET status = 'CLOSED',endtime=getdate() WHERE id = ?";
                    try
                    {
                        Database.Me.OpenConnection();
                        using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                        {
                            Int32 number = 0;

                            if (Int32.TryParse(_id, out number))
                            {
                                cmd.Parameters.Add("?", OleDbType.Integer).Value = number;
                                cmd.ExecuteNonQuery();

                                InitGridJobsFin();
                            }
                        }
                        // Update Job status on Display
                        UpdateDisplayJobStatus();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdateDisplayJobStatus()
        {
            if (Properties.Settings.Default.StationPost != Properties.Settings.Default.UpdateDisplayJobStatusPost)
            {
                return;
            }

            if (Database.Me.Connected)
            {
                try
                {
                    Database.Me.OpenConnection();

                    // update display
                    string sql1 = @"SELECT TOP 10 id,number, posts.numberprefix FROM jobs
                                    JOIN posts ON posts.name = jobs.post
                                    WHERE status = 'FINISHED'  AND date = (SELECT CAST(getdate() AS date))
                                    AND post = ? ORDER BY call2time ASC";

                    using (OleDbCommand cmdSelect = new OleDbCommand(sql1, Database.Me.Connection))
                    {
                        cmdSelect.Parameters.Add("?", OleDbType.VarChar, 32).Value = Properties.Settings.Default.StationPost;
                        OleDbDataReader reader = cmdSelect.ExecuteReader();
                        int _idx = 0;
                        string _msg = "";
                        //string _prefix = Tobasa.Properties.Settings.Default.NumberPrefix;

                        while (reader.Read())
                        {
                            string _number = reader.GetValue(1).ToString().Trim();
                            string _prefix = reader.GetValue(2).ToString().Trim();
                            if (_msg.Length > 0)
                                _msg += ",";

                            _msg += _prefix + _number;
                            _idx++;
                        }
                        reader.Close();

                        if (client.Connected)
                        {
                            string _message = "DISPLAY" + Msg.Separator + "UPDATE_JOB" + Msg.Separator;
                            _message += Properties.Settings.Default.StationName + Msg.Separator;
                            _message += Properties.Settings.Default.StationPost + Msg.Separator;
                            _message += _msg;
                            client.Send(_message);
                        }
                        else
                        {
                            MessageBox.Show(this, "Could not connect to server\r\nPlease restart application", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            InitGridJobs();
            InitGridJobsFin();
        }

        private void OnPageSelected(object sender, TabControlEventArgs e)
        {
            if (Properties.Settings.Default.StationPost != Properties.Settings.Default.UpdateDisplayJobStatusPost)
                return;

            TabPage page = e.TabPage;
            if (page == tabProcess )
                InitGridJobs();
            else if (page == tabFinish)
                InitGridJobsFin();
        }

        #endregion

        #region Miscs stuffs

        private string GetStationPostCaption(string post)
        {
            string postCaption = "";
            foreach (Object obj in Properties.Settings.Default.UIPostList)
            {
                string postFull = (string)obj;
                if (postFull.Contains("|"))
                {
                    char[] separators = new char[] { '|' };
                    string[] words = postFull.Split(separators);

                    if (post == words[0])
                    {
                        postCaption = words[1];
                        return postCaption;
                    }
                }
            }
            
            // default value = post
            return post; 
        }

        private string[] UIPostListToArray()
        {
            string[] cbPostItems = new string[Properties.Settings.Default.UIPostList.Count];

            for (int i = 0; i < Properties.Settings.Default.UIPostList.Count; i++)
            {
                string postFull = (string) Properties.Settings.Default.UIPostList[i];
                if (postFull.Contains("|"))
                {
                    char[] separators = new char[] { '|' };
                    string[] words = postFull.Split(separators);

                    cbPostItems[i] = words[0];
                }
                else
                    cbPostItems[i] = postFull;
            }

            return cbPostItems;
        }

        #endregion
    }
}
