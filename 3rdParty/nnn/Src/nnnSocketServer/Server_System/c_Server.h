//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : 服务端类
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKETSERVER___SERVER_SYSTEM___C_SERVER_H_
#define _NNNSOCKETSERVER___SERVER_SYSTEM___C_SERVER_H_

#include <stack>
#include <map>

#ifndef _WIN32
#include <pthread.h>
#endif	// !_WIN32

#include "../../nnnLib/nnnLib.h"
#include "../../nnnSocket/Socket_System/s_SessionData.h"

#define NNN_SOCKET_SERVER_LOG	"nnnSocketServer_log.txt"

namespace NNN
{
namespace Socket
{

class c_Server
{
public:
	// 服务器运行状态
	enum struct es_ServerState : BYTE
	{
		Stopped,	// 已停止
		Stopping,	// 正在停止
		Running,	// 正在运行
	};

	typedef void	(CALLBACK *LPCALLBACK_ACCEPT)(struct s_SessionData *sd);					// 建立新会话连接
	typedef void	(CALLBACK *LPCALLBACK_DISCONNECTING)(struct s_SessionData *sd);				// 正在断开会话（未清理各种资源）
	typedef void	(CALLBACK *LPCALLBACK_DISCONNECTED)(struct s_SessionData *sd);				// 断开会话（已清理各种资源）
	typedef void	(CALLBACK *LPCALLBACK_RECEIVED)(struct s_SessionData *sd);					// 接收信息完成
	typedef void	(CALLBACK *LPCALLBACK_KEEPALIVE)(struct s_SessionData *sd);					// KeepAlive

	typedef void	(CALLBACK *LPCALLBACK_OPENSSL_HANDSHAKE_DONE)(struct s_SessionData *sd);	// OpenSSL 握手完成回调

	DISALLOW_COPY_AND_ASSIGN(c_Server);

	// 构造函数/析构函数
	NNN_API							c_Server(size_t reserve_size = 4096);
	NNN_API							~c_Server();

	// 设置保活参数（Win 似乎无法使用系统的心跳包，需要 SetUseTcpKeepalive(false)）
	NNN_API inline void				SetKeepAlive(bool keep_alive = true, int idle = 10, int intv = 2, int cnt = 3)
	{
		KEEP_ALIVE.m_enabled			= keep_alive;
		KEEP_ALIVE.m_keepalivetime		= idle;
		KEEP_ALIVE.m_keepaliveinterval	= intv;
		KEEP_ALIVE.m_count				= cnt;
	}

	struct s_OpenServerParam
	{
		USHORT		m_port;				// 要监听的端口

		// 是否使用 IOCP/Epoll（Win32 下使用 IOCP、Linux/MAC 下使用 EPOLL；false 为 select 模型）
		// 如果使用 IOCP/Epoll，则无需调用 DoWork()
		bool		m_is_iocp_epoll				= true;

		// IOCP/EPOLL 的工作者线程数（0 表示「CPU 线程数」，-1 表示「CPU 线程数 * 2 + 2」）
		int			m_threads_count				= -1;

		// 发送/接收缓冲区的大小（仅给 Socket 内部使用，不影响应用层累积数据的缓冲区）
		USHORT		m_sending_buffer_size		= 4096;
		USHORT		m_receiving_buffer_size		= 4096;

		bool		m_calc_speed_status			= true;	// 是否计算收发速度/大小等状态（服务端）
		bool		m_calc_session_speed_status	= true;	// 是否计算收发速度/大小等状态（s_SessionData）

		struct
		{
			bool	m_use_openssl	= false;	// 是否使用 OpenSSL

			BYTE	*m_pkey_pem		= nullptr;	// 密钥文件的内存数据
			int		m_pkey_len		= 0;		// m_pkey_pem 的长度

