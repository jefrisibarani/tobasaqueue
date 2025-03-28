#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2015-2025  Jefri Sibarani
 
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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Tobasa
{
    #region Delegates

    public delegate void ClientClosed(NetSession ses);
    public delegate void ClientAccepted(NetSession ses);
    public delegate void IncomingConnection(IPEndPoint ep, out bool allow);
    public delegate void ServerStarted(TCPServer srv);
    
    #endregion

    public class TCPServer : Notifier, IDisposable
    {
        #region Member variables

        private bool disposed = false;
        private bool closed = false;
        private bool socketClosed = false;
        private bool shuttingDown = false;

        private Socket sock = null;
        private int tcpport;
        public static int sessionId = 100;

        public event ClientClosed ClientClosed;
        public event ClientAccepted ClientAccepted;
        public event IncomingConnection IncomingConnection;
        public event ServerStarted ServerStarted;
        public event DataReceived DataReceived;
        
        public static ManualResetEvent allDone = new ManualResetEvent(false);        
        private Dictionary<int, NetSession> sessions = new Dictionary<int, NetSession>();
        
        #endregion

        #region Constructor

        public TCPServer(int port)
        {
            tcpport = port;
        }

        #endregion

        #region Destructor

        ~TCPServer()
        {
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
                ClientClosed = null;
                ClientAccepted = null;
                IncomingConnection = null;
                ServerStarted = null;
                DataReceived = null;

                // Free any other managed objects here. 
            }

            // Free any unmanaged objects here. 
            Stop();
            Close();
            disposed = true;
        }

        #endregion

        #region Misc methods

        private int NewSessionId()
        {
            sessionId++;
            return sessionId;
        }

        public void BroadcastMessage(string data)
        {
            lock (sessions)
            {
                foreach (KeyValuePair<int, NetSession> kv in sessions)
                {
                    kv.Value.Send(data);
                }
            }
        }
        
        #endregion

        #region Connection and Listening

        private IPEndPoint GetLocalEndPoint()
        {
            // Establish the local endpoint for the socket.
            IPEndPoint localEndPoint=null;

            try
            {
                if (tcpport > 1024 && tcpport < 65536)
                    localEndPoint = new IPEndPoint(IPAddress.Any, tcpport);
                else
                {
                    NotifyEventArgs args = new NotifyEventArgs();
                    args.Summary = "TCP Port out of range";
                    args.Source = "TCPServer";
                    args.Message = "Port number must be in the 1024-65535 range.";
                    args.Exception = null;

                    OnNotifyError(args);
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "ArgumentOutOfRangeException";
                args.Source = "TCPServer";
                args.Message = "Port number must be in the 1024-65535 range.";
                args.Exception = e;

                OnNotifyError(args);
            }

            return localEndPoint;
        }

        public bool Start()
        {

            IPEndPoint localEndPoint = GetLocalEndPoint();
            if (localEndPoint == null)
                return false;

            try
            {
                // Create a TCP/IP socket.
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (SocketException e)
            {
                OnNotifyError("TCPServer", e);
            }

            // Connect our local client accepted event handler
            // before calling sock.BeginAccept()
            //this.ClientAccepted += new ClientAccepted(this.TCPServer_ClientAccepted);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                sock.Bind(localEndPoint);
                sock.Listen(10);

                ServerStarted?.Invoke(this);

                while (shuttingDown == false)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    sock.BeginAccept(new AsyncCallback(AcceptCallback), sock);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }
            }
            catch (SocketException e)
            {
                OnNotifyError("TCPServer", e);
                Close();
            }
            catch (Exception e)
            {
                OnNotifyError("TCPServer", e);
                Close();
            }

            return true;
        }

        public void Stop()
        {
            shuttingDown = true;
        }

        public void Close()
        {
            if (closed)
                return;

            if (!socketClosed)
            {
                ArrayList tmpList = new ArrayList();

                // copy sessions into new tmpList
                foreach (KeyValuePair<int, NetSession> kv in sessions)
                {
                    tmpList.Add(kv.Value);
                }

                // from tmpList, Close() every session
                // NetSession.Close() will call its ConnClosed handler
                // which resolved to OnSessionClosed()
                // OnSessionClosed() will clear sessions 

                foreach (NetSession ses in tmpList)
                {
                    ses.Dispose();
                }

                if (sock != null)
                {
                    sock.Close();
                    sock = null;
                }

                socketClosed = true;

                OnNotifyLog("TCPServer", "Socket closed");
            }
            closed = true;
        }

        private bool CanConnect(IPEndPoint ep)
        {
            bool allowed = false;
            IncomingConnection?.Invoke(ep, out allowed);

            return allowed;
        }

        //! Socket's BeginAccept() callback
        //  Handles new connection and create session
        private void AcceptCallback(IAsyncResult ar)
        {
            // our session
            NetSession ses = null; 

            try
            {
                // Signal the main thread to continue.
                allDone.Set();

                // Get the socket that handles the client request.
                //Socket listener = sock;
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);
                IPEndPoint ep = (IPEndPoint)handler.RemoteEndPoint;

                if (CanConnect(ep))
                {
                    // setup the session 
                    ses = new NetSession(handler)
                    {
                        Id = NewSessionId()
                    };

                    // bind session event handler 
                    ses.Notified += new Action<NotifyEventArgs>(NetSession_Notified);
                    ses.OnSocketClosed += new SocketClosed(NetSession_Closed);
                    ses.OnDataReceived += new DataReceived(NetSession_DataReceived);

                    ses.BeginReceive();
                    
                    // add session to our table
                    lock (sessions)
                    {
                        sessions.Add(ses.Id, ses);
                    }

                    // Call ClientAccepted handler
                    ClientAccepted?.Invoke(ses);
                }
                else
                {
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

                // start listen for connections again.
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
            }
            catch (SocketException e)
            {
                if (ses != null)
                    ses.Close();

                // Queue the next accept, think this should be here, stop attacks based on killing the waiting listeners
                if (sock != null)
                    sock.BeginAccept(new AsyncCallback(AcceptCallback), sock);
            }
            catch (Exception e)
            {
                if (ses != null)
                    ses.Close();

                // Queue the next accept, think this should be here, stop attacks based on killing the waiting listeners
                if (sock !=null)
                    sock.BeginAccept(new AsyncCallback(AcceptCallback), sock);
            }
        }
        
        #endregion
        
        #region Session event handlers

        private void NetSession_Notified(NotifyEventArgs e)
        {
            OnNotify(e);
        }

        private void NetSession_Closed(NetSession ses)
        {
            // Raise event to consumer
            ClientClosed?.Invoke(ses);

            lock (sessions)
            {
                sessions.Remove(ses.Id);
            }
            ses.Dispose();
        }

        private void NetSession_DataReceived(DataReceivedEventArgs arg)
        {
            // Raise event to consumer
            DataReceived?.Invoke(arg);
        }

        #endregion
    }
}