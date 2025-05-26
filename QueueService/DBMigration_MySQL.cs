using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tobasa
{
    internal class DBMigration_MySQL
    {
        public DBMigration_MySQL() { }

        public static string t_queue_ipaccesslists =
            @"
            CREATE TABLE queue_ipaccesslists (
               ipaddress  VARCHAR(15)   NOT NULL,
               allowed    INTEGER NOT   NULL,
               keterangan VARCHAR(256),
               PRIMARY KEY(ipaddress)
            );
            ";

        public static string t_queue_jobs =
            @"
            CREATE TABLE queue_jobs
            (
               id          INTEGER      NOT NULL,
               number      INTEGER      NOT NULL,
               status      VARCHAR(10)  NOT NULL DEFAULT 'WAITING',
               station     VARCHAR(32),
               post        VARCHAR(32)  NOT NULL,
               source      VARCHAR(32)  NOT NULL,
               date        DATE         NOT NULL DEFAULT (CURRENT_DATE),
               starttime   DATETIME     NOT NULL DEFAULT (CURRENT_TIMESTAMP),
               calltime    DATETIME,
               endtime     DATETIME,
               call2time   DATETIME,
               bookingcode VARCHAR(50) NOT NULL DEFAULT '',   
               PRIMARY KEY(id)
            );
            ";

        public static string t_queue_logins =
            @"
            CREATE TABLE queue_logins 
            (
               id        INTEGER     NOT NULL AUTO_INCREMENT,
               username  VARCHAR(50) NOT NULL UNIQUE,
               password  VARCHAR(64) NOT NULL UNIQUE,
               expired   DATETIME    NOT NULL,
               active    INTEGER     NOT NULL,
               PRIMARY KEY(id)
            );
            ";

        public static string t_queue_posts =
            @"
            CREATE TABLE queue_posts 
            (
               name         VARCHAR(32)  NOT NULL,
               keterangan   VARCHAR(256) NOT NULL DEFAULT '',
               numberprefix VARCHAR(5)   NOT NULL DEFAULT '',
               quota0       INTEGER      NOT NULL DEFAULT 1000,
               quota1       INTEGER      NOT NULL DEFAULT 1000,
               PRIMARY KEY(name)
            );
            ";

        public static string t_queue_runningtexts =
            @"
            CREATE TABLE queue_runningtexts (
               id           INTEGER     NOT NULL AUTO_INCREMENT,
               station_name VARCHAR(32) NOT NULL,
               sticky       INTEGER     NOT NULL DEFAULT 0,
               active       INTEGER     NOT NULL DEFAULT 0,
               running_text TEXT,
               PRIMARY KEY(id)
            );
            ";

        public static string t_queue_sequences =
            @"
            CREATE TABLE queue_sequences
            (
               id          INTEGER      NOT NULL AUTO_INCREMENT,
               number      INTEGER      NOT NULL,
               status      VARCHAR(10)  NOT NULL DEFAULT 'WAITING',
               station     VARCHAR(32),
               post        VARCHAR(32)  NOT NULL,
               source      VARCHAR(32)  NOT NULL,
               date        DATE         NOT NULL DEFAULT (CURRENT_DATE),
               starttime   DATETIME     NOT NULL DEFAULT (CURRENT_TIMESTAMP),
               calltime    DATETIME,
               endtime     DATETIME,
               call2time   DATETIME,
               bookingcode VARCHAR(50) NOT NULL DEFAULT '',
               PRIMARY KEY(id)
            );
            ";

        public static string t_queue_stations =
            @"
             CREATE TABLE queue_stations (
               name         VARCHAR(32) NOT NULL,
               post         VARCHAR(32) NOT NULL,
               keterangan   TEXT,
               canlogin     INTEGER     NOT NULL,
               PRIMARY KEY(name,post)
            );
            ";

        public static string v_queue_posts_summary =
            @"
            CREATE VIEW v_queue_posts_summary AS 
            SELECT p.name, p.numberprefix, p.keterangan
            ,( SELECT MAX(number)   FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = CURRENT_DATE ) AS called_last
            ,( SELECT COUNT(number) FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = CURRENT_DATE ) AS called_total
            ,( SELECT MIN(number)   FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = CURRENT_DATE ) AS waiting_first
            ,( SELECT MAX(number)   FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = CURRENT_DATE ) AS waiting_last
            ,( SELECT COUNT(number) FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = CURRENT_DATE ) AS waiting_total
            ,( SELECT station       FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = CURRENT_DATE
                  AND number = ( SELECT MAX(number) FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = CURRENT_DATE )
             ) AS last_station
            FROM queue_posts p;
            ";

        public static string v_queue_posts_summary_a =
            @"
            CREATE VIEW v_queue_posts_summary_a AS
            WITH 
            today_sequences AS ( SELECT * FROM queue_sequences WHERE date = CURDATE() ),
            per_post_stats AS (
              SELECT post,
                MAX(CASE WHEN status = 'PROCESS' THEN number END) AS called_last,
                COUNT(CASE WHEN status = 'PROCESS' THEN 1 END) AS called_total,
                MIN(CASE WHEN status = 'WAITING' THEN number END) AS waiting_first,
                MAX(CASE WHEN status = 'WAITING' THEN number END) AS waiting_last,
                COUNT(CASE WHEN status = 'WAITING' THEN 1 END) AS waiting_total
              FROM today_sequences GROUP BY post
            ),
            last_station_info AS (
              SELECT post, station FROM today_sequences WHERE status = 'PROCESS' AND (post, number) 
              IN ( SELECT post, MAX(number) FROM today_sequences WHERE status = 'PROCESS' GROUP BY post )
            )
            SELECT  p.name, p.numberprefix, p.keterangan, s.called_last, s.called_total, s.waiting_first,
              s.waiting_last, s.waiting_total, l.station AS last_station
            FROM queue_posts p
            LEFT JOIN per_post_stats s ON s.post = p.name
            LEFT JOIN last_station_info l ON l.post = p.name;
            ";

        public static string v_queue_sequences =
            @"
            CREATE VIEW v_queue_sequences
            AS 
            SELECT seq.id, 
                   seq.number, 
                   seq.status, 
                   seq.station, 
                   seq.post, 
                   seq.source, 
                   seq.starttime, 
                   seq.endtime, 
                   vsr.numberleft,
                   vsr.numbermax
            FROM queue_sequences AS seq
            JOIN
               (
                  SELECT post, MIN(id) AS idmin , COUNT(number) AS numberleft, MAX(number) AS numbermax
                  FROM queue_sequences WHERE status = 'WAITING' AND date = CURRENT_DATE
                  GROUP BY post
               ) AS vsr
            ON seq.post = vsr.post
            WHERE seq.id = vsr.idmin
            AND date = CURRENT_DATE;
            ";

        public static string tr_queue_update_jobs =
            @"
            CREATE TRIGGER tr_queue_update_jobs
            AFTER UPDATE ON queue_sequences 
            FOR EACH ROW
            BEGIN
             UPDATE queue_jobs
             SET   number     = NEW.number
                  ,status     = NEW.status
                  ,station    = NEW.station
                  ,post       = NEW.post
                  ,source     = NEW.source
                  ,date       = NEW.date
                  ,starttime  = NEW.starttime
                  ,calltime   = NEW.calltime
                  ,call2time  = NEW.call2time
                  ,endtime    = NEW.endtime
            WHERE id = NEW.id;
            END
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
               ('tobasaqueue', 'A1410C6E07BDA0D774A76E644024801EB00175B27B85D0289469978603EBB9F4','2035-01-01 00:00:00.000',1),
               ('admin',       '51C5FB67361F529DD9DDF96959FC5FA51960E0F7516560290BD7BFCF9421C6F6','2035-01-01 00:00:00.000',1);

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

               ('CALL#6',   'POST0',NULL, 1),
               ('CALL#6',   'POST1',NULL, 1),
               ('CALL#6',   'POST2',NULL, 1),
               ('CALL#6',   'POST3',NULL, 1),
               ('CALL#6',   'POST4',NULL, 1),
               ('CALL#6',   'POST5',NULL, 1),
               ('CALL#6',   'POST6',NULL, 1),
               ('CALL#6',   'POST7',NULL, 1),
               ('CALL#6',   'POST8',NULL, 1),
               ('CALL#6',   'POST9',NULL, 1),

               ('CALL#7',   'POST0',NULL, 1),
               ('CALL#7',   'POST1',NULL, 1),
               ('CALL#7',   'POST2',NULL, 1),
               ('CALL#7',   'POST3',NULL, 1),
               ('CALL#7',   'POST4',NULL, 1),
               ('CALL#7',   'POST5',NULL, 1),
               ('CALL#7',   'POST6',NULL, 1),
               ('CALL#7',   'POST7',NULL, 1),
               ('CALL#7',   'POST8',NULL, 1),
               ('CALL#7',   'POST9',NULL, 1),

               ('CALL#8',   'POST0',NULL, 1),
               ('CALL#8',   'POST1',NULL, 1),
               ('CALL#8',   'POST2',NULL, 1),
               ('CALL#8',   'POST3',NULL, 1),
               ('CALL#8',   'POST4',NULL, 1),
               ('CALL#8',   'POST5',NULL, 1),
               ('CALL#8',   'POST6',NULL, 1),
               ('CALL#8',   'POST7',NULL, 1),
               ('CALL#8',   'POST8',NULL, 1),
               ('CALL#8',   'POST9',NULL, 1),

               ('CALL#9',   'POST0',NULL, 1),
               ('CALL#9',   'POST1',NULL, 1),
               ('CALL#9',   'POST2',NULL, 1),
               ('CALL#9',   'POST3',NULL, 1),
               ('CALL#9',   'POST4',NULL, 1),
               ('CALL#9',   'POST5',NULL, 1),
               ('CALL#9',   'POST6',NULL, 1),
               ('CALL#9',   'POST7',NULL, 1),
               ('CALL#9',   'POST8',NULL, 1),
               ('CALL#9',   'POST9',NULL, 1),

               ('CALL#10',  'POST0',NULL, 1),
               ('CALL#10',  'POST1',NULL, 1),
               ('CALL#10',  'POST2',NULL, 1),
               ('CALL#10',  'POST3',NULL, 1),
               ('CALL#10',  'POST4',NULL, 1),
               ('CALL#10',  'POST5',NULL, 1),
               ('CALL#10',  'POST6',NULL, 1),
               ('CALL#10',  'POST7',NULL, 1),
               ('CALL#10',  'POST8',NULL, 1),
               ('CALL#10',  'POST9',NULL, 1),

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
               ('TICKET#2', 'POST9',NULL, 1),

               ('TICKET#3', 'POST0',NULL, 1),
               ('TICKET#3', 'POST1',NULL, 1),
               ('TICKET#3', 'POST2',NULL, 1),
               ('TICKET#3', 'POST3',NULL, 1),
               ('TICKET#3', 'POST4',NULL, 1),
               ('TICKET#3', 'POST5',NULL, 1),
               ('TICKET#3', 'POST6',NULL, 1),
               ('TICKET#3', 'POST7',NULL, 1),
               ('TICKET#3', 'POST8',NULL, 1),
               ('TICKET#3', 'POST9',NULL, 1),

               ('TICKET#4', 'POST0',NULL, 1),
               ('TICKET#4', 'POST1',NULL, 1),
               ('TICKET#4', 'POST2',NULL, 1),
               ('TICKET#4', 'POST3',NULL, 1),
               ('TICKET#4', 'POST4',NULL, 1),
               ('TICKET#4', 'POST5',NULL, 1),
               ('TICKET#4', 'POST6',NULL, 1),
               ('TICKET#4', 'POST7',NULL, 1),
               ('TICKET#4', 'POST8',NULL, 1),
               ('TICKET#4', 'POST9',NULL, 1);
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
                , v_queue_posts_summary_a
                , v_queue_sequences
                , tr_queue_update_jobs
            };

            return cmdList;
        }

        public static string GetObjectSummaryQuery(string databaseName)
        {
            string sql = $@"
            SELECT
              ( SELECT COUNT(*) FROM information_schema.tables 
                WHERE TABLE_TYPE = 'BASE TABLE'
                AND table_schema = '{databaseName}' AND table_name LIKE 'queue_%') AS total_table
                
            , ( SELECT COUNT(*) FROM information_schema.tables 
                WHERE TABLE_TYPE = 'VIEW' 
                AND table_schema = '{databaseName}' AND table_name LIKE 'v_queue_%') AS total_view
                
            , ( SELECT COUNT(*) FROM information_schema.TRIGGERS 
                WHERE EVENT_OBJECT_TABLE = 'queue_sequences' AND TRIGGER_SCHEMA = '{databaseName}' 
                AND TRIGGER_NAME = 'tr_queue_update_jobs' ) AS total_trigger;
            ";

            return sql;
        }
    }
}

