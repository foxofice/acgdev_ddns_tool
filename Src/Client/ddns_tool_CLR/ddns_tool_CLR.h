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

// ��¼����˵� user/pwd
extern std::wstring					*g_login_user;
extern std::wstring					*g_login_pwd;

// Key/IV
extern struct s_AES_KeyIV			g_KeyIV;

public ref class CLR
{
public:
	// ��ʼ��/����
	static bool		DoInit();
	static void		DoFinal();

	// ���ӵ� Server
	static bool		Connect(String ^ip, USHORT port, String ^user, String ^pwd);

	// �Ͽ��� Server ������
	static void		DisConnect();

	// �Ƿ������ӵ� Server
	static bool		is_connected();

#pragma region Send
	// Client ���� Ping
	// ��μ���ddns_server::Packet::recv_Ping()
	static void		send_Ping();

	// Client ���͡���¼���ݡ�
	// ��μ���ddns_server::Packet::recv_Login_Data()
	static void		send_Login_Data();

	// Client ���͡����������� A/AAAA ��¼��
	// ��μ���ddns_server::Packet::recv_Update_Domains()
	static void		send_Update_Domains(List<ddns_lib::c_Domain^>	^gc_domains,
										bool						DNS_Lookup_First,
										List<System::String^>		^gc_DNS_Server_List,
										int							timeout);
#pragma endregion

#pragma region �¼�
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

	// add_log �¼�
	ADD_CLR_EVENT_STATIC(	On_add_log,
							NNN_PARAMS(String ^txt, Color c),
							txt, c );
};

}	// namespace ddns_tool_CLR
