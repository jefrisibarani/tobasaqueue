# Software Antrian Tobasa
https://www.mangapul.net/p/software-antrian-tobasa.html

Software Antrian Open Source dan gratis.
Software sistem antrian andal untuk mengelola antrian pelanggan, mengurangi waktu tunggu, 
meningkatkan kualitas layanan dan memaksimalkan kepuasan pelanggan.

## Kebutuhan minimal
* Windows 7 Service Pack 1 
* Microsoft .NET Framework 4.6

## Build/Compile dari source code
* Clone/download project source code dari https://github.com/jefrisibarani/tobasaqueue
* Buka solution TobasaQueue.sln dengan Visual Studio 2022
* Build solution, aplikasi yang telah dibuild  ada di  folder  ***_OUTPUT***

#### Struktur folder output
```
_OUTPUT
   \---QueueAdmin
   \---QueueCaller
   \---QueueDisplay
       \---img
       \---movie
       \---wav
   \---QueueService
       \---Database
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
* https://www.mangapul.net/2016/05/download-software-antrian-tobasa.html

Lalu extract ke folder C:\AntrianTobasa


#### Menampilkan file video
Bila ingin menampilkan video pada QueueDisplay:
Copykan file video (**format wmv**), atau format lainnya(bila codec sudah terinstall pada Windows)
pada folder QueueDisplay\movie\ 


#### Menjalankan QueueService.exe sebagai  Windows service (opsional)
* Jalankan commmand prompt sebagai Administrator
* Masuk ke dalam folder QueueService
* Jalankan command berikut:
```
c:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil QueueService.exe
```

#### Untuk informasi/konfigurasi lebih lanjut
* https://www.mangapul.net/p/software-antrian-tobasa.html
* https://www.mangapul.net/2016/12/konfigurasi-aplikasi-antrian-tobasa.html


#### Download .NET
* https://dotnet.microsoft.com/download/dotnet-framework/net48


##### Copyright (C) 2026 Jefri Sibarani
