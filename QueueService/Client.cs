#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2018  Jefri Sibarani

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System;

namespace Tobasa
{
    public enum ClientType
    {
        QueueDisplay,
        QueueTicket,
        QueueCaller,
        Unknown
    }

    public class Client
    {
        private NetSession session = null;
        private ClientType cType;
        private string name = String.Empty;
        private string post = String.Empty;
        private string userName = String.Empty;
        private string password = String.Empty;
        private bool loggedIn = false;
        private bool receiveMessageFromOtherPost = true;

        public Client(NetSession ses)
        {
            session = ses;
            cType = ClientType.QueueCaller;
        }

        public bool ReceiveMessageFromOtherPost
        {
            get { return receiveMessageFromOtherPost; }
            set { receiveMessageFromOtherPost = value; }
        }

        public bool LoggedIn
        {
            get { return loggedIn; }
            set { loggedIn = value; }
        }

        public int Id
        {
            get
            {
                if (session != null)
                    return session.Id;
                else
                    return -1;
            }
        }

        public ClientType Type
        {
            get { return cType; }
            set { cType = value; }
        }

        public NetSession Session
        {
            get { return session; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Post
        {
            get { return post; }
            set { post = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public static ClientType ClientTypeFromString(string val)
        {
            // possible value for val : CALLER,DISPLAY,TICKET

            ClientType type = ClientType.Unknown;

            if (val == null)
                return type;
            
            if (val == "CALLER")
                type = ClientType.QueueCaller;
            else if (val == "DISPLAY")
                type = ClientType.QueueDisplay;
            else if (val == "TICKET")
                type = ClientType.QueueTicket;
            else
                type = ClientType.Unknown;

            return type;
        }

        public void Close()
        {
            if (session != null)
                session.Close();
        }
    }
}
