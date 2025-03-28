#region License
/*
    Sotware Antrian Tobasa
    Copyright (C) 2015-2025  Jefri Sibarani

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
        public Client(NetSession ses)
        {
            Session = ses;
            Type = ClientType.QueueCaller;
        }

        public bool ReceiveMessageFromOtherPost { get; set; } = true;

        public bool LoggedIn { get; set; } = false;

        public int Id
        {
            get
            {
                if (Session != null)
                    return Session.Id;
                else
                    return -1;
            }
        }

        public ClientType Type { get; set; }

        public NetSession Session { get; } = null;

        public string Name { get; set; } = string.Empty;

        public string Post { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

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

        public string RemoteInfo
        {
            get { return Session.RemoteInfo; }
        }

        public void Close()
        {
            if (Session != null)
                Session.Close();
        }
    }
}
