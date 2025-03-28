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
using System.Collections.Generic;
using System.IO;
using DirectShowLib;
using System.Net.Sockets;

namespace Tobasa
{
    public partial class MainForm : Form
    {
        #region Member variables

        private delegate void NetSessionDataReceivedCb(DataReceivedEventArgs arg);
        private delegate void TCPClientNotifiedCb(NotifyEventArgs e);
        private delegate void TCPClientClosedCb(TCPClient e);

        Properties.Settings _settings = Properties.Settings.Default;
        private PostPropertyCollection _postProperties = new PostPropertyCollection();
        private TCPClient _client = null;

        private System.Timers.Timer _keepConnectedTimer;
        private int _keepConnectedInterval = 10; // 5 second
        private bool _formIsClosing = false;

        private bool _autoConnectToServer = true;
        private String displayTheme = "Classic";
        #endregion

        #region TCP Client stuffs

        private void TCPClientNotified(NotifyEventArgs arg)
        {
            if (this.IsDisposed)
                return;

            if (this.InvokeRequired)
            {
                TCPClientNotifiedCb dlg = new TCPClientNotifiedCb(TCPClientNotified);
                this.Invoke(dlg, new object[] { arg });
            }
            else
            {
                if (arg.Exception is SocketException)
                {
                    SocketException socketEx = (SocketException)arg.Exception;
                    var errCode  = socketEx.SocketErrorCode;
                    var errMsg   = socketEx.Message;

                    if (errCode == SocketError.ConnectionRefused) {
                        // Handle connection refused error
                    }
                }

                if (!_autoConnectToServer && arg.Type == NotifyType.NOTIFY_ERR)
                {
                    MessageBox.Show(arg.Message, arg.Summary, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Logger.Log(arg);
            }
        }

        private void TCPClientConnected(TCPClient client)
        {
            if (client.Connected)
            {
                string clearPwd = Util.DecryptPassword(_settings.QueuePassword, _settings.SecuritySalt);
                string passwordHash = Util.GetPasswordHash(clearPwd, _settings.QueueUserName);

                // Log in to the server
                // SYS|LOGIN|REQ|[Module!Post!Station!Username!Password]
                string message =
                    Msg.SysLogin.Text +
                    Msg.Separator + "REQ" +
                    Msg.Separator + "DISPLAY" +
                    Msg.CompDelimiter + _settings.StationPost +
                    Msg.CompDelimiter + _settings.StationName +
                    Msg.CompDelimiter + _settings.QueueUserName +
                    Msg.CompDelimiter + passwordHash;

                client.Send(message);
            }
        }

        private void TCPClientClosed(TCPClient client)
        {
            if (this.IsDisposed)
                return;

            if (this.InvokeRequired)
            {
                TCPClientClosedCb dlg = new TCPClientClosedCb(TCPClientClosed);
                this.Invoke(dlg, new object[] { client });
            }
            else
            {
                lblStatus.Text = "Not Connected to Server";

                if (!_client.Connected && _autoConnectToServer)
                    RestartClient();
            }
        }

        public void StartClient()
        {
            _client = null;
            _client = new TCPClient(_settings.QueueServerHost, _settings.QueueServerPort);
            _client.Notified += new Action<NotifyEventArgs>(TCPClientNotified);
            _client.OnDataReceived += new DataReceived(NetSessionDataReceived);
            _client.OnConnected += new ConnectionConnected(TCPClientConnected);
            _client.OnClosed += new ConnectionClosed(TCPClientClosed);

            _client.Start();
        }

        private void CloseConnection()
        {
            if (_client.Connected)
                _client.Stop();
        }

        private void RestartClient()
        {
            Logger.Log("QueueAdmin", "Restarting TCPClient");
            CloseConnection();
            StartClient();
        }

        #endregion

        #region QueueServer message handlers


        // cross thread safe handler
        public void NetSessionDataReceived(DataReceivedEventArgs arg)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                NetSessionDataReceivedCb d = new NetSessionDataReceivedCb(NetSessionDataReceived);
                this.Invoke(d, new object[] { arg });
            }
            else
            {
                if (arg.DataString.StartsWith("SYS") || arg.DataString.StartsWith("DISPLAY"))
                    HandleMessage(arg);
                else
                {
                    string logmsg = String.Format("[QueueDisplay] Unhandled session message from: {0}", arg.RemoteInfo);
                    Logger.Log(logmsg);
                }
            }
        }

