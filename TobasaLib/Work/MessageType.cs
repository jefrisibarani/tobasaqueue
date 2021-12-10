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

using System.Collections.Generic;

namespace Tobasa
{
    public static class Msg
    {
        #region Message separator and component delimiter

        // Message's token Separator
        private static char[] _separator = new char[] { '|' };
        
        // Message's Payload/Component Delimiter
        private static char[] _compDelimiter = new char[] { '!' };  

        public static string Separator
        {
            get { return new string(_separator); }
            set { _separator = value.ToCharArray(); }
        }

        public static string CompDelimiter
        {
            get { return new string(_compDelimiter); }
            set { _compDelimiter = value.ToCharArray(); }
        }

        #endregion

        #region Message Type
        /***
                               ** QueueMessage stucture **

        Example:         SYS|UPDATE_JOB|REQ|Identifier|[Status!JobId!JobNo]
        Token position: | 0 |     1    | 2 |     3    |       4            |
                        |                             |                    |
                         \--------- Envelope ---------+-------Payload-----/

        Envelope tokens's 1-4 token must be upper case, no space
        Identifier must contain no space
        Payload tokens must be lower case, no space

         ** ENVELOPE **
            Separator  : |
            1st token  : MODULE name
            2nd token  : COMMAND
            3rd token  : REQ => Request, send by client to QueueServer
                         RES => Result, send by QueueServer to client
                         Not all Message have REQ or RES type, for example Msg.SysNotify
            4th token  : Identifier => JWT token

         ** PAYLOAD **
            Separator  : ! 
            Position   : 4th token (zero based index)
         
         ** MODULE **
            Valid name : SYS, ADMIN, TICKET, DISPLAY, CALLER
        */

        public class Type 
        {
            public Type() {}

            public Type(
                string module, 
                string command, 
                string typeStr, 
                int totalToken,
                int payloadPosition,
                int totalRequestPayload,
                int totalResultPayload)
            {
                Module              = module;
                Command             = command;
                String              = typeStr;
                TotalToken          = totalToken;
                PayloadPosition     = payloadPosition;
                TotalRequestPayload = totalRequestPayload;
                TotalResultPayload  = totalResultPayload;
            }
            
            // Message type Module name
            public string Module
            {
                get; set;
            }
            
            // Message type command
            public string Command
            {
                get; set;
            }
            
            // Message type name
            public string String
            {
                get; set;
            }
            // Total token 
            public int TotalToken
            {
                get; set;
            }
            
            // Zero based Payload position in message
            public int PayloadPosition
            {
                get; set;
            }
            
            public int TotalRequestPayload
            {
                get; set;
            }
            
            public int TotalResultPayload
            {
                get; set;
            }
            
            public string Text
            {
                get
                {
                    return Module + Separator + Command;
                }
            }
            
            public bool RequestPayloadIsEmpty
            {
                get 
                {
                    return (_requestPayload.Count == 0);
                }
            }
            
            public bool ResultPayloadIsEmpty
            {
                get
                {
                    return (_resultPayload.Count == 0);
                }
            }
            
            public Type AddRequestPayload(int pos, string value)
            {
                _requestPayload.Add(pos, value);
                return this;
            }
            
            public Type AddResultPayload(int pos, string value)
            {
                _resultPayload.Add(pos, value);
                return this;
            }

            public Dictionary<int, string> RequestPayload
            {
                get => _requestPayload;
            }
            
            public Dictionary<int, string> ResultPayload
            {
                get => _resultPayload;
            }

            // Dictionary to store REQUEST payloads metadata
            // Dictionary's Key   is payload position in payload tokens  (int)
            // Dictionary's Value is payload name                        (string)
            private Dictionary<int, string> _requestPayload = new Dictionary<int, string>();

            // Dictionary to store RESULT payloads metadata
            // Dictionary's Key   is payload position in payload tokens  (int)
            // Dictionary's Value is payload name                        (string)
            private Dictionary<int, string> _resultPayload  = new Dictionary<int, string>();
        }

