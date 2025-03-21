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
