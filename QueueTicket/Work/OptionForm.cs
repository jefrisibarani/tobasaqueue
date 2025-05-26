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

namespace Tobasa
{
    public partial class OptionForm : Form
    {
        Properties.Settings _settings = Properties.Settings.Default;

        private PostPropertyCollection postProperties = new PostPropertyCollection();
        private String displayTheme = "Classic";
        private MainForm mainForm = null;
        public OptionForm(MainForm mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponent();

            // Get POST Ids
            string[] postIds = new string[_settings.UIPostList.Count];
            _settings.UIPostList.CopyTo(postIds, 0);
            
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
                chkUseThemeButtonImage.Checked = postProperty.UseThmeButtonImage;

                btnPostImgOnSelect.Enabled = !postProperty.UseThmeButtonImage;
                btnPostImgOffSelect.Enabled = !postProperty.UseThmeButtonImage;
                tbPostBtnImgOn.ReadOnly = postProperty.UseThmeButtonImage;
                tbPostBtnImgOff.ReadOnly = postProperty.UseThmeButtonImage;


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
            tbServer.Text = _settings.QueueServerHost;
            tbPort.Text = _settings.QueueServerPort.ToString();
            tbStation.Text = _settings.StationName;
            cbPost.Text = _settings.StationPost;
            
            chkStartFullScreen.Checked = _settings.StartDisplayFullScreen;
            chkPrintTicket.Checked = _settings.PrintTicket;

            txtRuntext0.Text = _settings.RunningText0;
            txtRuntext1.Text = _settings.RunningText1;
            txtPrintFooter.Text = _settings.PrintFooter;

            chkButtonsWithLabel.Checked = _settings.DrawLabelOnButtons;
            btnFontSizeChanger.Value = _settings.ButtonLabelFontSize;
            labelFontSizeChanger.Value = _settings.MainMenuLabelFontSize;

            chkShowLeftMenu.Checked = _settings.ShowLeftMenu;
            chkShowRightMenu.Checked = _settings.ShowRightMenu;

            // Setup post options
            postProperties.LoadFromConfiguration();
            cbSelectPost.Text = "POST0";
            var postId = cbSelectPost.Text;
            SetPostPropertiesControl(postId);

            SetRadioButtonMainMenuLabelState();

            displayTheme = _settings.Theme;
            tbImgHeader.Text = _settings.MainBrandingImage;
            tbImgLogo.Text = _settings.LogoImage;

            bool useBrandingImage = _settings.UseMainBrandingImage;

            tbImgHeader.Enabled = useBrandingImage;
            btnSetBrandingImage.Enabled = useBrandingImage;
            tbLogoText.Enabled = !useBrandingImage;
            tbImgLogo.Enabled = !useBrandingImage;
            btnSetLogoImg.Enabled = !useBrandingImage;


            chkUseBrandingImageAsMainLogo.Checked = _settings.UseMainBrandingImage;
            tbLogoText.Text = _settings.MainLogoText;

            RestoreButtonThemes();

            cbBlueThemeButton.Text = _settings.ThemeBlueButtonColor;
            cbDarkThemeButton.Text = _settings.ThemeDarkButtonColor;
            cbGreenThemeButton.Text = _settings.ThemeGreenButtonColor;
            cbOrangeThemeButton.Text = _settings.ThemeOrangeButtonColor;
            cbRedThemeButton.Text = _settings.ThemeRedButtonColor;

            mainForm.SetLogoTextColor(_settings.LogoTextColor);
            btnLogoTextColor.BackColor = _settings.LogoTextColor;
        }

        private void SaveSettings()
        {
            _settings.QueueServerHost = tbServer.Text;
            _settings.QueueServerPort = Convert.ToInt32(tbPort.Text);
            _settings.StationName = tbStation.Text;
            _settings.StationPost = cbPost.Text;
            _settings.StartDisplayFullScreen = chkStartFullScreen.Checked;
            _settings.PrintTicket = chkPrintTicket.Checked;
            _settings.RunningText0 = txtRuntext0.Text;
            _settings.RunningText1 = txtRuntext1.Text;
            _settings.PrintFooter = txtPrintFooter.Text;

            _settings.DrawLabelOnButtons = chkButtonsWithLabel.Checked;
            _settings.ButtonLabelFontSize = (int) btnFontSizeChanger.Value;
            _settings.MainMenuLabelFontSize = (int)labelFontSizeChanger.Value;
            _settings.MainMenuLabelAlignment = GetMainMenuLabelAlignmentValue();

            _settings.ShowLeftMenu = chkShowLeftMenu.Checked;
            _settings.ShowRightMenu = chkShowRightMenu.Checked;

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
                prop.UseThmeButtonImage = chkUseThemeButtonImage.Checked;
            }
            postProperties.SaveToConfiguration();

            _settings.Theme = displayTheme;
            _settings.MainBrandingImage = tbImgHeader.Text;
            _settings.UseMainBrandingImage = chkUseBrandingImageAsMainLogo.Checked;   
            _settings.MainLogoText = tbLogoText.Text;
            _settings.LogoImage = tbImgLogo.Text;

            _settings.ThemeBlueButtonColor = cbBlueThemeButton.Text;
            _settings.ThemeDarkButtonColor = cbDarkThemeButton.Text;
            _settings.ThemeGreenButtonColor = cbGreenThemeButton.Text;
            _settings.ThemeOrangeButtonColor = cbOrangeThemeButton.Text;
            _settings.ThemeRedButtonColor = cbRedThemeButton.Text;

