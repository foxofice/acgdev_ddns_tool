//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ���磨�꣩
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"
#include "../../common/Common-inc.h"

namespace DDNS_Server
{
namespace Socket
{

typedef void			(CALLBACK *LPCALLBACK_ACCEPT)(struct NNN::Socket::s_SessionData *sd);							// Accept �ص�����
typedef void			(CALLBACK *LPCALLBACK_DISCONNECTING)(struct NNN::Socket::s_SessionData *sd);					// Disconnecting �ص�����
typedef void			(CALLBACK *LPCALLBACK_DISCONNECTED)(struct NNN::Socket::s_SessionData *sd);						// Disconnected �ص�����
typedef void			(CALLBACK *LPCALLBACK_LOGIN_DONE)(struct NNN::Socket::s_SessionData *sd);						// LoginDone �ص�����
typedef es_Parse_Result	(CALLBACK *LPCALLBACK_RECEIVED)(struct NNN::Socket::s_SessionData *sd, USHORT header_decode);	// Received �ص�����
typedef void			(CALLBACK *LPCALLBACK_DOWORK)();																// DoWork �ص�����

}	// namespace Socket
}	// namespace DDNS_Server
