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
using System.Collections.Generic;
using System.Text;

namespace Tobasa
{
    public enum MessageDirection
    {
        REQUEST,
        RESPONSE
    };

    public class Message
    {
        #region Member variables

        protected string _rawMessage;
        protected Msg.Type _messageType;
        protected int _totalPayloadToken;
        protected int _totalMessageToken;
        protected NetSession _session;
        protected MessageDirection _direction;
        protected bool _valid;

        // Dictionary to store REQUEST/RESULT payloads actual data/value
        // Dictionary's Key   is payload name  (string)
        // Dictionary's Value is payload value (string)
        protected Dictionary<string, string> _parameterValues = new Dictionary<string, string>();

        #endregion

        // Construct message from rawdata
        public Message(DataReceivedEventArgs arg)
        {
            _valid = false;

            string stringMessage = Encoding.UTF8.GetString(arg.Data);
            if (stringMessage != null)
            {
                _rawMessage = stringMessage;
            }

            _session = arg.Session;

            Parse();
        }

        // Construct message
        public Message(Msg.Type messageType)
        {
            _messageType = messageType;
        }

        public bool Valid
        {
            get => _valid;
        }

        public string RawMessage
        {
            get => _rawMessage;
            set => _rawMessage = value;
        }

        public Msg.Type MessageType
        {
            get => _messageType;
            set => _messageType = value;
        }

        public int TotalToken
        {
            get => _totalMessageToken;
        }

        public int TotalPayloadTokenm
        {
            get => _totalPayloadToken;
        }

        public int PayloadPosition
        {
            get => _messageType.PayloadPosition;
        }

        public MessageDirection Direction
        {
            get => _direction;
        }

        public NetSession Session
        {
            get => _session;
            set => _session = value;
        }

        public Dictionary<string, string> PayloadValues
        {
            get => _parameterValues;
        }

