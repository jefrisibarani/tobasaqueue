#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2021  Jefri Sibarani
 
    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/
#endregion

using System;
using System.IO;
using System.Windows.Forms;

namespace Tobasa
{
    public class Logger
    {
        private static string _logFile;
        private static readonly object _locker = new object();

        // Init log file in temporary folder
        public Logger()
        {
            _logFile = GetTempFilePath();
        }

        private static string GetTempFilePath()
        {
            string filename = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            string newFile = Path.GetTempPath() + filename + ".log";
            return newFile;
        }

        public static string LogFile
        {
            get { return _logFile; }
            set { _logFile = value; }
        }

        //  Log message to file, rotate everyday
        public static void Log(string source, string message)
        {
            Log(string.Format("[{0}] {1}", source, message));
        }

        public static void Log(string source, Exception exp)
        {
            Log(string.Format("[{0}] {1}: {2}", source, exp.GetType().Name, exp.Message));
        }

        public static void Log(NotifyEventArgs arg)
        {
            Logger.Log(string.Format("[{0}] {1} : {2}", arg.Source, arg.Summary, arg.Message));
        }

        public static void Log(string message)
        {
            lock (_locker)
            {
                // No logFile defined, use  temporary file in %TEMP%
                if (_logFile == null)
                    Logger.LogFile = GetTempFilePath();

                if (File.Exists(_logFile))
                {
                    DateTime dt = File.GetLastAccessTime(_logFile);
                    if (dt.Date != DateTime.Now.Date)
                    {
                        // rename the file

                        string oldDate = dt.ToString("yyyyMMdd_HHmmss");
                        string oldName = Path.GetFileNameWithoutExtension(_logFile);
                        string newName = oldDate + "_" + oldName;  // without file extension

                        string oldNameFull = Path.GetFullPath(_logFile);
                        string newNameFull = Path.GetDirectoryName(_logFile)+ @"\" + newName + ".log";
                        try
                        {
                            File.Move(oldNameFull, newNameFull);
                        }
                        catch (Exception e)
                        {
                            using (StreamWriter SW = File.AppendText(GetTempFilePath()))
                            {
                                string msg = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + e.ToString();
                                SW.WriteLine(msg);
                            }
                        }
                    }
                }

                try
                {
                    using (StreamWriter SW = File.AppendText(_logFile))
                    {
                        string msg = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + message;
                        SW.WriteLine(msg);
                    }
                }
                catch (Exception ex)
                {
                    using (StreamWriter SW = File.AppendText( GetTempFilePath() ))
                    {
                        string msg = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + message;
                        SW.WriteLine(msg);
                    }
                }
            }
        }
    }
}
