#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2018  Jefri Sibarani
 
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
        private static string logFile;
        
        /// Init log file in temporary folder
        public Logger()
        {
            logFile = GetTempFilePath();
        }

        private static string GetTempFilePath()
        {
            string filename = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            string newFile = Path.GetTempPath() + filename + ".log";
            return newFile;
        }

        public static string LogFile
        {
            get { return logFile; }
            set { logFile = value; }
        }

        private static readonly object locker = new object();

        ///  Log message to file, create new file everyday
        public static void Log(string message)
        {
            lock (locker)
            {
                /// No logFile defined, use  temporary file in %TEMP%
                if (logFile == null)
                    Logger.LogFile = GetTempFilePath();

                if (File.Exists(logFile))
                {
                    DateTime dt = File.GetLastAccessTime(logFile);
                    if (dt.Date != DateTime.Now.Date)
                    {
                        /// rename filenya

                        string oldDate = dt.ToString("yyyyMMdd_HHmmss");
                        string oldName = Path.GetFileNameWithoutExtension(logFile);
                        string newName = oldDate + "_" + oldName;  // no extension here

                        string oldNameFull = Path.GetFullPath(logFile);
                        string newNameFull = Path.GetDirectoryName(logFile)+ @"\" + newName + ".log";
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
                    using (StreamWriter SW = File.AppendText(logFile))
                    {
                        string msg = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + message;
                        SW.WriteLine(msg);
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    using (StreamWriter SW = File.AppendText( GetTempFilePath() ))
                    {
                        string msg = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + message;
                        SW.WriteLine(msg);
                    }
                }
                catch (ArgumentException e)
                {
                    using (StreamWriter SW = File.AppendText(GetTempFilePath()))
                    {
                        string msg = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + message;
                        SW.WriteLine(msg);
                    }
                }
                catch (DirectoryNotFoundException e)
                {
                    using (StreamWriter SW = File.AppendText(GetTempFilePath()))
                    {
                        string msg = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + " : " + message;
                        SW.WriteLine(msg);
                    }
                }
            }
        }
    }
}
