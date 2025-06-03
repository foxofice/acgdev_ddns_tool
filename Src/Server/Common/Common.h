//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : һЩ��������
//--------------------------------------------------------------------------------------

#pragma once

#include "../../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"

#include "Common-inc.h"
#include "s_AES_KeyIV.h"
#include "s_Domain.h"

// ��ȡ packet_data���̶����ȣ�
#define NNN_PACKET_READ_DATA_FIXED(recv_buffer, packet_len)				\
	{																	\
		es_Parse_Result res_ = packet_read_data_fixed(	recv_buffer,	\
														packet_data,	\
														packet_len,		\
														br);			\
		if(res_ != es_Parse_Result::OK)									\
			return res_;												\
	}
inline es_Parse_Result	packet_read_data_fixed(	struct NNN::Buffer::s_RingBuffer	&recv_buffer,
												__inout BYTE						*packet_data,
												USHORT								packet_len,
												struct NNN::Buffer::s_BinaryReader	&br )
{
	// ������ݳ���
	if(recv_buffer.get_data_len() < packet_len)
		return es_Parse_Result::NEXT;

	// ��ȡ����
	recv_buffer.read_data(packet_data, packet_len);
	br.m_offset = PACKET_HEADER_LEN * 2;

	return es_Parse_Result::OK;
}

// ��ȡ packet_len ������ packet_data��packet_len Ϊ USHORT��
#define NNN_PACKET_READ_DATA(recv_buffer)																\
	{																									\
		static_assert(sizeof(packet_len) == sizeof(USHORT), "sizeof(packet_len) != sizeof(USHORT)");	\
																										\
		es_Parse_Result res = packet_read_data(	recv_buffer,											\
												packet_data,											\
												packet_len,												\
												br );													\
		if(res != es_Parse_Result::OK)																	\
			return res;																					\
	}
inline es_Parse_Result	packet_read_data(	struct NNN::Buffer::s_RingBuffer	&recv_buffer,
											__inout BYTE						*packet_data,
											__inout USHORT						&packet_len,
											struct NNN::Buffer::s_BinaryReader	&br )
{
	packet_len = 0;
	const size_t k_ALL_HEADER_LEN = PACKET_HEADER_LEN * 2 + sizeof(packet_len);

	// ��ȡ header
	if(recv_buffer.get_data_len() < k_ALL_HEADER_LEN)
		return es_Parse_Result::NEXT;

	recv_buffer.read_data(packet_data, k_ALL_HEADER_LEN, false);

	br.m_offset = PACKET_HEADER_LEN * 2;
	packet_len = br.read<USHORT>();

	// ������ݳ���
	if(packet_len <= k_ALL_HEADER_LEN)
		return es_Parse_Result::Attack;

	if(recv_buffer.get_data_len() < packet_len)
		return es_Parse_Result::NEXT;

	// ��ȡ����
	recv_buffer.read_data(packet_data, packet_len);

	return es_Parse_Result::OK;
}

// ������ packet �� result��Server �汾��
#define CHECK_PACKET_RES_SERVER(server)				\
		if(res == es_Parse_Result::OK)				\
			continue;								\
		else if(res == es_Parse_Result::NEXT)		\
			break;									\
		else if(res == es_Parse_Result::Unknown)	\
		{											\
			/* ������������ */						\
			sd->RECV_DATA.m_buffer.clear();			\
			break;									\
		}											\
		else if(res == es_Parse_Result::Attack)		\
		{											\
			/* �Ͽ����� */							\
			(server)->DisconnectSession(sd);		\
			break;									\
		}											\
		else if(res == es_Parse_Result::Error)		\
		{											\
			/* ������������ */						\
			sd->RECV_DATA.m_buffer.clear();			\
			break;									\
		}

// ������ packet �� result��Client �汾��
#define CHECK_PACKET_RES_CLIENT(client)				\
		if(res == es_Parse_Result::OK)				\
			continue;								\
		else if(res == es_Parse_Result::NEXT)		\
			break;									\
		else if(res == es_Parse_Result::Unknown)	\
		{											\
			/* ������������ */						\
			sd->RECV_DATA.m_buffer.clear();			\
			break;									\
		}											\
		else if(res == es_Parse_Result::Attack)		\
		{											\
			/* �Ͽ����� */							\
			(client)->DisConnect();					\
			break;									\
		}											\
		else if(res == es_Parse_Result::Error)		\
		{											\
			/* ������������ */						\
			sd->RECV_DATA.m_buffer.clear();			\
			break;									\
		}
