//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "config.h"

namespace DDNS_Server
{
namespace Config
{

// 配置文件实例
struct s_Config	*g_config	= nullptr;

/*==============================================================
 * 初始化/清理
 * DoInit()
 * DoFinal()
 *==============================================================*/
HRESULT DoInit()
{
	if(g_config == nullptr)
		g_config = new struct s_Config();

	return S_OK;
}
//--------------------------------------------------
HRESULT DoFinal()
{
	SAFE_DELETE(g_config);

	return S_OK;
}

}	// namespace Config
}	// namespace DDNS_Server
