//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ddns_server 执行的 .Net 代码
//--------------------------------------------------------------------------------------
#pragma once

#include "../nnn/Src/common/common-macro.h"

namespace ddns_server_CLR
{

struct s_Record
{
	char	m_name[50]		= {};
	char	m_domain[100]	= {};
	int		m_TTL			= 0;
	int		m_user_idx		= 0;		// 索引（用户自定义数据）

	char	m_result_ip[46]		= {};	// 更新成功后的 IP
	char	m_err_msg[0xff + 1]	= {};	// 错误信息
};

// 初始化
NNN_API void	DoInit();

// 执行更新 IP 记录
NNN_API void	update_records_step3_update_by_server(	const char								*ip,
														const char								*Key,
														const char								*Secret,
														__inout std::vector<struct s_Record>	&records );

}	// namespace ddns_server_CLR
