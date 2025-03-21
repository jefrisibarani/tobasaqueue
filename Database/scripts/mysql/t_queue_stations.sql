CREATE TABLE queue_stations (
   name         VARCHAR(32) NOT NULL,
   post         VARCHAR(32) NOT NULL,
   keterangan   TEXT,
   canlogin     INTEGER     NOT NULL,
   PRIMARY KEY(name,post)
);