//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : �����ļ�
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"
#include "../common/Common-inc.h"

namespace DDNS_Server
{

// ����������״̬
enum struct es_State : BYTE
{
	Stopped,	// ֹͣ
	Running,	// ��������
	Exiting,	// ���ڹرճ���
};

}	// namespace DDNS_Server
