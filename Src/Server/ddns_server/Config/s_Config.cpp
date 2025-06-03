//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//--------------------------------------------------------------------------------------

#include "../Log/Log.h"

#include "s_Config.h"

namespace DDNS_Server
{
namespace Config
{

/*==============================================================
 * 构造函数/析构函数
 *==============================================================*/
s_Config::s_Config()
{
	Reset();
}
//--------------------------------------------------
s_Config::~s_Config()
{
}


/*==============================================================
 * 重置成员变量
 * Reset()
 *==============================================================*/
void s_Config::Reset()
{
	//==================================================
	// 一般设置
	//==================================================
	m_busy_time						= 300;

	//==================================================
	// Socket 设置
	//==================================================
	SOCKET.m_port					= 0;
	SOCKET.m_keepalive_idle			= 10;

	//==================================================
	// 安全登录设置
	//==================================================
	LOGIN.m_user					= L"\0";
	LOGIN.m_password				= L"\0";

	LOGIN.m_timeout					= 20;

	//==================================================
	// 日志记录
	//==================================================
	_STRCPY(LOG.m_filename,	"Logs/%04d%02d%02d.log");

	LOG.m_console_silent_flag		= 0;
	LOG.m_write_file_silent_flag	= 0;
	LOG.m_write_file_interval		= 1000;

	_STRCPY(LOG.m_timestamp_format,	"%Y.%m.%d %H:%M:%S");

	LOG.m_show_accept_client		= true;
	LOG.m_show_disconnect_client	= true;

	LOG.m_show_login_result			= true;
}


/*==============================================================
 * 读取配置文件
 * Read_Config()
 *==============================================================*/
HRESULT s_Config::Read_Config(const WCHAR conf_filename[MAX_PATH], bool reset)
{
	char char_conf_filename[MAX_PATH];
	NNN::Text::wchar2char(conf_filename, char_conf_filename, _countof(char_conf_filename), nullptr);

	if(!NNN::IO::File::exists(conf_filename))
	{
		Log::ShowError(	"File '" NNN_CL_VALUE "%s"
						NNN_CL_RESET "' not found! [%s : %s() - line %d]\n",
						char_conf_filename,
						__FILE__, __FUNCTION__, __LINE__ );
		return HRESULT_FROM_WIN32(ERROR_FILE_NOT_FOUND);
	}

	HRESULT hr;

	struct NNN::Config::s_Config_List config_list;
	V( NNN::Config::Read_Config(conf_filename, config_list) );
	if(FAILED(hr))
	{
		Log::ShowError(	"Read '" NNN_CL_VALUE "%s"
						NNN_CL_RESET "' failed! [%s : %s() - line %d]\n",
						char_conf_filename,
						__FILE__, __FUNCTION__, __LINE__ );
		return hr;
	}

	// 正在读取
	Log::ShowStatus("Reading '" NNN_CL_VALUE "%s" NNN_CL_RESET "' ...\n", char_conf_filename);

	// 重置
	if(reset)
		Reset();

	for(UINT i=0; i<config_list.m_count; ++i)
	{
		const WCHAR *w1	= config_list.m_config_name_list[i];
		const WCHAR *w2	= config_list.m_config_value_list[i];

		//============================================================
		// 一般设置
		//============================================================

		// 服务端繁忙的判定时间（ms）
		if(!_wcsicmp(w1, L"busy_time"))
		{
			m_busy_time = NNN::Text::ToUINT(w2);
			continue;
		}

		//============================================================
		// 服务器设置
		//============================================================

		// 服务器端口
		if(!_wcsicmp(w1, L"socket.port"))
		{
			SOCKET.m_port = NNN::Text::ToUSHORT(w2);
			continue;
		}

		// 多久没有数据就开始 send 心跳包（单位：秒；默认：10）
		if(!_wcsicmp(w1, L"socket.keepalive_idle"))
		{
			SOCKET.m_keepalive_idle = NNN::Text::ToInt(w2);
			continue;
		}

		//============================================================
		// 安全登录设置
		//============================================================

		// 登录时验证的用户名/密码
		if(!_wcsicmp(w1, L"login.user"))
		{
			LOGIN.m_user = w2;
			continue;
		}
		if(!_wcsicmp(w1, L"login.password"))
		{
			LOGIN.m_password = w2;
			continue;
		}

		// 登录超时（秒）
		if(!_wcsicmp(w1, L"login.timeout"))
		{
			LOGIN.m_timeout = NNN::Text::ToUINT(w2);
			continue;
		}

		//============================================================
		// 日志记录
		//============================================================

		// 日志文件名
		if(!_wcsicmp(w1, L"log.filename"))
		{
			NNN::Text::wchar2char(w2, LOG.m_filename, _countof(LOG.m_filename), nullptr);
			continue;
		}

		// 不输出的控制台信息
		if(!_wcsicmp(w1, L"log.console_silent_flag"))
		{
			LOG.m_console_silent_flag = NNN::Text::ToUINT(w2);
			continue;
		}

		// 不写入文件的消息
		if(!_wcsicmp(w1, L"log.write_file_silent_flag"))
		{
			LOG.m_write_file_silent_flag = NNN::Text::ToUINT(w2);
			continue;
		}

		// 写入日志文件的周期（ms），0 = 立即写入
		if(!_wcsicmp(w1, L"log.write_file_interval"))
		{
			LOG.m_write_file_interval = NNN::Text::ToUINT(w2);
			continue;
		}

		// 每条显示信息的时间戳格式
		if(!_wcsicmp(w1, L"log.timestamp_format"))
		{
			NNN::Text::wchar2char(w2, LOG.m_timestamp_format, _countof(LOG.m_timestamp_format), nullptr);
			continue;
		}

		// 是否显示客户端连接/断开信息
		if(!_wcsicmp(w1, L"log.show_accept_client"))
		{
			LOG.m_show_accept_client = NNN::Text::ToBoolean(w2);
			continue;
		}
		if(!_wcsicmp(w1, L"log.show_disconnect_client"))
		{
			LOG.m_show_disconnect_client = NNN::Text::ToBoolean(w2);
			continue;
		}

		// 是否显示登录成功/失败的信息
		if(!_wcsicmp(w1, L"log.show_login_result"))
		{
			LOG.m_show_login_result = NNN::Text::ToBoolean(w2);
			continue;
		}

		//============================================================
		// import
		//============================================================
		if(!_wcsicmp(w1, L"import"))
		{
			Read_Config(w2, false);
			continue;
		}

		//============================================================
		// 未知选项
		//============================================================

		char char_w1[1024];
		NNN::Text::wchar2char(w1, char_w1, _countof(char_w1), nullptr);

		Log::ShowWarning(	"Unknown option '" NNN_CL_VALUE "%s"
							NNN_CL_RESET "' in file '" NNN_CL_VALUE "%s"
							NNN_CL_RESET "' [%s : %s() - line %d]\n",
							char_w1, char_conf_filename,
							__FILE__, __FUNCTION__, __LINE__ );
	}

	// 读取成功
	Log::ShowStatus("Read '" NNN_CL_VALUE "%s" NNN_CL_RESET "' done.\n", char_conf_filename);

	return S_OK;
}

}	// namespace Config
}	// namespace DDNS_Server
