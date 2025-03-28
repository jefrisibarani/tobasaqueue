#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2015-2025  Jefri Sibarani

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
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tobasa
{
    public partial class MainForm : Form
    {
        #region Member variables / class

        private delegate void NetSessionDataReceivedCb(DataReceivedEventArgs arg);
        private delegate void TCPClientNotifiedCb(NotifyEventArgs e);
        private delegate void TCPClientClosedCb(TCPClient e);

        Properties.Settings _settings = Properties.Settings.Default;
        Dictionary<string, string> _postIdsDict;
        private TCPClient _client = null;
        private string _curNumber = "";

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

        // LabelRecord collection
        ArrayList _labelRecordList;

        #endregion

        #region Constructor / Destructor
        public MainForm()
        {
            _labelRecordList = new ArrayList();
            _postIdsDict = UIPostListToDictionary();

            InitializeComponent();

            // we want to receive key event
            KeyPreview = true;

            // Restore settings
            RestoreSettings();

            string fullStationName = _settings.StationName;
            string counterNo       = fullStationName.Substring(fullStationName.IndexOf('#') + 1);
            string stationName     = fullStationName.Substring(0, fullStationName.IndexOf('#'));

            tbStation.Text   = stationName;
            cbCounterNo.Text = counterNo;
            lblNumber.Text   = "";
            _curNumber       = "";

            // Populate cbPost
            string[] cbPostItems = UIPostListToArray();
            cbPost.Items.Clear();
            cbPost.Items.AddRange(cbPostItems);
            cbPost.Text = _settings.StationPost;

            RecordLabelSize();

            postsBtnDiv.Visible = _settings.ShowPostsButtonDiv;

            SetupToolTip();

            // Start TCP client
            StartClient();

            // In basic mode, Hide "Processing" and "Tab Completed"
            if (! _settings.ManageAdvanceQueue)
            {
                mainTab.TabPages.Remove(tabProcessing);
                mainTab.TabPages.Remove(tabFinished);
            }
            else
            {
                if (_settings.ManageAllPostAdvanceQueue)
                {
                }
                else
                {
                    btnRefresh.Enabled    = false;
                    btnRefreshFin.Enabled = false;
                }
            }

            // get last queue status from server
            GetQueuetInfo();
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
                    Msg.Separator + "CALLER" +
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
                lblStatus.Text = "Not Connected to Server";
            }
        }

        public void StartClient()
        {
            _client = null;
            _client = new TCPClient(_settings.QueueServerHost, _settings.QueueServerPort);
            _client.Notified += new Action<NotifyEventArgs>(TCPClientNotified);
            _client.OnDataReceived += new DataReceived(NetSessionDataReceived);
            _client.OnConnected += new ConnectionConnected(TCPClientConnected);
            _client.OnClosed += new ConnectionClosed(TCPClientClosed);

            _client.Start();
        }

        private void CloseConnection()
        {
            if (_client != null)
                _client.Stop();
        }

        private void RestartClient()
        {
            Logger.Log("QueueCaller", "Restarting TCPClient");
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
            _labelRecordList.Add(new LabelRecord(capStation));
            _labelRecordList.Add(new LabelRecord(capNumber));
            _labelRecordList.Add(new LabelRecord(capNext));
            _labelRecordList.Add(new LabelRecord(lblStation));

            _labelRecordList.Add(new LabelRecord(lblPost));
            _labelRecordList.Add(new LabelRecord(lblQueueCount));
            _labelRecordList.Add(new LabelRecord(capCurrNumber));
            _labelRecordList.Add(new LabelRecord(lblNumber));
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
            foreach (LabelRecord lbl in _labelRecordList)
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
                if (arg.DataString.StartsWith("SYS") || arg.DataString.StartsWith("CALLER"))
                    HandleMessage(arg);
                else
                {
                    string logmsg = String.Format("[QueueCaller] Unhandled session message from: {0}", arg.RemoteInfo);
                    Logger.Log(logmsg);
                }
            }
        }

        private void HandleMessage(DataReceivedEventArgs arg)
        {
            try
            {
                Message qmessage = new Message(arg);

                Logger.Log("[QueueCaller] Processing " + qmessage.MessageType.String + " from " + arg.Session.RemoteInfo);

                // Handle SysLogin
                if (qmessage.MessageType == Msg.SysLogin && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string result = qmessage.PayloadValues["result"];
                    string data = qmessage.PayloadValues["data"];

                    if (result == "OK")
                    {
                        lblStatus.Text = "Connected to Server : " + _client.Session.RemoteInfo + " - Post :" + _settings.StationPost + "  Station:" + _settings.StationName;
                        Logger.Log("[QueueCaller] Successfully logged in");
                    }
                    else
                    {
                        string reason = data;
                        string msg = "[QueueCaller] Could not logged in to server, \r\nReason: " + reason;

                        Logger.Log(msg);
                        MessageBox.Show(this, msg, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        CloseConnection();
                    }
                }
                // Handle CallerGetInfo, CallerGetNext
                else if ((qmessage.MessageType == Msg.CallerGetInfo || qmessage.MessageType == Msg.CallerGetNext)
                            && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string prefix = qmessage.PayloadValues["postprefix"];
                    string number = qmessage.PayloadValues["number"];
                    string numberleft = qmessage.PayloadValues["numberleft"];

                    if (string.IsNullOrWhiteSpace(number))
                    {
                        if (qmessage.MessageType == Msg.CallerGetInfo)
                        {
                            _curNumber = "";
                            lblNumber.Text = "";
                        }
                        MessageBox.Show("Queue empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _curNumber = number;
                        lblNumber.Text = prefix + number;
                    }

                    lblQueueCount.Text = numberleft;
                }
                // Handle SysUpdateJob
                else if (qmessage.MessageType == Msg.SysUpdateJob && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string status = qmessage.PayloadValues["status"];
                    string result = qmessage.PayloadValues["result"];

                    RequestJobsFromServer(_settings.StationPost, "PROCESS", 0, 0);
                    RequestJobsFromServer(_settings.StationPost, "FINISHED", 0, 0);
                }
                // Handle SysGetJob
                else if (qmessage.MessageType == Msg.SysGetJob && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string status = qmessage.PayloadValues["status"];
                    string jsonDataTable = qmessage.PayloadValues["result"];

                    if (status == "PROCESS")
                        InitGridProcessedJobs(jsonDataTable);
                    else if (status == "FINISHED")
                        InitGridFinishedJobs(jsonDataTable);
                    else
                    { }
                }
                // Handle SysGetQueueSummary
                else if (qmessage.MessageType == Msg.SysGetQueueSummary && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string jsonDataTable = qmessage.PayloadValues["result"];
                    InitGridSummary(jsonDataTable);
                }
                // Handle CallerUpdateQueueLeft
                else if (qmessage.MessageType == Msg.CallerUpdateQueueLeft && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string post = qmessage.PayloadValues["post"];
                    string totalWaiting = qmessage.PayloadValues["left"];

                    if (string.IsNullOrWhiteSpace(totalWaiting))
                        return;

                    if (_settings.StationPost == post)
                    {
                        lblQueueCount.Text = totalWaiting;
                    }
                }
                else if (qmessage.MessageType == Msg.CallerRecall && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string number    = qmessage.PayloadValues["number"];
                    string post      = qmessage.PayloadValues["post"];
                    string caller    = qmessage.PayloadValues["station"];
                    string postrefix = qmessage.PayloadValues["postPrefix"];
                    if (_settings.StationPost == post && _settings.StationName == caller)
                    {
                        lblQueueCount.Text = number;
                        lblNumber.Text = postrefix + number;
                    }

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
                    Logger.Log(string.Format("[QueueCaller] Unhandled message from: {0} - MSG: {1} ", arg.RemoteInfo, qmessage.RawMessage));
                }
            }
            catch (Exception ex)
            {
                Logger.Log("QueueCaller", ex);
            }
        }

        private void RequestJobsFromServer(string post, string jobStatus, int offset = 0, int limit = 0)
        {
            if (_client != null)
            {
                string message = Msg.SysGetJob.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + post +
                                 Msg.CompDelimiter + jobStatus +
                                 Msg.CompDelimiter + "0" +
                                 Msg.CompDelimiter + "0";

                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }

        private void RequestQueueSummaryServer(string post="all")
        {
            if (_client != null)
            {
                string message = Msg.SysGetQueueSummary.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + post;

                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }

        public void UpdateJobStatus(string jobStatus, string jobId, string jobNumber)
        {
            if (_client != null)
            {
                string message = Msg.SysUpdateJob.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + jobStatus +
                                 Msg.CompDelimiter + jobId +
                                 Msg.CompDelimiter + jobNumber;

                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }

        private void GetQueuetInfo()
        {
            if (_client != null)
            {
                string message = Msg.CallerGetInfo.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + _settings.StationPost;

                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }

        private void GetNextNumber()
        {
            if (_client != null)
            {
                string message = Msg.CallerGetNext.Text +
                             Msg.Separator + "REQ" +
                             Msg.Separator + "Identifier" +
                             Msg.Separator + _settings.StationPost +
                             Msg.CompDelimiter + _settings.StationName;
                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }

        private void RecallNumber()
        {
            if (_curNumber == "0" || _curNumber == "")
            {
                MessageBox.Show(this, "No number to call", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_client != null)
            {
                string message = Msg.CallerRecall.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + _curNumber +
                                 Msg.CompDelimiter + _settings.StationPost +
                                 Msg.CompDelimiter + _settings.StationName;

                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }

        public void CallAgain(String number)
        {
            if (number == "0" || number == "")
            {
                MessageBox.Show(this, "Invalid number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_client != null)
            {
                string message = Msg.CallerRecall.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + number +
                                 Msg.CompDelimiter + _settings.StationPost +
                                 Msg.CompDelimiter + _settings.StationName;

                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }

        #endregion

        #region Form event handlers

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
            _settings.Save();
        }

        private void OnNextNumber(object sender, EventArgs e)
        {
            GetNextNumber();
        }

        private void OnCallNumber(object sender, EventArgs e)
        {
            RecallNumber();
        }

        private void OnAbout(object sender, EventArgs e)
        {
            Form about = new AboutBox();
            about.ShowDialog();
        }

        private void OnSaveSettingsReconnect(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to save changes and reconnect?",
                                                  "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveSettings();

                RestartClient();

                lblStation.Text = _settings.StationName;
                lblPost.Text = GetStationPostCaption(cbPost.Text);

                // get last queue status from server
                GetQueuetInfo();
            }
        }

        private void OnSaveSettings(object sender, EventArgs e)
        {
            SaveSettings();
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

        private void OnCbCounterIndexChanged(object sender, EventArgs e)
        {
            string cbText = cbCounterNo.Text;

            // ASCII characters: 65 to 90
            // we use 1 = A = 65 ,  2 = B = 66
            int staId = Convert.ToInt32(cbText);
            staId = 64 + staId;
            string stChar = ((char)staId).ToString();

            lblCtrNoChar.Text = stChar;
        }

        private void OnChangePost(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want change Post?"
                                                   , "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string postId;
                string fullStationName = _settings.StationName;
                string counterNo = fullStationName.Substring(fullStationName.IndexOf('#') + 1);
                string stationName = fullStationName.Substring(0, fullStationName.IndexOf('#'));

                if ((Button)sender == btnChangePost0)
                {
                    postId = "POST0";
                }
                else if ((Button)sender == btnChangePost1)
                {
                    postId = "POST1";
                }
                else if ((Button)sender == btnChangePost2)
                {
                    postId = "POST2";
                }
                else if ((Button)sender == btnChangePost3)
                {
                    postId = "POST3";
                }
                else if ((Button)sender == btnChangePost4)
                {
                    postId = "POST4";
                }
                else if ((Button)sender == btnChangePost5)
                {
                    postId = "POST5";
                }
                else if ((Button)sender == btnChangePost6)
                {
                    postId = "POST6";
                }
                else if ((Button)sender == btnChangePost7)
                {
                    postId = "POST7";
                }
                else if ((Button)sender == btnChangePost8)
                {
                    postId = "POST8";
                }
                else if ((Button)sender == btnChangePost9)
                {
                    postId = "POST9";
                    _settings.StationName = fullStationName;
                }
                else
                    postId = cbPost.Text;

                cbPost.Text = postId;

                _settings.StationPost = postId;

                RestartClient();

                lblStation.Text = fullStationName;
                lblPost.Text = GetStationPostCaption(postId);

                // get last queue status from server
                GetQueuetInfo();
            }
        }

        #endregion

        #region Tab job finished

        private void InitGridProcessedJobs(string jsonDataTable = "")
        {
            try
            {
                DataTable dataTable = null;
                dataTable = (DataTable)JsonConvert.DeserializeObject(jsonDataTable, (typeof(DataTable)));

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    gridJobs.DataSource = null;
                    return;
                }

                gridJobs.DataSource = dataTable;

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
            catch (Exception e)
            {
                Logger.Log("QueueCaller", e);
            }
        }

        private void InitGridFinishedJobs(string jsonDataTable = "")
        {
            try
            {
                DataTable dataTable = null;
                dataTable = (DataTable)JsonConvert.DeserializeObject(jsonDataTable, (typeof(DataTable)));

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    gridJobsFin.DataSource = null;
                    return;
                }

                gridJobsFin.DataSource = dataTable;

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
            catch (Exception e)
            {
                Logger.Log("QueueCaller", e);
            }
        }

        private void InitGridSummary(string jsonDataTable = "")
        {
            try
            {
                DataTable dataTable = null;
                dataTable = (DataTable)JsonConvert.DeserializeObject(jsonDataTable, (typeof(DataTable)));

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    gridQueueSummary.DataSource = null;
                    return;
                }

                gridQueueSummary.DataSource = dataTable;

                DataGridView gridView = gridQueueSummary;
                DataGridViewColumn column = null;
                column = gridView.Columns[0];
                column.Width = 40;
                column.HeaderText = "Post";

                column = gridView.Columns[1];
                column.Width = 40;
                column.HeaderText = "Prefix";

                column = gridView.Columns[2];
                column.Width = 40;
                column.HeaderText = "Note";

                column = gridView.Columns[3];
                column.Width = 35;
                column.HeaderText = "Last Call";

                column = gridView.Columns[4];
                column.Width = 35;
                column.HeaderText = "Total Called";

                column = gridView.Columns[5];
                column.Width = 45;
                column.HeaderText = "First Waiting";

                column = gridView.Columns[6];
                column.Width = 45;
                column.HeaderText = "Last Waiting";

                column = gridView.Columns[7];
                column.Width = 45;
                column.HeaderText = "Total Waiting";

                column = gridView.Columns[8];
                column.Width = 70;
                column.HeaderText = "Last Station";
            }
            catch (Exception e)
            {
                Logger.Log("QueueCaller", e);
            }
        }

        private void OnGridJobsCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string jobId = Convert.ToString(gridJobs.Rows[e.RowIndex].Cells[0].Value);
            string jobNo = Convert.ToString(gridJobs.Rows[e.RowIndex].Cells[1].Value);

            if (jobId == "")
                return;

            string msg = $"Set this queue number {jobNo} status to FINISHED ?";

            if (MessageBox.Show(msg, "Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UpdateJobStatus("FINISHED", jobId, jobNo);
            }
        }

        private void OnGridJobsFinCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string jobId = Convert.ToString(gridJobsFin.Rows[e.RowIndex].Cells[0].Value);
            string jobNo = Convert.ToString(gridJobsFin.Rows[e.RowIndex].Cells[1].Value);

            if (jobId == "")
                return;

            string msg = $"Set this queue number {jobNo} status to CLOSED ?";

            if (MessageBox.Show(msg, "Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UpdateJobStatus("CLOSED", jobId, jobNo);
            }
        }

        private void OnGridJobsCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (e.RowIndex < 0)
                return;

            string jobId = Convert.ToString(gridJobs.Rows[e.RowIndex].Cells[0].Value);
            string jobNo = Convert.ToString(gridJobs.Rows[e.RowIndex].Cells[1].Value);

            if (jobId == "")
                return;


            // Select the row where right-click happened
            gridJobs.ClearSelection();
            gridJobs.Rows[e.RowIndex].Selected = true;

            string msg = $@"Call number {jobNo} again?";
            if (MessageBox.Show(msg, "Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CallAgain(jobNo);
            }

            //ActionDialog.Show(this, jobId, jobNo);
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            if ((Button)sender == btnRefresh)
                RequestJobsFromServer(_settings.StationPost,"PROCESS",0,0);
            else if ((Button)sender == btnRefreshFin)
                RequestJobsFromServer(_settings.StationPost,"FINISHED",0,0);
            else if ((Button)sender == btnRefreshSummary)
                RequestQueueSummaryServer(_settings.StationPost);
        }

        private void OnPageSelected(object sender, TabControlEventArgs e)
        {
            //if (!_settings.ManageAllPostAdvanceQueue)
            //    return;

            TabPage page = e.TabPage;
            
            if (page == tabProcessing )
                RequestJobsFromServer(_settings.StationPost,"PROCESS",0,0);
            else if (page == tabFinished)
                RequestJobsFromServer(_settings.StationPost,"FINISHED",0,0);
            else if (page == tabSummary)
                RequestQueueSummaryServer(_settings.StationPost);
        }

		#endregion

		#region Settings

        private void SaveSettings()
        {
            string staName = tbStation.Text + "#" + cbCounterNo.Text;
            _settings.StationName = staName;
            _settings.StationPost = cbPost.Text;
            _settings.QueueServerHost = tbServer.Text;
            _settings.QueueServerPort = Convert.ToInt32(tbPort.Text);

            _settings.ShowPostsButtonDiv = chkShowSwitchPostsButtons.Checked;
            _settings.ManageAdvanceQueue = chkManageAdvanceQueue.Checked;
            _settings.ManageAllPostAdvanceQueue = chkManageAllPostAdvanceQueue.Checked;
        }

        private void RestoreSettings()
        {
            lblStation.Text = _settings.StationName;
            lblPost.Text = GetStationPostCaption(_settings.StationPost);
            tbServer.Text = _settings.QueueServerHost;
            tbPort.Text = _settings.QueueServerPort.ToString();
            chkShowSwitchPostsButtons.Checked = _settings.ShowPostsButtonDiv;
            chkManageAdvanceQueue.Checked = _settings.ManageAdvanceQueue;
            chkManageAllPostAdvanceQueue.Checked = _settings.ManageAllPostAdvanceQueue;
        }

		#endregion

		#region Miscs stuffs

		private string GetStationPostCaption(string post)
        {
            string postCaption = "";
            foreach (Object obj in _settings.UIPostList)
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
            string[] cbPostItems = new string[_settings.UIPostList.Count];

            for (int i = 0; i < _settings.UIPostList.Count; i++)
            {
                string postFull = (string) _settings.UIPostList[i];
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

        private Dictionary<string, string> UIPostListToDictionary()
        {
            Dictionary<string, string> postIds = new Dictionary<string, string>();

            for (int i = 0; i < _settings.UIPostList.Count; i++)
            {
                string postFull = (string)_settings.UIPostList[i];
                if (postFull.Contains("|"))
                {
                    char[] separators = new char[] { '|' };
                    string[] words = postFull.Split(separators);

                    postIds.Add(words[0],words[1]);
                }
            }

            return postIds;
        }

        private void SetupToolTip()
        {
            toolTip.InitialDelay = 50;
            toolTip.ReshowDelay = 100;
            toolTip.AutoPopDelay = 5000;

            toolTip.SetToolTip(btnChangePost0, _postIdsDict["POST0"]);
            toolTip.SetToolTip(btnChangePost1, _postIdsDict["POST1"]);
            toolTip.SetToolTip(btnChangePost2, _postIdsDict["POST2"]);
            toolTip.SetToolTip(btnChangePost3, _postIdsDict["POST3"]);
            toolTip.SetToolTip(btnChangePost4, _postIdsDict["POST4"]);
            toolTip.SetToolTip(btnChangePost5, _postIdsDict["POST5"]);
            toolTip.SetToolTip(btnChangePost6, _postIdsDict["POST6"]);
            toolTip.SetToolTip(btnChangePost7, _postIdsDict["POST7"]);
            toolTip.SetToolTip(btnChangePost8, _postIdsDict["POST8"]);
            toolTip.SetToolTip(btnChangePost9, _postIdsDict["POST9"]);
        }

        #endregion
    }
}
