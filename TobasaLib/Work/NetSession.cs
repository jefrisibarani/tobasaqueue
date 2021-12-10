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
    public delegate void DataReceived(DataReceivedEventArgs arg);
    public delegate void ConnectionClosed(NetSession ses);

    public class DataReceivedEventArgs : EventArgs
    {
        private NetSession _session = null;
        private byte[] _data = null;

        public DataReceivedEventArgs(NetSession ses, byte[] data )
        {
            _session = ses;
            _data = data;
        }
        
        public NetSession Session
        {
            get => _session;
        }

        public string RemoteInfo 
        {
            get 
            {
                if (_session != null && !_session.Closed)
                    return _session.RemoteInfo;
                else
                    return "";
            }
        }

        public int SessionID
        {
            get
            {
                if (_session != null && !_session.Closed)
                    return _session.Id;
                else
                    return -1;
            }
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public string DataString
        {
            get
            {
                if (_data == null)
                    return "";

                /*
                // Deserialize the message
                object message = Message.Deserialize(_data);

                // Handle the message
                StringMessage stringMessage = message as StringMessage;
                if (stringMessage != null)
                    return stringMessage.Message;
                */

                string stringMessage = Encoding.UTF8.GetString(_data);
                if (stringMessage != null)
                    return stringMessage;

                return "";
            }
        }
    }

    public class NetSession : Notifier, IDisposable
    {
        #region Member variables

        public event DataReceived DataReceived;
        public event ConnectionClosed ConnectionClosed;

        bool _disposed = false;
        private bool _socketClosed = false;
        private bool _socketDisconnected = false;
        private Socket _sock = null;
        private int _id = -1;
        private string _remoteinfo = string.Empty;
        private string _localinfo = string.Empty;
        private int _keepAliveInterval = 5;             // 5 second
        private System.Timers.Timer _keepaliveTimer;
        private int _maxMessageSize;                    // 0 = no maximum message size
        private byte[] _lengthBuffer;
        private byte[] _dataBuffer;
        private int _bytesReceived;
        private bool _exceptionOccurred = false;

        #endregion

        #region Constructor and Destructor

        public NetSession(Socket handler)
        {
            _sock = handler;
            if (_sock.Connected)
            {
                _remoteinfo = _sock.RemoteEndPoint.ToString();
                _localinfo = _sock.LocalEndPoint.ToString();
            }

            // Allocate the buffer for receiving message lengths
            _lengthBuffer = new byte[sizeof(int)];
            _maxMessageSize = 102400;  // 100KB

            // Initialize the keepalive timer and its default value.
            _keepaliveTimer = new System.Timers.Timer();
            _keepaliveTimer.Elapsed += new System.Timers.ElapsedEventHandler(KeepaliveTimer_Elapsed);
            _keepaliveTimer.Interval = _keepAliveInterval*1000;
            _keepaliveTimer.Enabled = true;
        }

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
            if (_disposed)
                return;

            if (disposing)
            {
                DataReceived = null;
                ConnectionClosed = null;
                // Free any other managed objects here. 
            }

            #if DEBUG
            OnNotifyLog(NetSessionId, "Disposed successfully");
            #endif

            // Free any unmanaged objects here. 
            Close();
            _disposed = true;
        }

        #endregion

        #region Miscs

        private void KeepaliveTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            Send(BitConverter.GetBytes(0), true);
        }

        private bool SendReceiveShouldStop
        {
            get 
            { 
                return _socketDisconnected || _exceptionOccurred || _socketClosed; 
            }
        }

        private string NetSessionId
        {
            get => "NetSession: " + Id.ToString();
        }

        private void CheckSocketException(SocketException ex)
        {
            if (ex.SocketErrorCode == SocketError.ConnectionAborted || ex.SocketErrorCode == SocketError.ConnectionReset)
            {
                _socketDisconnected = true;
                _exceptionOccurred = true;
            }
        }

        #endregion

        #region Connection closing

        public void Close()
        {
            if (!_socketClosed)
            {
                _keepaliveTimer.Elapsed -= new System.Timers.ElapsedEventHandler(KeepaliveTimer_Elapsed);
                _keepaliveTimer.Enabled = false;
                _keepaliveTimer.Dispose();

                _socketDisconnected = true;
                _socketClosed = true;

                _sock.Shutdown(SocketShutdown.Both);
                _sock.Close();
                
                _sock = null;

                OnNotifyLog(NetSessionId, "Closed successfully");

                ConnectionClosed?.Invoke(this);
            }
        }

        #endregion

        #region Setter and Getter

        public int KeepAliveInterval
        {
            get { return _keepAliveInterval; }
            set { _keepAliveInterval = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Socket Socket
        {
            get { return _sock; }
        }

        public bool Closed
        {
            get { return _socketClosed; }
        }

        public string RemoteInfo
        {
            get { return _remoteinfo; }
        }

        public string LocalInfo
        {
            get { return _localinfo; }
        }

        #endregion

        #region Socket related method

        public void BeginReceive()
        {
            if (_socketClosed)
                return;

            // if established connection was aborted by remote side, we got SocketException here!

            // Read into the appropriate buffer: length or data
            if (_dataBuffer != null)
                _sock.BeginReceive(_dataBuffer, _bytesReceived, _dataBuffer.Length - _bytesReceived, 0, new AsyncCallback(ReceiveCallback), this);
            else
                _sock.BeginReceive(_lengthBuffer, _bytesReceived, _lengthBuffer.Length - _bytesReceived, 0, new AsyncCallback(ReceiveCallback), this);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            if (_socketClosed)
                return;

            try
            {
                int read = _sock.EndReceive(ar);
                ReadData(read);
            }
            catch (SocketException e)
            {
                CheckSocketException(e);
                OnNotifyError(NetSessionId, e);
                Close();
            }
            catch (Exception e)
            {
                _exceptionOccurred = true;
                OnNotifyError(NetSessionId, e);
                Close();
            }
        }

        internal void ReadData(int read)
        {
            // Get the number of bytes read into the buffer
            _bytesReceived += read;

            if (_dataBuffer == null)
            {
                // We're currently receiving the length buffer
                if (_bytesReceived != sizeof(int))
                {
                    // We haven't gotten all the length buffer yet
                    BeginReceive();
                }
                else
                {
                    // We've gotten the length buffer
                    int lengthNBO = BitConverter.ToInt32(_lengthBuffer, 0);
                    int length = IPAddress.NetworkToHostOrder(lengthNBO); 

                    // Sanity check for length < 0
                    // This check will catch 50% of transmission errors that make it past both the IP and Ethernet checksums
                    if (length < 0)
                    {
                        string errmsg = "Packet length less than zero (corrupted message)";
                        throw new System.IO.InvalidDataException(errmsg);
                    }

                    // Another sanity check is needed here for very large packets, to prevent denial-of-service attacks
                    if (_maxMessageSize > 0 && length > _maxMessageSize)
                    {
                        string errmsg = "Message length " + length.ToString() + " is larger than maximum message size " + _maxMessageSize.ToString();
                        throw new System.Net.ProtocolViolationException(errmsg);
                    }

                    // Zero-length packets are allowed as keepalives
                    if (length == 0)
                    {
                        _bytesReceived = 0;
                        BeginReceive();
                    }
                    else
                    {
                        // Create the data buffer and start reading into it
                        _dataBuffer = new byte[length];
                        _bytesReceived = 0;
                        BeginReceive();
                    }
                }
            }
            else
            {
                if (_bytesReceived != _dataBuffer.Length)
                {
                    // We haven't gotten all the data buffer yet
                    BeginReceive();
                }
                else
                {
                    // We've gotten an entire packet
                    OnDataReceived();

                    // Start reading the length buffer again
                    _dataBuffer = null;
                    _bytesReceived = 0;
                    BeginReceive();
                }
            }
        }

        public void Send(string data)
        {
            if (SendReceiveShouldStop)
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
            if (SendReceiveShouldStop)
                return;
            try
            {
                // Begin sending the data to the remote device.
                lock (_sock)
                {
                    if (keepalivemsg)
                    {
                        _sock.BeginSend(data, 0, data.Length, 0, null, this);
                    }
                    else
                    {
                        // convert string length value to network order
                        int dataLengthNBO = IPAddress.HostToNetworkOrder(data.Length);

                        // Get the length prefix for the message
                        byte[] lengthPrefix = BitConverter.GetBytes(dataLengthNBO);

                        // no callback here
                        _sock.BeginSend(lengthPrefix, 0, lengthPrefix.Length, 0, null, this);
                        // Send the actual message, this time enabling the normal callback.
                        _sock.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), this);
                    }
                }
            }
            catch (SocketException e)
            {
                CheckSocketException(e);
                OnNotifyError(NetSessionId, e);
                Close();
            }
            catch (Exception e)
            {
                _exceptionOccurred = true;
                OnNotifyError(NetSessionId, e);
                Close();
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            if (SendReceiveShouldStop)
                return;

            try
            {
                // Complete sending the data to the remote device.
                int bytesSent = _sock.EndSend(ar);

                OnNotifyLog(NetSessionId, String.Format("Sent {0} bytes data to {1}", bytesSent, RemoteInfo));
            }
            catch (SocketException e)
            {
                CheckSocketException(e);
                OnNotifyError(NetSessionId, e);
                Close();
            }
            catch (Exception e)
            {
                _exceptionOccurred = true;
                OnNotifyError(NetSessionId, e);
                Close();
            }
        }
        
        protected virtual void OnDataReceived()
        {
            if (DataReceived != null)
            {
                OnNotifyLog(NetSessionId, String.Format("Received {0} bytes data from {1}", _dataBuffer.Length.ToString(), RemoteInfo));

                DataReceivedEventArgs arg = new DataReceivedEventArgs(this, _dataBuffer);
                DataReceived(arg);
            }
        }

        #endregion
    }
}