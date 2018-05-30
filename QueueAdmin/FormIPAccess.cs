using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net;
namespace Tobasa
{
    public partial class FormIPAccess : Form
    {
        public event Action<EventArgs> DataChanged;
        private bool insertMode = false;
        private String ipAddress;

        public FormIPAccess(string ip)
        {
            ipAddress = ip;
            InitializeComponent();

            if (insertMode) btnAction.Text = "&Insert";
            else btnAction.Text = "&Update";
        }

        public FormIPAccess()
        {
            insertMode = true;
            InitializeComponent();

            if (insertMode) btnAction.Text = "&Insert";
            else btnAction.Text = "&Update";
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

            IPAddress valid;
            if (IPAddress.TryParse(txtIPAddress.Text, out valid) != true)
            {
                MessageBox.Show("Invalid IP Address : " + txtIPAddress.Text, "Error", MessageBoxButtons.OK);
                return;
            }

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
                    sql = "INSERT INTO ipaccesslists (ipaddress,allowed,keterangan) VALUES (?,?,?)";
                else
                    sql = "UPDATE ipaccesslists SET ipaddress = ? , allowed = ? , keterangan = ? WHERE ipaddress = ?";
                
                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 15).Value = txtIPAddress.Text;
                        cmd.Parameters.Add("?", OleDbType.Boolean).Value = rbCanConnect.Checked;
                        cmd.Parameters.Add("?", OleDbType.VarChar, 50).Value = txtRemark.Text;

                        if (!insertMode) // UPDATE
                            cmd.Parameters.Add("?", OleDbType.VarChar, 15).Value = ipAddress;

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

        private void IPAccessForm_Load(object sender, EventArgs e)
        {
            if (Database.Me.Connected)
            {
                string sql = "SELECT ipaddress,allowed,keterangan FROM ipaccesslists WHERE ipaddress = ?";
                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 15).Value = ipAddress;

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                bool _allowed = reader.GetBoolean(1);
                                var remark = reader.GetValue(2);
                                if(remark !=null) 
                                    txtRemark.Text = remark.ToString().Trim();
                                txtIPAddress.Text = ipAddress;
                                rbCanConnect.Checked = _allowed;
                                rbCannotConnect.Checked = !_allowed;
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

        private void txtIPAddress_Validating(object sender, CancelEventArgs e)
        {
            /*
            IPAddress valid;
            if (IPAddress.TryParse(txtIPAddress.Text, out valid) == true)
                e.Cancel = true;
            else
                MessageBox.Show("Invalid IP Address : " + txtIPAddress.Text, "Error", MessageBoxButtons.YesNo);
            */
        }
    }
}
