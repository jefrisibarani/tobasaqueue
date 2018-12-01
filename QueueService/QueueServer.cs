#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2018  Jefri Sibarani

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
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data.OleDb;

namespace Tobasa
{
    class QueueServer : IDisposable
    {
        #region Member variables

        private bool disposed = false;
        private bool stopped = false;
        private TCPServer tcpsrv = null;
        private Dictionary<int, Client> clients = new Dictionary<int, Client>();

        #endregion

        #region Constructor

        public QueueServer()
        {
        }

        #endregion

        #region Destructor

        ~QueueServer()
        {
            if (Environment.UserInteractive)
                Console.WriteLine("\nServer stopped...");

            Logger.Log("QueueService : Server stopped");
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                clients.Clear();

                // Free any other managed objects here. 
                this.Stop();
                if (tcpsrv != null)
                    tcpsrv.Dispose();
            }

            // Free any unmanaged objects here. 
             disposed = true;
        }

        #endregion

        #region Misc methods

        private Client FindClientBySession(NetSession ses)
        {
            foreach (KeyValuePair<int, Client> kv in clients)
            {
                Client c = kv.Value;
                if (c.Session.Id == ses.Id)
                {
                    return c;
                }
            }
            return null;
        }

        private Client FindClientByID(int id)
        {
            foreach (KeyValuePair<int, Client> kv in clients)
            {
                Client c = kv.Value;
                if (c.Session.Id == id)
                {
                    return c;
                }
            }
            return null;
        }

        private string GetConnectionString()
        {
            string partialConnStr = Properties.Settings.Default.ConnectionString;
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            builder.ConnectionString = partialConnStr;

            string encryptedPwd = Properties.Settings.Default.ConnectionStringPassword;
            string salt = Properties.Settings.Default.SecuritySalt;
            string clearPwd = Util.DecryptPassword(encryptedPwd, salt);

            builder.Add("Password", clearPwd);
            
            return builder.ToString();
        }

        private bool CanLogin(string staName, string staPost, out string reason)
        {
            bool canLogin = false;
            string _reason = "Station is not registered in database";

            if (Database.Me.Connected)
            {
                string sql = "SELECT canlogin FROM stations WHERE name = ? AND post = ?";
                try
                {
                    Database.Me.OpenConnection();

                    using (OleDbCommand cmd = new OleDbCommand(sql, ((OleDbConnection)Database.Me.Connection)))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = staName;
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = staPost;

                        var res = cmd.ExecuteScalar();
                        if (res != null)
                        {
                            canLogin = Convert.ToBoolean(res);
                            if (canLogin)
                                _reason = "OK";
                            else
                                _reason = "Station is not allowed to login to server";
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueService : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueService : Exception : " + e.Message);
                }
            }
            
            reason = _reason;
            return canLogin;
        }

        private bool Login(string userName, string password, out string reason)
        {
            bool ok = false;
            string _reason = "User is not registered in database";

            if (Database.Me.Connected)
            {
                string sql = "SELECT username,password,expired,active FROM logins WHERE username = ?";
                try
                {
                    Database.Me.OpenConnection();

                    using (OleDbCommand cmd = new OleDbCommand(sql, ((OleDbConnection)Database.Me.Connection)))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 50).Value = userName;

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                string _username = reader.GetString(0).Trim();
                                string _password = reader.GetString(1).Trim();
                                DateTime _expired = reader.GetDateTime(2);
                                bool _active = reader.GetBoolean(3);

                                if (!_active)
                                {
                                    _reason = "Inactive user";
                                    ok = false;
                                }
                                else if (DateTime.Now > _expired)
                                {
                                    _reason = "User expired";
                                    ok = false;
                                }
                                else if (password != _password)
                                {
                                    _reason = "Wrong password";
                                    ok = false;
                                }
                                else if (password == _password)
                                {
                                    _reason = "Succesfully Logged in";
                                    ok = true;
                                }
                            }
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueService : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueService : Exception : " + e.Message);
                }
            }

