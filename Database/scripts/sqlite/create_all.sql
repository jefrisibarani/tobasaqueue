CREATE TABLE queue_ipaccesslists (
   ipaddress  VARCHAR(15)   NOT NULL,
   allowed    INTEGER NOT   NULL,
   keterangan VARCHAR(256),
   PRIMARY KEY(ipaddress)
);

CREATE TABLE queue_jobs
(
   id        INTEGER      NOT NULL,
   number    INTEGER      NOT NULL,
   status    VARCHAR(10)  NOT NULL DEFAULT 'WAITING',
   station   VARCHAR(32),
   post      VARCHAR(32)  NOT NULL,
   source    VARCHAR(32)  NOT NULL,
   date      DATE         NOT NULL DEFAULT (date('now','localtime')),
   starttime DATETIME     NOT NULL DEFAULT (strftime('%Y-%m-%d %H:%M:%f','now', 'localtime')),
   calltime  DATETIME,
   endtime   DATETIME,
   call2time DATETIME,
   bookingcode VARCHAR(50) NOT NULL DEFAULT '', 
   PRIMARY KEY(id AUTOINCREMENT)
);

CREATE TABLE queue_logins
(
   id        INTEGER     NOT NULL,
   username  VARCHAR(50) NOT NULL UNIQUE,
   password  VARCHAR(64) NOT NULL UNIQUE,
   expired   DATETIME    NOT NULL,
   active    INTEGER     NOT NULL,
   PRIMARY KEY(id AUTOINCREMENT)
);

CREATE TABLE queue_posts 
(
   name         VARCHAR(32)  NOT NULL,
   keterangan   VARCHAR(256) NOT NULL DEFAULT '',
   numberprefix VARCHAR(2)	  NOT NULL DEFAULT '',
   PRIMARY KEY(name)
);

CREATE TABLE queue_runningtexts (
   id           INTEGER     NOT NULL,
   station_name VARCHAR(32) NOT NULL,
   sticky       TINYINT     NOT NULL DEFAULT 0,
   active       TINYINT     NOT NULL DEFAULT 0,
   running_text TEXT,
   PRIMARY KEY(id AUTOINCREMENT)
);

CREATE TABLE queue_sequences 
(
   id          INTEGER      NOT NULL,
   number      INTEGER      NOT NULL,
   status      VARCHAR(10)  NOT NULL DEFAULT 'WAITING',
   station     VARCHAR(32),
   post        VARCHAR(32)  NOT NULL,
   source      VARCHAR(32)  NOT NULL,
   date        DATE         NOT NULL DEFAULT (date('now','localtime')),
   starttime   DATETIME     NOT NULL DEFAULT (strftime('%Y-%m-%d %H:%M:%f','now', 'localtime')),
   calltime    DATETIME,
   endtime     DATETIME,
   call2time   DATETIME,
   bookingcode VARCHAR(50) NOT NULL DEFAULT '',
   PRIMARY KEY(id AUTOINCREMENT)
);

CREATE TABLE queue_stations (
   name         VARCHAR(32) NOT NULL,
   post         VARCHAR(32) NOT NULL,
   keterangan   TEXT,
   canlogin     INTEGER     NOT NULL,
   PRIMARY KEY(name,post)
);

CREATE VIEW v_queue_posts_summary
AS 
SELECT p.name, p.numberprefix, p.keterangan
,( SELECT MAX(NUMBER)   FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = date('now','localtime') ) AS called_last
,( SELECT COUNT(number) FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = date('now','localtime') ) AS called_total
,( SELECT MIN(number)   FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = date('now','localtime') ) AS waiting_first
,( SELECT MAX(number)   FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = date('now','localtime') ) AS waiting_last
,( SELECT COUNT(number) FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = date('now','localtime') ) AS waiting_total
FROM queue_posts p;


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
      FROM queue_sequences WHERE status = 'WAITING' AND date = date('now','localtime')
      GROUP BY post
   ) AS vsr
ON seq.post = vsr.post
WHERE seq.id = vsr.idmin
AND date = date('now','localtime');



CREATE TRIGGER tr_queue_update_jobs
UPDATE OF status ON queue_sequences 
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