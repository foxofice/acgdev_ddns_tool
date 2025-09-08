//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

//#include "../../Common/Common.h"

#include "../ddns_server.h"
#include "../Log/Log.h"
#include "../Config/Config.h"
#include "../Packet/packet.h"
#include "Socket.h"

namespace DDNS_Server
{
namespace Socket
{

// Server 实例
class NNN::Socket::c_Server	*g_server	= nullptr;

//==================== 客户端验证 ====================(Start)
// 正在登录的 Session
struct
{
	NNN_HASH_SET<UINT64>				*m_sessions	= nullptr;	// session_id[]
	class NNN::Thread::c_Atomic_Lock	m_lock;
} LOGINING_SESSIONS;
//==================== 客户端验证 ====================(End)

#pragma region g_server 的回调函数
/*==============================================================
 * 建立新客户端连接回调函数
 * OnAccept()
 *==============================================================*/
static void CALLBACK OnAccept(struct NNN::Socket::s_SessionData *sd)
{
	if(Config::g_config->LOG.m_show_accept_client)
	{
		char ip_buffer[46];

		Log::ShowMessage(	"Accept : " NNN_CL_VALUE "%s:%u" NNN_ANSI_RESET "\n",
							sd->GetClientIP(ip_buffer), sd->m_port );
	}

	sd->M_SD_DATA__LOGIN_DONE = 0;

	// 加入到「客户端列表」
	{
		NNN::Thread::c_Lock l(LOGINING_SESSIONS.m_lock);
		LOGINING_SESSIONS.m_sessions->insert(sd->m_session_id);
	}

	Packet::send_KeyIV(sd);
}


/*==============================================================
 * 断开客户端连接回调
 * OnDisconnecting()
 * OnDisconnected()
 *==============================================================*/
static void CALLBACK OnDisconnecting(struct NNN::Socket::s_SessionData *sd)
{
	if(Config::g_config->LOG.m_show_disconnect_client)
	{
		char ip_buffer[46];

		Log::ShowMessage(	"Disconnect : " NNN_CL_VALUE "%s:%u" NNN_ANSI_RESET "\n",
							sd->GetClientIP(ip_buffer), sd->m_port );
	}

	// 从「客户端列表」删除
	{
		NNN::Thread::c_Lock l(LOGINING_SESSIONS.m_lock);
		LOGINING_SESSIONS.m_sessions->erase(sd->m_session_id);
	}

	struct s_AES_KeyIV *key_iv = (struct s_AES_KeyIV*)sd->M_SD_DATA__KEY_IV.load();
	Release_KeyIV(key_iv);

	sd->Reset_UserData();
}
//--------------------------------------------------
//static void CALLBACK OnDisconnected(struct NNN::Socket::s_SessionData * /*sd*/)
//{
//}


/*==============================================================
 * 接收信息完成回调
 * OnReceived()
 *==============================================================*/
static void CALLBACK OnReceived(struct NNN::Socket::s_SessionData *sd)
{
	Packet::parse_packet(sd);
}


/*==============================================================
 * KeepAlive 回调
 * OnKeepAlive()
 *==============================================================*/
static void CALLBACK OnKeepAlive(struct NNN::Socket::s_SessionData *sd)
{
	Packet::send_KeepAlive(sd);
}
#pragma endregion


/*==============================================================
 * 初始化/清理
 * DoInit()
 * DoFinal()
 *==============================================================*/
HRESULT DoInit()
{
	HRESULT hr;

	V( NNN::Socket::DoInit() );
	if(FAILED(hr))
	{
		Log::ShowFatalError("Init '" NNN_CL_VALUE "Socket" NNN_ANSI_RESET "' failed! [%s : %s() - line %d]\n",
							__FILE__, __FUNCTION__, __LINE__);
		return hr;
	}

	if(g_server == nullptr)
	{
		g_server = new class NNN::Socket::c_Server();

		g_server->SetKeepAlive(true, Config::g_config->SOCKET.m_keepalive_idle);

		g_server->CALLBACKS.m_Accept_func			= OnAccept;
		g_server->CALLBACKS.m_Disconnecting_func	= OnDisconnecting;
		//g_server->CALLBACKS.m_Disconnected_func	= OnDisconnected;
		g_server->CALLBACKS.m_Received_func			= OnReceived;
		g_server->CALLBACKS.m_KeepAlive_func		= OnKeepAlive;
	}

	LOGINING_SESSIONS.m_sessions	= new NNN_HASH_SET<UINT64>();

	Log::ShowStatus("Init '" NNN_CL_VALUE "Socket" NNN_ANSI_RESET "' OK.\n");
	return S_OK;
}
//--------------------------------------------------
HRESULT DoFinal()
{
	SAFE_DELETE(LOGINING_SESSIONS.m_sessions);

	SAFE_DELETE(g_server);

	HRESULT hr;
	V( NNN::Socket::DoFinal() );

	return S_OK;
}


/*==============================================================
 * 打开/关闭服务器
 * Start()
 * Stop()
 *==============================================================*/
HRESULT Start()
{
	HRESULT hr;

	// 打开服务器
	struct NNN::Socket::c_Server::s_OpenServerParam param
	{
		.m_port = Config::g_config->SOCKET.m_port,
	};

	V( g_server->OpenServer(param) );

	if(FAILED(hr))
	{
		Log::ShowError(	"Open server failed!! (port = " NNN_CL_VALUE "%u" NNN_ANSI_RESET ") [%s : %s() - line %d]\n",
						param.m_port,
						__FILE__, __FUNCTION__, __LINE__ );
		return hr;
	}

	Log::ShowStatus("Server is running. (port = " NNN_CL_VALUE "%u" NNN_ANSI_RESET ")\n",
					param.m_port);

	g_running_state = es_State::Running;

	return S_OK;
}
//--------------------------------------------------
HRESULT Stop()
{
	HRESULT hr;

	Log::ShowStatus("Server is closing...\n");

	// 关闭服务器
	V( g_server->CloseServer() );

	g_running_state = es_State::Stopped;
	Log::ShowStatus("Server is closed.\n");

	return S_OK;
}


/*==============================================================
 * 执行周期性工作
 * DoWork()
 *==============================================================*/
void DoWork()
{
	// 踢出超时的 Logining Session
	time_t now = NNN::Time::get_current_time();

	std::vector<UINT64> kick_list;
	kick_list.reserve(LOGINING_SESSIONS.m_sessions->size());

	NNN::Thread::c_Lock l(LOGINING_SESSIONS.m_lock);

	for(UINT64 session_id : *LOGINING_SESSIONS.m_sessions)
	{
		NNN::Socket::s_SessionData *sd = g_server->GetSessionData(session_id);
		if(sd == nullptr)
		{
			kick_list.push_back(session_id);
			continue;
		}

		if(sd->m_connect_time + Config::g_config->LOGIN.m_timeout < now)
			kick_list.push_back(session_id);
	}	// for

	for(UINT64 session_id : kick_list)
	{
		LOGINING_SESSIONS.m_sessions->erase(session_id);
		g_server->DisconnectSession(session_id);
	}	// for
}


/*==============================================================
 * 从 LOGINING_SESSIONS.m_sessions 中移除 session_id
 * remove_logining_sessions()
 *==============================================================*/
void remove_logining_sessions(UINT64 session_id)
{
	NNN::Thread::c_Lock l(LOGINING_SESSIONS.m_lock);

	LOGINING_SESSIONS.m_sessions->erase(session_id);
}

}	// namespace Socket
}	// namespace DDNS_Server
