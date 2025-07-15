#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2015-2025  Jefri Sibarani
 
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

using MySqlConnector;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Tobasa
{
    public enum DatabaseProviderType
    {
        SQLITE,
        MYSQL,
        PGSQL,
        MSSQL,
        UNKNOWN
    }

    public class Database : Notifier
    {
        public static string DEFAULT_MYSQL_CONNSTRING           = "Data Source=127.0.0.1,3306;User ID=antrian;Initial Catalog=antri;";
        public static string DEFAULT_MYSQL_CONNSTRING_PASSWORD  = "ad7415644add93d6e719d2b593da6e6e";
        
        public static string DEFAULT_PGSQL_CONNSTRING           = "Host=127.0.0.1;Username=antrian;Database=antri;Port=5432;";
        public static string DEFAULT_PGSQL_CONNSTRING_PASSWORD  = "ad7415644add93d6e719d2b593da6e6e";
        
        public static string DEFAULT_MSSQL_CONNSTRING           = "Server=127.0.0.1,1433;Database=antri;User ID=antrian;Trusted_Connection=False;";
        public static string DEFAULT_MSSQL_CONNSTRING_PASSWORD  = "ad7415644add93d6e719d2b593da6e6e";
        
        public static string DEFAULT_SQLITE_CONNSTRING          = "Data Source=.\\Database\\antri.db3;Version=3;";
        public static string DEFAULT_SQLITE_CONNSTRING_PASSWORD = "ad7415644add93d6e719d2b593da6e6e";
        
        public static string DEFAULT_SECURITY_SALT              = "C4BC3A3AC2D6D367A74580388B20BC069C96B048DFEAF5CCDC0CE1E25BF23F39";

        /* A Singleton instance  */
        private static readonly Database _instance = new Database();

        private Database() 
        {
            _providerType = DatabaseProviderType.SQLITE;
        }

        public static Database Me
        {
            get { return _instance; }
        }

        private DatabaseProviderType _providerType;
        private bool _connected = false;
        private DbConnection _conn;
        private string _databaseName = "";

        public void SetProvider(string provider)
        {
            if (provider == "SQLITE")
                ProviderType = DatabaseProviderType.SQLITE;
            else if (provider == "MYSQL")
                ProviderType = DatabaseProviderType.MYSQL;
            else if (provider == "MSSQL")
                ProviderType = DatabaseProviderType.MSSQL;
            else if (provider == "PGSQL")
                ProviderType = DatabaseProviderType.PGSQL;
            else
                ProviderType = DatabaseProviderType.UNKNOWN;
        }

        public string GetConnectionString(string partialConnStr, string salt, string encryptedPwd = null)
        {
            if (ProviderType == DatabaseProviderType.SQLITE)
            {
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
                builder.ConnectionString = partialConnStr;
                return builder.ToString();
            }
            else if (ProviderType == DatabaseProviderType.MYSQL)
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.ConnectionString = partialConnStr;
                string clearPwd = Util.DecryptPassword(encryptedPwd, salt);
                builder.Add("Password", clearPwd);
                return builder.ToString();
            }
            else if (ProviderType == DatabaseProviderType.MSSQL)
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.ConnectionString = partialConnStr;
                string clearPwd = Util.DecryptPassword(encryptedPwd, salt);
                builder.Password = clearPwd;
                return builder.ToString();
            }
            else if (ProviderType == DatabaseProviderType.PGSQL)
            {
                NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
                builder.ConnectionString = partialConnStr;
                string clearPwd = Util.DecryptPassword(encryptedPwd, salt);
                builder.Add("Password", clearPwd);
                return builder.ToString();
            }
            else
                return "";
        }

        public bool Connect(string connString)
        {
            if (!_connected)
            {
                try
                {
                    if (_providerType == DatabaseProviderType.SQLITE)
                        _conn = new SQLiteConnection(connString);
                    else if (_providerType == DatabaseProviderType.MYSQL)
                        _conn = new MySqlConnection(connString);
                    else if (_providerType == DatabaseProviderType.MSSQL)
                        _conn = new SqlConnection(connString);
                    else if (_providerType == DatabaseProviderType.PGSQL)
                        _conn = new NpgsqlConnection(connString);
                    else
                    {
                        throw new AppException("Unsupported database provider");
                    }
                    
                    _conn.Open();
                    _databaseName = _conn.Database;
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

            return _connected;
        }

        private bool RetryConnectWithDefaults()
        {
            try
            {
                string conString;
                if (_providerType == DatabaseProviderType.SQLITE)
                {
                    conString = GetConnectionString(
                        DEFAULT_SQLITE_CONNSTRING,
                        DEFAULT_SECURITY_SALT,
                        DEFAULT_MSSQL_CONNSTRING_PASSWORD);

                    _conn = new SQLiteConnection(conString);
                    _conn.Open();
                    _connected = true;
                }
                else if (_providerType == DatabaseProviderType.MYSQL)
                {
                    conString = GetConnectionString(
                        DEFAULT_MYSQL_CONNSTRING,
                        DEFAULT_SECURITY_SALT,
                        DEFAULT_MYSQL_CONNSTRING_PASSWORD);

                    _conn = new MySqlConnection(conString);
                    _conn.Open();
                    _connected = true;
                }
                else if (_providerType == DatabaseProviderType.MSSQL)
                {
                    conString = GetConnectionString(
                        DEFAULT_MSSQL_CONNSTRING,
                        DEFAULT_SECURITY_SALT,
                        DEFAULT_MSSQL_CONNSTRING_PASSWORD);

                    _conn = new SqlConnection(conString);
                    _conn.Open();
                    _connected = true;
                }
                else if (_providerType == DatabaseProviderType.PGSQL)
                {
                    conString = GetConnectionString(
                        DEFAULT_PGSQL_CONNSTRING,
                        DEFAULT_SECURITY_SALT,
                        DEFAULT_PGSQL_CONNSTRING_PASSWORD);

                    _conn = new NpgsqlConnection(conString);
                    _conn.Open();
                    _connected = true;
                }
                else
                {
                    throw new AppException("Unsupported database provider");
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
                    _conn.Close();

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

        public DatabaseProviderType ProviderType
        {
            get { return _providerType; }
            set { _providerType = value; }
        }

        public string DatabaseProviderTypeString()
        {
            if (ProviderType == DatabaseProviderType.SQLITE)
                return "SQLite";
            else if (ProviderType == DatabaseProviderType.MYSQL)
                return "MySQL";
            else if (ProviderType == DatabaseProviderType.MSSQL)
                return "MSSQL";
            else if (ProviderType == DatabaseProviderType.PGSQL)
                return "PostgreSQL";
            else
                return "";
        }

        public DbConnection Connection
        {
            get { return _conn; }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
        }

        public DbDataAdapter CreateDataAdapter(string sql)
        {
            if (_connected)
            {
                try
                {
                    DbDataAdapter adapter;
                    if (ProviderType == DatabaseProviderType.SQLITE)
                        adapter = new SQLiteDataAdapter(sql, (SQLiteConnection)_conn);
                    else if (ProviderType == DatabaseProviderType.MYSQL)
                        adapter = new MySqlDataAdapter(sql, (MySqlConnection)_conn);
                    else if (ProviderType == DatabaseProviderType.MSSQL)
                        adapter = new SqlDataAdapter(sql, (SqlConnection)_conn);
                    else if (ProviderType == DatabaseProviderType.PGSQL)
                        adapter = new NpgsqlDataAdapter(sql, (NpgsqlConnection)_conn);
                    else
                        throw new AppException("Unsupported database provider");

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
                    if (ProviderType == DatabaseProviderType.SQLITE)
                    {
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, (SQLiteConnection)_conn);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                    }
                    else if (ProviderType == DatabaseProviderType.MYSQL)
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(sql, (MySqlConnection)_conn);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                    }
                    else if (ProviderType == DatabaseProviderType.MSSQL)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, (SqlConnection)_conn);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                    }
                    else if (ProviderType == DatabaseProviderType.PGSQL)
                    {
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, (NpgsqlConnection)_conn);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                    }
                    else
                        throw new AppException("Unsupported database provider");

                    return dataTable;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return null;
        }

        public DbParameter AddParameterBool(DbCommand command, string paramName, bool value)
        {
            int val = value == true ? 1 : 0;
            return AddParameter(command, paramName, val, DbType.Int32);
        }

        public DbParameter AddParameter(DbCommand command, string paramName, object value, DbType dataType)
        {
            // Adjust parameter prefix for specific providers
            if (ProviderType == DatabaseProviderType.MYSQL)
            {
                //paramName = $"?{paramName}";  // MySQL uses `?name`
                paramName = $"@{paramName}";  // MySQL uses @name in ADO.NET (not ?name)
            }
            else if (ProviderType == DatabaseProviderType.MSSQL)
            {
                paramName = $"@{paramName}";  // SQL Server uses @name
            }
            else
            {
                paramName = $"@{paramName}";  // PostgreSQL, SQLite, etc.
            }

            DbParameter param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value ?? DBNull.Value; // Ensure null safety
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
                            return res;
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

            if (Database.Me.ProviderType == DatabaseProviderType.MSSQL)
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
            if (ProviderType == DatabaseProviderType.SQLITE)
            {
                sql = "SELECT date('now','localtime')";
            }
            else if (ProviderType == DatabaseProviderType.MYSQL)
            {
                sql = "SELECT CURDATE()";
            }
            else if(ProviderType == DatabaseProviderType.MSSQL)
            {
                sql = "SELECT CAST(getdate() AS date)";
            }
            else if (ProviderType == DatabaseProviderType.PGSQL)
            {
                sql = "SELECT CURRENT_DATE";
            }
            else
            {
                throw new AppException("Unsupported database provider");
            }

            Database.Me.OpenConnection();
            using (DbCommand cmd = Database.Me.Connection.CreateCommand())
            {
                cmd.CommandText = sql;
                var res = cmd.ExecuteScalar();
                if (res != null)
                    currentDate = res.ToString();
            }

            return currentDate;
        }

        public string GetDateTime()
        {
            string sql;
            string currentDate = null;
            if (ProviderType == DatabaseProviderType.SQLITE)
            {
                sql = "SELECT strftime('%Y-%m-%d %H:%M:%f','now', 'localtime')";
            }
            else if (ProviderType == DatabaseProviderType.MYSQL)
            {
                sql = "SELECT CURRENT_TIMESTAMP";
            }
            else if (ProviderType == DatabaseProviderType.MSSQL)
            {
                sql = "SELECT getdate()";
            }
            else if (ProviderType == DatabaseProviderType.MSSQL)
            {
                sql = "SELECT CURRENT_TIMESTAMP";
            }
            else
            {
                throw new AppException("Unsupported database provider");
            }

            Database.Me.OpenConnection();
            using (DbCommand cmd = Database.Me.Connection.CreateCommand())
            {
                cmd.CommandText = sql;
                var res = cmd.ExecuteScalar();
                if (res != null)
                    currentDate = res.ToString();
            }

            return currentDate;
        }

        public string GetDateTimeSqlString()
        {
            if (ProviderType == DatabaseProviderType.SQLITE)
            {
                return "strftime('%Y-%m-%d %H:%M:%f','now', 'localtime')";
            }
            else if (ProviderType == DatabaseProviderType.MYSQL)
            {
                return "CURRENT_TIMESTAMP";
            }
            else if (ProviderType == DatabaseProviderType.MSSQL)
            {
                return "getdate()";
            }
            else if (ProviderType == DatabaseProviderType.MSSQL)
            {
                return "CURRENT_TIMESTAMP";
            }
            else
                throw new AppException("Unsupported database provider");
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
