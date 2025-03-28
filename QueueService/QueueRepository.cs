using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;

namespace Tobasa
{

    public class Tbl
    {
        public const string runningtexts  = "queue_runningtexts";
        public const string ipaccesslists = "queue_ipaccesslists";
        public const string stations      = "queue_stations";
        public const string posts         = "queue_posts";
        public const string logins        = "queue_logins";
        public const string jobs          = "queue_jobs";
        public const string sequences     = "queue_sequences";
        public const string v_sequences   = "v_queue_sequences";
    }

    public class QueueRepository
    {
        #region Login stuffs

        public static bool CheckIpAddress(IPAddress addr, out string reasonOut)
        {
            if (! Database.Me.Connected)
            {
                reasonOut = "QueueServer is not connected to database";
                return false;
            }

            try
            {
                string reason = "";
                bool canaccess = false;
                Database.Me.OpenConnection();
                using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                {
                    string strIp    = addr.ToString();
                    string sql      = $"SELECT allowed FROM {Tbl.ipaccesslists} WHERE ipaddress = @ipaddress";
                    cmd.CommandText = sql;
                    Database.Me.AddParameter(cmd, "ipaddress", strIp, DbType.String);

                    var res = cmd.ExecuteScalar();
                    if (res != null)
                    {
                        canaccess = Convert.ToBoolean(res);
                        if (canaccess)
                            reason = "SUCCESS";
                        else
                            reason = "forbidden";
                    }
                    else
                        reason = "access list not found";
                }

                reasonOut = reason;
                return canaccess;
            }
            catch (Exception e)
            {
                throw new AppException("CheckIpAddress, " + e.Message);
            }
        }

