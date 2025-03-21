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