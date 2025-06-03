//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : һЩ�������ݣ������ļ���
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../3rdParty/nnn/Src/common/common.h"

constexpr USHORT	PACKET_HEADER_LEN	= sizeof(BYTE);	// header ��С

//==================== sd->m_user_data? ====================(Start)
// �Ƿ��¼�ɹ���0/1��
#define M_SD_DATA__LOGIN_DONE	m_user_data1

/*
	sd		- ���ӵ� Server �� sd
	SD_DATA	- ���� AES ���ܵ� s_AES_KeyIV*
*/
#define M_SD_DATA__KEY_IV		m_user_data2
//==================== sd->m_user_data? ====================(End)

// ���� packet �Ľ��
enum struct es_Parse_Result : BYTE
{
	OK,			// ���� packet �ɹ����߼�������
	NEXT,		// ������һ��ѭ�����н���������δȫ��
	Unknown,	// ���� packet �ɹ���δ֪ packet��

	Attack,		// ���� packet �ɹ������Կͻ��˵Ĺ�����
	Error,		// ���� packet ���� or �������Ǳ�ڴ���
};

enum struct es_Result : BYTE
{
	OK,		// �ɹ�
	Failed,	// ʧ��
};
