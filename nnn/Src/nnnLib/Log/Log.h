//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 日志
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___LOG___LOG_H_
#define _NNNLIB___LOG___LOG_H_

#include "../../common/common.h"

namespace NNN
{
// 是否打印/写入错误日志
extern bool	g_log_error_nnnLib;

// 设置/获取是否打印/写入错误日志
NNN_API	void	SetLogError_nnnLib(bool log_err);
NNN_API	bool	GetLogError_nnnLib();
}	// namespace NNN

namespace NNN
{
namespace Log
{

// 写入日志
NNN_API HRESULT	AppendLog(	const WCHAR *filename, const WCHAR *txt,
							const WCHAR *prefix = nullptr, bool with_datetime = true );
NNN_API HRESULT	AppendLog(	const char *filename, const char *txt,
							const char *prefix = nullptr, bool with_datetime = true );

}	// namespace Log
}	// namespace NNN

#endif	// _NNNLIB___LOG___LOG_H_