        public static bool CanLogin(string staName, string staPost, out string reasonOut)
        {
            if (! Database.Me.Connected)
            {
                reasonOut = "QueueServer is not connected to database"; ;
                return false;
            }
            
            try
            {
                string reason = "";
                bool canLogin = false;
                Database.Me.OpenConnection();
                using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                {
                    string sql = $"SELECT canlogin FROM {Tbl.stations} WHERE name = @name AND post = @post ";
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

                reasonOut = reason;
                return canLogin;
            }
            catch (Exception e)
            {
                throw new AppException("CanLogin, " + e.Message);
            }
        }

        public static bool Login(string userName, string password, out string reasonOut)
        {
            if (! Database.Me.Connected)
            {
                reasonOut = "QueueServer is not connected to database";
                return false;
            }

            try
            {
                bool ok = false;
                string reason = "";
                Database.Me.OpenConnection();
                using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                {
                    string sql = $"SELECT username, password, expired, active FROM {Tbl.logins} WHERE username = @username";
                    cmd.CommandText = sql;
                    Database.Me.AddParameter(cmd, "username", userName, DbType.String);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string user = reader.GetString(0).Trim();
                            string pwd = reader.GetString(1).Trim();
                            DateTime expired = reader.GetDateTime(2);
                            bool active = Convert.ToBoolean(reader.GetInt32(3));

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
                        else {
                            reason = "User is not registered in database";
                        }

                        reasonOut = reason;
                        return ok;
                    }
                }
            }
            catch (Exception e)
            {
                throw new AppException("Login, " + e.Message);
            }
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

                if (tableName == Tbl.runningtexts)
                {
                    success = InsertUpdateRunningText(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == Tbl.ipaccesslists)
                {
                    success = InsertUpdateAccessList(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == Tbl.stations)
                {
                    success = InsertUpdateStation(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == Tbl.posts)
                {
                    success = InsertUpdatePost(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == Tbl.logins)
                {
                    success = InsertUpdateLogin(paramDict, out affected);
                    return Tuple.Create(success, affected);
                }
                else
                    throw new Exception("Invalid table name, " + tableName);
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("InsertUpdateTable, " + ex.Message);
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
                        sql = $"INSERT INTO {Tbl.runningtexts} (station_name, sticky, active, running_text) VALUES (?,?,?,?)";
                    else
                        sql = $"UPDATE {Tbl.runningtexts} SET station_name = ? , sticky = ? , active = ?, running_text = ? WHERE id = ?";

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
                throw new AppException("InsertUpdateRunningText, " + ex.Message);
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
                        sql = $"INSERT INTO {Tbl.ipaccesslists} (ipaddress,allowed,keterangan) VALUES (?,?,?)";
                    else
                        sql = $"UPDATE {Tbl.ipaccesslists} SET ipaddress = ? , allowed = ? , keterangan = ? WHERE ipaddress = ?";

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
                throw new AppException("InsertUpdateAccessList, " + ex.Message);
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
                        sql = $"INSERT INTO {Tbl.stations} (name, post, canlogin, keterangan) VALUES (@name, @post, @canlogin, @keterangan)";
                    else
                        sql = $"UPDATE {Tbl.stations} SET name = @name, post = @post, canlogin = @canlogin, keterangan = @keterangan WHERE name = @name AND post=@post ";

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
                throw new AppException("InsertUpdateStation, " + ex.Message);
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
                        sql = $"INSERT INTO {Tbl.posts} (name, numberprefix, keterangan, quota0, quota1) VALUES (?,?,?,?,?)";
                    else
                        sql = $"UPDATE {Tbl.posts} SET name = ? , numberprefix = ?, keterangan = ? , quota0 = ?, quota1 = ? WHERE name = ?";

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

                        param = cmd.CreateParameter();
                        param.ParameterName = "quota0";
                        param.Value = post.Quota0;
                        param.DbType = System.Data.DbType.Int32;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "quota1";
                        param.Value = post.Quota1;
                        param.DbType = System.Data.DbType.Int32;
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
                throw new AppException("InsertUpdatePost, " + ex.Message);
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
                        sql = $"INSERT INTO {Tbl.logins} (username, password, expired, active) VALUES (?,?,?,?)";
                    }
                    else if(command == "UPDATE_PASSWORD")
                    {
                        //newPasswordHash = Util.GetPasswordHash(newClearPass, newuserName);
                        sql = $"UPDATE {Tbl.logins} SET username = ? , password = ? , expired = ?, active = ? WHERE username = ?";
                    }
                    else 
                    {
                        sql = $"UPDATE {Tbl.logins} SET expired = ?, active = ? WHERE username = ?";
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
                throw new AppException("InsertUpdateLogin, " + ex.Message);
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

                if (tableName == Tbl.runningtexts)
                {
                    success = DeleteRunningText(param0, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == Tbl.ipaccesslists)
                {
                    success = DeleteAccessList(param0, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == Tbl.stations)
                {
                    success = DeleteStation(param0, param1, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == Tbl.posts)
                {
                    success = DeletePost(param0, out affected);
                    return Tuple.Create(success, affected);
                }
                else if (tableName == Tbl.logins)
                {
                    success = DeleteLogin(param0, out affected);
                    return Tuple.Create(success, affected);
                }
                else
                    throw new Exception("Invalid table name, " + tableName);
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("DeleteTable, " + ex.Message);
            }
        }

        public static bool DeleteRunningText(string id, out int affected)
        {
            if (! Database.Me.Connected)
            {
                affected = -1;
                return false;
            }

            try
            {
                string sql = $"DELETE FROM {Tbl.runningtexts} WHERE id = ?";

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
                throw new AppException("DeleteRunningText, " + ex.Message);
            }
        }

        public static bool DeleteAccessList(string ipaddress, out int affected)
        {
            if (! Database.Me.Connected)
            {
                affected = -1;
                return false;
            }

            try
            {
                string sql = $"DELETE FROM {Tbl.ipaccesslists} WHERE ipaddress = ?";

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
                throw new AppException("DeleteAccessList, " + ex.Message);
            }
        }

        public static bool DeleteStation(string staName, string staPost, out int affected)
        {
            if (! Database.Me.Connected)
            {
                affected = -1;
                return false;
            }

            try
            {
                string sql = $"DELETE FROM {Tbl.stations} WHERE name = @name AND post = @post";

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
                throw new AppException("DeleteStation, " + ex.Message);
            }
        }

        public static bool DeletePost(string postName, out int affected)
        {
            if (! Database.Me.Connected)
            {
                affected = -1;
                return false;
            }

            try
            {
                string sql = $"DELETE FROM {Tbl.posts} WHERE name = ?";

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
                throw new AppException("DeletePost, " + ex.Message);
            }
        }

        public static bool DeleteLogin(string username, out int affected)
        {
            if (! Database.Me.Connected)
            {
                affected = -1;
                return false;
            }

            try
            {
                string sql = $"DELETE FROM {Tbl.logins} WHERE username = @username";

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
                throw new AppException("DeleteLogin, " + ex.Message);
            }
        }

        #endregion


        #region Get data Queue sys table

        public static Dictionary<string, string> GetTable(Dictionary<string, string> parameter)
        {
            if (! Database.Me.Connected)
                return null;
            
            try
            {
                string tableName = parameter["tablename"];
                int offset = int.Parse(parameter["offset"]);
                int limit = int.Parse(parameter["limit"]);
                    
                int rowCount = 0;
                string jsonTable = "";


                if (tableName == Tbl.runningtexts)
                {
                    rowCount = GetTableRowCount(tableName);
                    if (rowCount > 0)
                        jsonTable = GetRunningText(offset, limit);
                }
                else if (tableName == Tbl.ipaccesslists)
                {
                    rowCount = GetTableRowCount(tableName);
                    if (rowCount > 0)
                        jsonTable = GetAccessList(offset, limit);
                }
                else if (tableName == Tbl.stations)
                {
                    rowCount = GetTableRowCount(tableName);
                    if (rowCount > 0)
                        jsonTable = GetStation(offset, limit);
                }
                else if (tableName == Tbl.posts)
                {
                    rowCount = GetTableRowCount(tableName);
                    if (rowCount > 0)
                        jsonTable = GetPost(offset, limit);
                }
                else if (tableName == Tbl.logins)
                {
                    rowCount = GetTableRowCount(tableName);
                    if (rowCount > 0)
                        jsonTable = GetLogin(offset, limit);
                }
                else
                    throw new Exception("Invalid table name, " + tableName);

                return new Dictionary<string, string>()
                {
                    ["totalrow"] = rowCount.ToString(),
                    ["jsontable"] = jsonTable
                };

            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetTable, " + ex.Message);
            }
        }

        public static string GetRunningText(int offset = 0, int limit = 0)
        {
            if (! Database.Me.Connected)
                return "";

            try
            {
                string sql = $"SELECT id, station_name, sticky, active, running_text from {Tbl.runningtexts} ORDER by id";
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
                throw new AppException("GetRunningText, " + ex.Message);
            }
        }

        public static string GetAccessList(int offset = 0, int limit = 0)
        {
            if (! Database.Me.Connected)
                return "";

            try
            {
                string sql = $"SELECT ipaddress,allowed,keterangan FROM {Tbl.ipaccesslists} ORDER BY ipaddress";
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
                throw new AppException("GetAccessList, " + ex.Message);
            }
        }

        public static string GetStation(int offset = 0, int limit = 0)
        {
            if (! Database.Me.Connected)
                return "";

            try
            {
                string sql = $"SELECT name, post, canlogin, keterangan FROM {Tbl.stations} ORDER BY name";
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
                throw new AppException("GetStation, " + ex.Message);
            }
        }

        public static string GetPost(int offset = 0, int limit = 0)
        {
            if (! Database.Me.Connected)
                return "";

            try
            {
                string sql = $"SELECT name, numberprefix, keterangan, quota0, quota1 FROM {Tbl.posts} ORDER BY name";
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
                throw new AppException("GetPost, " + ex.Message);
            }
        }

        public static string GetLogin(int offset = 0, int limit = 0)
        {
            if (! Database.Me.Connected)
                return "";

            try
            {
                string sql = $"SELECT username, password, expired, active FROM {Tbl.logins} ORDER BY username";
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
                throw new AppException("GetLogin, " + ex.Message);
            }
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
                    throw new AppException("Invalid job status");
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("UpdateJob, " + ex.Message);
            }
        }

        public static bool UpdateJob(string status, string jobid, string jobno)
        {
            if (! Database.Me.Connected)
                return false;

            try
            {
                string sql;

                if (status == "PROCESS") {
                    sql = $@"UPDATE {Tbl.jobs} SET status = 'PROCESS', calltime = {GetSqlDateTimeString()} WHERE id = @id";
                }
                else if (status == "FINISHED") {
                    sql = $@"UPDATE {Tbl.jobs} SET status = 'FINISHED', call2time = {GetSqlDateTimeString()} WHERE id = @id";
                }
                else if (status == "CLOSED") {
                    sql = $@"UPDATE {Tbl.jobs} SET status = 'CLOSED', endtime = {GetSqlDateTimeString()} WHERE id = @id";
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
                        Database.Me.AddParameter(cmd, "id", jobid_, DbType.Int32);

                        int recordAffected = cmd.ExecuteNonQuery();
                        return recordAffected > 0;
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                throw new AppException("UpdateJob, " + ex.Message);
            }
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
                throw new AppException("GetJob, " + ex.Message);
            }
        }

        public static string GetJob(string post, string status, int offset = 0, int limit = 0)
        {
            if (! Database.Me.Connected)
                return "";

            try
            {
                string sql;
                sql = $@"SELECT id, number, status, station, post, source, date, starttime, calltime, call2time, endtime FROM {Tbl.jobs}
                            WHERE status = '{status}'  AND date = {GetSqlDateString()}
                            AND post = @post ORDER BY starttime ASC";

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
                throw new AppException("GetJob, " + ex.Message);
            }
        }


        /** Get finished job/number in Comma Separated Values for all Post
            Returns csv
        */
        public static string GetFinishedJobInCsvList(string post, int limit = 10)
        {
            if (! Database.Me.Connected)
                return "";

            try
            {
                Database.Me.OpenConnection();

                // update display
                string sql1;
                string postFilter = "";

                if (!string.IsNullOrWhiteSpace(post))
                    postFilter = "AND post = @post";

                if (Database.Me.ProviderType == DatabaseProviderType.MSSQL)
                {
                    sql1 = $@"SELECT TOP {limit} id,number, {Tbl.posts}.numberprefix FROM {Tbl.jobs}
                                JOIN {Tbl.posts} ON {Tbl.posts}.name = {Tbl.jobs}.post
                                WHERE status = 'FINISHED' AND date = {GetSqlDateString()} {postFilter} 
                                ORDER BY call2time DESC";
                }
                else
                {
                    sql1 = $@"SELECT id, number, {Tbl.posts}.numberprefix FROM {Tbl.jobs}
                                JOIN {Tbl.posts} ON {Tbl.posts}.name = {Tbl.jobs}.post
                                WHERE status = 'FINISHED' AND date = {GetSqlDateString()} {postFilter} 
                                ORDER BY call2time DESC LIMIT {limit}";
                }

                Database.Me.OpenConnection();
                using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                {
                    cmd.CommandText = sql1;
                    if (!string.IsNullOrWhiteSpace(post))
                    {
                        Database.Me.AddParameter(cmd, "post", post, DbType.String);
                    }
                        
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
                throw new AppException("GetFinishedJobInCsvList, " + ex.Message);
            }
        }

        #endregion


        #region Queue stats

        /** Delete processed number from queue
            Returns boolean
        */
        public static bool DeleteProcessedNumberFromQueue(int number, string station, string post)
        {
            if (! Database.Me.Connected)
                return false;

            try
            {
                string sqlDelete = "";
                int affectedRows = 0;

                if (number == (Properties.Settings.Default.MaxQueueNumber - 1))
                {
                    // max_number-1 reached, delete all processed number for this post
                    sqlDelete = $"DELETE FROM {Tbl.sequences} WHERE status = 'PROCESS' AND number < @number AND post = @post";

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
                    sqlDelete = $"DELETE FROM {Tbl.sequences} WHERE status = 'PROCESS' AND number < @number AND post = @post AND station = @station";
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
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("DeleteProcessedNumberFromQueue, " + ex.Message);
            }
        }

        /** Get next waiting number and post summary
            Return Dictionary<string, string> with Keys: id, number, numberLeft, postPrefix, postId, station.
            Default data are id="", number ="", numberLeft="0", postPrefix always set with correct value.

            On error empty null
        */
        public static Dictionary<string, string> GetWaitingNumberAndPostSummary(Dictionary<string, string> parameter)
        {
            if (! Database.Me.Connected)
                return null;

            try
            {
                Database.Me.OpenConnection();
                // Get next smallest waiting queue number.
                // Update Display to display the number

                string post    = parameter["post"];
                string station = parameter["station"]; // ID caller yang memanggil
                string postNumberPrefix = GetPostNumberPrefix(post);


                int id = 0;
                int number = 0;
                object starttime = null;
                int numberLeft = 0;
                string postId = "";
                string stationId = station;

                using (DbCommand cmdSelect = Database.Me.Connection.CreateCommand())
                {
                    string sql = $@"SELECT id, number, status, station, post, source, starttime, 
                                endtime, numberleft, numbermax FROM {Tbl.v_sequences} WHERE post = @post";

                    cmdSelect.CommandText = sql;
                    Database.Me.AddParameter(cmdSelect, "post", post, DbType.String);

                    using (DbDataReader reader = cmdSelect.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            id         = reader.IsDBNull(0) ? 0  : reader.GetInt32(0);
                            number     = reader.IsDBNull(1) ? 0  : reader.GetInt32(1);
                            starttime  = reader.IsDBNull(6) ? 0  : reader.GetValue(6);
                            numberLeft = reader.IsDBNull(8) ? 0  : reader.GetInt32(8);
                            postId     = reader.IsDBNull(4) ? "" : reader.GetString(4);

                            if (!reader.IsDBNull(3)) 
                            {
                                // should never reached here
                                // station should alway null, because this is a waiting queue number
                                stationId = reader.GetString(3);
                            }
                        }
                    }
                }


                // Set number status as PROCESS
                int affectedRows = 0;
                using (DbCommand cmdInsert = Database.Me.Connection.CreateCommand())
                {
                    string sqlUpdate;
                    sqlUpdate = $@"UPDATE {Tbl.sequences} SET status = 'PROCESS', station = @station, 
                                        calltime = {GetSqlDateTimeString()} WHERE id = @id AND number = @number AND post = @post";

                    cmdInsert.CommandText = sqlUpdate;

                    Database.Me.AddParameter(cmdInsert, "station", station, DbType.String);
                    Database.Me.AddParameter(cmdInsert, "id",      id,      DbType.Int32);
                    Database.Me.AddParameter(cmdInsert, "number",  number,  DbType.Int32);
                    Database.Me.AddParameter(cmdInsert, "post",    post,    DbType.String);

                    affectedRows = cmdInsert.ExecuteNonQuery();
                }

                if (affectedRows > 0)
                {
                    int queueLeft = numberLeft;
                    queueLeft = queueLeft - 1;

                    return new Dictionary<string, string>()
                    {
                        ["id"]          = id.ToString(),
                        ["number"]      = number.ToString(),
                        ["numberLeft"]  = queueLeft.ToString(),
                        ["postPrefix"]  = postNumberPrefix,
                        ["postId"]      = postId.ToString(),
                        ["station"]     = stationId.ToString()
                    };
                }

                return new Dictionary<string, string>()
                {
                    ["id"]          = "",
                    ["number"]      = "",
                    ["numberLeft"]  = "0",
                    ["postPrefix"]  = postNumberPrefix,
                    ["postId"]      = post,
                    ["station"]     = station
                };
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetNextWaitingNumber, " + ex.Message);
            }
        }

        /** Get last proccessed number and post summary
            Return Dictionary<string,string> with Keys: id, number, numberLeft, postPrefix, postId, station.
            Default data are id="", number ="", numberLeft="0", postPrefix always set with correct value.

            On error returns null
        */
        public static Dictionary<string, string> GetLastProcessedNumberAndPostSummary(Dictionary<string, string> parameter)
        {
            try
            {
                string post   = parameter["post"];
                Dictionary<string, string> result1 = GetLastProcessedNumberAndPostSummary(post);
                
                if (result1 != null && result1.ContainsKey("number"))
                {
                    string lastProcessedNumber = result1["number"];
                    if ( ! string.IsNullOrWhiteSpace(lastProcessedNumber))
                    {
                        return result1;
                    }
                    else
                    {
                        Dictionary<string, string> result2 = GetWaitingNumberAndPostSummary(post);
                        if (result2 != null && result2.ContainsKey("number"))
                        {
                            return result2;
                        }
                    }
                }
                else
                {
                    Dictionary<string, string> result = GetWaitingNumberAndPostSummary(post);
                    if (result != null && result.ContainsKey("number"))
                    {
                        return result;
                    }
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetJob, " + ex.Message);
            }

            return null;
        }

        /** Get last proccessed number and post summary
            Return Dictionary<string,string> with Keys: id, number, numberLeft, postPrefix, postId, station.
            Default data are id="", number ="", numberLeft="0", postPrefix always set with correct value.

            On error returns null
        */
        public static Dictionary<string, string> GetLastProcessedNumberAndPostSummary(string post)
        {
            if (! Database.Me.Connected)
                return null;

            try
            {
                string postNumberPrefix = GetPostNumberPrefix(post);

                string sqlFirst = $@"
                    SELECT id,number,status,station,post,source,starttime,endtime
                        , ( SELECT COUNT(number) FROM {Tbl.sequences} WHERE status = 'WAITING' AND post = @par1 AND date = {GetSqlDateString()} ) AS numberleft
                        , ( SELECT MAX(number)   FROM {Tbl.sequences} WHERE status = 'WAITING' AND post = @par2 AND date = {GetSqlDateString()} ) AS numbermax
                        FROM {Tbl.sequences}
                        WHERE status = 'PROCESS' AND post = @par3 AND date = {GetSqlDateString()}
                        AND id = (SELECT MAX(id) FROM {Tbl.sequences} WHERE status = 'PROCESS' AND post = @par4 AND date = {GetSqlDateString()} )
                    ";

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
                        if (reader.HasRows)
                        {
                            reader.Read();

                            var id          = reader.GetValue(0);
                            var number      = reader.GetValue(1);
                            var numberLeft  = reader.GetValue(8);
                            var postId      = reader.GetValue(4);
                            var station     = reader.GetValue(3);

                            var result = new Dictionary<string, string>()
                            {
                                ["id"]         = id.ToString(),
                                ["number"]     = number.ToString(),
                                ["numberLeft"] = numberLeft.ToString(),
                                ["postPrefix"] = postNumberPrefix,
                                ["postId"]     = postId.ToString(),
                                ["station"]    = station.ToString()
                            };
                            return result;
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new AppException("GetLastProcessedNumberAndPostSummary, " + ex.Message);
            }
        }

        /** Get next waiting number and post summary
            Return Dictionary<string,string> with Keys: id, number, numberLeft, postPrefix, postId, station.
            Default data are id="", number ="", numberLeft="0", postPrefix always set with correct value.

            On error returns null
        */
        public static Dictionary<string,string> GetWaitingNumberAndPostSummary(string post)
        {
            if (! Database.Me.Connected)
                return null;

            try
            {
                string postNumberPrefix = GetPostNumberPrefix(post);

                using (DbCommand cmdSelect = Database.Me.Connection.CreateCommand())
                {
                    string sqlSecond = $@"SELECT id, number, status, station, post, source, starttime, 
                                            endtime, numberleft, numbermax FROM {Tbl.v_sequences} WHERE post = @post";
                        
                    cmdSelect.CommandText = sqlSecond;
                    Database.Me.AddParameter(cmdSelect, "post", post, DbType.String);
                    using (DbDataReader reader = cmdSelect.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            var id         = reader.GetValue(0);
                            var number     = reader.GetValue(1);
                            var numberLeft = reader.GetValue(8);
                            var postId     = reader.GetValue(4);
                            var station    = reader.GetValue(3);

                            var result = new Dictionary<string, string>()
                            {
                                ["id"]         = id.ToString(),
                                ["number"]     = number.ToString(),
                                ["numberLeft"] = numberLeft.ToString(),
                                ["postPrefix"] = postNumberPrefix,
                                ["postId"]     = postId.ToString(),
                                ["station"]    = station.ToString()
                            };
                            return result;
                        }
                    }
                }

                return new Dictionary<string, string>()
                {
                    ["id"]          = "",
                    ["number"]      = "",
                    ["numberLeft"]  = "",
                    ["postPrefix"]  = postNumberPrefix,
                    ["postId"]      = "" ,
                    ["station"]     = ""
                };
            }
            catch (Exception ex)
            {
                throw new AppException("GetWaitingNumberAndPostSummary, " + ex.Message);
            }
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
                throw new AppException("CreateNewNumber, " + ex.Message);
            }
        }

        /** Create new queue number
            Return Dictionary<string,string> with Keys: postprefix, number, post, timestamp.
            On error returns null
        */
        public static Dictionary<string, string> CreateNewNumber(string station, string post)
        {
            if (! Database.Me.Connected)
                return null;

            try
            {
                Database.Me.OpenConnection();

                // Get Post data
                Dto.Post postData = GetPost(post);
                if (postData == null)
                {
                    throw new AppException($"Invalid post {post} data");
                }

                // Get Post Prefix number
                //string postNumberPrefix = GetPostNumberPrefix(post);
                string postNumberPrefix = postData.NumberPrefix;


                int newNumberInt = 0;
                using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                {
                    string sql = $@"SELECT number+1 from {Tbl.sequences} WHERE post = @post1 AND date = {GetSqlDateString()} 
                        AND id = (SELECT MAX(id) from {Tbl.sequences} WHERE post = @post2 AND date = {GetSqlDateString()} )";

                    cmd.CommandText = sql;
                    Database.Me.AddParameter(cmd, "post1", post, DbType.String);
                    Database.Me.AddParameter(cmd, "post2", post, DbType.String);
                    var res = cmd.ExecuteScalar();
                    if (res != null) 
                    {
                        string newNumberStr = res.ToString();
                        if (int.TryParse(newNumberStr, out newNumberInt))
                        {
                            //throw new AppException($"Invalid new number data for post {post}");
                        }
                    }
                }


                int newNumber    = 0;
                string startTime = "";
                int sequenceId   = 0;

                using (DbCommand cmdInsert = Database.Me.Connection.CreateCommand())
                {
                    string insertSQL;
                    if (Database.Me.ProviderType == DatabaseProviderType.MSSQL)
                    {
                        insertSQL = $@"INSERT INTO {Tbl.sequences} (number,post,source) 
                                    OUTPUT INSERTED.number,INSERTED.starttime,INSERTED.id VALUES (@number, @post, @source)";
                    }
                    else
                    {
                        insertSQL = $@"INSERT INTO {Tbl.sequences} (number,post,source) VALUES (@number, @post, @source);
                                    SELECT number, starttime, id FROM {Tbl.sequences} ORDER BY id DESC LIMIT 1;";
                    }
                    cmdInsert.CommandText = insertSQL;

                    if (newNumberInt > 0)
                    {
                        //if (newNumberInt == Properties.Settings.Default.MaxQueueNumber)
                        if (newNumberInt > postData.Quota0)
                        {
                            // max queue number for the post reached, reset back to 1
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

                    using (DbDataReader reader = cmdInsert.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            newNumber  = reader.IsDBNull(0) ? 0  : reader.GetInt32(0);
                            startTime  = reader.IsDBNull(1) ? "" : reader.GetDateTime(1).ToString("dd MMMM yyyy, HH:mm");
                            sequenceId = reader.IsDBNull(2) ? 0  : reader.GetInt32(2);
                        }
                    }
                }

                if (newNumber > 0 && sequenceId > 0 && !string.IsNullOrWhiteSpace(startTime))
                {
                    // Copy data to jobs table
                    // ----------------------------
                    string insertJobSQL = $"INSERT INTO {Tbl.jobs} SELECT * FROM {Tbl.sequences} WHERE id = @id";
                        
                    using (DbCommand cmdInsertJob = Database.Me.Connection.CreateCommand())
                    {
                        cmdInsertJob.CommandText = insertJobSQL;
                        Database.Me.AddParameter(cmdInsertJob, "id", sequenceId, DbType.Int32);
                        var rc = cmdInsertJob.ExecuteNonQuery();
                        if (rc > 0) { } // sukses
                    }

                    Dictionary<string, string> result = new Dictionary<string, string>()
                    {
                        ["postprefix"] = postNumberPrefix,
                        ["number"]     = newNumber.ToString(),
                        ["post"]       = post,
                        ["timestamp"]  = startTime
                    };

                    return result;
                }
                else 
                { 
                    return new Dictionary<string, string>()
                    {
                        ["postprefix"]  = postNumberPrefix,
                        ["number"]      = "",
                        ["post"]        = post,
                        ["timestamp"]   = ""
                    };
                }
            }
            catch (Exception ex)
            {
                throw new AppException("CreateNewNumber, " + ex.Message);
            }
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
                throw new AppException("GetRunningText, " + ex.Message);
            }
        }

        public static List<string> GetStationRunningText(string station, string post)
        {
            if (! Database.Me.Connected)
                return null;

            try
            {
                string sql = $@"SELECT station_name, sticky, active, running_text FROM {Tbl.runningtexts} WHERE active=1 AND station_name = @station_name";

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
                throw new AppException("GetStationRunningText, " + ex.Message);
            }
        }

        #endregion


        #region Miscs stuff

        public static List<string> GetList(Dictionary<string, string> parameter)
        {
            if (! Database.Me.Connected)
                return null;

            try
            {
                string sql = "";
                string name = parameter["name"];

                if (name == Tbl.posts)
                    sql = $"SELECT name FROM {Tbl.posts}";
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
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("GetList, " + ex.Message);
            }
        }


        /** Get post's number prefix
            Returns post prefix
        */
        public static string GetPostNumberPrefix(string post)
        {
            if (! Database.Me.Connected)
                return "**";
            
            try
            {
                Database.Me.OpenConnection();
                using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                {
                    string sql = $"SELECT numberprefix FROM {Tbl.posts} WHERE name = @name";
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

                return "";
            }
            catch (Exception ex)
            {
                throw new AppException("GetPostNumberPrefix, " + ex.Message);
            }
        }

        public static Dto.Post GetPost(string postName)
        {
            if (!Database.Me.Connected)
                return null;

            try
            {
                Database.Me.OpenConnection();
                using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                {
                    string sql = $"SELECT name, keterangan, numberprefix, quota0, quota1 FROM {Tbl.posts} WHERE name = @name";
                    cmd.CommandText = sql;
                    Database.Me.AddParameter(cmd, "name", postName, DbType.String);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows )
                        {
                            reader.Read();

                            Dto.Post post   = new Dto.Post();

                            post.Name         = reader.GetString(0).Trim();
                            post.Keterangan   = reader.GetString(1).Trim();
                            post.NumberPrefix = reader.GetString(2).Trim();
                            post.Quota0       = reader.GetInt32(3);
                            post.Quota1       = reader.GetInt32(4);

                            return post;
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new AppException("GetPostNumberPrefix, " + ex.Message);
            }
        }


        public static int GetTableRowCount(string tablename)
        {
            object totalRow = Database.Me.ExecuteScalar("SELECT COUNT(*) FROM " + tablename);
            if (totalRow != null)
                return Convert.ToInt32(totalRow);
            
            return 0;
        }

        public static string GetSqlDateString()
        {
            if (Database.Me.ProviderType == DatabaseProviderType.SQLITE)
                return "date('now','localtime')";
            else if (Database.Me.ProviderType == DatabaseProviderType.MYSQL)
                return "CURDATE()";
            else if (Database.Me.ProviderType == DatabaseProviderType.MSSQL)
                return "CAST(getdate() AS date)";
            else if (Database.Me.ProviderType == DatabaseProviderType.PGSQL)
                return "CURRENT_DATE";
            else
                return "CAST(getdate() AS date)";
        }

        public static string GetSqlDateTimeString()
        {
            if (Database.Me.ProviderType == DatabaseProviderType.SQLITE)
                return "strftime('%Y-%m-%d %H:%M:%f','now', 'localtime')";
            else if (Database.Me.ProviderType == DatabaseProviderType.MYSQL)
               return "CURDATE()";
            else if (Database.Me.ProviderType == DatabaseProviderType.MSSQL)
                return "getdate()";
            else if (Database.Me.ProviderType == DatabaseProviderType.PGSQL)
                return "CURRENT_TIMESTAMP";
            else
                return "getdate()";
        }

        #endregion
    }
}
