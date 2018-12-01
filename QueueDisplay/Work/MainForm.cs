using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using DirectShowLib;

namespace Tobasa
{
    public partial class MainForm : Form
    {
        #region Member variables

        private delegate void ProcessMessageCallback(DataReceivedEventArgs arg, string text);
        private delegate void ProcessErrorCallBack(NotifyEventArgs e);
        
        private TCPClient client = null;

        #endregion

        #region TCP Client stuffs

        void TCPClient_Notified(NotifyEventArgs e)
        {
            ProcessError(e);
        }

        public void ProcessError(NotifyEventArgs e)
        {
            if (this.InvokeRequired)
            {
                ProcessErrorCallBack d = new ProcessErrorCallBack(ProcessError);
                this.Invoke(d, new object[] { e });
            }
            else
                MessageBox.Show(this, e.Message, e.Summary, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CloseConnection()
        {
            if (client.Connected)
                client.Stop();
        }

        public void StartClient()
        {
            string dispServerHost = Properties.Settings.Default.QueueServerHost;
            int dispServerPort = Properties.Settings.Default.QueueServerPort;
            string stationName = Properties.Settings.Default.StationName;
            string stationPost = Properties.Settings.Default.StationPost;
            string userName = Properties.Settings.Default.QueueUserName;
            string password = Properties.Settings.Default.QueuePassword;

            client = new TCPClient(dispServerHost, dispServerPort);
            client.Notified += new Action<NotifyEventArgs>(TCPClient_Notified);

            client.Start();

            if (client.Connected)
            {
                client.Session.DataReceived += new DataReceived(NetSession_DataReceived);

                string salt = Properties.Settings.Default.SecuritySalt;
                string clearPwd = Util.DecryptPassword(password, salt);
                string passwordHash = Util.GetPasswordHash(clearPwd, userName);

                // Send LOGIN message + our station name to server
                string message = String.Empty;
                message = "LOGIN" + Msg.Separator + "DISPLAY"+ Msg.Separator + stationName + Msg.Separator + stationPost + Msg.Separator + userName + Msg.Separator + passwordHash;
                client.Send(message);
            }
        }

        #endregion

        #region QueueServer message handlers

        private void NetSession_DataReceived(DataReceivedEventArgs arg)
        {
            string text = "";
            /*
            // Deserialize the message
            object message = Message.Deserialize(arg.Data);

            // Handle the message
            StringMessage stringMessage = message as StringMessage;
            if (stringMessage != null)
                text = stringMessage.Message;
            */
            string stringMessage = Encoding.UTF8.GetString(arg.Data);
            if (stringMessage != null)
            {
                text = stringMessage;

                if (text.StartsWith("DISPLAY") || text.StartsWith("LOGIN"))
                    ProcessMessage(arg, text);
                else
                {
                    string logmsg = String.Format("Unhandled session message from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                }
            }
        }

        /// cross thread safe handler
        public void ProcessMessage(DataReceivedEventArgs arg, string text)
        {
            /// InvokeRequired required compares the thread ID of the 
            /// calling thread to the thread ID of the creating thread. 
            /// If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                ProcessMessageCallback d = new ProcessMessageCallback(ProcessMessage);
                this.Invoke(d, new object[] { arg, text });
            }
            else
            {
                if (text.StartsWith("DISPLAY"))
                    HandleMessage(arg, text);
                else if (text.StartsWith("LOGIN"))
                {
                    string _response = text;
                    if (_response == Msg.LOGIN_OK)
                    {
                        lblStatus.Text = "Connected to Server : " + client.Session.RemoteInfo + " - Post :" + Properties.Settings.Default.StationPost + "  Station:" + Properties.Settings.Default.StationName;
                        Logger.Log("QueueDisplay : Successfully logged in");

                        // Request running text from server
                        client.Send("DISPLAY" + Msg.Separator + "GET_RUNNINGTEXT");
                    }
                    else
                    {
                        string reason = _response.Substring(10);
                        string msg = "QueueDisplay : Could not logged in to server, \r\nReason: " + reason;
                        Logger.Log(msg);
                        MessageBox.Show(this, msg, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CloseConnection();
                    }
                }
            }
        }

        public void HandleMessage(DataReceivedEventArgs arg, string text)
        {
            if (text.StartsWith(Msg.DISPLAY_RESET_VALUES))
            {
                mDisplay.ResetDisplayNumbers();
            }
            else if (text.StartsWith(Msg.DISPLAY_SET_RUNNINGTEXT))
            {
                string _station, _post, _rtext;
                _station = _post = _rtext = "";

                string[] words = text.Split(Msg.Separator.ToCharArray());

                if (words.Length == 5)
                {
                    _station = words[2];
                    _post = words[3];
                    _rtext = words[4];
                    
                    mDisplay.AddRunningText(_rtext);
                }
                else
                {
                    string logmsg = String.Format("Invalid DISPLAY:SET_RUNNINGTEXT from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
            else if (text.StartsWith(Msg.DISPLAY_RESET_RUNNINGTEXT))
            {
                mDisplay.ResetRunningText();
            }
            else if (text.StartsWith(Msg.DISPLAY_DELETE_RUNNINGTEXT))
            {
                string _station, _post, _rtext;
                _station = _post = _rtext = "";

                string[] words = text.Split(Msg.Separator.ToCharArray());

                if (words.Length == 5)
                {
                    _station = words[2];
                    _post = words[3];
                    _rtext = words[4];
                    
                    mDisplay.DeleteRunningText(_rtext);
                }
                else
                {
                    string logmsg = String.Format("Invalid DISPLAY:DELETE_RUNNINGTEXT from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
            else if (text.StartsWith(Msg.DISPLAY_CALL_NUMBER) || text.StartsWith(Msg.DISPLAY_RECALL_NUMBER))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());
                if (words.Length == 7)
                    mDisplay.ProcessDisplayCallNumber(text);
                else
                {
                    string logmsg = "";
                    if (text.StartsWith(Msg.DISPLAY_CALL_NUMBER))
                        logmsg = String.Format("Invalid DISPLAY:CALL_NUMBER from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    else if (text.StartsWith(Msg.DISPLAY_RECALL_NUMBER))
                        logmsg = String.Format("Invalid DISPLAY:RECALL_NUMBER from: {0} - MSG: {1} ", arg.RemoteInfo, text);

                    Logger.Log(logmsg);
                    return;
                }
            }
            else if ( text.StartsWith(Msg.DISPLAY_SHOW_MESSAGE))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());
                if (words.Length == 5)
                    mDisplay.ProcessDisplayShowMessage(text);
                else
                {
                    string logmsg = String.Format("Invalid DISPLAY:SHOW_MESSAGE from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
            else if (text.StartsWith(Msg.DISPLAY_UPDATE_JOB))
            {
                string _station, _post, _numbers;
                _station = _post = _numbers = "";

                string[] words = text.Split(Msg.Separator.ToCharArray());

                if (words.Length == 5)
                {
                    _station = words[2];
                    _post = words[3];
                    _numbers = words[4];
                    
                    mDisplay.ProcessDisplayUpdateFinishedJob(_numbers);
                }
                else
                {
                    string logmsg = String.Format("Invalid DISPLAY:UPDATE_JOB from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
        }

        #endregion

        #region DirectShow stuffs

        int lastVolume = 0; // max volume = 0, min volume= -10000
        bool startupComplete = false;
        private Display mDisplay;

        public class DeviceName
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }

        private void DSEngine_Notified(NotifyEventArgs e)
        {
            MessageBox.Show(this, e.Message, e.Summary, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        // set up correct volume level
        private void DSEngine_PlayerStarted(EventArgs e)
        {
            mDisplay.DSEngine.SetVolume(Properties.Settings.Default.DSEngineVolumeLevel);
        }

        public string GetClipFileName()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.avi; *.qt; *.mov; *.mpg; *.mpeg; *.m1v)|*.avi; *.qt; *.mov; *.mpg; *.mpeg; *.m1v|Audio files (*.wav; *.mpa; *.mp2; *.mp3; *.au; *.aif; *.aiff; *.snd)|*.wav; *.mpa; *.mp2; *.mp3; *.au; *.aif; *.aiff; *.snd|MIDI Files (*.mid, *.midi, *.rmi)|*.mid; *.midi; *.rmi|Image Files (*.jpg, *.bmp, *.gif, *.tga)|*.jpg; *.bmp; *.gif; *.tga|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
                return String.Empty;
        }

        private void InitDeviceList()
        {
            DsDevice[] vidInputDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            Array.Reverse(vidInputDevices);

            var dataSource = new List<DeviceName>();

            for (int i = 0; i < vidInputDevices.Length; i++)
            {
                DsDevice dev = vidInputDevices[i];
                string devicePath = dev.DevicePath;
                string deviceName = dev.Name;
                dataSource.Add(new DeviceName() { Name = deviceName, Path = devicePath });
            }

            //Setup combobox data binding
            this.cbDevice.DataSource = dataSource;
            this.cbDevice.DisplayMember = "Name";
            this.cbDevice.ValueMember = "Path";

            // make it readonly
            this.cbDevice.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CloseVideo()
        {
            mDisplay.DSEngine.CloseClip();
            this.EnableRateButtons(false);
            this.EnablePlayStopMuteButtons(false);
        }

        private void OpenClip(string videoFolder)
        {
            if (mDisplay.DSEngine.CurrentPlayState != PlayState.Init)
                mDisplay.DSEngine.CloseClip();

            // No clip folder given.. let user take the file
            if (videoFolder == String.Empty)
            {
                string fileName = GetClipFileName();
                mDisplay.DSEngine.StartDisplay(mDisplay.pnlVideo, mDisplay.Handle, DisplaySource.Clip, fileName);
            }
            else
            {
                mDisplay.DSEngine.UsePlayList = true;
                mDisplay.DSEngine.InitPlayList(videoFolder);
                mDisplay.DSEngine.StartFromPlayList();
            }

            if (mDisplay.DSEngine.CurrentPlayState == PlayState.Running)
            {
                this.EnableRateButtons(true);
                this.EnablePlayStopMuteButtons(true);
                btnOpenCloseVideo.Text = "Close Video";
            }
        }

        private void OpenVideoDevice()
        {
            if (!mDisplay.DSEngine.HasVideoInputDevices())
            {
                MessageBox.Show("No video input device found", "Information", MessageBoxButtons.OK);
                return;
            }

            if (mDisplay.DSEngine.CurrentPlayState != PlayState.Init)
                mDisplay.DSEngine.CloseClip();

            mDisplay.DSEngine.StartDisplay(mDisplay.pnlVideo, mDisplay.Handle, DisplaySource.Stream, "");

            if (mDisplay.DSEngine.CurrentPlayState == PlayState.Running)
            {
                this.btnChangeVideoInput.Enabled = mDisplay.DSEngine.UsingCrossbar;
                this.radTuner.Enabled = mDisplay.DSEngine.UsingCrossbar;
                this.radSVideo.Enabled = mDisplay.DSEngine.UsingCrossbar;
                this.radComposite.Enabled = mDisplay.DSEngine.UsingCrossbar;

                this.tabVideoClip.Enabled = false;
                this.EnablePlayStopMuteButtons(true);

                btnOpenCloseVideo.Text = "Close Video";
            }
        }

        #endregion

        #region Constructor/Destructor

        public MainForm()
        {
            InitializeComponent();
            /// we want to receive key event
            KeyPreview = true;
            this.WindowState = FormWindowState.Minimized;
            
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            pnlMore.Visible = false;

            InitDeviceList();

            mDisplay = new Display();
            mDisplay.DSEngine.Notified += new Action<NotifyEventArgs>(DSEngine_Notified);
            mDisplay.DSEngine.PlayerStarted += new Action<EventArgs>(DSEngine_PlayerStarted);

            /// Populate cbPost
            cbPost.Items.Clear();
            string[] cbPostItems = new string[Properties.Settings.Default.UIPostList.Count];
            Properties.Settings.Default.UIPostList.CopyTo(cbPostItems, 0);
            cbPost.Items.AddRange(cbPostItems);
            cbPost.Text = Properties.Settings.Default.StationPost;

            /// Check Screen Count
            Screen[] screens = Screen.AllScreens;
            if (screens.Length > 1)
                btnSwitchDisplay.Enabled = true;

            RestoreSettings();

            if (Properties.Settings.Default.PlayVideoAtStartup)
                OnOpenCloseVideo(this, EventArgs.Empty);

            /// start tcp client
            StartClient();

            //mDisplay.TopMost = true;
            mDisplay.Focus();
            mDisplay.Show();
            startupComplete = true;
        }

        #endregion

        #region Application settings stuffs
        private void RestoreSettings()
        {
            // Restore last used video device
            if (Properties.Settings.Default.VideoInputDevicePath != String.Empty)
                cbDevice.SelectedValue = Properties.Settings.Default.VideoInputDevicePath;
            
            // Restore last used video input device source
            if (Properties.Settings.Default.VideoSource == "VideoInputDevice")
                radVideoDevice.Checked = true;
            else if (Properties.Settings.Default.VideoSource == "VideoCLip")
                radVideoClip.Checked = true;

            if (Properties.Settings.Default.VideoCLipMode == "PlayFromFolder")
                rbPlayFromFolder.Checked = true;
            else if (Properties.Settings.Default.VideoCLipMode == "PlayBySelectManually")
                rbOpenClip.Checked = true;

            tbClipFolder.Text = Properties.Settings.Default.VideoClipLocation;

            tbServer.Text = Properties.Settings.Default.QueueServerHost;
            tbPort.Text = Properties.Settings.Default.QueueServerPort.ToString();
            tbStation.Text = Properties.Settings.Default.StationName;
            cbPost.Text = Properties.Settings.Default.StationPost;

            tbClipFolder.Text = Properties.Settings.Default.VideoClipLocation;
            tbAudioFolder.Text = Properties.Settings.Default.NumberAudioLocation;
            chkSpellNumber.Checked = Properties.Settings.Default.AudioSpellNumber;
            chkPlaySimpleNotification.Checked = Properties.Settings.Default.PlaySimpleSoundNotification;
            chkShowLogo.Checked = Properties.Settings.Default.ShowLogo;
            chkShowInfoTextTop.Checked = Properties.Settings.Default.ShowInfoTextTop1; 

            btnAnimationColor.BackColor = Properties.Settings.Default.NumberAnimationColor;
            numericUpDown.Value = (decimal)Properties.Settings.Default.QueueAnimationTimeInSecond;
            
            // DSEngine volume related settings
            volSlider.Value = Properties.Settings.Default.DSEngineVolumeLevel / 1000;
            mDisplay.DSEngine.SetVolume(Properties.Settings.Default.DSEngineVolumeLevel);
            lastVolume = Properties.Settings.Default.DSEngineVolumeLevel;
            chkMute.Checked = (lastVolume == -10000);
            volSlider.Enabled = (lastVolume > -10000);

            chkSetUnderscore.Checked = Properties.Settings.Default.StartNumberWithUnderscore;
            chkPlayVideoStartup.Checked = Properties.Settings.Default.PlayVideoAtStartup;

            // Check sound folder
            string _soundDir = Properties.Settings.Default.NumberAudioLocation;
            if (!Directory.Exists(_soundDir))
            {
                _soundDir = Util.ProcessDir + "\\wav";
                Properties.Settings.Default.NumberAudioLocation = _soundDir;
            }
            
            if (Directory.Exists(_soundDir))
                tbAudioFolder.Text = _soundDir;

            post0Name.Text = Properties.Settings.Default.Post0Name;
            post0Post.Text = Properties.Settings.Default.Post0Post;
            post0Caption.Text = Properties.Settings.Default.Post0Caption;
            post0RunText.Text = Properties.Settings.Default.Post0RunText;
            post0PlayAudio.Checked = Properties.Settings.Default.Post0PlayAudio;
            if (Properties.Settings.Default.Post0Post == Properties.Settings.Default.StationPost)
                post0PlayAudio.Checked = true;

            post1Name.Text = Properties.Settings.Default.Post1Name;
            post1Post.Text = Properties.Settings.Default.Post1Post;
            post1Caption.Text = Properties.Settings.Default.Post1Caption;
            post1RunText.Text = Properties.Settings.Default.Post1RunText;
            post1PlayAudio.Checked = Properties.Settings.Default.Post1PlayAudio;
            if (Properties.Settings.Default.Post1Post == Properties.Settings.Default.StationPost)
                post1PlayAudio.Checked = true;

            post2Name.Text = Properties.Settings.Default.Post2Name;
            post2Post.Text = Properties.Settings.Default.Post2Post;
            post2Caption.Text = Properties.Settings.Default.Post2Caption;
            post2RunText.Text = Properties.Settings.Default.Post2RunText;
            post2PlayAudio.Checked = Properties.Settings.Default.Post2PlayAudio;
            if (Properties.Settings.Default.Post2Post == Properties.Settings.Default.StationPost)
                post2PlayAudio.Checked = true;

            post3Name.Text = Properties.Settings.Default.Post3Name;
            post3Post.Text = Properties.Settings.Default.Post3Post;
            post3Caption.Text = Properties.Settings.Default.Post3Caption;
            post3RunText.Text = Properties.Settings.Default.Post3RunText;
            post3PlayAudio.Checked = Properties.Settings.Default.Post3PlayAudio;
            post3Visible.Checked = Properties.Settings.Default.Post3Visible;
            if (Properties.Settings.Default.Post3Post == Properties.Settings.Default.StationPost)
                post3PlayAudio.Checked = true;

            post4Name.Text = Properties.Settings.Default.Post4Name;
            post4Post.Text = Properties.Settings.Default.Post4Post;
            post4Caption.Text = Properties.Settings.Default.Post4Caption;
            post4RunText.Text = Properties.Settings.Default.Post4RunText;
            post4PlayAudio.Checked = Properties.Settings.Default.Post4PlayAudio;
            post4Visible.Checked = Properties.Settings.Default.Post4Visible;
            if (Properties.Settings.Default.Post4Post == Properties.Settings.Default.StationPost)
                post4PlayAudio.Checked = true;

            txtRuntext0.Text = Properties.Settings.Default.RunningText0;
            txtRuntext1.Text = Properties.Settings.Default.RunningText1;

            tbImgLogo.Text = Properties.Settings.Default.DisplayLogoImg;
            tbTextLogo.Text = Properties.Settings.Default.DisplayLogoText;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.QueueAnimationTimeInSecond = (int)numericUpDown.Value;
            Properties.Settings.Default.PlayVideoAtStartup = chkPlayVideoStartup.Checked;
            Properties.Settings.Default.StartNumberWithUnderscore = chkSetUnderscore.Checked;
            Properties.Settings.Default.NumberAudioLocation = tbAudioFolder.Text;
            Properties.Settings.Default.VideoClipLocation = tbClipFolder.Text;
            Properties.Settings.Default.StartDisplayFullScreen = mDisplay.isFullScreen;
            Properties.Settings.Default.AudioSpellNumber = chkSpellNumber.Checked;
            Properties.Settings.Default.PlaySimpleSoundNotification = chkPlaySimpleNotification.Checked;
            Properties.Settings.Default.ShowLogo = chkShowLogo.Checked;
            Properties.Settings.Default.ShowInfoTextTop1 = chkShowInfoTextTop.Checked;
            Properties.Settings.Default.DSEngineVolumeLevel = mDisplay.DSEngine.CurrentVolume;

            if (mDisplay.DSEngine.DisplaySource == DisplaySource.Stream)
            {
                Properties.Settings.Default.VideoSource = "VideoInputDevice";
                Properties.Settings.Default.VideoInputDevicePath = mDisplay.DSEngine.VideoInputDevicePath;
                Properties.Settings.Default.VideoInputDeviceName = mDisplay.DSEngine.VideoInputDeviceName;

                if (mDisplay.DSEngine.UsingCrossbar)
                {
                    string src;
                    if (mDisplay.DSEngine.VideoInputDeviceSource == PhysicalConnectorType.Video_Tuner)
                        src = "VideoTuner";
                    else if (mDisplay.DSEngine.VideoInputDeviceSource == PhysicalConnectorType.Video_SVideo)
                        src = "VideoSVideo";
                    else if (mDisplay.DSEngine.VideoInputDeviceSource == PhysicalConnectorType.Video_Composite)
                        src = "VideoComposite";
                    else src = String.Empty;

                    Properties.Settings.Default.VideoInputDeviceSource = src;
                }
            }
            else if (mDisplay.DSEngine.DisplaySource == DisplaySource.Clip)
                Properties.Settings.Default.VideoSource = "VideoCLip";

            Properties.Settings.Default.QueueServerHost = tbServer.Text;
            Properties.Settings.Default.QueueServerPort = Convert.ToInt32(tbPort.Text);
            Properties.Settings.Default.StationName = tbStation.Text;
            Properties.Settings.Default.StationPost = cbPost.Text;

            Properties.Settings.Default.Post0Name = post0Name.Text;
            Properties.Settings.Default.Post0Post = post0Post.Text;
            Properties.Settings.Default.Post0Caption = post0Caption.Text;
            Properties.Settings.Default.Post0RunText = post0RunText.Text;
            Properties.Settings.Default.Post0PlayAudio = post0PlayAudio.Checked;

            Properties.Settings.Default.Post1Name = post1Name.Text;
            Properties.Settings.Default.Post1Post = post1Post.Text;
            Properties.Settings.Default.Post1Caption = post1Caption.Text;
            Properties.Settings.Default.Post1RunText = post1RunText.Text;
            Properties.Settings.Default.Post1PlayAudio = post1PlayAudio.Checked;

            Properties.Settings.Default.Post2Name = post2Name.Text;
            Properties.Settings.Default.Post2Post = post2Post.Text;
            Properties.Settings.Default.Post2Caption = post2Caption.Text;
            Properties.Settings.Default.Post2RunText = post2RunText.Text;
            Properties.Settings.Default.Post2PlayAudio = post2PlayAudio.Checked;

            Properties.Settings.Default.Post3Name = post3Name.Text;
            Properties.Settings.Default.Post3Post = post3Post.Text;
            Properties.Settings.Default.Post3Caption = post3Caption.Text;
            Properties.Settings.Default.Post3RunText = post3RunText.Text;
            Properties.Settings.Default.Post3PlayAudio = post3PlayAudio.Checked;
            Properties.Settings.Default.Post3Visible = post3Visible.Checked;

            Properties.Settings.Default.Post4Name = post4Name.Text;
            Properties.Settings.Default.Post4Post = post4Post.Text;
            Properties.Settings.Default.Post4Caption = post4Caption.Text;
            Properties.Settings.Default.Post4RunText = post4RunText.Text;
            Properties.Settings.Default.Post4PlayAudio = post4PlayAudio.Checked;
            Properties.Settings.Default.Post4Visible = post4Visible.Checked;

            Properties.Settings.Default.RunningText0 = txtRuntext0.Text;
            Properties.Settings.Default.RunningText1 = txtRuntext1.Text;

            Properties.Settings.Default.DisplayLogoImg = tbImgLogo.Text;
            Properties.Settings.Default.DisplayLogoText = tbTextLogo.Text;

            Properties.Settings.Default.Save();
        }

        #endregion

        #region Form appearance stuffs

        private void EnableRateButtons(bool enable)
        {
            this.btnStep.Enabled = enable;
            this.btnIncreaseRate.Enabled = enable;
            this.btnDecreaseRate.Enabled = enable;
            this.btnResetRate.Enabled = enable;
            this.btnHalfRate.Enabled = enable;
            this.btnDoubleRate.Enabled = enable;
        }

        private void EnablePlayStopMuteButtons(bool enable)
        {
            this.btnPlay.Enabled = enable;
            this.btnStop.Enabled = enable;
            this.chkMute.Enabled = enable;
        }

        #endregion

        #region Form event handlers

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
            SaveSettings();
        }

        private void OnOption(object sender, EventArgs e)
        {
            if (pnlMore.Visible)
            {
                pnlMore.Visible = false;
                mnuOption.Text = "Show &Options";
            }
            else
            {
                pnlMore.Visible = true;
                mnuOption.Text = "Hide &Options";
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit application?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void OnAbout(object sender, EventArgs e)
        {
            Form about = new AboutBox();
            about.ShowDialog();
        }

        private void OnShowDisplay(object sender, EventArgs e)
        {
            if (mDisplay.IsDisposed )
                return;

            if (mDisplay.Visible == true)
                mDisplay.Hide();
            else
                mDisplay.Show();
        }

        private void OnFullScreen(object sender, EventArgs e)
        {
            mDisplay.ToggleFullScreen();
        }

        private void OnPlay(object sender, EventArgs e)
        {
            mDisplay.DSEngine.PauseClip();

            if (mDisplay.DSEngine.CurrentPlayState == PlayState.Paused)
                this.EnableRateButtons(false);
            else if (mDisplay.DSEngine.CurrentPlayState == PlayState.Running)
                this.EnableRateButtons(true);
        }

        private void OnStopPlayer(object sender, EventArgs e)
        {
            mDisplay.DSEngine.StopClip();
            this.EnableRateButtons(false);
            this.EnablePlayStopMuteButtons(false);
        }

        private void OnStep(object sender, EventArgs e)
        {
            mDisplay.DSEngine.StepOneFrame();
        }

        private void OnRbVideoInput(object sender, EventArgs e)
        {
            tabControl.SelectTab("tabVideoDevice");
            tabVideoDevice.Enabled = true;

            if (mDisplay.DSEngine.CurrentPlayState != PlayState.Init)
            {
                if (mDisplay.DSEngine.DisplaySource == DisplaySource.Clip)
                {
                    this.gbVideo.Enabled = false;
                }
            }
            else
            {
                this.cbDevice.Enabled = true;
                this.gbVideo.Enabled = true;
            }
        }

        private void OnRbVideoClip(object sender, EventArgs e)
        {
            tabControl.SelectTab("tabVideoClip");
            tabVideoClip.Enabled = true;
            tbClipFolder.Enabled = false;
            btnSetClipFolder.Enabled = false;

            if (mDisplay.DSEngine.CurrentPlayState!= PlayState.Init)
            {
                if ( mDisplay.DSEngine.DisplaySource == DisplaySource.Stream )
                    EnableRateButtons(false);
                else
                    EnableRateButtons(true);

                this.EnablePlayStopMuteButtons(true);
            }
            else
            {
                EnableRateButtons(false);
                this.EnablePlayStopMuteButtons(false);
                tabVideoDevice.Enabled = false;
            }
        }

        private void OnDeviceSelected(object sender, EventArgs e)
        {
            if (mDisplay != null)
                mDisplay.DSEngine.VideoInputDevicePath = this.cbDevice.SelectedValue.ToString();
        }

        private void OnChangeRate(object sender, EventArgs e)
        {
            if (sender == btnDecreaseRate) mDisplay.DSEngine.ModifyRate(-0.25);
            if (sender == btnIncreaseRate) mDisplay.DSEngine.ModifyRate(+0.25);
            if (sender == btnResetRate) mDisplay.DSEngine.SetRate(1.0);
            if (sender == btnHalfRate) mDisplay.DSEngine.SetRate(0.5);
            if (sender == btnDoubleRate) mDisplay.DSEngine.SetRate(2.0);
        }

        private void OnChangeVideoInput(object sender, EventArgs e)
        {
            if (radTuner.Checked == true)
            {
                mDisplay.DSEngine.ChangeVideoInputSource(PhysicalConnectorType.Video_Tuner);
            }
            else if (radSVideo.Checked == true)
            {
                mDisplay.DSEngine.ChangeVideoInputSource(PhysicalConnectorType.Video_SVideo);
            }
            else if (radComposite.Checked == true)
            {
                mDisplay.DSEngine.ChangeVideoInputSource(PhysicalConnectorType.Video_Composite);
            }
        }

        private void OnOpenCloseVideo(object sender, EventArgs e)
        {
            if (mDisplay.DSEngine.CurrentPlayState == PlayState.Init)
            {
                bool fromFolder = false;
                if (radVideoClip.Checked == true)
                {
                    if (rbOpenClip.Checked == true)
                        OpenClip(String.Empty);
                    else if ( rbPlayFromFolder.Checked == true )
                    {
                        if (Directory.Exists(tbClipFolder.Text))
                        {
                            OpenClip(tbClipFolder.Text);
                            fromFolder = true;
                        }
                        else
                            MessageBox.Show(this, "Video folder does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (radVideoDevice.Checked == true)
                {
                    OpenVideoDevice();
                }

                if (mDisplay.DSEngine.CurrentPlayState == PlayState.Running)
                {
                    if (fromFolder)
                        Properties.Settings.Default.VideoCLipMode = "PlayFromFolder";

                    gbDisplaySource.Enabled = false;
                    mDisplay.DSEngine.SetVolume(Properties.Settings.Default.DSEngineVolumeLevel);
                }
            }
            else 
            {
                CloseVideo();
                btnOpenCloseVideo.Text = "Open Video";
                gbDisplaySource.Enabled = true;
            }
        }

        private void OnRbPlayFromFolder(object sender, EventArgs e)
        {
            if (rbPlayFromFolder.Checked == false)
            {
                tbClipFolder.Enabled = false;
                btnSetClipFolder.Enabled = false;
            }
            else
            {
                tbClipFolder.Enabled = true;
                btnSetClipFolder.Enabled = true;
            }
        }

        private void OnSetClipFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new System.Windows.Forms.FolderBrowserDialog();
            DialogResult result = browse.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbClipFolder.Text = browse.SelectedPath; ;
            }
        }

        private void OnRbOpenClip(object sender, EventArgs e)
        {
            if (rbOpenClip.Checked == true)
            {
                tbClipFolder.Enabled = false;
                btnSetClipFolder.Enabled = false;
            }
        }

        private void OnInputProp(object sender, EventArgs e)
        {
            if (mDisplay.DSEngine.CurrentPlayState == PlayState.Running)
            {
                IBaseFilter ibf = (IBaseFilter)mDisplay.DSEngine.Tuner;
                mDisplay.DSEngine.DisplayPropertyPage(ibf);
            }
        }

        private void OnCrossbarProp(object sender, EventArgs e)
        {
            if (mDisplay.DSEngine.Crossbar != null)
            {
                IBaseFilter cb = (IBaseFilter)mDisplay.DSEngine.Crossbar;
                mDisplay.DSEngine.DisplayPropertyPage(cb);
            }
        }

        private void OnDeviceProp(object sender, EventArgs e)
        {
            if (mDisplay.DSEngine.SourceFilter != null)
            {
                mDisplay.DSEngine.DisplayPropertyPage(mDisplay.DSEngine.SourceFilter);
            }
        }

        private void OnSaveOpt(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to save changes and reconnect?"
                                                   , "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                Properties.Settings.Default.StationName = tbStation.Text;
                Properties.Settings.Default.StationPost = cbPost.Text;
                Properties.Settings.Default.QueueServerHost = tbServer.Text;
                Properties.Settings.Default.QueueServerPort = Convert.ToInt32(tbPort.Text);

                CloseConnection();
                StartClient();
            }
        }

        private void OnSetAudioFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new System.Windows.Forms.FolderBrowserDialog();
            DialogResult result = browse.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbAudioFolder.Text = browse.SelectedPath;
                Properties.Settings.Default.NumberAudioLocation = browse.SelectedPath;
            }
        }

        private void OnSetLogoImg(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.InitialDirectory = Util.ProcessDir;
            fileDlg.Filter = "Image Files(*.PNG;*.JPG;*.BMP)|*.PNG;*.JPG;*.BMP|All files (*.*)|*.*";

            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbImgLogo.Text = fileDlg.FileName;
                Properties.Settings.Default.DisplayLogoImg = fileDlg.FileName;
            }
        }

        private void OnAnimationColor(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.AllowFullOpen = false;
            dlg.ShowHelp = true;
            dlg.Color = btnAnimationColor.BackColor;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                btnAnimationColor.BackColor = dlg.Color;
                Properties.Settings.Default.NumberAnimationColor = dlg.Color;
            }
        }

        private void OnVolume(object sender, EventArgs e)
        {
            int lvl = volSlider.Value * 1000;
            mDisplay.DSEngine.SetVolume(lvl);
            Properties.Settings.Default.DSEngineVolumeLevel = lvl;
            chkMute.Checked = (lvl == -10000);
        }

        private void OnSpellNumber(object sender, EventArgs e)
        {
            Properties.Settings.Default.AudioSpellNumber = chkSpellNumber.Checked;
        }

        private void OnSaveSettings(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.O:
                    {
                        // Ctrl + O
                        if (e.Control)
                        {
                            this.WindowState = FormWindowState.Normal;
                            this.BringToFront();
                        }
                        break;
                    }
                case Keys.F8:
                    {
                        if (btnSwitchDisplay.Enabled)
                        {
                            OnSwitchDisplay(btnSwitchDisplay, EventArgs.Empty);
                        }
                        break;
                    }

                case Keys.F9:
                    {
                        btnShowDisplay.PerformClick();
                        break;
                    }

            }
        }

        private void OnChkMute(object sender, EventArgs e)
        {
            if (!startupComplete)
                return;
            
            if (chkMute.Checked)
            {
                // save current volume
                lastVolume = mDisplay.DSEngine.CurrentVolume;

                mDisplay.DSEngine.SetVolume(-10000);
                volSlider.Enabled = false;
                Properties.Settings.Default.DSEngineVolumeLevel = mDisplay.DSEngine.CurrentVolume;
            }
            else
            {
                mDisplay.DSEngine.SetVolume(lastVolume);
                volSlider.Enabled = true;
                Properties.Settings.Default.DSEngineVolumeLevel = mDisplay.DSEngine.CurrentVolume;
            }

            /*
            mDisplay.DSEngine.ToggleMute();

            if (mDisplay.DSEngine.CurrentVolume == 0)
                volSlider.Value = 0;
            else if (mDisplay.DSEngine.CurrentVolume == -10000)
                volSlider.Value = -10;

            Tobasa.Properties.Settings.Default.DSEngineVolumeLevel = mDisplay.DSEngine.CurrentVolume;
            */
        }
        
        private void OnSwitchDisplay(object sender, EventArgs e)
        {
            if (mDisplay.IsDisposed)
                return;

            //var pos = mDisplay.Location;
            Screen screen = Screen.FromControl(mDisplay);
            if (screen.Primary)
            {
                mDisplay.ShowToSecondScreen();
                mDisplay.SetFullScreen();
            }
            else
            {
                mDisplay.Location = Screen.AllScreens[0].WorkingArea.Location;
                mDisplay.DontFullScreen();
            }
        }

        private void OnPlaySimpleNotification(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            chkSpellNumber.Enabled = !c.Checked;
        }


        #endregion
    }
}
