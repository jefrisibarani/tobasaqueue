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
