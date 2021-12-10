using System;
using System.Collections.Generic;

namespace Tobasa
{
    /*
    public class MessageHandlerBase<TReceiveReturnType, TReceiveArgType>
    {
        public MessageHandlerBase(Message qmessage)
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
        public Func<TReceiveArgType, TReceiveReturnType> ReceiveHandler;

        // actual function to send response to Client 
        public Action<NetSession, TReceiveReturnType> ResponseHandler;
    }
    */



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
