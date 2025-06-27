using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ddns_tool
{
	internal class CONFIG
	{
		const string			m_k_CONFIG_FILE		= "Files\\config.txt";	// 配置文件的文件名
		const string			m_k_CONFIG_FILE_TMP	= "Files\\config.tmp";	// 配置文件的文件名（临时）
		internal static bool	m_s_dirty			= false;				// 设置是否已改变

		enum e_Header
		{
			//【更新方式】
			update_type,						// 更新方式（0 = 本地、1 = Server）

			//【远程 Server 设置】
			RemoteServer_Addr,					// 地址/端口
			RemoteServer_User,					// 登录到 Server 的用户名
			RemoteServer_Pwd,					// 登录到 Server 的密码
			RemoteServer_ShowPwd,				// 显示密码
			RemoteServer_AutoPing,				// 自动 ping 服务器

			//【设置 IP】
			SetIP_type_IPv4,					// IPv4 获取方式（0 = 通过 URL 获取公网 IP、1 = 手动设置 IP、2 = Server 接受连接的客户端 IP）
			SetIP_get_ipv4_URL,					// 检查公网 IPv4 的 URL
			SetIP_IPv4,							// 设置的 IPv4/上次的 IPv4

			SetIP_type_IPv6,					// IPv6 获取方式（0 = 通过 URL 获取公网 IP、1 = 手动设置 IP、2 = Server 接受连接的客户端 IP）
			SetIP_get_ipv6_URL,					// 检查公网 IPv6 的 URL
			SetIP_IPv6,							// 设置的 IPv6/上次的 IPv6

			//【安全设置】
			Security_Profile,					// [Header]
			Security_Profile__Name,
			Security_Profile__SaveToConfig,		// 保存安全信息到 Config 文件

			Security_Profile__Godaddy_Key,
			Security_Profile__Godaddy_Key_Visible,
			Security_Profile__Godaddy_Secret,
			Security_Profile__Godaddy_Secret_Visible,

			Security_Profile__dynv6_token,
			Security_Profile__dynv6_token_Visible,

			Security_Profile__dynu_API_Key,
			Security_Profile__dynu_API_Key_Visible,

			//【更新操作】
			UpdateAction_UpdateIP,				// 更新域名的 IP
			UpdateAction_AutoAction,			// 自动执行操作
			UpdateAction_AutoAction_interval,	// 自动执行操作的时间间隔（秒）
			UpdateAction_DNS_Lookup_First,		// 先解析域名
			UpdateAction_Use_Custom_DNS,		// 是否使用自定义 DNS 服务器
			UpdateAction_Custom_DNS_List,		// 自定义 DNS 服务器列表
			UpdateAction_Timeout,				// 自动更新超时（单位：秒。0 = 无限等待）
			UpdateAction_IP_Change_Popup,		// IP变动时，弹出提示窗口
			UpdateAction_IP_Change_Play_Sound,	// IP变动时，播放音乐
			UpdateAction_IP_Change_Sound_Path,	// 音乐路径

			//【日志记录】
			Log_MaxLines,						// 日志最大行数
			Log_SaveToFile,						// 保存到日志文件

			//【域名列表】
			Domain,								// [Header]
			Domain__Domain,
			Domain__Type,
			Domain__IPv4_Enabled,
			Domain__IPv6_Enabled,
			Domain__profile_idx,
			Domain__Current_IPv4,
			Domain__Current_IPv6,
			Domain__Godaddy_TTL,
			Domain__dynv6_auto_ipv4,
			Domain__dynv6_auto_ipv6,
			Domain__dynu_ID,
			Domain__dynu_TTL,
		};

		internal enum e_Update_Type
		{
			Local,
			Remote,
			MAX,
		};

		internal enum e_IP_Get_Type
		{
			Get_IP_From_URL,
			Manual_IP,
			Server_Accept_IP,
			MAX,
		};

		//【更新方式】
		internal static e_Update_Type	m_s_update_type	= e_Update_Type.Local;

		//【远程 Server 设置】
		internal class REMOTE_SERVER
		{
			internal static string	m_s_addr		= "";			// 地址/端口
			internal static string	m_s_user		= "";			// 登录到 Server 的用户名
			internal static string	m_s_pwd			= "";			// 登录到 Server 的密码
			internal static bool	m_s_show_pwd	= false;		// 显示密码
			internal static bool	m_s_auto_ping	= false;		// 自动 ping 服务器
		};

		//【设置 IP】
		internal class SET_IP
		{
			internal static e_IP_Get_Type	m_s_type_IPv4		= e_IP_Get_Type.Get_IP_From_URL;
			internal static string			m_s_Get_IPv4_URL	= "";	// 检查公网 IPv4 的 URL
			internal static string			m_s_IPv4			= "";	// 设置的 IPv4/上次的 IPv4

			internal static e_IP_Get_Type	m_s_type_IPv6		= e_IP_Get_Type.Get_IP_From_URL;
			internal static string			m_s_Get_IPv6_URL	= "";	// 检查公网 IPv6 的 URL
			internal static string			m_s_IPv6			= "";	// 设置的 IPv6/上次的 IPv6
		};

		//【安全设置】
		internal class SECURITY
		{
			internal static List<ddns_lib.c_Security_Profile>	m_s_profiles	= new();
		};

		//【更新操作】
		internal class UPDATE_ACTION
		{
			internal static bool			m_s_UpdateIP				= true;		// 更新域名的 IP
			internal static bool			m_s_DNS_Lookup_First		= true;		// 先解析域名
			internal static bool			m_s_Use_Custom_DNS			= true;		// 是否使用自定义 DNS 服务器
			internal static List<string>	m_s_Custom_DNS_List			= new();	// 自定义 DNS 服务器列表
			internal static int				m_s_Timeout					= 15;		// 自动更新超时（单位：秒。0 = 无限等待）
			internal static bool			m_s_IP_Change_Popup			= false;	// IP变动时，弹出提示窗口
			internal static bool			m_s_IP_Change_Play_Sound	= false;	// IP变动时，播放音乐
			internal static string			m_s_IP_Change_Sound_Path	= "Files\\Sound\\FF7CHOCO.MID";	// 音乐路径
		};

		//【日志记录】
		internal class LOG
		{
			internal static int		m_s_MaxLines		= 10000;	// 日志最大行数
			internal static bool	m_s_Save_To_File	= true;		// 保存到日志文件
		};

		//【更新】
		internal class ACTION
		{
			internal static bool	m_s_AutoAction			= true;	// 自动执行操作
			internal static uint	m_s_AutoAction_interval	= 600;	// 自动执行操作的时间间隔（秒）
		};

		//【域名列表】
		internal static List<ddns_lib.c_Domain>	m_s_domains_list	= new();

		/*==============================================================
		 * 查找设定的域名记录
		 *==============================================================*/
		internal static ddns_lib.c_Domain? find_domain(string domain_name)
		{
			foreach(ddns_lib.c_Domain domain in m_s_domains_list)
			{
				if(string.Compare(domain.m_domain, domain_name, true) == 0)
					return domain;
			}	// for

			return null;
		}

		/*==============================================================
		 * 重置「自定义 DNS 服务器」
		 *==============================================================*/
		static void reset_DNS_List()
		{
			UPDATE_ACTION.m_s_Custom_DNS_List.Clear();
			UPDATE_ACTION.m_s_Custom_DNS_List.AddRange(frm_MainForm.m_s_Mainform.textBox_Action_Custom_DNS_List.Lines);
		}

		/*==============================================================
		 * 获取 Server 的 IP/Port
		 *==============================================================*/
		internal static bool get_server_ip_port(out IPAddress? server_ip, out ushort server_port)
		{
			server_ip	= null;
			server_port	= 0;

			int idx = REMOTE_SERVER.m_s_addr.LastIndexOf(":");

			if(idx <= 0)
				return false;

			string str_ip	= REMOTE_SERVER.m_s_addr.Substring(0, idx);
			string str_port	= REMOTE_SERVER.m_s_addr.Substring(idx + 1);

			if(!IPAddress.TryParse(str_ip, out server_ip))
			{
				server_ip = null;
				return false;
			}

			if(!ushort.TryParse(str_port, out server_port))
			{
				server_port = 0;
				return false;
			}

			return true;
		}

		/*==============================================================
		 * 读取配置文件
		 *==============================================================*/
		internal static void read_config()
		{
			string conf_file = m_k_CONFIG_FILE;

			if(!File.Exists(m_k_CONFIG_FILE))
			{
				if(!File.Exists(m_k_CONFIG_FILE_TMP))
				{
					reset_DNS_List();
					return;
				}

				conf_file = m_k_CONFIG_FILE_TMP;
			}

			UPDATE_ACTION.m_s_Custom_DNS_List.Clear();
			m_s_domains_list.Clear();

			SECURITY.m_s_profiles.Clear();

			// c_Domain -> profile_index
			Dictionary<ddns_lib.c_Domain, int> domains_profile_index = new();

			string[] lines = File.ReadAllLines(conf_file, Encoding.UTF8);

			foreach(string line in lines)
			{
				if(line.Length < 2)
					continue;

				if(line[0] == '/' && line[1] == '/')
					continue;

				int idx = line.IndexOf(":");
				if(idx < 0)
					continue;

				string w1 = line.Substring(0, idx).Trim();
				string w2 = line.Substring(idx + 1).Trim();

				//====================【更新方式】====================(Start)
				if(string.Compare(w1, e_Header.update_type.ToString(), true) == 0)
				{
					if(int.TryParse(w2, out int val) && val >= 0 && val < (int)e_Update_Type.MAX)
						m_s_update_type = (e_Update_Type)val;

					continue;
				}
				//====================【更新方式】====================(End)

				//====================【远程 Server 设置】====================(Start)
				// 地址/端口
				if(string.Compare(w1, e_Header.RemoteServer_Addr.ToString(), true) == 0)
				{
					REMOTE_SERVER.m_s_addr = w2;
					continue;
				}

				// 登录到 Server 的用户名
				if(string.Compare(w1, e_Header.RemoteServer_User.ToString(), true) == 0)
				{
					REMOTE_SERVER.m_s_user = w2;
					continue;
				}

				// 登录到 Server 的密码
				if(string.Compare(w1, e_Header.RemoteServer_Pwd.ToString(), true) == 0)
				{
					REMOTE_SERVER.m_s_pwd = w2;
					continue;
				}

				// 显示密码
				if(string.Compare(w1, e_Header.RemoteServer_ShowPwd.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						REMOTE_SERVER.m_s_show_pwd = val;

					continue;
				}

				// 自动 ping 服务器
				if(string.Compare(w1, e_Header.RemoteServer_AutoPing.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						REMOTE_SERVER.m_s_auto_ping = val;

					continue;
				}
				//====================【远程 Server 设置】====================(End)

				//====================【设置 IP】====================(Start)
				// IPv4 获取方式
				if(string.Compare(w1, e_Header.SetIP_type_IPv4.ToString(), true) == 0)
				{
					if(int.TryParse(w2, out int val) && val >= 0 && val < (int)e_IP_Get_Type.MAX)
						SET_IP.m_s_type_IPv4 = (e_IP_Get_Type)val;

					continue;
				}

				// 检查公网 IPv4 的 URL
				if(string.Compare(w1, e_Header.SetIP_get_ipv4_URL.ToString(), true) == 0)
				{
					SET_IP.m_s_Get_IPv4_URL = w2;
					continue;
				}

				// 设置的 IPv4/上次的 IPv4
				if(string.Compare(w1, e_Header.SetIP_IPv4.ToString(), true) == 0)
				{
					SET_IP.m_s_IPv4 = w2;
					continue;
				}

				// IPv6 获取方式
				if(string.Compare(w1, e_Header.SetIP_type_IPv6.ToString(), true) == 0)
				{
					if(int.TryParse(w2, out int val) && val >= 0 && val < (int)e_IP_Get_Type.MAX)
						SET_IP.m_s_type_IPv6 = (e_IP_Get_Type)val;

					continue;
				}

				// 检查公网 IPv6 的 URL
				if(string.Compare(w1, e_Header.SetIP_get_ipv6_URL.ToString(), true) == 0)
				{
					SET_IP.m_s_Get_IPv6_URL = w2;
					continue;
				}

				// 设置的 IPv6/上次的 IPv6
				if(string.Compare(w1, e_Header.SetIP_IPv6.ToString(), true) == 0)
				{
					SET_IP.m_s_IPv6 = w2;
					continue;
				}
				//====================【设置 IP】====================(End)

				//====================【安全设置】====================(Start)
				string HEADER_Security_Profile = e_Header.Security_Profile.ToString();

				// Security_Profile[x].Name
				if(w1.IndexOf(HEADER_Security_Profile, StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					string[] vals = w1.Split('.');

					if(vals.Length == 2)
					{
						int		profile_idx		= int.Parse(vals[0].Replace(HEADER_Security_Profile, "").Replace("[", "").Replace("]", ""));
						string	profile_vars	= vals[1];

						ddns_lib.c_Security_Profile profile;

						if(SECURITY.m_s_profiles.Count < profile_idx + 1)
						{
							profile = new();
							SECURITY.m_s_profiles.Add(profile);
						}
						else
							profile = SECURITY.m_s_profiles[profile_idx];

						HEADER_Security_Profile += "__";

						// Name
						if(string.Compare(profile_vars, e_Header.Security_Profile__Name.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							profile.m_Name = w2;
							continue;
						}

						// SaveToConfig
						if(string.Compare(profile_vars, e_Header.Security_Profile__SaveToConfig.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_Save_To_Config = val;

							continue;
						}

						// [Godaddy] Key
						if(string.Compare(profile_vars, e_Header.Security_Profile__Godaddy_Key.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							profile.m_Godaddy__Key = w2;
							continue;
						}

						// [Godaddy] 显示 Key
						if(string.Compare(profile_vars, e_Header.Security_Profile__Godaddy_Key_Visible.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_Godaddy__Key_Visible = val;

							continue;
						}

						// [Godaddy] Secret
						if(string.Compare(profile_vars, e_Header.Security_Profile__Godaddy_Secret.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							profile.m_Godaddy__Secret = w2;
							continue;
						}

						// [Godaddy] 显示 Secret
						if(string.Compare(profile_vars, e_Header.Security_Profile__Godaddy_Secret_Visible.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_Godaddy__Secret_Visible = val;

							continue;
						}

						// [dynv6] token
						if(string.Compare(profile_vars, e_Header.Security_Profile__dynv6_token.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							profile.m_dynv6__token = w2;
							continue;
						}

						// [dynv6] 显示 token
						if(string.Compare(profile_vars, e_Header.Security_Profile__dynv6_token_Visible.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_dynv6__token_Visible = val;

							continue;
						}

						// [dynu] API_Key
						if(string.Compare(profile_vars, e_Header.Security_Profile__dynu_API_Key.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							profile.m_dynu__API_Key = w2;
							continue;
						}

						// [dynu] 显示 API_Key
						if(string.Compare(profile_vars, e_Header.Security_Profile__dynu_API_Key_Visible.ToString().Replace(HEADER_Security_Profile, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_dynu__API_Key_Visible = val;

							continue;
						}
					}
				}
				//====================【安全设置】====================(End)

				//====================【更新操作】====================(Start)
				// 更新域名的 IP
				if(string.Compare(w1, e_Header.UpdateAction_UpdateIP.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						UPDATE_ACTION.m_s_UpdateIP = val;

					continue;
				}

				// 自动执行操作
				if(string.Compare(w1, e_Header.UpdateAction_AutoAction.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						ACTION.m_s_AutoAction = val;

					continue;
				}

				// 自动执行操作的时间间隔（秒）
				if(string.Compare(w1, e_Header.UpdateAction_AutoAction_interval.ToString(), true) == 0)
				{
					if(uint.TryParse(w2, out uint val))
						ACTION.m_s_AutoAction_interval = val;

					continue;
				}

				// 先解析域名
				if(string.Compare(w1, e_Header.UpdateAction_DNS_Lookup_First.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						UPDATE_ACTION.m_s_DNS_Lookup_First = val;

					continue;
				}

				// 是否使用自定义 DNS 服务器
				if(string.Compare(w1, e_Header.UpdateAction_Use_Custom_DNS.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						UPDATE_ACTION.m_s_Use_Custom_DNS = val;

					continue;
				}

				// 自定义 DNS 服务器列表
				if(string.Compare(w1, e_Header.UpdateAction_Custom_DNS_List.ToString(), true) == 0)
				{
					UPDATE_ACTION.m_s_Custom_DNS_List.Add(w2);
					continue;
				}

				// 自动更新超时（单位：秒。0 = 无限等待）
				if(string.Compare(w1, e_Header.UpdateAction_Timeout.ToString(), true) == 0)
				{
					if(int.TryParse(w2, out int val))
						UPDATE_ACTION.m_s_Timeout = val;

					continue;
				}

				// IP变动时，弹出提示窗口
				if(string.Compare(w1, e_Header.UpdateAction_IP_Change_Popup.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						UPDATE_ACTION.m_s_IP_Change_Popup = val;

					continue;
				}

				// IP变动时，播放音乐
				if(string.Compare(w1, e_Header.UpdateAction_IP_Change_Play_Sound.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						UPDATE_ACTION.m_s_IP_Change_Play_Sound = val;

					continue;
				}

				// 音乐路径
				if(string.Compare(w1, e_Header.UpdateAction_IP_Change_Sound_Path.ToString(), true) == 0)
				{
					UPDATE_ACTION.m_s_IP_Change_Sound_Path = w2;
					continue;
				}
				//====================【更新操作】====================(End)

				//====================【日志记录】====================(Start)
				// 日志最大行数
				if(string.Compare(w1, e_Header.Log_MaxLines.ToString(), true) == 0)
				{
					if(int.TryParse(w2, out int val))
						LOG.m_s_MaxLines = val;

					continue;
				}

				// 保存到日志文件
				if(string.Compare(w1, e_Header.Log_SaveToFile.ToString(), true) == 0)
				{
					if(bool.TryParse(w2, out bool val))
						LOG.m_s_Save_To_File = val;

					continue;
				}
				//====================【日志记录】====================(End)

				//【域名列表】
				string HEADER_Domain = e_Header.Domain.ToString();

				// Domain[x].Domain
				if(w1.IndexOf(HEADER_Domain, StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					string[] vals = w1.Split('.');

					if(vals.Length == 2)
					{
						int		domain_idx	= int.Parse(vals[0].Replace(HEADER_Domain, "").Replace("[", "").Replace("]", ""));
						string	domain_vars	= vals[1];

						ddns_lib.c_Domain domain;

						if(m_s_domains_list.Count < domain_idx + 1)
						{
							domain = new();
							m_s_domains_list.Add(domain);
						}
						else
							domain = m_s_domains_list[domain_idx];

						HEADER_Domain += "__";

						// Domain
						if(string.Compare(domain_vars, e_Header.Domain__Domain.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							domain.m_domain = w2;
							continue;
						}

						// Type
						if(string.Compare(domain_vars, e_Header.Domain__Type.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							domain.m_type = ddns_lib.c_Domain.string_to_type(w2);
							continue;
						}

						// IPv4_enabled
						if(string.Compare(domain_vars, e_Header.Domain__IPv4_Enabled.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								domain.IPv4.m_enabled = val;

							continue;
						}

						// IPv6_enabled
						if(string.Compare(domain_vars, e_Header.Domain__IPv6_Enabled.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								domain.IPv6.m_enabled = val;

							continue;
						}

						// profile_idx
						if(string.Compare(domain_vars, e_Header.Domain__profile_idx.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							if(int.TryParse(w2, out int profile_idx))
								domains_profile_index.Add(domain, profile_idx);

							continue;
						}

						// Current_IPv4
						if(string.Compare(domain_vars, e_Header.Domain__Current_IPv4.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							domain.IPv4.m_current_IP = w2;
							continue;
						}

						// Current_IPv6
						if(string.Compare(domain_vars, e_Header.Domain__Current_IPv6.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							domain.IPv6.m_current_IP = w2;
							continue;
						}

						// Godaddy_TTL
						if(string.Compare(domain_vars, e_Header.Domain__Godaddy_TTL.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							if(int.TryParse(w2, out int val))
								domain.m_Godaddy__TTL = val;

							continue;
						}

						// dynv6_auto_ipv4
						if(string.Compare(domain_vars, e_Header.Domain__dynv6_auto_ipv4.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								domain.m_dynv6__Auto_IPv4 = val;

							continue;
						}

						// dynv6_auto_ipv6
						if(string.Compare(domain_vars, e_Header.Domain__dynv6_auto_ipv6.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								domain.m_dynv6__Auto_IPv6 = val;

							continue;
						}

						// dynu_ID
						if(string.Compare(domain_vars, e_Header.Domain__dynu_ID.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							if(int.TryParse(w2, out int val))
								domain.m_dynu__ID = val;

							continue;
						}

						// dynu_TTL
						if(string.Compare(domain_vars, e_Header.Domain__dynu_TTL.ToString().Replace(HEADER_Domain, ""), true) == 0)
						{
							if(int.TryParse(w2, out int val))
								domain.m_dynu__TTL = val;

							continue;
						}
					}
				}
			}	// for

			foreach(var kvp in domains_profile_index)
			{
				ddns_lib.c_Domain domain = kvp.Key;

				int profile_idx = kvp.Value;

				if(profile_idx >= 0 && profile_idx < SECURITY.m_s_profiles.Count)
					domain.m_Security_Profile = SECURITY.m_s_profiles[profile_idx];
			}	// for

			// 100: 读取配置文件 {0:s} 完成
			frm_MainForm.m_s_Mainform.add_log(string.Format(ddns_lib.LANGUAGES.txt(100), conf_file), Color.Green);
		}

		/*==============================================================
		 * 保存配置文件
		 *==============================================================*/
		internal static void save_config()
		{
			if(!m_s_dirty)
				return;

			StringBuilder sb = new();

			//====================【更新方式】====================(Start)
			sb.Append($"// {ddns_lib.LANGUAGES.txt(101)}");	// 101: 更新方式（
			for(int i=0; i<(int)e_Update_Type.MAX; ++i)
			{
				sb.Append($"{i} = {(e_Update_Type)i}");

				if(i == (int)e_Update_Type.MAX - 1)
					sb.AppendLine(ddns_lib.LANGUAGES.txt(103));	// 103: ）
				else
					sb.Append(ddns_lib.LANGUAGES.txt(102));	// 102: 、
			}	// for

			sb.AppendLine($"{e_Header.update_type}: {(int)m_s_update_type}");
			sb.AppendLine();
			//====================【更新方式】====================(End)

			//====================【远程 Server 设置】====================(Start)
			// 地址/端口
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(104)}");	// 104: server 地址/端口
			sb.AppendLine($"{e_Header.RemoteServer_Addr}: {REMOTE_SERVER.m_s_addr}");
			sb.AppendLine();

			// 登录到 Server 的用户名
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(105)}");	// 105: 登录到 Server 的用户名
			sb.AppendLine($"{e_Header.RemoteServer_User}: {REMOTE_SERVER.m_s_user}");
			sb.AppendLine();

			// 登录到 Server 的密码
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(106)}");	// 106: 登录到 Server 的密码
			sb.AppendLine($"{e_Header.RemoteServer_Pwd}: {REMOTE_SERVER.m_s_pwd}");
			sb.AppendLine();

			// 显示密码
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(107)}");	// 107: 显示密码
			sb.AppendLine($"{e_Header.RemoteServer_ShowPwd}: {REMOTE_SERVER.m_s_show_pwd}");
			sb.AppendLine();

			// 自动 ping 服务器
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(108)}");	// 108: 自动 ping 服务器
			sb.AppendLine($"{e_Header.RemoteServer_AutoPing}: {REMOTE_SERVER.m_s_auto_ping}");
			sb.AppendLine();
			//====================【远程 Server 设置】====================(End)

			//====================【设置 IP】====================(Start)
			// IPv4 获取方式
			// 109: IP{0:s} 获取方式
			// 112: （0 = 通过 URL 获取公网 IP、1 = 手动设置 IP、2 = Server 接受连接的客户端 IP）
			sb.AppendLine($"// {string.Format(ddns_lib.LANGUAGES.txt(109), "v4")}{ddns_lib.LANGUAGES.txt(112)}");
			sb.AppendLine($"{e_Header.SetIP_type_IPv4}: {(int)SET_IP.m_s_type_IPv4}");
			sb.AppendLine();

			// 检查公网 IPv4 的 URL
			sb.AppendLine("// " + string.Format(ddns_lib.LANGUAGES.txt(110), "v4"));	// 110: 检查公网 IP{0:s} 的 URL
			sb.AppendLine($"{e_Header.SetIP_get_ipv4_URL}: {SET_IP.m_s_Get_IPv4_URL}");
			sb.AppendLine();

			// 设置的 IPv4/上次的 IPv4
			sb.AppendLine("// " + string.Format(ddns_lib.LANGUAGES.txt(111), "v4"));	// 111: 设置的 IP{0:s}/上次的 IP{0:s}
			sb.AppendLine($"{e_Header.SetIP_IPv4}: {SET_IP.m_s_IPv4}");
			sb.AppendLine();

			// IPv6 获取方式
			// 109: IP{0:s} 获取方式
			// 112: （0 = 通过 URL 获取公网 IP、1 = 手动设置 IP、2 = Server 接受连接的客户端 IP）
			sb.AppendLine($"// {string.Format(ddns_lib.LANGUAGES.txt(109), "v6")}{ddns_lib.LANGUAGES.txt(112)}");
			sb.AppendLine($"{e_Header.SetIP_type_IPv6}: {(int)SET_IP.m_s_type_IPv6}");
			sb.AppendLine();

			// 检查公网 IPv6 的 URL
			sb.AppendLine("// " + string.Format(ddns_lib.LANGUAGES.txt(110), "v6"));	// 110: 检查公网 IP{0:s} 的 URL
			sb.AppendLine($"{e_Header.SetIP_get_ipv6_URL}: {SET_IP.m_s_Get_IPv6_URL}");
			sb.AppendLine();

			// 设置的 IPv6/上次的 IPv6
			sb.AppendLine("// " + string.Format(ddns_lib.LANGUAGES.txt(111), "v6"));	// 111: 设置的 IP{0:s}/上次的 IP{0:s}
			sb.AppendLine($"{e_Header.SetIP_IPv6}: {SET_IP.m_s_IPv6}");
			sb.AppendLine();
			//====================【设置 IP】====================(End)

			//====================【安全设置】====================(Start)
			for(int i=0; i<SECURITY.m_s_profiles.Count; ++i)
			{
				ddns_lib.c_Security_Profile profile = SECURITY.m_s_profiles[i];

				if(!profile.m_Save_To_Config)
					continue;

				// 113: 安全 Profile
				sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(113)}[{i}] - {profile.m_Name}");

				// Name
				sb.AppendLine($"{e_Header.Security_Profile__Name.ToString().Replace("__", $"[{i}].")}: {profile.m_Name}");

				// SaveToConfig
				sb.AppendLine($"{e_Header.Security_Profile__SaveToConfig.ToString().Replace("__", $"[{i}].")}: {profile.m_Save_To_Config}");

				// [Godaddy] Key
				sb.AppendLine($"{e_Header.Security_Profile__Godaddy_Key.ToString().Replace("__", $"[{i}].")}: {profile.m_Godaddy__Key}");

				// [Godaddy] 显示 Key
				sb.AppendLine($"{e_Header.Security_Profile__Godaddy_Key_Visible.ToString().Replace("__", $"[{i}].")}: {profile.m_Godaddy__Key_Visible}");

				// [Godaddy] Secret
				sb.AppendLine($"{e_Header.Security_Profile__Godaddy_Secret.ToString().Replace("__", $"[{i}].")}: {profile.m_Godaddy__Secret}");

				// [Godaddy] 显示 Secret
				sb.AppendLine($"{e_Header.Security_Profile__Godaddy_Secret_Visible.ToString().Replace("__", $"[{i}].")}: {profile.m_Godaddy__Secret_Visible}");

				// [dynv6] token
				sb.AppendLine($"{e_Header.Security_Profile__dynv6_token.ToString().Replace("__", $"[{i}].")}: {profile.m_dynv6__token}");

				// [dynv6] 显示 token
				sb.AppendLine($"{e_Header.Security_Profile__dynv6_token_Visible.ToString().Replace("__", $"[{i}].")}: {profile.m_dynv6__token_Visible}");

				// [dynu] API_Key
				sb.AppendLine($"{e_Header.Security_Profile__dynu_API_Key.ToString().Replace("__", $"[{i}].")}: {profile.m_dynu__API_Key}");

				// [dynu] 显示 API_Key
				sb.AppendLine($"{e_Header.Security_Profile__dynu_API_Key_Visible.ToString().Replace("__", $"[{i}].")}: {profile.m_dynu__API_Key_Visible}");

				sb.AppendLine();
			}	// for
			//====================【安全设置】====================(End)

			//====================【更新操作】====================(Start)
			// 更新域名的 IP
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(114)}");	// 114: 更新域名的 IP
			sb.AppendLine($"{e_Header.UpdateAction_UpdateIP}: {UPDATE_ACTION.m_s_UpdateIP}");
			sb.AppendLine();

			// 自动执行操作
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(115)}");	// 115: 自动执行操作
			sb.AppendLine($"{e_Header.UpdateAction_AutoAction}: {ACTION.m_s_AutoAction}");
			sb.AppendLine();

			// 自动执行操作的时间间隔（秒）
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(116)}");	// 116: 自动执行操作的时间间隔（秒）
			sb.AppendLine($"{e_Header.UpdateAction_AutoAction_interval}: {ACTION.m_s_AutoAction_interval}");
			sb.AppendLine();

			// 先解析域名
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(117)}");	// 117: 先解析域名
			sb.AppendLine($"{e_Header.UpdateAction_DNS_Lookup_First}: {UPDATE_ACTION.m_s_DNS_Lookup_First}");
			sb.AppendLine();

			// 是否使用自定义 DNS 服务器
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(118)}");	// 118: 是否使用自定义 DNS 服务器
			sb.AppendLine($"{e_Header.UpdateAction_Use_Custom_DNS}: {UPDATE_ACTION.m_s_Use_Custom_DNS}");
			sb.AppendLine();

			// 自定义 DNS 服务器列表
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(119)}");	// 119: 自定义 DNS 服务器列表（一行一个。//表示注释。""表示系统默认）
			foreach(string dns_server in UPDATE_ACTION.m_s_Custom_DNS_List)
				sb.AppendLine($"{e_Header.UpdateAction_Custom_DNS_List}: {dns_server}");

			sb.AppendLine();

			// 自动更新超时（单位：秒。0 = 无限等待）
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(120)}");	// 120: 自动更新超时（单位：秒。0 = 无限等待）
			sb.AppendLine($"{e_Header.UpdateAction_Timeout}: {UPDATE_ACTION.m_s_Timeout}");
			sb.AppendLine();

			// IP变动时，弹出提示窗口
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(121)}");	// 121: IP变动时，弹出提示窗口
			sb.AppendLine($"{e_Header.UpdateAction_IP_Change_Popup}: {UPDATE_ACTION.m_s_IP_Change_Popup}");
			sb.AppendLine();

			// IP变动时，播放音乐
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(122)}");	// 122: IP变动时，播放音乐
			sb.AppendLine($"{e_Header.UpdateAction_IP_Change_Play_Sound}: {UPDATE_ACTION.m_s_IP_Change_Play_Sound}");
			sb.AppendLine();

			// 音乐路径
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(123)}");	// 123: 音乐路径
			sb.AppendLine($"{e_Header.UpdateAction_IP_Change_Sound_Path}: {UPDATE_ACTION.m_s_IP_Change_Sound_Path}");
			sb.AppendLine();
			//====================【更新操作】====================(End)

			//====================【日志记录】====================(Start)
			// 日志最大行数
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(124)}");	// 124: 日志最大行数
			sb.AppendLine($"{e_Header.Log_MaxLines}: {LOG.m_s_MaxLines}");
			sb.AppendLine();

			// 保存到日志文件
			sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(125)}");	// 125: 保存到日志文件
			sb.AppendLine($"{e_Header.Log_SaveToFile}: {LOG.m_s_Save_To_File}");
			sb.AppendLine();
			//====================【日志记录】====================(End)

			//====================【域名列表】====================(Start)
			for(int i=0; i<m_s_domains_list.Count; ++i)
			{
				ddns_lib.c_Domain domain = m_s_domains_list[i];

				// 126: Domain
				sb.AppendLine($"// {ddns_lib.LANGUAGES.txt(126)}[{i}] - {domain.m_domain}");

				// Domain
				sb.AppendLine($"{e_Header.Domain__Domain.ToString().Replace("__", $"[{i}].")}: {domain.m_domain}");

				// Type
				sb.AppendLine($"{e_Header.Domain__Type.ToString().Replace("__", $"[{i}].")}: {domain.m_type}");

				// IPv4_Enabled
				sb.AppendLine($"{e_Header.Domain__IPv4_Enabled.ToString().Replace("__", $"[{i}].")}: {domain.IPv4.m_enabled}");

				// IPv6_Enabled
				sb.AppendLine($"{e_Header.Domain__IPv6_Enabled.ToString().Replace("__", $"[{i}].")}: {domain.IPv6.m_enabled}");

				// profile_idx
				sb.AppendLine($"{e_Header.Domain__profile_idx.ToString().Replace("__", $"[{i}].")}: {SECURITY.m_s_profiles.IndexOf(domain.m_Security_Profile!)}");

				// Current_IPv4
				sb.AppendLine($"{e_Header.Domain__Current_IPv4.ToString().Replace("__", $"[{i}].")}: {domain.IPv4.m_current_IP}");

				// Current_IPv6
				sb.AppendLine($"{e_Header.Domain__Current_IPv6.ToString().Replace("__", $"[{i}].")}: {domain.IPv6.m_current_IP}");

				// Godaddy_TTL
				sb.AppendLine($"{e_Header.Domain__Godaddy_TTL.ToString().Replace("__", $"[{i}].")}: {domain.m_Godaddy__TTL}");

				// dynv6_auto_ipv4
				sb.AppendLine($"{e_Header.Domain__dynv6_auto_ipv4.ToString().Replace("__", $"[{i}].")}: {domain.m_dynv6__Auto_IPv4}");

				// dynv6_auto_ipv6
				sb.AppendLine($"{e_Header.Domain__dynv6_auto_ipv6.ToString().Replace("__", $"[{i}].")}: {domain.m_dynv6__Auto_IPv6}");

				// dynu_ID
				sb.AppendLine($"{e_Header.Domain__dynu_ID.ToString().Replace("__", $"[{i}].")}: {domain.m_dynu__ID}");

				// dynu_TTL
				sb.AppendLine($"{e_Header.Domain__dynu_TTL.ToString().Replace("__", $"[{i}].")}: {domain.m_dynu__TTL}");

				sb.AppendLine();
			}	// for

			File.WriteAllText(m_k_CONFIG_FILE_TMP, sb.ToString(), Encoding.UTF8);

			if(File.Exists(m_k_CONFIG_FILE))
				File.Delete(m_k_CONFIG_FILE);

			File.Move(m_k_CONFIG_FILE_TMP, m_k_CONFIG_FILE);

			// 127: 保存配置文件 {0:s}
			frm_MainForm.m_s_Mainform.add_log(string.Format(ddns_lib.LANGUAGES.txt(127), m_k_CONFIG_FILE), Color.Blue);
			//====================【域名列表】====================(End)

			m_s_dirty = false;
		}
	};
}	// namespace ddns_tool
