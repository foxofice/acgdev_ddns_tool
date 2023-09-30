//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Web 相关
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___WEB___WEB_H_
#define _NNNLIB___WEB___WEB_H_

#include "../../common/common.h"

namespace NNN
{
namespace Web
{

// 获取指定 url 的 html（check_cert = true 时，表示检查服务器证书）
NNN_API bool	get_html(__out std::wstring &out_html, const char *url, bool check_cert = true);

}	// namespace Web
}	// namespace NNN

#endif	// _NNNLIB___WEB___WEB_H_
