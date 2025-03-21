CREATE TABLE queue_logins
(
   id        INTEGER     IDENTITY(1,1) PRIMARY KEY,
   username  VARCHAR(50) NOT NULL UNIQUE,
   password  VARCHAR(64) NOT NULL UNIQUE,
   expired   DATETIME    NOT NULL,
   active    BIT         NOT NULL
);