CREATE TABLE queue_logins
(
   id        INTEGER     NOT NULL,
   username  VARCHAR(50) NOT NULL UNIQUE,
   password  VARCHAR(64) NOT NULL UNIQUE,
   expired   DATETIME    NOT NULL,
   active    INTEGER     NOT NULL,
   PRIMARY KEY(id AUTOINCREMENT)
);