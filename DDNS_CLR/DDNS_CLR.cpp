//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../nnn/Src/nnnSocketClient/nnnSocketClient.h"

#include "Packet/DDNS_Server/packet.h"
#include "DDNS_CLR.h"

namespace DDNS_CLR
{

class NNN::Socket::c_Client	*g_client		= nullptr;

// 登录服务端的 user/pwd
std::wstring				*g_login_user	= nullptr;
std::wstring				*g_login_pwd	= nullptr;

// Key/IV
struct s_AES_KeyIV			g_KeyIV;

/*==============================================================
 * Connect 回调函数
 * OnConnecting()
 * OnConnected()
 *==============================================================*/
static void CALLBACK OnConnected(class NNN::Socket::c_Client *client)
{
	struct NNN::Socket::s_SessionData *sd = client->GetSessionData();

	char ip_buffer[46];

	CLR::Event_OnConnected(gcnew String(sd->GetClientIP(ip_buffer)), sd->m_port);
}


/*==============================================================
 * Disconnect 回调函数
 * OnDisconnecting()
 * OnDisconnected()
 *==============================================================*/
static void CALLBACK OnDisconnecting(class NNN::Socket::c_Client* /*client*/)
{
	CLR::Event_OnDisconnecting();
}


/*==============================================================
 * OnReceived 回调函数
 * OnReceived()
 *==============================================================*/
static void CALLBACK OnReceived(class NNN::Socket::c_Client* /*client*/)
{
	Packet::DDNS_Server::parse_packet();
}


/*==============================================================
 * 初始化/清理
 * DoInit()
 * DoFinal()
 *==============================================================*/
bool CLR::DoInit()
{
	HRESULT hr;

	// 内存泄漏检测
	NNN::Misc::MemoryLeakCheck();

	hr = NNN::DoInit();
	if(FAILED(hr))
		return false;

	NNN::Misc::enable_core_dump(NNN::Misc::s_CoreDump_settings(L"ddns.dmp"));

	hr = NNN::Socket::DoInit();
	if(FAILED(hr))
		return false;

	if(g_client == nullptr)
	{
		g_client = new class NNN::Socket::c_Client();

		//g_client->CALLBACKS.m_Connecting_func		= OnConnecting;
		g_client->CALLBACKS.m_Connected_func		= OnConnected;
		g_client->CALLBACKS.m_Disconnecting_func	= OnDisconnecting;
		//g_client->CALLBACKS.m_Disconnected_func	= OnDisconnected;
		g_client->CALLBACKS.m_Received_func			= OnReceived;
	}

	if(g_login_user == nullptr)
		g_login_user = new std::wstring();

	if(g_login_pwd == nullptr)
		g_login_pwd = new std::wstring();

	return true;
}
//--------------------------------------------------
void CLR::DoFinal()
{
	SAFE_DELETE(g_client);
	SAFE_DELETE(g_login_user);
	SAFE_DELETE(g_login_pwd);

	// 清理
	NNN::Socket::DoFinal();
	NNN::DoFinal();
}


/*==============================================================
 * 连接到 Server
 * Connect()
 *==============================================================*/
bool CLR::Connect(String ^ip, USHORT port, String ^user, String ^pwd)
{
	g_login_user->resize((size_t)user->Length);
	g_login_pwd->resize((size_t)pwd->Length);

	if(!g_login_user->empty())
		NNN::CLR::String_to_wchar(user, &(*g_login_user)[0]);

	if(!g_login_pwd->empty())
		NNN::CLR::String_to_wchar(pwd, &(*g_login_pwd)[0]);

	struct NNN::Socket::c_Client::s_ConnectParam connect_param;
	NNN::CLR::String_to_char(ip, connect_param.m_host);
	connect_param.m_port = port;

	if(g_client->GetConnectionState() != NNN::Socket::c_Client::es_ConnectState::Not_Connected)
		return false;

	HRESULT hr = g_client->Connect(connect_param);
	if(FAILED(hr))
		return false;

	return true;
}


/*==============================================================
 * 断开跟 Server 的连接
 * DisConnect()
 *==============================================================*/
void CLR::DisConnect()
{
	g_client->DisConnect();
}


/*==============================================================
 * 是否已连接到 Server
 * is_connected()
 *==============================================================*/
bool CLR::is_connected()
{
	return g_client->is_connected();
}


/*==============================================================
 * Client 发送 Ping
 * send_Ping()
 *==============================================================*/
void CLR::send_Ping()
{
	Packet::DDNS_Server::send_Ping();
}


/*==============================================================
 * Client 发送「登录数据」
 * send_Login_Data()
 *==============================================================*/
void CLR::send_Login_Data()
{
	Packet::DDNS_Server::send_Login_Data(g_KeyIV.m_Key, g_KeyIV.m_IV);
}


/*==============================================================
 * Client 发送「更新域名的 A/AAAA 记录」
 * send_Update_Domains()
 *==============================================================*/
void CLR::send_Update_Domains(String ^Key, String ^Secret, String ^ip, List<ddns_lib::c_Record^> ^records)
{
	char Key_[100];
	char Secret_[100];
	char ip_[46];

	NNN::CLR::String_to_char(Key, Key_);
	NNN::CLR::String_to_char(Secret, Secret_);
	NNN::CLR::String_to_char(ip, ip_);

	std::vector<struct ddns_server_CLR::s_Record> out_records;
	out_records.resize((size_t)records->Count);

	for(int i=0; i<records->Count; ++i)
	{
		struct ddns_server_CLR::s_Record	&record		= out_records[i];
		ddns_lib::c_Record					^gc_record	= records[i];

		NNN::CLR::String_to_char(gc_record->m_name,		record.m_name);
		NNN::CLR::String_to_char(gc_record->m_domain,	record.m_domain);
		record.m_TTL		= gc_record->m_TTL;
		record.m_user_idx	= gc_record->m_user_idx;
	}	// for

	Packet::DDNS_Server::send_Update_Domains(Key_, Secret_, ip_, out_records);
}

}	// namespace DDNS_CLR
