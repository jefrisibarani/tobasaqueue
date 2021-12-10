#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2021  Jefri Sibarani

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
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tobasa
{
    public partial class FormLogin : Form
    {
        #region Member variables

        public event Action<string> DataChanged;
        private readonly MainForm _mainForm;
        private bool _insertMode = false;
        private bool _changingPassword = false;
        Dictionary<string, string> _initialData;

        #endregion

        #region Form Loading/Unloading

        public FormLogin(MainForm form, Dictionary<string, string> data)
        {
            _mainForm = form;
            //_mainForm.MessageReceived += new MessageReceived(ProcessMessage);

            _initialData = data;
            InitializeComponent();

            if (_insertMode) 
                btnAction.Text = "&Insert";
            else 
                btnAction.Text = "&Update";
        }

        public FormLogin(MainForm form)
        {
            _mainForm = form;
            //_mainForm.MessageReceived += new MessageReceived(ProcessMessage);

            _insertMode = true;
            InitializeComponent();

            if (_insertMode) 
                btnAction.Text = "&Insert";
            else 
                btnAction.Text = "&Update";
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            if (!_insertMode && _initialData.Count > 0)
            {
                txtUser.Text        = _initialData["username"];
                txtPassword.Text    = _initialData["password"];
                //curPassword       = _initialData["password"];   // save current password
                dtExpired.Value     = Convert.ToDateTime(_initialData["expired"]);
                chkActive.Checked   = Convert.ToBoolean(Convert.ToInt32(_initialData["active"]));
            }
            else if (_insertMode)
            {
                dtExpired.Value             = DateTime.Now.AddYears(1);
                txtUser.ReadOnly            = false;
                txtPassword.ReadOnly        = false;
                txtPassword.Text            = "";
                btnChangePassword.Enabled   = false;
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            //_mainForm.MessageReceived -= ProcessMessage;
        }

        #endregion

        public void ProcessMessage(Message message)
        {
        }

        private void InsertUpdateDataLogin(string username, string password, DateTime expired, bool active)
        {

            if (_mainForm.TcpClient != null && _mainForm.TcpClient.Connected)
            {
                string messageT;
                string commandType;
                string userOld_;

                if (_insertMode)
                {
                    messageT = Msg.SysInsTable.Text;
                    commandType = "INSERT";
                    userOld_ = "";
                }
                else if (_changingPassword)
                {
                    messageT = Msg.SysUpdTable.Text;
                    commandType = "UPDATE_PASSWORD";
                    userOld_ = _initialData["username"];
                }
                else
                {
                    messageT = Msg.SysUpdTable.Text;
                    commandType = "UPDATE";
                    userOld_ = _initialData["username"];
                }

                Dto.Login login = new Dto.Login()
                {
                    //Id       = Convert.ToInt32(_initialData["id"]),
                    UsernameOld = userOld_,
                    Username = username,
                    Password = password,
                    Expired  = expired,
                    Active   = active
                };

                string jsonLogin = JsonConvert.SerializeObject(login, Formatting.None);
                Dictionary<string, string> paramDict = new Dictionary<string, string>()
                {
                    ["command"] = commandType,
                    ["data"]   = jsonLogin,
                };

                string jsonParam = JsonConvert.SerializeObject(paramDict, Formatting.None);

                // SYS|UPD_TABLE|REQ|Identifier|[Name!Params]
                string message = messageT +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + Tbl.logins +        // tablename
                                 Msg.CompDelimiter + jsonParam;      // parameter

                _mainForm.TcpClient.Send(message);
            }
            else
                Util.ShowConnectionError(this);
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
            string msg = "";
            if (_insertMode)
                msg = "Do you want to insert record?";
            else 
                msg = "Do you want to update record?";

            if (MessageBox.Show(msg, "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                // Send Insert/Update request to QueueServer
                // On receiving response from server in MainForm.HandleMessage, 
                // tell main form to update relevant grid view
                string newuserName  = txtUser.Text.Trim();
                string newClearPass = txtPassword.Text.Trim();
                string newPasswordHash = "";

                if (_insertMode || _changingPassword)
                {
                    if (CheckUserName(newuserName) == false)
                        return;
                    if (CheckPassword(newClearPass) == false)
                        return;
                }

                newPasswordHash = Util.GetPasswordHash(newClearPass, newuserName);

                InsertUpdateDataLogin(newuserName, newPasswordHash, dtExpired.Value, chkActive.Checked);

                // TODO: Remove DataChanged event, since MainForm now update 
                // relevant grid in its HandleMessage method
                //DataChanged?.Invoke(Tbl.logins);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnChangePasswordClick(object sender, EventArgs e)
        {
            if (!_insertMode)
            {
                if (MessageBox.Show("Are you sure you want to change your password?", "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                _changingPassword    = true;
                txtPassword.ReadOnly = false;
                txtUser.ReadOnly     = false;
                txtPassword.Text     = "";
            }
        }
    }
}
