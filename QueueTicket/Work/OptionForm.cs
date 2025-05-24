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
        private PostPropertyCollection postProperties = new PostPropertyCollection();
        private String displayTheme = "Classic";
        private MainForm mainForm = null;
        public OptionForm(MainForm mainForm)
        {
            this.mainForm = mainForm;

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
            tbServer.Text = Properties.Settings.Default.QueueServerHost;
            tbPort.Text = Properties.Settings.Default.QueueServerPort.ToString();
            tbStation.Text = Properties.Settings.Default.StationName;
            cbPost.Text = Properties.Settings.Default.StationPost;
            
            chkStartFullScreen.Checked = Properties.Settings.Default.StartDisplayFullScreen;
            chkPrintTicket.Checked = Properties.Settings.Default.PrintTicket;

            txtRuntext0.Text = Properties.Settings.Default.RunningText0;
            txtRuntext1.Text = Properties.Settings.Default.RunningText1;
            txtPrintFooter.Text = Properties.Settings.Default.PrintFooter;

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

            displayTheme = Properties.Settings.Default.Theme;
            tbImgHeader.Text = Properties.Settings.Default.MainBrandingImage;
            tbImgLogo.Text = Properties.Settings.Default.LogoImage;

            bool useBrandingImage = Properties.Settings.Default.UseMainBrandingImage;

            tbImgHeader.Enabled = useBrandingImage;
            btnSetBrandingImage.Enabled = useBrandingImage;
            tbLogoText.Enabled = !useBrandingImage;
            tbImgLogo.Enabled = !useBrandingImage;
            btnSetLogoImg.Enabled = !useBrandingImage;


            chkUseBrandingImageAsMainLogo.Checked = Properties.Settings.Default.UseMainBrandingImage;
            tbLogoText.Text = Tobasa.Properties.Settings.Default.MainLogoText;

            RestoreButtonThemes();

            cbBlueThemeButton.Text = Properties.Settings.Default.ThemeBlueButtonColor;
            cbDarkThemeButton.Text = Properties.Settings.Default.ThemeDarkButtonColor;
            cbGreenThemeButton.Text = Properties.Settings.Default.ThemeGreenButtonColor;
            cbOrangeThemeButton.Text = Properties.Settings.Default.ThemeOrangeButtonColor;
            cbRedThemeButton.Text = Properties.Settings.Default.ThemeRedButtonColor;

            mainForm.SetLogoTextColor(Properties.Settings.Default.LogoTextColor);
            btnLogoTextColor.BackColor = Properties.Settings.Default.LogoTextColor;
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
                prop.UseThmeButtonImage = chkUseThemeButtonImage.Checked;
            }
            postProperties.SaveToConfiguration();

            Properties.Settings.Default.Theme = displayTheme;
            Properties.Settings.Default.MainBrandingImage = tbImgHeader.Text;
            Properties.Settings.Default.UseMainBrandingImage = chkUseBrandingImageAsMainLogo.Checked;   
            Properties.Settings.Default.MainLogoText = tbLogoText.Text;
            Properties.Settings.Default.LogoImage = tbImgLogo.Text;

            Properties.Settings.Default.ThemeBlueButtonColor = cbBlueThemeButton.Text;
            Properties.Settings.Default.ThemeDarkButtonColor = cbDarkThemeButton.Text;
            Properties.Settings.Default.ThemeGreenButtonColor = cbGreenThemeButton.Text;
            Properties.Settings.Default.ThemeOrangeButtonColor = cbOrangeThemeButton.Text;
            Properties.Settings.Default.ThemeRedButtonColor = cbRedThemeButton.Text;

            Properties.Settings.Default.LogoTextColor = btnLogoTextColor.BackColor;

            Properties.Settings.Default.Save();
        }

        private void RestoreButtonThemes()
        {
            String themeName = Properties.Settings.Default.Theme;

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
                Tobasa.Properties.Settings.Default.MainBrandingImage = fileDlg.FileName;
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
                Tobasa.Properties.Settings.Default.LogoImage = fileDlg.FileName;
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
            var _settings = Tobasa.Properties.Settings.Default;

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
            var _settings = Tobasa.Properties.Settings.Default;

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

