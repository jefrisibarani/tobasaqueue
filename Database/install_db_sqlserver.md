# Instalasi database di MS SQL Server

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
   
# Pengaturan File Konfigurasi

Gunakan tool QueueConfig.exe untuk mengatur konfigurasi pada semua file konfigurasi.
   
# Links:
http://www.mangapul.net/2016/12/konfigurasi-aplikasi-antrian-tobasa.html
http://www.mangapul.net/2018/06/instalasi-sql-server-2008-r2-express.html
http://www.mangapul.net/2018/06/sql-server-2008-tcpip-network.html
http://www.mangapul.net/2018/06/setting-windows-firewall-untuk-sql.html
http://www.mangapul.net/2018/06/membuat-dan-restore-database-sql-server.html