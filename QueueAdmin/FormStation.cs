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
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tobasa
{
    public partial class FormStation : Form
    {
        #region Member variables

        private readonly MainForm _mainForm;
        private bool _insertMode = false;
        Dictionary<string, string> _initialData = new Dictionary<string, string>();

        #endregion

        #region Form Loading/Unloading

        public FormStation(MainForm form, Dictionary<string, string> data)
        {
            _mainForm = form;
            _mainForm.MessageReceived += new MessageReceived(ProcessMessage);

            _initialData = data;
            InitializeComponent();
        }

        public FormStation(MainForm form)
        {
            _mainForm = form;
            _mainForm.MessageReceived += new MessageReceived(ProcessMessage);

            _insertMode = true;
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            RequestPostsListFromServer();
        }

        // call this on SysGetList
        private void InitControlValues()
        {
            if (_insertMode)
                btnAction.Text = "&Insert";
            else
            {
                btnAction.Text = "&Update";
                cbPost.Enabled = true;
                txtStation.Enabled = true;
            }

            if (!_insertMode && _initialData.Count > 0)
            {
                bool canlogin = Util.StrToBool(_initialData["canlogin"]);

                txtStation.Text         = _initialData["name"];
                cbPost.Text             = _initialData["post"];
                rbCanLogin.Checked      = canlogin;
                rbCannotLogin.Checked   = !canlogin;
                txtRemark.Text          = _initialData["keterangan"];
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            _mainForm.MessageReceived -= ProcessMessage;
        }

        #endregion

        public void ProcessMessage(Message message)
        {
            try
            {
                Message qmsg = message;
                if (qmsg.MessageType == Msg.SysGetList)
                {
                    string jsonList = qmsg.PayloadValues["result"];
                    List<string> list = (List<string>)JsonConvert.DeserializeObject(jsonList, (typeof(List<string>)));
                    foreach (string post in list)
                    {
                        cbPost.Items.Add(post);
                    }

                    InitControlValues();
                }
            }
            catch(Exception ex)
            {
                Logger.Log("[FormStation] " + ex.Message);
            }
        }

        void RequestPostsListFromServer()
        {
            if(_mainForm.TcpClient != null)
            {
                // SYS|GET_LIST|REQ|Identifier|[Name]
                string message = Msg.SysGetList.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + Tbl.posts;
                _mainForm.TcpClient.Send(message);
            }
        }

        private void InsertUpdateDataStation(string name, string post, bool canlogin, string keterangan)
        {
            if (_mainForm.TcpClient != null)
            {
                string messageT;
                string commandType;
                string nameOld_;
                string postOld_;

                if (_insertMode)
                {
                    messageT    = Msg.SysInsTable.Text;
                    commandType = "INSERT";
                    nameOld_ = "";
                    postOld_ = "";
                }
                else
                {
                    messageT    = Msg.SysUpdTable.Text;
                    commandType = "UPDATE";
                    nameOld_ = _initialData["name"];
                    postOld_ = _initialData["post"];
                }

                Dto.Station station = new Dto.Station()
                {
                    NameOld     = nameOld_,
                    PostOld     = postOld_,
                    Name        = name,
                    Post        = post,
                    CanLogin    = canlogin,
                    Keterangan  = keterangan
                };

                string jsonStation = JsonConvert.SerializeObject(station, Formatting.None);
                Dictionary<string, string> paramDict = new Dictionary<string, string>()
                {
                    ["command"] = commandType,
                    ["data"] = jsonStation,
                };

                string jsonParam = JsonConvert.SerializeObject(paramDict, Formatting.None);

                // SYS|INS_TABLE|REQ|Identifier|[Name!Params]
                string message = messageT +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + Tbl.stations +      // tablename
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

        private void OnAction(object sender, EventArgs e)
        {
            string msg;

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

                InsertUpdateDataStation(txtStation.Text.Trim(), cbPost.Text.Trim(), rbCanLogin.Checked, txtRemark.Text.Trim());

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