        protected virtual void Parse()
        {
            if (_rawMessage.StartsWith(Msg.SysLogin.Text))
            {
                _messageType = Msg.SysLogin;
            }
            else if (_rawMessage.StartsWith(Msg.SysNotify.Text))
            {
                _messageType = Msg.SysNotify;
            }
            else if (_rawMessage.StartsWith(Msg.SysUpdTable.Text))
            {
                _messageType = Msg.SysUpdTable;
            }
            else if (_rawMessage.StartsWith(Msg.SysInsTable.Text))
            {
                _messageType = Msg.SysInsTable;
            }
            else if (_rawMessage.StartsWith(Msg.SysDelTable.Text))
            {
                _messageType = Msg.SysDelTable;
            }
            else if (_rawMessage.StartsWith(Msg.SysGetTable.Text))
            {
                _messageType = Msg.SysGetTable;
            }
            else if (_rawMessage.StartsWith(Msg.SysUpdateJob.Text))
            {
                _messageType = Msg.SysUpdateJob;
            }
            else if (_rawMessage.StartsWith(Msg.SysGetJob.Text))
            {
                _messageType = Msg.SysGetJob;
            }
            else if (_rawMessage.StartsWith(Msg.CallerGetInfo.Text))
            {
                _messageType = Msg.CallerGetInfo;
            }
            else if (_rawMessage.StartsWith(Msg.CallerGetNext.Text))
            {
                _messageType = Msg.CallerGetNext;
            }
            else if (_rawMessage.StartsWith(Msg.CallerRecall.Text))
            {
                _messageType = Msg.CallerRecall;
            }
            else if (_rawMessage.StartsWith(Msg.TicketCreate.Text))
            {
                _messageType = Msg.TicketCreate;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayCall.Text))
            {
                _messageType = Msg.DisplayCall;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayRecall.Text))
            {
                _messageType = Msg.DisplayRecall;
            }
            else if (_rawMessage.StartsWith(Msg.DisplaySetFinished.Text))
            {
                _messageType = Msg.DisplaySetFinished;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayShowInfo.Text))
            {
                _messageType = Msg.DisplayShowInfo;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayResetValues.Text))
            {
                _messageType = Msg.DisplayResetValues;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayResetRunText.Text))
            {
                _messageType = Msg.DisplayResetRunText;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayDelRunText.Text))
            {
                _messageType = Msg.DisplayDelRunText;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayGetRunText.Text))
            {
                _messageType = Msg.DisplayGetRunText;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayGetInfo.Text))
            {
                _messageType = Msg.DisplayGetInfo;
            }
            else if (_rawMessage.StartsWith(Msg.SysGetList.Text))
            {
                _messageType = Msg.SysGetList;
            }
            else if (_rawMessage.StartsWith(Msg.DisplayUpdateQueueLeft.Text))
            {
                _messageType = Msg.DisplayUpdateQueueLeft;
            }
            else if (_rawMessage.StartsWith(Msg.CallerUpdateQueueLeft.Text))
            {
                _messageType = Msg.CallerUpdateQueueLeft;
            }
            else
            {
                _valid = false;
                throw new AppException(string.Format("Invalid message format from {0}: {1}", _session.RemoteInfo, _rawMessage.Substring(0, 60)));
            }

            _valid = ExtractTokens();

            if (!_valid)
            {
                throw new AppException(string.Format("Invalid {0} from {1}: {2}", _messageType.String, _session.RemoteInfo, _rawMessage.Substring(0,60)));
            }
        }

        protected virtual bool ExtractTokens()
        {
            try
            {
                string[] tokens = _rawMessage.Split(Msg.Separator.ToCharArray());

                if (tokens.Length == _messageType.TotalToken)
                {
                    // Compare each token
                    // Make sure we got correct message structure!
                    if (tokens[0] != _messageType.Module)
                        return false;

                    if (tokens[1] != _messageType.Command)
                        return false;

                    if (_messageType == Msg.SysNotify)
                    {
                        // For other message, get total payload from RequestPayload
                        _totalPayloadToken = _messageType.TotalRequestPayload;
                    }
                    else
                    {
                        string dirStr = tokens[2];
                        if (dirStr == "REQ")
                        {
                            _direction = MessageDirection.REQUEST;
                            _totalPayloadToken = _messageType.TotalRequestPayload;
                        }
                        else if (dirStr == "RES")
                        {
                            _direction = MessageDirection.RESPONSE;
                            _totalPayloadToken = _messageType.TotalResultPayload;
                        }
                        else
                            return false;
                    }

                    if (_messageType.PayloadPosition > 0)
                    {
                        // Message structure OK, now extract payload/parameters
                        string payload = tokens[_messageType.PayloadPosition];
                        return ExtractPayload(payload);
                    }
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw new AppException(string.Format("Message parsing error: ", ex.Message));
            }

            return false;
        }

        protected virtual bool ExtractPayload(string payload)
        {
            // Message OK, now extract payload/parameters
            string[] payloadTokens = payload.Split(Msg.CompDelimiter.ToCharArray());
            if (payloadTokens.Length == _totalPayloadToken)
            {
                Dictionary<int, string> payloadMetaData = null;

                if (_messageType != Msg.SysNotify)
                {
                    if (_direction == MessageDirection.REQUEST)
                        payloadMetaData = _messageType.RequestPayload;
                    else
                        payloadMetaData = _messageType.ResultPayload;
                }
                else
                    payloadMetaData = _messageType.RequestPayload;

                // save parameter found in raw message to _parameterValues
                foreach (KeyValuePair<int, string> kv in payloadMetaData)
                {
                    int paramPos = kv.Key;   // parameter position payload
                    string paramName = kv.Value;
                    string paramValue = payloadTokens[paramPos];
                    // get parameter value from payloadTokens
                    _parameterValues.Add(paramName, paramValue);
                }

                return true;
            }

            return false;
        }
    }

    public class SysMessage : Message
    {
        public SysMessage(DataReceivedEventArgs arg)
            : base(arg)
        {
        }

        public SysMessage(Msg.Type messageType)
            : base(messageType)
        {
        }
    }
}
