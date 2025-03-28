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
using System.Text;

namespace Tobasa
{
    public delegate void DataReceived(DataReceivedEventArgs arg);
    public delegate void SocketClosed(NetSession ses);

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

                string stringMessage = Encoding.UTF8.GetString(_data);
                if (stringMessage != null)
                    return stringMessage;

                return "";
            }
        }
    }

    public class NetSession : Notifier, IDisposable
    {
        public const int MAX_MESSAGE_SIZE = 102400; // 100 KB, 0 = no maximum message size
        
        #region Member variables

        public event DataReceived OnDataReceived;
        public event SocketClosed OnSocketClosed;

        bool            _disposed           = false;
        private bool    _socketClosed       = false;
        private bool    _socketDisconnected = false;
        private Socket  _socket             = null;
        private int     _id                 = -1;
        private string  _remoteinfo         = string.Empty;
        private string  _localinfo          = string.Empty;
        private int     _keepAliveInterval  = 5; // 5 second
        private int     _maxMessageSize     = MAX_MESSAGE_SIZE;
        private byte[]  _lengthBuffer       = null;
        private byte[]  _dataBuffer         = null;
        private int     _bytesReceived      = 0;
        private bool    _exceptionOccurred  = false;

        private System.Timers.Timer _keepaliveTimer;

        #endregion

        #region Constructor and Destructor

        public NetSession(Socket handler)
        {
            _socket = handler;
            if (_socket.Connected)
            {
                _remoteinfo = _socket.RemoteEndPoint.ToString();
                _localinfo = _socket.LocalEndPoint.ToString();
            }

            // Allocate the buffer for receiving message lengths
            _lengthBuffer = new byte[sizeof(int)];
            _maxMessageSize = MAX_MESSAGE_SIZE;

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
                OnDataReceived = null;
                OnSocketClosed = null;
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
            // send keep alive ping to remote endpoint
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

                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                
                _socket = null;

                OnNotifyLog(NetSessionId, "Closed successfully");

                OnSocketClosed?.Invoke(this);
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
            get { return _socket; }
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
                _socket.BeginReceive(_dataBuffer, _bytesReceived, _dataBuffer.Length - _bytesReceived, 0, new AsyncCallback(ReceiveCallback), this);
            else
                _socket.BeginReceive(_lengthBuffer, _bytesReceived, _lengthBuffer.Length - _bytesReceived, 0, new AsyncCallback(ReceiveCallback), this);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            if (_socketClosed)
                return;

            try
            {
                int read = _socket.EndReceive(ar);
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
                    CallOnDataReceived();

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
                lock (_socket)
                {
                    if (keepalivemsg)
                    {
                        _socket.BeginSend(data, 0, data.Length, 0, null, this);
                    }
                    else
                    {
                        // we send lengthPrefix over the wire in network byte order/big endian.
                        
                        // convert string length value to network order
                        int dataLengthNBO = IPAddress.HostToNetworkOrder(data.Length);
                        // Get the length prefix for the message
                        byte[] lengthPrefix = BitConverter.GetBytes(dataLengthNBO);

                        // Send length prefix, no callback here
                        _socket.BeginSend(lengthPrefix, 0, lengthPrefix.Length, 0, null, this);
                        // Send the actual message, this time enabling the normal callback.
                        _socket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), this);
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
                int bytesSent = _socket.EndSend(ar);

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
        
        protected virtual void CallOnDataReceived()
        {
            if (OnDataReceived != null)
            {
                OnNotifyLog(NetSessionId, String.Format("Received {0} bytes data from {1}", _dataBuffer.Length.ToString(), RemoteInfo));

                DataReceivedEventArgs arg = new DataReceivedEventArgs(this, _dataBuffer);
                OnDataReceived(arg);
            }
        }

        #endregion
    }
}