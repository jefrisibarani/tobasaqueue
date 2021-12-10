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
    public partial class FormPost : Form
    {
        #region Member variables

        public event Action<string> DataChanged;
        private readonly MainForm _mainForm;
        private bool _insertMode = false;
        Dictionary<string, string> _initialData = new Dictionary<string, string>();

        #endregion

        #region Form Loading/Unloading

        public FormPost(MainForm form, Dictionary<string, string> data)
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

        public FormPost(MainForm form)
        {
            _mainForm = form;
            //_mainForm.MessageReceived += new MessageReceived(ProcessMessage);

            _insertMode = true;
            InitializeComponent();

            if (_insertMode) btnAction.Text = "&Insert";
            else btnAction.Text = "&Update";
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            if (!_insertMode && _initialData.Count > 0)
            {
                txtPost.Text = _initialData["postname"];

                var remark = _initialData["remark"];
                if (remark != null)
                    txtRemark.Text = remark.ToString().Trim();

                var prefix = _initialData["prefix"];
                if (prefix != null)
                    txtPrefix.Text = prefix.ToString().Trim();
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

        private void InsertUpdateDataPost(string postname, string remark, string prefix)
        {
            if (_mainForm.TcpClient != null && _mainForm.TcpClient.Connected)
            {
                string messageT;
                string commandType;
                string postOld_;

                if (_insertMode)
                {
                    messageT    = Msg.SysInsTable.Text;
                    commandType = "INSERT";
                    postOld_    = "";
                }
                else
                {
                    messageT    = Msg.SysUpdTable.Text;
                    commandType = "UPDATE";
                    postOld_    = _initialData["postname"];
                }

                Dto.Post post = new Dto.Post()
                {
                    NameOld         = postOld_,
                    Name            = postname,
                    NumberPrefix    = prefix,
                    Keterangan      = remark
                };

                string jsonPost = JsonConvert.SerializeObject(post, Formatting.None);
                Dictionary<string, string> paramDict = new Dictionary<string, string>()
                {
                    ["command"] = commandType,
                    ["data"] = jsonPost,
                };

                string jsonParam = JsonConvert.SerializeObject(paramDict, Formatting.None);

                // SYS|INS_TABLE|REQ|Identifier|[Name!Params]
                string message = messageT +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + Tbl.posts +       // tablename
                                 Msg.CompDelimiter + jsonParam;    // parameter

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

                InsertUpdateDataPost(txtPost.Text.Trim(), txtRemark.Text.Trim(), txtPrefix.Text.Trim());

                // TODO: Remove DataChanged event, since MainForm now update 
                // relevant grid in its HandleMessage method
                //DataChanged?.Invoke(Tbl.posts);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
