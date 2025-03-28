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
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data.Common;
using System.Data;

namespace Tobasa
{
    class QueueServer : IDisposable
    {
        #region Member variables

        private bool disposed = false;
        private bool stopped = false;
        private TCPServer tcpsrv = null;
        private static Dictionary<int, Client> clients = new Dictionary<int, Client>();

        private SysHandler sysHandler = new SysHandler();
        private CallerHandler callerHandler = new CallerHandler();
        private TicketHandler ticketHandler = new TicketHandler();
        private DisplayHandler displayHandler = new DisplayHandler();
        #endregion

        #region Constructor

        public QueueServer()
        {
            Database.Me.Notified += new Action<NotifyEventArgs>(Database_Notified);
        }

        #endregion

        #region Destructor

        ~QueueServer()
        {
            Log("Server stopped");
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

        #endregion

        #region TCPServer and Database event handlers
        private void Database_Notified(NotifyEventArgs arg)
        {
            Logger.Log(arg);
        }

        private void TCPServer_Notified(NotifyEventArgs arg)
        {
            Logger.Log(arg);
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
            Log("Client ID: " + id + " closed, RemoteInfo: " + remoteInfo + ", Client left: " + clientCount);
        }

        private void TCPServer_IncomingConnection(IPEndPoint ep, out bool allow)
        {
            bool allowed = false;
            string reason = "";

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
                    allowed = QueueRepository.CheckIpAddress(ep.Address, out reason);
                else
                    allowed = true;
            }
 
            if (allowed)
            {
                Log("Allow connection from " + ep.Address.ToString());
                allow = allowed;
            }
            else
            {
                allow = false;
                Log($"Reject connection from {ep.Address.ToString()} :  {reason}" );
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
            Log("Client ID: " + id + " accepted, RemoteInfo: " + remoteInfo + ",  Total Client: " + clientCount);
        }

        private void TCPServer_ServerStarted(TCPServer srv)
        {
            Log("Tobasa Queue Server started");
        }

        private void TCPServer_DataReceived(DataReceivedEventArgs arg)
        {
            string stringMessage = Encoding.UTF8.GetString(arg.Data);
            if (stringMessage != null)
            {
                HandleMessage(arg, stringMessage);
                return;
            }
        }
        
        #endregion

        #region Starting and stopping

        public void Start()
        {
            string conString = "";
            var setting = Properties.Settings.Default;

            Database.Me.SetProvider(setting.ProviderType);
            if (Database.Me.ProviderType == DatabaseProviderType.SQLITE)
            {
                conString = Database.Me.GetConnectionString(
                                setting.ConnectionString_SQLITE,
                                setting.SecuritySalt,
                                setting.ConnectionStringPassword );
            }
            else if (Database.Me.ProviderType == DatabaseProviderType.MYSQL)
            {
                conString = Database.Me.GetConnectionString(
                                setting.ConnectionString_MYSQL,
                                setting.SecuritySalt,
                                setting.ConnectionStringPassword);
            }
            else if (Database.Me.ProviderType == DatabaseProviderType.MSSQL)
            {
                conString = Database.Me.GetConnectionString(
                                setting.ConnectionString_MSSQL,
                                setting.SecuritySalt,
                                setting.ConnectionStringPassword);
            }
            else if (Database.Me.ProviderType == DatabaseProviderType.PGSQL)
            {
                conString = Database.Me.GetConnectionString(
                                setting.ConnectionString_PGSQL,
                                setting.SecuritySalt,
                                setting.ConnectionStringPassword);
            }
            else
            {
                Log("Tobasa Queue Server could not be started due to unsupported database provider");
                return;
            }


            if (!Database.Me.Connect(conString)) 
            {
                return;
            }

            string msgDbInfo = Database.Me.DatabaseProviderTypeString() + " Database Version " + Database.Me.Connection.ServerVersion;
            Log("Connected to " + msgDbInfo);

            bool dbOk = DBMigration.InitializeDatabase();
            if (!dbOk) {
                return;
            }

            int tcpListenPort = setting.ListenPort;
            tcpsrv = new TCPServer(tcpListenPort);
            tcpsrv.Notified           += new Action<NotifyEventArgs>(TCPServer_Notified);
            tcpsrv.DataReceived       += new DataReceived(TCPServer_DataReceived);
            tcpsrv.ClientClosed       += new ClientClosed(TCPServer_ClientClosed);
            tcpsrv.ClientAccepted     += new ClientAccepted(TCPServer_ClientAccepted);
            tcpsrv.IncomingConnection += new IncomingConnection(TCPServer_IncomingConnection);
            tcpsrv.ServerStarted      += new ServerStarted(TCPServer_ServerStarted);

            if (!tcpsrv.Start()) {
                Log("Could not start Tobasa Queue Server");
            }
        }

        public void Stop()
        {
            if (stopped)
                return;

            tcpsrv.Stop();
            tcpsrv.Close();

            if (Database.Me.Connected) {
                Log("Could not close connection to database");
            }

            stopped = true;
        }

        #endregion

        #region Message handlers

        private void HandleMessage(DataReceivedEventArgs arg, string message)
        {
            Client client = FindClientByID(arg.SessionID);
            if (client == null)
                return;

            if (!client.LoggedIn)
            {
                if (message.StartsWith(Msg.SysLogin.Text + Msg.Separator + "REQ"))
                {
                    // Handle SysLogin
                    HandleLogin(arg, client);
                }
                else
                {
                    // Reject not logged in client!
                    // SYS|NOTIFY|[Type!Message]
                    string msg =
                        Msg.SysNotify.Text +
                        Msg.Separator + "ERROR" +
                        Msg.CompDelimiter + "Not logged in";

                    client.Session.Send(msg);
                    //client.Close();
                }

                return;
            }

            if (message.StartsWith("QUIT"))
            {
                client.Session.Send("QUIT");
                client.Close();
            }
            else if (message.StartsWith("SYS"))
            {
                sysHandler.OnMessage(arg, client);
            }
            else if (message.StartsWith("CALLER"))
            {
                callerHandler.OnMessage(arg, client);
            }
            else if (message.StartsWith("TICKET"))
            {
                ticketHandler.OnMessage(arg, client);
            }
            else if (message.StartsWith("DISPLAY"))
            {
                displayHandler.OnMessage(arg, client);
            }
            else 
            {
                // SYS|NOTIFY|[Type!Message]
                string msg =
                    Msg.SysNotify.Text +
                    Msg.Separator + "ERROR" +
                    Msg.CompDelimiter + "Invalid message";

                client.Session.Send(msg);
            }
        }

        private void HandleLogin(DataReceivedEventArgs arg, Client client)
        {
            Exception exp = null;
            try
            {
                Message qmessage = new Message(arg);
                Log("Processing " + qmessage.MessageType.String + " from " + arg.RemoteInfo);

                string module   = qmessage.PayloadValues["module"];
                string post     = qmessage.PayloadValues["post"];
                string station  = qmessage.PayloadValues["station"];
                string username = qmessage.PayloadValues["username"];
                string password = qmessage.PayloadValues["password"];

                if (!client.LoggedIn)
                {
                    client.Type = Client.ClientTypeFromString(module);
                    client.Name = station;
                    client.Post = post;
                    client.UserName = username;
                    client.Password = password;

                    bool allowed = false;
                    string reason = "Not Allowed";
                    if (QueueRepository.CanLogin(client.Name, client.Post, out reason))
                    {
                        if (QueueRepository.Login(client.UserName, client.Password, out reason))
                            allowed = true;
                    }

                    if (allowed)
                    {
                        client.LoggedIn = true;

                        Log("Logged on : " + module + " - " + client.Name + " - " + client.Post + " from: " + client.RemoteInfo);

                        // SYS|LOGIN|RES|[Result!Data]
                        string message =
                            Msg.SysLogin.Text +
                            Msg.Separator + "RES" +
                            Msg.Separator + "OK" +
                            Msg.CompDelimiter + "Identifier";

                        client.Session.Send(message);
                    }
                    else
                    {
                        // SYS|LOGIN|RES|[Result!Data]
                        string message =
                            Msg.SysLogin.Text +
                            Msg.Separator + "RES" +
                            Msg.Separator + "FAIL" +
                            Msg.CompDelimiter + reason;

                        client.Session.Send(message);
                        //client.Close();
                    }
                }
            }
            catch (AppException ex)
            {
                exp = ex;
            }
            catch (Exception ex)
            {
                exp = ex;
            }

            if (exp != null)
            {
                Log(exp.Message);
            }
        }

        #endregion

        #region Send message

        public static void SendMessageToQueueDisplay(string text, string post = "")
        {
            SendMessageToClient(ClientType.QueueDisplay, text, post);
        }

        public static void SendMessageToQueueCaller(string text, string post = "")
        {
            SendMessageToClient(ClientType.QueueCaller, text, post);
        }

        public static void SendMessageToClient(ClientType type, string text, string post = "")
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

    
        public static void Log(string message)
        {
            string datetimeIfo = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + " : ";

            if (Environment.UserInteractive)
                Console.WriteLine(datetimeIfo + message);

            Logger.Log("[QueueServer] " + message);
        }
    }
}
