//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 一些公共数据（包含文件）
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _COMMON__COMMON_INC_H_
#define _COMMON__COMMON_INC_H_

#include "../nnn/Src/common/common.h"

constexpr USHORT	PACKET_HEADER_LEN	= sizeof(BYTE);	// header 大小

//==================== sd->m_user_data? ====================(Start)
// 是否登录成功（0/1）
#define M_SD_DATA__LOGIN_DONE	m_user_data1

/*
	sd		- 连接到 Server 的 sd
	SD_DATA	- 用于 AES 加密的 s_AES_KeyIV*
*/
#define M_SD_DATA__KEY_IV		m_user_data2
//==================== sd->m_user_data? ====================(End)

// 解析 packet 的结果
enum struct es_Parse_Result : BYTE
{
	OK,			// 解析 packet 成功（逻辑正常）
	NEXT,		// 留到下一次循环进行解析（数据未全）
	Unknown,	// 解析 packet 成功（未知 packet）

	Attack,		// 解析 packet 成功（来自客户端的攻击）
	Error,		// 解析 packet 错误 or 代码可能潜在错误
};

enum struct es_Result : BYTE
{
	OK,		// 成功
	Failed,	// 失败
};

#endif	// _COMMON__COMMON_INC_H_