            reason = _reason;
            return ok;
        }

        private string GetPostNumberPrefix(string staPost)
        {
            string postNumberPrefix = "-";

            if (Database.Me.Connected)
            {
                string sql = "SELECT numberprefix FROM posts WHERE name = ?";

                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, ((OleDbConnection)Database.Me.Connection)))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = staPost;
                        var res = cmd.ExecuteScalar();
                        if (res != null)
                        {
                            if (res.ToString().Length <= 0)
                                postNumberPrefix = "";
                            else
                            {
                                postNumberPrefix = res.ToString();
                                postNumberPrefix = postNumberPrefix.Trim();
                            }
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueService : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueService : Exception : " + e.Message);
                }
            }

            return postNumberPrefix;
        }

        private bool CheckIpAddress(IPAddress addr)
        {
            if (Database.Me.Connected)
            {
                string strIp = addr.ToString();

                string sql = "SELECT allowed FROM ipaccesslists WHERE ipaddress = ?";
                try
                {
                    Database.Me.OpenConnection();

                    using (OleDbCommand cmdSelect = new OleDbCommand(sql, ((OleDbConnection)Database.Me.Connection)))
                    {
                        cmdSelect.Parameters.Add("?", OleDbType.VarChar, 15).Value = strIp;
                        var res = cmdSelect.ExecuteScalar();
                        if (res != null)
                        {
                            return Convert.ToBoolean(res);
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueService : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueService : Exception : " + e.Message);
                }
            }
            return false;
        }

        #endregion

        #region TCPServer event handlers

        private void TCPServer_Notified(NotifyEventArgs e)
        {
            Logger.Log(e.Source + " : " + e.Summary + " : " + e.Message);
        }

        private void TCPServer_ClientClosed(NetSession ses)
        {
            string remoteInfo = ses.RemoteInfo;
            string id = ses.Id.ToString();

            lock (clients)
            {
                clients.Remove(ses.Id);
            }

            string clientCount = clients.Count.ToString();
            Logger.Log("QueueService : Client Closed ID: " + id + " RemoteInfo: " + remoteInfo + " Client left = " + clientCount);
        }

        private void TCPServer_IncomingConnection(IPEndPoint ep, out bool allow)
        {
            bool allowed = false;

            IPAddress lochost;
            IPAddress.TryParse("127.0.0.1", out lochost);
            // we always allow connection from localhost 
            if (lochost.Equals(ep.Address))
            {
                allowed = true;
            }
            else
            {
                if (Properties.Settings.Default.FilterClientIPAddress)
                    allowed = CheckIpAddress(ep.Address);
                else
                    allowed = true;
            }
 
            if (allowed)
            {
                Logger.Log("QueueService : Connection allowed from " + ep.Address.ToString());
                allow = allowed;
            }
            else
            {
                allow = false;
                Logger.Log("QueueService : Connection rejected from " + ep.Address.ToString());
            }
        }

        private void TCPServer_ClientAccepted(NetSession ses)
        {
            string remoteInfo = ses.RemoteInfo;
            string id = ses.Id.ToString();
            Client client = new Client(ses);

            lock (clients)
            {
                clients.Add(ses.Id, client);
            }

            string clientCount = clients.Count.ToString();
            Logger.Log("QueueService : Client Accepted ID: " + id + " RemoteInfo: " + remoteInfo + "  Total Client = " + clientCount);
        }

        private void TCPServer_ServerStarted(TCPServer srv)
        {
            if (Environment.UserInteractive)
                Console.WriteLine("\nServer started...");

            Logger.Log("QueueService : Server started");
        }

        private void TCPServer_DataReceived(DataReceivedEventArgs arg)
        {
            /*
            // Deserialize the message
            object message = Message.Deserialize(arg.Data);

            // Handle the message
            StringMessage stringMessage = message as StringMessage;
            if (stringMessage != null)
            {
                ProcessMessage(arg, stringMessage.Message);
                return;
            }

            ComplexMessage complexMessage = message as ComplexMessage;
            if (complexMessage != null)
            {
                string msg = "Socket read got a complex message: (UniqueID = " + complexMessage.UniqueID.ToString() +
                              ", Time = " + complexMessage.Time.ToString() + ", Message = " + complexMessage.Message + ")" + Environment.NewLine;
                Logger.Log(msg);
                return;
            }
            */
            string stringMessage = Encoding.UTF8.GetString(arg.Data);
            if (stringMessage != null)
            {
                ProcessMessage(arg, stringMessage);
                return;
            }
        }
        
        #endregion

        #region Starting and stopping

        public void Start()
        {
            Database.Me.Connect(GetConnectionString());

            int tcpListenPort = Properties.Settings.Default.ListenPort;
            tcpsrv = new TCPServer(tcpListenPort);
            tcpsrv.Notified           += new Action<NotifyEventArgs>(TCPServer_Notified);
            tcpsrv.DataReceived       += new DataReceived(TCPServer_DataReceived);
            tcpsrv.ClientClosed       += new ClientClosed(TCPServer_ClientClosed);
            tcpsrv.ClientAccepted     += new ClientAccepted(TCPServer_ClientAccepted);
            tcpsrv.IncomingConnection += new IncomingConnection(TCPServer_IncomingConnection);
            tcpsrv.ServerStarted      += new ServerStarted(TCPServer_ServerStarted);

            if (!tcpsrv.Start())
            {
                if (Environment.UserInteractive)
                    Console.WriteLine("\nCould not start Tobasa Queue Server");

                Logger.Log("QueueService : Could not start Tobasa Queue Server");
            }
        }

        public void Stop()
        {
            if (stopped)
                return;

            tcpsrv.Stop();
            tcpsrv.Close();

            if (Database.Me.Connected)
            {
                if (Database.Me.Disconnect())
                    Logger.Log("Database disconnected...");
                else
                    Logger.Log("Could not close connection to database");
            }
            stopped = true;
        }
        
        #endregion

        #region Message handlers

        private void ProcessMessage(DataReceivedEventArgs arg, string message)
        {
            Client cl = FindClientByID(arg.SessionID);
            if (cl != null)
            {
                if (!cl.LoggedIn)
                {
                    if (message.StartsWith("LOGIN"))
                        ProcessLogin(arg, message);
                    else
                        cl.Session.Send("You have to Login first");

                    return;
                }
            }
            else
                return;

            if (message.StartsWith("QUIT"))
            {
                cl.Session.Send("QUIT");
                cl.Close();
            }
            else if (message.StartsWith("LOGIN"))
            {
                /****************************************************
                 Summary     : Login ke Queue Service
                 Syntax      : LOGIN|Client_Type|Station|Post|UserName|Password
                 Client_Type : 
                     CALLER  : Aplikasi di counter, memanggil nomor
                     DISPLAY : Aplikasi menampilkan daftar antrian,running text
                     TICKET  : Aplikasi membuat nomor ticket
                     
                     Station : Nama Client - tanpa underscore, eg: DISP#1, CALL#2, TICKET#1
                        Post : Nama Post - tanpa underscore,   eg: FARMASI, RADIOLOGI
                 
                 message ini diissue oleh CALLER,DISPLAY,TICKET
                 sample LOGIN|CALLER|CALL#1|APOTIK , LOGIN|DISPLAY|DISP#1|APOTIK , LOGIN|TICKET|TICKET#1|APOTIK
                ****************************************************/

                cl.Session.Send("Already logged in");
            }
            else if (message.StartsWith(Msg.TICKET_CREATE_NEWNUMBER))
            {
                /****************************************************
                Summary           : Buar nomor antrian baru untuk pos tujuan
                Syntax            : TICKET|CREATE_NEWNUMBER|Station|Post
                          Station : Nama Client - tanpa underscore, eg: DISP#1, CALL#2, TICKET#1
                             Post : Nama Post - tanpa underscore,   eg: FARMASI, RADIOLOGI
                    Response sent : TICKET|SET_NEWNUMBER|Prefix|NewNumber|Post|Timestamp
                        Timestamp : Waktu tercatat nomor di server, eg: 22 November 2013, 13:21
                 *
                message ini diissue oleh TICKET
                sample : TICKET|CREATE_NEWNUMBER|TICKET#1|APOTIK
                ****************************************************/
                ProcessCreateNewNumber(arg, message);
            }
            else if (message.StartsWith(Msg.DISPLAY_SET_RUNNINGTEXT))
            {
                /****************************************************
                Summary           : Set message running text pada Queue display
                Syntax            : DISPLAY|SET_RUNNINGTEXT|Station|Post|RunningText
                      RunningText : Text to be displayed
                 
                message ini diissue oleh CALLER
                sample : DISPLAY|SET_RUNNINGTEXT|CALL#1|APOTIK|Selamat Datang
                ****************************************************/
                ProcessSetRunningText(arg, message);
            }
            else if (message == Msg.DISPLAY_GET_RUNNINGTEXT )
            {
                /****************************************************
                Summary           : Get message running text pada Queue display
                Syntax            : DISPLAY|GET_RUNNINGTEXT|Station|Post
                 
                message ini diissue oleh DISPLAY
                sample : DISPLAY|GET_RUNNINGTEXT|DISP#1|APOTIK
                ****************************************************/
                ProcessGetRunningText(arg, message);
            }
            else if (message.StartsWith(Msg.DISPLAY_RESET_RUNNINGTEXT))
            {
                /****************************************************
                Summary           : Reet message running text pada Queue display
                Syntax            : DISPLAY|RESET_RUNNINGTEXT|Station|Post
                
                message ini diissue oleh CALLER
                sample : DISPLAY|RESET_RUNNINGTEXT|CALL#1|APOTIK
                ****************************************************/
                ProcessResetRunningText(arg, message);
            }
            else if (message.StartsWith(Msg.DISPLAY_DELETE_RUNNINGTEXT))
            {
                /****************************************************
                Summary           : Delete message running text pada Queue display
                Syntax            : DISPLAY|DELETE_RUNNINGTEXT|Station|Post|RunningText
                      RunningText : Text to be deleted
                 
                message ini diissue oleh CALLER
                sample : DISPLAY|DELETE_RUNNINGTEXT|CALL#1|APOTIK|Selamat Datang
                ****************************************************/
                ProcessDeleteRunningText(arg, message);
            }
            else if (message.StartsWith(Msg.DISPLAY_RESET_VALUES))
            {
                /****************************************************
                Summary           : Reser queue numbers and counter number on display
                Syntax            : DISPLAY|RESET_VALUES|Station|Post
                 
                message ini diissue oleh CALLER
                sample : DISPLAY|RESET_VALUES|CALL#1|APOTIK
                ****************************************************/
                ProcessDisplayResetValues(arg, message);
            }
            else if (message.StartsWith(Msg.CALLER_GET_NEXTNUMBER))
            {
                /****************************************************
                Summary           : Ambil nomor antrian berikutnya
                Syntax            : CALLER|GET_NEXTNUMBER|Station|Post
                          Station : Nama Client - tanpa underscore, eg: DISP#1, CALL#2, TICKET#1
                             Post : Nama Post - tanpa underscore,   eg: FARMASI, RADIOLOGI
                Response sent     : CALLER|SET_NEXTNUMBER|Prefix|Number|TotalQueue
                                    CALLER|SET_NEXTNUMBER|NULL                      ==> Jika tidak ada antrian
                                    DISPLAY|CALL_NUMBER|Prefix|Number|QueueTotal|Station|Post

                message ini diissue oleh CALLER
                sample : CALLER|GET_NEXTNUMBER|CALL#2|FARMASI
                ****************************************************/
                ProcessCallGetNext(arg, message);
            }
            else if (message.StartsWith(Msg.CALLER_RECALL_NUMBER))
            {
                /****************************************************
                Summary           : Panggil ulang nomor antrian
                Syntax            : CALLER|RECALL_NUMBER|Number|Station|Post
                           Number : Nomor yang akan dipanggil ulang
                          Station : Nama Client - tanpa underscore, eg: DISP#1, CALL#2, TICKET#1
                             Post : Nama Post - tanpa underscore,   eg: FARMASI, RADIOLOGI
                Response sent     : DISPLAY|RECALL_NUMBER|Prefix|Number|XXX|Station|Post

                message ini diissue oleh CALLER
                sample : CALLER|RECALL_NUMBER|20|CALL#1|APOTIK
                ****************************************************/

                ProcessRecallNumber(arg, message);
            }
            else if (message.StartsWith(Msg.DISPLAY_SHOW_MESSAGE))
            {
                /****************************************************
                Summary           : Tampilkan pesan pada Label Top Display, dibawah informasi Jam
                Syntax            : DIPLAY|SHOW_MESSAGE|Station|Post|Message
               
                          Station : Nama Client - tanpa underscore, eg: DISP#1, CALL#2, TICKET#1
                             Post : Nama Post - tanpa underscore, eg: FARMASI, RADIOLOGI

                message ini diissue oleh CALLER, diforward ke display
                sample : DIPLAY|SHOW_MESSAGE|CALL#2|APOTIK|Antrian No. 2, Resep telah selesai
                ****************************************************/

                ProcessCallerDisplayMessage(arg, message);
            }
            else if (message.StartsWith(Msg.DISPLAY_UPDATE_JOB))
            {
                /****************************************************
                Summary           : Update nomor job/antrian yang telah selesai di proses
                Syntax            : DISPLAY|UPDATE_JOB|Station|Post|Message
               
                          Station : Nama Client - tanpa underscore, eg: CALL#2
                             Post : Nama Post - tanpa underscore, eg: FARMASI, RADIOLOGI
                          Message : Daftar nomor antrian yang telah selesai diproses

                message ini diissue oleh CALLER, diforward ke display sesuai dengan POST nya
                sample : DISPLAY|UPDATE_JOB|CALL#2|APOTIK|A1,A2,A3,A4,A5,A8,A9,A10,A11,A12
                ****************************************************/

                ProcessCallerUpdateJob(arg, message);
            }
            else
                cl.Session.Send("I don't understand");
        }

        private void ProcessSetRunningText(DataReceivedEventArgs arg, string text)
        {
            string _station, _post;
            _station = _post = "";

            if (text.StartsWith(Msg.DISPLAY_SET_RUNNINGTEXT))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                if (words.Length == 5)
                {
                    _station = words[2];
                    _post = words[3];
                    
                    SendMessageToQueueDisplay(text, _post);
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid DISPLAY:SET_RUNNINGTEXT from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
        }

        private void ProcessGetRunningText(DataReceivedEventArgs arg, string text)
        {
            /* DISPLAY|GET_RUNNINGTEXT */

            Client cl = FindClientByID(arg.SessionID);
            if (cl != null && cl.LoggedIn)
            {
                string _station, _post;
                _station = _post = "";

                _station = cl.Name;
                _post = cl.Post;

                if (Database.Me.Connected)
                {
                    try
                    {
                        Database.Me.OpenConnection();

                        string sql = @"SELECT station_name,sticky,active,running_text FROM runningtexts WHERE active=1 AND station_name=?";
                        using (OleDbCommand cmdSelect = new OleDbCommand(sql, ((OleDbConnection)Database.Me.Connection)))
                        {
                            cmdSelect.Parameters.Add("?", OleDbType.VarChar, 32).Value = _station;
                            using (OleDbDataReader reader = cmdSelect.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //var _id = reader.GetValue(0);
                                    string _runnningText = reader.GetString(3).Trim();
                                    string msg ="DISPLAY" + Msg.Separator + "SET_RUNNINGTEXT" + Msg.Separator + _station + Msg.Separator + _post + Msg.Separator + _runnningText;
                                    cl.Session.Send(msg);
                                }
                            }
                        }
                    }
                    catch (ArgumentException e)
                    {
                        Logger.Log("QueueService : ArgumentException : " + e.Message);
                    }
                    catch (Exception e)
                    {
                        Logger.Log("QueueService : Exception : " + e.Message);
                    }
                }
            }
        }
        
        private void ProcessResetRunningText(DataReceivedEventArgs arg, string text)
        {
            string _station, _post;
            _station = _post = "";

            if (text.StartsWith(Msg.DISPLAY_RESET_RUNNINGTEXT))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                if (words.Length == 4)
                {
                    _station = words[2];
                    _post = words[3];
                    
                    SendMessageToQueueDisplay(text, _post);
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid DISPLAY:RESET_RUNNINGTEXT from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
        }

        private void ProcessDeleteRunningText(DataReceivedEventArgs arg, string text)
        {
            string _station, _post;
            _station = _post = "";

            if (text.StartsWith(Msg.DISPLAY_DELETE_RUNNINGTEXT))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                if (words.Length == 5)
                {
                    _station = words[2];
                    _post = words[3];
                    
                    SendMessageToQueueDisplay(text, _post);  
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid DISPLAY:DELETE_RUNNINGTEXT from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
        }

        private void ProcessDisplayResetValues(DataReceivedEventArgs arg, string text)
        {
            string _station, _post;
            _station = _post = "";

            if (text.StartsWith(Msg.DISPLAY_RESET_VALUES))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                if (words.Length == 4)
                {
                    _station = words[2];
                    _post = words[3];
                    
                    SendMessageToQueueDisplay(text, _post);
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid DISPLAY:RESET_VALUES from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
        }

        private void ProcessLogin(DataReceivedEventArgs arg, string text)
        {
            foreach (KeyValuePair<int, Client> kv in clients)
            {
                Client c = kv.Value;
                if (c.Session.Id == arg.SessionID)
                {
                    string typ = String.Empty;

                    if (text.StartsWith("LOGIN"))
                    {
                        string[] words = text.Split(Msg.Separator.ToCharArray());

                        // LOGIN has 6 tokens
                        if (words.Length == 6)
                        {
                            typ = words[1];
                            c.Type = Client.ClientTypeFromString(typ);
                            c.Name = words[2];
                            c.Post = words[3];
                            c.UserName = words[4];
                            c.Password = words[5];

                            bool allowed = false;
                            string reason = "Not Allowed";
                            if (CanLogin(c.Name, c.Post, out reason))
                            {
                                if (Login(c.UserName, c.Password, out reason) )
                                    allowed = true;
                            }

                            if (allowed)
                            {
                                c.LoggedIn = true;
                                c.Session.Send("LOGIN" + Msg.Separator + "OK");
                                Logger.Log("QueueService : Logged on : " + typ + " - " + c.Name + " - " + c.Post + " From: " + arg.RemoteInfo);
                            }
                            else
                            {
                                c.Session.Send("LOGIN" + Msg.Separator + "BAD" + Msg.Separator + reason);
                                c.Close();
                            }

                        }
                        else
                        {
                            string logmsg = String.Format("QueueService : Invalid LOGIN from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                            Logger.Log(logmsg);
                        }
                    }
                    break;
                }
            }
        }

        private void ProcessCreateNewNumber(DataReceivedEventArgs arg, string text)
        {
            Client cl = FindClientByID(arg.SessionID);
            if (cl == null)
                return;

            bool data_ok = false;
            string post = String.Empty;
            string station = String.Empty;

            if (text.StartsWith(Msg.TICKET_CREATE_NEWNUMBER))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                // TICKET|CREATE_NEWNUMBER has 5 tokens
                if (words.Length == 4)
                {
                    station = words[2];
                    post = words[3];
                    data_ok = true;
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid TICKET:CREATE_NEWNUMBER from: {0} - TEXT: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }

            if (Database.Me.Connected && data_ok)
            {
                string sql = @"SELECT number+1 from sequences WHERE post = ? AND date = (SELECT CAST(getdate() AS date)) 
                             AND id = (SELECT MAX(id) from sequences WHERE post = ? AND date = (SELECT CAST(getdate() AS date)))"; 
                try
                {
                    Database.Me.OpenConnection();

                    using (OleDbCommand cmd = new OleDbCommand(sql, ((OleDbConnection)Database.Me.Connection)))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = post;
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = post;

                        var res = cmd.ExecuteScalar();

                        string dbNumber = "";
                        if (res != null)
                        {
                            dbNumber = res.ToString();
                        }
                        
                        // Get Post Prefix number
                        string postNumberPrefix = GetPostNumberPrefix(post);

                        string insertSQL = "INSERT INTO sequences (number,post,source) OUTPUT INSERTED.number,INSERTED.starttime,INSERTED.id VALUES (?, ?, ?)";
                        using (OleDbCommand cmdInsert = new OleDbCommand(insertSQL, ((OleDbConnection)Database.Me.Connection)))
                        {
                            int number = 0;

                            if (Int32.TryParse(dbNumber, out number))
                            {
                                if (number == Properties.Settings.Default.MaxQueueNumber)
                                {
                                    // max queue number reached, reset back to 1
                                    cmdInsert.Parameters.Add("?", OleDbType.Integer).Value = 1;
                                }
                                else
                                    cmdInsert.Parameters.Add("?", OleDbType.Integer).Value = number;
                            }
                            else
                            {
                                //Create initial number "1" if table has no number
                                cmdInsert.Parameters.Add("?", OleDbType.Integer).Value = 1;
                            }

                            cmdInsert.Parameters.Add("?", OleDbType.VarChar, 32).Value = post;
                            cmdInsert.Parameters.Add("?", OleDbType.VarChar, 32).Value = station;

                            using (OleDbDataReader reader = cmdInsert.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();

                                    var _number = reader.GetValue(0);
                                    var _starttime = reader.GetValue(1);
                                    var _id = reader.GetInt32(2);

                                    if (_number != null && _starttime != null && _id != 0 )
                                    {
                                          // Copy data to jobs table
                                        string insertJobSQL = "INSERT INTO jobs SELECT * FROM sequences WHERE id = ?";
                                        using (OleDbCommand cmdInsertJob = new OleDbCommand(insertJobSQL, ((OleDbConnection)Database.Me.Connection)))
                                        {
                                            cmdInsertJob.Parameters.Add("?", OleDbType.Integer).Value = (Int32)_id;
                                            cmdInsertJob.ExecuteNonQuery();
                                        }

                                        // Prepare data to be sent out to Ticket
                                        string numberStr = _number.ToString();
                                        DateTime timestamp = (DateTime)_starttime;
                                        string timestampStr = timestamp.ToString("dd MMMM yyyy - HH:mm");
                                        string message = "TICKET" + Msg.Separator + "SET_NEWNUMBER" + Msg.Separator + postNumberPrefix + Msg.Separator + numberStr + Msg.Separator + post + Msg.Separator + timestampStr;
                                        // Send response to caller, to process the number
                                        cl.Session.Send(message);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueService : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueService : Exception : " + e.Message);
                }
            }

        }

        private void ProcessCallGetNext(DataReceivedEventArgs arg, string text)
        {
            Client cl = FindClientByID(arg.SessionID);
            if (cl == null)
                return;

            bool data_ok = false;
            string _station, _post, sql;
            _station = _post = sql = "";

            if (text.StartsWith(Msg.CALLER_GET_NEXTNUMBER))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                // CALLER|GET_NEXTNUMBER has 5 tokens
                if (words.Length == 4)
                {
                    _station = words[2];
                    _post = words[3];
                    data_ok = true;
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid CALLER:GET_NEXTNUMBER from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }


            // ambil daftar nomor antrian yang belum dilayani, dan berikan pada caller nomor yang paling awal
            // update tampilan Queue display, nomor dilayani di pos CALLER ID yang memanggil
            /*
            sql = @"SELECT id,number,status,station,post,source,starttime,endtime
                   ,( SELECT COUNT(number) FROM sequences WHERE status = 'WAITING' AND post = ? AND [date] = (SELECT CAST(getdate() AS date)) ) AS numberleft
                   ,( SELECT MAX(number)   FROM sequences WHERE status = 'WAITING' AND post = ? AND [date] = (SELECT CAST(getdate() AS date)) ) AS numbermax
                   FROM sequences 
                   WHERE status = 'WAITING' AND post = ? AND [date] = (SELECT CAST(getdate() AS date))
                   AND id = (SELECT MIN(id) FROM sequences WHERE status = 'WAITING' AND post = ? AND [date] = (SELECT CAST(getdate() AS date)) ) ";
            */
            sql = @"SELECT id,number,status,station,post,source,starttime,endtime,numberleft,numbermax FROM v_sequences WHERE post = ?";
            
            if (Database.Me.Connected && data_ok)
            {
                try
                {
                    Database.Me.OpenConnection();

                    using (OleDbCommand cmdSelect = new OleDbCommand(sql, ((OleDbConnection)Database.Me.Connection)))
                    {
                        cmdSelect.Parameters.Add("?", OleDbType.VarChar, 32).Value = _post;

                        using (OleDbDataReader reader = cmdSelect.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();

                                var _id = reader.GetValue(0);
                                var _number = reader.GetValue(1);
                                var _starttime = reader.GetValue(6);
                                var _numCount = reader.GetValue(8);

                                // Get Post Prefix Number
                                string postNumberPrefix = GetPostNumberPrefix(_post);

                                string message = String.Empty;
                                message = "CALLER" + Msg.Separator + "SET_NEXTNUMBER" + Msg.Separator + postNumberPrefix + Msg.Separator + _number.ToString() + Msg.Separator + _numCount.ToString();

                                // Send response to caller, to process the number
                                cl.Session.Send(message);

                                // Send message to Queue display, to update displayed number, and total queue
                                string message1 = String.Empty;
                                message1 = "DISPLAY" + Msg.Separator + "CALL_NUMBER" + Msg.Separator + postNumberPrefix + Msg.Separator + _number.ToString() + Msg.Separator + _numCount.ToString() + Msg.Separator + _station + Msg.Separator + _post;
                                SendMessageToQueueDisplay(message1, _post);

                                // Update database
                                string sqlUpdate = "";
                                sqlUpdate = @"UPDATE sequences SET [status] = 'PROCESS',station = ?,calltime=getdate()
                                              WHERE id = ? AND number = ? AND post = ?";

                                using (OleDbCommand cmdInsert = new OleDbCommand(sqlUpdate, ((OleDbConnection)Database.Me.Connection)))
                                {
                                    cmdInsert.Parameters.Add("?", OleDbType.VarChar, 32).Value = _station;
                                    cmdInsert.Parameters.Add("?", OleDbType.Integer).Value = (Int32)_id;
                                    cmdInsert.Parameters.Add("?", OleDbType.Integer).Value = (Int32)_number;
                                    cmdInsert.Parameters.Add("?", OleDbType.VarChar, 32).Value = _post;

                                    cmdInsert.ExecuteNonQuery();
                                }

                                // Now we want to delete processed number
                                string sqlDelete = "";

                                if ((Int32)_number == (Properties.Settings.Default.MaxQueueNumber - 1))
                                {
                                    // max_number-1 reached, delete all processed number for this post
                                    sqlDelete = @"DELETE FROM sequences WHERE [status] = 'PROCESS' AND number < ? AND post = ?";

                                    using (OleDbCommand cmdDelete = new OleDbCommand(sqlDelete, ((OleDbConnection)Database.Me.Connection)))
                                    {
                                        cmdDelete.Parameters.Add("?", OleDbType.Integer).Value = Properties.Settings.Default.MaxQueueNumber;
                                        cmdDelete.Parameters.Add("?", OleDbType.VarChar, 32).Value = _post;
                                        cmdDelete.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // delete only processed numbers from specific station
                                    sqlDelete = @"DELETE FROM sequences WHERE [status] = 'PROCESS' AND number < ? AND post = ? AND station = ?";
                                    using (OleDbCommand cmdDelete = new OleDbCommand(sqlDelete, ((OleDbConnection)Database.Me.Connection)))
                                    {
                                        cmdDelete.Parameters.Add("?", OleDbType.Integer).Value = (Int32)_number;
                                        cmdDelete.Parameters.Add("?", OleDbType.VarChar, 32).Value = _post;
                                        cmdDelete.Parameters.Add("?", OleDbType.VarChar, 32).Value = _station;
                                        cmdDelete.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                string message = String.Empty;
                                message = "CALLER" + Msg.Separator + "SET_NEXTNUMBER" + Msg.Separator + "NULL";

                                // Send response to caller, to process the number
                                cl.Session.Send(message);
                            }
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Log("QueueService : ArgumentException : " + e.Message);
                }
                catch (Exception e)
                {
                    Logger.Log("QueueService : Exception : " + e.Message);
                }
            }

        }

        private void ProcessRecallNumber(DataReceivedEventArgs arg, string text)
        {
            string _number, _station, _post;
            _number = _station = _post = "";

            if (text.StartsWith(Msg.CALLER_RECALL_NUMBER))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                if (words.Length == 5)
                {
                    _number = words[2];
                    _station = words[3];
                    _post = words[4];

                    // Get Post Prefix Number
                    string postNumberPrefix = GetPostNumberPrefix(_post);

                    // Send message to Queue display, to update displayed number
                    // XXX is sent to replace total waiting queue 
                    string message = "DISPLAY" + Msg.Separator + "RECALL_NUMBER" + Msg.Separator + postNumberPrefix + Msg.Separator + _number + Msg.Separator + "XXX" + Msg.Separator +_station + Msg.Separator + _post;
                    SendMessageToQueueDisplay(message, _post);
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid CALLER:RECALL_NUMBER from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
        }

        private void ProcessCallerDisplayMessage(DataReceivedEventArgs arg, string text)
        {
            string _station, _post;
            _station = _post = "";

            if (text.StartsWith(Msg.DISPLAY_SHOW_MESSAGE))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                // DISPLAY_SHOW_MESSAGE has 5 tokens
                if (words.Length == 5)
                {
                    _station = words[2];
                    _post = words[3];

                    // Send message to Queue display, to update displayed number
                    SendMessageToQueueDisplay(text, _post);
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid DISPLAY:SHOW_MESSAGE from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }
        }

        private void ProcessCallerUpdateJob(DataReceivedEventArgs arg, string text)
        {
            string _station, _post;
            _station = _post = "";

            if (text.StartsWith(Msg.DISPLAY_UPDATE_JOB))
            {
                string[] words = text.Split(Msg.Separator.ToCharArray());

                // DISPLAY_UPDATE_JOB has 5 tokens
                if (words.Length == 5)
                {
                    _station = words[2];
                    _post = words[3];

                    // Send message to Queue display, to update displayed finished job/antrian
                    SendMessageToQueueDisplay(text, _post);
                }
                else
                {
                    string logmsg = String.Format("QueueService : Invalid DISPLAY:UPDATEJOB from: {0} - MSG: {1} ", arg.RemoteInfo, text);
                    Logger.Log(logmsg);
                    return;
                }
            }

        }
        #endregion

        #region Send message
        
        private void SendMessageToQueueDisplay(string text, string post = "")
        {
            SendMessageToClient(ClientType.QueueDisplay, text, post);
        }

        private void SendMessageToClient(ClientType type, string text, string post = "")
        {
            foreach (KeyValuePair<int, Client> kv in clients)
            {
                Client c = kv.Value;
                if (c.Type == type )
                {
                    if (c.Post == post || c.Post == "ANY" || c.ReceiveMessageFromOtherPost)
                        c.Session.Send(text);
                }
            }
        }
        
        #endregion

    }
}
