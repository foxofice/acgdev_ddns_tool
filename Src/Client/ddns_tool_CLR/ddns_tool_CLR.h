//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#pragma once

#include <string>

#include "../../../3rdParty/nnn/Src/nnnSocketClient/nnnSocketClient.h"
#include "../../../3rdParty/nnn/Src/nnnCLR/nnnCLR.h"
#include "../../Server/Common/s_AES_KeyIV.h"

using namespace System;
using namespace System::Drawing;
using namespace System::Collections::Generic;

namespace ddns_tool_CLR
{

extern class NNN::Socket::c_Client	*g_client;

// 登录服务端的 user/pwd
extern std::wstring					*g_login_user;
extern std::wstring					*g_login_pwd;

// Key/IV
extern struct s_AES_KeyIV			g_KeyIV;

public ref class CLR
{
public:
	// 初始化/清理
	static bool		DoInit();
	static void		DoFinal();

	// 连接到 Server
	static bool		Connect(String ^ip, USHORT port, String ^user, String ^pwd);

	// 断开跟 Server 的连接
	static void		DisConnect();

	// 是否已连接到 Server
	static bool		is_connected();

#pragma region Send
	// Client 发送 Ping
	// 请参见：ddns_server::Packet::recv_Ping()
	static void		send_Ping();

	// Client 发送「登录数据」
	// 请参见：ddns_server::Packet::recv_Login_Data()
	static void		send_Login_Data();

	// Client 发送「更新域名的 A/AAAA 记录」
	// 请参见：ddns_server::Packet::recv_Update_Domains()
	static void		send_Update_Domains(List<ddns_lib::c_Domain^>	^gc_domains,
										bool						DNS_Lookup_First,
										List<System::String^>		^gc_DNS_Server_List,
										int							timeout);
#pragma endregion

#pragma region 事件
	ADD_CLR_EVENT_STATIC(	OnConnected,
							NNN_PARAMS(String ^ip, USHORT port),
							ip, port );

	ADD_CLR_EVENT_STATIC(	OnDisconnecting,
							NNN_PARAMS() );

	ADD_CLR_EVENT_STATIC(	Recv_Ping,
							NNN_PARAMS(double ping),
							ping );

	ADD_CLR_EVENT_STATIC(	Recv_LoginResult,
							NNN_PARAMS(bool result),
							result );

	ADD_CLR_EVENT_STATIC(	Recv_Update_Domains_Result,
							NNN_PARAMS(List<ddns_lib::c_Domain^> ^domains),
							domains );
#pragma endregion

	// add_log 事件
	ADD_CLR_EVENT_STATIC(	On_add_log,
							NNN_PARAMS(String ^txt, Color c),
							txt, c );
};

}	// namespace ddns_tool_CLR
