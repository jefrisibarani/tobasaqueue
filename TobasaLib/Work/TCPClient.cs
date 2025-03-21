#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2015-2024  Jefri Sibarani
 
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
    public delegate void ConnectionConnected(TCPClient client);
    public delegate void ConnectionClosed(TCPClient client);
    public class TCPClient : Notifier
    {
        #region Member variables

        public event DataReceived OnDataReceived;
        public event ConnectionConnected OnConnected;
        public event ConnectionClosed OnClosed;

        private static int _sessionId = 100;
        private NetSession _sesion = null;
        private string _server;
        private int _port;
        private string _response;
        private int _sendRetry = 1;

        #endregion

        #region Constructor

        public TCPClient(string server, int port)
        {
            _server = server;
            _port = port;
        }

        #endregion

        public string Response
        {
            get { return _response; }
        }

        public void Send(string text)
        {
            if (Connected)
            {
                _sesion.Send(text);
            }
            else // auto reconnect
            {
                for (int i = 0; i< _sendRetry; i++)
                {
                    Stop();
                    Start();
                    if (Connected)
                    {
                        OnNotifyLog("TCPClient", "Retry send message #" + i.ToString());
                        _sesion.Send(text);
                    }
                }
            }
        }

        public NetSession Session
        {
            get { return (_sesion != null) ? _sesion : null; }
        }

        private int NewSessionId()
        {
            _sessionId++;
            return _sessionId;
        }

        public bool Connected
        {
            get { return Session != null && !Session.Closed; }
        }

        public void Stop()
        {
            if (Connected)
            {
                _sesion.Close();
                _sesion = null;
            }
        }

        public void Start()
        {
            if (!Connected)
            {
                Socket sock = GetSocket(_server, _port);

                if (sock != null && sock.Connected)
                {
                    _sesion = new NetSession(sock)
                    {
                        Id = NewSessionId()
                    };

                    _sesion.OnDataReceived += new DataReceived(NetSession_DataReceived);
                    _sesion.Notified += new Action<NotifyEventArgs>(NetSession_Notified);
                    _sesion.OnSocketClosed += new SocketClosed(NetSession_Closed);

                    _sesion.BeginReceive();

                    OnConnected?.Invoke(this);
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

        private void NetSession_Closed(NetSession ses)
        {
            OnClosed?.Invoke(this);
        }

        private void NetSession_Notified(NotifyEventArgs arg)
        {
            OnNotify(arg);
        }

        private void NetSession_DataReceived(DataReceivedEventArgs arg)
        {
            //string stringMessage = Encoding.UTF8.GetString(arg.Data);
            OnDataReceived?.Invoke(arg);
        }

    }
}
