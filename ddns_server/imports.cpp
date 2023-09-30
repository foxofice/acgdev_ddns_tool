//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 导入文件
//--------------------------------------------------------------------------------------

#include "../nnn/Src/common/common-macro.h"

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
#pragma comment(lib, "Common.lib")
#pragma comment(lib, "nnnLib.lib")
#pragma comment(lib, "nnnSocket.lib")
#pragma comment(lib, "nnnSocketServer.lib")

#pragma comment(lib, "ddns_server_CLR.lib")
#endif	// NNN_PLATFORM_WIN32
