CREATE TABLE "ipaccesslists" (
   "ipaddress"	 VARCHAR(15)   NOT NULL,
   "allowed"	 INTEGER NOT   NULL,
   "keterangan" VARCHAR(256),
   PRIMARY KEY("ipaddress")
)