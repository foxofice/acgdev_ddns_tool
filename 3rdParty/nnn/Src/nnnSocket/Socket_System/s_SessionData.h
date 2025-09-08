//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 会话数据
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKET___SOCKET_SYSTEM___S_SESSIONDATA_H_
#define _NNNSOCKET___SOCKET_SYSTEM___S_SESSIONDATA_H_

#include <time.h>
#include <atomic>

#include "../../nnnLib/nnnLib.h"

#include "nnnSocket-macro.h"
#include "nnnSocket-inc.h"
#include "nnnSocket-gcc.h"
#include "s_OVERLAPPED.h"
#include "c_Status.h"

namespace NNN
{
namespace Socket
{

struct s_SessionData
{
	DISALLOW_COPY_AND_ASSIGN(s_SessionData);

	// 构造函数/析构函数
	NNN_API					s_SessionData();
	NNN_API					~s_SessionData();

	// 取得本机的 MAC 地址
	NNN_API void			InitClientMAC();

	// 初始化连接时间
	NNN_API void			InitConnectTime();

	// 初始化状态统计
	NNN_API void			Init_Statistical_Status(size_t max_threads_count);

	// 重设 m_session_id
	NNN_API void			set_session_id();

	// 设置 Socket 发送/接收缓冲区的大小（使用 s_SessionData 前必须先设置）
	NNN_API void			SetBufferSize(	USHORT sending_buffer_size		= 4096,
											USHORT receiving_buffer_size	= 4096 );

	// 重置状态
	NNN_API void			Reset(bool reset_user_data = true);
	// 重置 user_data
	NNN_API void			Reset_UserData();

	// 获取服务端/客户端 IP 字符串
	NNN_API char*			GetServerIP(__out char buffer[46]);
	NNN_API char*			GetClientIP(__out char buffer[46]);

	// （线程安全）发送数据（如果返回值不成功，表示 socket 已关闭）
	/*
		openssl_handshaking	- true 时，表示仅发送握手数据（不发送用户数据）
	*/
	NNN_API HRESULT			send(	const BYTE	*data,
									size_t		data_len,
									bool		use_openssl,
									bool		openssl_handshaking	= false );

	//// 读取数据
	////		buffer = nullptr 时，表示不把数据复制到缓冲区
	////		remove_from_read_buffer = true 时，表示从接收缓冲区中移除读取的数据
	////		remove_from_read_buffer = false 时，表示不移除
	//NNN_API void			read(	BYTE	*buffer,
	//								size_t	read_len,
	//								bool	remove_from_read_buffer = true );

	// 更新最后活动时间
	NNN_API inline void		UpdateLastActiveTime()	{ m_last_active_time = time(nullptr); }

	// 更新发送/接收的速度、总大小
	NNN_API inline void		UpdateSendStatus(UINT add_size, size_t thread_idx)	{ return m_status.UpdateSendStatus(add_size, thread_idx); }
	NNN_API inline void		UpdateRecvStatus(UINT add_size, size_t thread_idx)	{ return m_status.UpdateRecvStatus(add_size, thread_idx); }

	// 获取当前发送/接收速度、总大小
	NNN_API inline UINT		GetSendSpeed()		{ return m_status.GetSendSpeed(); }
	NNN_API inline UINT		GetRecvSpeed()		{ return m_status.GetRecvSpeed(); }
	NNN_API inline UINT64	GetSendTotalLen()	{ return m_status.GetSendTotalLen(); }
	NNN_API inline UINT64	GetRecvTotalLen()	{ return m_status.GetRecvTotalLen(); }

	// 设置获取是否允许统计速度/数据大小
	NNN_API inline void		SetEnabledCalcStatus(bool enabled)	{ m_status.SetEnabled(enabled); }
	NNN_API inline bool		GetEnabledCalcStatus()				{ return m_status.GetEnabled(); }

	// 设置/获取是否调试封包
	NNN_API inline void		Set_DebugPacket(bool enabled)	{ DebugPacket.m_enabled = enabled; }
	NNN_API inline bool		Get_DebugPacket()				{ return DebugPacket.m_enabled; }

	// 设置保存封包调试数据的文件名
	NNN_API inline void		Set_DebugPacket_filename(const WCHAR *filename)	{ DebugPacket.m_filename = filename; }

	// 写入封包的调试数据
	NNN_API void			WriteDebugSendPacket(const BYTE *data, size_t data_len);
	NNN_API void			WriteDebugRecvPacket(const BYTE *data, size_t data_len);

	//======================================================================

	// 初始化/清理 OpenSSL 相关的东西
	NNN_API HRESULT			OpenSSL_Init(void *ctx);	// SSL_CTX *ctx
	NNN_API HRESULT			OpenSSL_Release();

	/*
		(for read)	底层socket ----> BIO_write ----> SSL_read
		(for write)	底层socket <---- BIO_read <---- SSL_write
	*/
	// 写入数据到 SEND_DATA.m_buffer
	NNN_API HRESULT			OpenSSL_write(_In_opt_ const BYTE *data, _In_opt_ size_t data_len);
	// 读取数据到 RECV_DATA.m_buffer
	NNN_API HRESULT			OpenSSL_read(const BYTE *data, size_t data_len);

