@echo off@echo off
TITLE Setup Point Blank DB by Skelleton
set mysql="C:\Program Files\MySQL\MySQL Server 5.5\bin\mysql.exe"
set user=root
set pass=123456
set DBHost=localhost
set DBname=pbbeta

%mysql% -h %DBHost% -u %user% --password=%pass% --execute="CREATE DATABASE IF NOT EXISTS %DBname%"

%mysql% -h %DBHost% -u %user% --password=%pass% -D %DBname% < pbbeta.sql
