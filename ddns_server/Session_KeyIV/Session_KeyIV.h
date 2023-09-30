//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 管理 session 的 KeyIV
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _DDNS_SERVER___SESSION_KEYIV___SESSION_KEYIV_H_
#define _DDNS_SERVER___SESSION_KEYIV___SESSION_KEYIV_H_

#include "../../common/s_AES_KeyIV.h"

namespace DDNS_Server
{
namespace Session_KeyIV
{

// 初始化/清理
HRESULT				DoInit();
HRESULT				DoFinal();

// 生成新的 s_AES_KeyIV，并跟 session 关联（如果 session_id 对应的 s_AES_KeyIV 已存在，则返回 nullptr）
struct s_AES_KeyIV*	add_KeyIV(UINT64 session_id);

// 移除 s_AES_KeyIV
void				remove_KeyIV(UINT64 session_id);

}	// namespace Session_KeyIV
}	// namespace DDNS_Server

#endif	// _DDNS_SERVER___SESSION_KEYIV___SESSION_KEYIV_H_
