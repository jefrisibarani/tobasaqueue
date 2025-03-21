CREATE TABLE queue_runningtexts (
   id           INTEGER     NOT NULL,
   station_name VARCHAR(32) NOT NULL,
   sticky       TINYINT     NOT NULL DEFAULT 0,
   active       TINYINT     NOT NULL DEFAULT 0,
   running_text TEXT,
   PRIMARY KEY(id AUTOINCREMENT)
);