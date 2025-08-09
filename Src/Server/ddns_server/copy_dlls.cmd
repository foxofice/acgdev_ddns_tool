::%1 - $(Configuration)		(Debug »ò Release)
::%2 - $(OutDir)			£¨..\..\..\bin\x64\Debug\£©

set EXE=..\..\..\diff_copy.exe
set SRC_DIR=..\..\..\3rdParty\nnn\Libs\x64\%1
set DST_DIR=%2

%EXE%	%SRC_DIR%\7zip.dll				%DST_DIR%\7zip.dll
%EXE%	%SRC_DIR%\c-ares.dll			%DST_DIR%\c-ares.dll
%EXE%	%SRC_DIR%\curl.dll				%DST_DIR%\curl.dll
%EXE%	%SRC_DIR%\libcrypto.dll			%DST_DIR%\libcrypto.dll
%EXE%	%SRC_DIR%\libssl.dll			%DST_DIR%\libssl.dll
%EXE%	%SRC_DIR%\lz4.dll				%DST_DIR%\lz4.dll
%EXE%	%SRC_DIR%\nnnLib.dll			%DST_DIR%\nnnLib.dll
%EXE%	%SRC_DIR%\nnnSocket.dll			%DST_DIR%\nnnSocket.dll
%EXE%	%SRC_DIR%\nnnSocketServer.dll	%DST_DIR%\nnnSocketServer.dll
%EXE%	%SRC_DIR%\OpenSSL.dll			%DST_DIR%\OpenSSL.dll
%EXE%	%SRC_DIR%\zlib.dll				%DST_DIR%\zlib.dll
%EXE%	%SRC_DIR%\zstd.dll				%DST_DIR%\zstd.dll

%EXE%	Files\Languages\en-US.txt		%DST_DIR%\Files\Languages\en-US.txt
%EXE%	Files\Languages\zh-CN.txt		%DST_DIR%\Files\Languages\zh-CN.txt
%EXE%	Files\Languages\zh-TW.txt		%DST_DIR%\Files\Languages\zh-TW.txt

if not exist %DST_DIR%\conf\ddns_server.txt	%EXE% conf\ddns_server.txt %DST_DIR%\conf\ddns_server.txt
