# Software Antrian Tobasa
http://www.mangapul.com/p/software-antrian-tobasa.html

Software sistem antrian andal untuk mengelola antrian pelanggan, mengurangi waktu tunggu, meningkatkan kualitas layanan dan memaksimalkan kepuasan customer.

## Kebutuhan minimal
* Windows 7 
* Microsoft .NET Framework 4
* Microsoft SQL Server 2008 R2 Express Edition

## Build/Compile
* Buka solution TobasaQueue.sln dengan Visual Studio 2015
* Build solution, aplikasi yang telah dibuild  ada di  folder ***_OUTPUT***

#### Struktur folder output
```
_OUTPUT
   \--Database
   \--QueueAdmin
   \--QueueCaller
   \--QueueDisplay
      \--img
      \--movie
      \--wav
   \--QueueService
   \--QueueTicket
      \--img
   \--LICENSE
   \--README.md
   \--startall_output.cmd
```

## Instalasi
Aplikasi yang telah dibuild/compile ada pada folder _OUTPUT, copy folder _OUTPUT ke folder lain: misalkan C:\AntrianTobasa

* Restore database ***antri.bak*** yang ada di folder Database,
  ikuti petunjuk pada file Database\INSTALL_DB.txt
* Jalankan program server QueueService.exe pada folder QueueService
* Jalankan program QueueDisplay, QueueAdmin, QueueCaller

#### Menjalankan QueueService.exe sebagai  Windows service
* Jalankan commmand prompt sebagai Administrator
* Masuk ke dalam folder QueueService
* Jalankan command berikut:
```
* c:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil QueueService.exe
```
#### Menampilkan file video
Bila ingin menampilkan video pada QueueDisplay,
copykan file video (wmv) pada folder QueueDisplay\movie\ 

#### Untuk informasi/konfigurasi lebih lanjut
* http://www.mangapul.com/p/software-antrian-tobasa.html
* http://www.mangapul.com/2016/12/konfigurasi-aplikasi-antrian-tobasa.html


##### Copyright (C) 2018 Jefri Sibarani