        private static Type CreateType(
                string module,
                string command,
                string typeStr,
                int totalToken,
                int payloadPosition,
                int totalRequestPayload,
                int totalResultPayload)
        {
            return new Type(module, 
                command, 
                typeStr, 
                totalToken, 
                payloadPosition, 
                totalRequestPayload, 
                totalResultPayload);
        }


        /** Login to QueueServer
        Payload     : 3rd token
        Syntax REQ  : SYS|LOGIN|REQ|[Module!Post!Station!Username!Password]
           Module   : Module name   => ADMIN, CALLER, TICKET, DISPLAY
           Post     : Post name     => POST1, POST2, POST3...
           Station  : Station Name  => CALLER#1, CALLER#2...
        Syntax RES  : SYS|LOGIN|RES|[Result!Data]
           Result   : OK or FAIL
           Data     : OK => Identifier, FAIL => Fail reason
        */
        public static readonly Type SysLogin = 
            CreateType("SYS", "LOGIN", "SysLogin", 4, 3, 5, 2)
                .AddRequestPayload(0, "module")
                .AddRequestPayload(1, "post")
                .AddRequestPayload(2, "station")
                .AddRequestPayload(3, "username")
                .AddRequestPayload(4, "password")
                .AddResultPayload(0, "result")
                .AddResultPayload(1, "data");


        /** Notify client for any Error, Warning and Info.
        Sender      : Only QueueServer can send this message
        Syntax      : SYS|NOTIFY|[Type!Message]
        Payload     : 2nd token
           Type     : ERROR,WARN,INFO
           Message  : Notification message
        
        Note: SysNotify doesn't have RES|RES, default to REQ
        */
        public static readonly Type SysNotify = 
            CreateType("SYS", "NOTIFY",    "SysNotify", 3, 2, 2, 0)
                .AddRequestPayload(0, "type")
                .AddRequestPayload(1, "message");


        /** Insert data to Table
        Payload     : 4th token
        Syntax GET  : SYS|INS_TABLE|REQ|Identifier|[Name!Param]
           Name     : Table name
           Param    : Json serialized table DTO

        Syntax RES  : SYS|INS_TABLE|RES|Identifier|[Name!Result!Affected]
           Name     : Table name
           Result   : OK or FAIL
           Affected : Total affected record
        */
        public static readonly Type SysInsTable = 
            CreateType("SYS", "INS_TABLE", "SysInsTable", 5, 4, 2, 3)
                .AddRequestPayload(0, "tablename")
                .AddRequestPayload(1, "parameter")
                .AddResultPayload(0, "tablename")
                .AddResultPayload(1, "result")
                .AddResultPayload(2, "affected");


        /** Update data to Table
        Payload     : 4th token
        Syntax GET  : SYS|UPD_TABLE|REQ|Identifier|[Name!Params]
           Name     : Table name
           Param    : Json serialized table DTO

        Syntax RES  : SYS|UPD_TABLE|RES|Identifier|[Name!Result!Affected]
           Name     : Table name
           Result   : OK or FAIL
           Affected : Total affected record
        */
        public static readonly Type SysUpdTable = 
            CreateType("SYS", "UPD_TABLE", "SysUpdTable", 5, 4, 2, 3)
                .AddRequestPayload(0, "tablename")
                .AddRequestPayload(1, "parameter")
                .AddResultPayload(0, "tablename")
                .AddResultPayload(1, "result")
                .AddResultPayload(2, "affected");


        /** Delete Table row
        Payload     : 4th token
        Syntax REQ  : SYS|DEL_TABLE|REQ|Identifier|[Name!Param0!Param1]
           Name     : Table name
           Param0   : First parameter
           Param1   : Second parameter
        Syntax RES  : SYS|DEL_TABLE|RES|Identifier|[Name!Result!Affected]
           Name     : Table name
           Result   : OK or FAIL
           Affected : Total affected record
        */
        public static readonly Type SysDelTable  = 
            CreateType("SYS", "DEL_TABLE", "SySDelTable", 5, 4, 3, 3)
                .AddRequestPayload(0, "tablename")
                .AddRequestPayload(1, "param0")
                .AddRequestPayload(2, "param1")
                .AddResultPayload(0, "tablename")
                .AddResultPayload(1, "result")
                .AddResultPayload(2, "affected");


