//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 网络（宏）
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"
#include "../../common/Common-inc.h"

namespace DDNS_Server
{
namespace Socket
{

typedef void			(CALLBACK *LPCALLBACK_ACCEPT)(struct NNN::Socket::s_SessionData *sd);							// Accept 回调函数
typedef void			(CALLBACK *LPCALLBACK_DISCONNECTING)(struct NNN::Socket::s_SessionData *sd);					// Disconnecting 回调函数
typedef void			(CALLBACK *LPCALLBACK_DISCONNECTED)(struct NNN::Socket::s_SessionData *sd);						// Disconnected 回调函数
typedef void			(CALLBACK *LPCALLBACK_LOGIN_DONE)(struct NNN::Socket::s_SessionData *sd);						// LoginDone 回调函数
typedef es_Parse_Result	(CALLBACK *LPCALLBACK_RECEIVED)(struct NNN::Socket::s_SessionData *sd, USHORT header_decode);	// Received 回调函数
typedef void			(CALLBACK *LPCALLBACK_DOWORK)();																// DoWork 回调函数

}	// namespace Socket
}	// namespace DDNS_Server
