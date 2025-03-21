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
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Tobasa
{
    public partial class MainForm : Form
    {
        #region Member variables

        private delegate void NetSessionDataReceivedCb(DataReceivedEventArgs arg);
        private delegate void TCPClientNotifiedCb(NotifyEventArgs arg);
        private delegate void TCPClientClosedCb(TCPClient e);
        private delegate void LogServerMessageCb(string text);

        private Tobasa.Properties.Settings _settings = Tobasa.Properties.Settings.Default;
        private TCPClient _client = null;
        public event MessageReceived MessageReceived;

        public TCPClient TcpClient
        {
            get => _client;
        }

        Dictionary<string, TableProp> _tableProps = new Dictionary<string, TableProp>()
        {
            [Tbl.runningtexts] = new TableProp(Tbl.runningtexts),
            [Tbl.ipaccesslists] = new TableProp(Tbl.ipaccesslists),
            [Tbl.stations] = new TableProp(Tbl.stations),
            [Tbl.posts] = new TableProp(Tbl.posts),
            [Tbl.logins] = new TableProp(Tbl.logins)
        };

        #endregion

        #region Form Starting/Stopping
        public MainForm()
        {
            InitializeComponent();
            gbEnryptTool.Visible = true;
            tabControl.TabPages.Remove(tabPageDiag);
            StartClient();
        }

        private void OnFormShown(object sender, EventArgs e)
        {
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
            _settings.Save();
        }

        private void OnLoggedOn()
        {
            if (_client != null)
            {
                RequestTableFromServer(Tbl.runningtexts);
                RequestTableFromServer(Tbl.posts);
                RequestTableFromServer(Tbl.logins);
                RequestTableFromServer(Tbl.stations);
                RequestTableFromServer(Tbl.ipaccesslists);
            }
        }

        #endregion

        #region Encryption stuff

        private void OnEncryptCheck(object sender, EventArgs e)
        {
            if (rbBF.Checked)
            {
                btnDecrypt.Enabled = true;
                btnEncrypt.Text = "Encrypt";
                tbInput.Enabled = true;
            }
            else if (rbSHA.Checked)
            {
                btnDecrypt.Enabled = false;
                btnEncrypt.Text = "Get Hash";
                tbInput.Enabled = false;
            }
        }

        private void OnDecrypt(object sender, EventArgs e)
        {
            try
            {
                string output = Util.DecryptBlowFish(tbKey.Text, tbSalt.Text, tbInput.Text);
                tbOutput.Text = output;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
        }

        private void OnEncrypt(object sender, EventArgs e)
        {

            if (rbBF.Checked)
            {
                try
                {
                    string output = Util.EncryptBlowFish(tbKey.Text, tbSalt.Text, tbInput.Text);
                    tbOutput.Text = output;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                }
            }
            else if (rbSHA.Checked)
            {
                try
                {
                    bool usesalt = chkSalt.Checked;

                    string output = Tobasa.Util.ComputeHashAsString(tbKey.Text, tbSalt.Text, usesalt);
                    tbOutput.Text = output;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                }
            }
        }

        private void OnCreatePasswordHash(object sender, EventArgs e)
        {
            try
            {
                string output = Util.GetPasswordHash(txtEPass.Text, txtEName.Text);
                txtEResult.Text = output;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
        }

        private void OnCreateEncryptedPassword(object sender, EventArgs e)
        {
            string salt = Properties.Settings.Default.SecuritySalt;
            string output = Util.EncryptPassword(txtCSPass.Text, salt);

            txtECSPass.Text = output;
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
                NetSessionDataReceivedCb dlg = new NetSessionDataReceivedCb(NetSessionDataReceived);
                this.Invoke(dlg, new object[] { arg });
            }
            else
            {
                if (arg.DataString.StartsWith("SYS"))
                    HandleMessage(arg);
                else
                {
                    LogServerMessage(string.Format("[Info] {0}", arg.DataString));
                    string logmsg = String.Format("[QueueAdmin] Unhandled session message from: {0}", arg.RemoteInfo);
                    Logger.Log(logmsg);
                }
            }
        }

        private void HandleMessage(DataReceivedEventArgs arg)
        {
            try
            {
                Message qmessage = new Message(arg);

                Logger.Log("[QueueAdmin] Processing " + qmessage.MessageType.String + " from " + arg.Session.RemoteInfo);

                // Handle SysLogin
                if (qmessage.MessageType == Msg.SysLogin && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string result = qmessage.PayloadValues["result"];
                    string data   = qmessage.PayloadValues["data"];

                    if (result == "OK")
                    {
                        lblStatus.Text = "Connected to Server : " + _client.Session.RemoteInfo + " - Post :" + _settings.StationPost + "  Station:" + _settings.StationName;
                        Logger.Log("[QueueAdmin] Successfully logged in");
                        
                        OnLoggedOn();
                    }
                    else
                    {
                        string reason = data;
                        string msg = "[QueueAdmin] Could not logged in to server, reason: " + reason;

                        Logger.Log(msg);
                        MessageBox.Show(this, msg, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        CloseConnection();
                    }
                }
                // Handle SysDelTable, SysUpdTable, SysInsTable
                else if ((qmessage.MessageType == Msg.SysDelTable ||
                          qmessage.MessageType == Msg.SysUpdTable ||
                          qmessage.MessageType == Msg.SysInsTable ) &&
                          qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string tableName = qmessage.PayloadValues["tablename"];
                    string result    = qmessage.PayloadValues["result"];
                    string affected  = qmessage.PayloadValues["affected"];

                    string cmdType;
                    if (qmessage.MessageType == Msg.SysDelTable)
                        cmdType = "deleted";
                    else if (qmessage.MessageType == Msg.SysUpdTable)
                        cmdType = "updated";
                    else
                        cmdType = "inserted";

                    LogServerMessage(string.Format("[Info] {0} row {1}, table {2}", affected, cmdType, tableName));

                    // Update relevant grid view
                    RequestTableFromServer(tableName);
                }
                // Handle SysGetTable
                else if (qmessage.MessageType == Msg.SysGetTable && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string tableName     = qmessage.PayloadValues["tablename"];
                    string jsonDataTable = qmessage.PayloadValues["result"];
                    string totalrow      = qmessage.PayloadValues["totalrow"];

                    int totalRowInt = Convert.ToInt32(totalrow);

                    if (tableName == Tbl.runningtexts)
                    {
                        _tableProps[tableName].TotalRows = totalRowInt;
                        InitGridRunningText(jsonDataTable);
                        tbReordStatus.Text = _tableProps[tableName].NavigationStatus;
                        btnDeleteData.Enabled = totalRowInt > 0;
                    }
                    else if (tableName == Tbl.ipaccesslists)
                    {
                        _tableProps[tableName].TotalRows = totalRowInt;
                        InitGridIpAccessList(jsonDataTable);
                        tbReordStatus.Text = _tableProps[tableName].NavigationStatus;
                        btnDeleteData.Enabled = totalRowInt > 0;
                    }
                    else if (tableName == Tbl.stations)
                    {
                        _tableProps[tableName].TotalRows = totalRowInt;
                        InitGridStation(jsonDataTable);
                        tbReordStatus.Text = _tableProps[tableName].NavigationStatus;
                        btnDeleteData.Enabled = totalRowInt > 0;
                    }
                    else if (tableName == Tbl.posts)
                    {
                        _tableProps[tableName].TotalRows = totalRowInt;
                        InitGridPost(jsonDataTable);
                        tbReordStatus.Text = _tableProps[tableName].NavigationStatus;
                        btnDeleteData.Enabled = totalRowInt > 0;
                    }
                    else if (tableName == Tbl.logins)
                    {
                        _tableProps[tableName].TotalRows = totalRowInt;
                        InitGridLogin(jsonDataTable);
                        tbReordStatus.Text = _tableProps[tableName].NavigationStatus;
                        btnDeleteData.Enabled = totalRowInt > 0;
                    }
                    else
                    { 
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

                    LogServerMessage(string.Format("[{0}] {1}", notifyTyp, notifyMsg));
                }
                else
                { }

                // Forward message to subform, by executing registered subform's handler
                MessageReceived?.Invoke(qmessage);
            }
            catch (Exception e)
            {
                Logger.Log("QueueAdmin", e);
            }
        }
        private void LogServerMessage(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                LogServerMessageCb dlg = new LogServerMessageCb(LogServerMessage);
                this.Invoke(dlg, new object[] { text });
            }
            else
            {
                string logmessage = DateTime.Now.ToString("yyyy-MM-dd") + " " +
                                    DateTime.Now.ToString("HH:mm:ss") + " : " +
                                    text;

                tbServerMessages.AppendText(logmessage + Environment.NewLine);
            }
        }

        #endregion

        #region Request/Delete table content from server
        
        private void RequestTableFromServer(string tablename)
        {
            _tableProps[tablename].RequestTableFromServer(this);
        }

        private void DeleteTableFromServer(string tablename, string param, string param1="")
        {
            if (_client != null)
            {
                string message = Msg.SysDelTable.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + tablename +
                                 Msg.CompDelimiter + param;

                if (!string.IsNullOrWhiteSpace(param1))
                {
                    message += Msg.CompDelimiter + param1;
                }
                else
                {
                    message += Msg.CompDelimiter + "unused";
                }

                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
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

                // SYS|LOGIN|REQ|[Module!Post!Station!Username!Password]
                string message =
                    Msg.SysLogin.Text +
                    Msg.Separator + "REQ" +
                    Msg.Separator + "ADMIN" +
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

            _client.Start();
        }

        private void CloseConnection()
        {
            if (_client != null)
                _client.Stop();
        }

        private void RestartClient()
        {
            Logger.Log("QueueAdmin", "Restarting TCPClient");
            CloseConnection();
            StartClient();
        }

        #endregion

        #region Connection  Send data tests
        /** WARNING/PERINGATAN 
         * OnSendXXXData(), OnSendData(), SendData() sudah tidak dimaintain.
         * Digunakan untuk test/diagnostic awal development
         */
        private void OnSendXXXData(object sender, EventArgs e)
        {
            if (_client != null)
            {
                if (chkSend1024.Checked)
                    SendData(1024);
                else if (chkSend1536.Checked)
                    SendData(1536);
                else if (chkSend2048.Checked)
                    SendData(2048);
                else if (chkSend4096.Checked)
                    SendData(4096);
            }
            else
                Util.ShowConnectionError(this);
        }
        /** WARNING/PERINGATAN 
         * OnSendXXXData(), OnSendData(), SendData() sudah tidak dimaintain.
         * Digunakan untuk test/diagnostic awal development
         */
        private void OnSendData(object sender, EventArgs e)
        {
            if (_client != null)
            {
                string message = cbCmd.Text;
                _client.Send(message);
            }
            else
                Util.ShowConnectionError(this);
        }
        /** WARNING/PERINGATAN 
         * OnSendXXXData(), OnSendData(), SendData() sudah tidak dimaintain.
         * Digunakan untuk test/diagnostic awal development
         */
        private void SendData(int length)
        {
            if (_client == null)
                return;

            int byteLength = length;
            byte[] dataToSend = new Byte[byteLength];

            char cx = 'X';
            for (int i = 0; i < byteLength; i++)
            {
                byte bx1 = (byte)cx;
                byte bx2 = Convert.ToByte(cx);
                dataToSend[i] = bx2;
                if (i== byteLength-1)
                    dataToSend[i] = Convert.ToByte('_');
            }

            string txt = Encoding.ASCII.GetString(dataToSend, 0, dataToSend.Length);
            int lgth = txt.Length;

            _client.Send(txt);
        }

        #endregion

        #region Init DataGridView

        private void InitGridIpAccessList(string jsonDataTable)
        {
            try
            {
                DataTable dataTable = null;
                dataTable = (DataTable)JsonConvert.DeserializeObject(jsonDataTable, (typeof(DataTable)));

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    gridIpAccess.DataSource = null;
                    return;
                }

                gridIpAccess.DataSource = dataTable;
                DataGridView gridView = gridIpAccess;

                DataGridViewColumn column = null;
                column = gridView.Columns[0];
                    column.Width = 100;
                    column.HeaderText = "IP Address";
                column = gridView.Columns[1];
                    column.Width = 50;
                    column.HeaderText = "Allowed";
                column = gridView.Columns[2];
                    column.Width = 347;
                    column.HeaderText = "Remark";
            }
            catch (Exception e)
            {
                Logger.Log("QueueAdmin", e);
            }
        }

        private void InitGridStation(string jsonDataTable)
        {
            try
            {
                DataTable dataTable = null;
                dataTable = (DataTable)JsonConvert.DeserializeObject(jsonDataTable, (typeof(DataTable)));

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    gridStations.DataSource = null;
                    return;
                }

                gridStations.DataSource = dataTable;
                DataGridView gridView = gridStations;

                DataGridViewColumn column = null;
                column = gridView.Columns[0];
                    column.Width = 80;
                    column.HeaderText = "Station";
                column = gridView.Columns[1];
                    column.Width = 81;
                    column.HeaderText = "Post";
                column = gridView.Columns[2];
                    column.Width = 70;
                    column.HeaderText = "Can Login";
                column = gridView.Columns[3];
                    column.Width = 266;
                    column.HeaderText = "Remark";
            }
            catch (Exception e)
            {
                Logger.Log("QueueAdmin", e);
            }
        }

        private void InitGridPost(string jsonDataTable)
        {
            try
            {
                DataTable dataTable = null;
                dataTable = (DataTable)JsonConvert.DeserializeObject(jsonDataTable, (typeof(DataTable)));

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    gridPosts.DataSource = null;
                    return;
                }

                gridPosts.DataSource = dataTable;
                DataGridView gridView = gridPosts;

                DataGridViewColumn column = null;
                column = gridView.Columns[0];
                    column.Width = 60;
                    column.HeaderText = "Post";
                column = gridView.Columns[1];
                    column.Width = 60;
                    column.HeaderText = "Prefix";
                column = gridView.Columns[2];
                    column.Width = 230;
                    column.HeaderText = "Remark";
                column = gridView.Columns[3];
                    column.Width = 70;
                    column.HeaderText = "Quota 0";
                column = gridView.Columns[4];
                    column.Width = 70;
                    column.HeaderText = "Quota 1";

            }
            catch (Exception e)
            {
                Logger.Log("QueueAdmin", e);
            }
        }

        private void InitGridLogin(string jsonDataTable)
        {
            try
            {
                DataTable dataTable = null;
                dataTable = (DataTable)JsonConvert.DeserializeObject(jsonDataTable, (typeof(DataTable)));

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    gridLogins.DataSource = null;
                    return;
                }

                gridLogins.DataSource = dataTable;
                DataGridView gridView = gridLogins;

                DataGridViewColumn column = null;
                column = gridView.Columns[0];
                    column.Width = 100;
                    column.HeaderText = "User Name";
                column = gridView.Columns[1];
                    column.Width = 265;
                    column.HeaderText = "Password";
                column = gridView.Columns[2];
                    column.Width = 81;
                    column.HeaderText = "Expired";
                column = gridView.Columns[3];
                    column.Width = 50;
                    column.HeaderText = "Active";
            }
            catch (Exception e)
            {
                Logger.Log("QueueAdmin", e);
            }
        }

        private void InitGridRunningText(string jsonDataTable)
        {
            try
            {
                DataTable dataTable = null;
                dataTable = (DataTable)JsonConvert.DeserializeObject(jsonDataTable, (typeof(DataTable)));

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    gridRunText.DataSource = null;
                    return;
                }

                gridRunText.DataSource = dataTable;
                DataGridView gridView = gridRunText;

                DataGridViewColumn column = null;
                column = gridView.Columns[0];
                    column.Width = 20;
                    column.HeaderText = "ID";
                column = gridView.Columns[1];
                    column.Width = 70;
                    column.HeaderText = "Station";
                column = gridView.Columns[2];
                    column.Width = 40;
                    column.HeaderText = "Sticky";
                column = gridView.Columns[3];
                    column.Width = 40;
                    column.HeaderText = "Active";
                column = gridView.Columns[4];
                    column.Width = 325;
                    column.HeaderText = "Running Text";
            }
            catch (Exception e)
            {
                Logger.Log("QueueAdmin", e);
            }
        }

        #endregion

        #region Add/Delete table data

        private void AddDataIPAccess()
        {
            FormIPAccess form = new FormIPAccess(this);
            form.ShowDialog();
        }

        private void AddDataStation()
        {
            FormStation form = new FormStation(this);
            form.ShowDialog();
        }

        private void AddDataPost()
        {
            FormPost form = new FormPost(this);
            form.ShowDialog();
        }

        private void AddDataLogin()
        {
            FormLogin form = new FormLogin(this);
            form.ShowDialog();
        }

        private void AddDataRunningText()
        {
            FormRunText form = new FormRunText(this);
            form.ShowDialog();
        }

        private void DeleteDataRunningText()
        {
            try
            {
                if (gridRunText.RowCount == 0)
                    return;

                int row = gridRunText.CurrentCell.RowIndex;
                int id = Convert.ToInt32(gridRunText.Rows[row].Cells[0].Value);
                if (id < 0)
                    return;

                if (MessageBox.Show(this,"Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                DeleteTableFromServer(Tbl.runningtexts, id.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteDataIpAccess()
        {
            try
            {
                if (gridIpAccess.RowCount == 0)
                    return;

                int row = gridIpAccess.CurrentCell.RowIndex;
                string ipaddress = Convert.ToString(gridIpAccess.Rows[row].Cells[0].Value);
                if (ipaddress == "")
                    return;

                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                DeleteTableFromServer(Tbl.ipaccesslists, ipaddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteDataStation()
        {
            try
            {
                if (gridStations.RowCount == 0)
                    return;

                int row = gridStations.CurrentCell.RowIndex;
                string staName = Convert.ToString(gridStations.Rows[row].Cells[0].Value);
                string postName = Convert.ToString(gridStations.Rows[row].Cells[1].Value);

                if (staName == "" || postName == "")
                    return;

                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                DeleteTableFromServer(Tbl.stations, staName, postName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteDataPost()
        {
            try
            {
                if (gridPosts.RowCount == 0)
                    return;

                int row = gridPosts.CurrentCell.RowIndex;
                string postName = Convert.ToString(gridPosts.Rows[row].Cells[0].Value);
                if (postName == "")
                    return;

                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                DeleteTableFromServer(Tbl.posts, postName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteDataLogin()
        {
            try
            {
                if (gridLogins.RowCount == 0)
                    return;

                int row = gridLogins.CurrentCell.RowIndex;
                string usrName = Convert.ToString(gridLogins.Rows[row].Cells[0].Value);
                if (usrName == "")
                    return;

                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                DeleteTableFromServer(Tbl.logins, usrName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Other form handler
        private void OnSubformDataChanged(string tablename)
        {
            RequestTableFromServer(tablename);
        }

        private void OnTabPageSelected(object sender, TabControlEventArgs e)
        {
            TabPage page = e.TabPage;

            if (page == tabIpAccessList)
                RequestTableFromServer(Tbl.ipaccesslists);
            else if (page == tabStation)
                RequestTableFromServer(Tbl.stations);
            if (page == tabLogin)
                RequestTableFromServer(Tbl.logins);
            else if (page == tabPost)
                RequestTableFromServer(Tbl.posts);
            else if (page == tabRunText)
                RequestTableFromServer(Tbl.runningtexts);
        }

        private void OnGridIpAccessSelectionChanged(object sender, EventArgs e)
        {
            //int row = gridIpAccess.CurrentCell.RowIndex;
            //Logger.Log("[QueueAdmin] Current row is " + row);
        }

        private void OnAbout(object sender, EventArgs e)
        {
            Form about = new AboutBox();
            about.ShowDialog();
        }

        private void OnOptions(object sender, EventArgs e)
        {

        }

        private void OnExit(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit application?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        #endregion

        #region Table Grid double  Click handler

        private void OnGgridIpAccessListCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string ipaddress = Convert.ToString(gridIpAccess.Rows[e.RowIndex].Cells[0].Value);
            string allowed   = Convert.ToString(gridIpAccess.Rows[e.RowIndex].Cells[1].Value);
            string remark    = Convert.ToString(gridIpAccess.Rows[e.RowIndex].Cells[2].Value);
            
            if (ipaddress == "")
                return;

            Dictionary<string, string> record = new Dictionary<string, string>
            {
                ["ipaddress"] = ipaddress.Trim(),
                ["allowed"]   = allowed.Trim(),
                ["remark"]    = remark.Trim()
            };

            FormIPAccess form = new FormIPAccess(this, record);
            form.ShowDialog();
        }

        private void OnGgridStationsCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string name       = Convert.ToString(gridStations.Rows[e.RowIndex].Cells[0].Value);
            string post       = Convert.ToString(gridStations.Rows[e.RowIndex].Cells[1].Value);
            string canlogin   = Convert.ToString(gridStations.Rows[e.RowIndex].Cells[2].Value);
            string keterangan = Convert.ToString(gridStations.Rows[e.RowIndex].Cells[3].Value);
            
            if (name == "" || post == "")
                return;

            Dictionary<string, string> record = new Dictionary<string, string>
            {
                ["name"]        = name.Trim(),
                ["post"]        = post.Trim(),
                ["canlogin"]    = canlogin.Trim(),
                ["keterangan"]  = keterangan.Trim()
            };

            FormStation form = new FormStation(this, record);
            form.ShowDialog();
        }

        private void OnGridPostsCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string postname = Convert.ToString(gridPosts.Rows[e.RowIndex].Cells[0].Value);
            string prefix   = Convert.ToString(gridPosts.Rows[e.RowIndex].Cells[1].Value);
            string remark   = Convert.ToString(gridPosts.Rows[e.RowIndex].Cells[2].Value);
            string quota0   = Convert.ToString(gridPosts.Rows[e.RowIndex].Cells[3].Value);
            string quota1   = Convert.ToString(gridPosts.Rows[e.RowIndex].Cells[4].Value);

            if (postname == "")
                return;

            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "postname", postname.Trim() },
                { "remark",   remark.Trim() },
                { "prefix",   prefix.Trim() },
                { "quota0",   quota0.Trim() },
                { "quota1",   quota1.Trim() }
            };

            FormPost form = new FormPost(this, data);
            form.ShowDialog();
        }

        private void OnGridLoginsCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string usrName  = Convert.ToString(gridLogins.Rows[e.RowIndex].Cells[0].Value);
            string password = Convert.ToString(gridLogins.Rows[e.RowIndex].Cells[1].Value);
            string expired  = Convert.ToString(gridLogins.Rows[e.RowIndex].Cells[2].Value);
            string active   = Convert.ToString(gridLogins.Rows[e.RowIndex].Cells[3].Value);

            if (usrName == "")
                return;

            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "username", usrName.Trim() },
                { "password", password.Trim() },
                { "expired",  expired.Trim() },
                { "active",   active.Trim() }
            };

            FormLogin form = new FormLogin(this, data);
            form.ShowDialog();
        }

        private void OnGridRunTextCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string id      = Convert.ToString(gridRunText.Rows[e.RowIndex].Cells[0].Value);
            string station = Convert.ToString(gridRunText.Rows[e.RowIndex].Cells[1].Value);
            string sticky  = Convert.ToString(gridRunText.Rows[e.RowIndex].Cells[2].Value);
            string active  = Convert.ToString(gridRunText.Rows[e.RowIndex].Cells[3].Value);
            string runText = Convert.ToString(gridRunText.Rows[e.RowIndex].Cells[4].Value);

            if (id == "")
                return;

            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "id",      id.Trim() },
                { "station", station.Trim() },
                { "sticky",  sticky.Trim() },
                { "active",  active.Trim() },
                { "runText", runText.Trim() }
            };

            FormRunText form = new FormRunText(this, data);
            form.ShowDialog();
        }

        #endregion

        #region Form Navigation
        private void OnBtnNextPage(object sender, EventArgs e)
        {
            TabPage tab = tabTable.SelectedTab;

            if (tab == tabIpAccessList)
            {
                tbReordStatus.Text = _tableProps[Tbl.ipaccesslists].MoveNextPage(this);
            }
            else if (tab == tabStation)
            {
                tbReordStatus.Text = _tableProps[Tbl.stations].MoveNextPage(this);
            }
            else if (tab == tabLogin)
            {
                tbReordStatus.Text = _tableProps[Tbl.logins].MoveNextPage(this);
            }
            else if (tab == tabPost)
            {
                tbReordStatus.Text = _tableProps[Tbl.posts].MoveNextPage(this);
            }
            else if (tab == tabRunText)
            {
                tbReordStatus.Text = _tableProps[Tbl.runningtexts].MoveNextPage(this);
            }
        }

        private void OnBtnPrevPage(object sender, EventArgs e)
        {
            TabPage tab = tabTable.SelectedTab;

            if (tab == tabIpAccessList)
            {
                tbReordStatus.Text = _tableProps[Tbl.ipaccesslists].MovePreviousPage(this);
            }
            else if (tab == tabStation)
            {
                tbReordStatus.Text = _tableProps[Tbl.stations].MovePreviousPage(this);
            }
            else if (tab == tabLogin)
            {
                tbReordStatus.Text = _tableProps[Tbl.logins].MovePreviousPage(this);
            }
            else if (tab == tabPost)
            {
                tbReordStatus.Text = _tableProps[Tbl.posts].MovePreviousPage(this);
            }
            else if (tab == tabRunText)
            {
                tbReordStatus.Text = _tableProps[Tbl.runningtexts].MovePreviousPage(this);
            }
        }

        private void OnBtnLastPage(object sender, EventArgs e)
        {
            TabPage tab = tabTable.SelectedTab;

            if (tab == tabIpAccessList)
            {
                tbReordStatus.Text = _tableProps[Tbl.ipaccesslists].MoveLastPage(this);
            }
            else if (tab == tabStation)
            {
                tbReordStatus.Text = _tableProps[Tbl.stations].MoveLastPage(this);
            }
            else if (tab == tabLogin)
            {
                tbReordStatus.Text = _tableProps[Tbl.logins].MoveLastPage(this);
            }
            else if (tab == tabPost)
            {
                tbReordStatus.Text = _tableProps[Tbl.posts].MoveLastPage(this);
            }
            else if (tab == tabRunText)
            {
                tbReordStatus.Text = _tableProps[Tbl.runningtexts].MoveLastPage(this);
            }
        }

        private void OnBtnFirstPage(object sender, EventArgs e)
        {
            TabPage tab = tabTable.SelectedTab;

            if (tab == tabIpAccessList)
            {
                tbReordStatus.Text = _tableProps[Tbl.ipaccesslists].MoveFirstPage(this);
            }
            else if (tab == tabStation)
            {
                tbReordStatus.Text = _tableProps[Tbl.stations].MoveFirstPage(this);
            }
            else if (tab == tabLogin)
            {
                tbReordStatus.Text = _tableProps[Tbl.logins].MoveFirstPage(this);
            }
            else if (tab == tabPost)
            {
                tbReordStatus.Text = _tableProps[Tbl.posts].MoveFirstPage(this);
            }
            else if (tab == tabRunText)
            {
                tbReordStatus.Text = _tableProps[Tbl.runningtexts].MoveFirstPage(this);
            }
        }

        private void OnBtnAddData(object sender, EventArgs e)
        {
            TabPage tab = tabTable.SelectedTab;

            if (tab == tabIpAccessList)
            {
                AddDataIPAccess();
            }
            else if (tab == tabStation)
            {
                AddDataStation();
            }
            else if (tab == tabLogin)
            {
                AddDataLogin();
            }
            else if (tab == tabPost)
            {
                AddDataPost();
            }
            else if (tab == tabRunText)
            {
                AddDataRunningText();
            }
        }

        private void OnBtnDeleteData(object sender, EventArgs e)
        {
            TabPage tab = tabTable.SelectedTab;

            if (tab == tabIpAccessList)
            {
                DeleteDataIpAccess();
            }
            else if (tab == tabStation)
            {
                DeleteDataStation();
            }
            else if (tab == tabLogin)
            {
                DeleteDataLogin();
            }
            else if (tab == tabPost)
            {
                DeleteDataPost();
            }
            else if (tab == tabRunText)
            {
                DeleteDataRunningText();
            }
        }


        #endregion

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.O:
                    {
                        // Ctrl + O
                        if (e.Control)
                        {
                            this.WindowState = FormWindowState.Normal;
                            this.BringToFront();
                        }
                        break;
                    }
            }
        }
    }
}
