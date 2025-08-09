//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../../3rdParty/nnn/Src/nnnSocketServer/nnnSocketServer.h"

#include "../Common/Common.h"

#include "Config/Config.h"
#include "Session_KeyIV/Session_KeyIV.h"
#include "Log/Log.h"
#include "Socket/Socket.h"
#include "Packet/packet.h"
#include "ddns_server.h"

namespace DDNS_Server
{

std::atomic<es_State>	g_running_state	= es_State::Stopped;	// 服务器运行状态

/*==============================================================
 * 显示 LOGO
 * display_logo()
 *==============================================================*/
static void display_logo()
{
	/*
+---------------------------------------------------------------------+
|                           AcgDev presents                           |
|    _           ___            ___  ___  _  _ ___   _____         _  |
|   /_\  __ __ _|   \ _____ __ |   \|   \| \| / __| |_   _|__  ___| | |
|  / _ \/ _/ _` | |) / -_) V / | |) | |) | .` \__ \   | |/ _ \/ _ \ | |
| /_/ \_\__\__, |___/\___|\_/  |___/|___/|_|\_|___/   |_|\___/\___/_| |
|          |___/                                                      |
|                           www.AcgDev.com                            |
+---------------------------------------------------------------------+
	*/

	Log::ShowMessage(R"(+---------------------------------------------------------------------+)" "\n");
	Log::ShowMessage(R"(|                           AcgDev presents                           |)" "\n");
	Log::ShowMessage(R"(|    _           ___            ___  ___  _  _ ___   _____         _  |)" "\n");
	Log::ShowMessage(R"(|   /_\  __ __ _|   \ _____ __ |   \|   \| \| / __| |_   _|__  ___| | |)" "\n");
	Log::ShowMessage(R"(|  / _ \/ _/ _` | |) / -_) V / | |) | |) | .` \__ \   | |/ _ \/ _ \ | |)" "\n");
	Log::ShowMessage(R"(| /_/ \_\__\__, |___/\___|\_/  |___/|___/|_|\_|___/   |_|\___/\___/_| |)" "\n");
	Log::ShowMessage(R"(|          |___/                                                      |)" "\n");
	Log::ShowMessage(R"(|                           www.AcgDev.com                            |)" "\n");
	Log::ShowMessage(R"(+---------------------------------------------------------------------+)" "\n");
}


/*==============================================================
 * SendLog 回调函数
 * On_SendLog()
 *==============================================================*/
static void CALLBACK On_SendLog(struct NNN::Socket::s_SessionData *sd, const WCHAR *log, DWORD rgb)
{
	Packet::send_Log(sd, log, rgb);
}


/*==============================================================
 * 初始化/清理
 * DoInit()
 * DoFinal()
 *==============================================================*/
HRESULT DoInit()
{
	HRESULT hr;

	V_RETURN( Log::DoInit() );

	// 初始化 Config
	V_RETURN( Config::DoInit() );

	display_logo();

	// 初始化
	V_RETURN( Session_KeyIV::DoInit() );

	V_RETURN( Config::g_config->Read_Config(Config::g_config->m_config_file) );

	// 初始化 Socket
	V_RETURN( Socket::DoInit() );

	// 初始化 ddns_server_CLR
	ddns_server_CLR::DoInit(On_SendLog, Config::g_config->LANGUAGES.m_culture.c_str());

	return S_OK;
}
//--------------------------------------------------
HRESULT DoFinal()
{
	HRESULT hr;

	// 各种清理
	V( Socket::DoFinal() );
	V( Session_KeyIV::DoFinal() );
	V( Log::DoFinal() );
	V( Config::DoFinal() );

	return S_OK;
}


/*==============================================================
 * 运行服务器
 * run_server()
 *==============================================================*/
void run_server()
{
	HRESULT hr;

	// 打开服务器
	V( Socket::Start() );
	if(FAILED(hr))
	{
		g_running_state = es_State::Stopped;
		return;
	}

	// 执行主循环
	while(g_running_state != es_State::Exiting)
	{
		UINT64 tick = NNN::Time::tick64();

		//========== 执行周期性工作 ==========(Start)
		if(g_running_state == es_State::Running)
			Socket::DoWork();

		Log::DoWork();

		bool is_busy = (NNN::Time::tick64() - tick >= Config::g_config->m_busy_time);
		if(!is_busy)
			NNN::Thread::Sleep(1);
		//========== 执行周期性工作 ==========(End)
	}

	// 关闭服务器
	V( Socket::Stop() );
}

}	// namespace DDNS_Server

/*==============================================================
 * 清理函数
 * cleanup()
 *==============================================================*/
void cleanup()
{
	// 清理
	DDNS_Server::DoFinal();
	NNN::DoFinal_nnnLib();
}


/*==============================================================
 * 程序入口点
 * main()
 *==============================================================*/
int main()
{
#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)
	// 内存泄漏检测
	NNN::Misc::MemoryLeakCheck();
#endif	// NNN_PLATFORM_WIN32

	NNN::DoInit_nnnLib();
	NNN::Misc::CoreDump::enable_core_dump(NNN::Misc::CoreDump::s_CoreDump_settings(L"ddns_server.dmp"));

	atexit(cleanup);

	// 初始化
	if(FAILED(DDNS_Server::DoInit()))
		return EXIT_FAILURE;

	// 运行 Server
	DDNS_Server::run_server();

	return 0;
}
