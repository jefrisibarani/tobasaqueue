using System;
using System.Collections.Generic;

namespace Tobasa
{
    class DisplayHandler
    {
        public DisplayHandler()
        {
        }

        public void OnMessage(DataReceivedEventArgs arg, Client client)
        {
            Exception exp = null;

            try
            {
                Message qmessage = new Message(arg);

                Logger.Log("[DisplayHandler] Processing " + qmessage.MessageType.Text + " from " + client.RemoteInfo);

                // Handle DisplayGetRunText message from QueueDisplay
                if (qmessage.MessageType == Msg.DisplayGetRunText && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string post    = qmessage.PayloadValues["post"];
                    string station = qmessage.PayloadValues["station"];

                    MessageHandler<List<string>> handler = new MessageHandler<List<string>>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, List<string>>(QueueRepository.GetStationRunningText),
                        ResponseHandler = (session, result) =>
                        {
                            if (result != null && result.Count >0 )
                            {
                                foreach( string text in result)
                                {
                                    string message = 
                                        Msg.DisplayGetRunText.Text +
                                        Msg.Separator + "RES" +
                                        Msg.Separator + "Identifier" +
                                        Msg.Separator + text;

                                    client.Session.Send(message);
                                }
                            }
                        }
                    };

                    handler.Process();
                }
                // DisplayResetRunText
                else if (qmessage.MessageType == Msg.DisplayResetRunText && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string post     = qmessage.PayloadValues["post"];
                    string station  = qmessage.PayloadValues["station"];

                    string message = 
                        Msg.DisplayResetRunText.Text +
                        Msg.Separator + "REQ" +
                        Msg.Separator + "Identifier" +
                        Msg.Separator + post +
                        Msg.CompDelimiter + station;
                    // Forward message to Display
                    QueueServer.SendMessageToQueueDisplay(message, post);
                }
                // DisplayDelRunText
                else if (qmessage.MessageType == Msg.DisplayDelRunText && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string post    = qmessage.PayloadValues["post"];
                    string station = qmessage.PayloadValues["station"];
                    string text    = qmessage.PayloadValues["text"];

                    string message = 
                        Msg.DisplayDelRunText.Text +
                        Msg.Separator + "REQ" +
                        Msg.Separator + "Identifier" +
                        Msg.Separator + post +
                        Msg.CompDelimiter + station +
                        Msg.CompDelimiter + text;
                    // Forward message to Display
                    QueueServer.SendMessageToQueueDisplay(message, post);
                }
                // DisplayResetValues
                else if (qmessage.MessageType == Msg.DisplayResetValues && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string post     = qmessage.PayloadValues["post"];
                    string station  = qmessage.PayloadValues["station"];

                    string message = 
                        Msg.DisplayResetValues.Text +
                        Msg.Separator + "REQ" +
                        Msg.Separator + "Identifier" +
                        Msg.Separator + post +
                        Msg.CompDelimiter + station;
                    // Forward to Display
                    QueueServer.SendMessageToQueueDisplay(message, post);
                }
                // Handle DisplayGetInfo
                else if (qmessage.MessageType == Msg.DisplayGetInfo && qmessage.Direction == MessageDirection.REQUEST)
                {
                    MessageHandler<Dictionary<string, string>> handler = new MessageHandler<Dictionary<string, string>>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, Dictionary<string, string>>(QueueRepository.GetLastProcessedNumberAndPostSummary),
                        ResponseHandler = (session, result) =>
                        {
                            // Send response to client
                            if (result != null && result.Count == 6)
                            {
                                string postPrefix = result["postPrefix"];
                                string numberS    = result["number"];
                                string numberLeft = result["numberLeft"];
                                string postId     = result["postId"];
                                string station    = result["station"];

                                // Send response to client(display)
                                string messageC =
                                    Msg.DisplayGetInfo.Text +
                                    Msg.Separator + "RES" +
                                    Msg.Separator + "Identifier" +
                                    Msg.Separator + postId +
                                    Msg.CompDelimiter + postPrefix +
                                    Msg.CompDelimiter + numberS +
                                    Msg.CompDelimiter + numberLeft +
                                    Msg.CompDelimiter + station;


                                session.Send(messageC);
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
                Logger.Log("DisplayHandler", exp);

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
