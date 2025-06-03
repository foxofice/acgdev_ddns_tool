#pragma once

#include "../../3rdParty/nnn/Src/nnnCLR/nnnCLR.h"

#include "Common/s_Domain.h"

/*==============================================================
 * s_Security_Profile <--> c_Security_Profile
 * make_Security_Profile()
 *==============================================================*/
namespace TO_NET
{
inline ddns_lib::c_Security_Profile^ make_Security_Profile(const struct s_Security_Profile &profile)
{
	ddns_lib::c_Security_Profile ^gc_profile = gcnew ddns_lib::c_Security_Profile();

	gc_profile->m_Godaddy__Key		= gcnew System::String(profile.GODADDY.m_Key);
	gc_profile->m_Godaddy__Secret	= gcnew System::String(profile.GODADDY.m_Secret);
	gc_profile->m_dynv6__token		= gcnew System::String(profile.DYNV6.m_token);

	return gc_profile;
}
}	// namespace TO_NET
//--------------------------------------------------
namespace TO_CPP
{
inline void make_Security_Profile(	__in ddns_lib::c_Security_Profile	^gc_profile,
									__out struct s_Security_Profile		&profile )
{
	NNN::CLR::TO_CPP::String_to_char(gc_profile->m_Godaddy__Key,	profile.GODADDY.m_Key);
	NNN::CLR::TO_CPP::String_to_char(gc_profile->m_Godaddy__Secret,	profile.GODADDY.m_Secret);
	NNN::CLR::TO_CPP::String_to_char(gc_profile->m_dynv6__token,	profile.DYNV6.m_token);
}
}	// namespace TO_CPP


/*==============================================================
 * s_Domain <--> c_Domain
 * make_domain()
 *==============================================================*/
namespace TO_NET
{
inline ddns_lib::c_Domain^ make_domain(	const struct s_Domain			&domain,
										ddns_lib::c_Security_Profile	^profile )
{
	ddns_lib::c_Domain ^gc_domain = gcnew ddns_lib::c_Domain();

	gc_domain->m_domain				= gcnew System::String(domain.m_domain);
	gc_domain->m_type				= (ddns_lib::e_DomainType)domain.m_type;
	gc_domain->m_input_IPv4			= gcnew System::String(domain.m_input_IPv4);
	gc_domain->m_input_IPv6			= gcnew System::String(domain.m_input_IPv6);
	gc_domain->m_current_IPv4		= gcnew System::String(domain.m_current_IPv4);
	gc_domain->m_current_IPv6		= gcnew System::String(domain.m_current_IPv6);

	gc_domain->m_Godaddy__TTL		= domain.GODADDY.m_TTL;

	gc_domain->m_dynv6__Auto_IPv4	= domain.DYNV6.m_Auto_IPv4;
	gc_domain->m_dynv6__Auto_IPv6	= domain.DYNV6.m_Auto_IPv6;

	gc_domain->m_Security_Profile	= profile;

	gc_domain->m_err_msg_IPv4		= gcnew System::String(domain.m_err_msg_IPv4);
	gc_domain->m_err_msg_IPv6		= gcnew System::String(domain.m_err_msg_IPv6);

	gc_domain->m_enabled			= domain.m_enabled;

	return gc_domain;
}
}	// namespace TO_NET
//--------------------------------------------------
namespace TO_CPP
{
inline void make_Domain(__in ddns_lib::c_Domain			^gc_domain,
						__in struct s_Security_Profile	*profile,
						__out struct s_Domain			&domain)
{
	NNN::CLR::TO_CPP::String_to_char(gc_domain->m_domain,		domain.m_domain);
	domain.m_type				= (es_DomainType)gc_domain->m_type;
	NNN::CLR::TO_CPP::String_to_char(gc_domain->m_input_IPv4,	domain.m_input_IPv4);
	NNN::CLR::TO_CPP::String_to_char(gc_domain->m_input_IPv6,	domain.m_input_IPv6);
	NNN::CLR::TO_CPP::String_to_char(gc_domain->m_current_IPv4,	domain.m_current_IPv4);
	NNN::CLR::TO_CPP::String_to_char(gc_domain->m_current_IPv6,	domain.m_current_IPv6);

	domain.GODADDY.m_TTL		= gc_domain->m_Godaddy__TTL;

	domain.DYNV6.m_Auto_IPv4	= gc_domain->m_dynv6__Auto_IPv4;
	domain.DYNV6.m_Auto_IPv6	= gc_domain->m_dynv6__Auto_IPv6;

	domain.m_Security_Profile	= profile;

	NNN::CLR::TO_CPP::String_to_wchar(gc_domain->m_err_msg_IPv4,	domain.m_err_msg_IPv4);
	NNN::CLR::TO_CPP::String_to_wchar(gc_domain->m_err_msg_IPv6,	domain.m_err_msg_IPv6);

	domain.m_enabled			= gc_domain->m_enabled;
}
}	// namespace TO_CPP
