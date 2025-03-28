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
using System.IO;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;


namespace Tobasa
{
    public partial class Display : Form
    {
        #region Member variables/class
        private const string TEXT_LOKET = "LOKET";
        private const string TEXT_NOMOR = "NOMOR";
        private const string TEXT_COUNTER = "COUNTER";

        private delegate void AddRunningTextCallback(string text);
        private delegate void ResetRunningTextTextCallback();
        private delegate void DeleteRunningTextCallback(string text);
        private delegate void UpdateTotalWaitingQueueCallback(string post, string total);

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
        
        Bitmap displayMainBrandingImage = null;
        PictureBox displayMainBrandingImageBox = null;

        private const int CP_NOCLOSE_BUTTON = 0x200;
        public bool isFullScreen;
        bool startUpCompleted;

        private DisplayTheme colorProfile = null;

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

        public void UpdateTotalWaitingQueue(string post, string total)
        {
            if (string.IsNullOrWhiteSpace(total))
                return;

            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.InvokeRequired)
            {
                UpdateTotalWaitingQueueCallback d = new UpdateTotalWaitingQueueCallback(UpdateTotalWaitingQueue);
                this.Invoke(d, new object[] { post, total });
            }
            else
            {
                if (post == "POST0")
                    lblPost0JumAnVal.Text = total;
                if (post == "POST1")
                    lblPost1JumAnVal.Text = total;
                if (post == "POST2")
                    lblPost2JumAnVal.Text = total;
                if (post == "POST3")
                    lblPost3JumAnVal.Text = total;
                if (post == "POST4")
                    lblPost4JumAnVal.Text = total;
                if (post == "POST5")
                    lblPost5JumAnVal.Text = total;
                if (post == "POST6")
                    lblPost6JumAnVal.Text = total;
                if (post == "POST7")
                    lblPost7JumAnVal.Text = total;
                if (post == "POST8")
                    lblPost8JumAnVal.Text = total;
                if (post == "POST9")
                    lblPost9JumAnVal.Text = total;
            }
        }

        #endregion

        #region QueueServer message handlers

        private bool ContinueProcessDisplayPost(string post)
        {
            // If matching panel/div is hidden, stop.
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
                return false;

            // main right posts visible?
            if ((post.Equals(post5Name) || post.Equals(post6Name) || post.Equals(post7Name) || post.Equals(post8Name) || post.Equals(post9Name))
                    && !rightDiv.Visible)
                return false;

            // right/left div,  two bottom posts visible?
            if (post.Equals(post3Name) && !Properties.Settings.Default.Post3Visible)
                return false;
            else if (post.Equals(post4Name) && !Properties.Settings.Default.Post4Visible)
                return false;
            else if (post.Equals(post8Name) && !Properties.Settings.Default.Post8Visible)
                return false;
            else if (post.Equals(post9Name) && !Properties.Settings.Default.Post9Visible)
                return false;

            return true;
        }

