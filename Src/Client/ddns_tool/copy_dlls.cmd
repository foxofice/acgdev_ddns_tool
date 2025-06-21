::%1 - $(Configuration)		(Debug »ò Release)
::%2 - $(BaseOutputPath)	£¨..\..\..\bin\x64\£©
::%3 - $(TargetFramework)	£¨net9.0-windows£©

set EXE=..\..\..\diff_copy.exe
set SRC_DIR=..\..\..\3rdParty\nnn\Libs\x64\%1
set DST_DIR=%2\%1\%3

%EXE%	%SRC_DIR%\7zip.dll				%DST_DIR%\7zip.dll
%EXE%	%SRC_DIR%\c-ares.dll			%DST_DIR%\c-ares.dll
%EXE%	%SRC_DIR%\curl.dll				%DST_DIR%\curl.dll
%EXE%	%SRC_DIR%\libcrypto.dll			%DST_DIR%\libcrypto.dll
%EXE%	%SRC_DIR%\libssl.dll			%DST_DIR%\libssl.dll
%EXE%	%SRC_DIR%\lz4.dll				%DST_DIR%\lz4.dll
%EXE%	%SRC_DIR%\nnnLib.dll			%DST_DIR%\nnnLib.dll
%EXE%	%SRC_DIR%\nnnSocket.dll			%DST_DIR%\nnnSocket.dll
%EXE%	%SRC_DIR%\nnnSocketClient.dll	%DST_DIR%\nnnSocketClient.dll
%EXE%	%SRC_DIR%\OpenSSL.dll			%DST_DIR%\OpenSSL.dll
%EXE%	%SRC_DIR%\TinyXML2.dll			%DST_DIR%\TinyXML2.dll
%EXE%	%SRC_DIR%\zlib.dll				%DST_DIR%\zlib.dll
%EXE%	%SRC_DIR%\zstd.dll				%DST_DIR%\zstd.dll

%EXE%	%2\%1\Common.dll				%DST_DIR%\Common.dll
%EXE%	%2\%1\ddns_server__Client.dll	%DST_DIR%\ddns_server__Client.dll
