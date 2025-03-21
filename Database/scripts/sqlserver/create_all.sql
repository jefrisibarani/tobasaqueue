CREATE TABLE queue_ipaccesslists (
   ipaddress  VARCHAR(15)   NOT NULL,
   allowed    BIT           NOT   NULL,
   keterangan VARCHAR(256),
   PRIMARY KEY(ipaddress)
);



CREATE TABLE queue_jobs
(
   id          INTEGER      IDENTITY(1,1) PRIMARY KEY,
   number      INTEGER      NOT NULL,
   status      VARCHAR(10)  NOT NULL DEFAULT 'WAITING',
   station     VARCHAR(32),
   post        VARCHAR(32)  NOT NULL,
   source      VARCHAR(32)  NOT NULL,
   date        DATE         NOT NULL DEFAULT getdate(),
   starttime   DATETIME     NOT NULL DEFAULT getdate(),
   calltime    DATETIME,
   endtime     DATETIME,
   call2time   DATETIME,
   bookingcode VARCHAR(50) NOT NULL DEFAULT ''
);



CREATE TABLE queue_logins
(
   id        INTEGER     IDENTITY(1,1) PRIMARY KEY,
   username  VARCHAR(50) NOT NULL UNIQUE,
   password  VARCHAR(64) NOT NULL UNIQUE,
   expired   DATETIME    NOT NULL,
   active    BIT         NOT NULL
);



CREATE TABLE queue_posts 
(
   name         VARCHAR(32)  NOT NULL,
   keterangan   VARCHAR(256) NOT NULL DEFAULT '',
   numberprefix VARCHAR(2)	  NOT NULL DEFAULT '',
   PRIMARY KEY(name)
);



CREATE TABLE queue_runningtexts (
   id           INTEGER IDENTITY(1,1) PRIMARY KEY,
   station_name VARCHAR(32)   NOT NULL,
   sticky       BIT           NOT NULL DEFAULT 0,
   active       BIT           NOT NULL DEFAULT 0,
   running_text VARCHAR(2048)
);



CREATE TABLE queue_sequences
(
   id          INTEGER      IDENTITY(1,1) PRIMARY KEY,
   number      INTEGER      NOT NULL,
   status      VARCHAR(10)  NOT NULL DEFAULT 'WAITING',
   station     VARCHAR(32),
   post        VARCHAR(32)  NOT NULL,
   source      VARCHAR(32)  NOT NULL,
   date        DATE         NOT NULL DEFAULT getdate(),
   starttime   DATETIME     NOT NULL DEFAULT getdate(),
   calltime    DATETIME,
   endtime     DATETIME,
   call2time   DATETIME,
   bookingcode VARCHAR(50) NOT NULL DEFAULT ''
);



CREATE TABLE queue_stations (
   name         VARCHAR(32)   NOT NULL,
   post         VARCHAR(32)   NOT NULL,
   keterangan   VARCHAR(256),
   canlogin     BIT           NOT NULL,
   PRIMARY KEY(name,post)
);  



CREATE VIEW v_queue_posts_summary
AS 
SELECT p.name, p.numberprefix, p.keterangan
,( SELECT MAX(NUMBER)   FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = CAST(getdate()as DATE) ) AS called_last
,( SELECT COUNT(number) FROM queue_sequences WHERE status = 'PROCESS' AND post = p.name AND date = CAST(getdate()as DATE) ) AS called_total
,( SELECT MIN(number)   FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = CAST(getdate()as DATE) ) AS waiting_first
,( SELECT MAX(number)   FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = CAST(getdate()as DATE) ) AS waiting_last
,( SELECT COUNT(number) FROM queue_sequences WHERE status = 'WAITING' AND post = p.name AND date = CAST(getdate()as DATE) ) AS waiting_total
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
      FROM queue_sequences WHERE status = 'WAITING' AND date = CAST(getdate() AS date)
      GROUP BY post
   ) AS vsr
ON seq.post = vsr.post
WHERE seq.id = vsr.idmin
AND date = CAST(getdate() AS date);



CREATE TRIGGER tr_queue_update_jobs
ON queue_sequences
FOR UPDATE AS
BEGIN
 SET NOCOUNT ON
 UPDATE queue_jobs
 SET   number     = i.number
      ,status     = i.status
      ,station    = i.station
      ,post       = i.post
      ,source     = i.source
      ,date       = i.date
      ,starttime  = i.starttime
      ,calltime   = i.calltime
      ,call2time  = i.call2time
      ,endtime    = i.endtime
 FROM INSERTED as i
 INNER JOIN queue_jobs on queue_jobs.id = i.id
END;