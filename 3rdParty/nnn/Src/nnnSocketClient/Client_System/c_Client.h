//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 客户端类
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKETCLIENT___CLIENT_SYSTEM___C_CLIENT_H_
#define _NNNSOCKETCLIENT___CLIENT_SYSTEM___C_CLIENT_H_

#include <atomic>

#include "../../nnnSocket/Socket_System/Socket_System.h"
#include "../../nnnSocket/Socket_System/s_SessionData.h"

namespace NNN
{
namespace Socket
{

class c_Client
{
public:
	typedef void	(CALLBACK *LPCALLBACK_CONNECTING)(class c_Client *client);				// 与服务端建立连接回调（刚建立连接，未完成各种初始化工作）
	typedef void	(CALLBACK *LPCALLBACK_CONNECTED)(class c_Client *client);				// 与服务端建立连接回调（建立连接，并完成各种初始化工作后）
	typedef void	(CALLBACK *LPCALLBACK_DISCONNECTING)(class c_Client *client);			// 与服务端断开连接回调（刚断开连接，但未完成各种清理工作）
	typedef void	(CALLBACK *LPCALLBACK_DISCONNECTED)(class c_Client *client);			// 与服务端断开连接回调（断开连接，并完成各种清理工作后）
	typedef void	(CALLBACK *LPCALLBACK_RECEIVED)(class c_Client *client);				// 接收信息完成回调

	typedef void	(CALLBACK *LPCALLBACK_OPENSSL_HANDSHAKE_DONE)(class c_Client *client);	// OpenSSL 握手完成回调

	enum struct es_ConnectState : BYTE
	{
		Not_Connected,
		Connecting,
		Connected,
		Disconnecting,
	};

	struct s_ConnectParam
	{
		// "" 表示本机（IPv4 环回地址：127.0.0.1、IPv6 环回地址：::1）
		char		m_host[100 + 1];

		// 要连接的端口
		USHORT		m_port;

		// 如果 < 0 时，表示使用默认 timeout（timeout 单位为毫秒）
		int			m_timeout						= -1;

		// 是否使用 IOCP/Epoll（Win32 下使用 IOCP、Linux/MAC 下使用 EPOLL；false 为 select 模型）
		// 如果使用 IOCP/Epoll，则无需调用 DoWork()
		bool		m_is_iocp_epoll					= true;

		// IOCP/EPOLL 的工作者线程数（0 表示「CPU 线程数」，-1 表示「CPU 线程数 * 2 + 2」）
		int			m_threads_count					= 2;

		// 是否重置 m_sd 的 user_data
		bool		m_reset_user_data				= true;

		// 发送/接收缓冲区的大小（仅给 Socket 内部使用，不影响应用层累积数据的缓冲区）
		USHORT		m_sending_buffer_size			= 8192;
		USHORT		m_receiving_buffer_size			= 8192;

		// 是否计算收发速度/大小等状态
		bool		m_calc_speed_status				= true;

		struct
		{
			bool	m_use_openssl					= false;	// 是否使用 OpenSSL

			char	m_ca_cert_filename[MAX_PATH]	= {};	// CA 证书的文件名
		} OpenSSL;
	};

	DISALLOW_COPY_AND_ASSIGN(c_Client);

	// 构造函数/析构函数
	NNN_API							c_Client();
	NNN_API							~c_Client();

	// 连接到服务端
	NNN_API HRESULT					Connect(const struct s_ConnectParam &connect_param);

	// 断开连接
	NNN_API HRESULT					DisConnect();

	// 是否已连接
	NNN_API inline bool				is_connected()			{ return (m_connection_state == es_ConnectState::Connected); }

	NNN_API inline es_ConnectState	GetConnectionState()	{ return m_connection_state; }

	// 是否使用 IOCP/Epoll
	NNN_API inline bool				is_iocp_epoll()			{ return m_is_iocp_epoll; }

	// 设置为不延迟（禁用 Nagle 算法）
	NNN_API inline void				set_no_delay(bool no_delay)
	{
		m_no_delay = no_delay;
		Socket::set_no_delay(m_sd.m_socket, no_delay);
	}
	NNN_API inline bool				get_no_delay()			{ return m_no_delay; }

	// 设置/获取系统缓冲区大小（返回 -1 时，表示出错）
	NNN_API inline void				set_send_buffer_size(int buffer_size)
	{
		m_system_send_buffer_size = buffer_size;
		Socket::set_system_send_buffer_size(m_sd.m_socket, buffer_size);
	}
	NNN_API inline int				get_send_buffer_size()	{ return m_system_send_buffer_size; }
	NNN_API inline void				set_recv_buffer_size(int buffer_size)
	{
		m_system_recv_buffer_size = buffer_size;
		Socket::set_system_recv_buffer_size(m_sd.m_socket, buffer_size);
	}
	NNN_API inline int				get_recv_buffer_size()	{ return m_system_recv_buffer_size; }

