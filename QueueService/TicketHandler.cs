using System;
using System.Collections.Generic;

namespace Tobasa
{
    class TicketHandler
    {
        public TicketHandler()
        {
        }

        public void OnMessage(DataReceivedEventArgs arg, Client client)
        {
            Exception exp = null;

            try
            {
                Message qmessage = new Message(arg);

                Logger.Log("[TicketHandler] Processing " + qmessage.MessageType.Text + " from " + client.RemoteInfo);

                // Handle TicketCreate
                if (qmessage.MessageType == Msg.TicketCreate && qmessage.Direction == MessageDirection.REQUEST)
                {
                    MessageHandler< Dictionary<string,string> > handler = new MessageHandler< Dictionary<string, string> >(qmessage)
                    {
                        ReceiveHandler = new Func< Dictionary<string, string>, Dictionary<string, string> >(QueueRepository.CreateNewNumber),
                        ResponseHandler = (session, result) =>
                        {
                            // Send response to client
                            if (result != null)
                            {
                                // Send response to client(caller)
                                string message = Msg.TicketCreate.Text +
                                                 Msg.Separator + "RES" +
                                                 Msg.Separator + "Identifier" +
                                                 Msg.Separator + result["postprefix"] +
                                                 Msg.CompDelimiter + result["number"] +
                                                 Msg.CompDelimiter + result["post"] +
                                                 Msg.CompDelimiter + result["timestamp"];

                                session.Send(message);
                            }
                        }
                    };

                    handler.Process();
                }
            }
            catch(AppException ex)
            {
                exp = ex;
            }
            catch (Exception ex)
            {
                exp = ex;
            }

            if(exp != null)
            {
                Logger.Log("TicketHandler", exp);

                // SYS|NOTIFY|[Type!Message]
                string message =
                    Msg.SysNotify.Text +
                    Msg.Separator + "ERROR" +
                    Msg.CompDelimiter + exp.Message;

                client.Session.Send(message);
            }
        }
    }
}
