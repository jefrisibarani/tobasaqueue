using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tobasa
{
    internal class DBMigration_PostgreSQL
    {
        public DBMigration_PostgreSQL() { }

        public static string t_queue_ipaccesslists =
            @"

            );
            ";

        public static string t_queue_jobs =
            @"

            ";

        public static string t_queue_logins =
            @"

            ";

        public static string t_queue_posts =
            @"

            ";

        public static string t_queue_runningtexts =
            @"

            ";

        public static string t_queue_sequences =
            @"

            ";

        public static string t_queue_stations =
            @"
 
            ";

        public static string v_queue_posts_summary =
            @"

            ";

        public static string v_queue_sequences =
            @"

            ";

        public static string tr_queue_update_jobs =
            @"

            ";

        public static string cmd_delete_all_tables =
            @"
            DROP TABLE queue_ipaccesslists;
            DROP TABLE queue_jobs;
            DROP TABLE queue_logins;
            DROP TABLE queue_posts ;
            DROP TABLE queue_runningtexts;
            DROP TABLE queue_sequences;
            DROP TABLE queue_stations;
            DROP VIEW v_queue_posts_summary;
            DROP VIEW v_queue_sequences;
            DROP TRIGGER tr_queue_update_jobs;
            ";

        public static string cmd_insert_all_basic_data =
            @"
            INSERT INTO queue_logins(username,password,expired,active)   VALUES
               ('tobasaqueue', 'A1410C6E07BDA0D774A76E644024801EB00175B27B85D0289469978603EBB9F4','2025-01-01 00:00:00.000',1),
               ('admin',       '51C5FB67361F529DD9DDF96959FC5FA51960E0F7516560290BD7BFCF9421C6F6','2025-01-01 00:00:00.000',1);

            INSERT INTO queue_ipaccesslists VALUES 
               ('10.62.22.1',  1, 'Komputer devepoment'),
               ('192.168.1.1', 1, 'Komputer Caller #1'),
               ('192.168.1.2', 1, 'Komputer Caller #2');
   
            INSERT INTO queue_runningtexts (station_name,sticky,active,running_text)  VALUES
               ('DISP#1', 1, 1, 'Running text Display #1 dari database server'),
               ('DISP#2', 1, 1, 'Running text Display #2 dari database server');
   
            INSERT INTO queue_posts (name,keterangan,numberprefix) VALUES
               ( 'POST0', 'Pos layanan 1',  'A'),
               ( 'POST1', 'Pos layanan 2',  'B'),
               ( 'POST2', 'Pos layanan 3',  'C'),
               ( 'POST3', 'Pos layanan 4',  'D'),
               ( 'POST4', 'Pos layanan 5',  'BK'),
               ( 'POST5', 'Pos layanan 6',  'TU'),
               ( 'POST6', 'Pos layanan 7',  'DR'),
               ( 'POST7', 'Pos layanan 8',  'P'),
               ( 'POST8', 'Pos layanan 9',  'Z'),
               ( 'POST9', 'Pos layanan 10', 'HO');   
	
            INSERT INTO queue_stations (name,post,keterangan,canlogin) VALUES
               ('ADMIN#1',  'POST0',NULL, 1),
               ('ADMIN#1',  'POST1',NULL, 1),
               ('ADMIN#1',  'POST2',NULL, 1),
               ('ADMIN#1',  'POST3',NULL, 1),
               ('ADMIN#1',  'POST4',NULL, 1),
               ('ADMIN#1',  'POST5',NULL, 1),
               ('ADMIN#1',  'POST6',NULL, 1),
               ('ADMIN#1',  'POST7',NULL, 1),
               ('ADMIN#1',  'POST8',NULL, 1),
               ('ADMIN#1',  'POST9',NULL, 1),
               ('ADMIN#2',  'POST0',NULL, 1),
               ('ADMIN#2',  'POST1',NULL, 1),
               ('ADMIN#2',  'POST2',NULL, 1),
               ('ADMIN#2',  'POST3',NULL, 1),
               ('ADMIN#2',  'POST4',NULL, 1),
               ('ADMIN#2',  'POST5',NULL, 1),
               ('ADMIN#2',  'POST6',NULL, 1),
               ('ADMIN#2',  'POST7',NULL, 1),
               ('ADMIN#2',  'POST8',NULL, 1),
               ('ADMIN#2',  'POST9',NULL, 1),

               ('CALL#1',   'POST0',NULL, 1),
               ('CALL#1',   'POST1',NULL, 1),
               ('CALL#1',   'POST2',NULL, 1),
               ('CALL#1',   'POST3',NULL, 1),
               ('CALL#1',   'POST4',NULL, 1),
               ('CALL#1',   'POST5',NULL, 1),
               ('CALL#1',   'POST6',NULL, 1),
               ('CALL#1',   'POST7',NULL, 1),
               ('CALL#1',   'POST8',NULL, 1),
               ('CALL#1',   'POST9',NULL, 1),

               ('CALL#2',   'POST0',NULL, 1),
               ('CALL#2',   'POST1',NULL, 1),
               ('CALL#2',   'POST2',NULL, 1),
               ('CALL#2',   'POST3',NULL, 1),
               ('CALL#2',   'POST4',NULL, 1),
               ('CALL#2',   'POST5',NULL, 1),
               ('CALL#2',   'POST6',NULL, 1),
               ('CALL#2',   'POST7',NULL, 1),
               ('CALL#2',   'POST8',NULL, 1),
               ('CALL#2',   'POST9',NULL, 1),

               ('CALL#3',   'POST0',NULL, 1),
               ('CALL#3',   'POST1',NULL, 1),
               ('CALL#3',   'POST2',NULL, 1),
               ('CALL#3',   'POST3',NULL, 1),
               ('CALL#3',   'POST4',NULL, 1),
               ('CALL#3',   'POST5',NULL, 1),
               ('CALL#3',   'POST6',NULL, 1),
               ('CALL#3',   'POST7',NULL, 1),
               ('CALL#3',   'POST8',NULL, 1),
               ('CALL#3',   'POST9',NULL, 1),

               ('CALL#4',   'POST0',NULL, 1),
               ('CALL#4',   'POST1',NULL, 1),
               ('CALL#4',   'POST2',NULL, 1),
               ('CALL#4',   'POST3',NULL, 1),
               ('CALL#4',   'POST4',NULL, 1),
               ('CALL#4',   'POST5',NULL, 1),
               ('CALL#4',   'POST6',NULL, 1),
               ('CALL#4',   'POST7',NULL, 1),
               ('CALL#4',   'POST8',NULL, 1),
               ('CALL#4',   'POST9',NULL, 1),

               ('CALL#5',   'POST0',NULL, 1),
               ('CALL#5',   'POST1',NULL, 1),
               ('CALL#5',   'POST2',NULL, 1),
               ('CALL#5',   'POST3',NULL, 1),
               ('CALL#5',   'POST4',NULL, 1),
               ('CALL#5',   'POST5',NULL, 1),
               ('CALL#5',   'POST6',NULL, 1),
               ('CALL#5',   'POST7',NULL, 1),
               ('CALL#5',   'POST8',NULL, 1),
               ('CALL#5',   'POST9',NULL, 1),

               ('DISP#1',   'POST0',NULL, 1),
               ('DISP#1',   'POST1',NULL, 1),
               ('DISP#1',   'POST2',NULL, 1),
               ('DISP#1',   'POST3',NULL, 1),
               ('DISP#1',   'POST4',NULL, 1),
               ('DISP#1',   'POST5',NULL, 1),
               ('DISP#1',   'POST6',NULL, 1),
               ('DISP#1',   'POST7',NULL, 1),
               ('DISP#1',   'POST8',NULL, 1),
               ('DISP#1',   'POST9',NULL, 1),

               ('DISP#2',   'POST0',NULL, 1),
               ('DISP#2',   'POST1',NULL, 1),
               ('DISP#2',   'POST2',NULL, 1),
               ('DISP#2',   'POST3',NULL, 1),
               ('DISP#2',   'POST4',NULL, 1),
               ('DISP#2',   'POST5',NULL, 1),
               ('DISP#2',   'POST6',NULL, 1),
               ('DISP#2',   'POST7',NULL, 1),
               ('DISP#2',   'POST8',NULL, 1),
               ('DISP#2',   'POST9',NULL, 1),

               ('TICKET#1', 'POST0',NULL, 1),
               ('TICKET#1', 'POST1',NULL, 1),
               ('TICKET#1', 'POST2',NULL, 1),
               ('TICKET#1', 'POST3',NULL, 1),
               ('TICKET#1', 'POST4',NULL, 1),
               ('TICKET#1', 'POST5',NULL, 1),
               ('TICKET#1', 'POST6',NULL, 1),
               ('TICKET#1', 'POST7',NULL, 1),
               ('TICKET#1', 'POST8',NULL, 1),
               ('TICKET#1', 'POST9',NULL, 1),

               ('TICKET#2', 'POST0',NULL, 1),
               ('TICKET#2', 'POST1',NULL, 1),
               ('TICKET#2', 'POST2',NULL, 1),
               ('TICKET#2', 'POST3',NULL, 1),
               ('TICKET#2', 'POST4',NULL, 1),
               ('TICKET#2', 'POST5',NULL, 1),
               ('TICKET#2', 'POST6',NULL, 1),
               ('TICKET#2', 'POST7',NULL, 1),
               ('TICKET#2', 'POST8',NULL, 1),
               ('TICKET#2', 'POST9',NULL, 1);
            ";

        public static List<string> GetCommandList()
        {
            List<string> cmdList = new List<string>
            {
                t_queue_ipaccesslists
                , t_queue_jobs
                , t_queue_logins
                , t_queue_posts
                , t_queue_runningtexts
                , t_queue_sequences
                , t_queue_stations
                , v_queue_posts_summary
                , v_queue_sequences
                , tr_queue_update_jobs
            };

            return cmdList;
        }


        public static string GetObjectSummaryQuery(string databaseName)
        {
            string sql = @"";
            return sql;
        }
    }
}