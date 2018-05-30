#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2018  Jefri Sibarani
 
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tobasa
{
    public static class Msg
    {
        #region Message separator

        private static char[] _separator = new char[] { '|' };  // Message Separator

        public static string Separator
        {
            get { return new string(_separator); }
            set { _separator = value.ToCharArray(); }
        }

        #endregion

        #region Messages format

        /// Message prefix
        /// Each message has one or more arguments
        public static string LOGIN_OK                     = "LOGIN" + Separator + "OK";
        public static string TICKET_SET_NEWNUMBER_NULL    = "TICKET" + Separator + "SET_NEWNUMBER" + Separator + "NULL";
        public static string TICKET_SET_NEWNUMBER         = "TICKET" + Separator + "SET_NEWNUMBER";
        public static string TICKET_CREATE_NEWNUMBER      = "TICKET" + Separator + "CREATE_NEWNUMBER";
        public static string CALLER_SET_NEXTNUMBER_NULL   = "CALLER" + Separator + "SET_NEXTNUMBER" + Separator + "NULL";
        public static string CALLER_SET_NEXTNUMBER        = "CALLER" + Separator + "SET_NEXTNUMBER";
        public static string CALLER_GET_NEXTNUMBER        = "CALLER" + Separator + "GET_NEXTNUMBER";
        public static string CALLER_RECALL_NUMBER         = "CALLER" + Separator + "RECALL_NUMBER";
        public static string DISPLAY_CALL_NUMBER          = "DISPLAY" + Separator + "CALL_NUMBER";
        public static string DISPLAY_RECALL_NUMBER        = "DISPLAY" + Separator + "RECALL_NUMBER";
        public static string DISPLAY_SHOW_MESSAGE         = "DISPLAY" + Separator + "SHOW_MESSAGE";
        public static string DISPLAY_UPDATE_JOB           = "DISPLAY" + Separator + "UPDATE_JOB";
        public static string DISPLAY_RESET_VALUES         = "DISPLAY" + Separator + "RESET_VALUES";
        public static string DISPLAY_SET_RUNNINGTEXT      = "DISPLAY" + Separator + "SET_RUNNINGTEXT";
        public static string DISPLAY_GET_RUNNINGTEXT      = "DISPLAY" + Separator + "GET_RUNNINGTEXT";
        public static string DISPLAY_RESET_RUNNINGTEXT    = "DISPLAY" + Separator + "RESET_RUNNINGTEXT";
        public static string DISPLAY_DELETE_RUNNINGTEXT   = "DISPLAY" + Separator + "DELETE_RUNNINGTEXT";
        
        #endregion

        #region Message Serializer/Deserializer

        /// Serializes an object to a binary representation, returned as a byte array.
        public static byte[] Serialize(object Object)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, Object);
                return stream.ToArray();
            }
        }

        /// Deserializes an object from a binary representation.
        public static object Deserialize(byte[] binaryObject)
        {
            using (MemoryStream stream = new MemoryStream(binaryObject))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }

        #endregion
    }

    #region Serializeable Message

    /// A message containing a single string.
    [Serializable]
    public class StringMessage
    {
        public string Message { get; set; }
    }

    /// A message with more information.
    [Serializable]
    public class ComplexMessage
    {
        public string Message { get; set; }

        public DateTimeOffset Time { get; set; }

        public Guid UniqueID { get; set; }
    }

    #endregion
}
