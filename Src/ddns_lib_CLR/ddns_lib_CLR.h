#pragma once

#include "../../3rdParty/nnn/Src/nnnCLR/nnnCLR.h"

using namespace System;
using namespace System::Collections::Generic;

namespace ddns_lib_CLR
{

public ref class c_DNS_Lookup_Result
{
public:
	List<String^>	^m_ipv4_list		= nullptr;
	String			^m_ipv4_error_msg	= "";

	List<String^>	^m_ipv6_list		= nullptr;
	String			^m_ipv6_error_msg	= "";
};

public ref class CLR
{
public:
	// DNS ½âÎö
	static bool	DNS_Lookup(String ^dns_server, String ^domain, __out c_DNS_Lookup_Result ^result);
};

}	// namespace ddns_lib_CLR
