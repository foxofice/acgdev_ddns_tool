//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 客户端数据
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKET___SOCKET_SYSTEM___C_STATUS_H_
#define _NNNSOCKET___SOCKET_SYSTEM___C_STATUS_H_

#include <atomic>

#include "../../common/common.h"

namespace NNN
{
namespace Socket
{

class c_Status
{
public:
	struct s_Speed
	{
		std::atomic<UINT>	m_last_speed		= 0;	// 实时速度（上一秒）
		std::atomic<UINT>	m_speed				= 0;	// 实时速度
		std::atomic<UINT64>	m_last_update_tick	= 0;	// 上一次更新 m_last_speed 的时刻
		std::atomic<UINT64>	m_total_len			= 0;	// 总大小
	};

	// 重置数据
	NNN_API inline void		Reset()
	{
		ZeroMemory(&m_SendSpeed, sizeof(m_SendSpeed));
		ZeroMemory(&m_RecvSpeed, sizeof(m_RecvSpeed));
	}

	// 设置获取是否允许统计速度/数据大小
	NNN_API inline void		SetEnabled(bool enabled)	{ m_enabled = enabled; }
	NNN_API inline bool		GetEnabled()				{ return m_enabled; }

	// 更新速度、总大小
	NNN_API void			UpdateSendStatus(UINT add_size);
	NNN_API void			UpdateRecvStatus(UINT add_size);

	// 获取实时速度、总大小
	NNN_API inline UINT		GetSendSpeed()		{ return m_SendSpeed.m_last_speed; }
	NNN_API inline UINT		GetRecvSpeed()		{ return m_RecvSpeed.m_last_speed; }
	NNN_API inline UINT64	GetSendTotalLen()	{ return m_SendSpeed.m_total_len; }
	NNN_API inline UINT64	GetRecvTotalLen()	{ return m_RecvSpeed.m_total_len; }

protected:
	bool			m_enabled	= false;	// 是否开启速度统计

	struct s_Speed	m_SendSpeed;
	struct s_Speed	m_RecvSpeed;
};

}	// namespace Socket
}	// namespace NNN

#endif	// _NNNSOCKET___SOCKET_SYSTEM___C_STATUS_H_
