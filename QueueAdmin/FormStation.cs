using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Tobasa
{
    public partial class FormStation : Form
    {
        public event Action<EventArgs> DataChanged;
        private bool insertMode = false;
        private string staName;
        private string postName;

        public FormStation(string station, string post)
        {
            staName = station;
            postName = post;
            InitializeComponent();
        }

        public FormStation()
        {
            insertMode = true;
            InitializeComponent();
        }

        void InitComboPost()
        {
            if (Database.Me.Connected)
            {
                string sql = "SELECT name FROM posts";
                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cbPost.Items.Add (reader.GetString(0).Trim());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("QueueAdmin : Exception : " + ex.Message);
                }
            }
        }

        private void OnDataChanged()
        {
            if (DataChanged != null)
                DataChanged(new EventArgs());
        }

        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnAction(object sender, EventArgs e)
        {
            if (Database.Me.Connected)
            {
                string msg = "";
                if (insertMode)
                    msg = "Do you want to insert record?";
                else 
                    msg = "Do you want to update record?";

                if (MessageBox.Show(msg, "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                string sql = "";
                if (insertMode)
                    sql = "INSERT INTO stations (name,post,canlogin,keterangan) VALUES (?,?,?,?)";
                else
                    sql = "UPDATE stations SET canlogin = ?, keterangan = ? WHERE name = ? AND post=? ";
                
                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        if (insertMode)
                        {
                            cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = txtStation.Text.Trim();
                            cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = cbPost.Text.Trim();
                            cmd.Parameters.Add("?", OleDbType.Boolean).Value = rbCanLogin.Checked;
                            cmd.Parameters.Add("?", OleDbType.VarChar, 255).Value = txtRemark.Text;
                        }
                        else // UPDATE
                        {
                            cmd.Parameters.Add("?", OleDbType.Boolean).Value = rbCanLogin.Checked;
                            cmd.Parameters.Add("?", OleDbType.VarChar, 255).Value = txtRemark.Text;
                            cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = staName;
                            cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = postName;
                        }

                        cmd.ExecuteNonQuery();

                        OnDataChanged();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (insertMode)
                btnAction.Text = "&Insert";
            else
            {
                btnAction.Text = "&Update";
                cbPost.Enabled = false;
                txtStation.Enabled = false;
            }

            InitComboPost();

            if (staName == "" || postName == "")
                return;

            if (Database.Me.Connected)
            {
                string sql = "SELECT name,post,canlogin,keterangan FROM stations WHERE name=? and post=?";
                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = staName;
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = postName;

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                
                                txtStation.Text = staName;

                                string _post = reader.GetString(1).Trim();
                                cbPost.Text = _post;

                                bool _canLogin = reader.GetBoolean(2);
                                rbCanLogin.Checked = _canLogin;
                                rbCannotLogin.Checked = !_canLogin;

                                var remark = reader.GetValue(3);
                                if (remark != null)
                                    txtRemark.Text = remark.ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("QueueAdmin : Exception : " + ex.Message);
                }
            }
        }
    }
}
