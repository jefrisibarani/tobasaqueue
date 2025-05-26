using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Tobasa
{
    internal class DBMigration
    {
        public static bool InitializeDatabase()
        {
            try
            {
                Database.Me.OpenConnection();

                List<string> commandList = new List<string>();
                string objectSummarySql = "";
                string pupulateDataSql = "";

                if (Database.Me.ProviderType == DatabaseProviderType.SQLITE)
                {
                    commandList = DBMigration_SQLite.GetCommandList();
                    objectSummarySql = DBMigration_SQLite.GetObjectSummaryQuery(Database.Me.DatabaseName);
                    pupulateDataSql = DBMigration_SQLite.cmd_insert_all_basic_data;
                }
                else if (Database.Me.ProviderType == DatabaseProviderType.MYSQL)
                {
                    commandList = DBMigration_MySQL.GetCommandList();
                    objectSummarySql = DBMigration_MySQL.GetObjectSummaryQuery(Database.Me.DatabaseName);
                    pupulateDataSql = DBMigration_MySQL.cmd_insert_all_basic_data;
                }
                else if (Database.Me.ProviderType == DatabaseProviderType.MSSQL)
                {
                    commandList = DBMigration_MSSQL.GetCommandList();
                    objectSummarySql = DBMigration_MSSQL.GetObjectSummaryQuery(Database.Me.DatabaseName);
                    pupulateDataSql = DBMigration_MSSQL.cmd_insert_all_basic_data;
                }
                else if (Database.Me.ProviderType == DatabaseProviderType.PGSQL)
                {
                    commandList = DBMigration_PostgreSQL.GetCommandList();
                    objectSummarySql = DBMigration_PostgreSQL.GetObjectSummaryQuery(Database.Me.DatabaseName);
                    pupulateDataSql = DBMigration_PostgreSQL.cmd_insert_all_basic_data;
                }
                else
                {
                    QueueServer.Log("DBMigration failed due to unsupported database type");
                    return false;
                }

                // Check for tables, views and triggers
                // ---------------------------------------------------------------------------------
                int tablesFound  = 0;
                int triggerFound = 0;
                int viewsFound   = 0;
                using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                {
                    cmd.CommandText = objectSummarySql;
                    //QueueServer.Log($"DBMigration Executing command \n{objectSummarySql}");
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            tablesFound  = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            viewsFound   = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            triggerFound = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        }
                    }
                }

                if (tablesFound == 7 && viewsFound == 3 && triggerFound == 1)
                {
                    QueueServer.Log($"DBMigration found correct database objects");
                    return true;
                }
                else if (tablesFound != 7 && viewsFound != 3 && triggerFound != 1)
                {
                    QueueServer.Log($"DBMigration creating database objects");

                    int affected0 = 0;
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        foreach (string sqlCmd in commandList)
                        {
                            QueueServer.Log($"DBMigration Executing command \n{sqlCmd}");

                            cmd.CommandText = sqlCmd;
                            affected0 = cmd.ExecuteNonQuery();
                        }
                    }

                    QueueServer.Log($"DBMigration populating basic data");
                    int affected1 = 0;
                    using (DbCommand cmd = Database.Me.Connection.CreateCommand())
                    {
                        QueueServer.Log($"DBMigration Executing command \n{pupulateDataSql}");

                        cmd.CommandText = pupulateDataSql;
                        affected1 = cmd.ExecuteNonQuery();
                    }

                    QueueServer.Log($"DBMigration completed successfully");

                    return true;
                }
                else
                {
                    QueueServer.Log($"DBMigration found {tablesFound} tables, {viewsFound} views, {triggerFound} trigger");
                    return false;
                }
            }
            catch (Exception e)
            {
                QueueServer.Log("DBMigration failed due to exception: " + e.Message);
            }

            return false;
        }
    }
}