        /** Get Table data
        Payload     : 4th token
        Syntax GET  : SYS|GET_TABLE|REQ|Identifier|[Table!Param0!Param1]
           Table    : Table name
           Param0   : Offset
           Param1   : Limit
        Syntax RES  : SYS|GET_TABLE|RES|Identifier|[Table!Result!TotalRow]
           Table    : Table name
           Result   : JSON Serialized DataTable
           TotalRow : Total rows in table
        */
        public static readonly Type SysGetTable  = 
            CreateType("SYS", "GET_TABLE", "SySGetTable", 5, 4, 3, 3)
                .AddRequestPayload(0, "tablename")
                .AddRequestPayload(1, "offset")
                .AddRequestPayload(2, "limit")
                .AddResultPayload(0, "tablename")
                .AddResultPayload(1, "result")
                .AddResultPayload(2, "totalrow");


        /** Update job/number status
        Payload     : 4th token
        Syntax REQ  : SYS|UPDATE_JOB|REQ|Identifier|[Status!JobId!JobNo]
           Status   : PROCESS, FINISHED, CLOSED
           JobId    : table jobs's id
           JobNo    : queue number
        Syntax RES  : SYS|UPDATE_JOB|RES|Identifier|[Status!Result]
           Status   : PROCESS, FINISHED, CLOSED
           Result   : OK or FAIL
        */
        public static readonly Type SysUpdateJob = 
            CreateType("SYS", "UPDATE_JOB", "SysUpdateJob", 5, 4, 3, 2)
                .AddRequestPayload(0, "status")
                .AddRequestPayload(1, "jobid")
                .AddRequestPayload(2, "jobno")
                .AddResultPayload(0, "status")
                .AddResultPayload(1, "result");


        /** Get Table data
        Payload     : 4th token
        Syntax REQ  : SYS|GET_JOB|REQ|Identifier|[Post!Status!Offset!Limit]
           Post     : Post name
           Status   : PROCESS, FINISHED, CLOSED
           Offset   : Offset
           Limit    : Limit
        Syntax RES  : SYS|GET_JOB|RES|Identifier|[Post!Status!Result]
           Post     : Post name
           Status   : PROCESS, FINISHED, CLOSED
           Result   : JSON Serialized DataTable
        */
        public static readonly Type SysGetJob = 
            CreateType("SYS", "GET_JOB",    "SysGetJob", 5, 4, 4, 3)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "status")
                .AddRequestPayload(2, "offset")
                .AddRequestPayload(3, "limit")
                .AddResultPayload(0, "post")
                .AddResultPayload(1, "status")
                .AddResultPayload(2, "result");


        /** Get basic summary of a Post, Issued by Modul Caller
        Payload     : 4th token
        Syntax REQ  : CALLER|GET_INFO|REQ|Identifier|[Post]
            Post    : Post name
        Syntax RES  : CALLER|GET_INFO|RES|Identifier|[Prefix!Number!Left]
            Prefix  : Post number prefix
            Number  : Next waiting number
            Left    : Total waiting queue
        */
        public static readonly Type CallerGetInfo = 
            CreateType("CALLER", "GET_INFO", "CallerGetInfo", 5, 4, 1, 3)
                .AddRequestPayload(0, "post")
                .AddResultPayload(0, "postprefix")
                .AddResultPayload(1, "number")
                .AddResultPayload(2, "numberleft");


