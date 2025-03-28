using System;
using System.Collections.Generic;

namespace Tobasa
{
    class CallerHandler
    {
        public CallerHandler()
        {
        }

        public void OnMessage(DataReceivedEventArgs arg, Client client)
        {
            Exception exp = null;

            try
            {
                Message qmessage = new Message(arg);

                Logger.Log("[CallerHandler] Processing " + qmessage.MessageType.Text + " from " + client.RemoteInfo);

                // Handle CallerGetInfo
                if (qmessage.MessageType == Msg.CallerGetInfo && qmessage.Direction == MessageDirection.REQUEST)
                {
                    MessageHandler<Dictionary<string, string>> handler = new MessageHandler<Dictionary<string, string>>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, Dictionary<string, string>>(QueueRepository.GetLastProcessedNumberAndPostSummary),
                        ResponseHandler = (session, result) =>
                        {
                            // Send response to client
                            if (result != null && result.Count == 6)
                            {
                                string postPrefix   = result["postPrefix"];
                                string numberS      = result["number"];
                                string numberLeft   = result["numberLeft"];

                                // Send response to client(caller)
                                string message = 
                                    Msg.CallerGetInfo.Text +
                                    Msg.Separator + "RES" +
                                    Msg.Separator + "Identifier" +
                                    Msg.Separator + postPrefix +
                                    Msg.CompDelimiter + numberS +
                                    Msg.CompDelimiter + numberLeft;

                                session.Send(message);
                            }
                            else
                            {
                                // Send response to client(caller)
                                string message =
                                    Msg.CallerGetInfo.Text +
                                    Msg.Separator + "RES" +
                                    Msg.Separator + "Identifier" +
                                    Msg.Separator + "" +
                                    Msg.CompDelimiter + "" +
                                    Msg.CompDelimiter + "0";

                                session.Send(message);
                            }
                        }
                    };

                    handler.Process();
                }
                // Handle CallerGetNext
                else if (qmessage.MessageType == Msg.CallerGetNext && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string post     = qmessage.PayloadValues["post"];
                    string callerId = client.Name;

                    MessageHandler<Dictionary<string, string>> handler = new MessageHandler<Dictionary<string, string>>(qmessage)
                    {
                        ReceiveHandler = new Func< Dictionary<string, string>, Dictionary<string, string> >(QueueRepository.GetWaitingNumberAndPostSummary),
                        ResponseHandler = (session, result) =>
                        {
                            if (result != null && result.Count == 6)
                            {
                                string postPrefix   = result["postPrefix"];
                                string numberS      = result["number"];
                                string numberLeft   = result["numberLeft"];

                                // Send response to client(caller)
                                string messageC = 
                                    Msg.CallerGetNext.Text +
                                    Msg.Separator + "RES" +
                                    Msg.Separator + "Identifier" +
                                    Msg.Separator + postPrefix +
                                    Msg.CompDelimiter + numberS +
                                    Msg.CompDelimiter + numberLeft;

                                session.Send(messageC);


                                // on a valid number, send message to Display
                                if (!string.IsNullOrWhiteSpace(numberS))
                                {
                                    // Send message to Queue display, to update displayed number, and total queue
                                    string messageD = 
                                        Msg.DisplayCall.Text +
                                        Msg.Separator + "REQ" +
                                        Msg.Separator + "Identifier" +
                                        Msg.Separator + postPrefix +
                                        Msg.CompDelimiter + numberS +
                                        Msg.CompDelimiter + numberLeft +
                                        Msg.CompDelimiter + post +
                                        Msg.CompDelimiter + callerId; 

                                    QueueServer.SendMessageToQueueDisplay(messageD, post);

                                    // Now we want to delete processed number
                                    int numberI = Convert.ToInt32(numberS);
                                    QueueRepository.DeleteProcessedNumberFromQueue(numberI, callerId, post);



                                    // update total waiting queue all Caller
                                    string message2 = Msg.CallerUpdateQueueLeft.Text +
                                                     Msg.Separator + "REQ" +
                                                     Msg.Separator + "Identifier" +
                                                     Msg.Separator + post +
                                                     Msg.CompDelimiter + numberLeft;
                                    QueueServer.SendMessageToQueueCaller(message2, post);
                                }
                            }
                            else
                            {
                                /*
                                string messageC = 
                                    Msg.CallerGetNext.Text +
                                    Msg.Separator + "RES" +
                                    Msg.Separator + "Identifier" +
                                    Msg.Separator + "" +
                                    Msg.Separator + "" +
                                    Msg.CompDelimiter + "" +
                                    Msg.CompDelimiter + "";

                                session.Send(messageC);
                                */
                            }
                        }
                    };

                    handler.Process();
                }
                // Handle CallerRecall
                else if (qmessage.MessageType == Msg.CallerRecall && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string number    = qmessage.PayloadValues["number"];
                    string post      = qmessage.PayloadValues["post"];
                    string caller    = qmessage.PayloadValues["station"];
                    string postrefix = QueueRepository.GetPostNumberPrefix(post);

                    // Send message to Queue display, to update displayed number
                    // XXX is sent to replace total waiting queue 
                    string message = 
                        Msg.DisplayRecall.Text +
                        Msg.Separator + "REQ" +
                        Msg.Separator + "Identifier" +
                        Msg.Separator + postrefix +
                        Msg.CompDelimiter + number +
                        Msg.CompDelimiter + "XXX" +
                        Msg.CompDelimiter + post +
                        Msg.CompDelimiter + caller;

                    QueueServer.SendMessageToQueueDisplay(message, post);


                    string message1 =
                        Msg.CallerRecall.Text +
                        Msg.Separator + "RES" +
                        Msg.Separator + "Identifier" +
                        Msg.Separator + number +
                        Msg.CompDelimiter + post +
                        Msg.CompDelimiter + postrefix +
                        Msg.CompDelimiter + caller;
                    QueueServer.SendMessageToQueueCaller(post, post);
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

            if (exp != null)
            {
                Logger.Log("CallerHandler", exp);

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
