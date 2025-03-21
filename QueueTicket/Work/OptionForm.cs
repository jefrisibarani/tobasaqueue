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
using System.Windows.Forms;

namespace Tobasa
{
    public partial class OptionForm : Form
    {
        private PostPropertyCollection postProperties = new PostPropertyCollection();
        public OptionForm()
        {
            InitializeComponent();

            // Get POST Ids
            string[] postIds = new string[Properties.Settings.Default.UIPostList.Count];
            Properties.Settings.Default.UIPostList.CopyTo(postIds, 0);
            
            // Populate PostPropertyCollection
            foreach (string id in postIds )
            {
                postProperties.Add(id, new PostProperty(id));
            }

            // Populate cbPost
            cbPost.Items.Clear();
            cbPost.Items.AddRange(postIds);

            // Populate cbSelectPost
            cbSelectPost.Items.Clear();
            cbSelectPost.Items.AddRange(postIds);

            RestoreSettings();
        }

        private void SetPostPropertiesControl(string postid)
        {
            string postId = postid;
            PostProperty postProperty = postProperties.FindById(postId);

            if (postProperty != null)
            {
                tbPostName.Text = postProperty.Name;
                tbPostId.Text = postProperty.Id;
                tbPostCaption.Text = postProperty.Caption;
                chkPostEnabled.Checked = postProperty.Enabled;
                tbPostBtnImgOn.Text = postProperty.BtnImageOn;
                tbPostBtnImgOff.Text = postProperty.BtnImageOff;
                pickPostPrintCopies.Value = (decimal)postProperty.PrintCopies;
                tbPostTicketHeader.Text = postProperty.PrintHeader;

                if (postProperty.Index == 0 || postProperty.Index == 1 || postProperty.Index == 2 ||
                   postProperty.Index == 5 || postProperty.Index == 6 || postProperty.Index == 7)
                {
                    chkPostVisible.Checked = true;
                    chkPostVisible.Enabled = false;
                }
                else
                {
                    chkPostVisible.Checked = postProperty.Visible;
                    chkPostVisible.Enabled = true;
                }
            }
        }
        private void RestoreSettings()
        {
            tbServer.Text = Properties.Settings.Default.QueueServerHost;
            tbPort.Text = Properties.Settings.Default.QueueServerPort.ToString();
            tbStation.Text = Properties.Settings.Default.StationName;
            cbPost.Text = Properties.Settings.Default.StationPost;
            
            chkStartFullScreen.Checked = Properties.Settings.Default.StartDisplayFullScreen;
            chkPrintTicket.Checked = Properties.Settings.Default.PrintTicket;

            txtRuntext0.Text = Properties.Settings.Default.RunningText0;
            txtRuntext1.Text = Properties.Settings.Default.RunningText1;
            txtPrintFooter.Text = Properties.Settings.Default.PrintFooter;

            tbImgLogo.Text = Properties.Settings.Default.DisplayLogoImg;
            tbImgHeader.Text = Properties.Settings.Default.DisplayHeaderImg;
            tbImgBG.Text = Properties.Settings.Default.DisplayHeaderBg;

            chkButtonsWithLabel.Checked = Properties.Settings.Default.DrawLabelOnButtons;
            btnFontSizeChanger.Value = Properties.Settings.Default.ButtonLabelFontSize;
            labelFontSizeChanger.Value = Properties.Settings.Default.MainMenuLabelFontSize;

            chkShowLeftMenu.Checked = Properties.Settings.Default.ShowLeftMenu;
            chkShowRightMenu.Checked = Properties.Settings.Default.ShowRightMenu;

            // Setup post options
            postProperties.LoadFromConfiguration();
            cbSelectPost.Text = "POST0";
            var postId = cbSelectPost.Text;
            SetPostPropertiesControl(postId);

            SetRadioButtonMainMenuLabelState();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.QueueServerHost = tbServer.Text;
            Properties.Settings.Default.QueueServerPort = Convert.ToInt32(tbPort.Text);
            Properties.Settings.Default.StationName = tbStation.Text;
            Properties.Settings.Default.StationPost = cbPost.Text;
            Properties.Settings.Default.StartDisplayFullScreen = chkStartFullScreen.Checked;
            Properties.Settings.Default.PrintTicket = chkPrintTicket.Checked;
            Properties.Settings.Default.RunningText0 = txtRuntext0.Text;
            Properties.Settings.Default.RunningText1 = txtRuntext1.Text;
            Properties.Settings.Default.PrintFooter = txtPrintFooter.Text;
            Properties.Settings.Default.DisplayLogoImg = tbImgLogo.Text;
            Properties.Settings.Default.DisplayHeaderImg = tbImgHeader.Text;
            Properties.Settings.Default.DisplayHeaderBg = tbImgBG.Text;

            Properties.Settings.Default.DrawLabelOnButtons = chkButtonsWithLabel.Checked;
            Properties.Settings.Default.ButtonLabelFontSize = (int) btnFontSizeChanger.Value;
            Properties.Settings.Default.MainMenuLabelFontSize = (int)labelFontSizeChanger.Value;
            Properties.Settings.Default.MainMenuLabelAlignment = GetMainMenuLabelAlignmentValue();

            Properties.Settings.Default.ShowLeftMenu = chkShowLeftMenu.Checked;
            Properties.Settings.Default.ShowRightMenu = chkShowRightMenu.Checked;

            // save post options
            var postId = cbSelectPost.Text;
            PostProperty prop = postProperties.FindById(postId);
            if (prop != null)
            {
                prop.Name = tbPostName.Text;
                //prop.Id = tbPostId.Text;
                prop.Caption = tbPostCaption.Text;
                prop.Visible = chkPostVisible.Checked;
                prop.Enabled = chkPostEnabled.Checked;
                prop.BtnImageOn = tbPostBtnImgOn.Text;
                prop.BtnImageOff = tbPostBtnImgOff.Text;
                prop.PrintHeader = tbPostTicketHeader.Text;
                //prop.PrintFooter = tbPostTicketFooter.Text;
                prop.PrintCopies = (short) pickPostPrintCopies.Value;
            }
            postProperties.SaveToConfiguration();

            Properties.Settings.Default.Save();
        }