			BYTE	*m_ca_cert_pem	= nullptr;	// 证书文件的内存数据
			int		m_ca_cert_len	= 0;		// m_ca_cert_pem 的长度
		} OpenSSL;
	};

	// 开启服务端
	/*
		注意：	SetKeepAlive()、SetUseSystemKeepalive()、GetUseSystemKeepalive()、SetCustomKeepalivePacket()，
				必须在 OpenServer() 之前设置，否则可能会无效或出现其他问题
	*/
	NNN_API HRESULT					OpenServer(const struct s_OpenServerParam &open_server_param);

	// 关闭服务端
	NNN_API HRESULT					CloseServer();

	// 获取本地端口
	NNN_API inline USHORT			GetPort()	{ return m_port; }

	// 获取服务器运行状态
	NNN_API inline es_ServerState	GetState()	{ return m_state; }

	// 设置为不延迟（禁用 Nagle 算法）
	NNN_API inline void				set_no_delay(bool no_delay)	{ m_no_delay = no_delay; }
	NNN_API inline bool				get_no_delay()				{ return m_no_delay; }

	// 设置/获取系统缓冲区大小（返回 -1 时，表示出错）
	NNN_API inline void				set_send_buffer_size(int buffer_size)	{ m_system_send_buffer_size = buffer_size; }
	NNN_API inline int				get_send_buffer_size()					{ return m_system_send_buffer_size; }
	NNN_API inline void				set_recv_buffer_size(int buffer_size)	{ m_system_recv_buffer_size = buffer_size; }
	NNN_API inline int				get_recv_buffer_size()					{ return m_system_recv_buffer_size; }

	// 发送数据到指定 session（线程安全，可能会执行异步发送）
	//		<data_len>				- =0 时，表示不追加数据，仅发送已有的剩余数据
	//		<openssl_handshaking>	- =true 时，表示仅发送握手数据（不发送用户数据）
	NNN_API HRESULT					Send(	const BYTE				*data,
											size_t					data_len,
											struct s_SessionData	*sd,
											bool					openssl_handshaking	= false );

	// 发送「结束」标记（当之前的所有数据发送完毕后，自动断开连接）
	NNN_API HRESULT					Send_EOF(struct s_SessionData *sd);

	//// 从 session 读取数据（线程安全）
	////		remove_from_read_buffer = true 时，表示从接收缓冲区中移除读取的数据
	////		remove_from_read_buffer = false 时，表示不移除
	//NNN_API void					Read(	BYTE *buffer, size_t read_len, struct s_SessionData *sd,
	//										bool remove_from_read_buffer = true );

	// 断开指定 session 连接
	//（exec_callback = true 时，表示执行回调函数。handle_sessions_list = true 时，表示处理 session 列表）
	NNN_API HRESULT					DisconnectSession(	struct s_SessionData	*sd,
														bool					exec_callback			= true,
														bool					handle_sessions_list	= true );
	NNN_API HRESULT					DisconnectSession(	UINT64					session_id,
														bool					exec_callback			= true,
														bool					handle_sessions_list	= true );

	// 执行周期性工作（仅 select 模型有效）
	NNN_API HRESULT					DoWork(long timeout_sec = 0, long timeout_usec = 0);

	// 发送/接收速度、总大小
	NNN_API inline class c_Status*	GetStatus()	{ return &m_status; }

	//======================================================================

	// 获取会话连接数
	NNN_API inline size_t			GetSessionsCount()	{ return m_sessions.size(); }

	// 获取会话（nullptr 表示没有找到该会话）
	NNN_API struct s_SessionData*	GetSessionData(UINT64 session_id);

	// 设置新的 session_id
	NNN_API bool					Set_New_SessionID(struct s_SessionData *sd, UINT64 new_session_id);

	// 获取指定的 session 已接收数据的大小（接收缓冲区中的有效数据大小）
	NNN_API inline size_t			GetReceivedDataSize(struct s_SessionData *sd)
	{
		return (sd == nullptr) ? 0 : sd->RECV_DATA.m_buffer.get_data_len();
	}

