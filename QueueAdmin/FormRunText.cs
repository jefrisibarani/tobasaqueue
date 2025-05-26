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
    public partial class FormRunText : Form
    {
        #region Member variables

        private readonly MainForm _mainForm;
        private bool _insertMode = false;
        Dictionary<string, string> _initialData = new Dictionary<string, string>();

        #endregion

        #region Form Loading/Unloading

        public FormRunText(MainForm form, Dictionary<string, string> data)
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

        public FormRunText(MainForm form)
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
                bool sticky = Util.StrToBool(_initialData["sticky"]);
                bool active = Util.StrToBool(_initialData["active"]);

                txtStation.Text   = _initialData["station"];
                chkSticky.Checked = sticky;
                chkActive.Checked = active;
                txtRunText.Text   = _initialData["runText"];
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            _mainForm.MessageReceived -= ProcessMessage;
        }

        #endregion

        public void ProcessMessage(Message message)
        {
        }

        private void InsertUpdateDataRunText(string station, bool sticky, bool active, string runText)
        {
            if (_mainForm.TcpClient != null)
            {
                string messageT;
                string commandType;
                int id_;

                if (_insertMode)
                {
                    messageT = Msg.SysInsTable.Text;
                    commandType = "INSERT";
                    id_ = -1;
                }
                else
                {
                    messageT = Msg.SysUpdTable.Text;
                    commandType = "UPDATE";
                    id_ = Convert.ToInt32(_initialData["id"]);
                }

                Dto.RunningText runningText = new Dto.RunningText()
                {
                    Id = id_,
                    StationName = station,
                    Sticky = sticky,
                    Active = active,
                    Text = runText
                };

                string jsonRText = JsonConvert.SerializeObject(runningText, Formatting.None);
                Dictionary<string, string> paramDict = new Dictionary<string, string>()
                {
                    ["command"] = commandType,
                    ["data"] = jsonRText,
                };

                string jsonParam = JsonConvert.SerializeObject(paramDict, Formatting.None);

                // SYS|INS_TABLE|REQ|Identifier|[Name!Params]
                string message = messageT +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + Tbl.runningtexts +  // tablename
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

                InsertUpdateDataRunText(txtStation.Text.Trim(), chkSticky.Checked, chkActive.Checked, txtRunText.Text.Trim());

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }
    }
}