        private String GetMainMenuLabelAlignmentValue()
        {
            if (rbLabelLeft.Checked)
                return "Left";
            else if (rbLabelMiddle.Checked)
                return "Middle";
            else if (rbLabelRight.Checked)
                return "Right";

            return "Middle";
        }

        private void SetRadioButtonMainMenuLabelState()
        {
            if (Properties.Settings.Default.MainMenuLabelAlignment == "Left")
                rbLabelLeft.Checked = true;
            else if (Properties.Settings.Default.MainMenuLabelAlignment == "Middle")
                rbLabelMiddle.Checked = true;
            else if (Properties.Settings.Default.MainMenuLabelAlignment == "Right")
                rbLabelRight.Checked = true;
            else
                rbLabelMiddle.Checked = true;
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void OnSave(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnBtnSetClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.InitialDirectory = Util.ProcessDir;
            fileDlg.Filter = "Image Files(*.PNG;*.JPG;*.BMP)|*.PNG;*.JPG;*.BMP|All files (*.*)|*.*";

            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                var postId = cbSelectPost.Text;
                var postProperty = postProperties.FindById(postId);
                if (postProperty != null)
                {
                    if ((Button)sender == btnPostImgOnSelect)
                    {
                        tbPostBtnImgOn.Text = fileDlg.FileName;
                        postProperty.BtnImageOn = fileDlg.FileName;
                    }
                    else if ((Button)sender == btnPostImgOffSelect)
                    {
                        tbPostBtnImgOff.Text = fileDlg.FileName;
                        postProperty.BtnImageOff = fileDlg.FileName;
                    }
                }
            }
        }

        private void OnPrintCopiesValueChanged(object sender, EventArgs e)
        {
            var postId = cbSelectPost.Text;
            var printCopieS = ((NumericUpDown)sender).Value;
            PostProperty prop = postProperties.FindById(postId);
            if (prop != null)
            {
                prop.PrintCopies = (short)printCopieS;
            }
        }

        private void OnPrintTicketChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PrintTicket = chkPrintTicket.Checked;
        }

        private void OnCbSelectPostChanged(object sender, EventArgs e)
        {
            string postId = cbSelectPost.Text;
            SetPostPropertiesControl(postId);
        }

        private void OnChkShowLeft(object sender, EventArgs e)
        {
            if (!chkShowLeftMenu.Checked && !chkShowRightMenu.Checked)
            {
                chkShowRightMenu.Checked = true;
            }
        }

        private void OnChkShowRight(object sender, EventArgs e)
        {
            if (!chkShowRightMenu.Checked && !chkShowLeftMenu.Checked)
            {
                chkShowLeftMenu.Checked = true;
            }
        }
    }
}