	// 获取 session 列表
	template <size_t stack_size = 4096>
	NNN_API inline void				get_sessions_list(__out struct Buffer::s_StackBuffer<struct s_SessionData*, stack_size> &list, __out size_t &count)
	{
		count = 0;

		m_lock_sessions.Lock();

		list.reserve(sizeof(void*) * m_sessions.size());

		for(auto &kvp : m_sessions)
			list.m_p[count++] = kvp.second;

		m_lock_sessions.UnLock();
	}
	NNN_API void					get_sessions_list(__out std::vector<struct s_SessionData*> &list);

protected:
	// 工作者线程（IOCP/EPOLL 使用）
	static void				WorkerThread(void *param);
#if NNN_SOCKET_SUPPORT_IOCP
	void					WorkerThread_IOCP();
#elif NNN_SOCKET_SUPPORT_EPOLL
	void					WorkerThread_Epoll();
#endif	// NNN_SOCKET_SUPPORT_IOCP

	// 保活的工作者线程（IOCP/EPOLL 使用）
	static void				WorkerThread_KeepAlive(void *param);

	// 创建/释放 s_SessionData*（线程安全）
	struct s_SessionData*	Create_session_data();
	void					Release_session_data(struct s_SessionData *sd);

	// 获取需要 keep_alive 的 session 列表
	void					get_keepalive_sessions_list(__out std::vector<struct s_SessionData*> &list);

#if NNN_SOCKET_SUPPORT_IOCP
	// 初始化函数指针
	bool					init_func_pointer();

	// 执行 AcceptEx（AF = AF_INET/AF_INET6）
	HRESULT					DoAcceptEx(int af);
#endif	// NNN_SOCKET_SUPPORT_IOCP

	// 初始化/清理 OpenSSL 上下文
	HRESULT					OpenSSL_Init_CTX();
	HRESULT					OpenSSL_Release_CTX();

	// 将「SSL 套接字」和「传输层的套接字」绑定起来
	HRESULT					OpenSSL_accept(struct s_SessionData *sd);

	// 设置证书/密钥
	HRESULT					OpenSSL_set_pkey_cert_pem(	const BYTE	*ca_cert_pem,
														int			ca_cert_len,
														const BYTE	*pkey_pem,
														int			pkey_len );

protected:
	SOCKET						m_listen_socket_ipv4		= INVALID_SOCKET;			// Listen 的 socket（IPv4）
	SOCKET						m_listen_socket_ipv6		= INVALID_SOCKET;			// Listen 的 socket（IPv6）

#if NNN_SOCKET_SUPPORT_IOCP
	std::atomic<struct s_SessionData*>	m_last_AcceptEx_IPv4_sd	= nullptr;
	std::atomic<struct s_SessionData*>	m_last_AcceptEx_IPv6_sd	= nullptr;
#endif	// NNN_SOCKET_SUPPORT_IOCP

	USHORT						m_port						= 0;						// 本地端口

	std::atomic<es_ServerState>	m_state						= es_ServerState::Stopped;	// 服务器运行状态
	bool						m_is_iocp_epoll				= true;						// 是否使用 IOCP/Epoll

	// 发送/接收缓冲区的大小（仅给 Socket 内部使用，不影响应用层累积数据的缓冲区）
	USHORT						m_sending_buffer_size		= 8192;
	USHORT						m_receiving_buffer_size		= 8192;

	// 是否为不延迟（禁用 Nagle 算法）
	/*
		如果是 true，则表示关闭 Nagle 算法。
		在交互式场景下（例如需要快速响应的网游）建议设置成 true，否则会造成延迟；
		如果是不需要实时响应的场景（例如棋牌类游戏），建设设置成 false
	*/
	bool						m_no_delay					= false;

