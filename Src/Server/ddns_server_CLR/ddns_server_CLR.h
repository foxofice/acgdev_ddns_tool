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

// ��ʼ��
NNN_API void	DoInit();

// ���� IP ��¼
NNN_API void	update_domains(	__inout std::vector<struct s_Domain>	&domains,
								bool									DNS_Lookup_First,
								__in_opt const std::vector<std::string>	*DNS_Server_List	= nullptr,	// �Զ���DNS���������б�Ԫ��Ϊ "" ʱ����ʾϵͳĬ�� DNS��
								int										timeout				= 15 * 1000 );

}	// namespace ddns_server_CLR
