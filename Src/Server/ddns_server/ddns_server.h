//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ddns_server
//--------------------------------------------------------------------------------------

#pragma once

#include "ddns_server-inc.h"

#include "../Common/s_AES_KeyIV.h"

namespace DDNS_Server
{

extern std::atomic<es_State>	g_running_state;	// ����������״̬

// �����µ� s_AES_KeyIV*
struct s_AES_KeyIV*	Create_KeyIV();
// ���� s_AES_KeyIV*
void				Release_KeyIV(struct s_AES_KeyIV *KeyIV);

}	// namespace DDNS_Server
