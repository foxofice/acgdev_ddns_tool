//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 配置文件 结构体
//--------------------------------------------------------------------------------------

#pragma once

#include <string>

#include "../../../../3rdParty/nnn/Src/nnnLib/nnnLib.h"

namespace DDNS_Server
{
namespace Config
{

struct s_Config
{
	// 构造函数/析构函数
	s_Config();
	~s_Config();

	// 重置成员变量
	void	Reset();

	// 读取配置文件
	HRESULT	Read_Config(const WCHAR conf_filename[MAX_PATH], bool reset = true);

	// 配置文件路径
	WCHAR	m_config_file[MAX_PATH]	= L"conf/ddns_server.txt";

	//==================================================
	// 一般设置
	//==================================================

	// 服务端繁忙的判定时间（ms）
	UINT	m_busy_time	= 300;

	//==================================================
	// Socket 设置
	//==================================================
	struct
	{
		// 服务器端口
		USHORT	m_port				= 0;

		// 多久没有数据就开始 send 心跳包（单位：秒；默认：10）
		int		m_keepalive_idle	= 10;
	} SOCKET;

	//==================================================
	// 安全登录设置
	//==================================================
	struct
	{
		// 登录时验证的用户名/密码
		std::wstring	m_user;
		std::wstring	m_password;

		// 登录超时（秒）
		UINT			m_timeout	= 20;
	} LOGIN;

	//==================================================
	// 日志记录
	//==================================================
	struct
	{
		// 日志文件名（YYYYMMDD）
		char	m_filename[MAX_PATH];

		// 不输出的控制台信息（同时不写入文件）
		UINT	m_console_silent_flag		= 0;

		// 不写入文件的消息
		UINT	m_write_file_silent_flag	= 0;

		// 写入日志文件的周期（ms），0 = 立即写入
		UINT	m_write_file_interval		= 1000;

		// 每条显示信息的时间戳格式
		char	m_timestamp_format[50 + 1];

		// 是否显示客户端连接/断开信息
		bool	m_show_accept_client		= true;
		bool	m_show_disconnect_client	= true;

		// 是否显示登录成功/失败的信息
		bool	m_show_login_result			= true;
	} LOG;
};

}	// namespace Config
}	// namespace DDNS_Server
