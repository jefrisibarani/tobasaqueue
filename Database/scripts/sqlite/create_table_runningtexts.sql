CREATE TABLE "runningtexts" (
   "id"	         INTEGER     NOT NULL,
   "station_name"	VARCHAR(32) NOT NULL,
   "sticky"	      INTEGER     NOT NULL,
   "active"	      INTEGER     NOT NULL,
   "running_text"	TEXT,
   PRIMARY KEY("id" AUTOINCREMENT)
)