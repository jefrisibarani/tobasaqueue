using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Tobasa
{
    class SysHandler
    {
        public SysHandler()
        {
        }

        public void OnMessage(DataReceivedEventArgs arg, Client client)
        {
            if (!client.LoggedIn)
                return;

            Exception exp = null;
            try
            {
                Message qmessage = new SysMessage(arg);
                Logger.Log("[SysHandler] Processing " + qmessage.MessageType.Text + " from " + client.RemoteInfo);

                // Handle SysInsTable and SysUpdTable
                if ( (qmessage.MessageType == Msg.SysInsTable || qmessage.MessageType == Msg.SysUpdTable) && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string tableName = qmessage.PayloadValues["tablename"];
                    //string param   = qmessage.PayloadValues["parameter"];

                    MessageHandler<Tuple<bool, int>> handler = new MessageHandler<Tuple<bool, int>>(qmessage)
                    {
                        ReceiveHandler = new Func< Dictionary<string, string>, Tuple<bool, int> >(QueueRepository.InsertUpdateTable),
                        ResponseHandler = (session, resultT) =>
                        {
                            // Send response to client
                            if (resultT != null)
                            {
                                string result    = resultT.Item1 ? "OK" : "FAIL";
                                string affected  = resultT.Item2.ToString();
                                string messsageT = "";

                                if (qmessage.MessageType == Msg.SysInsTable)
                                    messsageT = Msg.SysInsTable.Text;
                                else
                                    messsageT = Msg.SysUpdTable.Text;

                                // SYS|INS_TABLE|RES|Identifier|[Name!Result!Affected]
                                string message = messsageT +
                                                 Msg.Separator + "RES" +
                                                 Msg.Separator + "Identifier" +
                                                 Msg.Separator + tableName +        // tablename
                                                 Msg.CompDelimiter + result +       // result
                                                 Msg.CompDelimiter + affected;      // affected

                                session.Send(message);
                            }
                        }
                    };

                    handler.Process();
                }

                // Handle SysDelTable
                if (qmessage.MessageType == Msg.SysDelTable && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string tableName = qmessage.PayloadValues["tablename"];
                    //string param     = qmessage.PayloadValues["param"];

                    MessageHandler<Tuple<bool, int>> handler = new MessageHandler<Tuple<bool, int>>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, Tuple<bool, int>>(QueueRepository.DeleteTable),
                        ResponseHandler = (session, resultT) =>
                        {
                            // Send response to client
                            if (resultT != null)
                            {
                                string result = resultT.Item1 ? "OK" : "FAIL";
                                string affected = resultT.Item2.ToString();

                                // SYS|DEL_TABLE|RES|Identifier|[Name!Result!Affected]
                                string message = Msg.SysDelTable.Text +
                                                 Msg.Separator + "RES" +
                                                 Msg.Separator + "Identifier" +
                                                 Msg.Separator + tableName +        // tablename
                                                 Msg.CompDelimiter + result +       // result
                                                 Msg.CompDelimiter + affected;      // affected

                                session.Send(message);
                            }
                        }
                    };

                    handler.Process();
                }

                // Handle SysGetTable
                if (qmessage.MessageType == Msg.SysGetTable && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string tableName = qmessage.PayloadValues["tablename"];

                    MessageHandler<Dictionary<string, string>> handler = new MessageHandler<Dictionary<string, string>>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, Dictionary<string, string>>(QueueRepository.GetTable),
                        ResponseHandler = (session, resultDict) =>
                        {
                            // Send response to client
                            if (resultDict != null)
                            {
                                string totalRow  = resultDict["totalrow"];
                                string jsonTable = resultDict["jsontable"];

                                //if (string.IsNullOrWhiteSpace(jsonTable))
                                //    return;

                                // SYS|GET_TABLE|RES|Identifier|[Table!Result!TotalRow]
                                string message = Msg.SysGetTable.Text +
                                                 Msg.Separator + "RES" +
                                                 Msg.Separator + "Identifier" +
                                                 Msg.Separator + tableName +
                                                 Msg.CompDelimiter + jsonTable +
                                                 Msg.CompDelimiter + totalRow;

                                session.Send(message);
                            }
                        }
                    };

                    handler.Process();
                }

                // Handle SysUpdateJob
                if (qmessage.MessageType == Msg.SysUpdateJob && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string status = qmessage.PayloadValues["status"];

                    MessageHandler<bool> handler = new MessageHandler<bool>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, bool>(QueueRepository.UpdateJob),
                        ResponseHandler = (session, success) =>
                        {
                            // Send response to client
                            if (success)
                            {
                                // SYS|UPDATE_JOB|RES|Identifier|Status!Result
                                string message = Msg.SysUpdateJob.Text +
                                                 Msg.Separator + "RES" +
                                                 Msg.Separator + "Identifier" +
                                                 Msg.Separator + status +
                                                 Msg.CompDelimiter + "OK";

                                session.Send(message);

                                // send command to Module Display to update job status
                                string jobNo = qmessage.PayloadValues["jobno"];

                                ShowMessageJobFinishedOnDisplay(jobNo, client.Name, client.Post);

                                // get data from all post 
                                UpdateJobsStatusOnDisplay(client.Name, "");
                                //UpdateJobsStatusOnDisplay(client.Name, client.Post);
                            }
                            
                        }
                    };

                    handler.Process();
                }

                // Handle SysGetJob
                if (qmessage.MessageType == Msg.SysGetJob && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string post   = qmessage.PayloadValues["post"];
                    string status = qmessage.PayloadValues["status"];

                    // Make sure client post and requested post equal
                    if (post != client.Post)
                        throw new AppException("Client post mismatch requested post");

                    MessageHandler<string> handler = new MessageHandler<string>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, string>(QueueRepository.GetJob),
                        ResponseHandler = (session, jsonTable) =>
                        {
                            // Send response to client

                            //if (string.IsNullOrWhiteSpace(jsonTable))
                            //    return;

                            // SYS|GET_JOB|RES|Identifier|Post!Status!JsonResult
                            string message = Msg.SysGetJob.Text +
                                             Msg.Separator + "RES" +
                                             Msg.Separator + "Identifier" +
                                             Msg.Separator + client.Post +
                                             Msg.CompDelimiter + status +
                                             Msg.CompDelimiter + jsonTable;

                            session.Send(message);
                        }
                    };

                    handler.Process();
                }


                // Handle SysGetQueueSummary
                if (qmessage.MessageType == Msg.SysGetQueueSummary && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string post = qmessage.PayloadValues["post"];

                    MessageHandler<string> handler = new MessageHandler<string>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, string>(QueueRepository.GetQueueSummary),
                        ResponseHandler = (session, jsonTable) =>
                        {
                            // Send response to client
                            // SYS|GET_QUEUE_SUMMARY|RES|Identifier|Post!JsonResult
                            string message = Msg.SysGetQueueSummary.Text +
                                             Msg.Separator + "RES" +
                                             Msg.Separator + "Identifier" +
                                             Msg.Separator + client.Post +
                                             Msg.CompDelimiter + jsonTable;

                            session.Send(message);
                        }
                    };

                    handler.Process();
                }


                // Handle SysGetList
                if (qmessage.MessageType == Msg.SysGetList && qmessage.Direction == MessageDirection.REQUEST)
                {
                    string name   = qmessage.PayloadValues["name"];

                    MessageHandler<List<string>> handler = new MessageHandler<List<string>>(qmessage)
                    {
                        ReceiveHandler = new Func<Dictionary<string, string>, List<string>>(QueueRepository.GetList),
                        ResponseHandler = (session, list) =>
                        {
                            // Send response to client
                            if (list!=null)
                            {
                                string jsonList = JsonConvert.SerializeObject(list, Formatting.None);

                                // SYS|GET_LIST|RES|Identifier|[Result]
                                string message = Msg.SysGetList.Text +
                                                 Msg.Separator + "RES" +
                                                 Msg.Separator + "Identifier" +
                                                 Msg.Separator + jsonList;
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
                Logger.Log("SysHandler", exp);

                // SYS|NOTIFY|[Type!Message]
                string message = 
                    Msg.SysNotify.Text + 
                    Msg.Separator + "ERROR" + 
                    Msg.CompDelimiter + exp.Message;

                client.Session.Send(message);
            }
        }

        // Send command to Module Display to update job status
        private void ShowMessageJobFinishedOnDisplay(string jobNo, string stationName, string stationPost)
        {
            string message =
                Msg.DisplayShowInfo.Text +
                Msg.Separator + "REQ" +
                Msg.Separator + "Identifier" +
                Msg.Separator + stationPost +
                Msg.CompDelimiter + stationName +
                Msg.CompDelimiter + "Antrian no. " + jobNo + ", telah selesai diproses";

            QueueServer.SendMessageToQueueDisplay(message, stationPost);
        }

        // Send message to Queue display, to update displayed finished job/antrian
        // if stationPost empty, get all posts
        private void UpdateJobsStatusOnDisplay(string stationName, string stationPost="")
        {
            string csvList = QueueRepository.GetFinishedJobInCsvList(stationPost);

            string message = 
                Msg.DisplaySetFinished.Text +
                Msg.Separator + "REQ" +
                Msg.Separator + "Identifier" +
                Msg.Separator + stationPost +
                Msg.CompDelimiter + stationName +
                Msg.CompDelimiter + csvList;

            QueueServer.SendMessageToQueueDisplay(message, stationPost);
        }
    }
}
