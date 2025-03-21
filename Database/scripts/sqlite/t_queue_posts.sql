CREATE TABLE queue_posts 
(
   name         VARCHAR(32)  NOT NULL,
   keterangan   VARCHAR(256) NOT NULL DEFAULT '',
   numberprefix VARCHAR(2)	  NOT NULL DEFAULT '',
   PRIMARY KEY(name)
);