using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tobasa
{
    public partial class Display : Form
    {
        #region Member variables/class

        /// Struct to save label data -label that need to be resized automatically
        /// See labelRecordList,RecordLabelSize(),OnLabelResize()
        /// Set label Resize event handler to OnLabelResize()
        private struct LabelRecord
        {
            public LabelRecord(Label label)
            {
                this.label = label;
                this.initialWidth = label.Width;
                this.initialHeight = label.Height;
                this.initialFontSize = label.Font.Size;
            }

            public Label label;
            public int initialWidth;
            public int initialHeight;
            public float initialFontSize;
        }

        /// Array runnning text bottom
        ArrayList runningTextList = new ArrayList();

        /// array to list LabelRecord
        ArrayList labelRecordList;

        /// Stopwatch to measure label's animation time
        private Stopwatch swPost0 = new Stopwatch();
        private Stopwatch swPost1 = new Stopwatch();
        private Stopwatch swPost2 = new Stopwatch();
        private Stopwatch swPost3 = new Stopwatch();
        private Stopwatch swPost4 = new Stopwatch();

        /// Timer to display clock
        private Timer timerClock { get; set; }
        /// Timer to animate top text
        private Timer timerTopText;
        static int currentTopText = 0;
        string topTextPost0, topTextPost1, topTextPost2, topTextPost3, topTextPost4;
        string midTextPost0, midTextPost1, midTextPost2, midTextPost3, midTextPost4;
        Bitmap displayLogoImg  = null;

        private const int CP_NOCLOSE_BUTTON = 0x200;
        public bool isFullScreen;
        bool startUpCompleted;

        /// Our DirectShow engine
        private DSEngine dsEngine;

        public DSEngine DSEngine
        {
            get { return dsEngine; }
        }

        #endregion

        #region Running Text stuffs

        public void AddRunningText(string text)
        {
            runningTextList.Add(text);
            runTextBottom.Text = "";
            foreach (string txt in runningTextList)
            {
                if (runTextBottom.Text != "")
                    runTextBottom.Text += "  ::  ";
                runTextBottom.Text += txt;
            }
        }

        public void ResetRunningText()
        {
            runningTextList.Clear();
            runTextBottom.Text = "";
        }

        public void DeleteRunningText(string text)
        {
            foreach (string txt in runningTextList )
            {
                if (txt == text || txt == "ANY")
                {
                    runningTextList.Remove(txt);
                }
            }
            runTextBottom.Text = "";
            foreach (string txt in runningTextList)
            {
                if (runTextBottom.Text != "")
                    runTextBottom.Text += "  ::  ";
                runTextBottom.Text += txt;
            }
        }

        #endregion

        #region QueueServer message handlers

        public void ProcessDisplayCallNumber(string text)
        {
            if (text.StartsWith(Msg.DISPLAY_CALL_NUMBER) || text.StartsWith(Msg.DISPLAY_RECALL_NUMBER))
            {
                string _prefix, _number, _station, _post, _queueCount;
                _prefix = _number = _queueCount = _station = _post = "";

                string[] words = text.Split(Msg.Separator.ToCharArray());
                if (words.Length == 7)
                {
                    _prefix = words[2];
                    _number = words[3];
                    _queueCount = words[4];
                    _station = words[5];
                    _post = words[6];


                    // jika panel post3 atau post4 jalan di hidden, stop process
                    string post3Name = Properties.Settings.Default.Post3Post;
                    string post4Name = Properties.Settings.Default.Post4Post;
                    if (_post.Equals(post3Name) && !Properties.Settings.Default.Post3Visible)
                        return;
                    else if (_post.Equals(post4Name) && !Properties.Settings.Default.Post4Visible)
                        return;
                    else
                    {
                        bool recall = false;
                        if (text.StartsWith(Msg.DISPLAY_CALL_NUMBER))
                            recall = false;
                        else if (text.StartsWith(Msg.DISPLAY_RECALL_NUMBER))
                            recall = true;

                        ProcessDisplayCallNumber(_prefix, _number, _queueCount, _station, _post, recall);
                    }
                }
            }
        }

        public void ProcessDisplayCallNumber(string prefix, string number, string queueCount, string station, string post,bool recall = false)
        {
            string _id = station.Substring(station.IndexOf('#') + 1);

            string post0Name = Properties.Settings.Default.Post0Post;
            string post1Name = Properties.Settings.Default.Post1Post;
            string post2Name = Properties.Settings.Default.Post2Post;
            string post3Name = Properties.Settings.Default.Post3Post;
            string post4Name = Properties.Settings.Default.Post4Post;

            string post0RunText = Properties.Settings.Default.Post0RunText;
            string post1RunText = Properties.Settings.Default.Post1RunText;
            string post2RunText = Properties.Settings.Default.Post2RunText;
            string post3RunText = Properties.Settings.Default.Post3RunText;
            string post4RunText = Properties.Settings.Default.Post4RunText;

            string postMainCaption = "";
            bool playAudio = false;

            if (post.Equals(post0Name))
            {
                if (!recall ) lblPost0JumAnVal.Text = queueCount;
                
                lblPost0No.Text = prefix + number;
                lblPost0CounterNo.Text = _id;

                // animate label
                timerPost0.Start();
                swPost0.Start();

                string text = String.Format("{0} : Nomor {1} di Counter {2}                     ", post0RunText, prefix + number, _id);
                topTextPost0 = text;
                playAudio = Properties.Settings.Default.Post0PlayAudio;
                postMainCaption = Properties.Settings.Default.Post0Caption;
            }
            else if (post.Equals(post1Name))
            {
                if (!recall) lblPost1JumAnVal.Text = queueCount;

                lblPost1No.Text = prefix + number;
                lblPost1CounterNo.Text = _id;

                // animate label
                timerPost1.Start();
                swPost1.Start();

                string text = String.Format("{0} : Nomor {1} di Counter {2}               ", post1RunText, prefix + number, _id);
                topTextPost1 = text;
                playAudio = Properties.Settings.Default.Post1PlayAudio;
                postMainCaption = Properties.Settings.Default.Post1Caption;
            }
            else if (post.Equals(post2Name))
            {
                if (!recall) lblPost2JumAnVal.Text = queueCount;

                lblPost2No.Text = prefix + number;
                lblPost2CounterNo.Text = _id;

                // animate label
                timerPost2.Start();
                swPost2.Start();

                string text = String.Format("{0} : Nomor {1} di Counter {2}            ", post2RunText, prefix + number, _id);
                topTextPost2 = text;
                playAudio = Properties.Settings.Default.Post2PlayAudio;
                postMainCaption = Properties.Settings.Default.Post2Caption;
            }
            else if (post.Equals(post3Name))
            {
                if (!recall) lblPost3JumAnVal.Text = queueCount;

                lblPost3No.Text = prefix + number;
                lblPost3CounterNo.Text = _id;

                // animate label
                timerPost3.Start();
                swPost3.Start();

                string text = String.Format("{0} : Nomor {1} di Counter {2}    ", post3RunText, prefix + number, _id);
                topTextPost3 = text;
                playAudio = Properties.Settings.Default.Post3PlayAudio;
                postMainCaption = Properties.Settings.Default.Post3Caption;
            }
            else if (post.Equals(post4Name))
            {
                if (!recall) lblPost4JumAnVal.Text = queueCount;

                lblPost4No.Text = prefix + number;
                lblPost4CounterNo.Text = _id;

                // animate label
                timerPost4.Start();
                swPost4.Start();

                string text = String.Format("{0} : Nomor {1} di Counter {2}    ", post4RunText, prefix + number, _id);
                topTextPost4 = text;
                playAudio = Properties.Settings.Default.Post4PlayAudio;
                postMainCaption = Properties.Settings.Default.Post4Caption;
            }

            // Main Post Current number display
            if (post == Properties.Settings.Default.StationPost || Properties.Settings.Default.UpdateNumberFromOtherPost )
            {
                lblPostNumber.Text = prefix + number;
                lblPostCounter.Text = "Counter " + _id;
                lblPostNameCaption.Text = postMainCaption;
            }

            // Only play audio if source post equal this display post
            if (post == Properties.Settings.Default.StationPost)
                PlayAudio(prefix, number, _id);
            else if (playAudio)
                PlayAudio(prefix, number, _id);
        }

        public void ProcessDisplayShowMessage(string text)
        {
            if (text.StartsWith(Msg.DISPLAY_SHOW_MESSAGE))   
            {
                string _station, _post, _message;
                 _station = _post = _message = "";

                string[] words = text.Split(Msg.Separator.ToCharArray());
                if (words.Length == 5)
                {
                     _station = words[2];
                     _post = words[3];
                     _message = words[4];

                     string post0Name = Properties.Settings.Default.Post0Post;
                     string post1Name = Properties.Settings.Default.Post1Post;
                     string post2Name = Properties.Settings.Default.Post2Post;
                     string post3Name = Properties.Settings.Default.Post3Post;
                     string post4Name = Properties.Settings.Default.Post4Post;

                     string post0RunText = Properties.Settings.Default.Post0RunText;
                     string post1RunText = Properties.Settings.Default.Post1RunText;
                     string post2RunText = Properties.Settings.Default.Post2RunText;
                     string post3RunText = Properties.Settings.Default.Post3RunText;
                     string post4RunText = Properties.Settings.Default.Post4RunText;

                    // jika panel POST#3 atatu POST#4 di hidden, stop process
                    if (_post.Equals(post3Name) && !Properties.Settings.Default.Post3Visible)
                        return;
                    else if (_post.Equals(post4Name) && !Properties.Settings.Default.Post4Visible)
                        return;
                    else
                    {
                        string _id = _station.Substring(_station.IndexOf('#') + 1);

                        if (_post.Equals(post0Name))
                            midTextPost0 = post0RunText + " : " + _message;
                        else if (_post.Equals(post1Name))
                            midTextPost1 = post1RunText + " : " + _message;
                        else if (_post.Equals(post2Name))
                            midTextPost2 = post2RunText + " : " + _message;
                        else if (_post.Equals(post3Name))
                            midTextPost3 = post3RunText + " : " + _message;
                        else if (_post.Equals(post4Name))
                            midTextPost4 = post4RunText + " : " + _message;
                    }
                }
            }
        }

        public void ProcessDisplayUpdateFinishedJob(string text)
        {
            char[] charSeparators = new char[] { ',' };
            string[] words = text.Split(charSeparators);

            /// Reset first
            lblFin0.Text = ""; lblFin1.Text = ""; lblFin2.Text = ""; lblFin3.Text = ""; lblFin4.Text = "";
            lblFin5.Text = ""; lblFin6.Text = ""; lblFin7.Text = ""; lblFin8.Text = ""; lblFin9.Text = "";
                
            /// then update
            int i=0;
            while (i < words.Length)
            {
                Label _lbl = null;
                switch (i)
                {
                    case 0:
                        _lbl = lblFin0;
                        break;
                    case 1:
                        _lbl = lblFin1;
                        break;
                    case 2:
                        _lbl = lblFin2;
                        break;
                    case 3:
                        _lbl = lblFin3;
                        break;
                    case 4:
                        _lbl = lblFin4;
                        break;
                    case 5:
                        _lbl = lblFin5;
                        break;
                    case 6:
                        _lbl = lblFin6;
                        break;
                    case 7:
                        _lbl = lblFin7;
                        break;
                    case 8:
                        _lbl = lblFin8;
                        break;
                    case 9:
                        _lbl = lblFin9;
                        break;
                }

                _lbl.Text = words[i];
                i++;
            }
        }

        #endregion

        #region Audible information stuffs

        private void PlayAudio(string prefix, string number, string station)
        {
            AudioPlayer player = new Tobasa.AudioPlayer(prefix, number, station);
            player.PlayStartedHandler += new Tobasa.AudioPlayer.PlayStarted(OnPlayAudioStarted);
            player.PlayCompletedHandler += new Tobasa.AudioPlayer.PlayCompleted(OnPlayAudioCompleted);

            player.Play();
        }

        private void OnPlayAudioStarted()
        {
            dsEngine.SetVolume(-4000);
        }

        private void OnPlayAudioCompleted()
        {
            dsEngine.SetVolume(Properties.Settings.Default.DSEngineVolumeLevel);
        }

        #endregion

        #region Constructor / Destructor
        public Display()
        {
            startUpCompleted = false;
            labelRecordList = new ArrayList();
            topTextPost0 = topTextPost1 = topTextPost2 = topTextPost3 = string.Empty;
            isFullScreen = false;
            
            InitializeComponent();
            // need this so we can SetLocation to another screen
            StartPosition = FormStartPosition.Manual;
            // we want to receive key event
            KeyPreview = true;

            leftDivLogo.Visible = Properties.Settings.Default.ShowLogo;
            lbInfoTextTop.Visible = Properties.Settings.Default.ShowInfoTextTop;
            lblTopText.Visible = Properties.Settings.Default.ShowMessageTextMiddle;

            if (Properties.Settings.Default.BasicQueueMode)
            {
                pnlResepSelesai.Visible = false;
                tableLayoutPanelAdv.ColumnStyles[0].Width = 0F;
                tableLayoutPanelAdv.ColumnStyles[1].Width = 100F;
            }
            else
            {
                pnlResepSelesai.Visible = true;
                tableLayoutPanelAdv.ColumnStyles[0].Width = 50F;
                tableLayoutPanelAdv.ColumnStyles[1].Width = 50F;
            }

            lblPost0Caption.Text = Properties.Settings.Default.Post0Caption;
            lblPost1Caption.Text = Properties.Settings.Default.Post1Caption;
            lblPost2Caption.Text = Properties.Settings.Default.Post2Caption;
            lblPost3Caption.Text = Properties.Settings.Default.Post3Caption;
            lblPost4Caption.Text = Properties.Settings.Default.Post4Caption;

            AddRunningText(Properties.Settings.Default.RunningText0);
            AddRunningText(Properties.Settings.Default.RunningText1);

            lblPostNameCaption.Text = Properties.Settings.Default.Post0Caption;

            InitLogo();
            RecordLabelSize();
            StartTimers();

            dsEngine = new DSEngine(this.pnlVideo,this.Handle);

            /// Set location to another screen if available
            ShowToSecondScreen();

            if (Properties.Settings.Default.StartDisplayFullScreen)
                SetFullScreen();
            else
                DontFullScreen();
            
            if (Properties.Settings.Default.StartNumberWithUnderscore)
                ResetDisplayNumbers();
            
            AdaptLeftDivPostLayout();

            startUpCompleted = true;
        }
        #endregion

        #region Autoresize Form's Label stuffs

        /// Registered form labels are automatically resized when Form size resized

        /// Save initial data from labels that need to be resized
        /// Every label in the list will have its Resize event handled by OnLabelResize
        /// which will use this data
        private void RecordLabelSize()
        {
            // Logo Text
            labelRecordList.Add(new LabelRecord(lblLogo0));

            // Jam
            labelRecordList.Add(new LabelRecord(lblClock));
            labelRecordList.Add(new LabelRecord(lblDate));

            // POST#0
            labelRecordList.Add(new LabelRecord(lblPost0Caption));
            labelRecordList.Add(new LabelRecord(lblPost0JumAn));
            labelRecordList.Add(new LabelRecord(lblPost0No));
            labelRecordList.Add(new LabelRecord(lblPost0JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost0CounterNo)); 

            // POST#1
            labelRecordList.Add(new LabelRecord(lblPost1Caption));
            labelRecordList.Add(new LabelRecord(lblPost1JumAn));
            labelRecordList.Add(new LabelRecord(lblPost1No));
            labelRecordList.Add(new LabelRecord(lblPost1JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost1CounterNo));

            // POST#2
            labelRecordList.Add(new LabelRecord(lblPost2Caption));
            labelRecordList.Add(new LabelRecord(lblPost2JumAn));
            labelRecordList.Add(new LabelRecord(lblPost2No));
            labelRecordList.Add(new LabelRecord(lblPost2JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost2CounterNo));

            // POST#3
            labelRecordList.Add(new LabelRecord(lblPost3Caption));
            labelRecordList.Add(new LabelRecord(lblPost3JumAn));
            labelRecordList.Add(new LabelRecord(lblPost3No));
            labelRecordList.Add(new LabelRecord(lblPost3JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost3CounterNo));

            // POST#4
            labelRecordList.Add(new LabelRecord(lblPost4Caption));
            labelRecordList.Add(new LabelRecord(lblPost4JumAn));
            labelRecordList.Add(new LabelRecord(lblPost4No));
            labelRecordList.Add(new LabelRecord(lblPost4JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost4CounterNo));

            labelRecordList.Add(new LabelRecord(lblResepFin));
            labelRecordList.Add(new LabelRecord(lblFin0));
            labelRecordList.Add(new LabelRecord(lblFin1));
            labelRecordList.Add(new LabelRecord(lblFin2));
            labelRecordList.Add(new LabelRecord(lblFin3));       
            labelRecordList.Add(new LabelRecord(lblFin4));
            labelRecordList.Add(new LabelRecord(lblFin5));
            labelRecordList.Add(new LabelRecord(lblFin6));
            labelRecordList.Add(new LabelRecord(lblFin7));
            labelRecordList.Add(new LabelRecord(lblFin8));
            labelRecordList.Add(new LabelRecord(lblFin9));

            labelRecordList.Add(new LabelRecord(lblPostNameCaption));
            labelRecordList.Add(new LabelRecord(lblPostNumber));
            labelRecordList.Add(new LabelRecord(lblPostCounter));
        }

        private void ResizeLabel(LabelRecord lbl)
        {
            SuspendLayout();
            // Get the proportionality of the resize
            float proportionalNewWidth = (float)lbl.label.Width / lbl.initialWidth;
            float proportionalNewHeight = (float)lbl.label.Height / lbl.initialHeight;

            // Calculate the current font size
            lbl.label.Font = new Font(lbl.label.Font.FontFamily, lbl.initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               lbl.label.Font.Style);

            Image img = lbl.label.Image;
            if (img != null)
            {
                var pic = new Bitmap(img, lbl.label.Width, lbl.label.Height);
                lbl.label.Image = pic;
            }

            ResumeLayout();
        }

        private void OnLabelResize(object sender, EventArgs e)
        {
            foreach (LabelRecord lbl in labelRecordList)
            {
                if (lbl.label == (Label)sender)
                {
                    ResizeLabel(lbl);
                    break;
                }
            }
        }

        #endregion

        #region Form Timer stuffs

        private void StartTimers()
        {
            timerClock = new Timer();
            timerClock.Interval = 50;
            timerClock.Tick += new EventHandler(OnTimerClockTick);
            timerClock.Start();

            timerTopText = new Timer();
            timerTopText.Interval = 2000;
            timerTopText.Tick += new EventHandler(OnTimerTopTextTick);
            timerTopText.Start();

            timerPost0.Interval = 500;
            timerPost1.Interval = 500;
            timerPost2.Interval = 500;
            timerPost3.Interval = 500;
            timerPost4.Interval = 500;
        }

        private void OnTimerClockTick(object sender, EventArgs e)
        {
            string hari = "";
            if (DateTime.Now.ToString("dddd") == "Sunday")
                hari = "Minggu";
            else if (DateTime.Now.ToString("dddd") == "Monday")
                hari = "Senin";
            else if (DateTime.Now.ToString("dddd") == "Tuesday")
                hari = "Selasa";
            else if (DateTime.Now.ToString("dddd") == "Wednesday")
                hari = "Rabu";
            else if (DateTime.Now.ToString("dddd") == "Thursday")
                hari = "Kamis";
            else if (DateTime.Now.ToString("dddd") == "Friday")
                hari = "Jumat";
            else if (DateTime.Now.ToString("dddd") == "Saturday")
                hari = "Sabtu";

            lblClock.Text = DateTime.Now.ToString("HH:mm ");
            lblDate.Text = hari + DateTime.Now.ToString("  dd-MM-yyyy ");
            //lblDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
        }

        private void OnTimerTopTextTick(object sender, EventArgs e)
        {
            switch (currentTopText)
            {
                case 0: 
                    lblTopText.Text = topTextPost0;
                    lbInfoTextTop.Text = midTextPost0;
                    break;
                case 1: 
                    lblTopText.Text = topTextPost1;
                    lbInfoTextTop.Text = midTextPost1;
                    break;
                case 2: 
                    lblTopText.Text = topTextPost2;
                    lbInfoTextTop.Text = midTextPost2;
                    break;
                case 3: 
                    lblTopText.Text = topTextPost3;
                    lbInfoTextTop.Text = midTextPost3;
                    break;
                case 4:
                    lblTopText.Text = topTextPost4;
                    lbInfoTextTop.Text = midTextPost4;
                    break;
            }
            if (currentTopText == 4)
                currentTopText = 0;
            else
                currentTopText++;
        }

        private void OnTimer(object sender, EventArgs e)
        {
            Timer tmr = (Timer)sender;
            Stopwatch sw = null;
            Label lblNo = null;
            Label lblCtr = null;

            if (tmr == timerPost0)
            {
                lblNo = lblPost0No;
                lblCtr = lblPost0CounterNo;
                sw = swPost0;
            }
            else if (tmr == timerPost1)
            {
                lblNo = lblPost1No;
                lblCtr = lblPost1CounterNo;
                sw = swPost1;
            }
            else if (tmr == timerPost2)
            {
                lblNo = lblPost2No;
                lblCtr = lblPost2CounterNo;
                sw = swPost2;
            }
            else if (tmr == timerPost3)
            {
                lblNo = lblPost3No;
                lblCtr = lblPost3CounterNo;
                sw = swPost3;
            }
            else if (tmr == timerPost4)
            {
                lblNo = lblPost4No;
                lblCtr = lblPost4CounterNo;
                sw = swPost4;
            }

            // animate label
            if (lblNo.ForeColor == Color.Gold)
            {
                lblNo.ForeColor = Properties.Settings.Default.NumberAnimationColor;
                lblCtr.ForeColor = Properties.Settings.Default.NumberAnimationColor;
            }
            else
            {
                lblNo.ForeColor = Color.Gold;
                lblCtr.ForeColor = Color.Gold;
            }

            /// stops the timer after specified limit(seconds) in settings
            /// hardcoded max limit is 60 seconds
            int maxlimit = 60;
            int limit = Properties.Settings.Default.QueueAnimationTimeInSecond;
            if (limit > 0 && limit < maxlimit)
            {
                if (sw.ElapsedMilliseconds / 1000 > limit)
                {
                    tmr.Stop();
                    sw.Reset();
                }
            }

        }

        #endregion

        #region xxx stuffs

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            if (startUpCompleted)
            {
                switch (m.Msg)
                {
                    case DSEngine.WMGRAPHNOTIFY:
                    {
                        this.DSEngine.HandleGraphEvent();
                        break;
                    }
                }

                /// Pass this message to the video window for notification of system changes
                if (this.DSEngine.VideoWindow != null)
                    this.DSEngine.VideoWindow.NotifyOwnerMessage(m.HWnd, m.Msg, m.WParam, m.LParam);
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Form appearance stuff

        public void InitLogo()
        {
            if (File.Exists(Properties.Settings.Default.DisplayLogoImg))
                displayLogoImg = new Bitmap(Properties.Settings.Default.DisplayLogoImg);
            else
                displayLogoImg = Properties.Resources.QueueLogo150;

            picBoxLogo.Image = displayLogoImg;
            lblLogo0.Text = Properties.Settings.Default.DisplayLogoText;
        }

        public void ResetDisplayNumbers()
        {
            lblPost0No.Text = "_";
            lblPost1No.Text = "_";
            lblPost2No.Text = "_";
            lblPost3No.Text = "_";
            lblPost4No.Text = "_";

            lblPost0CounterNo.Text = "_";
            lblPost1CounterNo.Text = "_";
            lblPost2CounterNo.Text = "_";
            lblPost3CounterNo.Text = "_";
            lblPost4CounterNo.Text = "_";

            lblPostNumber.Text = "_";

            lblPost0JumAnVal.Text = "_";
            lblPost1JumAnVal.Text = "_";
            lblPost2JumAnVal.Text = "_";
            lblPost3JumAnVal.Text = "_";
            lblPost4JumAnVal.Text = "_";

            lblFin0.Text = "_";
            lblFin1.Text = "_";
            lblFin2.Text = "_";
            lblFin3.Text = "_";
            lblFin4.Text = "_";
            lblFin5.Text = "_";
            lblFin6.Text = "_";
            lblFin7.Text = "_";
            lblFin8.Text = "_";
            lblFin9.Text = "_";
        }

        private void AdaptLeftDivPostLayout()
        {
            bool visible3 = Properties.Settings.Default.Post3Visible;
            pnlPost3.Visible = visible3;

            bool visible4 = Properties.Settings.Default.Post4Visible;
            pnlPost4.Visible = visible4;

            if (visible3 && visible4)
            {
                foreach (RowStyle style in leftDivPost.RowStyles)
                {
                    style.SizeType = SizeType.Percent;
                    style.Height = 20F;
                }
            }
            else if (!visible3 && !visible4)
            {
                leftDivPost.RowStyles[0].Height = 33.33F;
                leftDivPost.RowStyles[1].Height = 33.33F;
                leftDivPost.RowStyles[2].Height = 33.33F;
                leftDivPost.RowStyles[3].Height = 0F;
                leftDivPost.RowStyles[4].Height = 0F;
            }
            else if (!visible3 && visible4)
            {
                leftDivPost.RowStyles[0].Height = 25F;
                leftDivPost.RowStyles[1].Height = 25F;
                leftDivPost.RowStyles[2].Height = 25F;
                leftDivPost.RowStyles[3].Height = 0F;
                leftDivPost.RowStyles[4].Height = 25F;
            }
            else if (visible3 && !visible4)
            {
                leftDivPost.RowStyles[0].Height = 25F;
                leftDivPost.RowStyles[1].Height = 25F;
                leftDivPost.RowStyles[2].Height = 25F;
                leftDivPost.RowStyles[3].Height = 25F;
                leftDivPost.RowStyles[4].Height = 0F;
            }
        }

        public void ShowToSecondScreen()
        {
            Screen[] screens = Screen.AllScreens;
            if (screens.Length > 1)
            {
                /// There are multiple monitors.
                foreach (Screen scr in screens)
                {
                    if (!scr.Primary)
                    {
                        /// This is not the primary monitor.
                        var workingArea = scr.WorkingArea;
                        this.Left = workingArea.Left;
                        this.Top = workingArea.Top;
                        this.Width = workingArea.Width;
                        this.Height = workingArea.Height;
                        break;
                    }
                }
            }
        }

        public void UnMinimize()
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        public void SetFullScreen()
        {
            this.WindowState = FormWindowState.Normal; // penting utk langsung tampil di screen kedua
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.isFullScreen = true;
        }

        public void DontFullScreen()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.isFullScreen = false;
        }

        public void ToggleFullScreen()
        {
            if (this.isFullScreen == false)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.isFullScreen = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.isFullScreen = false;
            }
        }

        #endregion

        #region Form event handlers

        private void Display_Move(object sender, System.EventArgs e)
        {
            if (! this.DSEngine.AudioOnly)
                this.DSEngine.ResizeVideoWindow(this.pnlVideo);
        }

        private void Display_Resize(object sender, System.EventArgs e)
        {
            if (!startUpCompleted)
                return;

            if (!this.DSEngine.AudioOnly)
                this.DSEngine.ResizeVideoWindow(this.pnlVideo);
        }

        private void Display_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DSEngine.StopClip();
            this.DSEngine.CloseInterfaces();
        }

        private void On_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    {
                        this.DSEngine.StepOneFrame();
                        break;
                    }
                case Keys.Left:
                    {
                        this.DSEngine.ModifyRate(-0.25);
                        break;
                    }
                case Keys.Right:
                    {
                        this.DSEngine.ModifyRate(+0.25);
                        break;
                    }
                case Keys.Down:
                    {
                        this.DSEngine.SetRate(1.0);
                        break;
                    }
                case Keys.P:
                    {
                        this.DSEngine.PauseClip();
                        break;
                    }
                case Keys.S:
                    {
                        this.DSEngine.StopClip();
                        break;
                    }
                case Keys.M:
                    {
                        this.DSEngine.ToggleMute();
                        break;
                    }
                case Keys.F:
                case Keys.Return:
                    {
                        ToggleFullScreen();
                        break;
                    }
                case Keys.Escape:
                    {
                        if (this.isFullScreen)
                            ToggleFullScreen();
                        else
                            this.DSEngine.CloseClip();
                        break;
                    }
                case Keys.F12 | Keys.Q | Keys.X:
                    {
                        this.DSEngine.CloseClip();
                        break;
                    }

                case Keys.F4:
                    {
                        // Alt + F4
                        if (e.Alt)
                        {
                            if (MessageBox.Show("Exit application?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Application.Exit();
                            }
                        }
                        break;
                    }

            }
        }
        
        #endregion
    }
}