            _settings.LogoTextColor = btnLogoTextColor.BackColor;

            _settings.Save();
        }

        private void RestoreButtonThemes()
        {
            String themeName = _settings.Theme;

            if (themeName == "btnThemeClassic" || themeName == "Classic")
            {
                btnThemeClassic.BackColor = System.Drawing.Color.LightGreen;
            }

            if (themeName == "btnThemeBlue" || themeName == "Blue")
            {
                btnThemeBlue.BackColor = System.Drawing.Color.LightGreen;
            }

            if (themeName == "btnThemeGreen" || themeName == "Green")
            {
                btnThemeGreen.BackColor = System.Drawing.Color.LightGreen;
            }

            if (themeName == "btnThemeDark" || themeName == "Dark")
            {
                btnThemeDark.BackColor = System.Drawing.Color.LightGreen;
            }

            if (themeName == "btnThemeRed" || themeName == "Red")
            {
                btnThemeRed.BackColor = System.Drawing.Color.LightGreen;
            }

            if (themeName == "btnThemeOrange" || themeName == "Orange")
            {
                btnThemeOrange.BackColor = System.Drawing.Color.LightGreen;
            }
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
            if (_settings.MainMenuLabelAlignment == "Left")
                rbLabelLeft.Checked = true;
            else if (_settings.MainMenuLabelAlignment == "Middle")
                rbLabelMiddle.Checked = true;
            else if (_settings.MainMenuLabelAlignment == "Right")
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
            _settings.PrintTicket = chkPrintTicket.Checked;
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

        private void OnThemeSelected(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            String themeName = b.Name;
            String selectedTheme = mainForm.ApplyTheme(themeName);
            displayTheme = selectedTheme;
        }

        private void OnBtnSetHeaderImage(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.InitialDirectory = Util.ProcessDir;
            fileDlg.Filter = "Image Files(*.PNG;*.JPG;*.BMP)|*.PNG;*.JPG;*.BMP|All files (*.*)|*.*";

            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbImgHeader.Text = fileDlg.FileName;
                _settings.MainBrandingImage = fileDlg.FileName;
            }
        }

        private void OnUseBrandingImageChecked(object sender, EventArgs e)
        {
            bool useBranding = chkUseBrandingImageAsMainLogo.Checked;

            tbImgHeader.Enabled         = useBranding;
            btnSetBrandingImage.Enabled = useBranding;  

            tbLogoText.Enabled      = !useBranding;
            tbImgLogo.Enabled       = !useBranding;
            btnSetLogoImg.Enabled   = !useBranding;

            //mainForm.SetupBranding(useBranding);
        }

        private void OnBtnSetLogoImage(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.InitialDirectory = Util.ProcessDir;
            fileDlg.Filter = "Image Files(*.PNG;*.JPG;*.BMP)|*.PNG;*.JPG;*.BMP|All files (*.*)|*.*";

            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbImgLogo.Text = fileDlg.FileName;
                _settings.LogoImage = fileDlg.FileName;
            }
        }

        private void OnBtnImageOn(object sender, EventArgs e)
        {
            var postId = cbSelectPost.Text;

            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.InitialDirectory = Util.ProcessDir;
            fileDlg.Filter = "Image Files(*.PNG;*.JPG;*.BMP)|*.PNG;*.JPG;*.BMP|All files (*.*)|*.*";

            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbPostBtnImgOn.Text = fileDlg.FileName;
            }
        }

        private void OnBtnImageOff(object sender, EventArgs e)
        {
            var postId = cbSelectPost.Text;

            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.InitialDirectory = Util.ProcessDir;
            fileDlg.Filter = "Image Files(*.PNG;*.JPG;*.BMP)|*.PNG;*.JPG;*.BMP|All files (*.*)|*.*";

            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbPostBtnImgOff.Text = fileDlg.FileName;
            }
        }

        private void OnChkUseThemeButtonChanged(object sender, EventArgs e)
        {
            btnPostImgOnSelect.Enabled = !chkUseThemeButtonImage.Checked;
            btnPostImgOffSelect.Enabled = !chkUseThemeButtonImage.Checked;
            tbPostBtnImgOn.ReadOnly = chkUseThemeButtonImage.Checked;
            tbPostBtnImgOff.ReadOnly = chkUseThemeButtonImage.Checked;
        }

        private void OnCbThemeButton(object sender, EventArgs e)
        {
            if (sender == cbBlueThemeButton )
            {
                _settings.ThemeBlueButtonColor = cbBlueThemeButton.Text;
            }
            else if (sender == cbRedThemeButton)
            {
                _settings.ThemeRedButtonColor = cbRedThemeButton.Text;
            }
            else if (sender == cbDarkThemeButton)
            {
                _settings.ThemeDarkButtonColor = cbDarkThemeButton.Text;
            }
            else if (sender == cbGreenThemeButton)
            {
                _settings.ThemeGreenButtonColor = cbGreenThemeButton.Text;
            }
            else if (sender == cbOrangeThemeButton)
            {
                _settings.ThemeOrangeButtonColor = cbOrangeThemeButton.Text;
            }
        }

        private void OnLogoTextColor(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();

            dlg.AllowFullOpen = true;
            dlg.FullOpen = true;

            //dlg.ShowHelp = true;
            dlg.Color = btnLogoTextColor.BackColor;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                btnLogoTextColor.BackColor = dlg.Color;
                _settings.LogoTextColor = dlg.Color;
                mainForm.SetLogoTextColor(dlg.Color);
            }
        }
    }
}

