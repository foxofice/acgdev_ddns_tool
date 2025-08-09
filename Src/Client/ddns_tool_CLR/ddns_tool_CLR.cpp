//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../../3rdParty/nnn/Src/nnnSocketClient/nnnSocketClient.h"

#include "../../Server/CLR.h"
#include "Packet/DDNS_Server/packet.h"
#include "ddns_tool_CLR.h"

namespace ddns_tool_CLR
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

	hr = NNN::DoInit_nnnLib();
	if(FAILED(hr))
		return false;

	NNN::Misc::CoreDump::enable_core_dump(struct NNN::Misc::CoreDump::s_CoreDump_settings(L"ddns_tool.dmp"));

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
	NNN::DoFinal_nnnLib();
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
		NNN::CLR::TO_CPP::String_to_wchar(user, &(*g_login_user)[0]);

	if(!g_login_pwd->empty())
		NNN::CLR::TO_CPP::String_to_wchar(pwd, &(*g_login_pwd)[0]);

	struct NNN::Socket::c_Client::s_ConnectParam connect_param;
	NNN::CLR::TO_CPP::String_to_char(ip, connect_param.m_host);
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
void CLR::send_Update_Domains(	List<ddns_lib::c_Domain^>	^gc_domains,
								bool						DNS_Lookup_First,
								List<System::String^>		^gc_DNS_Server_List,
								int							timeout )
{
	if(gc_domains->Count == 0)
		return;

	//========== 构造 profiles ==========(Start)
	// c_Security_Profile* -> Profile_idx
	Dictionary<ddns_lib::c_Security_Profile^, size_t>	^profiles_to_idx	= gcnew Dictionary<ddns_lib::c_Security_Profile^, size_t>(gc_domains->Count);

	List<ddns_lib::c_Security_Profile^>					^gc_profiles		= gcnew List<ddns_lib::c_Security_Profile^>(gc_domains->Count);

	for(int i=0; i<gc_domains->Count; ++i)
	{
		ddns_lib::c_Domain ^domain = gc_domains[i];

		if(!profiles_to_idx->ContainsKey(domain->m_Security_Profile))
		{
			profiles_to_idx->Add(domain->m_Security_Profile, (size_t)gc_profiles->Count);
			gc_profiles->Add(domain->m_Security_Profile);
		}
	}	// for

	std::vector<s_Security_Profile> profiles;
	profiles.resize((size_t)gc_profiles->Count);

	for(int i=0; i<gc_profiles->Count; ++i)
		TO_CPP::make_Security_Profile(gc_profiles[i], profiles[i]);
	//========== 构造 profiles ==========(End)

	//========== 构造 domains ==========(Start)
	std::vector<struct s_Domain> domains;
	domains.resize((size_t)gc_domains->Count);

	for(int i=0; i<gc_domains->Count; ++i)
	{
		ddns_lib::c_Domain ^domain = gc_domains[i];

		size_t Profile_idx = profiles_to_idx[domain->m_Security_Profile];

		TO_CPP::make_Domain(gc_domains[i], &profiles[Profile_idx], domains[i]);
	}	// for
	//========== 构造 domains ==========(End)

	//========== 构造 DNS_Server_List ==========(Start)
	std::vector<std::string> DNS_Server_List;

	if(gc_DNS_Server_List != nullptr)
	{
		DNS_Server_List.resize((size_t)gc_DNS_Server_List->Count);

		for(int i=0; i<gc_DNS_Server_List->Count; ++i)
		{
			char DNS_Server[1024];
			NNN::CLR::TO_CPP::String_to_char(gc_DNS_Server_List[i], DNS_Server);

			DNS_Server_List[i] = DNS_Server;
		}	// for
	}
	//========== 构造 DNS_Server_List ==========(End)

	Packet::DDNS_Server::send_Update_Domains(domains, DNS_Lookup_First, DNS_Server_List.empty() ? nullptr : &DNS_Server_List, timeout);
}

}	// namespace ddns_tool_CLR
