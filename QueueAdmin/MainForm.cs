using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Tobasa
{
    public partial class MainForm : Form
    {
        #region Member variables

        private delegate void ProcessMessageCallback(DataReceivedEventArgs arg, string text);
        private delegate void ProcessErrorCallBack(NotifyEventArgs e);
        private TCPClient client = null;

        #endregion

        #region Constructor / Destructor

        public MainForm()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            tabControl.TabPages.Remove(tabPageDiag);

            Database.Me.Connect(GetConnectionString());
            InitIPAccessList();
            InitStations();
            InitPosts();
            InitLogins();
            InitRunText();

            StartClient();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
            Tobasa.Properties.Settings.Default.Save();
        }

        #endregion
        
        #region Encryption

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
                MessageBox.Show(ex.Message);
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
                    MessageBox.Show(ex.Message);
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
                    MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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

        private void NetSession_DataReceived(DataReceivedEventArgs arg)
        {
            string text = "";
            /*
            // Deserialize the message
            object message = Message.Deserialize(arg.Data);

            // Handle the message
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

        // cross thread safe handler
        private void ProcessMessage(DataReceivedEventArgs arg, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                ProcessMessageCallback d = new ProcessMessageCallback(ProcessMessage);
                this.Invoke(d, new object[] { arg, text });
            }
            else
            {
                if (text.StartsWith("ADMIN"))
                    HandleMessage(arg, text);
                else if (text.StartsWith("LOGIN"))
                {
                    string _response = text;
                    if (_response == Msg.LOGIN_OK)
                    {
                        lblStatus.Text = "Connected to Server : " + client.Session.RemoteInfo + " - Post :" + Tobasa.Properties.Settings.Default.StationPost + "  Station:" + Tobasa.Properties.Settings.Default.StationName;
                        Logger.Log("QueueAdmin : Successfully logged in");
                    }
                    else
                    {
                        string reason = _response.Substring(10);
                        string msg = "QueueAdmin : Could not logged in to server, \r\nReason: " + reason;
                        Logger.Log(msg);
                        MessageBox.Show(this, msg, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseConnection();
                    }
                }
                else
                {
                    tbLog.AppendText("\r\n" + text);

                    string logmsg = String.Format("Unhandled session message from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                }
            }
        }

        private void HandleMessage(DataReceivedEventArgs arg, string text)
        {
        }

        #endregion

        #region TCP Connection stuffs

        private void TCPClient_Notified(NotifyEventArgs e)
        {
            ProcessError(e);
        }

        private void ProcessError(NotifyEventArgs e)
        {
            if (this.InvokeRequired)
            {
                ProcessErrorCallBack d = new ProcessErrorCallBack(ProcessError);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                MessageBox.Show(this, e.Message, e.Summary, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void StartClient()
        {
            client = null;

            string dispServerHost = Tobasa.Properties.Settings.Default.QueueServerHost;
            int dispServerPort = Tobasa.Properties.Settings.Default.QueueServerPort;
            string stationName = Tobasa.Properties.Settings.Default.StationName;
            string stationPost = Tobasa.Properties.Settings.Default.StationPost;
            string userName = Tobasa.Properties.Settings.Default.QueueUserName;
            string password = Tobasa.Properties.Settings.Default.QueuePassword;

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
                message = "LOGIN" + Msg.Separator + "ADMIN" + Msg.Separator + stationName + Msg.Separator + stationPost + Msg.Separator + userName + Msg.Separator + passwordHash;
                client.Send(message);
            }
        }

        private void CloseConnection()
        {
            if (client.Connected)
            {
                client.Stop();
            }
        }

        #endregion

        #region Connection  Send data tests

        private void OnSendXXXData(object sender, EventArgs e)
        {
            if (client.Connected)
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
            {
                MessageBox.Show(this, "Could not connect to server\r\nPlease restart application", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSendData(object sender, EventArgs e)
        {
            if (client.Connected)
            {
                string message = cbCmd.Text;
                client.Send(message);
            }
            else
            {
                MessageBox.Show(this, "Could not connect to server\r\nPlease restart application", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendData(int length)
        {
            if (!client.Connected)
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

            client.Send(txt);
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

        #region Init DataGridView

        private void InitIPAccessList()
        {
            if (Database.Me.Connected)
            {
                string sql = "SELECT ipaddress,allowed,keterangan FROM ipaccesslists";
                try
                {
                    Database.Me.OpenConnection();
                    OleDbDataAdapter sda = new OleDbDataAdapter(sql, Database.Me.Connection);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gridIpAccess.DataSource = dt;

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
                catch (ArgumentException e)
                {
                    Logger.Log("QueueService : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueService : Exception : " + e.Message);
                }
            }
        }

        private void InitStations()
        {
            if (Database.Me.Connected)
            {
                string sql = "SELECT name,post,canlogin,keterangan FROM stations";
                try
                {
                    Database.Me.OpenConnection();
                    OleDbDataAdapter sda = new OleDbDataAdapter(sql, Database.Me.Connection);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gridStations.DataSource = dt;

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
                catch (ArgumentException e)
                {
                    Logger.Log("QueueAdmin : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueAdmin : Exception : " + e.Message);
                }
            }
        }

        private void InitPosts()
        {
            if (Database.Me.Connected)
            {
                string sql = "SELECT name,keterangan,numberprefix FROM posts";
                try
                {
                    Database.Me.OpenConnection();
                    OleDbDataAdapter sda = new OleDbDataAdapter(sql, Database.Me.Connection);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gridPosts.DataSource = dt;

                    DataGridView gridView = gridPosts;
                    DataGridViewColumn column = null;
                    column = gridView.Columns[0];
                      column.Width = 114;
                      column.HeaderText = "Post";
                    column = gridView.Columns[1];
                      column.Width = 383;
                      column.HeaderText = "Remark";
                   column = gridView.Columns[3];
                      column.Width = 114;
                      column.HeaderText = "Prefix";

                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueAdmin : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueAdmin : Exception : " + e.Message);
                }
            }
        }

        private void InitLogins()
        {
            if (Database.Me.Connected)
            {
                string sql = "SELECT username,password,expired,active FROM logins";
                try
                {
                    Database.Me.OpenConnection();
                    OleDbDataAdapter sda = new OleDbDataAdapter(sql, Database.Me.Connection);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gridLogins.DataSource = dt;

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
                catch (ArgumentException e)
                {
                    Logger.Log("QueueAdmin : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueAdmin : Exception : " + e.Message);
                }
            }
        }

        private void InitRunText()
        {
            if (Database.Me.Connected)
            {
                string sql = "SELECT id,station_name,sticky,active,running_text FROM runningtexts";
                try
                {
                    Database.Me.OpenConnection();
                    OleDbDataAdapter sda = new OleDbDataAdapter(sql, Database.Me.Connection);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gridRunText.DataSource = dt;

                    DataGridViewColumn column = null;
                    column = gridRunText.Columns[0];
                      column.Width = 20;
                      column.HeaderText = "ID";
                    column = gridRunText.Columns[1];
                      column.Width = 70;
                      column.HeaderText = "Station";
                    column = gridRunText.Columns[2];
                      column.Width = 40;
                      column.HeaderText = "Sticky";
                    column = gridRunText.Columns[3];
                      column.Width = 40;
                      column.HeaderText = "Active";
                    column = gridRunText.Columns[4];
                      column.Width = 325;
                      column.HeaderText = "Running Text";
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueAdmin : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueAdmin : Exception : " + e.Message);
                }
            }
        }

        #endregion

        #region Login/Logout handler

        private void menuLogin_Click(object sender, EventArgs e)
        {
            if (!Database.Me.Connected)
            {
                string connStr = GetConnectionString();
                if (Database.Me.Connect(connStr))
                    MessageBox.Show("Database Connected", "Info", MessageBoxButtons.OK);
                else
                    MessageBox.Show("Could not connect to database", "Info", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Database Already Connected", "Info", MessageBoxButtons.OK);
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            if (Database.Me.Connected)
            {
                if (Database.Me.Disconnect())
                    MessageBox.Show("Database Disconnected", "Info", MessageBoxButtons.OK);
                else
                    MessageBox.Show("Could not close connection to database", "Info", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Database Already Disconnected", "Info", MessageBoxButtons.OK);
        }

        #endregion

        #region SQL Form DataChanged handler

        private void FormIPAccess_DataChanged(EventArgs arg)
        {
            InitIPAccessList();
        }

        private void FormStation_DataChanged(EventArgs arg)
        {
            InitStations();
        }

        private void FormPost_DataChanged(EventArgs arg)
        {
            InitPosts();
        }

        private void FormLogin_DataChanged(EventArgs arg)
        {
            InitLogins();
        }

        private void FormRunText_DataChanged(EventArgs arg)
        {
            InitRunText();
        }

        #endregion

        #region DataGridView Handlers

        private void gridIpAccess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string ipaddress = Convert.ToString(gridIpAccess.Rows[e.RowIndex].Cells[0].Value);
            if (ipaddress == "")
                return;
            
            FormIPAccess form = new FormIPAccess(ipaddress.Trim());
            form.DataChanged += new Action<EventArgs>(FormIPAccess_DataChanged);
            form.ShowDialog();
        }

        private void gridStations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string staName = Convert.ToString(gridStations.Rows[e.RowIndex].Cells[0].Value);
            string postName = Convert.ToString(gridStations.Rows[e.RowIndex].Cells[1].Value);
            if (staName == "" || postName == "")
                return;

            FormStation form = new FormStation(staName.Trim(), postName.Trim());
            form.DataChanged += new Action<EventArgs>(FormStation_DataChanged);
            form.ShowDialog();
        }

        private void gridPosts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string postName = Convert.ToString(gridPosts.Rows[e.RowIndex].Cells[0].Value);
            if (postName == "")
                return;

            FormPost form = new FormPost(postName.Trim());
            form.DataChanged += new Action<EventArgs>(FormPost_DataChanged);
            form.ShowDialog();
        }

        private void gridLogins_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string usrName = Convert.ToString(gridLogins.Rows[e.RowIndex].Cells[0].Value);
            if (usrName == "")
                return;

            FormLogin form = new FormLogin(usrName.Trim());
            form.DataChanged += new Action<EventArgs>(FormLogin_DataChanged);
            form.ShowDialog();
        }

        private void gridRunText_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string id = Convert.ToString(gridRunText.Rows[e.RowIndex].Cells[0].Value);
            if (id == "")
                return;

            FormRunText form = new FormRunText(id.Trim());
            form.DataChanged += new Action<EventArgs>(FormRunText_DataChanged);
            form.ShowDialog();
        }

        private void btnAddDataAccess_Click(object sender, EventArgs e)
        {
            FormIPAccess form = new FormIPAccess();
            form.DataChanged += new Action<EventArgs>(FormIPAccess_DataChanged);
            form.ShowDialog();
        }

        private void btnAddDataStation_Click(object sender, EventArgs e)
        {
            FormStation form = new FormStation();
            form.DataChanged += new Action<EventArgs>(FormStation_DataChanged);
            form.ShowDialog();
        }

        private void btnAddPost_Click(object sender, EventArgs e)
        {
            FormPost form = new FormPost();
            form.DataChanged += new Action<EventArgs>(FormPost_DataChanged);
            form.ShowDialog();
        }

        private void btnAddLogin_Click(object sender, EventArgs e)
        {
            FormLogin form = new FormLogin();
            form.DataChanged += new Action<EventArgs>(FormLogin_DataChanged);
            form.ShowDialog();
        }

        private void btnAddRunText_Click(object sender, EventArgs e)
        {
            FormRunText form = new FormRunText();
            form.DataChanged += new Action<EventArgs>(FormRunText_DataChanged);
            form.ShowDialog();
        }

        private void btnDeleteRunText_Click(object sender, EventArgs e)
        {
            int row = gridRunText.CurrentCell.RowIndex;
            int id = Convert.ToInt32(gridRunText.Rows[row].Cells[0].Value);
            if (id < 0 )
                return;

            if (Database.Me.Connected)
            {
                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                string sql = "DELETE FROM runningtexts WHERE id = ?";

                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = id;
                        int changed = cmd.ExecuteNonQuery();
                        if (changed > 0)
                            InitRunText();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteDataAccess_Click(object sender, EventArgs e)
        {
            int row = gridIpAccess.CurrentCell.RowIndex;
            string ipaddress = Convert.ToString(gridIpAccess.Rows[row].Cells[0].Value);
            if (ipaddress == "")
                return;

            if (Database.Me.Connected)
            {
                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                string sql = "DELETE FROM ipaccesslists WHERE ipaddress = ?";

                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 15).Value = ipaddress.Trim();
                        int changed = cmd.ExecuteNonQuery();
                        if (changed >0 )
                            InitIPAccessList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteStation_Click(object sender, EventArgs e)
        {
            int row = gridStations.CurrentCell.RowIndex;
            string staName = Convert.ToString(gridStations.Rows[row].Cells[0].Value);
            if (staName == "")
                return;

            if (Database.Me.Connected)
            {
                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                string sql = "DELETE FROM stations WHERE name = ?";

                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = staName.Trim();
                        int changed = cmd.ExecuteNonQuery();
                        if (changed > 0)
                            InitStations();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeletePost_Click(object sender, EventArgs e)
        {
            int row = gridPosts.CurrentCell.RowIndex;
            string postName = Convert.ToString(gridPosts.Rows[row].Cells[0].Value);
            if (postName == "")
                return;

            if (Database.Me.Connected)
            {
                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                string sql = "DELETE FROM posts WHERE name = ?";

                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = postName.Trim();
                        int changed = cmd.ExecuteNonQuery();
                        if (changed > 0)
                            InitPosts();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteLogin_Click(object sender, EventArgs e)
        {
            int row = gridLogins.CurrentCell.RowIndex;
            string usrName = Convert.ToString(gridLogins.Rows[row].Cells[0].Value);
            if (usrName == "")
                return;

            if (Database.Me.Connected)
            {
                if (MessageBox.Show("Do you want to delete record?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                string sql = "DELETE FROM logins WHERE username = ?";

                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 50).Value = usrName.Trim();
                        int changed = cmd.ExecuteNonQuery();
                        if (changed > 0)
                            InitLogins();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void gridStations_SelectionChanged(object sender, EventArgs e)
        {
            //int row = gridStations.CurrentCell.RowIndex;
            //Logger.Log("Current row is " + row);
        }

        private void gridIpAccess_SelectionChanged(object sender, EventArgs e)
        {
            //int row = gridIpAccess.CurrentCell.RowIndex;
            //Logger.Log("Current row is " + row);
        }

        #endregion

    }
}
