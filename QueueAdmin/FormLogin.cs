using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Tobasa
{
    public partial class FormLogin : Form
    {
        public event Action<EventArgs> DataChanged;
        private bool insertMode = false;
        private string usrName;
        private string curPassword = "";
        private bool changingPassword = false;

        public FormLogin(string user)
        {
            usrName = user;
            InitializeComponent();

            if (insertMode) btnAction.Text = "&Insert";
            else btnAction.Text = "&Update";

        }

        public FormLogin()
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

        private bool CheckUserName(string user)
        {
            if (user == "")
            {
                MessageBox.Show("You can not use empty user name", "Action", MessageBoxButtons.OK);
                return false;
            }
            else if (user.Length < 3)
            {
                MessageBox.Show("Minimum user name length is 3 characters", "Action", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private bool CheckPassword(string password)
        {
            if (password == "")
            {
                MessageBox.Show("You can not use empty password", "Action", MessageBoxButtons.OK);
                return false;
            }
            else if (password.Length < 5)
            {
                MessageBox.Show("Minimum password length is 5 characters", "Action", MessageBoxButtons.OK);
                return false;
            }

            return true;
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

                string newuserName = txtUser.Text.Trim();
                string newClearPass = txtPassword.Text.Trim();
                string newPasswordHash = "";

                string sql = "";
                if (insertMode)
                {
                    if (CheckUserName(newuserName) == false)  return;
                    if (CheckPassword(newClearPass) == false) return;
                    
                    newPasswordHash = Util.GetPasswordHash(newClearPass, newuserName);
                    sql = "INSERT INTO logins (username,password,expired,active) VALUES (?,?,?,?)";
                }
                else
                {
                    // user want to change password
                    if (changingPassword)
                    {
                        if (CheckUserName(newuserName) == false) return;
                        if (CheckPassword(newClearPass) == false) return;                    
                        
                        newPasswordHash = Util.GetPasswordHash(newClearPass, newuserName);
                        
                        sql = "UPDATE logins SET username = ? , password = ? , expired = ?, active = ? WHERE username = ?";
                    }
                    else
                    {
                        sql = "UPDATE logins SET expired = ?, active = ? WHERE username = ?";
                    }
                }


                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        if (insertMode) // INSERT
                        {
                            DateTime newExp = dtExpired.Value;
                            if (dtExpired.Value.Date == DateTime.Now.Date)
                                newExp = DateTime.Now.AddYears(1);

                            cmd.Parameters.Add("?", OleDbType.VarChar, 50).Value = newuserName;
                            cmd.Parameters.Add("?", OleDbType.VarChar, 64).Value = newPasswordHash;
                            cmd.Parameters.Add("?", OleDbType.Date).Value = newExp;
                            cmd.Parameters.Add("?", OleDbType.Boolean).Value = chkActive.Checked;
                        }
                        else if (!insertMode) // UPDATE
                        {
                            if (changingPassword)
                            {
                                cmd.Parameters.Add("?", OleDbType.VarChar, 50).Value = newuserName;
                                cmd.Parameters.Add("?", OleDbType.VarChar, 64).Value = newPasswordHash;
                                cmd.Parameters.Add("?", OleDbType.Date).Value = dtExpired.Value;
                                cmd.Parameters.Add("?", OleDbType.Boolean).Value = chkActive.Checked;
                                cmd.Parameters.Add("?", OleDbType.VarChar, 50).Value = usrName;
                            }
                            else if(!changingPassword)
                            {
                                cmd.Parameters.Add("?", OleDbType.Date).Value = dtExpired.Value;
                                cmd.Parameters.Add("?", OleDbType.Boolean).Value = chkActive.Checked;
                                cmd.Parameters.Add("?", OleDbType.VarChar, 50).Value = usrName;
                            }
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
            if (!insertMode) // Update
            {
                if (Database.Me.Connected)
                {
                    string sql = "SELECT username,password,expired,active FROM logins WHERE username= ?";
                    try
                    {
                        Database.Me.OpenConnection();
                        using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                        {
                            cmd.Parameters.Add("?", OleDbType.VarChar, 15).Value = usrName;

                            using (OleDbDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();

                                    txtUser.Text = usrName;
                                    txtPassword.Text = reader.GetString(1).Trim();
                                    curPassword = reader.GetString(1).Trim();     // save current password
                                    dtExpired.Value = reader.GetDateTime(2);
                                    chkActive.Checked = reader.GetBoolean(3);
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
            else if (insertMode)// insert
            {
                txtUser.ReadOnly = false;
                txtPassword.ReadOnly = false;
                txtPassword.Text = "";
                btnChangePassword.Enabled = false;
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!insertMode) // Update
            {
                if (MessageBox.Show("Are you sure you want to change your password?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                changingPassword = true;
                txtPassword.ReadOnly = false;
                txtPassword.Text = "";
            }
        }
    }
}
