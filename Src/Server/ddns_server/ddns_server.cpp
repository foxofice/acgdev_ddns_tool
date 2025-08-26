//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

//#define NNN_USE_CRT_DEBUG
#include "../../../3rdParty/nnn/Src/nnnSocketServer/nnnSocketServer.h"

#include "../Common/Common.h"

#include "Config/Config.h"
#include "Log/Log.h"
#include "Socket/Socket.h"
#include "Packet/packet.h"
#include "ddns_server.h"

static void cleanup();

namespace DDNS_Server
{

std::atomic<es_State>								g_running_state		= es_State::Stopped;	// ����������״̬

// �����
struct NNN::Buffer::s_Obj_Pool<struct s_AES_KeyIV>	g_KeyIV_pool;

static std::atomic<bool>							g_s_exec_clean_up	= false;	// �Ѿ�ִ���� clean_up
static std::atomic<bool>							g_s_exit_loop_done	= false;	// �˳�ѭ�����
static std::atomic<bool>							g_s_clean_up_done	= false;	// clean_up ���

/*==============================================================
 * ��ʾ LOGO
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
 * SendLog �ص�����
 * On_SendLog()
 *==============================================================*/
static void CALLBACK On_SendLog(struct NNN::Socket::s_SessionData *sd, const WCHAR *log, DWORD rgb)
{
	Packet::send_Log(sd, log, rgb);
}


/*==============================================================
 * ��ʼ��/����
 * DoInit()
 * DoFinal()
 *==============================================================*/
HRESULT DoInit()
{
	HRESULT hr;

	V_RETURN( Log::DoInit() );

	// ��ʼ�� Config
	V_RETURN( Config::DoInit() );

	display_logo();

	V_RETURN( Config::g_config->Read_Config(Config::g_config->m_config_file) );

	// ��ʼ�� Socket
	V_RETURN( Socket::DoInit() );

	// ��ʼ�� ddns_server_CLR
	ddns_server_CLR::DoInit(On_SendLog, Config::g_config->LANGUAGES.m_culture.c_str());

	return S_OK;
}
//--------------------------------------------------
HRESULT DoFinal()
{
	HRESULT hr;

	// ��������
	V( Socket::DoFinal() );
	V( Log::DoFinal() );
	V( Config::DoFinal() );

	return S_OK;
}


/*==============================================================
 * ���з�����
 * run_server()
 *==============================================================*/
void run_server()
{
	HRESULT hr;

	// �򿪷�����
	V( Socket::Start() );
	if(FAILED(hr))
	{
		g_running_state = es_State::Stopped;
		return;
	}

	// ִ����ѭ��
	while(g_running_state != es_State::Exiting)
	{
		UINT64 tick = NNN::Time::tick64();

		//========== ִ�������Թ��� ==========(Start)
		if(g_running_state == es_State::Running)
			Socket::DoWork();

		Log::DoWork();

		bool is_busy = (NNN::Time::tick64() - tick >= Config::g_config->m_busy_time);
		if(!is_busy)
			NNN::Thread::Sleep(1);
		//========== ִ�������Թ��� ==========(End)
	}	// while

	// �رշ�����
	V( Socket::Stop() );
}

/*==============================================================
 * �����µ� s_AES_KeyIV*
 * Create_KeyIV()
 *==============================================================*/
struct s_AES_KeyIV* Create_KeyIV()
{
	return g_KeyIV_pool.create_object();
}


/*==============================================================
 * ���� s_AES_KeyIV*
 * Release_KeyIV()
 *==============================================================*/
void Release_KeyIV(struct s_AES_KeyIV *KeyIV)
{
	if(KeyIV != nullptr)
	{
		KeyIV->recalc_KeyIV();
		g_KeyIV_pool.release_object(KeyIV);
	}
}

}	// namespace DDNS_Server

/*==============================================================
 * ������
 * cleanup()
 *==============================================================*/
void cleanup()
{
	bool val = false;
	if(!DDNS_Server::g_s_exec_clean_up.compare_exchange_strong(val, true))
		return;	// ��ִֻ֤��һ��

	DDNS_Server::g_running_state = DDNS_Server::es_State::Exiting;

	DDNS_Server::g_s_exit_loop_done.wait(false, std::memory_order_acquire);	// false ��ȴ�

	// ����
	DDNS_Server::DoFinal();
	NNN::DoFinal_nnnLib();

	DDNS_Server::g_s_clean_up_done.store(true, std::memory_order_release);
	DDNS_Server::g_s_clean_up_done.notify_one();

	const char log[] = "clean_up done\n";
	printf(log);
	OutputDebugStringA(log);
}


/*==============================================================
 * ������ڵ�
 * main()
 *==============================================================*/
int main()
{
	NNN::Leak_Detect::MemoryLeakCheck();
	NNN::Misc::CoreDump::enable_core_dump(NNN::Misc::CoreDump::s_CoreDump_settings(L"ddns_server.dmp"));
	NNN::DoInit_nnnLib();

	NNN::Misc::atexit(cleanup);

	// ��ʼ��
	if(FAILED(DDNS_Server::DoInit()))
		return EXIT_FAILURE;

	// ���� Server
	DDNS_Server::run_server();

	DDNS_Server::g_s_exit_loop_done.store(true, std::memory_order_release);
	DDNS_Server::g_s_exit_loop_done.notify_one();

	if(DDNS_Server::g_s_exec_clean_up)
		DDNS_Server::g_s_clean_up_done.wait(false, std::memory_order_acquire);	// false ��ȴ�
	else
		cleanup();

	const char log[] = "exit loop done\n";
	printf(log);
	OutputDebugStringA(log);

	return 0;
}
