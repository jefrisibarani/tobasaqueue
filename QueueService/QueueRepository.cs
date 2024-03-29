﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Tobasa
{
    public class QueueRepository
    {
        #region Login stuffs
        public static bool CanLogin(string staName, string staPost, out string reasonOut)
        {
            bool canLogin = false;
            string reason = "QueueServer is not connected to database";

            if (Database.Me.Connected)
            {
                string sql = "SELECT canlogin FROM stations WHERE name = ? AND post = ?";
                try
                {
                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        Database.Me.AddParameter(cmd, "name", staName, DbType.String);
                        Database.Me.AddParameter(cmd, "post", staPost, DbType.String);

                        var res = cmd.ExecuteScalar();
                        if (res != null)
                        {
                            canLogin = Convert.ToBoolean(res);
                            if (canLogin)
                                reason = "SUCCESS";
                            else
                                reason = "Station is not allowed to login to server";
                        }
                        else
                            reason = "Station is not registered in database";
                    }
                }
                catch (Exception e)
                {
                    throw new AppException("CanLogin: " + e.Message);
                }
            }

            reasonOut = reason;

            return canLogin;
        }

        public static bool Login(string userName, string password, out string reasonOut)
        {
            bool ok = false;
            string reason = "QueueServer is not connected to database";

            if (Database.Me.Connected)
            {
                string sql = "SELECT username, password, expired, active FROM logins WHERE username = ?";
                try
                {
                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        Database.Me.AddParameter(cmd, "username", userName, DbType.String);

                        using (DbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();

                                string user      = reader.GetString(0).Trim();
                                string pwd       = reader.GetString(1).Trim();
                                DateTime expired = reader.GetDateTime(2);
                                bool active      = reader.GetBoolean(3);

                                if (!active)
                                {
                                    reason = "Inactive user";
                                    ok = false;
                                }
                                else if (DateTime.Now > expired)
                                {
                                    reason = "User expired";
                                    ok = false;
                                }
                                else if (pwd != password)
                                {
                                    reason = "Wrong password";
                                    ok = false;
                                }
                                else if (pwd == password)
                                {
                                    reason = "Succesfully Logged in";
                                    ok = true;
                                }
                            }
                            else
                                reason = "User is not registered in database";
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new AppException("Login: " + e.Message);
                }
            }

            reasonOut = reason;

            return ok;
        }

        #endregion


        #region Insert or Update Queue sys table

        public static Tuple<bool, int> InsertUpdateTable(Dictionary<string, string> payload)
        {
            try
            {
                string tableName     = payload["tablename"];
                string jsonParameter = payload["parameter"];
                var paramDict = (Dictionary<string, string>)JsonConvert.DeserializeObject(jsonParameter, (typeof(Dictionary<string, string>)));

                bool success = false;
                int affected = -1;

                if (tableName == "runningtexts")
                {
                    success = InsertUpdateRunningText(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == "ipaccesslists")
                {
                    success = InsertUpdateAccessList(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == "stations")
                {
                    success = InsertUpdateStation(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == "posts")
                {
                    success = InsertUpdatePost(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == "logins")
                {
                    success = InsertUpdateLogin(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else
                    throw new Exception("Invalid table name: " + tableName);
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("InsertUpdateTable: " + ex.Message);
            }
        }

        public static bool InsertUpdateRunningText(Dictionary<string, string> parameter, out int affected)
        {
            try
            {
                string command  = parameter["command"];
                string jsonData = parameter["data"];
                Dto.RunningText runningText = (Dto.RunningText)JsonConvert.DeserializeObject(jsonData, (typeof(Dto.RunningText)));
                bool insertData = command == "INSERT";

                if (Database.Me.Connected)
                {
                    string sql = "";
                    if (insertData)
                        sql = "INSERT INTO runningtexts (station_name, sticky, active, running_text) VALUES (?,?,?,?)";
                    else
                        sql = "UPDATE runningtexts SET station_name = ? , sticky = ? , active = ?, running_text = ? WHERE id = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        DbParameter param = cmd.CreateParameter();
                        param.ParameterName = "station_name";
                        param.Value = runningText.StationName;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "sticky";
                        param.Value = runningText.Sticky;
                        param.DbType = System.Data.DbType.Boolean;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "active";
                        param.Value = runningText.Active;
                        param.DbType = System.Data.DbType.Boolean;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "running_text";
                        param.Value = runningText.Text;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        if (!insertData)
                        {
                            // UPDATE
                            param = cmd.CreateParameter();
                            param.ParameterName = "id";
                            param.Value = runningText.Id;
                            param.DbType = System.Data.DbType.Int32;
                            cmd.Parameters.Add(param);
                        }

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("InsertUpdateRunningText: " + ex.Message);
            }

            affected = -1;
            return false;
        }

        public static bool InsertUpdateAccessList(Dictionary<string, string> parameter, out int affected)
        {
            try
            {
                string command   = parameter["command"];
                string jsonData  = parameter["data"];
                Dto.IpAccessList accessList = (Dto.IpAccessList)JsonConvert.DeserializeObject(jsonData, (typeof(Dto.IpAccessList)));
                bool insertData  = command == "INSERT";

                if (Database.Me.Connected)
                {
                    string sql = "";
                    if (insertData)
                        sql = "INSERT INTO ipaccesslists (ipaddress,allowed,keterangan) VALUES (?,?,?)";
                    else
                        sql = "UPDATE ipaccesslists SET ipaddress = ? , allowed = ? , keterangan = ? WHERE ipaddress = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        DbParameter param = cmd.CreateParameter();
                        param.ParameterName = "ipaddress";
                        param.Value = accessList.IpAddress;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "allowed";
                        param.Value = accessList.Allowed;
                        param.DbType = System.Data.DbType.Boolean;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "keterangan";
                        param.Value = accessList.Keterangan;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        if (!insertData)
                        {
                            // UPDATE
                            param = cmd.CreateParameter();
                            param.ParameterName = "ipaddressW";
                            param.Value = accessList.IpAddressOld;
                            param.DbType = System.Data.DbType.String;
                            cmd.Parameters.Add(param);
                        }

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("InsertUpdateAccessList: " + ex.Message);
            }

            affected = -1;
            return false;
        }

        public static bool InsertUpdateStation(Dictionary<string, string> parameter, out int affected)
        {
            try
            {
                string command      = parameter["command"];
                string jsonData     = parameter["data"];
                Dto.Station station = (Dto.Station)JsonConvert.DeserializeObject(jsonData, (typeof(Dto.Station)));
                bool insertData     = command == "INSERT";

                if (Database.Me.Connected)
                {
                    string sql = "";
                    if (insertData)
                        sql = "INSERT INTO stations (name, post, canlogin, keterangan) VALUES (?,?,?,?)";
                    else
                        sql = "UPDATE stations SET name = ?, post = ?, canlogin = ?, keterangan = ? WHERE name = ? AND post=? ";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        if (insertData)
                        {
                            Database.Me.AddParameter(cmd, "name", station.Name, DbType.String);
                            Database.Me.AddParameter(cmd, "post", station.Post, DbType.String);
                            Database.Me.AddParameter(cmd, "canlogin", station.CanLogin, DbType.Boolean);
                            Database.Me.AddParameter(cmd, "keterangan", station.Keterangan, DbType.String);

                        }
                        else //update
                        {
                            Database.Me.AddParameter(cmd, "name", station.Name, DbType.String);
                            Database.Me.AddParameter(cmd, "post", station.Post, DbType.String);
                            Database.Me.AddParameter(cmd, "canlogin", station.CanLogin, DbType.Boolean);
                            Database.Me.AddParameter(cmd, "keterangan", station.Keterangan, DbType.String);
                            Database.Me.AddParameter(cmd, "nameO", station.NameOld, DbType.String);
                            Database.Me.AddParameter(cmd, "postO", station.PostOld, DbType.String);
                        }

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("InsertUpdateStation: " + ex.Message);
            }

            affected = -1;
            return false;
        }

        public static bool InsertUpdatePost(Dictionary<string, string> parameter, out int affected)
        {
            try
            {
                string command   = parameter["command"];
                string jsonData  = parameter["data"];
                Dto.Post post    = (Dto.Post)JsonConvert.DeserializeObject(jsonData, (typeof(Dto.Post)));
                bool insertData  = command == "INSERT";

                if (Database.Me.Connected)
                {
                    string sql = "";
                    if (insertData)
                        sql = "INSERT INTO posts (name, numberprefix, keterangan) VALUES (?,?,?)";
                    else
                        sql = "UPDATE posts SET name = ? , numberprefix = ?, keterangan = ?  WHERE name = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        DbParameter param = cmd.CreateParameter();
                        param.ParameterName = "name";
                        param.Value = post.Name;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "numberprefix";
                        param.Value = post.NumberPrefix;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "keterangan";
                        param.Value = post.Keterangan;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        if (!insertData)
                        {
                            // UPDATE
                            param = cmd.CreateParameter();
                            param.ParameterName = "nameW";
                            param.Value = post.NameOld;
                            param.DbType = System.Data.DbType.String;
                            cmd.Parameters.Add(param);
                        }

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("InsertUpdatePost: " + ex.Message);
            }

            affected = -1;
            return false;
        }

        public static bool InsertUpdateLogin(Dictionary<string, string> parameter, out int affected)
        {
            try
            {
                string command  = parameter["command"];
                string jsonData = parameter["data"];
                Dto.Login login = (Dto.Login)JsonConvert.DeserializeObject(jsonData, (typeof(Dto.Login)));
                bool insertData = command == "INSERT";

                if (Database.Me.Connected)
                {
                    string sql = "";

                    if (command == "INSERT" || command == "UPDATE_PASSWORD")
                    {
                        if (string.IsNullOrWhiteSpace(login.Username))
                            throw new Exception("You can not use empty user name");
                        if (login.Username.Length < 3)
                            throw new Exception("Minimum user name length is 3 characters");
                        //if (string.IsNullOrWhiteSpace(login.Password))
                        //    throw new Exception("You can not use empty password");
                        //if (login.Password.Length < 5)
                        //    throw new Exception("Minimum password length is 5 characters");
                    }

                    if (command == "INSERT")
                    {
                        //newPasswordHash = Util.GetPasswordHash(newClearPass, newuserName);
                        sql = "INSERT INTO logins (username, password, expired, active) VALUES (?,?,?,?)";
                    }
                    else if(command == "UPDATE_PASSWORD")
                    {
                        //newPasswordHash = Util.GetPasswordHash(newClearPass, newuserName);
                        sql = "UPDATE logins SET username = ? , password = ? , expired = ?, active = ? WHERE username = ?";
                    }
                    else 
                    {
                        sql = "UPDATE logins SET expired = ?, active = ? WHERE username = ?";
                    }

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        if (command == "INSERT")
                        {
                            DateTime newExp = login.Expired;
                            if (login.Expired.Date == DateTime.Now.Date)
                                newExp = DateTime.Now.AddYears(2);

                            DbParameter param = cmd.CreateParameter();
                            param.ParameterName = "username";
                            param.Value = login.Username;
                            param.DbType = System.Data.DbType.String;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "password";
                            param.Value = login.Password;
                            param.DbType = System.Data.DbType.String;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "expired";
                            param.Value = newExp;
                            param.DbType = System.Data.DbType.Date;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "active";
                            param.Value = login.Active;
                            param.DbType = System.Data.DbType.Boolean;
                            cmd.Parameters.Add(param);
                        }
                        else if (command == "UPDATE_PASSWORD")
                        {
                            DbParameter param = cmd.CreateParameter();
                            param.ParameterName = "username";
                            param.Value = login.Username;
                            param.DbType = System.Data.DbType.String;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "password";
                            param.Value = login.Password;
                            param.DbType = System.Data.DbType.String;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "expired";
                            param.Value = login.Expired;
                            param.DbType = System.Data.DbType.Date;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "active";
                            param.Value = login.Active;
                            param.DbType = System.Data.DbType.Boolean;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "usernameW";
                            param.Value = login.UsernameOld;
                            param.DbType = System.Data.DbType.String;
                            cmd.Parameters.Add(param);
                        }
                        else
                        {
                            DbParameter param = cmd.CreateParameter();
                            param.ParameterName = "expired";
                            param.Value = login.Expired;
                            param.DbType = System.Data.DbType.Date;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "active";
                            param.Value = login.Active;
                            param.DbType = System.Data.DbType.Boolean;
                            cmd.Parameters.Add(param);

                            param = cmd.CreateParameter();
                            param.ParameterName = "username";
                            param.Value = login.UsernameOld;
                            param.DbType = System.Data.DbType.String;
                            cmd.Parameters.Add(param);
                        }

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("InsertUpdateLogin: " + ex.Message);
            }

            affected = -1;
            return false;
        }

        #endregion


        #region Delete data Queue sys table
        public static Tuple<bool,int> DeleteTable(Dictionary<string, string> parameter)
        {
            try
            {
                string tableName = parameter["tablename"];
                string param0    = parameter["param0"];
                string param1    = parameter["param1"];

                bool success = false;
                int affected = -1;

                if (tableName == "runningtexts")
                {
                    success = DeleteRunningText(param0, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == "ipaccesslists")
                {
                    success = DeleteAccessList(param0, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == "stations")
                {
                    success = DeleteStation(param0, param1, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == "posts")
                {
                    success = DeletePost(param0, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == "logins")
                {
                    success = DeleteLogin(param0, out affected);
                    return Tuple.Create(success, affected);
                }
                else
                    throw new Exception("Invalid table name: " + tableName);
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("DeleteTable: " + ex.Message);
            }
        }

        public static bool DeleteRunningText(string id, out int affected)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = "DELETE FROM runningtexts WHERE id = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        DbParameter param = cmd.CreateParameter();
                        param.ParameterName = "id";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Int32;
                        cmd.Parameters.Add(param);

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("DeleteRunningText: " + ex.Message);
                }
            }

            affected = -1;
            return false;
        }

        public static bool DeleteAccessList(string ipaddress, out int affected)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = "DELETE FROM ipaccesslists WHERE ipaddress = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        DbParameter param = cmd.CreateParameter();
                        param.ParameterName = "ipaddress";
                        param.Value = ipaddress.Trim();
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("DeleteAccessList: " + ex.Message);
                }
            }

            affected = -1;
            return false;
        }

        public static bool DeleteStation(string staName, string staPost, out int affected)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = "DELETE FROM stations WHERE name = ? AND post = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        Database.Me.AddParameter(cmd, "name", staName, DbType.String);
                        Database.Me.AddParameter(cmd, "post", staPost, DbType.String);

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("DeleteStation: " + ex.Message);
                }
            }

            affected = -1;
            return false;
        }

        public static bool DeletePost(string postName, out int affected)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = "DELETE FROM posts WHERE name = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        DbParameter param = cmd.CreateParameter();
                        param.ParameterName = "name";
                        param.Value = postName.Trim();
                        param.DbType = DbType.String;
                        cmd.Parameters.Add(param);

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("DeletePost: " + ex.Message);
                }
            }

            affected = -1;
            return false;
        }

        public static bool DeleteLogin(string username, out int affected)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = "DELETE FROM logins WHERE username = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        Database.Me.AddParameter(cmd, "username", username.Trim(), DbType.String);

                        affected = cmd.ExecuteNonQuery();
                        return affected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("DeleteLogin: " + ex.Message);
                }
            }

            affected = -1;
            return false;
        }

        #endregion


        #region Get data Queue sys table

        public static Dictionary<string, string> GetTable(Dictionary<string, string> parameter)
        {
            try
            {
                if (Database.Me.Connected)
                {
                    string tableName = parameter["tablename"];
                    int offset = int.Parse(parameter["offset"]);
                    int limit = int.Parse(parameter["limit"]);
                    
                    int rowCount = 0;
                    string jsonTable = "";


                    if (tableName == "runningtexts")
                    {
                        rowCount = GetTableRowCount(tableName);
                        if (rowCount > 0)
                            jsonTable = GetRunningText(offset, limit);
                    }
                    else if (tableName == "ipaccesslists")
                    {
                        rowCount = GetTableRowCount(tableName);
                        if (rowCount > 0)
                            jsonTable = GetAccessList(offset, limit);
                    }
                    else if (tableName == "stations")
                    {
                        rowCount = GetTableRowCount(tableName);
                        if (rowCount > 0)
                            jsonTable = GetStation(offset, limit);
                    }
                    else if (tableName == "posts")
                    {
                        rowCount = GetTableRowCount(tableName);
                        if (rowCount > 0)
                            jsonTable = GetPost(offset, limit);
                    }
                    else if (tableName == "logins")
                    {
                        rowCount = GetTableRowCount(tableName);
                        if (rowCount > 0)
                            jsonTable = GetLogin(offset, limit);
                    }
                    else
                        throw new Exception("Invalid table name: " + tableName);

                    return new Dictionary<string, string>()
                    {
                        ["totalrow"] = rowCount.ToString(),
                        ["jsontable"] = jsonTable
                    };
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetTable: " + ex.Message);
            }

            return null;
        }

        public static string GetRunningText(int offset = 0, int limit = 0)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = string.Format("SELECT id, station_name, sticky, active, running_text from runningtexts ORDER by id");
                    sql += Database.Me.GetOffsetLimit(offset, limit);

                    Database.Me.OpenConnection();

                    DataTable dataTable = Database.Me.CreateDataTable(sql);
                    string jsonDataTable = JsonConvert.SerializeObject(dataTable, Formatting.None);
                    if (jsonDataTable == "[]")
                        jsonDataTable = "";

                    return jsonDataTable;
                }
                catch (Exception ex)
                {
                    throw new AppException("GetRunningText: " + ex.Message);
                }
            }

            return "";
        }

        public static string GetAccessList(int offset = 0, int limit = 0)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = string.Format("SELECT ipaddress,allowed,keterangan FROM ipaccesslists ORDER BY ipaddress");
                    sql += Database.Me.GetOffsetLimit(offset, limit);

                    Database.Me.OpenConnection();

                    DataTable dataTable = Database.Me.CreateDataTable(sql);
                    string jsonDataTable = JsonConvert.SerializeObject(dataTable, Formatting.None);
                    if (jsonDataTable == "[]")
                        jsonDataTable = "";

                    return jsonDataTable;
                }
                catch (Exception ex)
                {
                    throw new AppException("GetAccessList: " + ex.Message);
                }
            }

            return "";
        }

        public static string GetStation(int offset = 0, int limit = 0)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = string.Format("SELECT name, post, canlogin, keterangan FROM stations ORDER BY name");
                    sql += Database.Me.GetOffsetLimit(offset, limit);

                    Database.Me.OpenConnection();

                    DataTable dataTable = Database.Me.CreateDataTable(sql);
                    string jsonDataTable = JsonConvert.SerializeObject(dataTable, Formatting.None);
                    if (jsonDataTable == "[]")
                        jsonDataTable = "";

                    return jsonDataTable;
                }
                catch (Exception ex)
                {
                    throw new AppException("GetStation: " + ex.Message);
                }
            }

            return "";
        }

        public static string GetPost(int offset = 0, int limit = 0)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = string.Format("SELECT name, numberprefix, keterangan FROM posts ORDER BY name");
                    sql += Database.Me.GetOffsetLimit(offset, limit);

                    Database.Me.OpenConnection();

                    DataTable dataTable = Database.Me.CreateDataTable(sql);
                    string jsonDataTable = JsonConvert.SerializeObject(dataTable, Formatting.None);
                    if (jsonDataTable == "[]")
                        jsonDataTable = "";

                    return jsonDataTable;
                }
                catch (Exception ex)
                {
                    throw new AppException("GetPost: " + ex.Message);
                }
            }

            return "";
        }

        public static string GetLogin(int offset = 0, int limit = 0)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = string.Format("SELECT username, password, expired, active FROM logins ORDER BY username");
                    sql += Database.Me.GetOffsetLimit(offset, limit);

                    Database.Me.OpenConnection();

                    DataTable dataTable = Database.Me.CreateDataTable(sql);
                    string jsonDataTable = JsonConvert.SerializeObject(dataTable, Formatting.None);
                    if (jsonDataTable == "[]")
                        jsonDataTable = "";

                    return jsonDataTable;
                }
                catch (Exception ex)
                {
                    throw new AppException("GetLogin: " + ex.Message);
                }
            }

            return "";
        }

        #endregion


        #region Queue Jobs/Sequences/Numbers

        public static bool UpdateJob(Dictionary<string, string> parameter)
        {
            try
            {
                string status = parameter["status"];
                string jobid  = parameter["jobid"];
                string jobno  = parameter["jobno"];

                if (status == "FINISHED" || status == "CLOSED")
                    return UpdateJob(status, jobid, jobno);
                else
                    throw new AppException("UpdateJob: Invalid job status");
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("UpdateJob: " + ex.Message);
            }
        }

        public static bool UpdateJob(string status, string jobid, string jobno)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql;

                    if (status == "PROCESS")
                    {
                        if (Database.Me.ProviderType == ProviderType.SQLITE)
                            sql = @"UPDATE jobs SET status = 'PROCESS', calltime = strftime('%Y-%m-%d %H:%M:%f','now', 'localtime') WHERE id = ?";
                        else
                            sql = @"UPDATE jobs SET status = 'PROCESS', calltime = getdate() WHERE id = ?";
                    }
                    else if (status == "FINISHED")
                    {
                        if (Database.Me.ProviderType == ProviderType.SQLITE)
                            sql = @"UPDATE jobs SET status = 'FINISHED', call2time = strftime('%Y-%m-%d %H:%M:%f','now', 'localtime') WHERE id = ?";
                        else
                            sql = @"UPDATE jobs SET status = 'FINISHED', call2time = getdate() WHERE id = ?";
                    }
                    else if (status == "CLOSED")
                    {
                        if (Database.Me.ProviderType == ProviderType.SQLITE)
                            sql = @"UPDATE jobs SET status = 'CLOSED', endtime = strftime('%Y-%m-%d %H:%M:%f','now', 'localtime') WHERE id = ?";
                        else
                            sql = @"UPDATE jobs SET status = 'CLOSED', endtime = getdate() WHERE id = ?";
                    }
                    else
                        throw new Exception("Invalid job status");

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        Int32 jobid_ = 0;

                        if (Int32.TryParse(jobid, out jobid_))
                        {
                            Database.Me.AddParameter(cmd, "id", jobid, DbType.Int32);

                            int recordAffected = cmd.ExecuteNonQuery();
                            return recordAffected > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("UpdateJob: " + ex.Message);
                }
            }

            return false;
        }

        public static string GetJob(Dictionary<string, string> parameter)
        {
            try
            {
                string post     = parameter["post"];
                string status   = parameter["status"];
                int offset      = int.Parse(parameter["offset"]);
                int limit       = int.Parse(parameter["limit"]);

                if (status == "PROCESS" || status == "FINISHED")
                    return GetJob(post, status, offset, limit);
                else
                    throw new AppException("GetJob: Invalid job status");
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetJob: " + ex.Message);
            }
        }

        public static string GetJob(string post, string status, int offset = 0, int limit = 0)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql;
                    if (Database.Me.ProviderType == ProviderType.SQLITE)
                    {
                        sql = string.Format(@"SELECT id, number, status, station, post, source, date, starttime, calltime, call2time, endtime FROM jobs
                                  WHERE status = '{0}'  AND date = date('now','localtime')
                                  AND post = ? ORDER BY starttime ASC", status);
                    }
                    else
                    {
                        sql = string.Format(@"SELECT id, number, status, station, post, source, date, starttime, calltime, call2time, endtime FROM jobs
                                  WHERE status = '{0}'  AND date = CAST(getdate() AS date)
                                  AND post = ? ORDER BY starttime ASC", status);
                    }

                    Database.Me.OpenConnection();
                    DbDataAdapter sda = Database.Me.CreateDataAdapter(sql);
                    Database.Me.AddParameter(sda.SelectCommand, "post", post, DbType.String);

                    DataTable dataTable = new DataTable();
                    sda.Fill(dataTable);

                    string jsonDataTable = JsonConvert.SerializeObject(dataTable, Formatting.None);
                    if (jsonDataTable == "[]")
                        jsonDataTable = "";

                    return jsonDataTable;
                }
                catch (Exception ex)
                {
                    throw new AppException("GetJob: " + ex.Message);
                }
            }

            return "";
        }


        /** Get finished job/number is Comma Separated Values
            Returns csv
        */
        public static string GetFinishedJobInCsvList(string post, int limit = 10)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    Database.Me.OpenConnection();

                    // update display
                    string sql1;
                    if (Database.Me.ProviderType == ProviderType.SQLITE)
                    {
                        sql1 = string.Format(@"SELECT id, number, posts.numberprefix FROM jobs
                                    JOIN posts ON posts.name = jobs.post
                                    WHERE status = 'FINISHED'  AND date = date('now','localtime')
                                    AND post = ? ORDER BY call2time ASC LIMIT {0}", limit);
                    }
                    else
                    {
                        sql1 = string.Format(@"SELECT TOP {0} id,number, posts.numberprefix FROM jobs
                                    JOIN posts ON posts.name = jobs.post
                                    WHERE status = 'FINISHED'  AND date = CAST(getdate() AS date)
                                    AND post = ? ORDER BY call2time ASC", limit);
                    }

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql1;
                        Database.Me.AddParameter(cmd, "post", post, DbType.String);
                        DbDataReader reader = cmd.ExecuteReader();

                        int idx = 0;
                        string csvList = "";

                        while (reader.Read())
                        {
                            string _number = reader.GetValue(1).ToString().Trim();
                            string _prefix = reader.GetValue(2).ToString().Trim();
                            if (csvList.Length > 0)
                                csvList += ",";

                            csvList += _prefix + _number;
                            idx++;
                        }
                        reader.Close();

                        return csvList;
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("GetFinishedJobInCsvList: " + ex.Message);
                }
            }
            return "";
        }

        #endregion


        #region Queue stats

        /** Delete processed number from queue
            Returns boolean
        */
        public static bool DeleteProcessedNumberFromQueue(int number, string station, string post)
        {
            try
            {
                if (Database.Me.Connected)
                {
                    string sqlDelete = "";
                    int affectedRows = 0;

                    if (number == (Properties.Settings.Default.MaxQueueNumber - 1))
                    {
                        // max_number-1 reached, delete all processed number for this post
                        sqlDelete = @"DELETE FROM sequences WHERE status = 'PROCESS' AND number < ? AND post = ?";

                        using (DbCommand cmdDelete = Database.Me.Connection.CreateCommand())
                        {
                            cmdDelete.CommandText = sqlDelete;
                            int maxNumber = Properties.Settings.Default.MaxQueueNumber;
                            Database.Me.AddParameter(cmdDelete, "number", maxNumber, DbType.Int32);
                            Database.Me.AddParameter(cmdDelete, "post",   station,   DbType.String);
                            
                            affectedRows = cmdDelete.ExecuteNonQuery();
                            
                            return affectedRows > 0;
                        }
                    }
                    else
                    {
                        // delete only processed numbers from specific station
                        sqlDelete = @"DELETE FROM sequences WHERE status = 'PROCESS' AND number < ? AND post = ? AND station = ?";
                        using (DbCommand cmdDelete = Database.Me.Connection.CreateCommand())
                        {
                            cmdDelete.CommandText = sqlDelete;
                            Database.Me.AddParameter(cmdDelete, "number",  number,  DbType.Int32);
                            Database.Me.AddParameter(cmdDelete, "post",    post,    DbType.String);
                            Database.Me.AddParameter(cmdDelete, "station", station, DbType.String);
                            
                            affectedRows = cmdDelete.ExecuteNonQuery();
                            
                            return affectedRows > 0;
                        }
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("DeleteProcessedNumberFromQueue: " + ex.Message);
            }

            return false;
        }

        /** Get next waiting number and post summary
            Return Dictionary<string, string> with Keys: id, number, numberLeft, postPrefix.
            Default data are id="", number ="", numberLeft="0", postPrefix always set with correct value.

            On error empty null
        */
        public static Dictionary<string, string> GetWaitingNumberAndPostSummary(Dictionary<string, string> parameter)
        {
            try
            {
                // Get next smallest waiting queue number.
                // Update Display to display the number
                if (Database.Me.Connected)
                {
                    string post    = parameter["post"];
                    string station = parameter["station"];
                    string sql = @"SELECT id, number, status, station, post, source, starttime, endtime, numberleft, numbermax FROM v_sequences WHERE post = ?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmdSelect = Database.Me.Connection.CreateCommand())
                    {
                        cmdSelect.CommandText = sql;
                        Database.Me.AddParameter(cmdSelect, "post", post, DbType.String);

                        using (DbDataReader reader = cmdSelect.ExecuteReader())
                        {
                            string postNumberPrefix = GetPostNumberPrefix(post);

                            if (reader.HasRows)
                            {
                                int affectedRows = 0;

                                reader.Read();

                                int id           = reader.GetInt32(0);
                                int number       = reader.GetInt32(1);
                                object starttime = reader.GetValue(6);
                                int numberLeft   = reader.GetInt32(8);

                                // Set number status as PROCESS
                                string sqlUpdate;
                                if (Database.Me.ProviderType == ProviderType.SQLITE)
                                {
                                    sqlUpdate = $@"UPDATE sequences SET status = 'PROCESS', station = ?, calltime = strftime('%Y-%m-%d %H:%M:%f','now', 'localtime')
                                                    WHERE id = ? AND number = ? AND post = ?";
                                }
                                else
                                {
                                    sqlUpdate = $@"UPDATE sequences SET status = 'PROCESS', station = ?, calltime = getdate()
                                                    WHERE id = ? AND number = ? AND post = ?";
                                }

                                using (DbCommand cmdInsert = Database.Me.Connection.CreateCommand())
                                {
                                    cmdInsert.CommandText = sqlUpdate;

                                    Database.Me.AddParameter(cmdInsert, "station",  station, DbType.String);
                                    Database.Me.AddParameter(cmdInsert, "id",       id,      DbType.Int32);
                                    Database.Me.AddParameter(cmdInsert, "number",   number,  DbType.Int32);
                                    Database.Me.AddParameter(cmdInsert, "post",     post,    DbType.String);

                                    affectedRows = cmdInsert.ExecuteNonQuery();
                                }

                                if (affectedRows > 0)
                                {
                                    int queueLeft = numberLeft;
                                    queueLeft     = queueLeft - 1;

                                    return new Dictionary<string, string>()
                                    {
                                        ["id"]          = id.ToString(),
                                        ["number"]      = number.ToString(),
                                        ["numberLeft"]  = queueLeft.ToString(),
                                        ["postPrefix"]  = postNumberPrefix,
                                    };
                                }
                            }
                            else
                            {
                                return new Dictionary<string, string>()
                                {
                                    ["id"]          = "",
                                    ["number"]      = "",
                                    ["numberLeft"]  = "0",
                                    ["postPrefix"]  = postNumberPrefix
                                };
                            }
                        }
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetNextWaitingNumber: " + ex.Message);
            }

            return null;
        }

        /** Get last proccessed number and post summary
            Return Dictionary<string,string> with Keys: id, number, numberLeft, postPrefix.
            Default data are id="", number ="", numberLeft="0", postPrefix always set with correct value.

            On error returns null
        */
        public static Dictionary<string, string> GetLastProcessedNumberAndPostSummary(Dictionary<string, string> parameter)
        {
            try
            {
                string post   = parameter["post"];
                Dictionary<string, string> result1 = GetLastProcessedNumberAndPostSummary(post);
                
                if(result1 != null && result1.ContainsKey("number"))
                {
                    string lastProcessedNumber = result1["number"];
                    if( ! string.IsNullOrWhiteSpace(lastProcessedNumber))
                    {
                        return result1;
                    }
                    else
                    {
                        Dictionary<string, string> result2 = GetWaitingNumberAndPostSummary(post);
                        if(result2 != null && result2.ContainsKey("number"))
                        {
                            return result2;
                        }
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetJob: " + ex.Message);
            }

            return null;
        }

        /** Get last proccessed number and post summary
            Return Dictionary<string,string> with Keys: id, number, numberLeft, postPrefix.
            Default data are id="", number ="", numberLeft="0", postPrefix always set with correct value.

            On error returns null
        */
        public static Dictionary<string, string> GetLastProcessedNumberAndPostSummary(string post)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sqlFirst;

                    if (Database.Me.ProviderType == ProviderType.SQLITE)
                    {
                        sqlFirst = @"
                        SELECT id,number,status,station,post,source,starttime,endtime
                            ,( SELECT COUNT(number) FROM sequences WHERE status = 'WAITING' AND post = ? AND date = date('now','localtime') ) AS numberleft
                            ,( SELECT MAX(number)   FROM sequences WHERE status = 'WAITING' AND post = ? AND date = date('now','localtime') ) AS numbermax
                            FROM sequences 
                            WHERE status = 'PROCESS' AND post = ? AND date = date('now','localtime')
                            AND id = (SELECT MAX(id) FROM sequences WHERE status = 'PROCESS' AND post = ? AND date = date('now','localtime') )
                        ";
                    }
                    else
                    {
                        sqlFirst = @"
                        SELECT id,number,status,station,post,source,starttime,endtime
                            ,( SELECT COUNT(number) FROM sequences WHERE status = 'WAITING' AND post = ? AND date = CAST(getdate() AS date) ) AS numberleft
                            ,( SELECT MAX(number)   FROM sequences WHERE status = 'WAITING' AND post = ? AND date = CAST(getdate() AS date) ) AS numbermax
                            FROM sequences 
                            WHERE status = 'PROCESS' AND post = ? AND date = CAST(getdate() AS date)
                            AND id = (SELECT MAX(id) FROM sequences WHERE status = 'PROCESS' AND post = ? AND date = CAST(getdate() AS date) )
                        ";
                    }

                    Database.Me.OpenConnection();

                    using (DbCommand cmdSelect = Database.Me.Connection.CreateCommand())
                    {
                        cmdSelect.CommandText = sqlFirst;

                        Database.Me.AddParameter(cmdSelect, "par1", post, DbType.String);
                        Database.Me.AddParameter(cmdSelect, "par2", post, DbType.String);
                        Database.Me.AddParameter(cmdSelect, "par3", post, DbType.String);
                        Database.Me.AddParameter(cmdSelect, "par4", post, DbType.String);

                        using (DbDataReader reader = cmdSelect.ExecuteReader())
                        {
                            Dictionary<string, string> result = null;
                            string postNumberPrefix = GetPostNumberPrefix(post);

                            if (reader.HasRows)
                            {
                                reader.Read();

                                var id          = reader.GetValue(0);
                                var number      = reader.GetValue(1);
                                var numberLeft  = reader.GetValue(8);

                                result = new Dictionary<string, string>()
                                {
                                    ["id"]         = id.ToString(),
                                    ["number"]     = number.ToString(),
                                    ["numberLeft"] = numberLeft.ToString(),
                                    ["postPrefix"] = postNumberPrefix
                                };
                            }
                            else
                            {
                                result = new Dictionary<string, string>()
                                {
                                    ["id"]         = "",
                                    ["number"]     = "",
                                    ["numberLeft"] = "0",
                                    ["postPrefix"] = postNumberPrefix
                                };
                            }
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("GetLastProcessedNumberAndPostSummary: " + ex.Message);
                }
            }

            return null;
        }

        /** Get next waiting number and post summary
            Return Dictionary<string,string> with Keys: id, number, numberLeft, postPrefix.
            Default data are id="", number ="", numberLeft="0", postPrefix always set with correct value.

            On error returns null
        */
        public static Dictionary<string,string> GetWaitingNumberAndPostSummary(string post)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    using (DbCommand cmdSelect = Database.Me.Connection.CreateCommand())
                    {
                        string sqlSecond = @"SELECT id, number, status, station, post, source, starttime, endtime, numberleft, numbermax FROM v_sequences WHERE post = ?";
                        
                        cmdSelect.CommandText = sqlSecond;

                        Database.Me.AddParameter(cmdSelect, "post", post, DbType.String);

                        using (DbDataReader reader = cmdSelect.ExecuteReader())
                        {
                            Dictionary<string, string> result = null;
                            string postNumberPrefix = GetPostNumberPrefix(post);

                            if (reader.HasRows)
                            {
                                reader.Read();

                                var id         = reader.GetValue(0);
                                var number     = reader.GetValue(1);
                                var numberLeft = reader.GetValue(8);

                                result = new Dictionary<string, string>()
                                {
                                    ["id"]         = id.ToString(),
                                    ["number"]     = number.ToString(),
                                    ["numberLeft"] = numberLeft.ToString(),
                                    ["postPrefix"] = postNumberPrefix
                                };
                            }
                            else
                            {
                                result = new Dictionary<string, string>()
                                {
                                    ["id"]         = "",
                                    ["number"]     = "",
                                    ["numberLeft"] = "0",
                                    ["postPrefix"] = postNumberPrefix
                                };
                            }
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("GetWaitingNumberAndPostSummary: " + ex.Message);
                }
            }
            
            return null;
        }

        #endregion


        #region Create new number

        /** Create new queue number
            Return Dictionary<string,string> with Keys: postprefix, number, post, timestamp.
            On error returns null
        */
        public static Dictionary<string, string> CreateNewNumber(Dictionary<string, string> parameter)
        {
            try
            {
                string post    = parameter["post"];
                string station = parameter["station"];

                return CreateNewNumber(station, post);
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("CreateNewNumber: " + ex.Message);
            }
        }

        /** Create new queue number
            Return Dictionary<string,string> with Keys: postprefix, number, post, timestamp.
            On error returns null
        */
        public static Dictionary<string, string> CreateNewNumber(string station, string post)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    Database.Me.OpenConnection();
                    string sql;
                    if (Database.Me.ProviderType == ProviderType.SQLITE)
                    {
                        sql = $@"SELECT number+1 from sequences WHERE post = ? AND date = date('now','localtime') 
                             AND id = (SELECT MAX(id) from sequences WHERE post = ? AND date = date('now','localtime') )";
                    }
                    else
                    {
                        sql = $@"SELECT number+1 from sequences WHERE post = ? AND date = CAST(getdate() AS date) 
                             AND id = (SELECT MAX(id) from sequences WHERE post = ? AND date = CAST(getdate() AS date) )";
                    }

                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        Database.Me.AddParameter(cmd, "post1", post, DbType.String);
                        Database.Me.AddParameter(cmd, "post2", post, DbType.String);

                        var res = cmd.ExecuteScalar();
                        string newNumberStr = "";
                        if (res != null)
                            newNumberStr = res.ToString();

                        // Get Post Prefix number
                        string postNumberPrefix = GetPostNumberPrefix(post);

                        string insertSQL;
                        if (Database.Me.ProviderType == ProviderType.SQLITE)
                        {
                            insertSQL = @"INSERT INTO sequences (number,post,source) VALUES (?, ?, ?);
                                          SELECT number, starttime, id FROM sequences ORDER BY id DESC LIMIT 1;";
                        }
                        else
                        {
                            insertSQL = @"INSERT INTO sequences (number,post,source) 
                                          OUTPUT INSERTED.number,INSERTED.starttime,INSERTED.id VALUES (?, ?, ?)";
                        }

                        using (DbCommand cmdInsert = Database.Me.Connection.CreateCommand())
                        {
                            cmdInsert.CommandText = insertSQL;
                            if (int.TryParse(newNumberStr, out int newNumberInt))
                            {
                                if (newNumberInt == Properties.Settings.Default.MaxQueueNumber)
                                {
                                    // max queue number reached, reset back to 1
                                    Database.Me.AddParameter(cmdInsert, "number", 1, DbType.Int32);
                                }
                                else
                                    Database.Me.AddParameter(cmdInsert, "number", newNumberInt, DbType.Int32);
                            }
                            else
                            {
                                // Create initial number "1" if table has no number
                                Database.Me.AddParameter(cmdInsert, "number", 1, DbType.Int32);
                            }

                            Database.Me.AddParameter(cmdInsert, "post",   post,    DbType.String);
                            Database.Me.AddParameter(cmdInsert, "source", station, DbType.String);


                            object newNumber = null;
                            object startTime = null;
                            int jobId = 0;
                            using (DbDataReader reader = cmdInsert.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();

                                    newNumber = reader.GetValue(0);
                                    startTime = reader.GetValue(1);
                                    jobId     = reader.GetInt32(2);

                                    if (newNumber != null && startTime != null && jobId > 0)
                                    {
                                        // Copy data to jobs table
                                        string insertJobSQL = "INSERT INTO jobs SELECT * FROM sequences WHERE id = ?";
                                        using (DbCommand cmdInsertJob = Database.Me.Connection.CreateCommand())
                                        {
                                            cmdInsertJob.CommandText = insertJobSQL;
                                            Database.Me.AddParameter(cmdInsertJob, "id", jobId, DbType.Int32);

                                            cmdInsertJob.ExecuteNonQuery();
                                        }

                                        Dictionary<string, string> result = new Dictionary<string, string>()
                                        {
                                            ["postprefix"]  = postNumberPrefix,
                                            ["number"]      = newNumber.ToString(),
                                            ["post"]        = post,
                                            ["timestamp"]   = ((DateTime)startTime).ToString("dd MMMM yyyy - HH:mm")
                                        };
                                        return result;
                                    }
                                }

                                return new Dictionary<string, string>()
                                {
                                    ["postprefix"] = "",
                                    ["number"]     = "",
                                    ["post"]       = "",
                                    ["timestamp"]  = ""
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("CreateNewNumber: " + ex.Message);
                }
            }
            
            return null;
        }

        #endregion


        #region Running Text stuffs

        /** Get running tet
            Return List<string> running text
            On error returns null
        */
        public static List<string> GetStationRunningText(Dictionary<string, string> parameter)
        {
            try
            {
                string station = parameter["station"];
                string post    = parameter["post"];

                return GetStationRunningText(station, post);
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetRunningText: " + ex.Message);
            }
        }

        public static List<string> GetStationRunningText(string station, string post)
        {
            if (Database.Me.Connected)
            {
                try
                {
                    string sql = @"SELECT station_name, sticky, active, running_text FROM runningtexts WHERE active=1 AND station_name=?";

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        Database.Me.AddParameter(cmd, "station_name", station, DbType.String);

                        using (DbDataReader reader = cmd.ExecuteReader())
                        {
                            List<string> runningTexts = new List<string>();
                            while (reader.Read())
                            {
                                string text = reader.GetString(3).Trim();
                                runningTexts.Add(text);
                            }

                            return runningTexts;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("GetStationRunningText: " + ex.Message);
                }
            }

            return null;
        }

        #endregion


        #region Miscs stuff

        public static List<string> GetList(Dictionary<string, string> parameter)
        {
            try
            {
                if (Database.Me.Connected)
                {
                    string sql = "";
                    string name = parameter["name"];

                    if (name == "posts")
                        sql = "SELECT name FROM posts";
                    else
                        return null;

                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        using (DbDataReader reader = cmd.ExecuteReader())
                        {
                            List<string> list = new List<string>();
                            while (reader.Read())
                            {
                                list.Add(reader.GetString(0).Trim());
                            }
                            return list;
                        }
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetList: " + ex.Message);
            }
            return null;
        }


        /** Get post's number prefix
            Returns post prefix
        */
        public static string GetPostNumberPrefix(string post)
        {
            if (Database.Me.Connected)
            {
                string sql = "SELECT numberprefix FROM posts WHERE name = ?";

                try
                {
                    Database.Me.OpenConnection();
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        Database.Me.AddParameter(cmd, "name", post, DbType.String);

                        var res = cmd.ExecuteScalar();
                        if (res != null && res.ToString().Length > 0)
                        {
                            string prefix = res.ToString();
                            prefix = prefix.Trim();
                            return prefix;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException("GetPostNumberPrefix: " + ex.Message);
                }
            }

            return "**";
        }

        public static int GetTableRowCount(string tablename)
        {
            object totalRow = Database.Me.ExecuteScalar("SELECT COUNT(*) FROM " + tablename);
            if (totalRow != null)
                return Convert.ToInt32(totalRow);
            
            return 0;
        }

        #endregion
    }
}
