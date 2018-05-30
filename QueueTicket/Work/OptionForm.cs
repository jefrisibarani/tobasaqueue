using System;
using System.Windows.Forms;

namespace Tobasa
{
    public partial class OptionForm : Form
    {
      
        public OptionForm()
        {
            InitializeComponent();

            /// Populate cbPost
            cbPost.Items.Clear();
            string[] cbPostItems = new string[Properties.Settings.Default.UIPostList.Count];
            Properties.Settings.Default.UIPostList.CopyTo(cbPostItems, 0);
            cbPost.Items.AddRange(cbPostItems);
            cbPost.Text = Properties.Settings.Default.StationPost;

            RestoreSettings();
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

            post0Name.Text = Properties.Settings.Default.Post0Name;
            post0Post.Text = Properties.Settings.Default.Post0Post;
            post0Caption.Text = Properties.Settings.Default.Post0Caption;
            post0Chk.Checked = Properties.Settings.Default.Post0Enabled;
            post0ImgOn.Text = Properties.Settings.Default.Post0BtnImgOn;
            post0ImgOff.Text = Properties.Settings.Default.Post0BtnImgOff;
            post0Copies.Value = (decimal)Properties.Settings.Default.Post0PrintCopies;
            post0PrintHeader.Text = Properties.Settings.Default.Post0PrintHeader;

            post1Name.Text = Properties.Settings.Default.Post1Name;
            post1Post.Text = Properties.Settings.Default.Post1Post;
            post1Caption.Text = Properties.Settings.Default.Post1Caption;
            post1Chk.Checked = Properties.Settings.Default.Post1Enabled;
            post1ImgOn.Text = Properties.Settings.Default.Post1BtnImgOn;
            post1ImgOff.Text = Properties.Settings.Default.Post1BtnImgOff;
            post1Copies.Value = (decimal)Properties.Settings.Default.Post1PrintCopies;
            post1PrintHeader.Text = Properties.Settings.Default.Post1PrintHeader;

            post2Name.Text = Properties.Settings.Default.Post2Name;
            post2Post.Text = Properties.Settings.Default.Post2Post;
            post2Caption.Text = Properties.Settings.Default.Post2Caption;
            post2Chk.Checked = Properties.Settings.Default.Post2Enabled;
            post2ImgOn.Text = Properties.Settings.Default.Post2BtnImgOn;
            post2ImgOff.Text = Properties.Settings.Default.Post2BtnImgOff;
            post2Copies.Value = (decimal)Properties.Settings.Default.Post2PrintCopies;
            post2PrintHeader.Text = Properties.Settings.Default.Post2PrintHeader;

            post3Name.Text = Properties.Settings.Default.Post3Name;
            post3Post.Text = Properties.Settings.Default.Post3Post;
            post3Caption.Text = Properties.Settings.Default.Post3Caption;
            post3Chk.Checked = Properties.Settings.Default.Post3Enabled;
            post3ChkVisible.Checked = Properties.Settings.Default.Post3Visible;
            post3ImgOn.Text = Properties.Settings.Default.Post3BtnImgOn;
            post3ImgOff.Text = Properties.Settings.Default.Post3BtnImgOff;
            post3Copies.Value = (decimal)Properties.Settings.Default.Post3PrintCopies;
            post3PrintHeader.Text = Properties.Settings.Default.Post3PrintHeader;

            post4Name.Text = Properties.Settings.Default.Post4Name;
            post4Post.Text = Properties.Settings.Default.Post4Post;
            post4Caption.Text = Properties.Settings.Default.Post4Caption;
            post4Chk.Checked = Properties.Settings.Default.Post4Enabled;
            post4ChkVisible.Checked = Properties.Settings.Default.Post4Visible;
            post4ImgOn.Text = Properties.Settings.Default.Post4BtnImgOn;
            post4ImgOff.Text = Properties.Settings.Default.Post4BtnImgOff;
            post4Copies.Value = (decimal)Properties.Settings.Default.Post4PrintCopies;
            post4PrintHeader.Text = Properties.Settings.Default.Post4PrintHeader;

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

            Properties.Settings.Default.Post0Name = post0Name.Text;
            Properties.Settings.Default.Post0Post = post0Post.Text;
            Properties.Settings.Default.Post0Caption = post0Caption.Text;
            Properties.Settings.Default.Post0Enabled = post0Chk.Checked;
            Properties.Settings.Default.Post0BtnImgOn = post0ImgOn.Text;
            Properties.Settings.Default.Post0BtnImgOff = post0ImgOff.Text;
            Properties.Settings.Default.Post0PrintCopies = (short)post0Copies.Value;
            Properties.Settings.Default.Post0PrintHeader = post0PrintHeader.Text;

            Properties.Settings.Default.Post1Name = post1Name.Text;
            Properties.Settings.Default.Post1Post = post1Post.Text;
            Properties.Settings.Default.Post1Caption = post1Caption.Text;
            Properties.Settings.Default.Post1Enabled = post1Chk.Checked;
            Properties.Settings.Default.Post1BtnImgOn = post1ImgOn.Text;
            Properties.Settings.Default.Post1BtnImgOff = post1ImgOff.Text;
            Properties.Settings.Default.Post1PrintCopies = (short)post1Copies.Value;
            Properties.Settings.Default.Post1PrintHeader = post1PrintHeader.Text;

            Properties.Settings.Default.Post2Name = post2Name.Text;
            Properties.Settings.Default.Post2Post = post2Post.Text;
            Properties.Settings.Default.Post2Caption = post2Caption.Text;
            Properties.Settings.Default.Post2Enabled = post2Chk.Checked;
            Properties.Settings.Default.Post2BtnImgOn = post2ImgOn.Text;
            Properties.Settings.Default.Post2BtnImgOff = post2ImgOff.Text;
            Properties.Settings.Default.Post2PrintCopies = (short)post2Copies.Value;
            Properties.Settings.Default.Post2PrintHeader = post2PrintHeader.Text;

            Properties.Settings.Default.Post3Name = post3Name.Text;
            Properties.Settings.Default.Post3Post = post3Post.Text;
            Properties.Settings.Default.Post3Caption = post3Caption.Text;
            Properties.Settings.Default.Post3Enabled = post3Chk.Checked;
            Properties.Settings.Default.Post3Visible = post3ChkVisible.Checked;
            Properties.Settings.Default.Post3BtnImgOn = post3ImgOn.Text;
            Properties.Settings.Default.Post3BtnImgOff = post3ImgOff.Text;
            Properties.Settings.Default.Post3PrintCopies = (short)post3Copies.Value;
            Properties.Settings.Default.Post3PrintHeader = post3PrintHeader.Text;

            Properties.Settings.Default.Post4Name = post4Name.Text;
            Properties.Settings.Default.Post4Post = post4Post.Text;
            Properties.Settings.Default.Post4Caption = post4Caption.Text;
            Properties.Settings.Default.Post4Enabled = post4Chk.Checked;
            Properties.Settings.Default.Post4Visible = post4ChkVisible.Checked;
            Properties.Settings.Default.Post4BtnImgOn = post4ImgOn.Text;
            Properties.Settings.Default.Post4BtnImgOff = post4ImgOff.Text;
            Properties.Settings.Default.Post4PrintCopies = (short)post4Copies.Value;
            Properties.Settings.Default.Post4PrintHeader = post4PrintHeader.Text;

            Properties.Settings.Default.Save();
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
                Button btn = (Button)sender ;
                if (btn == btnSetPost0ImgOn)
                    post0ImgOn.Text = fileDlg.FileName;
                else if (btn == btnSetPost0ImgOff)
                    post0ImgOff.Text = fileDlg.FileName;
                else if (btn == btnSetPost1ImgOn)
                    post1ImgOn.Text = fileDlg.FileName;
                else if (btn == btnSetPost1ImgOff)
                    post1ImgOff.Text = fileDlg.FileName;
                else if (btn == btnSetPost2ImgOn)
                    post2ImgOn.Text = fileDlg.FileName;
                else if (btn == btnSetPost2ImgOff)
                    post2ImgOff.Text = fileDlg.FileName;
                else if (btn == btnSetPost3ImgOn)
                    post3ImgOn.Text = fileDlg.FileName;
                else if (btn == btnSetPost3ImgOff)
                    post3ImgOff.Text = fileDlg.FileName;
                else if (btn == btnSetPost4ImgOn)
                    post4ImgOn.Text = fileDlg.FileName;
                else if (btn == btnSetPost4ImgOff)
                    post4ImgOff.Text = fileDlg.FileName;
                else if (btn == btnSetLogoImg)
                    tbImgLogo.Text = fileDlg.FileName;
                else if (btn == btnSetHeaderImg)
                    tbImgHeader.Text = fileDlg.FileName;
                else if (btn == btnSetHeaderBgImg)
                    tbImgBG.Text = fileDlg.FileName;
            }
        }

        private void OnPrintCopiesValueChanged(object sender, EventArgs e)
        {
            if ((NumericUpDown)sender == post0Copies)
                Properties.Settings.Default.Post0PrintCopies = (short)post0Copies.Value;
            else if ((NumericUpDown)sender == post1Copies)
                Properties.Settings.Default.Post1PrintCopies = (short)post1Copies.Value;
            else if ((NumericUpDown)sender == post2Copies)
                Properties.Settings.Default.Post2PrintCopies = (short)post2Copies.Value;
            else if ((NumericUpDown)sender == post3Copies)
                Properties.Settings.Default.Post3PrintCopies = (short)post3Copies.Value;
            else if ((NumericUpDown)sender == post4Copies)
                Properties.Settings.Default.Post4PrintCopies = (short)post4Copies.Value;
        }

        private void OnPrintTicketChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PrintTicket = chkPrintTicket.Checked;
        }
    }
}
