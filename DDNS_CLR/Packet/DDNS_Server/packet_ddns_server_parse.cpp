//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../../Common/Encrypt.h"
#include "../../../ddns_server/Packet/packet_header.h"
#include "../../../ddns_server_client/Packet/packet.h"

#include "../../DDNS_CLR.h"

#include "packet.h"

namespace DDNS_CLR
{
namespace Packet
{
namespace DDNS_Server
{

/*==============================================================
 * 解析数据包
 * parse_packet()
 *==============================================================*/
void parse_packet()
{
	struct NNN::Socket::s_SessionData *sd = g_client->GetSessionData();

	BYTE all_header[2] = {};
	while(sd->RECV_DATA.m_buffer.get_data_len() >= PACKET_HEADER_LEN * 2)
	{
		size_t read_len = 0;
		sd->RECV_DATA.m_buffer.read_data((BYTE*)all_header, sizeof(all_header), read_len, false);

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
	namespace PACKET = ::DDNS_Server_Client::Packet;

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
	namespace PACKET = ::DDNS_Server_Client::Packet;

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
	namespace PACKET = ::DDNS_Server_Client::Packet;

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
	namespace PACKET = ::DDNS_Server_Client::Packet;

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
	namespace PACKET = ::DDNS_Server_Client::Packet;

	std::vector<struct ddns_server_CLR::s_Record> records;

	es_Parse_Result ret = PACKET::recv_Update_Domains_Result(sd, g_KeyIV.m_Key, g_KeyIV.m_IV, records);

	if(ret == es_Parse_Result::OK)
	{
		List<ddns_lib::c_Record^> ^gc_records = gcnew List<ddns_lib::c_Record^>((int)records.size());

		for(const struct ddns_server_CLR::s_Record &record : records)
		{
			ddns_lib::c_Record ^gc_record = gcnew ddns_lib::c_Record();

			gc_record->m_name		= gcnew String(record.m_name);
			gc_record->m_domain		= gcnew String(record.m_domain);
			gc_record->m_TTL		= record.m_TTL;
			gc_record->m_user_idx	= record.m_user_idx;

			gc_record->m_result_ip	= gcnew String(record.m_result_ip);
			gc_record->m_err_msg	= gcnew String(record.m_err_msg);

			gc_records->Add(gc_record);
		}	// for

		CLR::Event_Recv_Update_Domains_Result(gc_records);
	}

	return ret;
}

}	// namespace DDNS_Server
}	// namespace Packet
}	// namespace DDNS_CLR