        /** Get next waiting number , issued by Modul Caller
        Payload     : 4th token
        Syntax REQ  : CALLER|GET_NEXT|REQ|Identifier|[Post!Station]
            Post    : Post name
            Station : Station name
        Syntax RES  : CALLER|GET_NEXT|RES|Identifier|[Prefix!Number!Left]
            Prefix  : Post number prefix
            Number  : Next waiting number
            Left    : Total waiting queue
        */
        public static readonly Type CallerGetNext = 
            CreateType("CALLER", "GET_NEXT", "CallerGetNext", 5, 4, 2, 3)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "station")
                .AddResultPayload(0, "postprefix")
                .AddResultPayload(1, "number")
                .AddResultPayload(2, "numberleft");


        /** Recall current number , issued by Modul Caller
        Payload     : 4th token
        Syntax REQ  : CALLER|RECALL|REQ|Identifier|[Number!Post!Station]
           Number   : Number to call 
           Post     : Post name
           Station  : Station name
        Syntax RES  : CALLER|RECALL|RES|Identifier|[Result]
           Result   : OK or FAIL
        */
        public static readonly Type CallerRecall = 
            CreateType("CALLER", "RECALL", "CallerRecall", 5, 4, 3, 1)
                .AddRequestPayload(0, "number")
                .AddRequestPayload(1, "post")
                .AddRequestPayload(2, "station")
                .AddResultPayload(0, "result");


        /** Create new queue number/ticket
        Payload     : 4th token
        Syntax REQ  : TICKET|CREATE|REQ|Identifier|[Post!Station]
           Post     : Post name
           Station  : Ticket Station Name
        Syntax RES  : TICKET|CREATE|RES|Identifier|[Prefix!Number!Post!Time]
           Prefix   : Post Prefixx
           Number   : New created number
           Post     : Post name
           Time     : creation timestamp
        */
        public static readonly Type TicketCreate = 
            CreateType("TICKET", "CREATE", "TicketCreate", 5, 4, 2, 4)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "station")
                .AddResultPayload(0, "postprefix")
                .AddResultPayload(1, "number")
                .AddResultPayload(2, "post")
                .AddResultPayload(3, "timestamp");

        /** Call a number.
        Issued by QueueServer to QueueDisplay, as a response to QueueCaller's Msg.CallerGetNext
        
        Payload     : 4th token
        Syntax REQ  : DISPLAY|CALL|REQ|Identifier|[Prefix!Number!Left!Post!Caller]
           Prefix   : Post Prefix
           Number   : Number to call
           Left     : Queue left
           Post     : Post name
           Caller   : Calling Caller ID/Name
        Syntax RES  : DISPLAY|CALL|RES|Identifier|[Status]
           Status   : OK or FAIL
        */
        public static readonly Type DisplayCall = 
            CreateType("DISPLAY", "CALL", "DisplayCall", 5, 4, 5, 1)
                .AddRequestPayload(0, "prefix")
                .AddRequestPayload(1, "number")
                .AddRequestPayload(2, "left")
                .AddRequestPayload(3, "post")
                .AddRequestPayload(4, "caller")
                .AddResultPayload(1, "status");


        /** Recall a number on Display.
        Issued by QueueServer to QueueDisplay, as a response to QueueCaller's Msg.CallerRecall
        
        Payload     : Position 4
        Syntax REQ  : DISPLAY|RECALL|REQ|Identifier|[Prefix!Number!Left!Post!Caller]
           Prefix   : Post Prefix
           Number   : Number to call
           Left     : Queue left
           Post     : Post name
           Caller   : Calling Caller ID/Name
        Syntax RES  : DISPLAY|RECALL|RES|Identifier|[Status]
           Status   : OK or FAIL
        */
        public static readonly Type DisplayRecall = 
            CreateType("DISPLAY", "RECALL", "DisplayRecall", 5, 4, 5, 1)
                .AddRequestPayload(0, "prefix")
                .AddRequestPayload(1, "number")
                .AddRequestPayload(2, "left")
                .AddRequestPayload(3, "post")
                .AddRequestPayload(4, "caller")
                .AddResultPayload(1, "status");

        /** Refresh finished jobs/numbers on Display
        Issued by QueueServer to QueueDisplay, in response to QueueCaller's Msg.SysUpdateJob
        
        Payload     : Position 4
        Syntax REQ  : DISPLAY|SET_FINISHED|REQ|Identifier|[Post!Caller|Data]
           Post     : Post name
           Caller   : Calling Caller ID/Name
           Data     : Comma Separated Values contain finished numbers
        Syntax RES  : DISPLAY|SET_FINISHED|RES|Identifier|[Status]
           Status   : OK or FAIL
        */
        public static readonly Type DisplaySetFinished = 
            CreateType("DISPLAY", "SET_FINISHED", "DisplaySetFinished", 5, 4, 3, 1)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "caller")
                .AddRequestPayload(2, "data")
                .AddResultPayload(1, "status");


        /** Show message on Display
        Issued by QueueServer to QueueDisplay, in response to QueueCaller's Msg.SysUpdateJob
        
        Payload     : Position 4
        Syntax REQ  : DISPLAY|SHOW_INFO|REQ|Identifier|[Post!Caller|Info]
           Post     : Post name
           Caller   : Calling Caller ID/Name
           Info     : Information 
        Syntax RES  : DISPLAY|SHOW_INFO|RES|Identifier|[Status]
           Status   : OK or FAIL
        */
        public static readonly Type DisplayShowInfo = 
            CreateType("DISPLAY", "SHOW_INFO", "DisplayShowInfo", 5, 4, 3, 1)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "caller")
                .AddRequestPayload(2, "info")
                .AddResultPayload(1, "status");

        /** Reset values on Display
        Payload     : None
        Sender      : 
        Syntax REQ  : DISPLAY|RESET_VALUES|REQ|Identifier|[Post!Station]
        Syntax REQ  : DISPLAY|RESET_VALUES|RES|Identifier|[Status]
        */
        public static readonly Type DisplayResetValues = 
            CreateType("DISPLAY", "RESET_VALUES", "DisplayResetValues", 5, 4, 2, 1)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "station")
                .AddResultPayload(0, "result");


        /** Reset running text on Display
        Payload     : None
        Sender      : 
        Syntax REQ  : DISPLAY|RESET_RUNTEXT|REQ|Identifier|[Post!Station]
        Syntax RES  : DISPLAY|RESET_RUNTEXT|RES|Identifier|[Status]
        */
        public static readonly Type DisplayResetRunText = 
            CreateType("DISPLAY", "RESET_RUNTEXT", "DisplayResetRunText", 5, 4, 2, 1)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "station")
                .AddResultPayload(0, "result");


        /** Delete running text on Display
        Payload     : Position 4
        Sender      :
        Syntax REQ  : DISPLAY|DEL_RUNTEXT|REQ|Identifier|[Post!Station!Text]
        Syntax RES  : DISPLAY|DEL_RUNTEXT|RES|Identifier|[Status]
        */
        public static readonly Type DisplayDelRunText = 
            CreateType("DISPLAY", "DEL_RUNTEXT", "DisplayDelRunText", 5, 4, 3, 1)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "station")
                .AddRequestPayload(2, "text")
                .AddResultPayload(0, "status");


        /** Request running text from QueueServer.
        This message sent by Display, QueueServer return running text one by one

        Payload     : 4th token
        Syntax REQ  : DISPLAY|GET_RUNTEXT|REQ|Identifier|[Post!Station]
            Post    : Post name
        Syntax RES  : DISPLAY|GET_RUNTEXT|RES|Identifier|[Result]
            Result  : Running text
        */
        public static readonly Type DisplayGetRunText = 
            CreateType("DISPLAY", "GET_RUNTEXT", "DisplayGetRunText", 5, 4, 2, 1)
                .AddRequestPayload(0, "post")
                .AddRequestPayload(1, "station")
                .AddResultPayload(0, "result");


        /** Get a List from Database
        Payload     : 4th token
        Syntax GET  : SYS|GET_LIST|REQ|Identifier|[Name]
           Name     : List name

        Syntax RES  : SYS|GET_LIST|RES|Identifier|[Result]
           Result   : JSON Serialized List<string>
        */
        public static readonly Type SysGetList = 
            CreateType("SYS", "GET_LIST", "SysGetList", 5, 4, 1, 1)
                .AddRequestPayload(0, "name")
                .AddResultPayload(0, "result");

        #endregion
    }
}