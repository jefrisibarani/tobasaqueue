CREATE TABLE queue_stations (
   name         VARCHAR(32)   NOT NULL,
   post         VARCHAR(32)   NOT NULL,
   keterangan   VARCHAR(256),
   canlogin     BIT           NOT NULL,
   PRIMARY KEY(name,post)
);  