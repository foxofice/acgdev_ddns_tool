using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ddns_tool
{
	internal class CONFIG
	{
		const string			m_k_CONFIG_FILE		= "config.txt";	// 配置文件的文件名
		const string			m_k_CONFIG_FILE_TMP	= "config.tmp";	// 配置文件的文件名（临时）
		internal static bool	m_s_dirty			= false;		// 设置是否已改变

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
			SetIP_type,							// IP 获取方式（1 = 通过互联网获取公网 IP、2 = 手动指定 IP、3 = Server 接受连接的客户端 IP）
			SetIP_get_ipv4_URL,					// 检查公网 IPv4 的 URL
			SetIP_get_ipv6_URL,					// 检查公网 IPv6 的 URL
			SetIP_IPv4,							// 设置的 IPv4/上次的 IPv4
			SetIP_IPv6,							// 设置的 IPv6/上次的 IPv6

			//【安全设置】
			Security_Profile__Name,
			Security_Profile__SaveToConfig,		// 保存安全信息到 Config 文件

			Security_Profile__Godaddy_Key,
			Security_Profile__Godaddy_Key_Visible,
			Security_Profile__Godaddy_Secret,
			Security_Profile__Godaddy_Secret_Visible,

			Security_Profile__dynv6_token,
			Security_Profile__dynv6_token_Visible,

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
			domain,								// 域名
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
			internal static e_IP_Get_Type	m_s_type			= e_IP_Get_Type.Get_IP_From_URL;
			internal static string			m_s_Get_IPv4_URL	= "";	// 检查公网 IPv4 的 URL
			internal static string			m_s_Get_IPv6_URL	= "";	// 检查公网 IPv6 的 URL
			internal static string			m_s_IPv4			= "";	// 设置的 IPv4/上次的 IPv4
			internal static string			m_s_IPv6			= "";	// 设置的 IPv6/上次的 IPv6
		};

		//【安全设置】
		internal class SECURITY
		{
			internal static List<ddns_lib.c_Security_Profile>	m_s_profiles	= new List<ddns_lib.c_Security_Profile>();
		};

		//【更新操作】
		internal class UPDATE_ACTION
		{
			internal static bool			m_s_UpdateIP				= true;						// 更新域名的 IP
			internal static bool			m_s_DNS_Lookup_First		= true;						// 先解析域名
			internal static bool			m_s_Use_Custom_DNS			= true;						// 是否使用自定义 DNS 服务器
			internal static List<string>	m_s_Custom_DNS_List			= new List<string>();		// 自定义 DNS 服务器列表
			internal static int				m_s_Timeout					= 15;						// 自动更新超时（单位：秒。0 = 无限等待）
			internal static bool			m_s_IP_Change_Popup			= false;					// IP变动时，弹出提示窗口
			internal static bool			m_s_IP_Change_Play_Sound	= false;					// IP变动时，播放音乐
			internal static string			m_s_IP_Change_Sound_Path	= "Sound\\FF7CHOCO.MID";	// 音乐路径
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
		internal static List<ddns_lib.c_Domain>	m_s_domains_list	= new List<ddns_lib.c_Domain>();

		/*==============================================================
		 * 查找设定的域名记录
		 *==============================================================*/
		internal static ddns_lib.c_Domain find_domain(string domain_name)
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

			// "Security_Profile"
			string HEADER_Security_Profile = e_Header.Security_Profile__Name.ToString().Substring(0, e_Header.Security_Profile__Name.ToString().IndexOf("__"));

			// c_Domain -> profile_index
			Dictionary<ddns_lib.c_Domain, int> domains_profile_index = new Dictionary<ddns_lib.c_Domain, int>();

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
				// IP 获取方式
				if(string.Compare(w1, e_Header.SetIP_type.ToString(), true) == 0)
				{
					if(int.TryParse(w2, out int val) && val >= 0 && val < (int)e_IP_Get_Type.MAX)
						SET_IP.m_s_type = (e_IP_Get_Type)val;

					continue;
				}

				// 检查公网 IPv4 的 URL
				if(string.Compare(w1, e_Header.SetIP_get_ipv4_URL.ToString(), true) == 0)
				{
					SET_IP.m_s_Get_IPv4_URL = w2;
					continue;
				}

				// 检查公网 IPv6 的 URL
				if(string.Compare(w1, e_Header.SetIP_get_ipv6_URL.ToString(), true) == 0)
				{
					SET_IP.m_s_Get_IPv6_URL = w2;
					continue;
				}

				// 设置的 IPv4/上次的 IPv4
				if(string.Compare(w1, e_Header.SetIP_IPv4.ToString(), true) == 0)
				{
					SET_IP.m_s_IPv4 = w2;
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
							profile = new ddns_lib.c_Security_Profile();
							SECURITY.m_s_profiles.Add(profile);
						}
						else
							profile = SECURITY.m_s_profiles[profile_idx];

						// Name
						if(string.Compare(profile_vars, e_Header.Security_Profile__Name.ToString().Replace(HEADER_Security_Profile, "").Substring(2), true) == 0)
						{
							profile.m_Name = w2;
							continue;
						}

						// SaveToConfig
						if(string.Compare(profile_vars, e_Header.Security_Profile__SaveToConfig.ToString().Replace(HEADER_Security_Profile, "").Substring(2), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_Save_To_Config = val;

							continue;
						}

						// [Godaddy] Key
						if(string.Compare(profile_vars, e_Header.Security_Profile__Godaddy_Key.ToString().Replace(HEADER_Security_Profile, "").Substring(2), true) == 0)
						{
							profile.m_Godaddy__Key = w2;
							continue;
						}

						// [Godaddy] 显示 Key
						if(string.Compare(profile_vars, e_Header.Security_Profile__Godaddy_Key_Visible.ToString().Replace(HEADER_Security_Profile, "").Substring(2), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_Godaddy__Key_Visible = val;

							continue;
						}

						// [Godaddy] Secret
						if(string.Compare(profile_vars, e_Header.Security_Profile__Godaddy_Secret.ToString().Replace(HEADER_Security_Profile, "").Substring(2), true) == 0)
						{
							profile.m_Godaddy__Secret = w2;
							continue;
						}

						// [Godaddy] 显示 Secret
						if(string.Compare(profile_vars, e_Header.Security_Profile__Godaddy_Secret_Visible.ToString().Replace(HEADER_Security_Profile, "").Substring(2), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_Godaddy__Secret_Visible = val;

							continue;
						}

						// [dynv6] token
						if(string.Compare(profile_vars, e_Header.Security_Profile__dynv6_token.ToString().Replace(HEADER_Security_Profile, "").Substring(2), true) == 0)
						{
							profile.m_dynv6__token = w2;
							continue;
						}

						// [dynv6] 显示 token
						if(string.Compare(profile_vars, e_Header.Security_Profile__dynv6_token_Visible.ToString().Replace(HEADER_Security_Profile, "").Substring(2), true) == 0)
						{
							if(bool.TryParse(w2, out bool val))
								profile.m_dynv6__token_Visible = val;

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
				if(string.Compare(w1, e_Header.domain.ToString(), true) == 0)
				{
					// <domain>,<type>,<enabled>,<current_ipv4>,<current_ipv6>,<TTL>,<auto_ipv4>,<auto_ipv6>,<profile_idx>
					string[] vals = w2.Split(',');

					if(vals.Length < 9)
						continue;

					ddns_lib.c_Domain new_domain = new ddns_lib.c_Domain();

					int col_idx = -1;
					new_domain.m_domain				= vals[++col_idx].Trim();

					if(int.TryParse(vals[++col_idx], out int type) && type >= 0 && type < (int)ddns_lib.e_DomainType.MAX)
						new_domain.m_type = (ddns_lib.e_DomainType)type;
					else
						new_domain.m_type = ddns_lib.e_DomainType.dynv6;

					new_domain.m_enabled			= bool.Parse(vals[++col_idx]);

					new_domain.m_current_IPv4		= vals[++col_idx].Trim();
					new_domain.m_current_IPv6		= vals[++col_idx].Trim();

					new_domain.m_Godaddy__TTL		= uint.Parse(vals[++col_idx]);
					new_domain.m_dynv6__Auto_IPv4	= bool.Parse(vals[++col_idx]);
					new_domain.m_dynv6__Auto_IPv6	= bool.Parse(vals[++col_idx]);

					m_s_domains_list.Add(new_domain);

					int profile_idx = int.Parse(vals[++col_idx]);
					domains_profile_index.Add(new_domain, profile_idx);
				}
			}	// for

			foreach(var kvp in domains_profile_index)
			{
				ddns_lib.c_Domain domain = kvp.Key;

				int profile_idx = kvp.Value;

				if(profile_idx >= 0 && profile_idx < SECURITY.m_s_profiles.Count)
					domain.m_Security_Profile = SECURITY.m_s_profiles[profile_idx];
			}	// for

			frm_MainForm.m_s_Mainform.add_log($"读取配置文件 {conf_file} 完成", Color.Green);
		}

		/*==============================================================
		 * 保存配置文件
		 *==============================================================*/
		internal static void save_config()
		{
			if(!m_s_dirty)
				return;

			StringBuilder sb = new StringBuilder();

			//====================【更新方式】====================(Start)
			sb.Append("// 更新方式（");
			for(int i=0; i<(int)e_Update_Type.MAX; ++i)
			{
				sb.Append($"{i} = {(e_Update_Type)i}");

				if(i == (int)e_Update_Type.MAX - 1)
					sb.AppendLine("）");
				else
					sb.Append("、");
			}	// for

			sb.AppendLine($"{e_Header.update_type}: {(int)m_s_update_type}");
			sb.AppendLine();
			//====================【更新方式】====================(End)

			//====================【远程 Server 设置】====================(Start)
			// 地址/端口
			sb.AppendLine("// server 地址/端口");
			sb.AppendLine($"{e_Header.RemoteServer_Addr}: {REMOTE_SERVER.m_s_addr}");
			sb.AppendLine();

			// 登录到 Server 的用户名
			sb.AppendLine("// 登录到 Server 的用户名");
			sb.AppendLine($"{e_Header.RemoteServer_User}: {REMOTE_SERVER.m_s_user}");
			sb.AppendLine();

			// 登录到 Server 的密码
			sb.AppendLine("// 登录到 Server 的密码");
			sb.AppendLine($"{e_Header.RemoteServer_Pwd}: {REMOTE_SERVER.m_s_pwd}");
			sb.AppendLine();

			// 显示密码
			sb.AppendLine("// 显示密码");
			sb.AppendLine($"{e_Header.RemoteServer_ShowPwd}: {REMOTE_SERVER.m_s_show_pwd}");
			sb.AppendLine();

			// 自动 ping 服务器
			sb.AppendLine("// 自动 ping 服务器");
			sb.AppendLine($"{e_Header.RemoteServer_AutoPing}: {REMOTE_SERVER.m_s_auto_ping}");
			sb.AppendLine();
			//====================【远程 Server 设置】====================(End)

			//====================【设置 IP】====================(Start)
			// IP 获取方式
			sb.AppendLine("// IP 获取方式（0 = 通过互联网获取公网 IP、2 = 手动设置 IP、3 = Server 接受连接的客户端 IP）");
			sb.AppendLine($"{e_Header.SetIP_type}: {(int)SET_IP.m_s_type}");
			sb.AppendLine();

			// 检查公网 IPv4 的 URL
			sb.AppendLine("// 检查公网 IPv4 的 URL");
			sb.AppendLine($"{e_Header.SetIP_get_ipv4_URL}: {SET_IP.m_s_Get_IPv4_URL}");
			sb.AppendLine();

			// 检查公网 IPv6 的 URL
			sb.AppendLine("// 检查公网 IPv6 的 URL");
			sb.AppendLine($"{e_Header.SetIP_get_ipv6_URL}: {SET_IP.m_s_Get_IPv6_URL}");
			sb.AppendLine();

			// 设置的 IPv4/上次的 IPv4
			sb.AppendLine("// 设置的 IPv4/上次的 IPv4");
			sb.AppendLine($"{e_Header.SetIP_IPv4}: {SET_IP.m_s_IPv4}");
			sb.AppendLine();

			// 设置的 IPv6/上次的 IPv6
			sb.AppendLine("// 设置的 IPv6/上次的 IPv6");
			sb.AppendLine($"{e_Header.SetIP_IPv6}: {SET_IP.m_s_IPv6}");
			sb.AppendLine();
			//====================【设置 IP】====================(End)

			//====================【安全设置】====================(Start)
			for(int i=0; i<SECURITY.m_s_profiles.Count; ++i)
			{
				ddns_lib.c_Security_Profile profile = SECURITY.m_s_profiles[i];

				if(!profile.m_Save_To_Config)
					continue;

				sb.AppendLine($"// Security_Profile[{i}] - {profile.m_Name}");

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

				sb.AppendLine();
			}	// for
			//====================【安全设置】====================(End)

			//====================【更新操作】====================(Start)
			// 更新域名的 IP
			sb.AppendLine("// 更新域名的 IP");
			sb.AppendLine($"{e_Header.UpdateAction_UpdateIP}: {UPDATE_ACTION.m_s_UpdateIP}");
			sb.AppendLine();

			// 自动执行操作
			sb.AppendLine("// 自动执行操作");
			sb.AppendLine($"{e_Header.UpdateAction_AutoAction}: {ACTION.m_s_AutoAction}");
			sb.AppendLine();

			// 自动执行操作的时间间隔（秒）
			sb.AppendLine("// 自动执行操作的时间间隔（秒）");
			sb.AppendLine($"{e_Header.UpdateAction_AutoAction_interval}: {ACTION.m_s_AutoAction_interval}");
			sb.AppendLine();

			// 先解析域名
			sb.AppendLine("// 先解析域名");
			sb.AppendLine($"{e_Header.UpdateAction_DNS_Lookup_First}: {UPDATE_ACTION.m_s_DNS_Lookup_First}");
			sb.AppendLine();

			// 是否使用自定义 DNS 服务器
			sb.AppendLine("// 是否使用自定义 DNS 服务器");
			sb.AppendLine($"{e_Header.UpdateAction_Use_Custom_DNS}: {UPDATE_ACTION.m_s_Use_Custom_DNS}");
			sb.AppendLine();

			// 自定义 DNS 服务器列表
			sb.AppendLine("// 自定义 DNS 服务器列表（一行一个。//表示注释。\"\"表示系统默认）");
			foreach(string dns_server in UPDATE_ACTION.m_s_Custom_DNS_List)
				sb.AppendLine($"{e_Header.UpdateAction_Custom_DNS_List}: {dns_server}");

			sb.AppendLine();

			// 自动更新超时（单位：秒。0 = 无限等待）
			sb.AppendLine("// 自动更新超时（单位：秒。0 = 无限等待）");
			sb.AppendLine($"{e_Header.UpdateAction_Timeout}: {UPDATE_ACTION.m_s_Timeout}");
			sb.AppendLine();

			// IP变动时，弹出提示窗口
			sb.AppendLine("// IP变动时，弹出提示窗口");
			sb.AppendLine($"{e_Header.UpdateAction_IP_Change_Popup}: {UPDATE_ACTION.m_s_IP_Change_Popup}");
			sb.AppendLine();

			// IP变动时，播放音乐
			sb.AppendLine("// IP变动时，播放音乐");
			sb.AppendLine($"{e_Header.UpdateAction_IP_Change_Play_Sound}: {UPDATE_ACTION.m_s_IP_Change_Play_Sound}");
			sb.AppendLine();

			// 音乐路径
			sb.AppendLine("// 音乐路径");
			sb.AppendLine($"{e_Header.UpdateAction_IP_Change_Sound_Path}: {UPDATE_ACTION.m_s_IP_Change_Sound_Path}");
			sb.AppendLine();
			//====================【更新操作】====================(End)

			//====================【日志记录】====================(Start)
			// 日志最大行数
			sb.AppendLine("// 日志最大行数");
			sb.AppendLine($"{e_Header.Log_MaxLines}: {LOG.m_s_MaxLines}");
			sb.AppendLine();

			// 保存到日志文件
			sb.AppendLine("// 保存到日志文件");
			sb.AppendLine($"{e_Header.Log_SaveToFile}: {LOG.m_s_Save_To_File}");
			sb.AppendLine();
			//====================【日志记录】====================(End)

			//====================【域名列表】====================(Start)
			sb.AppendLine("// 域名列表（<domain>,<type>,<enabled>,<current_ipv4>,<current_ipv6>,<TTL>,<auto_ipv4>,<auto_ipv6>,<profile_idx>）");

			sb.Append("// <type>：");
			for(int i=0; i<(int)ddns_lib.e_DomainType.MAX; ++i)
			{
				sb.Append($"{i} = {(ddns_lib.e_DomainType)i}");

				if(i == (int)ddns_lib.e_DomainType.MAX - 1)
					sb.AppendLine();
				else
					sb.Append("、");
			}	// for

			foreach(ddns_lib.c_Domain domain in m_s_domains_list)
			{
				string line = string.Format("{0:s}: {1:s},{2:d},{3:s},{4:s},{5:s},{6:d},{7:s},{8:s},{9:d}",
											e_Header.domain.ToString(),
											domain.m_domain,
											(int)domain.m_type,
											domain.m_enabled.ToString(),
											domain.m_current_IPv4,
											domain.m_current_IPv6,
											domain.m_Godaddy__TTL,
											domain.m_dynv6__Auto_IPv4.ToString(),
											domain.m_dynv6__Auto_IPv6.ToString(),
											SECURITY.m_s_profiles.IndexOf(domain.m_Security_Profile));

				sb.AppendLine(line);
			}	// for

			File.WriteAllText(m_k_CONFIG_FILE, sb.ToString(), Encoding.UTF8);

			frm_MainForm.m_s_Mainform.add_log($"保存配置文件 {m_k_CONFIG_FILE}", Color.Blue);
			//====================【域名列表】====================(End)

			m_s_dirty = false;
		}
	};
}	// namespace ddns_tool