	// 默认系统缓冲区大小（也就是 SO_SNDBUF/SO_RCVBUF）
	int							m_default_system_send_buffer_size;
	int							m_default_system_recv_buffer_size;
	// 系统缓冲区大小
	int							m_system_send_buffer_size;
	int							m_system_recv_buffer_size;

	struct
	{
		bool					m_enabled;					// 启用保活（Win 的心跳包似乎无效，建议自定义 KeepAlive 回调函数）
		int						m_keepalivetime;			// 多长时间（秒）没有数据就开始 send 心跳包
		int						m_keepaliveinterval;		// 每隔多长时间（秒）send 一个心跳包
		int						m_count;					// 连续 x 次没收到心跳包，视为连接失效（对于 Win 系统，2000/XP/2003 默认发 5 次，Vista 后默认发 10 次）
#if NNN_SOCKET_SUPPORT_SELECT
		UINT64					m_can_check_tick;			// 可以检查保活的 tick
#endif	// NNN_SOCKET_SUPPORT_SELECT

		struct Thread::s_Thread	*m_thread	= nullptr;		// 保活的工作者线程
	} KEEP_ALIVE;

	NNN_HASH_MAP<UINT64, struct s_SessionData*>				m_sessions;			// session_id -> 会话连接
	class Thread::c_Atomic_Lock								m_lock_sessions;	// 锁定 m_sessions
	struct NNN::Buffer::s_Obj_Pool<struct s_SessionData>	m_sessions_pool;	// （不使用的）session 对象池

	// 工作者线程（IOCP/EPOLL 使用）
	std::vector<struct Thread::s_Thread*>						m_threads;

#if NNN_SOCKET_SUPPORT_IOCP
	struct
	{
		HANDLE						m_CompletionPort			= nullptr;	// 完成端口对象

		// AcceptEx/GetAcceptExSockaddrs/ConnectEx/DisconnectEx
		bool						m_have_init_func_pointer	= false;
		LPFN_ACCEPTEX				m_AcceptEx					= nullptr;
		LPFN_GETACCEPTEXSOCKADDRS	m_GetAcceptExSockaddrs		= nullptr;
	} IOCP;
#elif NNN_SOCKET_SUPPORT_EPOLL
	struct
	{
		int											m_epoll_fd	= 0;	// epoll_create 的 fd
	} EPOLL;
#endif	// NNN_SOCKET_SUPPORT_IOCP

	/*
	底层通讯流程：
		client -> server	[ssl]
		server -> client	[ssl]
		client -> server	[正常数据]
	*/
	struct
	{
		bool	m_use_openssl	= false;	// 是否使用 OpenSSL
		void	*m_server_ctx	= nullptr;	// OpenSSL 上下文（SSL_CTX* 类型）

		void	*m_cert			= nullptr;	// 证书（X509* 类型）
		void	*m_pkey			= nullptr;	// 密钥（EVP_PKEY* 类型）
	} OpenSSL;

public:
	// 回调函数
	struct
	{
		LPCALLBACK_ACCEPT					m_Accept_func					= nullptr;
		LPCALLBACK_DISCONNECTING			m_Disconnecting_func			= nullptr;
		LPCALLBACK_DISCONNECTED				m_Disconnected_func				= nullptr;
		LPCALLBACK_RECEIVED					m_Received_func					= nullptr;
		LPCALLBACK_RECEIVED					m_KeepAlive_func				= nullptr;
		LPCALLBACK_OPENSSL_HANDSHAKE_DONE	m_openssl_handshake_done_func	= nullptr;
	} CALLBACKS;

protected:
	// 发送/接收速度、总大小
	class c_Status	m_status;

	// 是否计算收发速度/大小等状态（s_SessionData）
	bool			m_calc_session_speed_status	= true;
};

}	// namespace Socket
}	// namespace NNN

#endif	// _NNNSOCKETSERVER___SERVER_SYSTEM___C_SERVER_H_