	// 此会话是否正在使用 OpenSSL
	NNN_API inline bool		is_use_OpenSSL()	{ return (OpenSSL.m_ssl != nullptr); }

	//======================================================================

	SOCKET								m_socket;

	// 此会话是否在 c_Server 对象中（true = 在 c_Server 中，否则表示在 c_Client 中）
	bool								m_session_in_server	= true;

	in_addr								m_server_addr_ipv4;						// 服务端 IP（IPv4）
	in_addr								m_client_addr_ipv4;						// 客户端 IP（IPv4）

	in6_addr							m_server_addr_ipv6;						// 服务端 IP（IPv6）
	in6_addr							m_client_addr_ipv6;						// 客户端 IP（IPv6）

	int									m_af;									// AF_UNSPEC/AF_INET/AF_INET6
	USHORT								m_port;									// 客户端端口
	USHORT								m_server_port;

	BYTE								m_MAC[ETHER_ADDR_LEN];					// MAC 地址

	struct
	{
		// 缓冲区（待发送 + 正在发送）
		struct Buffer::s_RingBuffer		m_buffer;

		// 缓冲区（Socket 正在发送）
		BYTE							*m_sending_buffer		= nullptr;
		USHORT							m_sending_buffer_size	= 0;

		// Socket 是否正在发送数据（是否已投递 send）
		std::atomic<bool>				m_socket_is_sending		= false;
	} SEND_DATA;

	// 同一个 session 必定只有一个线程操作 RECV_DATA 的数据
	struct
	{
		// 缓冲区（待用户处理）
		struct Buffer::s_RingBuffer		m_buffer;

		// 缓冲区（Socket 正在接收）
		BYTE							*m_receiving_buffer			= nullptr;
		USHORT							m_receiving_buffer_size		= 0;

#if NNN_SOCKET_SUPPORT_EPOLL
		std::atomic<bool>				m_socket_is_receiving		= false;
		std::atomic<int>				m_unhandled_EPOLLIN_count	= 0;	// 未处理的 EPOLLIN 事件数量
#endif	// NNN_SOCKET_SUPPORT_EPOLL
	} RECV_DATA;

	// 服务端相关数据
	struct
	{
		// Session 是否正在从服务端断开连接中
		std::atomic<bool>				m_disconnecting						= false;
		// 是否在 Server 中标记为不使用状态
		std::atomic<bool>				m_no_used							= true;
		// 是否已经执行了 Disconnecting 回调
		std::atomic<bool>				m_have_exec_disconnecting_callback	= false;
		// 是否已经执行了 Disconnected 回调
		std::atomic<bool>				m_have_exec_disconnected_callback	= false;
	} SERVER_DATA;

	bool								m_is_iocp_epoll				= true;

#if NNN_SOCKET_SUPPORT_IOCP
	struct
	{
		struct s_OVERLAPPED	m_Overlapped_Send;			// 重叠结构（发送）
		struct s_OVERLAPPED	m_Overlapped_Receive;		// 重叠结构（接收）
		struct s_OVERLAPPED	m_Overlapped_AcceptIPv4;	// 重叠结构（Accept IPv4）
		struct s_OVERLAPPED	m_Overlapped_AcceptIPv6;	// 重叠结构（Accept IPv6）
	} IOCP;
#elif NNN_SOCKET_SUPPORT_EPOLL
	struct
	{
		int					m_epoll_fd	= 0;			// epoll_create 的 fd
	} EPOLL;
#endif	// NNN_SOCKET_SUPPORT_IOCP

	time_t	m_connect_time		= 0;	// 连接时间
	time_t	m_last_active_time	= 0;	// 最后活动时间
	UINT64	m_session_id		= 0;	// 唯一会话 ID（由 server 或 client 设置）

	struct
	{
		bool								m_enabled	= false;
		std::wstring						m_filename;
		struct Thread::s_CriticalSection	m_cs;	// 确保写入调试封包时线程安全
	} DebugPacket;

	struct
	{
		// OpenSSL 对象（SSL*）
		void								*m_ssl			= nullptr;

		// 是否已完成 SSL 握手
		std::atomic<bool>					m_ssl_connected	= false;

		// OpenSSL 使用的内存 BIO（BIO*）
		void								*m_bio_send		= nullptr;
		void								*m_bio_recv		= nullptr;

		struct Buffer::s_RingBuffer			m_receive_buffer;	// 接收缓冲区（转换前的内容）

		struct Thread::s_CriticalSection	m_cs_write;			// 确保 OpenSSL_write() 线程安全
		struct Thread::s_CriticalSection	m_cs_read;			// 确保 OpenSSL_read() 线程安全
	} OpenSSL;

	// 用户自定义数据
	std::atomic<UINT64>	m_user_data1	= 0;
	std::atomic<UINT64>	m_user_data2	= 0;
	std::atomic<UINT64>	m_user_data3	= 0;
	std::atomic<UINT64>	m_user_data4	= 0;

	// 发送/接收速度、总大小
	class c_Status		m_status;

	// EOF 标记（当之前的所有数据发送完毕后，自动断开连接）
	std::atomic<bool>	m_EOF			= false;
};

}	// namespace Socket
}	// namespace NNN

#endif	// _NNNSOCKET___SOCKET_SYSTEM___S_SESSIONDATA_H_