        public void ProcessDisplayCallNumber(Message qmessage)
        {
            string prefix       = qmessage.PayloadValues["prefix"];
            string number       = qmessage.PayloadValues["number"];
            string station      = qmessage.PayloadValues["caller"];
            string post         = qmessage.PayloadValues["post"];
            string queueLeft    = qmessage.PayloadValues["left"];

            if (ContinueProcessDisplayPost(post))
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
            string stationId = station.Substring(station.IndexOf('#') + 1);

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
                lblPost0CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost0.Start();
                swPost0.Start();

                topTextPost0 = String.Format("{0} : Nomor {1} di {2} {3}  ", post0RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post0PlayAudio;
                postMainCaption = Properties.Settings.Default.Post0Caption;
            }
            else if (post.Equals(post1Name))
            {
                if (!recall) lblPost1JumAnVal.Text = queueCount;

                lblPost1No.Text = prefix + number;
                lblPost1CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost1.Start();
                swPost1.Start();

                topTextPost1 = String.Format("{0} : Nomor {1} di {2} {3}  ", post1RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post1PlayAudio;
                postMainCaption = Properties.Settings.Default.Post1Caption;
            }
            else if (post.Equals(post2Name))
            {
                if (!recall) lblPost2JumAnVal.Text = queueCount;

                lblPost2No.Text = prefix + number;
                lblPost2CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost2.Start();
                swPost2.Start();

                topTextPost2 = String.Format("{0} : Nomor {1} di {2} {3}  ", post2RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post2PlayAudio;
                postMainCaption = Properties.Settings.Default.Post2Caption;
            }
            else if (post.Equals(post3Name))
            {
                if (!recall) lblPost3JumAnVal.Text = queueCount;

                lblPost3No.Text = prefix + number;
                lblPost3CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost3.Start();
                swPost3.Start();

                topTextPost3 = String.Format("{0} : Nomor {1} di {2} {3}  ", post3RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post3PlayAudio;
                postMainCaption = Properties.Settings.Default.Post3Caption;
            }
            else if (post.Equals(post4Name))
            {
                if (!recall) lblPost4JumAnVal.Text = queueCount;

                lblPost4No.Text = prefix + number;
                lblPost4CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost4.Start();
                swPost4.Start();

                topTextPost4 = String.Format("{0} : Nomor {1} di {2} {3}  ", post4RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post4PlayAudio;
                postMainCaption = Properties.Settings.Default.Post4Caption;
            }
            else if (post.Equals(post5Name))
            {
                if (!recall) lblPost5JumAnVal.Text = queueCount;

                lblPost5No.Text = prefix + number;
                lblPost5CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost5.Start();
                swPost5.Start();

                topTextPost5 = String.Format("{0} : Nomor {1} di {2} {3}  ", post5RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post5PlayAudio;
                postMainCaption = Properties.Settings.Default.Post5Caption;
            }
            else if (post.Equals(post6Name))
            {
                if (!recall) lblPost6JumAnVal.Text = queueCount;

                lblPost6No.Text = prefix + number;
                lblPost6CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost6.Start();
                swPost6.Start();

                topTextPost6 = String.Format("{0} : Nomor {1} di {2} {3}  ", post6RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post6PlayAudio;
                postMainCaption = Properties.Settings.Default.Post6Caption;
            }
            else if (post.Equals(post7Name))
            {
                if (!recall) lblPost7JumAnVal.Text = queueCount;

                lblPost7No.Text = prefix + number;
                lblPost7CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost7.Start();
                swPost7.Start();

                topTextPost7 = String.Format("{0} : Nomor {1} di {2} {3}  ", post7RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post7PlayAudio;
                postMainCaption = Properties.Settings.Default.Post7Caption;
            }
            else if (post.Equals(post8Name))
            {
                if (!recall) lblPost8JumAnVal.Text = queueCount;

                lblPost8No.Text = prefix + number;
                lblPost8CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost8.Start();
                swPost8.Start();

                topTextPost8 = String.Format("{0} : Nomor {1} di {2} {3}  ", post8RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post8PlayAudio;
                postMainCaption = Properties.Settings.Default.Post8Caption;
            }
            else if (post.Equals(post9Name))
            {
                if (!recall) lblPost9JumAnVal.Text = queueCount;

                lblPost9No.Text = prefix + number;
                lblPost9CounterNo.Text = GetStationIdAsChar(stationId);

                // animate label
                timerPost9.Start();
                swPost9.Start();

                topTextPost9 = String.Format("{0} : Nomor {1} di {2} {3}  ", post9RunText, prefix + number, textCounterOrLabel, stationId);

                playAudio = Properties.Settings.Default.Post9PlayAudio;
                postMainCaption = Properties.Settings.Default.Post9Caption;
            }

            // Main Post Current number display
            if (post == Properties.Settings.Default.StationPost || Properties.Settings.Default.UpdateNumberFromOtherPost)
            {
                lblPostNumber.Text = prefix + number;
                lblPostCounter.Text = " " + textCounterOrLabel + " " + GetStationIdAsChar(stationId);

                if (Tobasa.Properties.Settings.Default.MainPostCounterText.Length > 0)
                    lblPostCounter.Text = Tobasa.Properties.Settings.Default.MainPostCounterText + " " + GetStationIdAsChar(stationId);

                lblPostNameCaption.Text = postMainCaption;
            }

            // Only play audio if source post equal this display post
            if (post == Properties.Settings.Default.StationPost)
                PlayAudio(prefix, number, stationId);
            else if (playAudio)
                PlayAudio(prefix, number, stationId);
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
                string stationId = station.Substring(station.IndexOf('#') + 1);

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

        public void ProcessDisplayGetPostInfo(Message qmessage)
        {
            string post       = qmessage.PayloadValues["postid"];

            if (!ContinueProcessDisplayPost(post))
            {
                return;
            }

            string prefix     = qmessage.PayloadValues["postprefix"];
            string number     = qmessage.PayloadValues["number"];
            string numberleft = qmessage.PayloadValues["numberleft"];
            string station    = qmessage.PayloadValues["station"];
            string stationId ="";
            
            if (!string.IsNullOrWhiteSpace(station) && station.IndexOf('#')>=0)
            {
                stationId = station.Substring(station.IndexOf('#') + 1);
                stationId = GetStationIdAsChar(stationId);
            }

            string queueNo = "";
            if (!string.IsNullOrWhiteSpace(number))
            {
                queueNo = prefix + number;
            }

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
            
            string postMainCaption = "";

            // Loket or Counter
            string textCounterOrLabel;
            if (Properties.Settings.Default.AudioUseLoket)
                textCounterOrLabel = "Loket";
            else
                textCounterOrLabel = "Counter";

            if (post == post0Name)
            {
                lblPost0JumAnVal.Text = numberleft;
                lblPost0No.Text = queueNo;
                lblPost0CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post0Caption;
            }
            else if (post == post1Name)
            {
                lblPost1JumAnVal.Text = numberleft;
                lblPost1No.Text = queueNo;
                lblPost1CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post1Caption;
            }
            else if (post == post2Name)
            {
                lblPost2JumAnVal.Text = numberleft;
                lblPost2No.Text = queueNo;
                lblPost2CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post2Caption;
            }
            else if (post == post3Name)
            {
                lblPost3JumAnVal.Text = numberleft;
                lblPost3No.Text = queueNo;
                lblPost3CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post3Caption;
            }
            else if (post == post4Name)
            {
                lblPost4JumAnVal.Text = numberleft;
                lblPost4No.Text = queueNo;
                lblPost4CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post4Caption;
            }
            else if (post == post5Name)
            {
                lblPost5JumAnVal.Text = numberleft;
                lblPost5No.Text = queueNo;
                lblPost5CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post5Caption;
            }
            else if (post == post6Name)
            {
                lblPost6JumAnVal.Text = numberleft;
                lblPost6No.Text = queueNo;
                lblPost6CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post6Caption;
            }
            else if (post == post7Name)
            {
                lblPost7JumAnVal.Text = numberleft;
                lblPost7No.Text = queueNo;
                lblPost7CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post7Caption;
            }
            else if (post == post8Name)
            {
                lblPost8JumAnVal.Text = numberleft;
                lblPost8No.Text = queueNo;
                lblPost8CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post8Caption;
            }
            else if (post == post9Name)
            {
                lblPost9JumAnVal.Text = numberleft;
                lblPost9No.Text = queueNo;
                lblPost9CounterNo.Text = stationId;
                postMainCaption = Properties.Settings.Default.Post9Caption;
            }

            // Main Post Current number display
            if (post == Properties.Settings.Default.StationPost || Properties.Settings.Default.UpdateNumberFromOtherPost)
            {
                lblPostNumber.Text = queueNo;
                lblPostCounter.Text = " " + textCounterOrLabel + " " + stationId;

                if (Tobasa.Properties.Settings.Default.MainPostCounterText.Length > 0)
                    lblPostCounter.Text = Tobasa.Properties.Settings.Default.MainPostCounterText + " " + stationId;

                lblPostNameCaption.Text = postMainCaption;
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
            try
            {

                startUpCompleted = false;
                labelRecordList = new ArrayList();
                isFullScreen = false;

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

                if (Properties.Settings.Default.Theme != "Classic")
                {
                    ApplyTheme(Properties.Settings.Default.Theme);
                }
                else
                {
                    colorProfile = new DisplayTheme();
                    colorProfile.basePostTextColor = Color.Gold;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error during start up: " + e.Message + "\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (lblNo.ForeColor == colorProfile.basePostTextColor)
            {
                lblNo.ForeColor  = Properties.Settings.Default.NumberAnimationColor;
                lblCtr.ForeColor = Properties.Settings.Default.NumberAnimationColor;
            }
            else
            {
                lblNo.ForeColor  = colorProfile.basePostTextColor;
                lblCtr.ForeColor = colorProfile.basePostTextColor;
            }

            /// stops the timer after specified limit(seconds) in settings
            /// hardcoded max limit is 60 seconds
            int maxlimit = 120;
            int limit = Properties.Settings.Default.QueueAnimationTimeInSecond;
            if (limit > 0 && limit < maxlimit)
            {
                if (sw.ElapsedMilliseconds / 1000 > limit)
                {
                    tmr.Stop();
                    sw.Reset();
                    // go back to initial color
                    lblNo.ForeColor  = colorProfile.basePostTextColor;
                    lblCtr.ForeColor = colorProfile.basePostTextColor;
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
            String displayMainBrandingImagePath = Properties.Settings.Default.DisplayMainBrandingImage;
            bool useMainBrandingImage = Properties.Settings.Default.UseMainBrandingImage;
            if (File.Exists(displayMainBrandingImagePath) && useMainBrandingImage)
            {
                displayMainBrandingImage = new Bitmap(displayMainBrandingImagePath);

                this.centerBrandDiv.Controls.Clear();

                displayMainBrandingImageBox = new PictureBox();
                displayMainBrandingImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
                displayMainBrandingImageBox.Image = displayMainBrandingImage;
                displayMainBrandingImageBox.Location = new System.Drawing.Point(0, 0);
                displayMainBrandingImageBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                displayMainBrandingImageBox.Name = "displayMainBrandingImageBox";
                //displayMainBrandingImageBox.Size = new System.Drawing.Size(97, 80);
                displayMainBrandingImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                //displayMainBrandingImageBox.TabIndex = 0;
                displayMainBrandingImageBox.TabStop = false;


                this.centerBrandDiv.Controls.Add(displayMainBrandingImageBox, 0, 0);
            }
            else
            {
                if (File.Exists(Properties.Settings.Default.DisplayLogoImg))
                    displayLogoImg = new Bitmap(Properties.Settings.Default.DisplayLogoImg);
                else
                    displayLogoImg = Properties.Resources.QueueLogo150;

                pictureBoxLogo.Image = displayLogoImg;
                lblBranding.Text = Properties.Settings.Default.DisplayLogoText;
            }
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
                lblCounterLeft.Text = TEXT_LOKET;
                lblCounterRight.Text = TEXT_LOKET;
                lblPostCounter.Text = TEXT_LOKET;

                // unknown bug, in win 7 32 bit, we need to swap label position
                if (Tobasa.Properties.Settings.Default.SwapCounterNoumberLabelPosition)
                {
                    lblNomorLeft.Text = TEXT_LOKET;
                    lblNomorRight.Text = TEXT_LOKET;
                }
            }
            else
            {
                lblCounterLeft.Text = TEXT_COUNTER;
                lblCounterRight.Text = TEXT_COUNTER;
                lblPostCounter.Text = TEXT_COUNTER;

                if (Tobasa.Properties.Settings.Default.SwapCounterNoumberLabelPosition)
                {
                    lblNomorLeft.Text = TEXT_COUNTER;
                    lblNomorRight.Text = TEXT_COUNTER;
                }
            }

            lblNomorLeft.Text = TEXT_NOMOR;
            lblNomorRight.Text = TEXT_NOMOR;

            if (Tobasa.Properties.Settings.Default.SwapCounterNoumberLabelPosition)
            {
                lblCounterLeft.Text = TEXT_NOMOR;
                lblCounterRight.Text = TEXT_NOMOR;
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

        #region Form Theme
        public String ApplyTheme(String themeName)
        {

            if (themeName == "btnThemeClassic" || themeName == "Classic")
            {
                //DisplayTheme colorProfile = new ThemeClassic();
                //mDisplay.UpdateColor(colorProfile);
                return "Classic";
            }

            if (themeName == "btnThemeBlue" || themeName == "Blue")
            {
                DisplayTheme colorProfile = new DisplayTheme();
                UpdateColor(colorProfile);
                return "Blue";
            }

            if (themeName == "btnThemeGreen" || themeName == "Green")
            {
                DisplayTheme colorProfile = new ThemeGreen();
                UpdateColor(colorProfile);
                return "Green";
            }

            if (themeName == "btnThemeDark" || themeName == "Dark")
            {
                DisplayTheme colorProfile = new ThemeDark();
                UpdateColor(colorProfile);
                return "Dark";
            }

            if (themeName == "btnThemeRed" || themeName == "Red")
            {
                DisplayTheme colorProfile = new ThemeRed();
                UpdateColor(colorProfile);
                return "Red";
            }

            if (themeName == "btnThemeOrange" || themeName == "Orange")
            {
                DisplayTheme colorProfile = new ThemeOrange();
                UpdateColor(colorProfile);
                return "Orange";
            }

            return "Classic";
        }

        public void UpdateColor(DisplayTheme profile)
        {
            colorProfile = profile;

            var baseBackgroundColor         = profile.baseBackgroundColor;
            var baseTextColor               = profile.baseTextColor;
            var baseTextBrandLogoColor      = profile.baseTextBrandLogoColor;

            #region CENTER TOP DIV COLORS
            var centerTopPanelBackColor     = baseBackgroundColor;
            var textInfoDatetimeColor       = profile.basetTextLightColor;
            var textInfoStrip0Color         = profile.textInfoStrip0Color;
            var textInfoStrip1Color         = profile.textInfoStrip1Color;
            var textInfoDatetimeBackColor   = profile.baseInfoTextBackColor;
            var textInfoStrip0BackColor     = profile.textInfoStrip0BackColor;
            var textInfoStrip1BackColor     = profile.textInfoStrip1BackColor;


            var centerMainInfoBoxCaptionBackColor   = profile.centerMainInfoBoxCaptionBackColor;
            var textJobFinishedColor                = profile.labelJobFinishedColor;
            var labelJobFinishedColor               = profile.labelJobFinishedColor;
            var labelFinisBackOddRowColor           = profile.centerMainInfoBoxBackColor; ;
            var labelFinishEvenRowBackColor         = profile.labelFinishEvenRowBackColor;
            var displayOwnPostBackColor             = profile.centerMainInfoBoxBackColor; ;
            var displayOwnPostColor                 = profile.centerMainInfoBoxColor;
            #endregion

            #region LEFT AND RIGHT TOP LABEL COLORS
            var leftRightDivBackColor           = baseBackgroundColor;
            var leftRigthTopQueueNoColor        = profile.leftRigthTopQueueNoColor;
            var leftRigthTopQueueNoBackColor    = baseBackgroundColor;
            var leftRigthTopCounterColor        = profile.leftRigthTopCounterColor;
            var leftRightTopCounterBackColor    = baseBackgroundColor;
            #endregion

            #region POST BOX COLORS
            // COLOR FOR POST INFORMATION
            // --------------------------------------------------------------
            // Post Panel
            var postPanelBackColor              = profile.postPanelBackColor;
            // Post Name Caption
            var postCaptionColor                = profile.postCaptionColor;
            var postCaptionBackColor            = profile.postCaptionBackColor;
            // Queue Number
            var postQueueNoColor                = profile.basePostTextColor;
            var postQueueNoBackColor            = profile.postQueueNoBackColor;
            // Counter/loket Number
            var postCounterNoColor              = profile.basePostTextColor;
            var postCounterNoBackColor          = profile.postCounterNoBackColor;
            // Total Queue Label
            var postTotalQueueLabelColor        = profile.basePostTextColor;
            var postTotalQueueLabelBackColor    = profile.postTotalQueueLabelBackColor;
            // Total Queue Value
            var postTotalQueueValueColor        = profile.basePostTextColor;
            var postTotalQueueValueBackColor    = profile.postTotalQueueValueBackColor;
            #endregion

            #region BOTOM DIV RUNNING TEXT COLOR 
            var bottomDivBackColor              = profile.bottomDivBackColor;
            var bottomDivForeColor              = profile.bottomDivForeColor;
            #endregion

            #region MAIN BRANDING/LOGO BOX
            this.centerBrandDiv.BackColor = centerTopPanelBackColor;
            this.centerBrandDiv.BackgroundImage = null;
            this.centerBrandLogoDiv.BackColor = centerTopPanelBackColor;
            this.centerBrandLogoDiv.BackgroundImage = null;
            this.centerBrandLogoLabelDiv.BackColor = centerTopPanelBackColor;
            this.lblBranding.ForeColor = baseTextBrandLogoColor;
            this.lblBranding.BackColor = centerTopPanelBackColor;
            this.pictureBoxLogo.BackColor = centerTopPanelBackColor;
            #endregion

            #region DATE INFO STRIP 
            this.centerDateTimeDiv.BackgroundImage = null;
            this.centerDateTimeDiv.BackColor = textInfoDatetimeBackColor;
            this.lblDate.ForeColor = textInfoDatetimeColor;
            this.lblDate.BackColor = textInfoDatetimeBackColor;
            #endregion

            #region TOP INFO STRIP 0
            this.centerInfoStrip0Div.BackgroundImage = null;
            this.centerInfoStrip0Div.BackColor = textInfoStrip0BackColor;
            this.lblTopText0.ForeColor = textInfoStrip0Color;
            this.lblTopText0.BackColor = textInfoStrip0BackColor;
            #endregion

            #region TOP INFO STRIP 1
            this.centerInfoStrip1Div.BackgroundImage = null;
            this.centerInfoStrip1Div.BackColor = textInfoStrip1BackColor;
            this.lblTopText1.ForeColor = textInfoStrip1Color;
            this.lblTopText1.BackColor = textInfoStrip1BackColor;
            #endregion

            #region DIV WRAPPER FOR FINISHED JOB AND MAIN POST 
            this.centerMiddleDiv.BackColor = profile.centerMainInfoBoxBackColor; 
            this.centerMiddleDiv.BackgroundImage = null;
            #endregion

            #region FINISHED QUEUE INFO BOX
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(2);

            this.pnlAntrianFinished.BackColor = profile.centerMainInfoBoxBackColor; ;
            this.tableLayoutPanel1.BackColor = profile.centerMainInfoBoxBackColor; ;
            this.lblQueueNumberFinished.ForeColor = textJobFinishedColor;
            this.lblQueueNumberFinished.BackColor = centerMainInfoBoxCaptionBackColor;
            this.tableLayoutPanel2.BackColor = profile.centerMainInfoBoxBackColor; ;
            this.tableLayoutPanel2.BackgroundImage = null;
            this.lblFin0.BackColor = labelFinishEvenRowBackColor;
            this.lblFin1.BackColor = labelFinisBackOddRowColor;
            this.lblFin2.BackColor = labelFinishEvenRowBackColor;
            this.lblFin3.BackColor = labelFinisBackOddRowColor;
            this.lblFin4.BackColor = labelFinishEvenRowBackColor;
            this.lblFin5.BackColor = labelFinishEvenRowBackColor;
            this.lblFin6.BackColor = labelFinisBackOddRowColor;
            this.lblFin7.BackColor = labelFinishEvenRowBackColor;
            this.lblFin8.BackColor = labelFinisBackOddRowColor;
            this.lblFin9.BackColor = labelFinishEvenRowBackColor;

            this.lblFin0.Image= null;
            this.lblFin0.ForeColor = labelJobFinishedColor;
            this.lblFin1.Image = null;
            this.lblFin1.ForeColor = labelJobFinishedColor;
            this.lblFin2.Image = null;
            this.lblFin2.ForeColor = labelJobFinishedColor;
            this.lblFin3.Image = null;
            this.lblFin3.ForeColor = labelJobFinishedColor;
            this.lblFin4.Image = null;
            this.lblFin4.ForeColor = labelJobFinishedColor;
            this.lblFin5.Image = null;
            this.lblFin5.ForeColor = labelJobFinishedColor;
            this.lblFin6.Image = null;
            this.lblFin6.ForeColor = labelJobFinishedColor;
            this.lblFin7.Image = null;
            this.lblFin7.ForeColor = labelJobFinishedColor;
            this.lblFin8.Image = null;
            this.lblFin8.ForeColor = labelJobFinishedColor;
            this.lblFin9.Image = null;
            this.lblFin9.ForeColor = labelJobFinishedColor;
            #endregion

            #region DISPLAY CENTER MAIN POST INFO
            // Panel Post khusus display
            this.pnlOwnPostStat.BackColor = displayOwnPostBackColor;
            this.tableLayoutPanel6.BackColor = displayOwnPostBackColor;
            this.lblPostNameCaption.ForeColor = displayOwnPostColor;
            this.lblPostNameCaption.BackColor = centerMainInfoBoxCaptionBackColor;
            this.tableLayoutPanel3.BackColor = displayOwnPostBackColor;
            this.lblPostCounter.BackColor = displayOwnPostBackColor;
            this.lblPostCounter.ForeColor = displayOwnPostColor;
            this.lblPostNumber.BackColor = displayOwnPostBackColor;
            this.lblPostNumber.ForeColor = displayOwnPostColor;
            #endregion

            #region LEFT AND RIGHT TOP LABEL
            // Form Backgrond Color 
            this.BackColor = baseBackgroundColor;
            // Left and Right Main DIV
            this.leftDivPost.BackColor = leftRightDivBackColor;
            this.rightDivPost.BackColor = leftRightDivBackColor;
            // Top Label Div Queue and Counter 
            this.leftDivNmrCtr.BackgroundImage = null;
            this.leftDivNmrCtr.ForeColor = leftRigthTopQueueNoColor;
            this.leftDivNmrCtr.BackColor = leftRigthTopQueueNoBackColor;
            this.rightDivNmrCtr.BackgroundImage = null;
            this.rightDivNmrCtr.ForeColor = leftRigthTopCounterColor;
            this.rightDivNmrCtr.BackColor = leftRightTopCounterBackColor;
            // Top Label Counter Number
            this.lblCounterLeft.ForeColor = leftRigthTopCounterColor;
            this.lblCounterRight.ForeColor = leftRigthTopCounterColor;
            // Top Label Queue Number
            this.lblNomorLeft.ForeColor = leftRigthTopCounterColor;
            this.lblNomorRight.ForeColor = leftRigthTopCounterColor;
            #endregion

            #region POST0 - POST9 Box

            // --------------------------------------------------------------
            // POST 0
            // --------------------------------------------------------------
            this.pnlPost0.BackgroundImage = null;
            this.pnlPost0.BackColor = postPanelBackColor;
            this.lblPost0Caption.Image = null;
            this.lblPost0Caption.ForeColor = postCaptionColor;
            this.lblPost0Caption.BackColor = postCaptionBackColor;
            this.lblPost0No.ForeColor = postQueueNoColor;
            this.lblPost0No.BackColor = postQueueNoBackColor;
            this.lblPost0CounterNo.Image = null;
            this.lblPost0CounterNo.ForeColor = postCounterNoColor;
            this.lblPost0CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost0JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost0JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost0JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost0JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 1
            // --------------------------------------------------------------
            this.pnlPost1.BackgroundImage = null;
            this.pnlPost1.BackColor = postPanelBackColor;
            this.lblPost1Caption.Image = null;
            this.lblPost1Caption.ForeColor = postCaptionColor;
            this.lblPost1Caption.BackColor = postCaptionBackColor;
            this.lblPost1No.ForeColor = postQueueNoColor;
            this.lblPost1No.BackColor = postQueueNoBackColor;
            this.lblPost1CounterNo.Image = null;
            this.lblPost1CounterNo.ForeColor = postCounterNoColor;
            this.lblPost1CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost1JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost1JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost1JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost1JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 2
            // --------------------------------------------------------------
            this.pnlPost2.BackgroundImage = null;
            this.pnlPost2.BackColor = postPanelBackColor;
            this.lblPost2Caption.Image = null;
            this.lblPost2Caption.ForeColor = postCaptionColor;
            this.lblPost2Caption.BackColor = postCaptionBackColor;
            this.lblPost2No.ForeColor = postQueueNoColor;
            this.lblPost2No.BackColor = postQueueNoBackColor;
            this.lblPost2CounterNo.Image = null;
            this.lblPost2CounterNo.ForeColor = postCounterNoColor;
            this.lblPost2CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost2JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost2JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost2JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost2JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 3
            // --------------------------------------------------------------
            this.pnlPost3.BackgroundImage = null;
            this.pnlPost3.BackColor = postPanelBackColor;
            this.lblPost3Caption.Image = null;
            this.lblPost3Caption.ForeColor = postCaptionColor;
            this.lblPost3Caption.BackColor = postCaptionBackColor;
            this.lblPost3No.ForeColor = postQueueNoColor;
            this.lblPost3No.BackColor = postQueueNoBackColor;
            this.lblPost3CounterNo.Image = null;
            this.lblPost3CounterNo.ForeColor = postCounterNoColor;
            this.lblPost3CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost3JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost3JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost3JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost3JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 4
            // --------------------------------------------------------------
            this.pnlPost4.BackgroundImage = null;
            this.pnlPost4.BackColor = postPanelBackColor;
            this.lblPost4Caption.Image = null;
            this.lblPost4Caption.ForeColor = postCaptionColor;
            this.lblPost4Caption.BackColor = postCaptionBackColor;
            this.lblPost4No.ForeColor = postQueueNoColor;
            this.lblPost4No.BackColor = postQueueNoBackColor;
            this.lblPost4CounterNo.Image = null;
            this.lblPost4CounterNo.ForeColor = postCounterNoColor;
            this.lblPost4CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost4JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost4JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost4JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost4JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 5
            // --------------------------------------------------------------
            this.pnlPost5.BackgroundImage = null;
            this.pnlPost5.BackColor = postPanelBackColor;
            this.lblPost5Caption.Image = null;
            this.lblPost5Caption.ForeColor = postCaptionColor;
            this.lblPost5Caption.BackColor = postCaptionBackColor;
            this.lblPost5No.ForeColor = postQueueNoColor;
            this.lblPost5No.BackColor = postQueueNoBackColor;
            this.lblPost5CounterNo.Image = null;
            this.lblPost5CounterNo.ForeColor = postCounterNoColor;
            this.lblPost5CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost5JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost5JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost5JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost5JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 6
            // --------------------------------------------------------------
            this.pnlPost6.BackgroundImage = null;
            this.pnlPost6.BackColor = postPanelBackColor;
            this.lblPost6Caption.Image = null;
            this.lblPost6Caption.ForeColor = postCaptionColor;
            this.lblPost6Caption.BackColor = postCaptionBackColor;
            this.lblPost6No.ForeColor = postQueueNoColor;
            this.lblPost6No.BackColor = postQueueNoBackColor;
            this.lblPost6CounterNo.Image = null;
            this.lblPost6CounterNo.ForeColor = postCounterNoColor;
            this.lblPost6CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost6JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost6JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost6JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost6JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 7
            // --------------------------------------------------------------
            this.pnlPost7.BackgroundImage = null;
            this.pnlPost7.BackColor = postPanelBackColor;
            this.lblPost7Caption.Image = null;
            this.lblPost7Caption.ForeColor = postCaptionColor;
            this.lblPost7Caption.BackColor = postCaptionBackColor;
            this.lblPost7No.ForeColor = postQueueNoColor;
            this.lblPost7No.BackColor = postQueueNoBackColor;
            this.lblPost7CounterNo.Image = null;
            this.lblPost7CounterNo.ForeColor = postCounterNoColor;
            this.lblPost7CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost7JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost7JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost7JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost7JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 8
            // --------------------------------------------------------------
            this.pnlPost8.BackgroundImage = null;
            this.pnlPost8.BackColor = postPanelBackColor;
            this.lblPost8Caption.Image = null;
            this.lblPost8Caption.ForeColor = postCaptionColor;
            this.lblPost8Caption.BackColor = postCaptionBackColor;
            this.lblPost8No.ForeColor = postQueueNoColor;
            this.lblPost8No.BackColor = postQueueNoBackColor;
            this.lblPost8CounterNo.Image = null;
            this.lblPost8CounterNo.ForeColor = postCounterNoColor;
            this.lblPost8CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost8JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost8JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost8JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost8JumAnVal.BackColor = postTotalQueueValueBackColor;

            // --------------------------------------------------------------
            // POST 9
            // --------------------------------------------------------------
            this.pnlPost9.BackgroundImage = null;
            this.pnlPost9.BackColor = postPanelBackColor;
            this.lblPost9Caption.Image = null;
            this.lblPost9Caption.ForeColor = postCaptionColor;
            this.lblPost9Caption.BackColor = postCaptionBackColor;
            this.lblPost9No.ForeColor = postQueueNoColor;
            this.lblPost9No.BackColor = postQueueNoBackColor;
            this.lblPost9CounterNo.Image = null;
            this.lblPost9CounterNo.ForeColor = postCounterNoColor;
            this.lblPost9CounterNo.BackColor = postCounterNoBackColor;
            this.lblPost9JumAn.ForeColor = postTotalQueueLabelColor;
            this.lblPost9JumAn.BackColor = postTotalQueueLabelBackColor;
            this.lblPost9JumAnVal.ForeColor = postTotalQueueValueColor;
            this.lblPost9JumAnVal.BackColor = postTotalQueueValueBackColor;

            #endregion

            #region BOTTOM RUNNING TEXT DIV
            this.bottomDiv.BackColor = bottomDivBackColor;
            this.bottomDiv.BackgroundImage = null;
            this.runningTextBottom.ForeColor = bottomDivForeColor;
            #endregion
        }

        #endregion
    }
}


