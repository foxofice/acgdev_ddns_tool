//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 一些公共数据
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _COMMON__COMMON_H_
#define _COMMON__COMMON_H_

#include "../nnn/Src/nnnSocket/nnnSocket.h"

#include "Common-inc.h"
#include "s_AES_KeyIV.h"

// 读取 packet_data（固定长度）
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
	// 检查数据长度
	if(recv_buffer.get_data_len() < packet_len)
		return es_Parse_Result::NEXT;

	// 读取数据
	size_t read_len = 0;
	recv_buffer.read_data(packet_data, packet_len, read_len);
	br.m_offset = PACKET_HEADER_LEN * 2;

	return es_Parse_Result::OK;
}

// 读取 packet_len 和所有 packet_data（packet_len 为 USHORT）
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

	// 读取 header
	if(recv_buffer.get_data_len() < k_ALL_HEADER_LEN)
		return es_Parse_Result::NEXT;

	size_t read_len = 0;
	recv_buffer.read_data(packet_data, k_ALL_HEADER_LEN, read_len, false);

	br.m_offset = PACKET_HEADER_LEN * 2;
	packet_len = br.read<USHORT>();

	// 检查数据长度
	if(packet_len <= k_ALL_HEADER_LEN)
		return es_Parse_Result::Attack;

	if(recv_buffer.get_data_len() < packet_len)
		return es_Parse_Result::NEXT;

	// 读取数据
	recv_buffer.read_data(packet_data, packet_len, read_len);

	return es_Parse_Result::OK;
}

// 检查解析 packet 的 result（Server 版本）
#define CHECK_PACKET_RES_SERVER(server)				\
		if(res == es_Parse_Result::OK)				\
			continue;								\
		else if(res == es_Parse_Result::NEXT)		\
			break;									\
		else if(res == es_Parse_Result::Unknown)	\
		{											\
			/* 丢弃所有数据 */						\
			sd->RECV_DATA.m_buffer.clear();			\
			break;									\
		}											\
		else if(res == es_Parse_Result::Attack)		\
		{											\
			/* 断开连接 */							\
			(server)->DisconnectSession(sd);		\
			break;									\
		}											\
		else if(res == es_Parse_Result::Error)		\
		{											\
			/* 丢弃所有数据 */						\
			sd->RECV_DATA.m_buffer.clear();			\
			break;									\
		}

// 检查解析 packet 的 result（Client 版本）
#define CHECK_PACKET_RES_CLIENT(client)				\
		if(res == es_Parse_Result::OK)				\
			continue;								\
		else if(res == es_Parse_Result::NEXT)		\
			break;									\
		else if(res == es_Parse_Result::Unknown)	\
		{											\
			/* 丢弃所有数据 */						\
			sd->RECV_DATA.m_buffer.clear();			\
			break;									\
		}											\
		else if(res == es_Parse_Result::Attack)		\
		{											\
			/* 断开连接 */							\
			(client)->DisConnect();					\
			break;									\
		}											\
		else if(res == es_Parse_Result::Error)		\
		{											\
			/* 丢弃所有数据 */						\
			sd->RECV_DATA.m_buffer.clear();			\
			break;									\
		}

#endif	// _COMMON__COMMON_H_
