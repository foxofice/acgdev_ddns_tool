//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : ddns_server ִ�е� .Net ����
//--------------------------------------------------------------------------------------
#pragma once

#include "../../../3rdParty/nnn/Src/common/common-macro.h"
#include "../../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"
#include "../Common/Common.h"

namespace ddns_server_CLR
{

typedef void	(CALLBACK *LPCALLBACK_SEND_LOG)(struct NNN::Socket::s_SessionData *sd, const WCHAR *log, DWORD color);

// ��ʼ��
NNN_API void	DoInit(LPCALLBACK_SEND_LOG send_log_func, const WCHAR *culture = L"");

// ���� IP ��¼
NNN_API void	update_domains(	__inout std::vector<struct s_Domain>	&domains,
								bool									DNS_Lookup_First,
								__in_opt const std::vector<std::string>	*DNS_Server_List	= nullptr,		// �Զ���DNS���������б�Ԫ��Ϊ "" ʱ����ʾϵͳĬ�� DNS��
								int										timeout				= 15 * 1000,
								struct NNN::Socket::s_SessionData		*sd_SendLog			= nullptr );	// ���� Log �� sd��Server �ã�

}	// namespace ddns_server_CLR
