//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "session_KeyIV.h"

namespace DDNS_Server
{
namespace Session_KeyIV
{

struct
{
	// session_id -> s_AES_KeyIV*
	NNN_HASH_MAP<UINT64, struct s_AES_KeyIV*>			*m_list			= nullptr;
	struct NNN::Thread::s_CriticalSection				m_cs;

	// 对象池
	struct NNN::Buffer::s_Obj_Pool<struct s_AES_KeyIV>	*m_pool	= nullptr;
} KEY_IV;

/*==============================================================
 * 初始化/清理
 * DoInit()
 * DoFinal()
 *==============================================================*/
HRESULT DoInit()
{
	KEY_IV.m_list	= new NNN_HASH_MAP<UINT64, struct s_AES_KeyIV*>();
	KEY_IV.m_pool	= new struct NNN::Buffer::s_Obj_Pool<struct s_AES_KeyIV>();

	return S_OK;
}
//--------------------------------------------------
HRESULT DoFinal()
{
	if(KEY_IV.m_list != nullptr)
	{
		for(auto &kvp : *KEY_IV.m_list)
		{
			struct s_AES_KeyIV *key_iv = kvp.second;
			SAFE_DELETE(key_iv);
		}	// for

		SAFE_DELETE(KEY_IV.m_list);
	}

	SAFE_DELETE(KEY_IV.m_pool);

	return S_OK;
}


/*==============================================================
 * 生成新的 s_AES_KeyIV，并跟 session 关联
 * add_KeyIV()
 *==============================================================*/
struct s_AES_KeyIV* add_KeyIV(UINT64 session_id)
{
	struct s_AES_KeyIV *ret = nullptr;

	NNN::Thread::c_Lock l(KEY_IV.m_cs);

	auto iter = KEY_IV.m_list->find(session_id);
	if(iter == KEY_IV.m_list->end())
	{
		ret = KEY_IV.m_pool->create_object();
		ret->recalc_KeyIV();

		KEY_IV.m_list->insert({ session_id, ret });
	}

	return ret;
}


/*==============================================================
 * 移除 s_AES_KeyIV
 * remove_KeyIV()
 *==============================================================*/
void remove_KeyIV(UINT64 session_id)
{
	NNN::Thread::c_Lock l(KEY_IV.m_cs);

	auto iter = KEY_IV.m_list->find(session_id);
	if(iter != KEY_IV.m_list->end())
	{
		struct s_AES_KeyIV *key_iv = iter->second;
		KEY_IV.m_list->erase(iter);

		KEY_IV.m_pool->release_object(key_iv);
	}
}

}	// namespace Session_KeyIV
}	// namespace DDNS_Server
