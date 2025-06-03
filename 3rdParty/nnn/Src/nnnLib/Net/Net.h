//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 网络相关
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___NET___NET_H_
#define _NNNLIB___NET___NET_H_

#include "../../common/common.h"

namespace NNN
{
namespace Net
{

// 是否合法的 Email 地址
NNN_API bool	is_email(const WCHAR *email_address);
NNN_API bool	is_email(const char *email_address);

}	// namespace Net
}	// namespace NNN

#endif	// _NNNLIB___NET___NET_H_
