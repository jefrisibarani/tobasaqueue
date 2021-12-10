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

using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Tobasa
{
    public enum ProviderType
    {
        SQLITE,
        OLEDB
    }

    public class Database : Notifier
    {
        public static string DEFAULT_SQLSRV_CONNSTRING = "Provider=SQLOLEDB;Data Source=127.0.0.1,1433;User ID=antrian;Initial Catalog=antri;";
        public static string DEFAULT_SQLSRV_CONNSTRING_PASSWORD = "ad7415644add93d6e719d2b593da6e6e";
        public static string DEFAULT_SQLITE_CONNSTRING = "Data Source=..\\Database\\antri.db3;Version=3;";
        public static string DEFAULT_SQLITE_CONNSTRING_PASSWORD = "";
        public static string DEFAULT_SECURITY_SALT = "C4BC3A3AC2D6D367A74580388B20BC069C96B048DFEAF5CCDC0CE1E25BF23F39";

        /* A Singleton instance  */
        private static readonly Database _instance = new Database();

        private Database() 
        {
            _providerType = ProviderType.SQLITE;
        }

        public static Database Me
        {
            get { return _instance; }
        }

        private ProviderType _providerType;
        private bool _connected = false;
        private DbConnection _conn;

        public void SetProvider(string provider)
        {
            if (provider == "SQLITE")
                ProviderType = ProviderType.SQLITE;
            else
                ProviderType = ProviderType.OLEDB;
        }

        public string GetConnectionString(string partialConnStr, string salt, string encryptedPwd = null)
        {
            if (ProviderType == ProviderType.SQLITE)
            {
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
                builder.ConnectionString = partialConnStr;
                if ( ! string.IsNullOrWhiteSpace(encryptedPwd))
                {
                    string clearPwd = Util.DecryptPassword(encryptedPwd, salt);
                    builder.Add("Password", clearPwd);
                }
                return builder.ToString();
            }
            else
            {
                OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
                builder.ConnectionString = partialConnStr;
                string clearPwd = Util.DecryptPassword(encryptedPwd, salt);
                builder.Add("Password", clearPwd);
                return builder.ToString();
            }
        }

        public bool Connect(string connString)
        {
            if (!_connected)
            {
                try
                {
                    if (_providerType == ProviderType.SQLITE)
                        _conn = new SQLiteConnection(connString);
                    else
                        _conn = new OleDbConnection(connString);

                    _conn.Open();
                    _connected = true;
                }
                catch (ArgumentException e)
                {
                    OnNotifyLog("Database", e);

                    if (!Util.IsRunInGUI() && Environment.UserInteractive)
                        Console.WriteLine("\nError Connecting to SQL database: " + e.Message);
                    else
                        MessageBox.Show("Error Connecting to SQL database :" + Environment.NewLine + e.Message, 
                            "Database connection error", MessageBoxButtons.OK);

                    OnNotifyLog("Database", "Try connecting to database with default connection string");

                    if (!Util.IsRunInGUI() && Environment.UserInteractive)
                        Console.WriteLine("\nTry connecting to database with default connection string...");

                    if ( RetryConnectWithDefaults())
                    {
                        if (!Util.IsRunInGUI() && Environment.UserInteractive)
                            Console.WriteLine("\nConnected to SQL database with default connection string");

                        OnNotifyLog("Database", "Connected to SQL database with default connection string");
                    }
                }
                catch (AppException e)
                {
                    OnNotifyLog("Database", e);
                    
                    if (!Util.IsRunInGUI() && Environment.UserInteractive)
                        Console.WriteLine("\nFailed to connect with default connection string to SQL Database Provider: " + e.Message);
                    else
                        MessageBox.Show("Failed to connect with default connection string to SQL Database Provider: " + Environment.NewLine + e.Message, 
                            "Database connection error", MessageBoxButtons.OK);
                }
                catch (Exception e)
                {
                    OnNotifyLog("Database", e);

                    if (!Util.IsRunInGUI() && Environment.UserInteractive)
                        Console.WriteLine("\nError Connecting to SQL database: " + e.Message);
                    else
                        MessageBox.Show("Error Connecting to SQL database :" + Environment.NewLine + e.Message, 
                            "Database connection error", MessageBoxButtons.OK);
                }
            }

            string backendType = "SQLite";
            if (_providerType == ProviderType.OLEDB)
                backendType = "SQL Server";

            OnNotifyLog("Database", "Backend " + backendType + " Version " + _conn.ServerVersion);

            return _connected;
        }

        private bool RetryConnectWithDefaults()
        {
            try
            {
                string conString;
                if (_providerType == ProviderType.SQLITE)
                {
                    conString = GetConnectionString(
                        DEFAULT_SQLITE_CONNSTRING,
                        DEFAULT_SECURITY_SALT,
                        DEFAULT_SQLSRV_CONNSTRING_PASSWORD);

                    _conn = new SQLiteConnection(conString);
                    _conn.Open();
                    _connected = true;
                }
                else
                {
                    conString = GetConnectionString(
                        DEFAULT_SQLSRV_CONNSTRING,
                        DEFAULT_SECURITY_SALT,
                        DEFAULT_SQLSRV_CONNSTRING_PASSWORD);

                    _conn = new OleDbConnection(conString);
                    _conn.Open();
                    _connected = true;
                }
            }
            catch (Exception e)
            {
                // handle ArgumentException here!, otherwise we trapped in loop! 
                OnNotifyLog("Libtobasa", "Retry Connecting to database failed, error was: " + e.Message, e.GetType().Name);

                if (!Util.IsRunInGUI() && Environment.UserInteractive)
                    Console.WriteLine("\nRetry Connecting to database failed, error was: " + e.Message);
                else
                    MessageBox.Show("Retry Connecting to database failed, error was: " + Environment.NewLine + e.Message, 
                        "Database connection error", MessageBoxButtons.OK);
            }

            return Connected;
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

        public ProviderType ProviderType
        {
            get { return _providerType; }
            set { _providerType = value; }
        }

        public DbConnection Connection
        {
            get { return _conn; }
        }

        public DbDataAdapter CreateDataAdapter(string sql)
        {
            if (_connected)
            {
                try
                {
                    DbDataAdapter adapter;
                    if (ProviderType == ProviderType.SQLITE)
                        adapter = new SQLiteDataAdapter(sql, (SQLiteConnection)_conn);
                    else
                        adapter = new OleDbDataAdapter(sql, (OleDbConnection)_conn);

                    return adapter;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return null;
        }

        public DataTable CreateDataTable(string sql)
        {
            if (_connected)
            {
                try
                {
                    DataTable dataTable;
                    if (ProviderType == ProviderType.SQLITE)
                    {
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, (SQLiteConnection)_conn);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                    }
                    else
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter(sql, (OleDbConnection)_conn);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                    }

                    return dataTable;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return null;
        }

        public DbParameter AddParameter(DbCommand command, string paramName, object value, DbType dataType)
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value;
            param.DbType = dataType;

            command.Parameters.Add(param);
            return param;
        }

        public object ExecuteScalar(string sql)
        {
            if (_connected)
            {
                try
                {
                    Me.OpenConnection();
                    using (DbCommand cmd = Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        var res = cmd.ExecuteScalar();
                        if (res != null)
                        {
                            return res;
                        }
                    }
                }
                catch (Exception e)
                {
                    OnNotifyLog("Database", e);
                }
            }

            return null;
        }

        public string GetOffsetLimit(int offset, int limit)
        {
            string sql = "";

            if (Database.Me.ProviderType == ProviderType.OLEDB)
            {
                if (ServerVersionMajor > 10)
                {
                    // Available since SQL Server 2012
                    sql = string.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", offset, limit);
                }
            }
            else
                sql = string.Format(" LIMIT {0} OFFSET {1}", limit, offset);

            return sql;
        }

        public string GetDate()
        {
            string sql;
            string currentDate = null;
            if (ProviderType == ProviderType.SQLITE)
            {
                sql = "SELECT date('now','localtime')";
            }
            else
            {
                sql = "SELECT CAST(getdate() AS date)";
            }

            Database.Me.OpenConnection();
            using (DbCommand cmd = Database.Me.Connection.CreateCommand())
            {
                cmd.CommandText = sql;
                var res = cmd.ExecuteScalar();
                if (res != null)
                {
                    currentDate = res.ToString();
                }
            }

            return currentDate;
        }

        public string GetDateTime()
        {
            string sql;
            string currentDate = null;
            if (ProviderType == ProviderType.SQLITE)
            {
                sql = "SELECT strftime('%Y-%m-%d %H:%M:%f','now', 'localtime')";
            }
            else
            {
                sql = "SELECT getdate()";
            }

            Database.Me.OpenConnection();
            using (DbCommand cmd = Database.Me.Connection.CreateCommand())
            {
                cmd.CommandText = sql;
                var res = cmd.ExecuteScalar();
                if (res != null)
                {
                    currentDate = res.ToString();
                }
            }

            return currentDate;
        }

        public int ServerVersionMajor
        {
            get 
            {
                string version = _conn.ServerVersion;
                string[] versionDetails = version.Split(new string[] { "." }, StringSplitOptions.None);
                int versionNumber = int.Parse(versionDetails[0]);
                return versionNumber;
            }
        }
    }
}
