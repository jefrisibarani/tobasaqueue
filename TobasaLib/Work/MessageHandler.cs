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

namespace Tobasa
{
    public class MessageHandler<TReceiveReturnType>
    {
        public MessageHandler(Message qmessage)
        {
            Message = qmessage;
        }

        public Message Message
        {
            get;
            set;
        }

        public void Process()
        {
            if (ReceiveHandler != null)
            {
                // Execute receiveHandler
                TReceiveReturnType retVal = ReceiveHandler(Message.PayloadValues);

                // Execute responseHandler, passing return Client and receiveHandler's return value
                ResponseHandler?.Invoke(Message.Session, retVal);
            }
        }

        // actual function to process request from Client
        public Func< Dictionary<string, string> , TReceiveReturnType > ReceiveHandler;

        // actual function to send response to Client 
        public Action<NetSession, TReceiveReturnType> ResponseHandler;
    }
}