	// （异步）发送数据（如果返回值不成功，表示 socket 已关闭）
	/*
		openssl_handshaking	- true 时，表示仅发送握手数据（不发送用户数据）
	*/
	NNN_API HRESULT					Send(	const BYTE	*data,
											size_t		data_len,
											bool		openssl_handshaking	= false );

	// 发送「结束」标记（当之前的所有数据发送完毕后，自动断开连接）
	NNN_API HRESULT					Send_EOF();

	//// 读取数据
	////		buffer = nullptr 时，表示不把数据复制到缓冲区
	////		remove_from_read_buffer = true 时，表示从接收缓冲区中移除读取的数据
	////		remove_from_read_buffer = false 时，表示不移除
	//NNN_API void		Read(BYTE *buffer, size_t read_len, bool remove_from_read_buffer = true);

	// 执行周期性工作（失败表示已断开连接，仅 select 模型有效）
	NNN_API HRESULT					DoWork(long timeout_sec = 0, long timeout_usec = 0);

	// 获取 m_data
	NNN_API inline struct s_SessionData*	GetSessionData()	{ return &m_sd; }

	// 获取最后一次连接时的 OpenSSL 证书文件路径
	NNN_API inline char*					GetLastCert()		{ return OpenSSL.m_ca_cert_filename; }

protected:
	// 工作者线程
	static void	WorkerThread(void *param);
#if NNN_SOCKET_SUPPORT_IOCP
	void		WorkerThread_IOCP();
#elif NNN_SOCKET_SUPPORT_EPOLL
	void		WorkerThread_Epoll();
#endif	// NNN_SOCKET_SUPPORT_IOCP

	// 初始化/清理 OpenSSL 上下文
	HRESULT		OpenSSL_Init_CTX();
	HRESULT		OpenSSL_Release_CTX();

	// 将「SSL 套接字」和「传输层的套接字」绑定起来
	HRESULT		OpenSSL_connect();

	// 设置 CA 证书
	HRESULT		OpenSSL_set_ca_cert_pem(const char *ca_cert_filename);

protected:
	// 会话连接
	struct s_SessionData					m_sd;

	// 是否重置 m_sd 的 user_data
	bool									m_reset_user_data	= true;

	// 是否为不延迟（禁用 Nagle 算法）
	/*
		如果是 true，则表示关闭 Nagle 算法。
		在交互式场景下（例如需要快速响应的网游）建议设置成 true，否则会造成延迟；
		如果是不需要实时响应的场景（例如棋牌类游戏），建设设置成 false
	*/
	bool									m_no_delay			= false;

	// 默认系统缓冲区大小（也就是 SO_SNDBUF/SO_RCVBUF）
	int										m_default_system_send_buffer_size;
	int										m_default_system_recv_buffer_size;
	// 系统缓冲区大小
	int										m_system_send_buffer_size;
	int										m_system_recv_buffer_size;

	// 连接状态
	std::atomic<es_ConnectState>			m_connection_state	= es_ConnectState::Not_Connected;

	// 是否使用 IOCP/Epoll
	bool									m_is_iocp_epoll	= false;

	// 工作者线程（IOCP/EPOLL 使用）
	std::vector<struct Thread::s_Thread*>	m_threads;

#if NNN_SOCKET_SUPPORT_IOCP
	struct
	{
		HANDLE	m_CompletionPort;	// 完成端口对象
	} IOCP;
#endif	// NNN_SOCKET_SUPPORT_IOCP

#if NNN_SOCKET_SUPPORT_EPOLL
	struct
	{
		int		m_epoll_fd;			// epoll_create 的 fd
	} EPOLL;
#endif	// NNN_SOCKET_SUPPORT_EPOLL

	struct
	{
		bool	m_use_openssl	= false;				// 是否使用 OpenSSL
		void	*m_client_ctx	= nullptr;				// OpenSSL 上下文（SSL_CTX* 类型）

		void	*m_ca_cert		= nullptr;				// 证书（X509* 类型）

		// 以下用于自动重连
		char	m_ca_cert_filename[MAX_PATH]	= {};	// 保存证书文件路径
	} OpenSSL;

public:
	struct
	{
		LPCALLBACK_CONNECTING				m_Connecting_func				= nullptr;	// Connecting 事件
		LPCALLBACK_CONNECTED				m_Connected_func				= nullptr;	// Connected 事件
		LPCALLBACK_DISCONNECTING			m_Disconnecting_func			= nullptr;	// Disconnecting 事件
		LPCALLBACK_DISCONNECTED				m_Disconnected_func				= nullptr;	// Disconnected 事件
		LPCALLBACK_RECEIVED					m_Received_func					= nullptr;	// Received 事件
		LPCALLBACK_OPENSSL_HANDSHAKE_DONE	m_openssl_handshake_done_func	= nullptr;	// openssl_handshake_done 事件
	} CALLBACKS;

protected:
};

}	// namespace Socket
}	// namespace NNN

#endif	// _NNNSOCKETCLIENT___CLIENT_SYSTEM___C_CLIENT_H_
