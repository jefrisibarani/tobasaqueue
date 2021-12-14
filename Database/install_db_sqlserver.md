# Instalasi database

1. Install Microsoft SQL Server 2008 R2 Express Edition
   Pastikan SQL Server bisa diakses lewat network(TCP/IP) di port 1433

2. Buat user login di SQL Server
   (SQL Server authentication)
      login name : antrian
      password   : TOBASA

3. Buat database
      nama database	: antri
      owner database	: antrian
      recovery model : Full
   
4. Restore database backup sqlserver_2008_R2_antri.bak ke database yang dibuat pada step 3
   Pastikan untuk Overwrite/Replace database 
   
5. Set kembali owner database antri ke user "antrian" yang dibuat di step 2

# Pengaturan File Konfigurasi

Set file konfigurasi QueueService.exe.config agar aplikasi menggunakan SQL server sebagai database.
Pada bagian **connectionString**, set dengan:
`
   "Provider=SQLOLEDB;Data Source=127.0.0.1,1433;User ID=antrian;Initial Catalog=antri;"
`	
Pada bagian **ProviderType**, set dengan:
`
   OLEDB
`
Pada bagian **ConnectionStringPassword**, set dengan:
`		
   ad7415644add93d6e719d2b593da6e6e
`
Atau gunakan tool QueueConfig.exe untuk mengatur konfigurasi pada semua file konfigurasi.
   
# Links:
http://www.mangapul.com/2016/12/konfigurasi-aplikasi-antrian-tobasa.html
http://www.mangapul.com/2018/06/instalasi-sql-server-2008-r2-express.html
http://www.mangapul.com/2018/06/sql-server-2008-tcpip-network.html
http://www.mangapul.com/2018/06/setting-windows-firewall-untuk-sql.html
http://www.mangapul.com/2018/06/membuat-dan-restore-database-sql-server.html