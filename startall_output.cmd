@echo off

SETLOCAL
SET OUTPUTDIR=%~dp0

cd %OUTPUTDIR%\QueueService
start QueueService.exe

ping 192.0.2.2 -n 1 -w 900 > nul
cd %OUTPUTDIR%\QueueAdmin
start QueueAdmin.exe

ping 192.0.2.2 -n 1 -w 500 > nul
cd %OUTPUTDIR%\QueueCaller
start QueueCaller.exe

ping 192.0.2.2 -n 1 -w 500 > nul
cd %OUTPUTDIR%\QueueDisplay
start QueueDisplay.exe

ping 192.0.2.2 -n 1 -w 500 > nul
cd %OUTPUTDIR%\QueueTicket
start QueueTicket.exe

cd %OUTPUTDIR%