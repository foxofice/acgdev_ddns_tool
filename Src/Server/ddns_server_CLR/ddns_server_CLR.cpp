//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------
#include "../../../3rdParty/nnn/Src/nnnCLR/nnnCLR.h"

#include "../CLR.h"
#include "ddns_server_CLR.h"

using namespace System;
using namespace System::Threading;
using namespace System::Collections::Generic;
using namespace System::Drawing;
using namespace System::Net;

namespace ddns_server_CLR
{

/*==============================================================
 * 添加日志
 * Add_Log()
 *==============================================================*/
void Add_Log(String ^txt, Color /*c*/)
{
	Console::WriteLine(txt);
}


/*==============================================================
 * 初始化
 * DoInit()
 *==============================================================*/
void DoInit()
{
	/*
		纯 c++ 的执行程序调用 .net 时，ServicePointManager.SecurityProtocol 可能并没有正确进行设置，
		当纯 c++ 项目设置成 clr 支持时，项目可能正确进行了设置，所以能解决问题（请求被中止: 未能创建 SSL/TLS 安全通道。），
		所以必须加上这行代码
	*/
	ServicePointManager::SecurityProtocol		= SecurityProtocolType::Tls13;

	ServicePointManager::DefaultConnectionLimit	= 1000;

	ddns_lib::LIB::EVENTS::Event_On_AddLog += gcnew ddns_lib::LIB::EVENTS::e_Add_Log(&Add_Log);
}


/*==============================================================
 * 执行更新 IP 记录
 * update_domains()
 *==============================================================*/
void update_domains(__inout std::vector<struct s_Domain>	&domains,
					bool									DNS_Lookup_First,
					__in_opt const std::vector<std::string>	*DNS_Server_List,	// 自定义DNS服务器（列表元素为 "" 时，表示系统默认 DNS）
					int										timeout)
{
	List<ddns_lib::c_Domain^> ^gc_domains = gcnew List<ddns_lib::c_Domain^>((int)domains.size());

	for(const struct s_Domain &domain : domains)
	{
		ddns_lib::c_Security_Profile ^profile = nullptr;

		if(domain.m_Security_Profile != nullptr)
			profile = TO_NET::make_Security_Profile(*domain.m_Security_Profile);

		ddns_lib::c_Domain ^gc_domain = TO_NET::make_domain(domain, profile);

		gc_domains->Add(gc_domain);
	}	// for

	List<System::String^> ^gc_DNS_Server_List = nullptr;

	if(DNS_Server_List != nullptr)
	{
		gc_DNS_Server_List = gcnew List<System::String^>((int)DNS_Server_List->size());

		for(const std::string &dns : *DNS_Server_List)
			gc_DNS_Server_List->Add(gcnew System::String(dns.c_str()));
	}

	ddns_lib::LIB::update_domains(gc_domains, DNS_Lookup_First, gc_DNS_Server_List, timeout);

	// 处理结果
	for(int i=0; i<gc_domains->Count; ++i)
	{
		ddns_lib::c_Domain	^gc_domain	= gc_domains[i];
		struct s_Domain		&domain		= domains[i];

		NNN::CLR::TO_CPP::String_to_char(gc_domain->m_current_IPv4,		domain.m_current_IPv4);
		NNN::CLR::TO_CPP::String_to_char(gc_domain->m_current_IPv6,		domain.m_current_IPv6);
		NNN::CLR::TO_CPP::String_to_wchar(gc_domain->m_err_msg_IPv4,	domain.m_err_msg_IPv4);
		NNN::CLR::TO_CPP::String_to_wchar(gc_domain->m_err_msg_IPv6,	domain.m_err_msg_IPv6);
	}	// for
}

}	// namespace ddns_server_CLR
