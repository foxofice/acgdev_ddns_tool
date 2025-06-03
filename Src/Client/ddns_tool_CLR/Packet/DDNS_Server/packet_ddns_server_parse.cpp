//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../../../Server/Common/Encrypt.h"
#include "../../../../Server/ddns_server/Packet/packet_header.h"
#include "../../../../Server/ddns_server__Client/Packet/packet.h"
#include "../../../../Server/CLR.h"

#include "../../ddns_tool_CLR.h"

#include "packet.h"

namespace ddns_tool_CLR
{
namespace Packet
{
namespace DDNS_Server
{

namespace PACKET = ::ddns_server__Client::Packet;

/*==============================================================
 * 解析数据包
 * parse_packet()
 *==============================================================*/
void parse_packet()
{
	struct NNN::Socket::s_SessionData *sd = g_client->GetSessionData();

	BYTE all_header[PACKET_HEADER_LEN * 2] = {};

	while(sd->RECV_DATA.m_buffer.get_data_len() >= sizeof(all_header))
	{
		if(sd->RECV_DATA.m_buffer.read_data(all_header, sizeof(all_header), false) != sizeof(all_header))
			continue;

		// 解码 header
		BYTE header = Common::Encrypt::decode_header(all_header[0], all_header[1]);

		es_Parse_Result res = es_Parse_Result::Unknown;

#define SWITCH_LINE(val, func_name)	case (BYTE)val:	res = func_name(sd);	break;

		// 解析 packet
		switch(header)
		{
			using ES_HEADER = ::DDNS_Server::Packet::es_Header;

		// Server 发送心跳包
		SWITCH_LINE(ES_HEADER::Server_KeepAlive,				recv_KeepAlive);

		// Server 回应 Ping
		SWITCH_LINE(ES_HEADER::Server_Ping,						recv_Ping);

		// 【登录验证】

		// Server 发送 KeyIV
		SWITCH_LINE(ES_HEADER::Server_KeyIV,					recv_KeyIV);

		// Server 发送「登录结果」
		SWITCH_LINE(ES_HEADER::Server_Login_Result,				recv_Login_Result);

		//【登录验证后】

		// Server 发送「更新域名的 A/AAAA 记录的结果」
		SWITCH_LINE(ES_HEADER::Server_Update_Domains_Result,	recv_Update_Domains_Result);
		}	// switch

#undef SWITCH_LINE

		switch(res)
		{
		case es_Parse_Result::Unknown:
			CLR::Event_On_add_log(	String::Format("Unknown packet {0:d} !", header),
									Color::Red );
			break;

		case es_Parse_Result::Attack:
			CLR::Event_On_add_log(	String::Format("Packet {0:d} attack!!", header),
									Color::Red );
			break;

		case es_Parse_Result::Error:
			CLR::Event_On_add_log(	String::Format("Packet {0:d} parse error!!!!", header),
									Color::Red );
			break;
		}	// switch

		CHECK_PACKET_RES_CLIENT(g_client);
	}	// while
}


/*==============================================================
 * Server 发送心跳包
 * recv_KeepAlive()
 *==============================================================*/
es_Parse_Result recv_KeepAlive(struct NNN::Socket::s_SessionData *sd)
{
	es_Parse_Result ret = PACKET::recv_KeepAlive(sd);

	if(ret == es_Parse_Result::OK)
	{
		// do nothing
	}

	return ret;
}


/*==============================================================
 * Server 回应 Ping
 * recv_Ping()
 *==============================================================*/
es_Parse_Result recv_Ping(struct NNN::Socket::s_SessionData *sd)
{
	double ping = 0.0f;

	es_Parse_Result ret = PACKET::recv_Ping(sd, ping);

	if(ret == es_Parse_Result::OK)
	{
		CLR::Event_Recv_Ping(ping);
	}

	return ret;
}

// 【登录验证】

/*==============================================================
 * Server 发送 KeyIV
 * recv_KeyIV()
 *==============================================================*/
es_Parse_Result recv_KeyIV(struct NNN::Socket::s_SessionData *sd)
{
	BYTE	send_Key[AES_KEY_LEN];
	BYTE	send_IV[AES_IV_LEN];

	es_Parse_Result ret = PACKET::recv_KeyIV(	sd,
												g_login_user->c_str(),
												g_login_pwd->c_str(),
												send_Key,
												send_IV );

	if(ret == es_Parse_Result::OK)
	{
		CopyMemory(g_KeyIV.m_Key,	send_Key,	sizeof(send_Key));
		CopyMemory(g_KeyIV.m_IV,	send_IV,	sizeof(send_IV));

		// Client 发送「登录数据」
		send_Login_Data(send_Key, send_IV);
	}

	return ret;
}


/*==============================================================
 * Server 发送「登录结果」
 * recv_Login_Result()
 *==============================================================*/
es_Parse_Result recv_Login_Result(struct NNN::Socket::s_SessionData *sd)
{
	es_Result result;

	es_Parse_Result ret = PACKET::recv_Login_Result(sd, result);

	if(ret == es_Parse_Result::OK)
	{
		CLR::Event_Recv_LoginResult(result == es_Result::OK);

		if(result != es_Result::OK)
			g_client->DisConnect();
	}

	return ret;
}

//【登录验证后】

/*==============================================================
 * Server 发送「更新域名的 A/AAAA 记录的结果」
 * recv_Update_Domains_Result()
 *==============================================================*/
es_Parse_Result recv_Update_Domains_Result(struct NNN::Socket::s_SessionData *sd)
{
	std::vector<struct s_Domain> domains;

	es_Parse_Result ret = PACKET::recv_Update_Domains_Result(sd, g_KeyIV.m_Key, g_KeyIV.m_IV, domains);

	if(ret == es_Parse_Result::OK)
	{
		List<ddns_lib::c_Domain^> ^gc_domains = gcnew List<ddns_lib::c_Domain^>((int)domains.size());

		for(const struct s_Domain &domain : domains)
		{
			ddns_lib::c_Domain ^gc_domain = TO_NET::make_domain(domain, nullptr);
			gc_domains->Add(gc_domain);
		}	// for

		CLR::Event_Recv_Update_Domains_Result(gc_domains);
	}

	return ret;
}

}	// namespace DDNS_Server
}	// namespace Packet
}	// namespace ddns_tool_CLR
