//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : Socket 库接口
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNSOCKET___SOCKET_H_
#define _NNNSOCKET___SOCKET_H_

#include "nnnSocket-inc.h"
#include "nnnSocket-macro.h"
#include "nnnSocket-gcc.h"

namespace NNN
{
namespace Socket
{

// 初始化/清理
NNN_API HRESULT		DoInit();
NNN_API HRESULT		DoFinal();

// 枚举所有协议（成功的话，dwCount 将返回协议数）
#ifdef _WIN32
NNN_API HRESULT		EnumProtocols(__out WSAPROTOCOL_INFO **wsapi, __out DWORD *dwCount);
#endif	// _WIN32

// 建立套接字
NNN_API HRESULT		Socket(	__out SOCKET	&s,
							int				af			= AF_INET,
							int				type		= SOCK_STREAM,
							int				protocol	= IPPROTO_TCP);

// 接受连接（无连接只有 bind，没有 listen）
NNN_API HRESULT		Accept(	SOCKET					server_socket,
							__out SOCKET			&client_socket,
							__out struct sockaddr	*server_addr,
							__inout_opt socklen_t	*server_addrlen,
							__out struct sockaddr	*client_addr,
							__inout_opt socklen_t	*client_addrlen );
NNN_API HRESULT		Accept(	SOCKET				server_socket,
							__out SOCKET		&client_socket,
							__out sockaddr_in	&server_addr,
							__out sockaddr_in	&client_addr );
NNN_API HRESULT		Accept(	SOCKET				server_socket,
							__out SOCKET		&client_socket,
							__out sockaddr_in6	&server_addr,
							__out sockaddr_in6	&client_addr );

// 发送数据（如果返回值不成功，表示 socket 已关闭）
//（flags 可为 0、MSG_DONTROUTE、MSG_OOB、MSG_PARTIAL 的组合）
// 如果 WSAGetLastError() 为 WSAECONNABORTED 或 WSAECONNRESET 的时候，应该关闭这个套接字
//（当 pOverlapped != nullptr 时，NumberOfBytesRecvd 无效）
NNN_API HRESULT		Send(	SOCKET				s,
							const BYTE			*buf,
							int					len,
							__out DWORD			&NumberOfBytesSent,
							int					flags			= 0,
							void/*OVERLAPPED*/	*pOverlapped	= nullptr );

// 发送所有数据（如果返回值不成功，表示 socket 已关闭）
NNN_API HRESULT		SendAll(SOCKET				s,
							const BYTE			*buf,
							int					len,
							int					flags			= 0,
							void/*OVERLAPPED*/	*pOverlapped	= nullptr);

// 发送数据到指定地址（flags 可为 0、MSG_DONTROUTE、MSG_OOB、MSG_PARTIAL 的组合）
NNN_API HRESULT		SendTo(	SOCKET					s,
							const BYTE				*buf,
							int						len,
							const struct sockaddr	*to,
							int						tolen,
							__out DWORD				&NumberOfBytesSent,
							int						flags			= 0,
							void/*OVERLAPPED*/		*pOverlapped	= nullptr );
NNN_API HRESULT		SendTo(	SOCKET						s,
							const BYTE					*buf,
							int							len,
							const struct sockaddr_in	&addr,
							__out DWORD					&NumberOfBytesSent,
							int							flags			= 0,
							void/*OVERLAPPED*/			*pOverlapped	= nullptr );
NNN_API HRESULT		SendTo(	SOCKET						s,
							const BYTE					*buf,
							int							len,
							const struct sockaddr_in6	&addr,
							__out DWORD					&NumberOfBytesSent,
							int							flags			= 0,
							void/*OVERLAPPED*/			*pOverlapped	= nullptr );

// 接收数据（如果返回值不成功，表示 socket 已关闭）
//（flags 可为 0、MSG_PEEK、MSG_OOB、MSG_PARTIAL、MSG_WAITALL 的组合）
//（当 pOverlapped != nullptr 时，NumberOfBytesRecvd 无效）
NNN_API HRESULT		Recv(	SOCKET				s,
							__out BYTE			*buf,
							int					len,
							__out DWORD			&NumberOfBytesRecvd,
							int					flags			= 0,
							void/*OVERLAPPED*/	*pOverlapped	= nullptr );

// 从指定地址接收数据（flags 可为 0、MSG_PEEK、MSG_OOB、MSG_PARTIAL 的组合）
NNN_API HRESULT		RecvFrom(	SOCKET					s,
								__out BYTE				*buf,
								int						len,
								__out struct sockaddr	*from,
								socklen_t				*fromlen,
								__out DWORD				&NumberOfBytesRecvd,
								int						flags			= 0,
								void/*OVERLAPPED*/		*pOverlapped	= nullptr );
NNN_API HRESULT		RecvFrom(	SOCKET						s,
								__out BYTE					*buf,
								int							len,
								__out struct sockaddr_in	&addr,
								__out DWORD					&NumberOfBytesRecvd,
								int							flags			= 0,
								void/*OVERLAPPED*/			*pOverlapped	= nullptr );
NNN_API HRESULT		RecvFrom(	SOCKET						s,
								__out BYTE					*buf,
								int							len,
								__out struct sockaddr_in6	&addr,
								__out DWORD					&NumberOfBytesRecvd,
								int							flags			= 0,
								void/*OVERLAPPED*/			*pOverlapped	= nullptr );

// 从容关闭（<how>：SD_RECEIVE、SD_SEND、SD_BOTH）
NNN_API HRESULT		Shutdown(SOCKET s, int how = SD_BOTH);

// 关闭 Socket
NNN_API HRESULT		CloseSocket(SOCKET s);

// 设置 I/O 模式
NNN_API HRESULT		IoCtlSocket(SOCKET s, long cmd, __inout u_long *argp);

// 设置为非阻塞
NNN_API HRESULT		set_nonblocking(SOCKET s, bool NonBlock = true);

// 设置为不延迟（禁用 Nagle 算法）
NNN_API HRESULT		set_no_delay(SOCKET s, bool no_delay = true);

// 设置/获取系统缓冲区大小（返回 -1 时，表示出错）
NNN_API void		set_system_send_buffer_size(SOCKET s, int buffer_size);
NNN_API int			get_system_send_buffer_size(SOCKET s);
NNN_API void		set_system_recv_buffer_size(SOCKET s, int buffer_size);
NNN_API int			get_system_recv_buffer_size(SOCKET s);

// 设置保活（Win 版本貌似无效）
/*
	<idle> 秒钟无数据，触发保活机制，发送保活包；
	如果没有收到回应，则 <intv> 秒钟后重发保活包；
	连续 <cnt> 次没收到保活包，视为连接失效（对于 Win 系统，2000/XP/2003 默认发 5 次，Vista 后默认发 10 次）
*/
#if defined(NNN_ANDROID) || defined(NNN_LINUX)
NNN_API HRESULT		SetKeepAlive(	SOCKET				s,
									bool				keep_alive		= true,
									int					idle			= 10,
									int					intv			= 2,
									int					cnt				= 3,
									void/*OVERLAPPED*/	*pOverlapped	= nullptr );
#endif	// NNN_ANDROID || NNN_LINUX

// gethostbyaddr
// getservbyname
// WSASendDisconnect
// WSARecvDisconnect
// WSADuplicateSocket
// TransmitFile
// WSAGETSELECTEVENT
// WSAEnumNetworkEvents

// 名字解析（pNodeName = "" 时，表示本机）
NNN_API HRESULT		host2ip(const char		*pNodeName,
							__out UINT8		&ipv4_count,
							__out in_addr	ipv4_list[NNN_MAX_IP_COUNT],
							__out UINT8		&ipv6_count,
							__out in6_addr	ipv6_list[NNN_MAX_IP_COUNT]);

// IP 地址 -> 字符串
NNN_API char*		ip2str(in_addr addr, __out char buffer[16]);
NNN_API char*		ip2str(in6_addr addr, __out char buffer[46]);

// 取得本地 MAC 地址
NNN_API HRESULT		get_mac_address(__out BYTE MAC[ETHER_ADDR_LEN]);

// 取得本地 IP 列表
NNN_API HRESULT		get_local_ip_list(	__out UINT8				&ip_count,
										__out struct s_Local_IP	ip_list[NNN_MAX_IP_COUNT] );

// 判断一个 IP 是否局域网 IP
NNN_API bool		is_lan_address(const char *ip);

// 判断一个 IP 是否为 IPv4/IPv6
NNN_API bool		is_IPv4(const char *ip);
NNN_API bool		is_IPv6(const char *ip);

//======================================================================

// bind + listen（addr = INADDR_ANY/in6addr_any 时，表示所有地址。<backlog> 在 linux 下的默认值为 20）
// （无连接只有 bind，没有 listen）
NNN_API HRESULT		make_bind_listen(	__out SOCKET			&s,
										const struct sockaddr	*name,
										int						namelen,
										int						af			= AF_INET,
										int						type		= SOCK_STREAM,
										int						protocol	= IPPROTO_TCP,
										int						backlog		= SOMAXCONN );
NNN_API HRESULT		make_bind_listen(	__out SOCKET			&s,
										in_addr					addr,
										USHORT					port,
										int						type		= SOCK_STREAM,
										int						protocol	= IPPROTO_TCP,
										int						backlog		= SOMAXCONN );
NNN_API HRESULT		make_bind_listen(	__out SOCKET			&s,
										in6_addr				addr,
										USHORT					port,
										int						type		= SOCK_STREAM,
										int						protocol	= IPPROTO_TCP,
										int						backlog		= SOMAXCONN );

// bind + listen（IPv4 + IPv6）
NNN_API HRESULT		make_bind_listen(__out SOCKET &socket_ipv4, __out SOCKET &socket_ipv6, USHORT port);

//======================================================================

// connect（无连接也可以 connect；timeout < 0 时，表示使用默认 timeout，timeout 单位为毫秒）
NNN_API HRESULT		make_connect(	__out SOCKET			&s,
									const struct sockaddr	*name,
									int						namelen,
									int						af			= AF_INET,
									int						type		= SOCK_STREAM,
									int						protocol	= IPPROTO_TCP,
									int						timeout		= -1 );
NNN_API HRESULT		make_connect(	__out SOCKET	&s,
									in_addr			addr,
									USHORT			port,
									int				type		= SOCK_STREAM,
									int				protocol	= IPPROTO_TCP,
									int				timeout		= -1 );
NNN_API HRESULT		make_connect(	__out SOCKET	&s,
									in6_addr		addr,
									USHORT			port,
									int				type		= SOCK_STREAM,
									int				protocol	= IPPROTO_TCP,
									int				timeout		= -1 );

// connect（IPv4 + IPv6）
NNN_API HRESULT		make_connect(	__out SOCKET	&s,
									const char		*host,
									USHORT			port,
									__out USHORT	&client_port,
									__out int		&af,
									__out in_addr	&server_addr_ipv4,
									__out in6_addr	&server_addr_ipv6,
									__out in_addr	&client_addr_ipv4,
									__out in6_addr	&client_addr_ipv6,
									int				timeout	= -1 );

//======================================================================

// dwError -> const char*
NNN_API inline const char*	GetErrorTxt(DWORD dwError, __out char error_msg_tmp[64])
{
	switch(dwError)
	{
	case WSAECONNABORTED:		return "WSAECONNABORTED";
	case WSAECONNRESET:			return "WSAECONNRESET";
	case WSAEFAULT:				return "WSAEFAULT";
	case WSAEINTR:				return "WSAEINTR";
	case WSAEINPROGRESS:		return "WSAEINPROGRESS";
	case WSAEINVAL:				return "WSAEINVAL";
	case WSAEMSGSIZE:			return "WSAEMSGSIZE";
	case WSAENETDOWN:			return "WSAENETDOWN";
	case WSAENETRESET:			return "WSAENETRESET";
	case WSAENOBUFS:			return "WSAENOBUFS";
	case WSAENOTCONN:			return "WSAENOTCONN";
	case WSAENOTSOCK:			return "WSAENOTSOCK";
	case WSAEOPNOTSUPP:			return "WSAEOPNOTSUPP";
	case WSAESHUTDOWN:			return "WSAESHUTDOWN";
	case WSAEWOULDBLOCK:		return "WSAEWOULDBLOCK";	// 经常发生的错误？
	case WSANOTINITIALISED:		return "WSANOTINITIALISED";
#ifdef _WIN32
	case WSA_IO_PENDING:		return "WSA_IO_PENDING";	// 经常发生的错误
	case WSA_OPERATION_ABORTED:	return "WSA_OPERATION_ABORTED";
#endif	// _WIN32

	case WSAEACCES:				return "WSAEACCES";
	case WSAEADDRNOTAVAIL:		return "WSAEADDRNOTAVAIL";
	case WSAEAFNOSUPPORT:		return "WSAEAFNOSUPPORT";
	case WSAEDESTADDRREQ:		return "WSAEDESTADDRREQ";
	case WSAEHOSTUNREACH:		return "WSAEHOSTUNREACH";
	case WSAENETUNREACH:		return "WSAENETUNREACH";

	case WSAEDISCON:			return "WSAEDISCON";
	case WSAETIMEDOUT:			return "WSAETIMEDOUT";

	case WSAEADDRINUSE:			return "WSAEADDRINUSE";
	case WSAEISCONN:			return "WSAEISCONN";
	case WSAEMFILE:				return "WSAEMFILE";

	case WSAEALREADY:			return "WSAEALREADY";
	case WSAECONNREFUSED:		return "WSAECONNREFUSED";
	case WSAEPROTONOSUPPORT:	return "WSAEPROTONOSUPPORT";
	}	// switch

	C::SPRINTF(error_msg_tmp, 64, "ErrorCode(%u)", dwError);

	return error_msg_tmp;
}

//======================================================================

// 追加（用于调试的）封包数据到指定的文件名（is_send = true 时，表示发送；否则表示接收）
NNN_API void	append_DebugPacketData(	const BYTE	*data,
										size_t		data_len,
										const WCHAR	*filename,
										const char	*ip1,
										USHORT		port1,
										const char	*ip2,
										USHORT		port2,
										bool		is_send );

}	// namespace Socket
}	// namespace NNN

#endif	// _NNNSOCKET___SOCKET_H_
