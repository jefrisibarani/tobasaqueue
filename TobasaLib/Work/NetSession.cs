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
    public delegate void DataReceived(DataReceivedEventArgs arg);
    public delegate void ConnectionClosed(NetSession ses);

    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(NetSession ses, byte[] data )
        {
            mSession = ses;
            mData = data;
        }

        private NetSession mSession = null;
        private byte[] mData = null;

        public string RemoteInfo 
        {
            get 
            {
                if (mSession != null && !mSession.Closed)
                    return mSession.RemoteInfo;
                else
                    return "";
            }
        }

        public int SessionID
        {
            get
            {
                if (mSession != null && !mSession.Closed)
                    return mSession.Id;
                else
                    return -1;
            }
        }

        public byte[] Data
        {
            get { return mData; }
        }
    }

    public class NetSession : Notifier, IDisposable
    {
        #region Member variables
        
        bool disposed = false;
        private bool socketClosed = false;
        private bool socketDisconnected = false;

        private Socket sock = null;
        private int id = -1;
        private string remoteinfo = String.Empty;
        private string localinfo = String.Empty;

        public event DataReceived DataReceived;
        public event ConnectionClosed ConnectionClosed;

        private int keepAliveInterval = 5; // 5 second
        System.Timers.Timer KeepaliveTimer;

        private int maxMessageSize; // 0 = no maximum message size
        private byte[] lengthBuffer;
        private byte[] dataBuffer;
        private int bytesReceived;

        #endregion

        #region Constructor

        public NetSession(Socket handler)
        {
            sock = handler;
            if (sock.Connected)
            {
                remoteinfo = sock.RemoteEndPoint.ToString();
                localinfo = sock.LocalEndPoint.ToString();
            }

            // Allocate the buffer for receiving message lengths
            lengthBuffer = new byte[sizeof(int)];
            maxMessageSize = 1024; 

            // Initialize the keepalive timer and its default value.
            KeepaliveTimer = new System.Timers.Timer();
            KeepaliveTimer.Elapsed += new System.Timers.ElapsedEventHandler(KeepaliveTimer_Elapsed);
            KeepaliveTimer.Interval = keepAliveInterval*1000;
            KeepaliveTimer.Enabled = true;
        }

        private void KeepaliveTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            Send(BitConverter.GetBytes(0),true);
        }

        #endregion

        #region Destructor

        ~NetSession()
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
                DataReceived = null;
                ConnectionClosed = null;
                // Free any other managed objects here. 
            }

            // Free any unmanaged objects here. 
            Close();
            disposed = true;

            #if DEBUG
            Logger.Log("NetSession : ID " + Id.ToString() + " Disposed...............................");
            #endif
        }

        #endregion

        #region Connection closing

        public void Close()
        {
            if (!socketClosed)
            {
                KeepaliveTimer.Elapsed -= new System.Timers.ElapsedEventHandler(KeepaliveTimer_Elapsed);
                KeepaliveTimer.Enabled = false;
                KeepaliveTimer.Dispose();

                socketDisconnected = true;
                socketClosed = true;

                sock.Shutdown(SocketShutdown.Both);
                sock.Close();
                sock = null;

                // call callback
                if (ConnectionClosed != null)
                    ConnectionClosed(this);

                Logger.Log("NetSession : ID " + Id.ToString() + " Closed...............................");
            }
        }

        #endregion

        #region Setter and Getter

        public int KeepAliveInterval
        {
            get { return keepAliveInterval; }
            set { keepAliveInterval = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Socket Socket
        {
            get { return sock; }
        }

        public bool Closed
        {
            get { return socketClosed; }
        }

        public bool SocketDisconnected
        {
            get { return socketDisconnected; }
        }

        public string RemoteInfo
        {
            get { return remoteinfo; }
        }

        public string LocalInfo
        {
            get { return localinfo; }
        }

        #endregion

        #region Socket related method

        public void BeginReceive()
        {
            if (socketClosed)
                return;

            // Read into the appropriate buffer: length or data
            if (dataBuffer != null)
                sock.BeginReceive(dataBuffer, bytesReceived, dataBuffer.Length - bytesReceived, 0,new AsyncCallback(ReceiveCallback), this);
            else
                sock.BeginReceive(lengthBuffer, bytesReceived, lengthBuffer.Length - bytesReceived, 0,new AsyncCallback(ReceiveCallback), this);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            if (socketClosed)
                return;

            try
            {
                int read = sock.EndReceive(ar);
                ReadData(read);
            }
            catch (SocketException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : SocketExceptions Code: " + Convert.ToString(e.ErrorCode) + "  " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "SocketException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;
                // Notify disconected event - ErrorCode 10054 - SocketErrorCode.ConnectionReset
                //if (e.ErrorCode == 10054)
                //    socketDisconnected = true;
                OnNotifyError(args);
            }
            catch (ArgumentNullException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : ArgumentNullException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "ArgumentNullException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (InvalidOperationException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : InvalidOperationException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "InvalidOperationException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (Exception e)
            {
                // Exception throwed by ReadData(), will be handled here

                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : Exception : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "Exception";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
        }

        internal void ReadData(int read)
        {
            // Get the number of bytes read into the buffer
            bytesReceived += read;

            if (dataBuffer == null)
            {
                // We're currently receiving the length buffer
                if (bytesReceived != sizeof(int))
                {
                    // We haven't gotten all the length buffer yet
                    BeginReceive();
                }
                else
                {
                    // We've gotten the length buffer
                    int lengthNBO = BitConverter.ToInt32(lengthBuffer, 0);
                    int length = IPAddress.NetworkToHostOrder(lengthNBO); 

                    // Sanity check for length < 0
                    // This check will catch 50% of transmission errors that make it past both the IP and Ethernet checksums
                    if (length < 0)
                    {
                        string errmsg = "Packet length less than zero (corrupted message)";
                        throw new System.IO.InvalidDataException(errmsg);
                    }

                    // Another sanity check is needed here for very large packets, to prevent denial-of-service attacks
                    if (maxMessageSize > 0 && length > maxMessageSize)
                    {
                        string errmsg = "Message length " + length.ToString() + " is larger than maximum message size " + maxMessageSize.ToString();
                        throw new System.Net.ProtocolViolationException(errmsg);
                    }

                    // Zero-length packets are allowed as keepalives
                    if (length == 0)
                    {
                        bytesReceived = 0;
                        BeginReceive();
                    }
                    else
                    {
                        // Create the data buffer and start reading into it
                        dataBuffer = new byte[length];
                        bytesReceived = 0;
                        BeginReceive();
                    }
                }
            }
            else
            {
                if (bytesReceived != dataBuffer.Length)
                {
                    // We haven't gotten all the data buffer yet
                    BeginReceive();
                }
                else
                {
                    // We've gotten an entire packet
                    OnDataReceived();

                    // Start reading the length buffer again
                    dataBuffer = null;
                    bytesReceived = 0;
                    BeginReceive();
                }
            }
        }

        public void Send(string data)
        {
            if (socketClosed)
                return;
            
            /*
            Messages.StringMessage message = new Messages.StringMessage();
            message.Message = data;
            // Serialize the message to a binary array
            byte[] binaryMessage = Messages.Util.Serialize(message);
            */

            // Convert the string data to byte data using UTF8 encoding.
            byte[] binaryMessage = Encoding.UTF8.GetBytes(data);
            Send(binaryMessage);
        }

        public void Send(byte[] data,bool keepalivemsg = false)
        {
            if (socketClosed)
                return;
            try
            {
                // Begin sending the data to the remote device.
                lock (sock)
                {
                    if (keepalivemsg)
                    {
                        sock.BeginSend(data, 0, data.Length, 0, null, this);
                    }
                    else
                    {
                        // convert string length value to network order
                        int dataLengthNBO = IPAddress.HostToNetworkOrder(data.Length);

                        // Get the length prefix for the message
                        byte[] lengthPrefix = BitConverter.GetBytes(dataLengthNBO);

                        // no callback here
                        sock.BeginSend(lengthPrefix, 0, lengthPrefix.Length, 0, null, this);
                        // Send the actual message, this time enabling the normal callback.
                        sock.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), this);
                    }
                }
            }
            catch (SocketException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : SocketException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "SocketException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (ArgumentNullException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : ArgumentNullException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "ArgumentNullException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : ArgumentOutOfRangeException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "ArgumentOutOfRangeException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (ObjectDisposedException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : ObjectDisposedException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "ObjectDisposed";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (Exception e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : Exception : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "Exception";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            if (socketClosed)
                return;

            try
            {
                // Complete sending the data to the remote device.
                int bytesSent = sock.EndSend(ar);
                Logger.Log(String.Format("NetSession : ID " + Id.ToString() + " Sent {0} bytes data to {1}", bytesSent, RemoteInfo));
            }
            catch (SocketException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : SocketException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "SocketException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (ArgumentNullException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : ArgumentNullException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "ArgumentNullException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (ArgumentException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : ArgumentException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "ArgumentException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (InvalidOperationException e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : InvalidOperationException : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "InvalidOperationException";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
            catch (Exception e)
            {
                Close();

                string msg = "NetSession : ID " + Id.ToString() + " : Exception : " + e.Message;

                NotifyEventArgs args = new NotifyEventArgs();
                args.Summary = "Exception";
                args.Source = e.Source;
                args.Message = msg;
                args.Exception = e;

                OnNotifyError(args);
            }
        }
        
        protected virtual void OnDataReceived()
        {
            if (DataReceived != null)
            {
                Logger.Log(String.Format("NetSession : ID " + Id.ToString() + " Received {0} bytes data from {1}", dataBuffer.Length.ToString(), RemoteInfo));
                DataReceivedEventArgs arg = new DataReceivedEventArgs(this, dataBuffer);
                DataReceived(arg);
            }
        }

        #endregion

    }
}