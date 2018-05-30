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
using System.Data;
using System.Data.OleDb;

using System.Windows.Forms;

namespace Tobasa
{
    public class Database
    {
        /* A Singleton class  */

        private static readonly Database _instance = new Database();
        private Database() { }
        public static Database Me
        {
            get { return _instance; }
        }

        private bool _connected = false;
        private OleDbConnection _conn;

        public bool Connect(string connString)
        {
            if (!_connected)
            {
                try
                {
                    _conn = new OleDbConnection(connString);
                    _conn.Open();
                    _connected = true;
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show("Error Connecting to database :" + Environment.NewLine + e.Message, "Error Connecting to database", MessageBoxButtons.OK);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Connecting to database :" + Environment.NewLine + e.Message, "Error Connecting to database", MessageBoxButtons.OK);
                }
            }
            return _connected;
        }

        public bool Disconnect()
        {
            if (_conn != null)
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
                _connected = false;
            }
            else
                _connected = false;

            return !_connected;
        }

        public void OpenConnection()
        {
            if (_conn.State != ConnectionState.Open)
                _conn.Open();
        }

        public bool Connected
        {
            get { return _connected; }
        }

        public OleDbConnection Connection
        {
            get { return _conn; }
        }
    }
}
