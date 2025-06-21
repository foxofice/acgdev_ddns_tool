#include "../../3rdParty/nnn/Src/nnnSocket/nnnSocket.h"

#include "ddns_lib_CLR.h"

namespace ddns_lib_CLR
{

/*==============================================================
 * DNS ½âÎö
 * DNS_Lookup()
 *==============================================================*/
bool CLR::DNS_Lookup(String ^dns_server, String ^domain, __out c_DNS_Lookup_Result ^result)
{
	char dns_server_[1024];
	NNN::CLR::TO_CPP::String_to_char(dns_server, dns_server_);

	char domain_[1024];
	NNN::CLR::TO_CPP::String_to_char(domain, domain_);

	struct NNN::Socket:: s_DNS_Lookup_Result result_;
	bool res = NNN::Socket::DNS_Lookup(dns_server_, domain_, result_);

	if(res)
	{
		// IPv4
		result->m_ipv4_list = gcnew List<String^>((int)result_.IPV4.m_ip_list.size());

		for(const std::string &ip : result_.IPV4.m_ip_list)
			result->m_ipv4_list->Add(gcnew String(ip.c_str()));

		result->m_ipv4_error_msg = gcnew String(result_.IPV4.m_error_msg);

		// IPv6
		result->m_ipv6_list = gcnew List<String^>((int)result_.IPV6.m_ip_list.size());

		for(const std::string &ip : result_.IPV6.m_ip_list)
			result->m_ipv6_list->Add(gcnew String(ip.c_str()));

		result->m_ipv6_error_msg = gcnew String(result_.IPV6.m_error_msg);
	}

	return res;
}

}	// namespace ddns_lib_CLR
