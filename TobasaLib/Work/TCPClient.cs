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
                    ses = new NetSession(sock);
                    ses.DataReceived += new DataReceived(NetSession_DataReceived);
                    ses.Notified += new Action<NotifyEventArgs>(NetSession_Notified);
                    ses.BeginReceive();
                }
            }
        }

        private Socket GetSocket(string server, int port)
        {
            /// Create socket and connect to a remote device.
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

                /// resolv ip dengan GetHostAddresses, karena GetHostEntry 
                /// meresolv 192.169.1.1 menjadi 36.22.22.180, bila dns 
                /// bukan local lan dns
                 
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

                /// Create a TCP/IP socket.
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                /// Connect to the remote endpoint.
                sock.Connect(remoteEP);
                
                return sock;
            }
            catch (SocketException e)
            {
                Logger.Log("TCPClient : SocketException : " + e.Message);

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "SocketException";
                args.Source = "TCPClient";
                args.Message = e.Message;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (ArgumentNullException e)
            {
                Logger.Log("TCPClient : ArgumentNullException : " + e.Message);

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "ArgumentNullException";
                args.Source = "TCPClient";
                args.Message = e.Message;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (NullReferenceException e)
            {
                Logger.Log("TCPClient : NullReferenceException : " + e.Message);

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "NullReferenceException";
                args.Source = "TCPClient";
                args.Message = e.Message;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (Exception e)
            {
                Logger.Log("TCPClient : Exception : " + e.Message);

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "Exception";
                args.Source = "TCPClient";
                args.Message = e.Message;
                args.Exception = e;

                OnNotifyError(args);
            }
            
            return null;
        }

        private void NetSession_Notified(NotifyEventArgs e)
        {
            Logger.Log(e.Source + " : " + e.Summary + " : " + e.Message);
            OnNotify(e);
        }

        private void NetSession_DataReceived(DataReceivedEventArgs arg)
        {
            /*
            /// Deserialize the message
            object message = Msg.Deserialize(arg.Data);

            /// Handle the message
            StringMessage stringMessage = message as StringMessage;
            if (stringMessage != null)
                response = stringMessage.Message;
            */
            string stringMessage = Encoding.UTF8.GetString(arg.Data);
            if (stringMessage != null)
            {
                response = stringMessage;
                return;
            }
        }

    }
}
