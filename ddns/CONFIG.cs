using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ddns
{
	internal class CONFIG
	{
		const string			m_k_CONFIG_FILE	= "config.txt";	// 配置文件的文件名
		internal static bool	m_s_dirty		= false;		// 设置是否已改变

		enum e_Config_Header
		{
			//【更新方式】
			update_type,			// 更新方式（0 = 本地、1 = Server）

			//【Server 设置】
			Server_Addr,			// Server 地址/端口
			Server_User,			// 登录到 Server 的用户名
			Server_Pwd,				// 登录到 Server 的密码
			Show_Server_Pwd,		// 显示服务器密码

			//【IP 设置】
			IP_get_type,			// IP 获取方式（1 = 通过互联网获取公网 IP
									//				2 = 手动设置 IP
									//				3 = Server 接受连接的客户端 IP）

			get_IP_URL,				// 检查公网 IP 的 URL
			Last_IP,				// 上次的 IP

			//【安全设置】
			Key,
			Show_Key,
			Secret,
			Show_Secret,
			save_Key_Secret,		// 保存 Key/Secret 到配置文件中

			//【域名列表】
			domain,					// 域名

			//【更新】
			auto_update,			// 是否自动更新
			auto_update_interval,	// 自动更新时间间隔（秒）
			Update_Force,			// 强制更新（即使上次成功更新的 IP 跟当前的 IP 一样）
		};

		internal class c_Domain
		{
			internal string	m_name			= "";
			internal string	m_root_domain	= "";
			internal int	m_TTL			= 0;	// <=0 表示省略
			internal string	m_last_ip		= "";
		};

		internal enum e_Update_Type
		{
			Local,
			Server,
			MAX,
		};

		internal enum e_IP_Get_Type
		{
			Get_IP_From_URL,
			Specific_IP,
			Server_Accept_IP,
			MAX,
		};

		//【更新方式】
		internal static e_Update_Type	m_s_update_type		= e_Update_Type.Local;

		//【Server 设置】
		internal static string			m_s_server_addr			= "";		// server 地址
		internal static string			m_s_server_user			= "";		// server 用户名
		internal static string			m_s_server_pwd			= "";		// server 密码
		internal static bool			m_s_show_server_pwd		= false;	// 显示 server 密码

		//【IP 设置】
		internal static e_IP_Get_Type	m_s_IP_get_type			= e_IP_Get_Type.Get_IP_From_URL;
		internal static string			m_s_Get_IP_URL			= "";		// 检查公网 IP 的 URL
		internal static string			m_s_Last_IP				= "";		// 上次的 IP

		//【安全设置】
		internal static string			m_s_Key					= "";		// Key
		internal static bool			m_s_show_Key			= false;	// 显示 Key
		internal static string			m_s_Secret				= "";		// Secret
		internal static bool			m_s_show_Secret			= false;	// 显示 Secret
		internal static bool			m_s_save_Key_Secret		= false;	// 保存 Key/Secret 到配置文件中

		//【域名列表】
		internal static List<c_Domain>	m_s_domain_list			= new List<c_Domain>();

		//【更新】
		internal static bool			m_s_AutoUpdate			= true;		// 是否自动更新
		internal static uint			m_s_AutoUpdate_Interval	= 600;		// 自动更新时间间隔（秒）
		internal static bool			m_s_Update_Force		= false;	// 强制更新


		/*==============================================================
		 * 读取配置文件
		 *==============================================================*/
		internal static void read_config()
		{
			if(!File.Exists(m_k_CONFIG_FILE))
				return;

			m_s_domain_list.Clear();

			string[] lines = File.ReadAllLines(m_k_CONFIG_FILE, Encoding.UTF8);

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

				//【更新方式】

				if(string.Compare(w1, e_Config_Header.update_type.ToString(), true) == 0)
				{
					int val;

					if(int.TryParse(w2, out val) && val >= 0 && val < (int)e_Update_Type.MAX)
						m_s_update_type = (e_Update_Type)val;

					continue;
				}

				//【Server 设置】

				if(string.Compare(w1, e_Config_Header.Server_Addr.ToString(), true) == 0)
				{
					m_s_server_addr = w2;
					continue;
				}

				if(string.Compare(w1, e_Config_Header.Server_User.ToString(), true) == 0)
				{
					m_s_server_user = w2;
					continue;
				}

				if(string.Compare(w1, e_Config_Header.Server_Pwd.ToString(), true) == 0)
				{
					m_s_server_pwd = w2;
					continue;
				}

				if(string.Compare(w1, e_Config_Header.Show_Server_Pwd.ToString(), true) == 0)
				{
					bool val;

					if(bool.TryParse(w2, out val))
						m_s_show_server_pwd = val;

					continue;
				}

				//【IP 设置】

				if(string.Compare(w1, e_Config_Header.IP_get_type.ToString(), true) == 0)
				{
					int val;

					if(int.TryParse(w2, out val) && val >= 0 && val < (int)e_IP_Get_Type.MAX)
						m_s_IP_get_type = (e_IP_Get_Type)val;

					continue;
				}

				if(string.Compare(w1, e_Config_Header.get_IP_URL.ToString(), true) == 0)
				{
					m_s_Get_IP_URL = w2;
					continue;
				}

				if(string.Compare(w1, e_Config_Header.Last_IP.ToString(), true) == 0)
				{
					m_s_Last_IP = w2;
					continue;
				}

				//【安全设置】

				if(string.Compare(w1, e_Config_Header.Key.ToString(), true) == 0)
				{
					m_s_Key = w2;
					continue;
				}

				if(string.Compare(w1, e_Config_Header.Show_Key.ToString(), true) == 0)
				{
					bool val;

					if(bool.TryParse(w2, out val))
						m_s_show_Key = val;

					continue;
				}

				if(string.Compare(w1, e_Config_Header.Secret.ToString(), true) == 0)
				{
					m_s_Secret = w2;
					continue;
				}

				if(string.Compare(w1, e_Config_Header.Show_Secret.ToString(), true) == 0)
				{
					bool val;

					if(bool.TryParse(w2, out val))
						m_s_show_Secret = val;

					continue;
				}

				if(string.Compare(w1, e_Config_Header.save_Key_Secret.ToString(), true) == 0)
				{
					bool val;

					if(bool.TryParse(w2, out val))
						m_s_save_Key_Secret = val;

					continue;
				}

				//【域名列表】

				if(string.Compare(w1, e_Config_Header.domain.ToString(), true) == 0)
				{
					// <name>,<domain>,<TTL>,<last_ip>
					string[] vals = w2.Split(',');

					if(vals.Length < 4)
						continue;

					c_Domain new_domain = new c_Domain();

					new_domain.m_name			= vals[0].Trim();
					new_domain.m_root_domain	= vals[1].Trim();
					new_domain.m_TTL			= int.Parse(vals[2]);
					new_domain.m_last_ip		= vals[3].Trim();

					m_s_domain_list.Add(new_domain);
				}

				//【更新】

				if(string.Compare(w1, e_Config_Header.auto_update.ToString(), true) == 0)
				{
					bool val;

					if(bool.TryParse(w2, out val))
						m_s_AutoUpdate = val;

					continue;
				}

				if(string.Compare(w1, e_Config_Header.auto_update_interval.ToString(), true) == 0)
				{
					uint interval;

					if(uint.TryParse(w2, out interval))
						m_s_AutoUpdate_Interval = interval;

					continue;
				}

				if(string.Compare(w1, e_Config_Header.Update_Force.ToString(), true) == 0)
				{
					bool val;

					if(bool.TryParse(w2, out val))
						m_s_Update_Force = val;

					continue;
				}
			}	// for

			frm_MainForm.m_s_Mainform.add_log($"读取配置文件 {m_k_CONFIG_FILE} 完成", Color.Green);
		}


		/*==============================================================
		 * 写入配置文件
		 *==============================================================*/
		internal static void write_config()
		{
			if(!m_s_dirty)
				return;

			StringBuilder sb = new StringBuilder();

			//【更新方式】

			sb.AppendLine("// 更新方式（0 = 本地、1 = Server）");
			sb.AppendLine($"{e_Config_Header.update_type}: {(int)m_s_update_type}");
			sb.AppendLine();

			//【Server 设置】

			sb.AppendLine("// server 地址");
			sb.AppendLine($"{e_Config_Header.Server_Addr}: {m_s_server_addr}");
			sb.AppendLine();

			sb.AppendLine("// server 用户名");
			sb.AppendLine($"{e_Config_Header.Server_User}: {m_s_server_user}");
			sb.AppendLine();

			sb.AppendLine("// server 密码");
			sb.AppendLine($"{e_Config_Header.Server_Pwd}: {m_s_server_pwd}");
			sb.AppendLine();

			sb.AppendLine("// 显示 server 密码");
			sb.AppendLine($"{e_Config_Header.Show_Server_Pwd}: {m_s_show_server_pwd}");
			sb.AppendLine();

			//【IP 设置】
			sb.AppendLine("// IP 获取方式（1 = 通过互联网获取公网 IP、2 = 手动设置 IP、3 = Server 接受连接的客户端 IP）");
			sb.AppendLine($"{e_Config_Header.IP_get_type}: {(int)m_s_IP_get_type}");
			sb.AppendLine();

			sb.AppendLine("// 检查公网 IP 的 URL");
			sb.AppendLine($"{e_Config_Header.get_IP_URL}: {m_s_Get_IP_URL}");
			sb.AppendLine();

			sb.AppendLine("// 上次的 IP");
			sb.AppendLine($"{e_Config_Header.Last_IP}: {m_s_Last_IP}");
			sb.AppendLine();

			//【安全设置】

			if(m_s_save_Key_Secret)
			{
				sb.AppendLine("// Key");
				sb.AppendLine($"{e_Config_Header.Key}: {m_s_Key}");
				sb.AppendLine();

				sb.AppendLine("// Secret");
				sb.AppendLine($"{e_Config_Header.Secret}: {m_s_Secret}");
				sb.AppendLine();
			}

			sb.AppendLine("// 显示 Key");
			sb.AppendLine($"{e_Config_Header.Show_Key}: {m_s_show_Key}");
			sb.AppendLine();

			sb.AppendLine("// 显示 Secret");
			sb.AppendLine($"{e_Config_Header.Show_Secret}: {m_s_show_Secret}");
			sb.AppendLine();

			sb.AppendLine("// 保存 Key/Secret 到配置文件中");
			sb.AppendLine($"{e_Config_Header.save_Key_Secret}: {m_s_save_Key_Secret}");
			sb.AppendLine();

			//【更新】

			sb.AppendLine("// 是否自动更新");
			sb.AppendLine($"{e_Config_Header.auto_update}: {m_s_AutoUpdate}");
			sb.AppendLine();

			sb.AppendLine("// 自动更新时间间隔（秒）");
			sb.AppendLine($"{e_Config_Header.auto_update_interval}: {m_s_AutoUpdate_Interval}");
			sb.AppendLine();

			sb.AppendLine("// 强制更新");
			sb.AppendLine($"{e_Config_Header.Update_Force}: {m_s_Update_Force}");
			sb.AppendLine();

			//【域名列表】

			// <name>,<domain>,<TTL>,<last_ip>
			sb.AppendLine("// 域名列表");
			foreach(c_Domain domain in m_s_domain_list)
				sb.AppendLine($"{e_Config_Header.domain}: {domain.m_name},{domain.m_root_domain},{domain.m_TTL},{domain.m_last_ip}");

			File.WriteAllText(m_k_CONFIG_FILE, sb.ToString(), Encoding.UTF8);

			frm_MainForm.m_s_Mainform.add_log($"保存配置文件 {m_k_CONFIG_FILE}", Color.Blue);

			m_s_dirty = false;
		}


		/*==============================================================
		 * 查找设定的域名记录
		 *==============================================================*/
		internal static c_Domain find_domain(string name, string root_domain)
		{
			foreach(c_Domain domain in m_s_domain_list)
			{
				if(	string.Compare(domain.m_name, name, true) == 0	&&
					string.Compare(domain.m_root_domain, root_domain, true) == 0 )
					return domain;
			}	// for

			return null;
		}
	};
}	// namespace ddns
