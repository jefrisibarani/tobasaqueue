﻿using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Text.RegularExpressions;

namespace Tobasa
{
    class AudioPlayer
    {
        #region Member variables
        public delegate void PlayStarted();
        public delegate void PlayCompleted();
        public event PlayStarted PlayStartedHandler;
        public event PlayCompleted PlayCompletedHandler;

        int _number = 0;
        int _station = 0;
        string _prefix = "";
        string _soundDir = "";
        protected Thread thread = null;
        private static AutoResetEvent allDone = new AutoResetEvent(true);

        #endregion

        public AudioPlayer(string prefix, string number, string station)
        {
            _prefix = prefix.Trim();
            _number  = Convert.ToInt32(number);
            _station = Convert.ToInt32(station);
            _soundDir = Properties.Settings.Default.NumberAudioLocation;
            _soundDir += "\\";
        }

        public void Play()
        {
            if (_number < 1000 && _station < 10)
            {
                /// Play audio in separate thread
                ThreadStart starter = new ThreadStart(DoPlay);
                thread = new Thread(starter);
                thread.Start();
            }
            else
            {
                Logger.Log("QueueDisplay AudioPlayer : Maximum queue number is 999, station number is 9 ");
            }
        }

        private void DoPlay()
        {
            /// get exclusive access to play
            allDone.WaitOne();

            if (PlayStartedHandler != null)
                PlayStartedHandler();

            string audioFile = "";
           
            try
            {
                if (Properties.Settings.Default.PlaySimpleSoundNotification)
                {
                    audioFile = _soundDir + "simple_notification.wav";
                    new SoundPlayer(audioFile).PlaySync();
                }
                else
                {
                    audioFile = _soundDir + "antrian.wav";
                    new SoundPlayer(audioFile).PlaySync();


                    /// Play every char in Prefix
                    
                    if (Regex.IsMatch(_prefix, @"^[a-zA-Z]+$"))
                    {
                        foreach (char c in _prefix)
                        {
                            /// Play Prefix
                            string pref = c.ToString();
                            pref = pref.ToLower();
                            pref = pref + ".wav";
                            audioFile = _soundDir + pref;
                            new SoundPlayer(audioFile).PlaySync();
                        }
                    }
                    

                    /// Play nomor antrian
                    string txt0 = "";
                    if (Properties.Settings.Default.AudioSpellNumber)
                        txt0 = NumToWords2(_number);
                    else
                        txt0 = NumToWords(_number);

                    /// split with null = whitespace
                    string[] words0 = txt0.Split(null);
                    foreach (string word in words0)
                    {
                        audioFile = _soundDir + word + ".wav";
                        new SoundPlayer(audioFile).PlaySync();
                    }

                    /// Play "di counter"
                    audioFile = _soundDir + "counter.wav";
                    new SoundPlayer(audioFile).PlaySync();

                    /// Play nomor counter
                    string txt1 = NumToWords(_station);
                    string[] words1 = txt1.Split(null);
                    foreach (string word in words1)
                    {
                        audioFile = _soundDir + word + ".wav";
                        new SoundPlayer(audioFile).PlaySync();
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Logger.Log("QueueDisplay AudioPlayer : FileNotFoundException : " + e.Message + " - " + audioFile);
            }
            catch (InvalidOperationException e)    
            {
                Logger.Log("QueueDisplay AudioPlayer : InvalidOperationException : " + e.Message);
            }
            catch (Exception e)
            {
                Logger.Log("QueueDisplay AudioPlayer : Exception : " + e.Message);
            }

            if (PlayCompletedHandler != null)
                PlayCompletedHandler();

            // allow another thread to Play
            allDone.Set();
        }

        public string NumToWords(int nNumber)
        {
            string cWords = "";

            if (nNumber < 20 )
            {
                switch ( (int)nNumber )
                {
                    case 0: cWords = "nol";break;
                    case 1: cWords = "satu";break;
                    case 2: cWords = "dua";break;
                    case 3: cWords = "tiga";break;
                    case 4: cWords = "empat";break;
                    case 5: cWords = "lima";break;
                    case 6: cWords = "enam";break;
                    case 7: cWords = "tujuh";break;
                    case 8: cWords = "delapan";break;
                    case 9: cWords = "sembilan";break;
                    case 10: cWords = "sepuluh";break;
                    case 11: cWords = "sebelas";break;
                    case 12: cWords = "duabelas";break;
                    case 13: cWords = "tigabelas";break;
                    case 14: cWords = "empatbelas";break;
                    case 15: cWords = "limabelas";break;
                    case 16: cWords = "enambelas";break;
                    case 17: cWords = "tujuhbelas";break;
                    case 18: cWords = "delapanbelas";break;
                    case 19: cWords = "sembilanbelas";break;
                }
            }
            else if (nNumber < 100)
            {
                int nTensPlace = (int) (nNumber / 10);
                int nOnesPlace = (int) (nNumber % 10);
            
                switch (nTensPlace * 10)
                {
                    case 20: cWords = "duapuluh";break;
                    case 30: cWords = "tigapuluh";break;
                    case 40: cWords = "empatpuluh";break;
                    case 50: cWords = "limapuluh";break;
                    case 60: cWords = "enampuluh";break;
                    case 70: cWords = "tujuhpuluh";break;
                    case 80: cWords = "delapanpuluh";break;
                    case 90: cWords = "sembilanpuluh";break;
                }

                if (nOnesPlace > 0 )
                {
                    cWords = cWords + " " + NumToWords(nOnesPlace);
                }
            }
            else if (nNumber < 1000 )
            {
                int nHundredsPlace = (int) (nNumber / 100);
                int nRest = (int) (nNumber % 100);
                if (nHundredsPlace == 1)
                    cWords = "seratus";
                else
                {
                    cWords = NumToWords(nHundredsPlace);
                    cWords = cWords + "ratus";
                }
                
                if (nRest > 0 )
                {
                    cWords = cWords + " " + NumToWords( nRest );
                }
            }

            return cWords;
        }

        public string NumToWords2(int nNumber)
        {
            string str = nNumber.ToString();
            string retVal = "";

            foreach (char c in str)
            {
                string cWord="";
                int i = int.Parse(c.ToString());
                switch (i)
                {
                    case 0: cWord = "nol"; break;
                    case 1: cWord = "satu"; break;
                    case 2: cWord = "dua"; break;
                    case 3: cWord = "tiga"; break;
                    case 4: cWord = "empat"; break;
                    case 5: cWord = "lima"; break;
                    case 6: cWord = "enam"; break;
                    case 7: cWord = "tujuh"; break;
                    case 8: cWord = "delapan"; break;
                    case 9: cWord = "sembilan"; break;
                }

                if (retVal.Length > 0)
                    retVal += " ";
                retVal += cWord;
            }
            return retVal;
        }
    }
}
