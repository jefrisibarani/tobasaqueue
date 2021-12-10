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
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tobasa
{
    public class TCPClient : Notifier
    {
        #region Member variables

        NetSession ses = null;
        private string mServer;
        private int mPort;
        private string response;
        public static int sessionId = 100;

        #endregion

        #region Constructor

        public TCPClient(string server, int port)
        {
            mServer = server;
            mPort = port;
        }

        #endregion

        public string Response
        {
            get { return response; }
        }

        public void Send(string text)
        {
            if (Connected)
                ses.Send(text);
        }

        public NetSession Session
        {
            get { return (ses != null) ? ses : null; }
        }

        private int NewSessionId()
        {
            sessionId++;
            return sessionId;
        }

        public bool Connected
        {
            get { return Session != null && !Session.Closed; }
        }

        public void Stop()
        {
            if (Connected)
            {
                ses.Close();
                ses = null;
            }
        }

        public void Start()
        {
            if (!Connected)
            {
                Socket sock = GetSocket(mServer, mPort);

                if (sock != null)
                {
                    ses = new NetSession(sock)
                    {
                        Id = NewSessionId()
                    };
                    ses.DataReceived += new DataReceived(NetSession_DataReceived);
                    ses.Notified += new Action<NotifyEventArgs>(NetSession_Notified);
                    ses.BeginReceive();
                }
            }
        }

        private Socket GetSocket(string server, int port)
        {
            // Create socket and connect to a remote device.
            try
            {
                IPAddress ipAddress = null;
                
                /*
                IPHostEntry ipHostInfo = Dns.GetHostEntry(server);

                // Get only IPv4 Address
                foreach (IPAddress a in ipHostInfo.AddressList)
                {
                    if (a.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = a;
                        break;
                    }
                }
                */

                // resolv ip dengan GetHostAddresses, karena GetHostEntry 
                // meresolv 192.169.1.1 menjadi 36.22.22.180, bila dns 
                // bukan local lan dns
                 
                IPAddress[] ips;
                ips = Dns.GetHostAddresses(server);

                foreach (IPAddress a in ips)
                {
                    if (a.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = a;
                        break;
                    }
                }
               
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                sock.Connect(remoteEP);
                
                return sock;
            }
            catch (SocketException e)
            {
                OnNotifyError("TCPClient", e);
            }
            catch (Exception e)
            {
                OnNotifyError("TCPClient", e);
            }
            
            return null;
        }

        private void NetSession_Notified(NotifyEventArgs arg)
        {
            OnNotify(arg);
        }

        private void NetSession_DataReceived(DataReceivedEventArgs arg)
        {
            string stringMessage = Encoding.UTF8.GetString(arg.Data);
            if (stringMessage != null)
            {
                response = stringMessage;
                return;
            }
        }

    }
}
