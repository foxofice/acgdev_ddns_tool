//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../../Common/Encrypt.h"

#include "packet.h"

namespace ddns_server__Client
{
namespace Packet
{

/*==============================================================
 * Server ����������
 * recv_KeepAlive()
 *==============================================================*/
es_Parse_Result recv_KeepAlive(struct NNN::Socket::s_SessionData *sd)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// ��ȡ packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	return es_Parse_Result::OK;
}


/*==============================================================
 * Server ��Ӧ Ping
 * recv_Ping()
 *==============================================================*/
es_Parse_Result recv_Ping(	struct NNN::Socket::s_SessionData	*sd,
							__out double						&ping )
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(ping)];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// ��ȡ packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	// ��������
	double client_time = br.read<double>();

	ping = NNN::Time::get_time() - client_time;

	return es_Parse_Result::OK;
}

// ����¼��֤��

/*==============================================================
 * Server ���� KeyIV
 * recv_KeyIV()
 *==============================================================*/
es_Parse_Result recv_KeyIV(	struct NNN::Socket::s_SessionData	*sd,
							const WCHAR							*user,
							const WCHAR							*password,
							__out BYTE							send_Key[AES_KEY_LEN],
							__out BYTE							send_IV[AES_IV_LEN] )
{
	BYTE								send_KeyIV[AES_KEY_LEN + AES_IV_LEN];
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(send_KeyIV)];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// ��ȡ packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	// ��ѹ
	BYTE Key[AES_KEY_LEN];
	BYTE IV[AES_IV_LEN];
	Common::Encrypt::gen_KeyIV(user, password, Key, IV);

	HRESULT hr = NNN::Encrypt::Rijndael_Decrypt(packet_data + br.m_offset,
												sizeof(send_KeyIV),
												Key,
												IV,
												send_KeyIV);
	if(FAILED(hr))
		return es_Parse_Result::Attack;

	CopyMemory(send_Key,	send_KeyIV,					AES_KEY_LEN);
	CopyMemory(send_IV,		send_KeyIV + AES_KEY_LEN,	AES_IV_LEN);

	return es_Parse_Result::OK;
}


/*==============================================================
 * Server ���͡���¼�����
 * recv_Login_Result()
 *==============================================================*/
es_Parse_Result recv_Login_Result(struct NNN::Socket::s_SessionData *sd, __out es_Result &result)
{
	BYTE								packet_data[PACKET_HEADER_LEN * 2	+
													sizeof(BYTE)];	// result
	struct NNN::Buffer::s_BinaryReader	br(packet_data);

	// ��ȡ packet_data
	NNN_PACKET_READ_DATA_FIXED(sd->RECV_DATA.m_buffer, sizeof(packet_data));

	// ��������
	result = (es_Result)br.read<BYTE>();

	return es_Parse_Result::OK;
}

//����¼��֤��

/*==============================================================
 * Server ���͡����������� A/AAAA ��¼�Ľ����
 * recv_Update_Domains_Result()
 *==============================================================*/
es_Parse_Result recv_Update_Domains_Result(	struct NNN::Socket::s_SessionData	*sd,
											const BYTE							Key[AES_KEY_LEN],
											const BYTE							IV[AES_IV_LEN],
											__out std::vector<struct s_Domain>	&domains )
{
	BYTE								packet_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);
	USHORT								packet_len	= 0;

	// ��ȡ packet_data
	NNN_PACKET_READ_DATA(sd->RECV_DATA.m_buffer);

	// ��������
	USHORT aes_data_len = (USHORT)(packet_len - br.m_offset);

	const BYTE *aes_data = br.read_array(aes_data_len);

	BYTE								data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryReader	br_data(data);

	HRESULT hr = NNN::Encrypt::Rijndael_Decrypt(aes_data, aes_data_len, Key, IV, data);
	if(FAILED(hr))
		return es_Parse_Result::Error;

	// ���� aes_data �Ľ�������
	USHORT count = br_data.read<USHORT>();
	domains.resize(count);

	for(USHORT i=0; i<count; ++i)
	{
		struct s_Domain &domain = domains[i];

		// domain_len
		BYTE domain_len = br_data.read<BYTE>();

		// domain
		const BYTE *domain_ = br_data.read_array(domain_len);

		CopyMemory(domain.m_domain, domain_, domain_len);
		domain.m_domain[domain_len] = '\0';

		// current_IPv4_len
		BYTE current_IPv4_len = br_data.read<BYTE>();

		// current_IPv4
		const BYTE *current_IPv4 = br_data.read_array(current_IPv4_len);

		CopyMemory(domain.IPv4.m_current_IP, current_IPv4, current_IPv4_len);
		domain.IPv4.m_current_IP[current_IPv4_len] = '\0';

		// same_ipv4
		domain.IPv4.m_same_ip = br_data.read<bool>();

		// err_msg_IPv4_len
		USHORT err_msg_IPv4_len = br_data.read<USHORT>();

		// err_msg_IPv4
		br_data.read_wchar2(domain.IPv4.m_err_msg, err_msg_IPv4_len / 2);

		// current_IPv6_len
		BYTE current_IPv6_len = br_data.read<BYTE>();

		// current_IPv6
		const BYTE *current_IPv6 = br_data.read_array(current_IPv6_len);

		CopyMemory(domain.IPv6.m_current_IP, current_IPv6, current_IPv6_len);
		domain.IPv6.m_current_IP[current_IPv6_len] = '\0';

		// same_ipv6
		domain.IPv6.m_same_ip = br_data.read<bool>();

		// err_msg_IPv6_len
		USHORT err_msg_IPv6_len = br_data.read<USHORT>();

		// err_msg_IPv6
		br_data.read_wchar2(domain.IPv6.m_err_msg, err_msg_IPv6_len / 2);
	}	// for

	return es_Parse_Result::OK;
}


/*==============================================================
 * Server ���� Log
 * recv_Log()
 *==============================================================*/
es_Parse_Result recv_Log(	struct NNN::Socket::s_SessionData	*sd,
							const BYTE							Key[AES_KEY_LEN],
							const BYTE							IV[AES_IV_LEN],
							__out WCHAR							log[4096],
							__out UINT							&rgb )
{
	BYTE								packet_data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryReader	br(packet_data);
	USHORT								packet_len	= 0;

	// ��ȡ packet_data
	NNN_PACKET_READ_DATA(sd->RECV_DATA.m_buffer);

	// ��������
	USHORT aes_data_len = (USHORT)(packet_len - br.m_offset);

	const BYTE *aes_data = br.read_array(aes_data_len);

	BYTE								data[USHRT_MAX];
	struct NNN::Buffer::s_BinaryReader	br_data(data);

	HRESULT hr = NNN::Encrypt::Rijndael_Decrypt(aes_data, aes_data_len, Key, IV, data);
	if(FAILED(hr))
		return es_Parse_Result::Error;

	// ���� aes_data �Ľ�������
	br_data.read_wchar2(log, (aes_data_len - 3) / 2);

	BYTE r = br_data.read<BYTE>();
	BYTE g = br_data.read<BYTE>();
	BYTE b = br_data.read<BYTE>();

	rgb = (0xff << 24) + (r << 16) + (g << 8) + b;

	return es_Parse_Result::OK;
}

}	// namespace Packet
}	// namespace ddns_server__Client
