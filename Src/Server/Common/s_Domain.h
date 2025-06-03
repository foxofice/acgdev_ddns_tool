#pragma once

struct s_Security_Profile
{
	// Godaddy
	struct
	{
		char	m_Key[255]		= {};
		char	m_Secret[255]	= {};
	} GODADDY;

	// dynv6
	struct
	{
		char	m_token[255]	= {};
	} DYNV6;
};

enum struct es_DomainType
{
	Godaddy,
	dynv6,
};

struct s_Domain
{
	char			m_domain[255]		= {};
	es_DomainType	m_type				= es_DomainType::dynv6;
	char			m_input_IPv4[46]	= {};
	char			m_input_IPv6[46]	= {};
	char			m_current_IPv4[46]	= {};
	char			m_current_IPv6[46]	= {};

	// Godaddy
	struct
	{
		int		m_TTL			= 0;	// <=0 表示省略
	} GODADDY;

	// dynv6
	struct
	{
		bool	m_Auto_IPv4		= false;
		bool	m_Auto_IPv6		= false;
	} DYNV6;

	// 安全信息
	struct s_Security_Profile	*m_Security_Profile	= nullptr;

	// 出错信息
	WCHAR	m_err_msg_IPv4[1024]	= {};
	WCHAR	m_err_msg_IPv6[1024]	= {};

	bool	m_enabled				= true;		// 是否执行更新
};
