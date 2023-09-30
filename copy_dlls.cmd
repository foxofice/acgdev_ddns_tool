@echo off
cd /d %~dp0

if not exist bin		md bin
if not exist bin\x64		md bin\x64
if not exist bin\x64\Debug	md bin\x64\Debug
if not exist bin\x64\Release	md bin\x64\Release

echo [x64 - Debug]
copy nnn\Libs\x64\Debug\*.dll	bin\x64\Debug /y

echo [x64 - Release]
copy nnn\Libs\x64\Release\*.dll	bin\x64\Release /y

pause
