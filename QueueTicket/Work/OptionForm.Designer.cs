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

namespace Tobasa
{
    partial class OptionForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.cbPost = new System.Windows.Forms.ComboBox();
            this.chkConnectAtStart = new System.Windows.Forms.CheckBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbStation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkStartFullScreen = new System.Windows.Forms.CheckBox();
            this.chkPrintTicket = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.tabPostOpt = new System.Windows.Forms.TabPage();
            this.cbSelectPost = new System.Windows.Forms.ComboBox();
            this.label91 = new System.Windows.Forms.Label();
            this.tbPostTicketHeader = new System.Windows.Forms.TextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.pickPostPrintCopies = new System.Windows.Forms.NumericUpDown();
            this.btnPostImgOffSelect = new System.Windows.Forms.Button();
            this.tbPostBtnImgOff = new System.Windows.Forms.TextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.btnPostImgOnSelect = new System.Windows.Forms.Button();
            this.tbPostBtnImgOn = new System.Windows.Forms.TextBox();
            this.chkPostVisible = new System.Windows.Forms.CheckBox();
            this.tbPostCaption = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.chkPostEnabled = new System.Windows.Forms.CheckBox();
            this.tbPostId = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.tbPostName = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.tabMics = new System.Windows.Forms.TabPage();
            this.chkShowRightMenu = new System.Windows.Forms.CheckBox();
            this.chkShowLeftMenu = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelFontSizeChanger = new System.Windows.Forms.NumericUpDown();
            this.label48 = new System.Windows.Forms.Label();
            this.rbLabelRight = new System.Windows.Forms.RadioButton();
            this.rbLabelMiddle = new System.Windows.Forms.RadioButton();
            this.rbLabelLeft = new System.Windows.Forms.RadioButton();
            this.label47 = new System.Windows.Forms.Label();
            this.chkButtonsWithLabel = new System.Windows.Forms.CheckBox();
            this.btnFontSizeChanger = new System.Windows.Forms.NumericUpDown();
            this.label46 = new System.Windows.Forms.Label();
            this.txtPrintFooter = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtRuntext1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtRuntext0 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabHeader = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLogoText = new System.Windows.Forms.TextBox();
            this.chkUseBrandingImageAsMainLogo = new System.Windows.Forms.CheckBox();
            this.btnThemeOrange = new System.Windows.Forms.Button();
            this.btnThemeBlue = new System.Windows.Forms.Button();
            this.btnThemeRed = new System.Windows.Forms.Button();
            this.btnThemeDark = new System.Windows.Forms.Button();
            this.btnThemeGreen = new System.Windows.Forms.Button();
            this.btnThemeClassic = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btnSetBrandingImage = new System.Windows.Forms.Button();
            this.tbImgHeader = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnSetLogoImg = new System.Windows.Forms.Button();
            this.tbImgLogo = new System.Windows.Forms.TextBox();
            this.mainTab.SuspendLayout();
            this.tabPostOpt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pickPostPrintCopies)).BeginInit();
            this.tabMics.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelFontSizeChanger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFontSizeChanger)).BeginInit();
            this.tabHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPost
            // 
            this.cbPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPost.FormattingEnabled = true;
            this.cbPost.Items.AddRange(new object[] {
            "POST0"});
            this.cbPost.Location = new System.Drawing.Point(97, 43);
            this.cbPost.MaxLength = 10;
            this.cbPost.Name = "cbPost";
            this.cbPost.Size = new System.Drawing.Size(110, 21);
            this.cbPost.TabIndex = 34;
            // 
            // chkConnectAtStart
            // 
            this.chkConnectAtStart.AutoSize = true;
            this.chkConnectAtStart.Checked = true;
            this.chkConnectAtStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConnectAtStart.Enabled = false;
            this.chkConnectAtStart.Location = new System.Drawing.Point(242, 20);
            this.chkConnectAtStart.Name = "chkConnectAtStart";
            this.chkConnectAtStart.Size = new System.Drawing.Size(113, 17);
            this.chkConnectAtStart.TabIndex = 33;
            this.chkConnectAtStart.Text = "Connect at startup";
            this.chkConnectAtStart.UseVisualStyleBackColor = true;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(97, 90);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(110, 20);
            this.tbPort.TabIndex = 32;
            this.tbPort.Text = "2345";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(97, 68);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(110, 20);
            this.tbServer.TabIndex = 31;
            this.tbServer.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Server";
            // 
            // tbStation
            // 
            this.tbStation.Location = new System.Drawing.Point(97, 20);
            this.tbStation.Name = "tbStation";
            this.tbStation.Size = new System.Drawing.Size(110, 20);
            this.tbStation.TabIndex = 27;
            this.tbStation.Text = "TICKET#1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Post";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Station Name";
            // 
            // chkStartFullScreen
            // 
            this.chkStartFullScreen.AutoSize = true;
            this.chkStartFullScreen.Checked = true;
            this.chkStartFullScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartFullScreen.Location = new System.Drawing.Point(242, 43);
            this.chkStartFullScreen.Name = "chkStartFullScreen";
            this.chkStartFullScreen.Size = new System.Drawing.Size(99, 17);
            this.chkStartFullScreen.TabIndex = 36;
            this.chkStartFullScreen.Text = "Start Fullscreen";
            this.chkStartFullScreen.UseVisualStyleBackColor = true;
            // 
            // chkPrintTicket
            // 
            this.chkPrintTicket.AutoSize = true;
            this.chkPrintTicket.Checked = true;
            this.chkPrintTicket.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintTicket.Location = new System.Drawing.Point(242, 66);
            this.chkPrintTicket.Name = "chkPrintTicket";
            this.chkPrintTicket.Size = new System.Drawing.Size(80, 17);
            this.chkPrintTicket.TabIndex = 37;
            this.chkPrintTicket.Text = "Print Ticket";
            this.chkPrintTicket.UseVisualStyleBackColor = true;
            this.chkPrintTicket.CheckedChanged += new System.EventHandler(this.OnPrintTicketChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(394, 363);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.OnSave);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(478, 363);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 39;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnClose);
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.tabPostOpt);
            this.mainTab.Controls.Add(this.tabMics);
            this.mainTab.Controls.Add(this.tabHeader);
            this.mainTab.Location = new System.Drawing.Point(24, 128);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(529, 229);
            this.mainTab.TabIndex = 40;
            // 
            // tabPostOpt
            // 
            this.tabPostOpt.Controls.Add(this.cbSelectPost);
            this.tabPostOpt.Controls.Add(this.label91);
            this.tabPostOpt.Controls.Add(this.tbPostTicketHeader);
            this.tabPostOpt.Controls.Add(this.label84);
            this.tabPostOpt.Controls.Add(this.label85);
            this.tabPostOpt.Controls.Add(this.pickPostPrintCopies);
            this.tabPostOpt.Controls.Add(this.btnPostImgOffSelect);
            this.tabPostOpt.Controls.Add(this.tbPostBtnImgOff);
            this.tabPostOpt.Controls.Add(this.label86);
            this.tabPostOpt.Controls.Add(this.label87);
            this.tabPostOpt.Controls.Add(this.btnPostImgOnSelect);
            this.tabPostOpt.Controls.Add(this.tbPostBtnImgOn);
            this.tabPostOpt.Controls.Add(this.chkPostVisible);
            this.tabPostOpt.Controls.Add(this.tbPostCaption);
            this.tabPostOpt.Controls.Add(this.label88);
            this.tabPostOpt.Controls.Add(this.chkPostEnabled);
            this.tabPostOpt.Controls.Add(this.tbPostId);
            this.tabPostOpt.Controls.Add(this.label89);
            this.tabPostOpt.Controls.Add(this.tbPostName);
            this.tabPostOpt.Controls.Add(this.label90);
            this.tabPostOpt.Location = new System.Drawing.Point(4, 22);
            this.tabPostOpt.Name = "tabPostOpt";
            this.tabPostOpt.Padding = new System.Windows.Forms.Padding(3);
            this.tabPostOpt.Size = new System.Drawing.Size(521, 203);
            this.tabPostOpt.TabIndex = 13;
            this.tabPostOpt.Text = "Post Options";
            this.tabPostOpt.UseVisualStyleBackColor = true;
            // 
            // cbSelectPost
            // 
            this.cbSelectPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectPost.FormattingEnabled = true;
            this.cbSelectPost.Items.AddRange(new object[] {
            "POST0"});
            this.cbSelectPost.Location = new System.Drawing.Point(111, 12);
            this.cbSelectPost.Name = "cbSelectPost";
            this.cbSelectPost.Size = new System.Drawing.Size(121, 21);
            this.cbSelectPost.TabIndex = 70;
            this.cbSelectPost.SelectedIndexChanged += new System.EventHandler(this.OnCbSelectPostChanged);
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(18, 15);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(42, 13);
            this.label91.TabIndex = 69;
            this.label91.Text = "Post ID";
            // 
            // tbPostTicketHeader
            // 
            this.tbPostTicketHeader.Location = new System.Drawing.Point(111, 108);
            this.tbPostTicketHeader.MaxLength = 25;
            this.tbPostTicketHeader.Name = "tbPostTicketHeader";
            this.tbPostTicketHeader.Size = new System.Drawing.Size(219, 20);
            this.tbPostTicketHeader.TabIndex = 67;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(18, 111);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(75, 13);
            this.label84.TabIndex = 66;
            this.label84.Text = "Ticket Header";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(402, 111);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(63, 13);
            this.label85.TabIndex = 65;
            this.label85.Text = "Print Copies";
            // 
            // pickPostPrintCopies
            // 
            this.pickPostPrintCopies.Location = new System.Drawing.Point(469, 109);
            this.pickPostPrintCopies.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.pickPostPrintCopies.Name = "pickPostPrintCopies";
            this.pickPostPrintCopies.Size = new System.Drawing.Size(36, 20);
            this.pickPostPrintCopies.TabIndex = 64;
            this.pickPostPrintCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnPostImgOffSelect
            // 
            this.btnPostImgOffSelect.Location = new System.Drawing.Point(480, 170);
            this.btnPostImgOffSelect.Name = "btnPostImgOffSelect";
            this.btnPostImgOffSelect.Size = new System.Drawing.Size(27, 23);
            this.btnPostImgOffSelect.TabIndex = 63;
            this.btnPostImgOffSelect.Text = "...";
            this.btnPostImgOffSelect.UseVisualStyleBackColor = true;
            // 
            // tbPostBtnImgOff
            // 
            this.tbPostBtnImgOff.Location = new System.Drawing.Point(111, 172);
            this.tbPostBtnImgOff.Name = "tbPostBtnImgOff";
            this.tbPostBtnImgOff.Size = new System.Drawing.Size(363, 20);
            this.tbPostBtnImgOff.TabIndex = 62;
            this.tbPostBtnImgOff.Text = "\r\n        ";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(18, 175);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(87, 13);
            this.label86.TabIndex = 61;
            this.label86.Text = "Button Image Off";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(18, 149);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(70, 13);
            this.label87.TabIndex = 56;
            this.label87.Text = "Button Image";
            // 
            // btnPostImgOnSelect
            // 
            this.btnPostImgOnSelect.Location = new System.Drawing.Point(480, 144);
            this.btnPostImgOnSelect.Name = "btnPostImgOnSelect";
            this.btnPostImgOnSelect.Size = new System.Drawing.Size(27, 23);
            this.btnPostImgOnSelect.TabIndex = 55;
            this.btnPostImgOnSelect.Text = "...";
            this.btnPostImgOnSelect.UseVisualStyleBackColor = true;
            // 
            // tbPostBtnImgOn
            // 
            this.tbPostBtnImgOn.Location = new System.Drawing.Point(111, 146);
            this.tbPostBtnImgOn.Name = "tbPostBtnImgOn";
            this.tbPostBtnImgOn.Size = new System.Drawing.Size(363, 20);
            this.tbPostBtnImgOn.TabIndex = 54;
            this.tbPostBtnImgOn.Text = "\r\n        ";
            // 
            // chkPostVisible
            // 
            this.chkPostVisible.AutoSize = true;
            this.chkPostVisible.Checked = true;
            this.chkPostVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPostVisible.Location = new System.Drawing.Point(405, 84);
            this.chkPostVisible.Name = "chkPostVisible";
            this.chkPostVisible.Size = new System.Drawing.Size(56, 17);
            this.chkPostVisible.TabIndex = 48;
            this.chkPostVisible.Text = "Visible";
            this.chkPostVisible.UseVisualStyleBackColor = true;
            // 
            // tbPostCaption
            // 
            this.tbPostCaption.Location = new System.Drawing.Point(111, 82);
            this.tbPostCaption.Multiline = true;
            this.tbPostCaption.Name = "tbPostCaption";
            this.tbPostCaption.Size = new System.Drawing.Size(219, 20);
            this.tbPostCaption.TabIndex = 47;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(18, 85);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(43, 13);
            this.label88.TabIndex = 46;
            this.label88.Text = "Caption";
            // 
            // chkPostEnabled
            // 
            this.chkPostEnabled.AutoSize = true;
            this.chkPostEnabled.Checked = true;
            this.chkPostEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPostEnabled.Location = new System.Drawing.Point(405, 58);
            this.chkPostEnabled.Name = "chkPostEnabled";
            this.chkPostEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkPostEnabled.TabIndex = 45;
            this.chkPostEnabled.Text = "Enabled";
            this.chkPostEnabled.UseVisualStyleBackColor = true;
            // 
            // tbPostId
            // 
            this.tbPostId.Location = new System.Drawing.Point(395, 12);
            this.tbPostId.MaxLength = 32;
            this.tbPostId.Name = "tbPostId";
            this.tbPostId.Size = new System.Drawing.Size(110, 20);
            this.tbPostId.TabIndex = 44;
            this.tbPostId.Visible = false;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(352, 15);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(28, 13);
            this.label89.TabIndex = 43;
            this.label89.Text = "Post";
            this.label89.Visible = false;
            // 
            // tbPostName
            // 
            this.tbPostName.Location = new System.Drawing.Point(111, 55);
            this.tbPostName.Name = "tbPostName";
            this.tbPostName.Size = new System.Drawing.Size(121, 20);
            this.tbPostName.TabIndex = 42;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(18, 58);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(35, 13);
            this.label90.TabIndex = 41;
            this.label90.Text = "Name";
            // 
            // tabMics
            // 
            this.tabMics.Controls.Add(this.chkShowRightMenu);
            this.tabMics.Controls.Add(this.chkShowLeftMenu);
            this.tabMics.Controls.Add(this.panel1);
            this.tabMics.Controls.Add(this.txtPrintFooter);
            this.tabMics.Controls.Add(this.label19);
            this.tabMics.Controls.Add(this.txtRuntext1);
            this.tabMics.Controls.Add(this.label18);
            this.tabMics.Controls.Add(this.txtRuntext0);
            this.tabMics.Controls.Add(this.label17);
            this.tabMics.Location = new System.Drawing.Point(4, 22);
            this.tabMics.Name = "tabMics";
            this.tabMics.Padding = new System.Windows.Forms.Padding(3);
            this.tabMics.Size = new System.Drawing.Size(521, 203);
            this.tabMics.TabIndex = 5;
            this.tabMics.Text = "Miscellaneous";
            this.tabMics.UseVisualStyleBackColor = true;
            // 
            // chkShowRightMenu
            // 
            this.chkShowRightMenu.AutoSize = true;
            this.chkShowRightMenu.Checked = true;
            this.chkShowRightMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowRightMenu.Location = new System.Drawing.Point(214, 171);
            this.chkShowRightMenu.Name = "chkShowRightMenu";
            this.chkShowRightMenu.Size = new System.Drawing.Size(111, 17);
            this.chkShowRightMenu.TabIndex = 70;
            this.chkShowRightMenu.Text = "Show Right Menu";
            this.chkShowRightMenu.UseVisualStyleBackColor = true;
            this.chkShowRightMenu.CheckedChanged += new System.EventHandler(this.OnChkShowRight);
            // 
            // chkShowLeftMenu
            // 
            this.chkShowLeftMenu.AutoSize = true;
            this.chkShowLeftMenu.Checked = true;
            this.chkShowLeftMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLeftMenu.Location = new System.Drawing.Point(92, 171);
            this.chkShowLeftMenu.Name = "chkShowLeftMenu";
            this.chkShowLeftMenu.Size = new System.Drawing.Size(104, 17);
            this.chkShowLeftMenu.TabIndex = 69;
            this.chkShowLeftMenu.Text = "Show Left Menu";
            this.chkShowLeftMenu.UseVisualStyleBackColor = true;
            this.chkShowLeftMenu.CheckedChanged += new System.EventHandler(this.OnChkShowLeft);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelFontSizeChanger);
            this.panel1.Controls.Add(this.label48);
            this.panel1.Controls.Add(this.rbLabelRight);
            this.panel1.Controls.Add(this.rbLabelMiddle);
            this.panel1.Controls.Add(this.rbLabelLeft);
            this.panel1.Controls.Add(this.label47);
            this.panel1.Controls.Add(this.chkButtonsWithLabel);
            this.panel1.Controls.Add(this.btnFontSizeChanger);
            this.panel1.Controls.Add(this.label46);
            this.panel1.Location = new System.Drawing.Point(9, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 61);
            this.panel1.TabIndex = 68;
            // 
            // labelFontSizeChanger
            // 
            this.labelFontSizeChanger.Location = new System.Drawing.Point(440, 6);
            this.labelFontSizeChanger.Maximum = new decimal(new int[] {
            26,
            0,
            0,
            0});
            this.labelFontSizeChanger.Name = "labelFontSizeChanger";
            this.labelFontSizeChanger.Size = new System.Drawing.Size(36, 20);
            this.labelFontSizeChanger.TabIndex = 72;
            this.labelFontSizeChanger.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(383, 8);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(51, 13);
            this.label48.TabIndex = 73;
            this.label48.Text = "Font Size";
            // 
            // rbLabelRight
            // 
            this.rbLabelRight.AutoSize = true;
            this.rbLabelRight.Location = new System.Drawing.Point(334, 33);
            this.rbLabelRight.Name = "rbLabelRight";
            this.rbLabelRight.Size = new System.Drawing.Size(50, 17);
            this.rbLabelRight.TabIndex = 71;
            this.rbLabelRight.TabStop = true;
            this.rbLabelRight.Text = "Right";
            this.rbLabelRight.UseVisualStyleBackColor = true;
            // 
            // rbLabelMiddle
            // 
            this.rbLabelMiddle.AutoSize = true;
            this.rbLabelMiddle.Location = new System.Drawing.Point(272, 33);
            this.rbLabelMiddle.Name = "rbLabelMiddle";
            this.rbLabelMiddle.Size = new System.Drawing.Size(56, 17);
            this.rbLabelMiddle.TabIndex = 70;
            this.rbLabelMiddle.TabStop = true;
            this.rbLabelMiddle.Text = "Middle";
            this.rbLabelMiddle.UseVisualStyleBackColor = true;
            // 
            // rbLabelLeft
            // 
            this.rbLabelLeft.AutoSize = true;
            this.rbLabelLeft.Location = new System.Drawing.Point(223, 33);
            this.rbLabelLeft.Name = "rbLabelLeft";
            this.rbLabelLeft.Size = new System.Drawing.Size(43, 17);
            this.rbLabelLeft.TabIndex = 69;
            this.rbLabelLeft.TabStop = true;
            this.rbLabelLeft.Text = "Left";
            this.rbLabelLeft.UseVisualStyleBackColor = true;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(220, 8);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(152, 13);
            this.label47.TabIndex = 68;
            this.label47.Text = "Main menu label text alignment";
            // 
            // chkButtonsWithLabel
            // 
            this.chkButtonsWithLabel.AutoSize = true;
            this.chkButtonsWithLabel.Checked = true;
            this.chkButtonsWithLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkButtonsWithLabel.Location = new System.Drawing.Point(13, 7);
            this.chkButtonsWithLabel.Name = "chkButtonsWithLabel";
            this.chkButtonsWithLabel.Size = new System.Drawing.Size(129, 17);
            this.chkButtonsWithLabel.TabIndex = 49;
            this.chkButtonsWithLabel.Text = "Draw label on buttons";
            this.chkButtonsWithLabel.UseVisualStyleBackColor = true;
            // 
            // btnFontSizeChanger
            // 
            this.btnFontSizeChanger.Location = new System.Drawing.Point(67, 33);
            this.btnFontSizeChanger.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.btnFontSizeChanger.Name = "btnFontSizeChanger";
            this.btnFontSizeChanger.Size = new System.Drawing.Size(36, 20);
            this.btnFontSizeChanger.TabIndex = 66;
            this.btnFontSizeChanger.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(10, 35);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(51, 13);
            this.label46.TabIndex = 67;
            this.label46.Text = "Font Size";
            // 
            // txtPrintFooter
            // 
            this.txtPrintFooter.Location = new System.Drawing.Point(92, 138);
            this.txtPrintFooter.Name = "txtPrintFooter";
            this.txtPrintFooter.Size = new System.Drawing.Size(412, 20);
            this.txtPrintFooter.TabIndex = 37;
            this.txtPrintFooter.Text = "Print Footer";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 138);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Ticket Footer";
            // 
            // txtRuntext1
            // 
            this.txtRuntext1.Location = new System.Drawing.Point(92, 112);
            this.txtRuntext1.Multiline = true;
            this.txtRuntext1.Name = "txtRuntext1";
            this.txtRuntext1.Size = new System.Drawing.Size(412, 20);
            this.txtRuntext1.TabIndex = 35;
            this.txtRuntext1.Text = "Terimakasih";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 115);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "Running Text 1";
            // 
            // txtRuntext0
            // 
            this.txtRuntext0.Location = new System.Drawing.Point(92, 87);
            this.txtRuntext0.Multiline = true;
            this.txtRuntext0.Name = "txtRuntext0";
            this.txtRuntext0.Size = new System.Drawing.Size(412, 20);
            this.txtRuntext0.TabIndex = 33;
            this.txtRuntext0.Text = "Selamat Datang";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 90);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 32;
            this.label17.Text = "Running Text 0";
            // 
            // tabHeader
            // 
            this.tabHeader.Controls.Add(this.label31);
            this.tabHeader.Controls.Add(this.btnSetLogoImg);
            this.tabHeader.Controls.Add(this.tbImgLogo);
            this.tabHeader.Controls.Add(this.label1);
            this.tabHeader.Controls.Add(this.tbLogoText);
            this.tabHeader.Controls.Add(this.chkUseBrandingImageAsMainLogo);
            this.tabHeader.Controls.Add(this.btnThemeOrange);
            this.tabHeader.Controls.Add(this.btnThemeBlue);
            this.tabHeader.Controls.Add(this.btnThemeRed);
            this.tabHeader.Controls.Add(this.btnThemeDark);
            this.tabHeader.Controls.Add(this.btnThemeGreen);
            this.tabHeader.Controls.Add(this.btnThemeClassic);
            this.tabHeader.Controls.Add(this.label11);
            this.tabHeader.Controls.Add(this.label21);
            this.tabHeader.Controls.Add(this.btnSetBrandingImage);
            this.tabHeader.Controls.Add(this.tbImgHeader);
            this.tabHeader.Location = new System.Drawing.Point(4, 22);
            this.tabHeader.Name = "tabHeader";
            this.tabHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tabHeader.Size = new System.Drawing.Size(521, 203);
            this.tabHeader.TabIndex = 6;
            this.tabHeader.Text = "Header Images & Theme";
            this.tabHeader.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Logo Text";
            // 
            // tbLogoText
            // 
            this.tbLogoText.Location = new System.Drawing.Point(97, 95);
            this.tbLogoText.Name = "tbLogoText";
            this.tbLogoText.Size = new System.Drawing.Size(368, 20);
            this.tbLogoText.TabIndex = 62;
            this.tbLogoText.Text = "Tobasa Queue system";
            // 
            // chkUseBrandingImageAsMainLogo
            // 
            this.chkUseBrandingImageAsMainLogo.AutoSize = true;
            this.chkUseBrandingImageAsMainLogo.Location = new System.Drawing.Point(11, 21);
            this.chkUseBrandingImageAsMainLogo.Name = "chkUseBrandingImageAsMainLogo";
            this.chkUseBrandingImageAsMainLogo.Size = new System.Drawing.Size(182, 17);
            this.chkUseBrandingImageAsMainLogo.TabIndex = 61;
            this.chkUseBrandingImageAsMainLogo.Text = "Use branding image as main logo";
            this.chkUseBrandingImageAsMainLogo.UseVisualStyleBackColor = true;
            this.chkUseBrandingImageAsMainLogo.CheckedChanged += new System.EventHandler(this.OnUseBrandingImageChecked);
            // 
            // btnThemeOrange
            // 
            this.btnThemeOrange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemeOrange.Location = new System.Drawing.Point(438, 127);
            this.btnThemeOrange.Name = "btnThemeOrange";
            this.btnThemeOrange.Size = new System.Drawing.Size(70, 23);
            this.btnThemeOrange.TabIndex = 60;
            this.btnThemeOrange.Text = "Orange";
            this.btnThemeOrange.UseVisualStyleBackColor = true;
            this.btnThemeOrange.Click += new System.EventHandler(this.OnThemeSelected);
            // 
            // btnThemeBlue
            // 
            this.btnThemeBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemeBlue.Location = new System.Drawing.Point(130, 127);
            this.btnThemeBlue.Name = "btnThemeBlue";
            this.btnThemeBlue.Size = new System.Drawing.Size(70, 23);
            this.btnThemeBlue.TabIndex = 59;
            this.btnThemeBlue.Text = "Blue";
            this.btnThemeBlue.UseVisualStyleBackColor = true;
            this.btnThemeBlue.Click += new System.EventHandler(this.OnThemeSelected);
            // 
            // btnThemeRed
            // 
            this.btnThemeRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemeRed.Location = new System.Drawing.Point(361, 127);
            this.btnThemeRed.Name = "btnThemeRed";
            this.btnThemeRed.Size = new System.Drawing.Size(70, 23);
            this.btnThemeRed.TabIndex = 58;
            this.btnThemeRed.Text = "Red";
            this.btnThemeRed.UseVisualStyleBackColor = true;
            this.btnThemeRed.Click += new System.EventHandler(this.OnThemeSelected);
            // 
            // btnThemeDark
            // 
            this.btnThemeDark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemeDark.Location = new System.Drawing.Point(285, 127);
            this.btnThemeDark.Name = "btnThemeDark";
            this.btnThemeDark.Size = new System.Drawing.Size(70, 23);
            this.btnThemeDark.TabIndex = 57;
            this.btnThemeDark.Text = "Dark";
            this.btnThemeDark.UseVisualStyleBackColor = true;
            this.btnThemeDark.Click += new System.EventHandler(this.OnThemeSelected);
            // 
            // btnThemeGreen
            // 
            this.btnThemeGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemeGreen.Location = new System.Drawing.Point(208, 127);
            this.btnThemeGreen.Name = "btnThemeGreen";
            this.btnThemeGreen.Size = new System.Drawing.Size(70, 23);
            this.btnThemeGreen.TabIndex = 56;
            this.btnThemeGreen.Text = "Green";
            this.btnThemeGreen.UseVisualStyleBackColor = true;
            this.btnThemeGreen.Click += new System.EventHandler(this.OnThemeSelected);
            // 
            // btnThemeClassic
            // 
            this.btnThemeClassic.BackColor = System.Drawing.Color.Transparent;
            this.btnThemeClassic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemeClassic.Location = new System.Drawing.Point(53, 127);
            this.btnThemeClassic.Name = "btnThemeClassic";
            this.btnThemeClassic.Size = new System.Drawing.Size(70, 23);
            this.btnThemeClassic.TabIndex = 55;
            this.btnThemeClassic.Text = "Classic";
            this.btnThemeClassic.UseVisualStyleBackColor = false;
            this.btnThemeClassic.Click += new System.EventHandler(this.OnThemeSelected);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Theme";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 48);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(81, 13);
            this.label21.TabIndex = 29;
            this.label21.Text = "Branding Image";
            // 
            // btnSetBrandingImage
            // 
            this.btnSetBrandingImage.Location = new System.Drawing.Point(372, 45);
            this.btnSetBrandingImage.Name = "btnSetBrandingImage";
            this.btnSetBrandingImage.Size = new System.Drawing.Size(31, 23);
            this.btnSetBrandingImage.TabIndex = 28;
            this.btnSetBrandingImage.Text = "...";
            this.btnSetBrandingImage.UseVisualStyleBackColor = true;
            this.btnSetBrandingImage.Click += new System.EventHandler(this.OnBtnSetHeaderImage);
            // 
            // tbImgHeader
            // 
            this.tbImgHeader.Location = new System.Drawing.Point(97, 45);
            this.tbImgHeader.Name = "tbImgHeader";
            this.tbImgHeader.Size = new System.Drawing.Size(269, 20);
            this.tbImgHeader.TabIndex = 27;
            this.tbImgHeader.Text = ".\\img\\MainBrandingImage.png\r\n        ";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(9, 73);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(63, 13);
            this.label31.TabIndex = 66;
            this.label31.Text = "Logo Image";
            // 
            // btnSetLogoImg
            // 
            this.btnSetLogoImg.Location = new System.Drawing.Point(372, 68);
            this.btnSetLogoImg.Name = "btnSetLogoImg";
            this.btnSetLogoImg.Size = new System.Drawing.Size(31, 23);
            this.btnSetLogoImg.TabIndex = 65;
            this.btnSetLogoImg.Text = "...";
            this.btnSetLogoImg.UseVisualStyleBackColor = true;
            this.btnSetLogoImg.Click += new System.EventHandler(this.OnBtnSetLogoImage);
            // 
            // tbImgLogo
            // 
            this.tbImgLogo.Location = new System.Drawing.Point(97, 70);
            this.tbImgLogo.Name = "tbImgLogo";
            this.tbImgLogo.Size = new System.Drawing.Size(269, 20);
            this.tbImgLogo.TabIndex = 64;
            this.tbImgLogo.Text = "\r\n        ";
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 398);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkPrintTicket);
            this.Controls.Add(this.chkStartFullScreen);
            this.Controls.Add(this.cbPost);
            this.Controls.Add(this.chkConnectAtStart);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbStation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OptionForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.mainTab.ResumeLayout(false);
            this.tabPostOpt.ResumeLayout(false);
            this.tabPostOpt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pickPostPrintCopies)).EndInit();
            this.tabMics.ResumeLayout(false);
            this.tabMics.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelFontSizeChanger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFontSizeChanger)).EndInit();
            this.tabHeader.ResumeLayout(false);
            this.tabHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPost;
        private System.Windows.Forms.CheckBox chkConnectAtStart;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbStation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkStartFullScreen;
        private System.Windows.Forms.CheckBox chkPrintTicket;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage tabMics;
        private System.Windows.Forms.TextBox txtRuntext1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtRuntext0;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPrintFooter;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage tabHeader;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnSetBrandingImage;
        private System.Windows.Forms.TextBox tbImgHeader;
        private System.Windows.Forms.CheckBox chkButtonsWithLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown btnFontSizeChanger;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.RadioButton rbLabelRight;
        private System.Windows.Forms.RadioButton rbLabelMiddle;
        private System.Windows.Forms.RadioButton rbLabelLeft;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.NumericUpDown labelFontSizeChanger;
        private System.Windows.Forms.Label label48;
		private System.Windows.Forms.TabPage tabPostOpt;
		private System.Windows.Forms.TextBox tbPostTicketHeader;
		private System.Windows.Forms.Label label84;
		private System.Windows.Forms.Label label85;
		private System.Windows.Forms.NumericUpDown pickPostPrintCopies;
		private System.Windows.Forms.Button btnPostImgOffSelect;
		private System.Windows.Forms.TextBox tbPostBtnImgOff;
		private System.Windows.Forms.Label label86;
		private System.Windows.Forms.Label label87;
		private System.Windows.Forms.Button btnPostImgOnSelect;
		private System.Windows.Forms.TextBox tbPostBtnImgOn;
		private System.Windows.Forms.CheckBox chkPostVisible;
		private System.Windows.Forms.TextBox tbPostCaption;
		private System.Windows.Forms.Label label88;
		private System.Windows.Forms.CheckBox chkPostEnabled;
		private System.Windows.Forms.TextBox tbPostId;
		private System.Windows.Forms.Label label89;
		private System.Windows.Forms.TextBox tbPostName;
		private System.Windows.Forms.Label label90;
		private System.Windows.Forms.Label label91;
		private System.Windows.Forms.ComboBox cbSelectPost;
		private System.Windows.Forms.CheckBox chkShowRightMenu;
		private System.Windows.Forms.CheckBox chkShowLeftMenu;
        private System.Windows.Forms.Button btnThemeOrange;
        private System.Windows.Forms.Button btnThemeBlue;
        private System.Windows.Forms.Button btnThemeRed;
        private System.Windows.Forms.Button btnThemeDark;
        private System.Windows.Forms.Button btnThemeGreen;
        private System.Windows.Forms.Button btnThemeClassic;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkUseBrandingImageAsMainLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLogoText;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button btnSetLogoImg;
        private System.Windows.Forms.TextBox tbImgLogo;
    }
}