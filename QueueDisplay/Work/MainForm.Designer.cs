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
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.chkPlaySimpleNotification = new System.Windows.Forms.CheckBox();
			this.chkShowInfoTextTop = new System.Windows.Forms.CheckBox();
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
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.post0PlayAudio = new System.Windows.Forms.CheckBox();
			this.post0RunText = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.post0Caption = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.post0Post = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.post0Name = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.post1PlayAudio = new System.Windows.Forms.CheckBox();
			this.post1RunText = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.post1Caption = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.post1Post = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.post1Name = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.post2PlayAudio = new System.Windows.Forms.CheckBox();
			this.post2RunText = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.post2Caption = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.post2Post = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.post2Name = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.post3PlayAudio = new System.Windows.Forms.CheckBox();
			this.post3Visible = new System.Windows.Forms.CheckBox();
			this.post3RunText = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.post3Caption = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.post3Post = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.post3Name = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.post4Visible = new System.Windows.Forms.CheckBox();
			this.post4PlayAudio = new System.Windows.Forms.CheckBox();
			this.post4RunText = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.post4Caption = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.post4Post = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.post4Name = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.txtRuntext1 = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.txtRuntext0 = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
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
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.tabPage5.SuspendLayout();
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
            this.toolsToolStripMenuItem,
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
			this.mnuOption.Size = new System.Drawing.Size(148, 22);
			this.mnuOption.Text = "Show &Options";
			this.mnuOption.Click += new System.EventHandler(this.OnOption);
			// 
			// mnuExit
			// 
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.Size = new System.Drawing.Size(148, 22);
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
			this.mnuPlayer.Text = "Display";
			// 
			// mnuShowDisplay
			// 
			this.mnuShowDisplay.Name = "mnuShowDisplay";
			this.mnuShowDisplay.Size = new System.Drawing.Size(144, 22);
			this.mnuShowDisplay.Text = "Show Display";
			this.mnuShowDisplay.Click += new System.EventHandler(this.OnShowDisplay);
			// 
			// mnuFullScreen
			// 
			this.mnuFullScreen.Name = "mnuFullScreen";
			this.mnuFullScreen.Size = new System.Drawing.Size(144, 22);
			this.mnuFullScreen.Text = "Full Screen";
			this.mnuFullScreen.Click += new System.EventHandler(this.OnFullScreen);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// mnuAbout
			// 
			this.mnuAbout.Name = "mnuAbout";
			this.mnuAbout.Size = new System.Drawing.Size(52, 20);
			this.mnuAbout.Text = "About";
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
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Controls.Add(this.tabPage4);
			this.tabControl.Controls.Add(this.tabPage6);
			this.tabControl.Controls.Add(this.tabPage5);
			this.tabControl.Location = new System.Drawing.Point(0, 5);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(625, 219);
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
			this.tabVideoClip.Size = new System.Drawing.Size(617, 193);
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
			this.tabVideoDevice.Size = new System.Drawing.Size(617, 193);
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
			this.tabOptions.Size = new System.Drawing.Size(617, 193);
			this.tabOptions.TabIndex = 2;
			this.tabOptions.Text = "Connection";
			this.tabOptions.UseVisualStyleBackColor = true;
			// 
			// cbPost
			// 
			this.cbPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPost.FormattingEnabled = true;
			this.cbPost.Items.AddRange(new object[] {
            "POST#0",
            "POST#1",
            "POST#2",
            "POST#3"});
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
			this.tabMisc.Controls.Add(this.chkPlaySimpleNotification);
			this.tabMisc.Controls.Add(this.chkShowInfoTextTop);
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
			this.tabMisc.Size = new System.Drawing.Size(617, 193);
			this.tabMisc.TabIndex = 3;
			this.tabMisc.Text = "Miscellaneous";
			this.tabMisc.UseVisualStyleBackColor = true;
			// 
			// chkPlaySimpleNotification
			// 
			this.chkPlaySimpleNotification.AutoSize = true;
			this.chkPlaySimpleNotification.Checked = true;
			this.chkPlaySimpleNotification.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPlaySimpleNotification.Location = new System.Drawing.Point(380, 80);
			this.chkPlaySimpleNotification.Name = "chkPlaySimpleNotification";
			this.chkPlaySimpleNotification.Size = new System.Drawing.Size(164, 17);
			this.chkPlaySimpleNotification.TabIndex = 33;
			this.chkPlaySimpleNotification.Text = "Play simple sound notification";
			this.chkPlaySimpleNotification.UseVisualStyleBackColor = true;
			this.chkPlaySimpleNotification.CheckedChanged += new System.EventHandler(this.OnPlaySimpleNotification);
			// 
			// chkShowInfoTextTop
			// 
			this.chkShowInfoTextTop.AutoSize = true;
			this.chkShowInfoTextTop.Location = new System.Drawing.Point(380, 34);
			this.chkShowInfoTextTop.Name = "chkShowInfoTextTop";
			this.chkShowInfoTextTop.Size = new System.Drawing.Size(146, 17);
			this.chkShowInfoTextTop.TabIndex = 32;
			this.chkShowInfoTextTop.Text = "Show top screen info text";
			this.chkShowInfoTextTop.UseVisualStyleBackColor = true;
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
			this.chkShowLogo.Location = new System.Drawing.Point(380, 13);
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
			this.chkSpellNumber.Location = new System.Drawing.Point(380, 103);
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
			this.label10.Location = new System.Drawing.Point(197, 162);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(42, 13);
			this.label10.TabIndex = 19;
			this.label10.Text = "second";
			// 
			// numericUpDown
			// 
			this.numericUpDown.Location = new System.Drawing.Point(147, 159);
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
			this.label9.Location = new System.Drawing.Point(11, 162);
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
			this.chkSetUnderscore.Location = new System.Drawing.Point(380, 158);
			this.chkSetUnderscore.Name = "chkSetUnderscore";
			this.chkSetUnderscore.Size = new System.Drawing.Size(205, 17);
			this.chkSetUnderscore.TabIndex = 16;
			this.chkSetUnderscore.Text = "Set number with underscore at startup";
			this.chkSetUnderscore.UseVisualStyleBackColor = true;
			// 
			// btnAnimationColor
			// 
			this.btnAnimationColor.Location = new System.Drawing.Point(147, 133);
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
			this.label8.Location = new System.Drawing.Point(11, 135);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(113, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "Queue animation color";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(11, 84);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(74, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Audio location";
			// 
			// btnSetAudioFolder
			// 
			this.btnSetAudioFolder.Location = new System.Drawing.Point(322, 79);
			this.btnSetAudioFolder.Name = "btnSetAudioFolder";
			this.btnSetAudioFolder.Size = new System.Drawing.Size(31, 23);
			this.btnSetAudioFolder.TabIndex = 11;
			this.btnSetAudioFolder.Text = "...";
			this.btnSetAudioFolder.UseVisualStyleBackColor = true;
			this.btnSetAudioFolder.Click += new System.EventHandler(this.OnSetAudioFolder);
			// 
			// tbAudioFolder
			// 
			this.tbAudioFolder.Location = new System.Drawing.Point(87, 81);
			this.tbAudioFolder.Name = "tbAudioFolder";
			this.tbAudioFolder.Size = new System.Drawing.Size(229, 20);
			this.tbAudioFolder.TabIndex = 9;
			this.tbAudioFolder.Text = "\r\n        ";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.post0PlayAudio);
			this.tabPage2.Controls.Add(this.post0RunText);
			this.tabPage2.Controls.Add(this.label14);
			this.tabPage2.Controls.Add(this.post0Caption);
			this.tabPage2.Controls.Add(this.label11);
			this.tabPage2.Controls.Add(this.post0Post);
			this.tabPage2.Controls.Add(this.label12);
			this.tabPage2.Controls.Add(this.post0Name);
			this.tabPage2.Controls.Add(this.label13);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(617, 193);
			this.tabPage2.TabIndex = 5;
			this.tabPage2.Text = "Post # 0";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// post0PlayAudio
			// 
			this.post0PlayAudio.AutoSize = true;
			this.post0PlayAudio.Location = new System.Drawing.Point(102, 117);
			this.post0PlayAudio.Name = "post0PlayAudio";
			this.post0PlayAudio.Size = new System.Drawing.Size(75, 17);
			this.post0PlayAudio.TabIndex = 51;
			this.post0PlayAudio.Text = "Play audio";
			this.post0PlayAudio.UseVisualStyleBackColor = true;
			// 
			// post0RunText
			// 
			this.post0RunText.Location = new System.Drawing.Point(102, 89);
			this.post0RunText.Name = "post0RunText";
			this.post0RunText.Size = new System.Drawing.Size(216, 20);
			this.post0RunText.TabIndex = 49;
			this.post0RunText.Text = "Post 0 Info";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(9, 92);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(49, 13);
			this.label14.TabIndex = 48;
			this.label14.Text = "Info Text";
			// 
			// post0Caption
			// 
			this.post0Caption.Location = new System.Drawing.Point(102, 64);
			this.post0Caption.Name = "post0Caption";
			this.post0Caption.Size = new System.Drawing.Size(216, 20);
			this.post0Caption.TabIndex = 47;
			this.post0Caption.Text = "Post 0";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(9, 67);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(43, 13);
			this.label11.TabIndex = 46;
			this.label11.Text = "Caption";
			// 
			// post0Post
			// 
			this.post0Post.Location = new System.Drawing.Point(102, 40);
			this.post0Post.Name = "post0Post";
			this.post0Post.Size = new System.Drawing.Size(107, 20);
			this.post0Post.TabIndex = 44;
			this.post0Post.Text = "POST0";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(9, 43);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(16, 13);
			this.label12.TabIndex = 43;
			this.label12.Text = "Id";
			// 
			// post0Name
			// 
			this.post0Name.Location = new System.Drawing.Point(102, 17);
			this.post0Name.Name = "post0Name";
			this.post0Name.Size = new System.Drawing.Size(107, 20);
			this.post0Name.TabIndex = 42;
			this.post0Name.Text = "Post 0";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(9, 20);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(35, 13);
			this.label13.TabIndex = 41;
			this.label13.Text = "Name";
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.post1PlayAudio);
			this.tabPage1.Controls.Add(this.post1RunText);
			this.tabPage1.Controls.Add(this.label15);
			this.tabPage1.Controls.Add(this.post1Caption);
			this.tabPage1.Controls.Add(this.label16);
			this.tabPage1.Controls.Add(this.post1Post);
			this.tabPage1.Controls.Add(this.label17);
			this.tabPage1.Controls.Add(this.post1Name);
			this.tabPage1.Controls.Add(this.label18);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(617, 193);
			this.tabPage1.TabIndex = 6;
			this.tabPage1.Text = "Post # 1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// post1PlayAudio
			// 
			this.post1PlayAudio.AutoSize = true;
			this.post1PlayAudio.Location = new System.Drawing.Point(102, 117);
			this.post1PlayAudio.Name = "post1PlayAudio";
			this.post1PlayAudio.Size = new System.Drawing.Size(75, 17);
			this.post1PlayAudio.TabIndex = 52;
			this.post1PlayAudio.Text = "Play audio";
			this.post1PlayAudio.UseVisualStyleBackColor = true;
			// 
			// post1RunText
			// 
			this.post1RunText.Location = new System.Drawing.Point(102, 89);
			this.post1RunText.Name = "post1RunText";
			this.post1RunText.Size = new System.Drawing.Size(216, 20);
			this.post1RunText.TabIndex = 49;
			this.post1RunText.Text = "Post 1 Info";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(9, 92);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(49, 13);
			this.label15.TabIndex = 48;
			this.label15.Text = "Info Text";
			// 
			// post1Caption
			// 
			this.post1Caption.Location = new System.Drawing.Point(102, 64);
			this.post1Caption.Name = "post1Caption";
			this.post1Caption.Size = new System.Drawing.Size(216, 20);
			this.post1Caption.TabIndex = 47;
			this.post1Caption.Text = "Post 1";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(9, 67);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(43, 13);
			this.label16.TabIndex = 46;
			this.label16.Text = "Caption";
			// 
			// post1Post
			// 
			this.post1Post.Location = new System.Drawing.Point(102, 40);
			this.post1Post.Name = "post1Post";
			this.post1Post.Size = new System.Drawing.Size(107, 20);
			this.post1Post.TabIndex = 44;
			this.post1Post.Text = "POST1";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(9, 43);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(16, 13);
			this.label17.TabIndex = 43;
			this.label17.Text = "Id";
			// 
			// post1Name
			// 
			this.post1Name.Location = new System.Drawing.Point(102, 17);
			this.post1Name.Name = "post1Name";
			this.post1Name.Size = new System.Drawing.Size(107, 20);
			this.post1Name.TabIndex = 42;
			this.post1Name.Text = "Post 1";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(9, 20);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(35, 13);
			this.label18.TabIndex = 41;
			this.label18.Text = "Name";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.post2PlayAudio);
			this.tabPage3.Controls.Add(this.post2RunText);
			this.tabPage3.Controls.Add(this.label19);
			this.tabPage3.Controls.Add(this.post2Caption);
			this.tabPage3.Controls.Add(this.label20);
			this.tabPage3.Controls.Add(this.post2Post);
			this.tabPage3.Controls.Add(this.label21);
			this.tabPage3.Controls.Add(this.post2Name);
			this.tabPage3.Controls.Add(this.label22);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(617, 193);
			this.tabPage3.TabIndex = 7;
			this.tabPage3.Text = "Post # 2";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// post2PlayAudio
			// 
			this.post2PlayAudio.AutoSize = true;
			this.post2PlayAudio.Location = new System.Drawing.Point(102, 117);
			this.post2PlayAudio.Name = "post2PlayAudio";
			this.post2PlayAudio.Size = new System.Drawing.Size(75, 17);
			this.post2PlayAudio.TabIndex = 52;
			this.post2PlayAudio.Text = "Play audio";
			this.post2PlayAudio.UseVisualStyleBackColor = true;
			// 
			// post2RunText
			// 
			this.post2RunText.Location = new System.Drawing.Point(102, 89);
			this.post2RunText.Name = "post2RunText";
			this.post2RunText.Size = new System.Drawing.Size(216, 20);
			this.post2RunText.TabIndex = 49;
			this.post2RunText.Text = "Post 2 Info";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(9, 92);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(49, 13);
			this.label19.TabIndex = 48;
			this.label19.Text = "Info Text";
			// 
			// post2Caption
			// 
			this.post2Caption.Location = new System.Drawing.Point(102, 64);
			this.post2Caption.Name = "post2Caption";
			this.post2Caption.Size = new System.Drawing.Size(216, 20);
			this.post2Caption.TabIndex = 47;
			this.post2Caption.Text = "Post 2";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(9, 67);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(43, 13);
			this.label20.TabIndex = 46;
			this.label20.Text = "Caption";
			// 
			// post2Post
			// 
			this.post2Post.Location = new System.Drawing.Point(102, 40);
			this.post2Post.Name = "post2Post";
			this.post2Post.Size = new System.Drawing.Size(107, 20);
			this.post2Post.TabIndex = 44;
			this.post2Post.Text = "POST2";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(9, 43);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(16, 13);
			this.label21.TabIndex = 43;
			this.label21.Text = "Id";
			// 
			// post2Name
			// 
			this.post2Name.Location = new System.Drawing.Point(102, 17);
			this.post2Name.Name = "post2Name";
			this.post2Name.Size = new System.Drawing.Size(107, 20);
			this.post2Name.TabIndex = 42;
			this.post2Name.Text = "Post 2";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(9, 20);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(35, 13);
			this.label22.TabIndex = 41;
			this.label22.Text = "Name";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.post3PlayAudio);
			this.tabPage4.Controls.Add(this.post3Visible);
			this.tabPage4.Controls.Add(this.post3RunText);
			this.tabPage4.Controls.Add(this.label23);
			this.tabPage4.Controls.Add(this.post3Caption);
			this.tabPage4.Controls.Add(this.label24);
			this.tabPage4.Controls.Add(this.post3Post);
			this.tabPage4.Controls.Add(this.label25);
			this.tabPage4.Controls.Add(this.post3Name);
			this.tabPage4.Controls.Add(this.label26);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(617, 193);
			this.tabPage4.TabIndex = 8;
			this.tabPage4.Text = "Post # 3";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// post3PlayAudio
			// 
			this.post3PlayAudio.AutoSize = true;
			this.post3PlayAudio.Location = new System.Drawing.Point(102, 117);
			this.post3PlayAudio.Name = "post3PlayAudio";
			this.post3PlayAudio.Size = new System.Drawing.Size(75, 17);
			this.post3PlayAudio.TabIndex = 52;
			this.post3PlayAudio.Text = "Play audio";
			this.post3PlayAudio.UseVisualStyleBackColor = true;
			// 
			// post3Visible
			// 
			this.post3Visible.AutoSize = true;
			this.post3Visible.Location = new System.Drawing.Point(197, 117);
			this.post3Visible.Name = "post3Visible";
			this.post3Visible.Size = new System.Drawing.Size(56, 17);
			this.post3Visible.TabIndex = 50;
			this.post3Visible.Text = "Visible";
			this.post3Visible.UseVisualStyleBackColor = true;
			// 
			// post3RunText
			// 
			this.post3RunText.Location = new System.Drawing.Point(102, 89);
			this.post3RunText.Name = "post3RunText";
			this.post3RunText.Size = new System.Drawing.Size(216, 20);
			this.post3RunText.TabIndex = 49;
			this.post3RunText.Text = "Post 3 Info";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(9, 92);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(49, 13);
			this.label23.TabIndex = 48;
			this.label23.Text = "Info Text";
			// 
			// post3Caption
			// 
			this.post3Caption.Location = new System.Drawing.Point(102, 64);
			this.post3Caption.Name = "post3Caption";
			this.post3Caption.Size = new System.Drawing.Size(216, 20);
			this.post3Caption.TabIndex = 47;
			this.post3Caption.Text = "Post 3";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(9, 67);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(43, 13);
			this.label24.TabIndex = 46;
			this.label24.Text = "Caption";
			// 
			// post3Post
			// 
			this.post3Post.Location = new System.Drawing.Point(102, 40);
			this.post3Post.Name = "post3Post";
			this.post3Post.Size = new System.Drawing.Size(107, 20);
			this.post3Post.TabIndex = 44;
			this.post3Post.Text = "POST3";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(9, 43);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(16, 13);
			this.label25.TabIndex = 43;
			this.label25.Text = "Id";
			// 
			// post3Name
			// 
			this.post3Name.Location = new System.Drawing.Point(102, 17);
			this.post3Name.Name = "post3Name";
			this.post3Name.Size = new System.Drawing.Size(107, 20);
			this.post3Name.TabIndex = 42;
			this.post3Name.Text = "Post 3";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(9, 20);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(35, 13);
			this.label26.TabIndex = 41;
			this.label26.Text = "Name";
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.post4Visible);
			this.tabPage6.Controls.Add(this.post4PlayAudio);
			this.tabPage6.Controls.Add(this.post4RunText);
			this.tabPage6.Controls.Add(this.label33);
			this.tabPage6.Controls.Add(this.post4Caption);
			this.tabPage6.Controls.Add(this.label34);
			this.tabPage6.Controls.Add(this.post4Post);
			this.tabPage6.Controls.Add(this.label35);
			this.tabPage6.Controls.Add(this.post4Name);
			this.tabPage6.Controls.Add(this.label36);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(617, 193);
			this.tabPage6.TabIndex = 10;
			this.tabPage6.Text = "Post # 4";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// post4Visible
			// 
			this.post4Visible.AutoSize = true;
			this.post4Visible.Location = new System.Drawing.Point(197, 117);
			this.post4Visible.Name = "post4Visible";
			this.post4Visible.Size = new System.Drawing.Size(56, 17);
			this.post4Visible.TabIndex = 53;
			this.post4Visible.Text = "Visible";
			this.post4Visible.UseVisualStyleBackColor = true;
			// 
			// post4PlayAudio
			// 
			this.post4PlayAudio.AutoSize = true;
			this.post4PlayAudio.Location = new System.Drawing.Point(102, 117);
			this.post4PlayAudio.Name = "post4PlayAudio";
			this.post4PlayAudio.Size = new System.Drawing.Size(75, 17);
			this.post4PlayAudio.TabIndex = 52;
			this.post4PlayAudio.Text = "Play audio";
			this.post4PlayAudio.UseVisualStyleBackColor = true;
			// 
			// post4RunText
			// 
			this.post4RunText.Location = new System.Drawing.Point(102, 89);
			this.post4RunText.Name = "post4RunText";
			this.post4RunText.Size = new System.Drawing.Size(216, 20);
			this.post4RunText.TabIndex = 49;
			this.post4RunText.Text = "Post 4 Info";
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(9, 92);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(49, 13);
			this.label33.TabIndex = 48;
			this.label33.Text = "Info Text";
			// 
			// post4Caption
			// 
			this.post4Caption.Location = new System.Drawing.Point(102, 64);
			this.post4Caption.Name = "post4Caption";
			this.post4Caption.Size = new System.Drawing.Size(216, 20);
			this.post4Caption.TabIndex = 47;
			this.post4Caption.Text = "Post 4";
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(9, 67);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(43, 13);
			this.label34.TabIndex = 46;
			this.label34.Text = "Caption";
			// 
			// post4Post
			// 
			this.post4Post.Location = new System.Drawing.Point(102, 40);
			this.post4Post.Name = "post4Post";
			this.post4Post.Size = new System.Drawing.Size(107, 20);
			this.post4Post.TabIndex = 44;
			this.post4Post.Text = "POST4";
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(9, 43);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(16, 13);
			this.label35.TabIndex = 43;
			this.label35.Text = "Id";
			// 
			// post4Name
			// 
			this.post4Name.Location = new System.Drawing.Point(102, 17);
			this.post4Name.Name = "post4Name";
			this.post4Name.Size = new System.Drawing.Size(107, 20);
			this.post4Name.TabIndex = 42;
			this.post4Name.Text = "Post 4";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(9, 20);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(35, 13);
			this.label36.TabIndex = 41;
			this.label36.Text = "Name";
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.txtRuntext1);
			this.tabPage5.Controls.Add(this.label27);
			this.tabPage5.Controls.Add(this.txtRuntext0);
			this.tabPage5.Controls.Add(this.label28);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(617, 193);
			this.tabPage5.TabIndex = 9;
			this.tabPage5.Text = "Running Text";
			this.tabPage5.UseVisualStyleBackColor = true;
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
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(10, 46);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(80, 13);
			this.label27.TabIndex = 38;
			this.label27.Text = "Running Text 1";
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
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(10, 12);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(80, 13);
			this.label28.TabIndex = 36;
			this.label28.Text = "Running Text 0";
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
			this.btnSave.Location = new System.Drawing.Point(531, 230);
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
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox post0Caption;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox post0Post;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox post0Name;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox post0RunText;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox post1RunText;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox post1Caption;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox post1Post;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox post1Name;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox post2RunText;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox post2Caption;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox post2Post;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox post2Name;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox post3RunText;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox post3Caption;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox post3Post;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox post3Name;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox post3Visible;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtRuntext1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtRuntext0;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox post0PlayAudio;
        private System.Windows.Forms.CheckBox post1PlayAudio;
        private System.Windows.Forms.CheckBox post2PlayAudio;
        private System.Windows.Forms.CheckBox post3PlayAudio;
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
        private System.Windows.Forms.CheckBox chkShowInfoTextTop;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.CheckBox post4PlayAudio;
        private System.Windows.Forms.TextBox post4RunText;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox post4Caption;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox post4Post;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox post4Name;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.CheckBox post4Visible;
        private System.Windows.Forms.CheckBox chkPlaySimpleNotification;
    }
}

