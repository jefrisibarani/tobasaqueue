# Software Antrian Tobasa
http://www.mangapul.com/p/software-antrian-tobasa.html

Software Antrian Open Source dan gratis.
Software sistem antrian andal untuk mengelola antrian pelanggan, mengurangi waktu tunggu, 
meningkatkan kualitas layanan dan memaksimalkan kepuasan pelanggan.

## Kebutuhan minimal
* Windows 7 Service Pack 1 
* Microsoft .NET Framework 4.0

Bila ingin menggunakan SQL Server sebagai database:
* Microsoft SQL Server 2008 R2 Express Edition 

Dengan konfigurasi default, tidak perlu menggunakan Microsoft SQL Server, 
karena Antrian Tobasa menggunakan database SQLite.


## Build/Compile dari source code
* Clone/download project source code dari https://github.com/jefrisibarani/tobasaqueue
* Buka solution TobasaQueue.sln dengan Visual Studio 2019
* Build solution, aplikasi yang telah dibuild  ada di  folder  ***_OUTPUT***

#### Struktur folder output
```
_OUTPUT
   \---Database
   \---QueueAdmin
   \---QueueCaller
   \---QueueDisplay
       \---img
       \---movie
       \---wav
   \---QueueService
   \---QueueTicket
       \---img
   \---LICENSE
   \---QueueConfig.exe
   \---README.md
   \---startall_output.cmd
```

## Instalasi
Aplikasi yang telah dibuild/compile ada pada folder _OUTPUT, copy folder _OUTPUT ke folder lain: 
misalkan C:\AntrianTobasa

Atau silahkan download versi binary dari:
* https://github.com/jefrisibarani/tobasaqueue/releases
* http://www.mangapul.com/2016/05/download-software-antrian-tobasa.html

Lalu extract ke folder C:\AntrianTobasa


#### Bila ingin menggunakan SQL Server sebagai database:
* Restore database ***sqlserver_2008_r2_antri.bak*** yang ada di folder Database,
  ikuti petunjuk pada file **Database\install_db_sqlserver.md**
  
* Jalankan program server **QueueService.exe** pada folder QueueService
* Jalankan program QueueDisplay.exe, QueueAdmin.exe, QueueCaller.exe


#### Menampilkan file video
Bila ingin menampilkan video pada QueueDisplay:
Copykan file video (**format wmv**), atau format lainnya(bila codec sudah terinstall pada windows)
pada folder QueueDisplay\movie\ 


#### Menjalankan QueueService.exe sebagai  Windows service (opsional)
* Jalankan commmand prompt sebagai Administrator
* Masuk ke dalam folder QueueService
* Jalankan command berikut:
```
c:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil QueueService.exe
```

#### Untuk informasi/konfigurasi lebih lanjut
* http://www.mangapul.com/p/software-antrian-tobasa.html
* http://www.mangapul.com/2016/12/konfigurasi-aplikasi-antrian-tobasa.html


#### Download .NET
* https://dotnet.microsoft.com/download/dotnet-framework/net48


##### Copyright (C) 2021 Jefri Sibarani
