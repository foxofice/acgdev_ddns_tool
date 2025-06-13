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

	// dynu
	struct
	{
		char	m_API_KEY[255]	= {};
	} DYNU;
};

enum struct es_DomainType
{
	Godaddy,
	dynv6,
	dynu,
};

struct s_Domain
{
	char			m_domain[255]		= {};
	es_DomainType	m_type				= es_DomainType::Godaddy;

	struct
	{
		char	m_input_IP[46]		= {};
		char	m_current_IP[46]	= {};
		bool	m_enabled			= true;	// �Ƿ����� IP ����
		WCHAR	m_err_msg[1024]		= {};
	} IPv4;

	struct
	{
		char	m_input_IP[46]		= {};
		char	m_current_IP[46]	= {};
		bool	m_enabled			= true;	// �Ƿ����� IP ����
		WCHAR	m_err_msg[1024]		= {};
	} IPv6;

	// Godaddy
	struct
	{
		int		m_TTL			= 0;	// <=0 ��ʾʡ��
	} GODADDY;

	// dynv6
	struct
	{
		bool	m_Auto_IPv4		= false;
		bool	m_Auto_IPv6		= false;
	} DYNV6;

	// dynu
	struct
	{
		int		m_ID			= 0;	// ���� ID��ͨ�� dynu API ��ѯ��
		int		m_TTL			= 0;	// <=0 ��ʾʡ��
	} DYNU;

	// ��ȫ��Ϣ
	struct s_Security_Profile	*m_Security_Profile	= nullptr;
};
