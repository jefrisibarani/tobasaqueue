using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

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




                                // Send message to all Display to update their total waiting queue
                                string post = qmessage.PayloadValues["post"];
                                var queueInfo = QueueRepository.GetWaitingNumberAndPostSummary(post);
                                if (queueInfo == null)
                                {
                                    return;
                                }

                                string totalWaiting = queueInfo["numberLeft"];


                                string message1 = Msg.DisplayUpdateQueueLeft.Text +
                                                 Msg.Separator + "REQ" +
                                                 Msg.Separator + "Identifier" +
                                                 Msg.Separator + post +
                                                 Msg.CompDelimiter + totalWaiting;
                                QueueServer.SendMessageToQueueDisplay(message1, post);

                                // also to all Caller
                                string message2 = Msg.CallerUpdateQueueLeft.Text +
                                                 Msg.Separator + "REQ" +
                                                 Msg.Separator + "Identifier" +
                                                 Msg.Separator + post +
                                                 Msg.CompDelimiter + totalWaiting;
                                QueueServer.SendMessageToQueueCaller(message2, post);
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
