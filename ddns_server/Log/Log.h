//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 日志记录
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _DDNS_SERVER___LOG___LOG_H_
#define _DDNS_SERVER___LOG___LOG_H_

#include "Log-macro.h"
#include "Log-inc.h"

namespace DDNS_Server
{
namespace Log
{

// 初始化/清理
HRESULT	DoInit();
HRESULT	DoFinal();

// 执行周期性工作
void	DoWork();

// 显示信息
void	ShowMessage(const char *, ...);		// 一般信息
void	ShowStatus(const char *, ...);		// 良好的东西
void	ShowSQL(const char *, ...);			// 输出 SQL 相关的东西
void	ShowInfo(const char *, ...);		// 变量信息
void	ShowNotice(const char *, ...);		// 轻于警告
void	ShowWarning(const char *, ...);		// 警告
void	ShowDebug(const char *, ...);		// 调试
void	ShowError(const char *, ...);		// 常规错误
void	ShowFatalError(const char *, ...);	// 致命错误

}	// namespace Log
}	// namespace DDNS_Server

#endif	// _DDNS_SERVER___LOG___LOG_H_
