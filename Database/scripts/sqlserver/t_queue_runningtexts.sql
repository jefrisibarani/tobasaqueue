CREATE TABLE queue_runningtexts (
   id           INTEGER IDENTITY(1,1) PRIMARY KEY,
   station_name VARCHAR(32)   NOT NULL,
   sticky       BIT           NOT NULL DEFAULT 0,
   active       BIT           NOT NULL DEFAULT 0,
   running_text VARCHAR(2048)
);