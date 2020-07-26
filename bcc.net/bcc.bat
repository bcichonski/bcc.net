@echo off
@set pathcopy=%path%
rem change paths below
@set path=%path%;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools
echo *********************************** COMPILATION ***********************************
bcc %1
echo *********************************** ASSEMBLY **************************************
ilasm %~p1%~n1.il /DEBUG
echo *********************************** VERIFICATION ***********************************
peverify %~p1%~n1.exe /md /il
echo *********************************** EXECUTION *************************************
echo.
%~p1%~n1.exe
@set path=%pathcopy%
@echo on
