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

using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace Tobasa
{
    public partial class Display : Form
    {
        #region Member variables/class

        private delegate void AddRunningTextCallback(string text);
        private delegate void ResetRunningTextTextCallback();
        private delegate void DeleteRunningTextCallback(string text);

        // Struct to save label data -label that need to be resized automatically
        // See labelRecordList,RecordLabelSize(),OnLabelResize()
        // Set label Resize event handler to OnLabelResize()
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

        // Array runnning text bottom
        ArrayList runningTextList = new ArrayList();

        // array to list LabelRecord
        ArrayList labelRecordList;

        // Stopwatch to measure label's animation time
        private Stopwatch swPost0 = new Stopwatch();
        private Stopwatch swPost1 = new Stopwatch();
        private Stopwatch swPost2 = new Stopwatch();
        private Stopwatch swPost3 = new Stopwatch();
        private Stopwatch swPost4 = new Stopwatch();
        private Stopwatch swPost5 = new Stopwatch();
        private Stopwatch swPost6 = new Stopwatch();
        private Stopwatch swPost7 = new Stopwatch();
        private Stopwatch swPost8 = new Stopwatch();
        private Stopwatch swPost9 = new Stopwatch();

        // Timer to display clock
        private Timer timerClock { get; set; }

        // Timer to animate top text
        private Timer timerTopText0;
        private Timer timerTopText1;

        static int currentTopText0 = 0;
        static int currentTopText1 = 0;

        // optional Msg.DISPLAY_CALL_NUMBER info strip, shown in lblTopText0
        string topTextPost0, topTextPost1, topTextPost2, topTextPost3, topTextPost4;
        string topTextPost5, topTextPost6, topTextPost7, topTextPost8, topTextPost9;

        // optional msg.DISPLAY_SHOW_MESSAGE info strip, shown in lblTopText1
        string midTextPost0, midTextPost1, midTextPost2, midTextPost3, midTextPost4;
        string midTextPost5, midTextPost6, midTextPost7, midTextPost8, midTextPost9;
        
        Bitmap displayLogoImg  = null;

        private const int CP_NOCLOSE_BUTTON = 0x200;
        public bool isFullScreen;
        bool startUpCompleted;

        // Our DirectShow engine
        private DSEngine dsEngine;

        public DSEngine DSEngine
        {
            get { return dsEngine; }
        }

        #endregion

        #region Running Text stuffs

        public void AddRunningText(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                AddRunningTextCallback d = new AddRunningTextCallback(AddRunningText);
                this.Invoke(d, new object[] { text });
            }
            else
            { 
                runningTextList.Add(text);
                runningTextBottom.Text = "";
                foreach (string txt in runningTextList)
                {
                    if (runningTextBottom.Text != "")
                        runningTextBottom.Text += "  ::  ";
                    runningTextBottom.Text += txt;
                }
            }
        }

        public void ResetRunningText()
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                ResetRunningTextTextCallback d = new ResetRunningTextTextCallback(ResetRunningText);
                this.Invoke(d, new object[] {});
            }
            else
            {
                runningTextList.Clear();
                runningTextBottom.Text = "";
            }
        }

        public void DeleteRunningText(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                DeleteRunningTextCallback d = new DeleteRunningTextCallback(DeleteRunningText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                foreach (string txt in runningTextList)
                {
                    if (txt == text || txt == "ANY")
                    {
                        runningTextList.Remove(txt);
                    }
                }
                runningTextBottom.Text = "";
                foreach (string txt in runningTextList)
                {
                    if (runningTextBottom.Text != "")
                        runningTextBottom.Text += "  ::  ";
                    runningTextBottom.Text += txt;
                }
            }
        }
        
        #endregion

        #region QueueServer message handlers

        public void ProcessDisplayCallNumber(Message qmessage)
        {
            string prefix       = qmessage.PayloadValues["prefix"];
            string number       = qmessage.PayloadValues["number"];
            string station      = qmessage.PayloadValues["caller"];
            string post         = qmessage.PayloadValues["post"];
            string queueLeft    = qmessage.PayloadValues["left"];

            // If relevant panel/div is hidden, stop.
            string post0Name = Properties.Settings.Default.Post0Post;
            string post1Name = Properties.Settings.Default.Post1Post;
            string post2Name = Properties.Settings.Default.Post2Post;
            string post3Name = Properties.Settings.Default.Post3Post;
            string post4Name = Properties.Settings.Default.Post4Post;
            string post5Name = Properties.Settings.Default.Post5Post;
            string post6Name = Properties.Settings.Default.Post6Post;
            string post7Name = Properties.Settings.Default.Post7Post;
            string post8Name = Properties.Settings.Default.Post8Post;
            string post9Name = Properties.Settings.Default.Post9Post;

            // main left posts visible?
            if ((post.Equals(post0Name) || post.Equals(post1Name) || post.Equals(post2Name) || post.Equals(post3Name) || post.Equals(post4Name))
                && !leftDiv.Visible)
                    return;
                    
            // main right posts visible?
            if ((post.Equals(post5Name) || post.Equals(post6Name) || post.Equals(post7Name) || post.Equals(post8Name) || post.Equals(post9Name))
                    && !rightDiv.Visible)
                    return;

            // right/left div,  two bottom posts visible?
            if (post.Equals(post3Name) && !Properties.Settings.Default.Post3Visible)
                return;
            else if (post.Equals(post4Name) && !Properties.Settings.Default.Post4Visible)
                return;
            else if (post.Equals(post8Name) && !Properties.Settings.Default.Post8Visible)
                return;
            else if (post.Equals(post9Name) && !Properties.Settings.Default.Post9Visible)
                return;
            else
            {
                bool recall = false;
                if (qmessage.MessageType == Msg.DisplayCall)
                    recall = false;
                else if (qmessage.MessageType == Msg.DisplayRecall)
                    recall = true;

                ProcessDisplayCallNumber(prefix, number, queueLeft, station, post, recall);
            }
        }

        public void ProcessDisplayCallNumber(string prefix, string number, string queueCount, string station, string post, bool recall = false)
        {
            string _id = station.Substring(station.IndexOf('#') + 1);

            string post0Name = Properties.Settings.Default.Post0Post;
            string post1Name = Properties.Settings.Default.Post1Post;
            string post2Name = Properties.Settings.Default.Post2Post;
            string post3Name = Properties.Settings.Default.Post3Post;
            string post4Name = Properties.Settings.Default.Post4Post;
            string post5Name = Properties.Settings.Default.Post5Post;
            string post6Name = Properties.Settings.Default.Post6Post;
            string post7Name = Properties.Settings.Default.Post7Post;
            string post8Name = Properties.Settings.Default.Post8Post;
            string post9Name = Properties.Settings.Default.Post9Post;

            // Posts info strip prefix, shown in lblTopText0
            string post0RunText = Properties.Settings.Default.Post0RunText;
            string post1RunText = Properties.Settings.Default.Post1RunText;
            string post2RunText = Properties.Settings.Default.Post2RunText;
            string post3RunText = Properties.Settings.Default.Post3RunText;
            string post4RunText = Properties.Settings.Default.Post4RunText;
            string post5RunText = Properties.Settings.Default.Post5RunText;
            string post6RunText = Properties.Settings.Default.Post6RunText;
            string post7RunText = Properties.Settings.Default.Post7RunText;
            string post8RunText = Properties.Settings.Default.Post8RunText;
            string post9RunText = Properties.Settings.Default.Post9RunText;

            string postMainCaption = "";
            bool playAudio = false;

            // Loket or Counter
            string textCounterOrLabel;
            if (Properties.Settings.Default.AudioUseLoket)
                textCounterOrLabel = "Loket";
            else
                textCounterOrLabel = "Counter";

            // Now, show the information
            if (post.Equals(post0Name))
            {
                if (!recall) lblPost0JumAnVal.Text = queueCount;
                
                lblPost0No.Text = prefix + number;
                lblPost0CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost0.Start();
                swPost0.Start();

                topTextPost0 = String.Format("{0} : Nomor {1} di {2} {3}  ", post0RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post0PlayAudio;
                postMainCaption = Properties.Settings.Default.Post0Caption;
            }
            else if (post.Equals(post1Name))
            {
                if (!recall) lblPost1JumAnVal.Text = queueCount;

                lblPost1No.Text = prefix + number;
                lblPost1CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost1.Start();
                swPost1.Start();

                topTextPost1 = String.Format("{0} : Nomor {1} di {2} {3}  ", post1RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post1PlayAudio;
                postMainCaption = Properties.Settings.Default.Post1Caption;
            }
            else if (post.Equals(post2Name))
            {
                if (!recall) lblPost2JumAnVal.Text = queueCount;

                lblPost2No.Text = prefix + number;
                lblPost2CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost2.Start();
                swPost2.Start();

                topTextPost2 = String.Format("{0} : Nomor {1} di {2} {3}  ", post2RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post2PlayAudio;
                postMainCaption = Properties.Settings.Default.Post2Caption;
            }
            else if (post.Equals(post3Name))
            {
                if (!recall) lblPost3JumAnVal.Text = queueCount;

                lblPost3No.Text = prefix + number;
                lblPost3CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost3.Start();
                swPost3.Start();

                topTextPost3 = String.Format("{0} : Nomor {1} di {2} {3}  ", post3RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post3PlayAudio;
                postMainCaption = Properties.Settings.Default.Post3Caption;
            }
            else if (post.Equals(post4Name))
            {
                if (!recall) lblPost4JumAnVal.Text = queueCount;

                lblPost4No.Text = prefix + number;
                lblPost4CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost4.Start();
                swPost4.Start();

                topTextPost4 = String.Format("{0} : Nomor {1} di {2} {3}  ", post4RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post4PlayAudio;
                postMainCaption = Properties.Settings.Default.Post4Caption;
            }
            else if (post.Equals(post5Name))
            {
                if (!recall) lblPost5JumAnVal.Text = queueCount;

                lblPost5No.Text = prefix + number;
                lblPost5CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost5.Start();
                swPost5.Start();

                topTextPost5 = String.Format("{0} : Nomor {1} di {2} {3}  ", post5RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post5PlayAudio;
                postMainCaption = Properties.Settings.Default.Post5Caption;
            }
            else if (post.Equals(post6Name))
            {
                if (!recall) lblPost6JumAnVal.Text = queueCount;

                lblPost6No.Text = prefix + number;
                lblPost6CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost6.Start();
                swPost6.Start();

                topTextPost6 = String.Format("{0} : Nomor {1} di {2} {3}  ", post6RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post6PlayAudio;
                postMainCaption = Properties.Settings.Default.Post6Caption;
            }
            else if (post.Equals(post7Name))
            {
                if (!recall) lblPost7JumAnVal.Text = queueCount;

                lblPost7No.Text = prefix + number;
                lblPost7CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost7.Start();
                swPost7.Start();

                topTextPost7 = String.Format("{0} : Nomor {1} di {2} {3}  ", post7RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post7PlayAudio;
                postMainCaption = Properties.Settings.Default.Post7Caption;
            }
            else if (post.Equals(post8Name))
            {
                if (!recall) lblPost8JumAnVal.Text = queueCount;

                lblPost8No.Text = prefix + number;
                lblPost8CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost8.Start();
                swPost8.Start();

                topTextPost8 = String.Format("{0} : Nomor {1} di {2} {3}  ", post8RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post8PlayAudio;
                postMainCaption = Properties.Settings.Default.Post8Caption;
            }
            else if (post.Equals(post9Name))
            {
                if (!recall) lblPost9JumAnVal.Text = queueCount;

                lblPost9No.Text = prefix + number;
                lblPost9CounterNo.Text = GetStationIdAsChar(_id);

                // animate label
                timerPost9.Start();
                swPost9.Start();

                topTextPost9 = String.Format("{0} : Nomor {1} di {2} {3}  ", post9RunText, prefix + number, textCounterOrLabel, _id);

                playAudio = Properties.Settings.Default.Post9PlayAudio;
                postMainCaption = Properties.Settings.Default.Post9Caption;
            }

            // Main Post Current number display
            if (post == Properties.Settings.Default.StationPost || Properties.Settings.Default.UpdateNumberFromOtherPost)
            {
                lblPostNumber.Text = prefix + number;
                lblPostCounter.Text = " " + textCounterOrLabel + " " + GetStationIdAsChar(_id);

                if (Tobasa.Properties.Settings.Default.MainPostCounterText.Length > 0)
                    lblPostCounter.Text = Tobasa.Properties.Settings.Default.MainPostCounterText + " " + GetStationIdAsChar(_id);

                lblPostNameCaption.Text = postMainCaption;
            }

            // Only play audio if source post equal this display post
            if (post == Properties.Settings.Default.StationPost)
                PlayAudio(prefix, number, _id);
            else if (playAudio)
                PlayAudio(prefix, number, _id);
        }

        public void ProcessDisplayShowMessage(Message qmessage)
        {
            string station  = qmessage.PayloadValues["caller"];
            string post     = qmessage.PayloadValues["post"];
            string message  = qmessage.PayloadValues["info"];

            string post0Name = Properties.Settings.Default.Post0Post;
            string post1Name = Properties.Settings.Default.Post1Post;
            string post2Name = Properties.Settings.Default.Post2Post;
            string post3Name = Properties.Settings.Default.Post3Post;
            string post4Name = Properties.Settings.Default.Post4Post;
            string post5Name = Properties.Settings.Default.Post5Post;
            string post6Name = Properties.Settings.Default.Post6Post;
            string post7Name = Properties.Settings.Default.Post7Post;
            string post8Name = Properties.Settings.Default.Post8Post;
            string post9Name = Properties.Settings.Default.Post9Post;

            string post0RunText = Properties.Settings.Default.Post0RunText;
            string post1RunText = Properties.Settings.Default.Post1RunText;
            string post2RunText = Properties.Settings.Default.Post2RunText;
            string post3RunText = Properties.Settings.Default.Post3RunText;
            string post4RunText = Properties.Settings.Default.Post4RunText;
            string post5RunText = Properties.Settings.Default.Post5RunText;
            string post6RunText = Properties.Settings.Default.Post6RunText;
            string post7RunText = Properties.Settings.Default.Post7RunText;
            string post8RunText = Properties.Settings.Default.Post8RunText;
            string post9RunText = Properties.Settings.Default.Post9RunText;

            // if panel POST#3/POST#4/POST#8/POST#9 is hidden, stop process
            if (post.Equals(post3Name) && !Properties.Settings.Default.Post3Visible)
                return;
            else if (post.Equals(post4Name) && !Properties.Settings.Default.Post4Visible)
                return;
            else if (post.Equals(post8Name) && !Properties.Settings.Default.Post8Visible)
                return;
            else if (post.Equals(post9Name) && !Properties.Settings.Default.Post9Visible)
                return;
            else
            {
                string _id = station.Substring(station.IndexOf('#') + 1);

                if (post.Equals(post0Name))
                    midTextPost0 = post0RunText + " : " + message;
                else if (post.Equals(post1Name))
                    midTextPost1 = post1RunText + " : " + message;
                else if (post.Equals(post2Name))
                    midTextPost2 = post2RunText + " : " + message;
                else if (post.Equals(post3Name))
                    midTextPost3 = post3RunText + " : " + message;
                else if (post.Equals(post4Name))
                    midTextPost4 = post4RunText + " : " + message;
                else if (post.Equals(post5Name))
                    midTextPost5 = post5RunText + " : " + message;
                else if (post.Equals(post6Name))
                    midTextPost6 = post6RunText + " : " + message;
                else if (post.Equals(post7Name))
                    midTextPost7 = post7RunText + " : " + message;
                else if (post.Equals(post8Name))
                    midTextPost8 = post8RunText + " : " + message;
                else if (post.Equals(post9Name))
                    midTextPost9 = post9RunText + " : " + message;
            }
        }

        public void ProcessDisplayUpdateFinishedJob(Message qmessage)
        {
            string csvData = qmessage.PayloadValues["data"];

            char[] charSeparators = new char[] { ',' };
            string[] words = csvData.Split(charSeparators);

            // Reset first
            lblFin0.Text = ""; lblFin1.Text = ""; lblFin2.Text = ""; lblFin3.Text = ""; lblFin4.Text = "";
            lblFin5.Text = ""; lblFin6.Text = ""; lblFin7.Text = ""; lblFin8.Text = ""; lblFin9.Text = "";
                
            // then update
            int i = 0;
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
            labelRecordList  = new ArrayList();
            isFullScreen     = false;
            
            InitializeComponent();

            // need this so we can SetLocation to another screen
            StartPosition = FormStartPosition.Manual;
            
            // we want to receive key event
            KeyPreview = true;

            SetPostCaptions();
            InitLogo();
            SetLoketOrCounterText();
            RecordLabelSize();
            AdaptCenterLayout();
            AdaptLeftDivPostLayout();
            AdaptRightDivPostLayout();
            AdaptMainLeftAndRightLayout();

            if (Properties.Settings.Default.StartNumberWithUnderscore)
                ResetDisplayNumbers();

            // set two built in runnning text
            AddRunningText(Properties.Settings.Default.RunningText0);
            AddRunningText(Properties.Settings.Default.RunningText1);

            dsEngine = new DSEngine(this.centerPanelVideo, this.Handle);

            // Set location to another screen if available
            ShowToSecondScreen();

            if (Properties.Settings.Default.StartDisplayFullScreen)
                SetFullScreen();
            else
                DontFullScreen();

            StartTimers();

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
            labelRecordList.Add(new LabelRecord(lblBranding));

            // Informasi Hari, Tanggal, Jam
            labelRecordList.Add(new LabelRecord(lblDate));
            
            // Top info strip
            labelRecordList.Add(new LabelRecord(lblTopText0));
            labelRecordList.Add(new LabelRecord(lblTopText1));

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

            // POST#5
            labelRecordList.Add(new LabelRecord(lblPost5Caption));
            labelRecordList.Add(new LabelRecord(lblPost5JumAn));
            labelRecordList.Add(new LabelRecord(lblPost5No));
            labelRecordList.Add(new LabelRecord(lblPost5JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost5CounterNo));

            // POST#6
            labelRecordList.Add(new LabelRecord(lblPost6Caption));
            labelRecordList.Add(new LabelRecord(lblPost6JumAn));
            labelRecordList.Add(new LabelRecord(lblPost6No));
            labelRecordList.Add(new LabelRecord(lblPost6JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost6CounterNo));

            // POST#7
            labelRecordList.Add(new LabelRecord(lblPost7Caption));
            labelRecordList.Add(new LabelRecord(lblPost7JumAn));
            labelRecordList.Add(new LabelRecord(lblPost7No));
            labelRecordList.Add(new LabelRecord(lblPost7JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost7CounterNo));

            // POST#8
            labelRecordList.Add(new LabelRecord(lblPost8Caption));
            labelRecordList.Add(new LabelRecord(lblPost8JumAn));
            labelRecordList.Add(new LabelRecord(lblPost8No));
            labelRecordList.Add(new LabelRecord(lblPost8JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost8CounterNo));

            // POST#9
            labelRecordList.Add(new LabelRecord(lblPost9Caption));
            labelRecordList.Add(new LabelRecord(lblPost9JumAn));
            labelRecordList.Add(new LabelRecord(lblPost9No));
            labelRecordList.Add(new LabelRecord(lblPost9JumAnVal));
            labelRecordList.Add(new LabelRecord(lblPost9CounterNo));

            labelRecordList.Add(new LabelRecord(lblQueueNumberFinished));
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
            bool topInfoStrip0Visible = Properties.Settings.Default.ShowInfoTextTop0;
            bool topInfoStrip1Visible = Properties.Settings.Default.ShowInfoTextTop1;

            timerClock = new Timer();
            timerClock.Interval = 1000;
            timerClock.Tick += new EventHandler(OnTimerClockTick);
            timerClock.Start();
            
            //if (topInfoStrip0Visible)
            {
                timerTopText0 = new Timer();
                timerTopText0.Interval = 4000;
                timerTopText0.Tick += new EventHandler(OnTimerTopText0Tick);
                timerTopText0.Start();
            }

            //if (topInfoStrip1Visible)
            {
                timerTopText1 = new Timer();
                timerTopText1.Interval = 4000;
                timerTopText1.Tick += new EventHandler(OnTimerTopText1Tick);
                timerTopText1.Start();
            }

            timerPost0.Interval = 500;
            timerPost1.Interval = 500;
            timerPost2.Interval = 500;
            timerPost3.Interval = 500;
            timerPost4.Interval = 500;
            timerPost5.Interval = 500;
            timerPost6.Interval = 500;
            timerPost7.Interval = 500;
            timerPost8.Interval = 500;
            timerPost9.Interval = 500;
        }

        private void OnTimerClockTick(object sender, EventArgs e)
        {
            CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");

            string hari = "";
            string dayName = DateTime.Now.ToString("dddd", enUS.DateTimeFormat);

            if (dayName == "Sunday")
                hari = "Minggu";
            else if (dayName == "Monday")
                hari = "Senin";
            else if (dayName == "Tuesday")
                hari = "Selasa";
            else if (dayName == "Wednesday")
                hari = "Rabu";
            else if (dayName == "Thursday")
                hari = "Kamis";
            else if (dayName == "Friday")
                hari = "Jumat";
            else if (dayName == "Saturday")
                hari = "Sabtu";

            string jamMenit = DateTime.Now.ToString("HH.mm.ss ");
            lblDate.Text = hari + DateTime.Now.ToString(" dd-MM-yyyy ") + jamMenit;
        }

        private void OnTimerTopText0Tick(object sender, EventArgs e)
        {
            switch (currentTopText0)
            {
                case 0: lblTopText0.Text = topTextPost0; break;
                case 1: lblTopText0.Text = topTextPost1; break;
                case 2: lblTopText0.Text = topTextPost2; break;
                case 3: lblTopText0.Text = topTextPost3; break;
                case 4: lblTopText0.Text = topTextPost4; break;
                case 5: lblTopText0.Text = topTextPost5; break;
                case 6: lblTopText0.Text = topTextPost6; break;
                case 7: lblTopText0.Text = topTextPost7; break;
                case 8: lblTopText0.Text = topTextPost8; break;
                case 9: lblTopText0.Text = topTextPost9; break;
            }

            if (currentTopText0 == 9)
                currentTopText0 = 0;
            else
                currentTopText0++;
        }

        private void OnTimerTopText1Tick(object sender, EventArgs e)
        {
            switch (currentTopText1)
            {
                case 0: lblTopText1.Text = midTextPost0; break;
                case 1: lblTopText1.Text = midTextPost1; break;
                case 2: lblTopText1.Text = midTextPost2; break;
                case 3: lblTopText1.Text = midTextPost3; break;
                case 4: lblTopText1.Text = midTextPost4; break;
                case 5: lblTopText1.Text = midTextPost5; break;
                case 6: lblTopText1.Text = midTextPost6; break;
                case 7: lblTopText1.Text = midTextPost7; break;
                case 8: lblTopText1.Text = midTextPost8; break;
                case 9: lblTopText1.Text = midTextPost9; break;
            }
            if (currentTopText1 == 9)
                currentTopText1 = 0;
            else
                currentTopText1++;
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
            if (tmr == timerPost5)
            {
                lblNo = lblPost5No;
                lblCtr = lblPost5CounterNo;
                sw = swPost5;
            }
            else if (tmr == timerPost6)
            {
                lblNo = lblPost6No;
                lblCtr = lblPost6CounterNo;
                sw = swPost6;
            }
            else if (tmr == timerPost7)
            {
                lblNo = lblPost7No;
                lblCtr = lblPost7CounterNo;
                sw = swPost7;
            }
            else if (tmr == timerPost8)
            {
                lblNo = lblPost8No;
                lblCtr = lblPost8CounterNo;
                sw = swPost8;
            }
            else if (tmr == timerPost9)
            {
                lblNo = lblPost9No;
                lblCtr = lblPost9CounterNo;
                sw = swPost9;
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

        protected override void WndProc(ref System.Windows.Forms.Message m)
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

        private String GetStationIdAsChar(string id)
        {
            if (Tobasa.Properties.Settings.Default.AudioLoketIDUseAlphabet)
            {
                // ASCII characters: 65 to 90
                // we use 1 = A = 65 ,  2 = B = 66
                int staId = Convert.ToInt32(id);
                staId = 64 + staId;
                
                string stChar = ((char)staId).ToString();
                return stChar;
            }
            
            return id;
        }        

        #endregion

        #region Form appearance stuff

        public void InitLogo()
        {
            if (File.Exists(Properties.Settings.Default.DisplayLogoImg))
                displayLogoImg = new Bitmap(Properties.Settings.Default.DisplayLogoImg);
            else
                displayLogoImg = Properties.Resources.QueueLogo150;

            pictureBoxLogo.Image = displayLogoImg;
            lblBranding.Text = Properties.Settings.Default.DisplayLogoText;
        }

        public void ResetDisplayNumbers()
        {
            lblPost0No.Text = "_";
            lblPost1No.Text = "_";
            lblPost2No.Text = "_";
            lblPost3No.Text = "_";
            lblPost4No.Text = "_";
            lblPost5No.Text = "_";
            lblPost6No.Text = "_";
            lblPost7No.Text = "_";
            lblPost8No.Text = "_";
            lblPost9No.Text = "_";

            lblPost0CounterNo.Text = "_";
            lblPost1CounterNo.Text = "_";
            lblPost2CounterNo.Text = "_";
            lblPost3CounterNo.Text = "_";
            lblPost4CounterNo.Text = "_";
            lblPost5CounterNo.Text = "_";
            lblPost6CounterNo.Text = "_";
            lblPost7CounterNo.Text = "_";
            lblPost8CounterNo.Text = "_";
            lblPost9CounterNo.Text = "_";

            lblPostNumber.Text = "_";

            lblPost0JumAnVal.Text = "_";
            lblPost1JumAnVal.Text = "_";
            lblPost2JumAnVal.Text = "_";
            lblPost3JumAnVal.Text = "_";
            lblPost4JumAnVal.Text = "_";
            lblPost5JumAnVal.Text = "_";
            lblPost6JumAnVal.Text = "_";
            lblPost7JumAnVal.Text = "_";
            lblPost8JumAnVal.Text = "_";
            lblPost9JumAnVal.Text = "_";

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

        private void AdaptRightDivPostLayout()
        {
            bool visible8 = Properties.Settings.Default.Post8Visible;
            pnlPost8.Visible = visible8;

            bool visible9 = Properties.Settings.Default.Post9Visible;
            pnlPost9.Visible = visible9;

            if (visible8 && visible9)
            {
                foreach (RowStyle style in rightDivPost.RowStyles)
                {
                    style.SizeType = SizeType.Percent;
                    style.Height = 20F;
                }
            }
            else if (!visible8 && !visible9)
            {
                rightDivPost.RowStyles[0].Height = 33.33F;
                rightDivPost.RowStyles[1].Height = 33.33F;
                rightDivPost.RowStyles[2].Height = 33.33F;
                rightDivPost.RowStyles[3].Height = 0F;
                rightDivPost.RowStyles[4].Height = 0F;
            }
            else if (!visible8 && visible9)
            {
                rightDivPost.RowStyles[0].Height = 25F;
                rightDivPost.RowStyles[1].Height = 25F;
                rightDivPost.RowStyles[2].Height = 25F;
                rightDivPost.RowStyles[3].Height = 0F;
                rightDivPost.RowStyles[4].Height = 25F;
            }
            else if (visible8 && !visible9)
            {
                rightDivPost.RowStyles[0].Height = 25F;
                rightDivPost.RowStyles[1].Height = 25F;
                rightDivPost.RowStyles[2].Height = 25F;
                rightDivPost.RowStyles[3].Height = 25F;
                rightDivPost.RowStyles[4].Height = 0F;
            }
        }

        private void AdaptMainLeftAndRightLayout()
        {
            bool rightDivVisible = Properties.Settings.Default.ShowRightPosts;
            bool leftDivVisible   = Properties.Settings.Default.ShowLeftPosts;

            if (rightDivVisible == false && leftDivVisible == true)
            {
                rightDiv.Visible = false;
                leftDiv.Visible  = true;

                topDiv.ColumnStyles[0].Width = 35F;
                topDiv.ColumnStyles[1].Width = 65F;
                topDiv.ColumnStyles[2].Width = 0F;
            }
            else if (rightDivVisible == true && leftDivVisible == false)
            {
                leftDiv.Visible  = false;
                rightDiv.Visible = true;

                topDiv.ColumnStyles[0].Width = 0F;
                topDiv.ColumnStyles[1].Width = 65F;
                topDiv.ColumnStyles[2].Width = 35F;
            }
        }

        private void AdaptCenterLayout()
        {
            bool logoVisible          = Properties.Settings.Default.ShowLogo;
            bool topInfoStrip0Visible = Properties.Settings.Default.ShowInfoTextTop0;
            bool topInfoStrip1Visible = Properties.Settings.Default.ShowInfoTextTop1;
            bool centerMiddleVisible  = Properties.Settings.Default.ShowCenterMiddleDiv;
            
            if (!logoVisible && topInfoStrip0Visible && topInfoStrip1Visible && centerMiddleVisible)
            {
                centerBrandDiv.Visible = false;
                centerDiv.RowStyles[0].Height = 0F;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 5F;
                centerDiv.RowStyles[3].Height = 5F;
                centerDiv.RowStyles[4].Height = 25F;
                centerDiv.RowStyles[5].Height = 60F;
            }
            else if (!logoVisible && !topInfoStrip0Visible && topInfoStrip1Visible && centerMiddleVisible)
            {
                centerBrandDiv.Visible = false;
                centerDiv.RowStyles[0].Height = 0F;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 0F;
                centerDiv.RowStyles[3].Height = 5F;
                centerDiv.RowStyles[4].Height = 30F;
                centerDiv.RowStyles[5].Height = 60F;
            }
            else if (!logoVisible && topInfoStrip0Visible && !topInfoStrip1Visible && centerMiddleVisible)
            {
                centerBrandDiv.Visible = false;
                centerDiv.RowStyles[0].Height = 0F;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 5F;
                centerDiv.RowStyles[3].Height = 0F;
                centerDiv.RowStyles[4].Height = 30F;
                centerDiv.RowStyles[5].Height = 60F;
            }
            else if (!logoVisible && !topInfoStrip0Visible && !topInfoStrip1Visible && centerMiddleVisible)
            {
                centerBrandDiv.Visible = false;
                centerDiv.RowStyles[0].Height = 0F;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 0F;
                centerDiv.RowStyles[3].Height = 0F;
                centerDiv.RowStyles[4].Height = 30F;
                centerDiv.RowStyles[5].Height = 65F;
            }
            else if (logoVisible && !topInfoStrip0Visible && !topInfoStrip1Visible && centerMiddleVisible)
            {
                centerDiv.RowStyles[0].Height = 13;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 0F;
                centerDiv.RowStyles[3].Height = 0F;
                centerDiv.RowStyles[4].Height = 25F;
                centerDiv.RowStyles[5].Height = 57F;
            }
            else if (logoVisible && topInfoStrip0Visible && !topInfoStrip1Visible && centerMiddleVisible)
            {
                centerDiv.RowStyles[0].Height = 13;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 5F;
                centerDiv.RowStyles[3].Height = 0F;
                centerDiv.RowStyles[4].Height = 25F;
                centerDiv.RowStyles[5].Height = 52F;
            }
            else if (logoVisible && !topInfoStrip0Visible && topInfoStrip1Visible && centerMiddleVisible)
            {
                centerDiv.RowStyles[0].Height = 13;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 0F;
                centerDiv.RowStyles[3].Height = 5F;
                centerDiv.RowStyles[4].Height = 25F;
                centerDiv.RowStyles[5].Height = 52F;
            }
            else if (logoVisible && topInfoStrip0Visible && topInfoStrip1Visible && !centerMiddleVisible)
            {
                centerDiv.RowStyles[0].Height = 15;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 5F;
                centerDiv.RowStyles[3].Height = 5F;
                centerDiv.RowStyles[4].Height = 0F;
                centerDiv.RowStyles[5].Height = 65F;
            }
            else if (logoVisible && !topInfoStrip0Visible && topInfoStrip1Visible && !centerMiddleVisible)
            {
                centerDiv.RowStyles[0].Height = 15;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 0F;
                centerDiv.RowStyles[3].Height = 5F;
                centerDiv.RowStyles[4].Height = 0F;
                centerDiv.RowStyles[5].Height = 70F;
            }
            else if (logoVisible && topInfoStrip0Visible && !topInfoStrip1Visible && !centerMiddleVisible)
            {
                centerDiv.RowStyles[0].Height = 15;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 5F;
                centerDiv.RowStyles[3].Height = 0F;
                centerDiv.RowStyles[4].Height = 0F;
                centerDiv.RowStyles[5].Height = 70F;
            }
            else if (logoVisible && !topInfoStrip0Visible && !topInfoStrip1Visible && !centerMiddleVisible)
            {
                centerDiv.RowStyles[0].Height = 15;
                centerDiv.RowStyles[1].Height = 5F;
                centerDiv.RowStyles[2].Height = 5F;
                centerDiv.RowStyles[3].Height = 0F;
                centerDiv.RowStyles[4].Height = 0F;
                centerDiv.RowStyles[5].Height = 70F;
            }
            else
            {
                centerDiv.RowStyles[0].Height = 13F;  // branding/logo div
                centerDiv.RowStyles[1].Height = 4F;   // date time
                centerDiv.RowStyles[2].Height = 4F;   // topInfoStrip0
                centerDiv.RowStyles[3].Height = 4F;   // topInfoStrip1
                centerDiv.RowStyles[4].Height = 25F;  // centerMiddleDiv
                centerDiv.RowStyles[5].Height = 50F;  // video panel
            }

            if (Properties.Settings.Default.BasicQueueMode)
            {
                pnlAntrianFinished.Visible = false;
                centerMiddleDiv.ColumnStyles[0].Width = 0F;
                centerMiddleDiv.ColumnStyles[1].Width = 100F;
            }
            else
            {
                pnlAntrianFinished.Visible = true;
                centerMiddleDiv.ColumnStyles[0].Width = 50F;
                centerMiddleDiv.ColumnStyles[1].Width = 50F;
            }
        }

        public void ShowToSecondScreen()
        {
            Screen[] screens = Screen.AllScreens;
            if (screens.Length > 1)
            {
                // There are multiple monitors.
                foreach (Screen scr in screens)
                {
                    if (!scr.Primary)
                    {
                        // This is not the primary monitor.
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

        private void SetLoketOrCounterText()
        {
            if (Tobasa.Properties.Settings.Default.AudioUseLoket)
            {
                lblCounterLeft.Text = "Loket";
                lblCounterRight.Text = "Loket";
            }
            else
            {
                lblCounterLeft.Text = "Counter";
                lblCounterRight.Text = "Counter";
            }
        }

        private void SetPostCaptions()
        {
            lblPost0Caption.Text = Properties.Settings.Default.Post0Caption;
            lblPost1Caption.Text = Properties.Settings.Default.Post1Caption;
            lblPost2Caption.Text = Properties.Settings.Default.Post2Caption;
            lblPost3Caption.Text = Properties.Settings.Default.Post3Caption;
            lblPost4Caption.Text = Properties.Settings.Default.Post4Caption;
            lblPost5Caption.Text = Properties.Settings.Default.Post5Caption;
            lblPost6Caption.Text = Properties.Settings.Default.Post6Caption;
            lblPost7Caption.Text = Properties.Settings.Default.Post7Caption;
            lblPost8Caption.Text = Properties.Settings.Default.Post8Caption;
            lblPost9Caption.Text = Properties.Settings.Default.Post9Caption;

            lblPostNameCaption.Text = Properties.Settings.Default.Post0Caption;
        }

        #endregion

        #region Form event handlers

        private void Display_Move(object sender, System.EventArgs e)
        {
            if (!this.DSEngine.AudioOnly)
                this.DSEngine.ResizeVideoWindow(this.centerPanelVideo);
        }

        private void Display_Resize(object sender, System.EventArgs e)
        {
            if (!startUpCompleted)
                return;

            if (!this.DSEngine.AudioOnly)
                this.DSEngine.ResizeVideoWindow(this.centerPanelVideo);
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