        public void HandleMessage(DataReceivedEventArgs arg)
        {
            try
            {
                Message qmessage = new Message(arg);

                Logger.Log("[QueueDisplay] Processing " + qmessage.MessageType.String + " from " + arg.Session.RemoteInfo);

                // Handle SysLogin
                if (qmessage.MessageType == Msg.SysLogin && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    string result = qmessage.PayloadValues["result"];
                    string data = qmessage.PayloadValues["data"];

                    if (result == "OK")
                    {
                        lblStatus.Text = "Connected to Server : " + _client.Session.RemoteInfo + " - Post :" + _settings.StationPost + "  Station:" + _settings.StationName;
                        Logger.Log("[QueueDisplay] Successfully logged in");

                        // Request running text stored on database
                        RequestRunningTextFromServer();

                        // Get Last Queue status
                        GetPostQueueInfo("POST0");
                        GetPostQueueInfo("POST1");
                        GetPostQueueInfo("POST2");
                        GetPostQueueInfo("POST3");
                        GetPostQueueInfo("POST4");
                        GetPostQueueInfo("POST5");
                        GetPostQueueInfo("POST6");
                        GetPostQueueInfo("POST7");
                        GetPostQueueInfo("POST8");
                        GetPostQueueInfo("POST9");
                    }
                    else
                    {
                        string reason = data;
                        string msg = "[QueueDisplay] Could not logged in to server, \r\nReason: " + reason;

                        Logger.Log(msg);
                        MessageBox.Show(this, msg, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        CloseConnection();
                    }
                }
                // Handle DisplayCall and DisplayRecall
                else if (qmessage.MessageType == Msg.DisplayCall || qmessage.MessageType == Msg.DisplayRecall)
                {
                    mDisplay.ProcessDisplayCallNumber(qmessage);
                }
                // Handle DisplayShowInfo
                else if (qmessage.MessageType == Msg.DisplayShowInfo && qmessage.Direction == MessageDirection.REQUEST)
                {
                    mDisplay.ProcessDisplayShowMessage(qmessage);
                }
                // Handle DisplaySetFinished
                else if (qmessage.MessageType == Msg.DisplaySetFinished && qmessage.Direction == MessageDirection.REQUEST)
                {
                    mDisplay.ProcessDisplayUpdateFinishedJob(qmessage);
                }
                // Handle DisplayResetValues
                else if (qmessage.MessageType == Msg.DisplayResetValues && qmessage.Direction == MessageDirection.REQUEST)
                {
                    mDisplay.ResetDisplayNumbers();
                }
                // Handle DisplayGetRunText
                else if (qmessage.MessageType == Msg.DisplayGetRunText && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    // we are receiving running text from QueueServer
                    string runningText = qmessage.PayloadValues["result"];
                    mDisplay.AddRunningText(runningText);
                }
                // Handle DisplayResetRunText
                else if (qmessage.MessageType == Msg.DisplayResetRunText && qmessage.Direction == MessageDirection.REQUEST)
                {
                    mDisplay.ResetRunningText();
                }
                // Handle DisplayDelRunText
                else if (qmessage.MessageType == Msg.DisplayDelRunText && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string runningText = qmessage.PayloadValues["text"];
                    mDisplay.DeleteRunningText(runningText);
                }
                // Handle SysNotify
                else if (qmessage.MessageType == Msg.SysNotify)
                {
                    // extract payload
                    string notifyTyp = qmessage.PayloadValues["type"];
                    string notifyMsg = qmessage.PayloadValues["message"];

                    if (notifyTyp == "ERROR")
                        MessageBox.Show(notifyMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        // WARNING, INFO
                        MessageBox.Show(notifyMsg, notifyTyp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (qmessage.MessageType == Msg.DisplayGetInfo && qmessage.Direction == MessageDirection.RESPONSE)
                {
                    mDisplay.ProcessDisplayGetPostInfo(qmessage);
                }
                else
                {
                    Logger.Log(string.Format("[QueueDisplay] Unhandled message from: {0} - MSG: {1} ", arg.RemoteInfo, qmessage.RawMessage));
                }
            }
            catch (Exception ex)
            {
                Logger.Log("QueueDisplay", ex);
            }
        }

        private void RequestRunningTextFromServer()
        {
            if (_client != null)
            {
                string message =
                    Msg.DisplayGetRunText.Text +
                    Msg.Separator + "REQ" +
                    Msg.Separator + "Identifier" +
                    Msg.Separator + _settings.StationPost +
                    Msg.CompDelimiter + _settings.StationName;

                _client.Send(message);
            }
        }

        private void GetPostQueueInfo(string post)
        {
            if (_client != null)
            {
                string message = Msg.DisplayGetInfo.Text +
                                 Msg.Separator + "REQ" +
                                 Msg.Separator + "Identifier" +
                                 Msg.Separator + post;

                _client.Send(message);
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
            mDisplay.DSEngine.SetVolume(_settings.DSEngineVolumeLevel);
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
                mDisplay.DSEngine.StartDisplay(mDisplay.centerPanelVideo, mDisplay.Handle, DisplaySource.Clip, fileName);
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

            mDisplay.DSEngine.StartDisplay(mDisplay.centerPanelVideo, mDisplay.Handle, DisplaySource.Stream, "");

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
            // we want to receive key event
            KeyPreview = true;
            this.WindowState = FormWindowState.Minimized;
            
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            pnlMore.Visible = false;

            InitDeviceList();

            mDisplay = new Display();
            mDisplay.DSEngine.Notified += new Action<NotifyEventArgs>(DSEngine_Notified);
            mDisplay.DSEngine.PlayerStarted += new Action<EventArgs>(DSEngine_PlayerStarted);

            // Get POST Ids
            string[] postIds = new string[_settings.UIPostList.Count];
            _settings.UIPostList.CopyTo(postIds, 0);

            // Populate PostPropertyCollection
            foreach (string id in postIds)
            {
                _postProperties.Add(id, new PostProperty(id));
            }

            // Populate cbPost
            cbPost.Items.Clear();
            cbPost.Items.AddRange(postIds);

            // Populate cbSelectPost
            cbSelectPost.Items.Clear();
            cbSelectPost.Items.AddRange(postIds);

            // Check Screen Count
            Screen[] screens = Screen.AllScreens;
            if (screens.Length > 1)
                btnSwitchDisplay.Enabled = true;

            RestoreSettings();

            if (_settings.PlayVideoAtStartup)
                OnOpenCloseVideo(this, EventArgs.Empty);

            // start tcp client
            StartClient();

            InitKeepConnectedTimer();

            //mDisplay.TopMost = true;
            mDisplay.Focus();
            mDisplay.Show();
            startupComplete = true;
        }

        #endregion

        #region Keep Connected Timer stuff

        private void InitKeepConnectedTimer()
        {
            if (_autoConnectToServer)
            {
                // Initialize the keepalive timer and its default value.
                _keepConnectedTimer = new System.Timers.Timer();
                _keepConnectedTimer.Elapsed += new System.Timers.ElapsedEventHandler(KeepConnectedTimer_Elapsed);
                _keepConnectedTimer.Interval = _keepConnectedInterval * 1000;
                _keepConnectedTimer.Enabled = true;
            }
        }

        private void KeepConnectedTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            if (_formIsClosing)
                return;

            if (!_client.Connected && _autoConnectToServer)
                RestartClient();
        }

        #endregion

        #region Application settings stuffs

        private void SetPostPropertiesControl(string postid)
        {
            string postId = postid;
            PostProperty prop = _postProperties.FindById(postId);

            if (prop != null)
            {
                tbPostName.Text = prop.Name;
                tbPostId.Text = prop.Id;
                tbPostCaption.Text = prop.Caption;
                chkPostPlayAudio.Checked = prop.PlayAudio;
                tbPostRunText.Text = prop.RunText;

                if (prop.Id == _settings.StationPost)
                    chkPostPlayAudio.Checked = true;

                if (prop.Index == 0 || prop.Index == 1 || prop.Index == 2 ||
                   prop.Index == 5 || prop.Index == 6 || prop.Index == 7)
                {
                    chkPostVisible.Checked = true;
                    chkPostVisible.Enabled = false;
                }
                else
                {
                    chkPostVisible.Checked = prop.Visible;
                    chkPostVisible.Enabled = true;
                }
            }
        }

        private void RestoreSettings()
        {
            // Restore last used video device
            if (_settings.VideoInputDevicePath != String.Empty)
                cbDevice.SelectedValue = _settings.VideoInputDevicePath;
            
            // Restore last used video input device source
            if (_settings.VideoSource == "VideoInputDevice")
                radVideoDevice.Checked = true;
            else if (_settings.VideoSource == "VideoCLip")
                radVideoClip.Checked = true;

            if (_settings.VideoCLipMode == "PlayFromFolder")
                rbPlayFromFolder.Checked = true;
            else if (_settings.VideoCLipMode == "PlayBySelectManually")
                rbOpenClip.Checked = true;

            tbClipFolder.Text = _settings.VideoClipLocation;

            tbServer.Text = _settings.QueueServerHost;
            tbPort.Text = _settings.QueueServerPort.ToString();
            tbStation.Text = _settings.StationName;
            cbPost.Text = _settings.StationPost;

            tbClipFolder.Text = _settings.VideoClipLocation;
            tbAudioFolder.Text = _settings.NumberAudioLocation;
            chkSpellNumber.Checked = _settings.AudioSpellNumber;
            chkPlaySimpleNotification.Checked = _settings.PlaySimpleSoundNotification;
            chkShowLogo.Checked = _settings.ShowLogo;
            chkShowInfoTextTop0.Checked = _settings.ShowInfoTextTop0;
            chkShowInfoTextTop1.Checked = _settings.ShowInfoTextTop1;
            chkShowCenterMiddleDiv.Checked = _settings.ShowCenterMiddleDiv;
            chkShowLeftPosts.Checked = _settings.ShowLeftPosts;
            chkShowRightPosts.Checked = _settings.ShowRightPosts;

            btnAnimationColor.BackColor = _settings.NumberAnimationColor;
            numericUpDown.Value = (decimal)_settings.QueueAnimationTimeInSecond;
            
            // DSEngine volume related settings
            volSlider.Value = _settings.DSEngineVolumeLevel / 1000;
            mDisplay.DSEngine.SetVolume(_settings.DSEngineVolumeLevel);
            lastVolume = _settings.DSEngineVolumeLevel;
            chkMute.Checked = (lastVolume == -10000);
            volSlider.Enabled = (lastVolume > -10000);

            chkSetUnderscore.Checked = _settings.StartNumberWithUnderscore;
            chkPlayVideoStartup.Checked = _settings.PlayVideoAtStartup;

            // Check sound folder
            string _soundDir = _settings.NumberAudioLocation;
            if (!Directory.Exists(_soundDir))
            {
                _soundDir = Util.ProcessDir + "\\wav";
                _settings.NumberAudioLocation = _soundDir;
            }
            
            if (Directory.Exists(_soundDir))
                tbAudioFolder.Text = _soundDir;

            chkAudioLoketIDUseAlphabet.Checked = _settings.AudioLoketIDUseAlphabet;

            txtRuntext0.Text = _settings.RunningText0;
            txtRuntext1.Text = _settings.RunningText1;

            
            tbMainBrandingImage.Text = _settings.DisplayMainBrandingImage; // Main Branding image that replace tbImgLogo and tbTextLogo
            tbImgLogo.Text = _settings.DisplayLogoImg;
            tbTextLogo.Text = _settings.DisplayLogoText;
            chkUseBrandingImageAsMainLogo.Checked = _settings.UseMainBrandingImage;
            ApplyDisplayLogoOptionState(_settings.UseMainBrandingImage);

            // setup POST Options
            _postProperties.LoadFromConfiguration();
            cbSelectPost.Text = "POST0";
            var postId = cbSelectPost.Text;
            SetPostPropertiesControl(postId);

            RestoreButtonThemes();
        }

        private void SaveSettings()
        {
            _settings.AudioLoketIDUseAlphabet = chkAudioLoketIDUseAlphabet.Checked;
            _settings.QueueAnimationTimeInSecond = (int)numericUpDown.Value;
            _settings.PlayVideoAtStartup = chkPlayVideoStartup.Checked;
            _settings.StartNumberWithUnderscore = chkSetUnderscore.Checked;
            _settings.NumberAudioLocation = tbAudioFolder.Text;
            _settings.VideoClipLocation = tbClipFolder.Text;
            _settings.StartDisplayFullScreen = mDisplay.isFullScreen;
            _settings.AudioSpellNumber = chkSpellNumber.Checked;
            _settings.PlaySimpleSoundNotification = chkPlaySimpleNotification.Checked;
            _settings.ShowLogo = chkShowLogo.Checked;
            _settings.ShowInfoTextTop0 = chkShowInfoTextTop0.Checked;
            _settings.ShowInfoTextTop1 = chkShowInfoTextTop1.Checked;
            _settings.ShowCenterMiddleDiv = chkShowCenterMiddleDiv.Checked;
            _settings.ShowLeftPosts = chkShowLeftPosts.Checked;
            _settings.ShowRightPosts = chkShowRightPosts.Checked;

            _settings.DSEngineVolumeLevel = mDisplay.DSEngine.CurrentVolume;

            if (mDisplay.DSEngine.DisplaySource == DisplaySource.Stream)
            {
                _settings.VideoSource = "VideoInputDevice";
                _settings.VideoInputDevicePath = mDisplay.DSEngine.VideoInputDevicePath;
                _settings.VideoInputDeviceName = mDisplay.DSEngine.VideoInputDeviceName;

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

                    _settings.VideoInputDeviceSource = src;
                }
            }
            else if (mDisplay.DSEngine.DisplaySource == DisplaySource.Clip)
                _settings.VideoSource = "VideoCLip";

            _settings.QueueServerHost = tbServer.Text;
            _settings.QueueServerPort = Convert.ToInt32(tbPort.Text);
            _settings.StationName = tbStation.Text;
            _settings.StationPost = cbPost.Text;

            _settings.RunningText0 = txtRuntext0.Text;
            _settings.RunningText1 = txtRuntext1.Text;

            _settings.DisplayMainBrandingImage = tbMainBrandingImage.Text;
            _settings.DisplayLogoImg = tbImgLogo.Text;
            _settings.DisplayLogoText = tbTextLogo.Text;
            _settings.UseMainBrandingImage = chkUseBrandingImageAsMainLogo.Checked;


            // save post options
            var postId = cbSelectPost.Text;
            PostProperty prop = _postProperties.FindById(postId);
            if (prop != null)
            {
                prop.Name = tbPostName.Text;
                //prop.Id = tbPostId.Text;
                prop.Caption = tbPostCaption.Text;
                prop.Visible = chkPostVisible.Checked;
                prop.PlayAudio = chkPostPlayAudio.Checked;
                prop.RunText = tbPostRunText.Text;

                if (prop.Index == 0 || prop.Index == 1 || prop.Index == 2 ||
                   prop.Index == 5 || prop.Index == 6 || prop.Index == 7)
                {
                    prop.Visible = true;
                }
            }

            _postProperties.SaveToConfiguration();

            _settings.Theme = displayTheme;

            _settings.Save();
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

        private void ApplyDisplayLogoOptionState(bool useBranding)
        {
            tbMainBrandingImage.Enabled = useBranding;
            btnSetMainLogo.Enabled = useBranding;
            tbImgLogo.Enabled = !useBranding;
            tbTextLogo.Enabled = !useBranding;
            btnSetLogoImg.Enabled = !useBranding;
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
        #endregion

        #region Form event handlers

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            _formIsClosing = true;
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
                        _settings.VideoCLipMode = "PlayFromFolder";

                    gbDisplaySource.Enabled = false;
                    mDisplay.DSEngine.SetVolume(_settings.DSEngineVolumeLevel);
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

                _settings.StationName = tbStation.Text;
                _settings.StationPost = cbPost.Text;
                _settings.QueueServerHost = tbServer.Text;
                _settings.QueueServerPort = Convert.ToInt32(tbPort.Text);

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
                _settings.NumberAudioLocation = browse.SelectedPath;
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
                _settings.DisplayLogoImg = fileDlg.FileName;
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
                _settings.NumberAnimationColor = dlg.Color;
            }
        }

        private void OnVolume(object sender, EventArgs e)
        {
            int lvl = volSlider.Value * 1000;
            mDisplay.DSEngine.SetVolume(lvl);
            _settings.DSEngineVolumeLevel = lvl;
            chkMute.Checked = (lvl == -10000);
        }

        private void OnSpellNumber(object sender, EventArgs e)
        {
            _settings.AudioSpellNumber = chkSpellNumber.Checked;
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
                _settings.DSEngineVolumeLevel = mDisplay.DSEngine.CurrentVolume;
            }
            else
            {
                mDisplay.DSEngine.SetVolume(lastVolume);
                volSlider.Enabled = true;
                _settings.DSEngineVolumeLevel = mDisplay.DSEngine.CurrentVolume;
            }

            /*
            mDisplay.DSEngine.ToggleMute();

            if (mDisplay.DSEngine.CurrentVolume == 0)
                volSlider.Value = 0;
            else if (mDisplay.DSEngine.CurrentVolume == -10000)
                volSlider.Value = -10;

            Tobasa._settings.DSEngineVolumeLevel = mDisplay.DSEngine.CurrentVolume;
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

        private void OnCounterUseAlphabet(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            _settings.AudioLoketIDUseAlphabet = c.Checked;
        }

        private void OnCbSelectPostChanged(object sender, EventArgs e)
        {
            string postId = cbSelectPost.Text;
            SetPostPropertiesControl(postId);
        }

        private void OnThemeSelected(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            String themeName = b.Name;
            String selectedTheme = mDisplay.ApplyTheme(themeName);
            displayTheme = selectedTheme;
        }

        private void OnSetMainLogoImage(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.InitialDirectory = Util.ProcessDir;
            fileDlg.Filter = "Image Files(*.PNG;*.JPG;*.BMP)|*.PNG;*.JPG;*.BMP|All files (*.*)|*.*";

            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbMainBrandingImage.Text = fileDlg.FileName;
                _settings.DisplayMainBrandingImage = fileDlg.FileName;
            }
        }

        private void OnUseBrandingImageAsMainLogo(object sender, EventArgs e)
        {
            bool useBranding = chkUseBrandingImageAsMainLogo.Checked;

            ApplyDisplayLogoOptionState(useBranding);
        }

        #endregion
    }
}
