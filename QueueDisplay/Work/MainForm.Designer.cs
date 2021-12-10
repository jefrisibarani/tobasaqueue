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

namespace Tobasa
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOption = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabVideoClip = new System.Windows.Forms.TabPage();
            this.chkPlayVideoStartup = new System.Windows.Forms.CheckBox();
            this.btnSetClipFolder = new System.Windows.Forms.Button();
            this.rbPlayFromFolder = new System.Windows.Forms.RadioButton();
            this.rbOpenClip = new System.Windows.Forms.RadioButton();
            this.tbClipFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabVideoDevice = new System.Windows.Forms.TabPage();
            this.gbVideo = new System.Windows.Forms.GroupBox();
            this.btnDeviceProp = new System.Windows.Forms.Button();
            this.btnCrossbarProp = new System.Windows.Forms.Button();
            this.btnInputProp = new System.Windows.Forms.Button();
            this.btnChangeVideoInput = new System.Windows.Forms.Button();
            this.radComposite = new System.Windows.Forms.RadioButton();
            this.radSVideo = new System.Windows.Forms.RadioButton();
            this.radTuner = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.cbPost = new System.Windows.Forms.ComboBox();
            this.chkConnectAtStart = new System.Windows.Forms.CheckBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveOpt = new System.Windows.Forms.Button();
            this.tbStation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.chkShowCenterMiddleDiv = new System.Windows.Forms.CheckBox();
            this.chkShowRightPosts = new System.Windows.Forms.CheckBox();
            this.chkShowLeftPosts = new System.Windows.Forms.CheckBox();
            this.chkShowInfoTextTop1 = new System.Windows.Forms.CheckBox();
            this.chkAudioLoketIDUseAlphabet = new System.Windows.Forms.CheckBox();
            this.chkPlaySimpleNotification = new System.Windows.Forms.CheckBox();
            this.chkShowInfoTextTop0 = new System.Windows.Forms.CheckBox();
            this.tbTextLogo = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.btnSetLogoImg = new System.Windows.Forms.Button();
            this.tbImgLogo = new System.Windows.Forms.TextBox();
            this.chkShowLogo = new System.Windows.Forms.CheckBox();
            this.chkSpellNumber = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.chkSetUnderscore = new System.Windows.Forms.CheckBox();
            this.btnAnimationColor = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSetAudioFolder = new System.Windows.Forms.Button();
            this.tbAudioFolder = new System.Windows.Forms.TextBox();
            this.tabPostOpt = new System.Windows.Forms.TabPage();
            this.cbSelectPost = new System.Windows.Forms.ComboBox();
            this.label91 = new System.Windows.Forms.Label();
            this.chkPostVisible = new System.Windows.Forms.CheckBox();
            this.chkPostPlayAudio = new System.Windows.Forms.CheckBox();
            this.tbPostRunText = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.tbPostCaption = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.tbPostId = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.tbPostName = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.tabPageRunText = new System.Windows.Forms.TabPage();
            this.txtRuntext1 = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.txtRuntext0 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnDoubleRate = new System.Windows.Forms.Button();
            this.btnHalfRate = new System.Windows.Forms.Button();
            this.btnResetRate = new System.Windows.Forms.Button();
            this.btnDecreaseRate = new System.Windows.Forms.Button();
            this.btnIncreaseRate = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.gbDisplaySource = new System.Windows.Forms.GroupBox();
            this.radVideoDevice = new System.Windows.Forms.RadioButton();
            this.radVideoClip = new System.Windows.Forms.RadioButton();
            this.btnOpenCloseVideo = new System.Windows.Forms.Button();
            this.volSlider = new System.Windows.Forms.TrackBar();
            this.btnSave = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowDisplay = new System.Windows.Forms.Button();
            this.btnSwitchDisplay = new System.Windows.Forms.Button();
            this.chkMute = new System.Windows.Forms.CheckBox();
            this.pnlMore = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabVideoClip.SuspendLayout();
            this.tabVideoDevice.SuspendLayout();
            this.gbVideo.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.tabPostOpt.SuspendLayout();
            this.tabPageRunText.SuspendLayout();
            this.gbDisplaySource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volSlider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnlMore.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuPlayer,
            this.menuTools,
            this.mnuAbout});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(649, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOption,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuOption
            // 
            this.mnuOption.Name = "mnuOption";
            this.mnuOption.Size = new System.Drawing.Size(180, 22);
            this.mnuOption.Text = "Show &Options";
            this.mnuOption.Click += new System.EventHandler(this.OnOption);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuExit.Size = new System.Drawing.Size(180, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.OnExit);
            // 
            // mnuPlayer
            // 
            this.mnuPlayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowDisplay,
            this.mnuFullScreen});
            this.mnuPlayer.Name = "mnuPlayer";
            this.mnuPlayer.Size = new System.Drawing.Size(57, 20);
            this.mnuPlayer.Text = "&Display";
            // 
            // mnuShowDisplay
            // 
            this.mnuShowDisplay.Name = "mnuShowDisplay";
            this.mnuShowDisplay.Size = new System.Drawing.Size(180, 22);
            this.mnuShowDisplay.Text = "Show Display";
            this.mnuShowDisplay.Click += new System.EventHandler(this.OnShowDisplay);
            // 
            // mnuFullScreen
            // 
            this.mnuFullScreen.Name = "mnuFullScreen";
            this.mnuFullScreen.Size = new System.Drawing.Size(180, 22);
            this.mnuFullScreen.Text = "Full Screen";
            this.mnuFullScreen.Click += new System.EventHandler(this.OnFullScreen);
            // 
            // menuTools
            // 
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(46, 20);
            this.menuTools.Text = "&Tools";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.mnuAbout.Size = new System.Drawing.Size(52, 20);
            this.mnuAbout.Text = "&About";
            this.mnuAbout.Click += new System.EventHandler(this.OnAbout);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabVideoClip);
            this.tabControl.Controls.Add(this.tabVideoDevice);
            this.tabControl.Controls.Add(this.tabOptions);
            this.tabControl.Controls.Add(this.tabMisc);
            this.tabControl.Controls.Add(this.tabPostOpt);
            this.tabControl.Controls.Add(this.tabPageRunText);
            this.tabControl.Location = new System.Drawing.Point(0, 5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(625, 224);
            this.tabControl.TabIndex = 2;
            // 
            // tabVideoClip
            // 
            this.tabVideoClip.BackColor = System.Drawing.Color.Transparent;
            this.tabVideoClip.Controls.Add(this.chkPlayVideoStartup);
            this.tabVideoClip.Controls.Add(this.btnSetClipFolder);
            this.tabVideoClip.Controls.Add(this.rbPlayFromFolder);
            this.tabVideoClip.Controls.Add(this.rbOpenClip);
            this.tabVideoClip.Controls.Add(this.tbClipFolder);
            this.tabVideoClip.Controls.Add(this.label2);
            this.tabVideoClip.Location = new System.Drawing.Point(4, 22);
            this.tabVideoClip.Name = "tabVideoClip";
            this.tabVideoClip.Padding = new System.Windows.Forms.Padding(3);
            this.tabVideoClip.Size = new System.Drawing.Size(617, 198);
            this.tabVideoClip.TabIndex = 0;
            this.tabVideoClip.Text = "Video Clip File";
            // 
            // chkPlayVideoStartup
            // 
            this.chkPlayVideoStartup.AutoSize = true;
            this.chkPlayVideoStartup.Location = new System.Drawing.Point(17, 107);
            this.chkPlayVideoStartup.Name = "chkPlayVideoStartup";
            this.chkPlayVideoStartup.Size = new System.Drawing.Size(143, 17);
            this.chkPlayVideoStartup.TabIndex = 18;
            this.chkPlayVideoStartup.Text = "Play video files at startup";
            this.chkPlayVideoStartup.UseVisualStyleBackColor = true;
            // 
            // btnSetClipFolder
            // 
            this.btnSetClipFolder.Location = new System.Drawing.Point(301, 62);
            this.btnSetClipFolder.Name = "btnSetClipFolder";
            this.btnSetClipFolder.Size = new System.Drawing.Size(31, 23);
            this.btnSetClipFolder.TabIndex = 8;
            this.btnSetClipFolder.Text = "...";
            this.btnSetClipFolder.UseVisualStyleBackColor = true;
            this.btnSetClipFolder.Click += new System.EventHandler(this.OnSetClipFolder);
            // 
            // rbPlayFromFolder
            // 
            this.rbPlayFromFolder.AutoSize = true;
            this.rbPlayFromFolder.Location = new System.Drawing.Point(17, 42);
            this.rbPlayFromFolder.Name = "rbPlayFromFolder";
            this.rbPlayFromFolder.Size = new System.Drawing.Size(155, 17);
            this.rbPlayFromFolder.TabIndex = 7;
            this.rbPlayFromFolder.Text = "Play video files in this forder";
            this.rbPlayFromFolder.UseVisualStyleBackColor = true;
            this.rbPlayFromFolder.CheckedChanged += new System.EventHandler(this.OnRbPlayFromFolder);
            // 
            // rbOpenClip
            // 
            this.rbOpenClip.AutoSize = true;
            this.rbOpenClip.Checked = true;
            this.rbOpenClip.Location = new System.Drawing.Point(17, 19);
            this.rbOpenClip.Name = "rbOpenClip";
            this.rbOpenClip.Size = new System.Drawing.Size(108, 17);
            this.rbOpenClip.TabIndex = 6;
            this.rbOpenClip.TabStop = true;
            this.rbOpenClip.Text = "Open a video clip";
            this.rbOpenClip.UseVisualStyleBackColor = true;
            this.rbOpenClip.CheckedChanged += new System.EventHandler(this.OnRbOpenClip);
            // 
            // tbClipFolder
            // 
            this.tbClipFolder.Location = new System.Drawing.Point(35, 65);
            this.tbClipFolder.Name = "tbClipFolder";
            this.tbClipFolder.Size = new System.Drawing.Size(260, 20);
            this.tbClipFolder.TabIndex = 5;
            this.tbClipFolder.Text = ".\\movie";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 4;
            // 
            // tabVideoDevice
            // 
            this.tabVideoDevice.Controls.Add(this.gbVideo);
            this.tabVideoDevice.Location = new System.Drawing.Point(4, 22);
            this.tabVideoDevice.Name = "tabVideoDevice";
            this.tabVideoDevice.Padding = new System.Windows.Forms.Padding(3);
            this.tabVideoDevice.Size = new System.Drawing.Size(617, 198);
            this.tabVideoDevice.TabIndex = 1;
            this.tabVideoDevice.Text = "Video Device";
            this.tabVideoDevice.UseVisualStyleBackColor = true;
            // 
            // gbVideo
            // 
            this.gbVideo.Controls.Add(this.btnDeviceProp);
            this.gbVideo.Controls.Add(this.btnCrossbarProp);
            this.gbVideo.Controls.Add(this.btnInputProp);
            this.gbVideo.Controls.Add(this.btnChangeVideoInput);
            this.gbVideo.Controls.Add(this.radComposite);
            this.gbVideo.Controls.Add(this.radSVideo);
            this.gbVideo.Controls.Add(this.radTuner);
            this.gbVideo.Controls.Add(this.label1);
            this.gbVideo.Controls.Add(this.cbDevice);
            this.gbVideo.Location = new System.Drawing.Point(4, 10);
            this.gbVideo.Name = "gbVideo";
            this.gbVideo.Size = new System.Drawing.Size(399, 146);
            this.gbVideo.TabIndex = 5;
            this.gbVideo.TabStop = false;
            this.gbVideo.Text = "Video input control";
            // 
            // btnDeviceProp
            // 
            this.btnDeviceProp.Location = new System.Drawing.Point(11, 109);
            this.btnDeviceProp.Name = "btnDeviceProp";
            this.btnDeviceProp.Size = new System.Drawing.Size(106, 23);
            this.btnDeviceProp.TabIndex = 9;
            this.btnDeviceProp.Text = "Device properties";
            this.btnDeviceProp.UseVisualStyleBackColor = true;
            this.btnDeviceProp.Click += new System.EventHandler(this.OnDeviceProp);
            // 
            // btnCrossbarProp
            // 
            this.btnCrossbarProp.Location = new System.Drawing.Point(283, 109);
            this.btnCrossbarProp.Name = "btnCrossbarProp";
            this.btnCrossbarProp.Size = new System.Drawing.Size(110, 23);
            this.btnCrossbarProp.TabIndex = 8;
            this.btnCrossbarProp.Text = "CrossBar properties";
            this.btnCrossbarProp.UseVisualStyleBackColor = true;
            this.btnCrossbarProp.Click += new System.EventHandler(this.OnCrossbarProp);
            // 
            // btnInputProp
            // 
            this.btnInputProp.Location = new System.Drawing.Point(143, 109);
            this.btnInputProp.Name = "btnInputProp";
            this.btnInputProp.Size = new System.Drawing.Size(110, 23);
            this.btnInputProp.TabIndex = 7;
            this.btnInputProp.Text = "Input properties";
            this.btnInputProp.UseVisualStyleBackColor = true;
            this.btnInputProp.Click += new System.EventHandler(this.OnInputProp);
            // 
            // btnChangeVideoInput
            // 
            this.btnChangeVideoInput.Location = new System.Drawing.Point(11, 70);
            this.btnChangeVideoInput.Name = "btnChangeVideoInput";
            this.btnChangeVideoInput.Size = new System.Drawing.Size(82, 23);
            this.btnChangeVideoInput.TabIndex = 6;
            this.btnChangeVideoInput.Text = "Change Input";
            this.btnChangeVideoInput.UseVisualStyleBackColor = true;
            this.btnChangeVideoInput.Click += new System.EventHandler(this.OnChangeVideoInput);
            // 
            // radComposite
            // 
            this.radComposite.AutoSize = true;
            this.radComposite.Location = new System.Drawing.Point(300, 73);
            this.radComposite.Name = "radComposite";
            this.radComposite.Size = new System.Drawing.Size(74, 17);
            this.radComposite.TabIndex = 5;
            this.radComposite.TabStop = true;
            this.radComposite.Text = "Composite";
            this.radComposite.UseVisualStyleBackColor = true;
            // 
            // radSVideo
            // 
            this.radSVideo.AutoSize = true;
            this.radSVideo.Location = new System.Drawing.Point(215, 73);
            this.radSVideo.Name = "radSVideo";
            this.radSVideo.Size = new System.Drawing.Size(59, 17);
            this.radSVideo.TabIndex = 4;
            this.radSVideo.TabStop = true;
            this.radSVideo.Text = "SVideo";
            this.radSVideo.UseVisualStyleBackColor = true;
            // 
            // radTuner
            // 
            this.radTuner.AutoSize = true;
            this.radTuner.Checked = true;
            this.radTuner.Location = new System.Drawing.Point(121, 73);
            this.radTuner.Name = "radTuner";
            this.radTuner.Size = new System.Drawing.Size(53, 17);
            this.radTuner.TabIndex = 3;
            this.radTuner.TabStop = true;
            this.radTuner.Text = "Tuner";
            this.radTuner.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Video device";
            // 
            // cbDevice
            // 
            this.cbDevice.Enabled = false;
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(87, 33);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(306, 21);
            this.cbDevice.TabIndex = 1;
            this.cbDevice.SelectedIndexChanged += new System.EventHandler(this.OnDeviceSelected);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.cbPost);
            this.tabOptions.Controls.Add(this.chkConnectAtStart);
            this.tabOptions.Controls.Add(this.tbPort);
            this.tabOptions.Controls.Add(this.tbServer);
            this.tabOptions.Controls.Add(this.label4);
            this.tabOptions.Controls.Add(this.label5);
            this.tabOptions.Controls.Add(this.btnSaveOpt);
            this.tabOptions.Controls.Add(this.tbStation);
            this.tabOptions.Controls.Add(this.label3);
            this.tabOptions.Controls.Add(this.label6);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(617, 198);
            this.tabOptions.TabIndex = 2;
            this.tabOptions.Text = "Connection";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // cbPost
            // 
            this.cbPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPost.FormattingEnabled = true;
            this.cbPost.Items.AddRange(new object[] {
            "POST0"});
            this.cbPost.Location = new System.Drawing.Point(84, 34);
            this.cbPost.MaxLength = 10;
            this.cbPost.Name = "cbPost";
            this.cbPost.Size = new System.Drawing.Size(110, 21);
            this.cbPost.TabIndex = 24;
            // 
            // chkConnectAtStart
            // 
            this.chkConnectAtStart.AutoSize = true;
            this.chkConnectAtStart.Checked = true;
            this.chkConnectAtStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConnectAtStart.Enabled = false;
            this.chkConnectAtStart.Location = new System.Drawing.Point(84, 107);
            this.chkConnectAtStart.Name = "chkConnectAtStart";
            this.chkConnectAtStart.Size = new System.Drawing.Size(113, 17);
            this.chkConnectAtStart.TabIndex = 21;
            this.chkConnectAtStart.Text = "Connect at startup";
            this.chkConnectAtStart.UseVisualStyleBackColor = true;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(84, 81);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(110, 20);
            this.tbPort.TabIndex = 20;
            this.tbPort.Text = "2345";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(84, 59);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(110, 20);
            this.tbServer.TabIndex = 19;
            this.tbServer.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Server";
            // 
            // btnSaveOpt
            // 
            this.btnSaveOpt.Location = new System.Drawing.Point(67, 135);
            this.btnSaveOpt.Name = "btnSaveOpt";
            this.btnSaveOpt.Size = new System.Drawing.Size(130, 21);
            this.btnSaveOpt.TabIndex = 16;
            this.btnSaveOpt.Text = "&Save and Reconnect";
            this.btnSaveOpt.UseVisualStyleBackColor = true;
            this.btnSaveOpt.Click += new System.EventHandler(this.OnSaveOpt);
            // 
            // tbStation
            // 
            this.tbStation.Location = new System.Drawing.Point(84, 11);
            this.tbStation.Name = "tbStation";
            this.tbStation.Size = new System.Drawing.Size(110, 20);
            this.tbStation.TabIndex = 15;
            this.tbStation.Text = "DISP#1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Post";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Station name";
            // 
            // tabMisc
            // 
            this.tabMisc.Controls.Add(this.chkShowCenterMiddleDiv);
            this.tabMisc.Controls.Add(this.chkShowRightPosts);
            this.tabMisc.Controls.Add(this.chkShowLeftPosts);
            this.tabMisc.Controls.Add(this.chkShowInfoTextTop1);
            this.tabMisc.Controls.Add(this.chkAudioLoketIDUseAlphabet);
            this.tabMisc.Controls.Add(this.chkPlaySimpleNotification);
            this.tabMisc.Controls.Add(this.chkShowInfoTextTop0);
            this.tabMisc.Controls.Add(this.tbTextLogo);
            this.tabMisc.Controls.Add(this.label32);
            this.tabMisc.Controls.Add(this.label31);
            this.tabMisc.Controls.Add(this.btnSetLogoImg);
            this.tabMisc.Controls.Add(this.tbImgLogo);
            this.tabMisc.Controls.Add(this.chkShowLogo);
            this.tabMisc.Controls.Add(this.chkSpellNumber);
            this.tabMisc.Controls.Add(this.label10);
            this.tabMisc.Controls.Add(this.numericUpDown);
            this.tabMisc.Controls.Add(this.label9);
            this.tabMisc.Controls.Add(this.chkSetUnderscore);
            this.tabMisc.Controls.Add(this.btnAnimationColor);
            this.tabMisc.Controls.Add(this.label8);
            this.tabMisc.Controls.Add(this.label7);
            this.tabMisc.Controls.Add(this.btnSetAudioFolder);
            this.tabMisc.Controls.Add(this.tbAudioFolder);
            this.tabMisc.Location = new System.Drawing.Point(4, 22);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tabMisc.Size = new System.Drawing.Size(617, 198);
            this.tabMisc.TabIndex = 3;
            this.tabMisc.Text = "Miscellaneous";
            this.tabMisc.UseVisualStyleBackColor = true;
            // 
            // chkShowCenterMiddleDiv
            // 
            this.chkShowCenterMiddleDiv.AutoSize = true;
            this.chkShowCenterMiddleDiv.Location = new System.Drawing.Point(392, 83);
            this.chkShowCenterMiddleDiv.Name = "chkShowCenterMiddleDiv";
            this.chkShowCenterMiddleDiv.Size = new System.Drawing.Size(150, 17);
            this.chkShowCenterMiddleDiv.TabIndex = 38;
            this.chkShowCenterMiddleDiv.Text = "Show Center Midle Layout";
            this.chkShowCenterMiddleDiv.UseVisualStyleBackColor = true;
            // 
            // chkShowRightPosts
            // 
            this.chkShowRightPosts.AutoSize = true;
            this.chkShowRightPosts.Location = new System.Drawing.Point(392, 129);
            this.chkShowRightPosts.Name = "chkShowRightPosts";
            this.chkShowRightPosts.Size = new System.Drawing.Size(104, 17);
            this.chkShowRightPosts.TabIndex = 37;
            this.chkShowRightPosts.Text = "Show right posts";
            this.chkShowRightPosts.UseVisualStyleBackColor = true;
            // 
            // chkShowLeftPosts
            // 
            this.chkShowLeftPosts.AutoSize = true;
            this.chkShowLeftPosts.Location = new System.Drawing.Point(392, 106);
            this.chkShowLeftPosts.Name = "chkShowLeftPosts";
            this.chkShowLeftPosts.Size = new System.Drawing.Size(98, 17);
            this.chkShowLeftPosts.TabIndex = 36;
            this.chkShowLeftPosts.Text = "Show left posts";
            this.chkShowLeftPosts.UseVisualStyleBackColor = true;
            // 
            // chkShowInfoTextTop1
            // 
            this.chkShowInfoTextTop1.AutoSize = true;
            this.chkShowInfoTextTop1.Location = new System.Drawing.Point(392, 60);
            this.chkShowInfoTextTop1.Name = "chkShowInfoTextTop1";
            this.chkShowInfoTextTop1.Size = new System.Drawing.Size(196, 17);
            this.chkShowInfoTextTop1.TabIndex = 35;
            this.chkShowInfoTextTop1.Text = "Show top info strip (server message)";
            this.chkShowInfoTextTop1.UseVisualStyleBackColor = true;
            // 
            // chkAudioLoketIDUseAlphabet
            // 
            this.chkAudioLoketIDUseAlphabet.AutoSize = true;
            this.chkAudioLoketIDUseAlphabet.Checked = true;
            this.chkAudioLoketIDUseAlphabet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAudioLoketIDUseAlphabet.Location = new System.Drawing.Point(211, 151);
            this.chkAudioLoketIDUseAlphabet.Name = "chkAudioLoketIDUseAlphabet";
            this.chkAudioLoketIDUseAlphabet.Size = new System.Drawing.Size(156, 17);
            this.chkAudioLoketIDUseAlphabet.TabIndex = 34;
            this.chkAudioLoketIDUseAlphabet.Text = "Counter audio use alphabet";
            this.chkAudioLoketIDUseAlphabet.UseVisualStyleBackColor = true;
            this.chkAudioLoketIDUseAlphabet.CheckedChanged += new System.EventHandler(this.OnCounterUseAlphabet);
            // 
            // chkPlaySimpleNotification
            // 
            this.chkPlaySimpleNotification.AutoSize = true;
            this.chkPlaySimpleNotification.Checked = true;
            this.chkPlaySimpleNotification.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlaySimpleNotification.Location = new System.Drawing.Point(14, 151);
            this.chkPlaySimpleNotification.Name = "chkPlaySimpleNotification";
            this.chkPlaySimpleNotification.Size = new System.Drawing.Size(164, 17);
            this.chkPlaySimpleNotification.TabIndex = 33;
            this.chkPlaySimpleNotification.Text = "Play simple sound notification";
            this.chkPlaySimpleNotification.UseVisualStyleBackColor = true;
            this.chkPlaySimpleNotification.CheckedChanged += new System.EventHandler(this.OnPlaySimpleNotification);
            // 
            // chkShowInfoTextTop0
            // 
            this.chkShowInfoTextTop0.AutoSize = true;
            this.chkShowInfoTextTop0.Location = new System.Drawing.Point(392, 37);
            this.chkShowInfoTextTop0.Name = "chkShowInfoTextTop0";
            this.chkShowInfoTextTop0.Size = new System.Drawing.Size(221, 17);
            this.chkShowInfoTextTop0.TabIndex = 32;
            this.chkShowInfoTextTop0.Text = "Show top info strip (queue number called)";
            this.chkShowInfoTextTop0.UseVisualStyleBackColor = true;
            // 
            // tbTextLogo
            // 
            this.tbTextLogo.Location = new System.Drawing.Point(87, 38);
            this.tbTextLogo.Name = "tbTextLogo";
            this.tbTextLogo.Size = new System.Drawing.Size(229, 20);
            this.tbTextLogo.TabIndex = 31;
            this.tbTextLogo.Text = "\r\n        ";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(11, 38);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(55, 13);
            this.label32.TabIndex = 30;
            this.label32.Text = "Logo Text";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(11, 17);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(63, 13);
            this.label31.TabIndex = 29;
            this.label31.Text = "Logo Image";
            // 
            // btnSetLogoImg
            // 
            this.btnSetLogoImg.Location = new System.Drawing.Point(322, 14);
            this.btnSetLogoImg.Name = "btnSetLogoImg";
            this.btnSetLogoImg.Size = new System.Drawing.Size(31, 23);
            this.btnSetLogoImg.TabIndex = 28;
            this.btnSetLogoImg.Text = "...";
            this.btnSetLogoImg.UseVisualStyleBackColor = true;
            this.btnSetLogoImg.Click += new System.EventHandler(this.OnSetLogoImg);
            // 
            // tbImgLogo
            // 
            this.tbImgLogo.Location = new System.Drawing.Point(87, 14);
            this.tbImgLogo.Name = "tbImgLogo";
            this.tbImgLogo.Size = new System.Drawing.Size(229, 20);
            this.tbImgLogo.TabIndex = 27;
            this.tbImgLogo.Text = "\r\n        ";
            // 
            // chkShowLogo
            // 
            this.chkShowLogo.AutoSize = true;
            this.chkShowLogo.Location = new System.Drawing.Point(392, 16);
            this.chkShowLogo.Name = "chkShowLogo";
            this.chkShowLogo.Size = new System.Drawing.Size(76, 17);
            this.chkShowLogo.TabIndex = 21;
            this.chkShowLogo.Text = "Show logo";
            this.chkShowLogo.UseVisualStyleBackColor = true;
            // 
            // chkSpellNumber
            // 
            this.chkSpellNumber.AutoSize = true;
            this.chkSpellNumber.Checked = true;
            this.chkSpellNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpellNumber.Location = new System.Drawing.Point(14, 173);
            this.chkSpellNumber.Name = "chkSpellNumber";
            this.chkSpellNumber.Size = new System.Drawing.Size(186, 17);
            this.chkSpellNumber.TabIndex = 20;
            this.chkSpellNumber.Text = "Spell numbers when playing audio";
            this.chkSpellNumber.UseVisualStyleBackColor = true;
            this.chkSpellNumber.CheckedChanged += new System.EventHandler(this.OnSpellNumber);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(177, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "second";
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(127, 117);
            this.numericUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Queue animation time";
            // 
            // chkSetUnderscore
            // 
            this.chkSetUnderscore.AutoSize = true;
            this.chkSetUnderscore.Checked = true;
            this.chkSetUnderscore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSetUnderscore.Location = new System.Drawing.Point(211, 174);
            this.chkSetUnderscore.Name = "chkSetUnderscore";
            this.chkSetUnderscore.Size = new System.Drawing.Size(205, 17);
            this.chkSetUnderscore.TabIndex = 16;
            this.chkSetUnderscore.Text = "Set number with underscore at startup";
            this.chkSetUnderscore.UseVisualStyleBackColor = true;
            // 
            // btnAnimationColor
            // 
            this.btnAnimationColor.Location = new System.Drawing.Point(127, 90);
            this.btnAnimationColor.Name = "btnAnimationColor";
            this.btnAnimationColor.Size = new System.Drawing.Size(61, 20);
            this.btnAnimationColor.TabIndex = 14;
            this.btnAnimationColor.Text = "...";
            this.btnAnimationColor.UseVisualStyleBackColor = true;
            this.btnAnimationColor.Click += new System.EventHandler(this.OnAnimationColor);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Queue animation color";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Audio location";
            // 
            // btnSetAudioFolder
            // 
            this.btnSetAudioFolder.Location = new System.Drawing.Point(322, 62);
            this.btnSetAudioFolder.Name = "btnSetAudioFolder";
            this.btnSetAudioFolder.Size = new System.Drawing.Size(31, 23);
            this.btnSetAudioFolder.TabIndex = 11;
            this.btnSetAudioFolder.Text = "...";
            this.btnSetAudioFolder.UseVisualStyleBackColor = true;
            this.btnSetAudioFolder.Click += new System.EventHandler(this.OnSetAudioFolder);
            // 
            // tbAudioFolder
            // 
            this.tbAudioFolder.Location = new System.Drawing.Point(87, 64);
            this.tbAudioFolder.Name = "tbAudioFolder";
            this.tbAudioFolder.Size = new System.Drawing.Size(229, 20);
            this.tbAudioFolder.TabIndex = 9;
            this.tbAudioFolder.Text = "\r\n        ";
            // 
            // tabPostOpt
            // 
            this.tabPostOpt.Controls.Add(this.cbSelectPost);
            this.tabPostOpt.Controls.Add(this.label91);
            this.tabPostOpt.Controls.Add(this.chkPostVisible);
            this.tabPostOpt.Controls.Add(this.chkPostPlayAudio);
            this.tabPostOpt.Controls.Add(this.tbPostRunText);
            this.tabPostOpt.Controls.Add(this.label57);
            this.tabPostOpt.Controls.Add(this.tbPostCaption);
            this.tabPostOpt.Controls.Add(this.label58);
            this.tabPostOpt.Controls.Add(this.tbPostId);
            this.tabPostOpt.Controls.Add(this.label59);
            this.tabPostOpt.Controls.Add(this.tbPostName);
            this.tabPostOpt.Controls.Add(this.label60);
            this.tabPostOpt.Location = new System.Drawing.Point(4, 22);
            this.tabPostOpt.Name = "tabPostOpt";
            this.tabPostOpt.Padding = new System.Windows.Forms.Padding(3);
            this.tabPostOpt.Size = new System.Drawing.Size(617, 198);
            this.tabPostOpt.TabIndex = 18;
            this.tabPostOpt.Text = "Post Options";
            this.tabPostOpt.UseVisualStyleBackColor = true;
            // 
            // cbSelectPost
            // 
            this.cbSelectPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectPost.FormattingEnabled = true;
            this.cbSelectPost.Items.AddRange(new object[] {
            "POST0"});
            this.cbSelectPost.Location = new System.Drawing.Point(106, 18);
            this.cbSelectPost.Name = "cbSelectPost";
            this.cbSelectPost.Size = new System.Drawing.Size(107, 21);
            this.cbSelectPost.TabIndex = 72;
            this.cbSelectPost.SelectedIndexChanged += new System.EventHandler(this.OnCbSelectPostChanged);
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(13, 21);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(42, 13);
            this.label91.TabIndex = 71;
            this.label91.Text = "Post ID";
            // 
            // chkPostVisible
            // 
            this.chkPostVisible.AutoSize = true;
            this.chkPostVisible.Location = new System.Drawing.Point(201, 151);
            this.chkPostVisible.Name = "chkPostVisible";
            this.chkPostVisible.Size = new System.Drawing.Size(56, 17);
            this.chkPostVisible.TabIndex = 53;
            this.chkPostVisible.Text = "Visible";
            this.chkPostVisible.UseVisualStyleBackColor = true;
            // 
            // chkPostPlayAudio
            // 
            this.chkPostPlayAudio.AutoSize = true;
            this.chkPostPlayAudio.Location = new System.Drawing.Point(106, 151);
            this.chkPostPlayAudio.Name = "chkPostPlayAudio";
            this.chkPostPlayAudio.Size = new System.Drawing.Size(75, 17);
            this.chkPostPlayAudio.TabIndex = 52;
            this.chkPostPlayAudio.Text = "Play audio";
            this.chkPostPlayAudio.UseVisualStyleBackColor = true;
            // 
            // tbPostRunText
            // 
            this.tbPostRunText.Location = new System.Drawing.Point(106, 116);
            this.tbPostRunText.Name = "tbPostRunText";
            this.tbPostRunText.Size = new System.Drawing.Size(216, 20);
            this.tbPostRunText.TabIndex = 49;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(13, 119);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(49, 13);
            this.label57.TabIndex = 48;
            this.label57.Text = "Info Text";
            // 
            // tbPostCaption
            // 
            this.tbPostCaption.Location = new System.Drawing.Point(106, 90);
            this.tbPostCaption.Name = "tbPostCaption";
            this.tbPostCaption.Size = new System.Drawing.Size(216, 20);
            this.tbPostCaption.TabIndex = 47;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(13, 93);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(43, 13);
            this.label58.TabIndex = 46;
            this.label58.Text = "Caption";
            // 
            // tbPostId
            // 
            this.tbPostId.Location = new System.Drawing.Point(432, 19);
            this.tbPostId.Name = "tbPostId";
            this.tbPostId.ReadOnly = true;
            this.tbPostId.Size = new System.Drawing.Size(107, 20);
            this.tbPostId.TabIndex = 44;
            this.tbPostId.Visible = false;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(339, 22);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(16, 13);
            this.label59.TabIndex = 43;
            this.label59.Text = "Id";
            this.label59.Visible = false;
            // 
            // tbPostName
            // 
            this.tbPostName.Location = new System.Drawing.Point(106, 64);
            this.tbPostName.Name = "tbPostName";
            this.tbPostName.Size = new System.Drawing.Size(107, 20);
            this.tbPostName.TabIndex = 42;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(13, 67);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(35, 13);
            this.label60.TabIndex = 41;
            this.label60.Text = "Name";
            // 
            // tabPageRunText
            // 
            this.tabPageRunText.Controls.Add(this.txtRuntext1);
            this.tabPageRunText.Controls.Add(this.label53);
            this.tabPageRunText.Controls.Add(this.txtRuntext0);
            this.tabPageRunText.Controls.Add(this.label54);
            this.tabPageRunText.Location = new System.Drawing.Point(4, 22);
            this.tabPageRunText.Name = "tabPageRunText";
            this.tabPageRunText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRunText.Size = new System.Drawing.Size(617, 198);
            this.tabPageRunText.TabIndex = 17;
            this.tabPageRunText.Text = "Running Text";
            this.tabPageRunText.UseVisualStyleBackColor = true;
            // 
            // txtRuntext1
            // 
            this.txtRuntext1.Location = new System.Drawing.Point(96, 43);
            this.txtRuntext1.Multiline = true;
            this.txtRuntext1.Name = "txtRuntext1";
            this.txtRuntext1.Size = new System.Drawing.Size(356, 30);
            this.txtRuntext1.TabIndex = 39;
            this.txtRuntext1.Text = "Terimakasih";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(10, 46);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(80, 13);
            this.label53.TabIndex = 38;
            this.label53.Text = "Running Text 1";
            // 
            // txtRuntext0
            // 
            this.txtRuntext0.Location = new System.Drawing.Point(96, 9);
            this.txtRuntext0.Multiline = true;
            this.txtRuntext0.Name = "txtRuntext0";
            this.txtRuntext0.Size = new System.Drawing.Size(356, 28);
            this.txtRuntext0.TabIndex = 37;
            this.txtRuntext0.Text = "Selamat Datang";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(10, 12);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(80, 13);
            this.label54.TabIndex = 36;
            this.label54.Text = "Running Text 0";
            // 
            // btnStep
            // 
            this.btnStep.Enabled = false;
            this.btnStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new System.Drawing.Point(294, 117);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(38, 16);
            this.btnStep.TabIndex = 8;
            this.btnStep.Text = "1,1,1";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.OnStep);
            // 
            // btnDoubleRate
            // 
            this.btnDoubleRate.Enabled = false;
            this.btnDoubleRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoubleRate.Location = new System.Drawing.Point(267, 117);
            this.btnDoubleRate.Name = "btnDoubleRate";
            this.btnDoubleRate.Size = new System.Drawing.Size(23, 16);
            this.btnDoubleRate.TabIndex = 7;
            this.btnDoubleRate.Text = "2x";
            this.btnDoubleRate.UseVisualStyleBackColor = true;
            this.btnDoubleRate.Click += new System.EventHandler(this.OnChangeRate);
            // 
            // btnHalfRate
            // 
            this.btnHalfRate.Enabled = false;
            this.btnHalfRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHalfRate.Location = new System.Drawing.Point(233, 117);
            this.btnHalfRate.Name = "btnHalfRate";
            this.btnHalfRate.Size = new System.Drawing.Size(28, 16);
            this.btnHalfRate.TabIndex = 6;
            this.btnHalfRate.Text = "1/2";
            this.btnHalfRate.UseVisualStyleBackColor = true;
            this.btnHalfRate.Click += new System.EventHandler(this.OnChangeRate);
            // 
            // btnResetRate
            // 
            this.btnResetRate.Enabled = false;
            this.btnResetRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetRate.Location = new System.Drawing.Point(233, 95);
            this.btnResetRate.Name = "btnResetRate";
            this.btnResetRate.Size = new System.Drawing.Size(31, 16);
            this.btnResetRate.TabIndex = 5;
            this.btnResetRate.Text = "0";
            this.btnResetRate.UseVisualStyleBackColor = true;
            this.btnResetRate.Click += new System.EventHandler(this.OnChangeRate);
            // 
            // btnDecreaseRate
            // 
            this.btnDecreaseRate.Enabled = false;
            this.btnDecreaseRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecreaseRate.Location = new System.Drawing.Point(267, 95);
            this.btnDecreaseRate.Name = "btnDecreaseRate";
            this.btnDecreaseRate.Size = new System.Drawing.Size(31, 16);
            this.btnDecreaseRate.TabIndex = 4;
            this.btnDecreaseRate.Text = "--";
            this.btnDecreaseRate.UseVisualStyleBackColor = true;
            this.btnDecreaseRate.Click += new System.EventHandler(this.OnChangeRate);
            // 
            // btnIncreaseRate
            // 
            this.btnIncreaseRate.Enabled = false;
            this.btnIncreaseRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncreaseRate.Location = new System.Drawing.Point(301, 95);
            this.btnIncreaseRate.Name = "btnIncreaseRate";
            this.btnIncreaseRate.Size = new System.Drawing.Size(31, 16);
            this.btnIncreaseRate.TabIndex = 3;
            this.btnIncreaseRate.Text = "++";
            this.btnIncreaseRate.UseVisualStyleBackColor = true;
            this.btnIncreaseRate.Click += new System.EventHandler(this.OnChangeRate);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(235, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.OnStopPlayer);
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(129, 69);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(100, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play/Pause";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.OnPlay);
            // 
            // gbDisplaySource
            // 
            this.gbDisplaySource.Controls.Add(this.radVideoDevice);
            this.gbDisplaySource.Controls.Add(this.radVideoClip);
            this.gbDisplaySource.Location = new System.Drawing.Point(12, 34);
            this.gbDisplaySource.Name = "gbDisplaySource";
            this.gbDisplaySource.Size = new System.Drawing.Size(111, 90);
            this.gbDisplaySource.TabIndex = 2;
            this.gbDisplaySource.TabStop = false;
            this.gbDisplaySource.Text = "Display Source";
            // 
            // radVideoDevice
            // 
            this.radVideoDevice.AutoSize = true;
            this.radVideoDevice.Location = new System.Drawing.Point(12, 61);
            this.radVideoDevice.Name = "radVideoDevice";
            this.radVideoDevice.Size = new System.Drawing.Size(89, 17);
            this.radVideoDevice.TabIndex = 1;
            this.radVideoDevice.Text = "Video Device";
            this.radVideoDevice.UseVisualStyleBackColor = true;
            this.radVideoDevice.CheckedChanged += new System.EventHandler(this.OnRbVideoInput);
            // 
            // radVideoClip
            // 
            this.radVideoClip.AutoSize = true;
            this.radVideoClip.Checked = true;
            this.radVideoClip.Location = new System.Drawing.Point(12, 35);
            this.radVideoClip.Name = "radVideoClip";
            this.radVideoClip.Size = new System.Drawing.Size(93, 17);
            this.radVideoClip.TabIndex = 0;
            this.radVideoClip.TabStop = true;
            this.radVideoClip.Text = "Video Clip/File";
            this.radVideoClip.UseVisualStyleBackColor = true;
            this.radVideoClip.CheckedChanged += new System.EventHandler(this.OnRbVideoClip);
            // 
            // btnOpenCloseVideo
            // 
            this.btnOpenCloseVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCloseVideo.Location = new System.Drawing.Point(129, 40);
            this.btnOpenCloseVideo.Name = "btnOpenCloseVideo";
            this.btnOpenCloseVideo.Size = new System.Drawing.Size(100, 23);
            this.btnOpenCloseVideo.TabIndex = 2;
            this.btnOpenCloseVideo.Text = "Open Video";
            this.btnOpenCloseVideo.UseVisualStyleBackColor = true;
            this.btnOpenCloseVideo.Click += new System.EventHandler(this.OnOpenCloseVideo);
            // 
            // volSlider
            // 
            this.volSlider.LargeChange = 1;
            this.volSlider.Location = new System.Drawing.Point(69, 141);
            this.volSlider.Maximum = 0;
            this.volSlider.Minimum = -10;
            this.volSlider.Name = "volSlider";
            this.volSlider.Size = new System.Drawing.Size(145, 45);
            this.volSlider.TabIndex = 10;
            this.volSlider.TickFrequency = 100;
            this.volSlider.Scroll += new System.EventHandler(this.OnVolume);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(524, 235);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save Settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.OnSaveSettings);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(21, 142);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(42, 13);
            this.label29.TabIndex = 12;
            this.label29.Text = "Volume";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(129, 110);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(85, 13);
            this.label30.TabIndex = 13;
            this.label30.Text = "Playback Speed";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShowDisplay);
            this.groupBox1.Controls.Add(this.btnSwitchDisplay);
            this.groupBox1.Location = new System.Drawing.Point(399, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 183);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display";
            // 
            // btnShowDisplay
            // 
            this.btnShowDisplay.BackColor = System.Drawing.Color.PaleGreen;
            this.btnShowDisplay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnShowDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowDisplay.Location = new System.Drawing.Point(18, 106);
            this.btnShowDisplay.Name = "btnShowDisplay";
            this.btnShowDisplay.Size = new System.Drawing.Size(202, 62);
            this.btnShowDisplay.TabIndex = 1;
            this.btnShowDisplay.Text = "Show/Hide  Display - F9";
            this.btnShowDisplay.UseVisualStyleBackColor = false;
            this.btnShowDisplay.Click += new System.EventHandler(this.OnShowDisplay);
            // 
            // btnSwitchDisplay
            // 
            this.btnSwitchDisplay.BackColor = System.Drawing.Color.LightGreen;
            this.btnSwitchDisplay.Enabled = false;
            this.btnSwitchDisplay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSwitchDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitchDisplay.Location = new System.Drawing.Point(18, 32);
            this.btnSwitchDisplay.Name = "btnSwitchDisplay";
            this.btnSwitchDisplay.Size = new System.Drawing.Size(202, 62);
            this.btnSwitchDisplay.TabIndex = 0;
            this.btnSwitchDisplay.Text = "Switch  Display  - F8";
            this.btnSwitchDisplay.UseVisualStyleBackColor = false;
            this.btnSwitchDisplay.Click += new System.EventHandler(this.OnSwitchDisplay);
            // 
            // chkMute
            // 
            this.chkMute.AutoSize = true;
            this.chkMute.Location = new System.Drawing.Point(235, 142);
            this.chkMute.Name = "chkMute";
            this.chkMute.Size = new System.Drawing.Size(84, 17);
            this.chkMute.TabIndex = 15;
            this.chkMute.Text = "Mute Sound";
            this.chkMute.UseVisualStyleBackColor = true;
            this.chkMute.CheckedChanged += new System.EventHandler(this.OnChkMute);
            // 
            // pnlMore
            // 
            this.pnlMore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMore.Controls.Add(this.tabControl);
            this.pnlMore.Controls.Add(this.btnSave);
            this.pnlMore.Location = new System.Drawing.Point(12, 223);
            this.pnlMore.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.pnlMore.MinimumSize = new System.Drawing.Size(625, 229);
            this.pnlMore.Name = "pnlMore";
            this.pnlMore.Size = new System.Drawing.Size(625, 263);
            this.pnlMore.TabIndex = 16;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(0, 486);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 2);
            this.lblStatus.Size = new System.Drawing.Size(135, 15);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Not Connected to Server";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(649, 501);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlMore);
            this.Controls.Add(this.chkMute);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnOpenCloseVideo);
            this.Controls.Add(this.btnResetRate);
            this.Controls.Add(this.btnDoubleRate);
            this.Controls.Add(this.btnHalfRate);
            this.Controls.Add(this.btnDecreaseRate);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.gbDisplaySource);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnIncreaseRate);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.volSlider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(665, 39);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tobasa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabVideoClip.ResumeLayout(false);
            this.tabVideoClip.PerformLayout();
            this.tabVideoDevice.ResumeLayout(false);
            this.gbVideo.ResumeLayout(false);
            this.gbVideo.PerformLayout();
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.tabMisc.ResumeLayout(false);
            this.tabMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.tabPostOpt.ResumeLayout(false);
            this.tabPostOpt.PerformLayout();
            this.tabPageRunText.ResumeLayout(false);
            this.tabPageRunText.PerformLayout();
            this.gbDisplaySource.ResumeLayout(false);
            this.gbDisplaySource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volSlider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.pnlMore.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        public System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuOption;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuPlayer;
        private System.Windows.Forms.ToolStripMenuItem mnuShowDisplay;
        private System.Windows.Forms.ToolStripMenuItem mnuFullScreen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabVideoClip;
        private System.Windows.Forms.TabPage tabVideoDevice;
        private System.Windows.Forms.GroupBox gbDisplaySource;
        private System.Windows.Forms.RadioButton radVideoDevice;
        private System.Windows.Forms.RadioButton radVideoClip;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnDoubleRate;
        private System.Windows.Forms.Button btnHalfRate;
        private System.Windows.Forms.Button btnResetRate;
        private System.Windows.Forms.Button btnDecreaseRate;
        private System.Windows.Forms.Button btnIncreaseRate;
        private System.Windows.Forms.Button btnOpenCloseVideo;
        private System.Windows.Forms.GroupBox gbVideo;
        private System.Windows.Forms.Button btnChangeVideoInput;
        private System.Windows.Forms.RadioButton radComposite;
        private System.Windows.Forms.RadioButton radSVideo;
        private System.Windows.Forms.RadioButton radTuner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbClipFolder;
        private System.Windows.Forms.RadioButton rbPlayFromFolder;
        private System.Windows.Forms.RadioButton rbOpenClip;
        private System.Windows.Forms.Button btnSetClipFolder;
        private System.Windows.Forms.Button btnInputProp;
        private System.Windows.Forms.Button btnCrossbarProp;
        private System.Windows.Forms.Button btnDeviceProp;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.ComboBox cbPost;
        private System.Windows.Forms.CheckBox chkConnectAtStart;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveOpt;
        private System.Windows.Forms.TextBox tbStation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabMisc;
        private System.Windows.Forms.Button btnSetAudioFolder;
        private System.Windows.Forms.TextBox tbAudioFolder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAnimationColor;
        private System.Windows.Forms.TrackBar volSlider;
        private System.Windows.Forms.CheckBox chkSetUnderscore;
        private System.Windows.Forms.CheckBox chkPlayVideoStartup;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkSpellNumber;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkShowLogo;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSwitchDisplay;
        private System.Windows.Forms.CheckBox chkMute;
        private System.Windows.Forms.Button btnShowDisplay;
        private System.Windows.Forms.Panel pnlMore;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox tbTextLogo;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button btnSetLogoImg;
        private System.Windows.Forms.TextBox tbImgLogo;
        private System.Windows.Forms.CheckBox chkShowInfoTextTop0;
        private System.Windows.Forms.CheckBox chkPlaySimpleNotification;
		private System.Windows.Forms.TabPage tabPageRunText;
		private System.Windows.Forms.TextBox txtRuntext1;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.TextBox txtRuntext0;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.CheckBox chkAudioLoketIDUseAlphabet;
		private System.Windows.Forms.TabPage tabPostOpt;
		private System.Windows.Forms.CheckBox chkPostVisible;
		private System.Windows.Forms.CheckBox chkPostPlayAudio;
		private System.Windows.Forms.TextBox tbPostRunText;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.TextBox tbPostCaption;
		private System.Windows.Forms.Label label58;
		private System.Windows.Forms.TextBox tbPostId;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.TextBox tbPostName;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.ComboBox cbSelectPost;
		private System.Windows.Forms.Label label91;
		private System.Windows.Forms.CheckBox chkShowInfoTextTop1;
		private System.Windows.Forms.CheckBox chkShowRightPosts;
		private System.Windows.Forms.CheckBox chkShowLeftPosts;
		private System.Windows.Forms.CheckBox chkShowCenterMiddleDiv;
	}
}

