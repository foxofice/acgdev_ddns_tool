using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ddns_lib
{
	// 安全配置
	public class c_Security_Profile
	{
		public string		m_Name						= "";	// 配置的名称

		public string		m_Godaddy__Key				= "";
		public bool			m_Godaddy__Key_Visible		= false;
		public string		m_Godaddy__Secret			= "";
		public bool			m_Godaddy__Secret_Visible	= false;

		public string		m_dynv6__token				= "";
		public bool			m_dynv6__token_Visible		= false;

		public bool			m_Save_To_Config			= true;
	};

	public enum e_DomainType
	{
		Godaddy,
		dynv6,
		MAX,
	};

	public enum e_Progress
	{
		None,
		Starting,	// 开始更新
		DNS_Lookup,	// 正在解析域名
		Updating,	// 正在更新 A/AAAA
		Done,		// 更新完成
		Failed,		// 更新失败
	};

	public class c_Domain
	{
		// 复制数据
		public void	CopyTo(c_Domain domain)
		{
			domain.m_domain				= m_domain;
			domain.m_type				= m_type;
			domain.m_input_IPv4			= m_input_IPv4;
			domain.m_input_IPv6			= m_input_IPv6;
			domain.m_current_IPv4		= m_current_IPv4;
			domain.m_current_IPv6		= m_current_IPv6;

			domain.m_Godaddy__TTL		= m_Godaddy__TTL;

			domain.m_dynv6__Auto_IPv4	= m_dynv6__Auto_IPv4;
			domain.m_dynv6__Auto_IPv6	= m_dynv6__Auto_IPv6;

			domain.m_Security_Profile	= m_Security_Profile;
		}

		// 字符串 -> e_DomainType
		public static e_DomainType	string_to_type(string txt)
		{
			for(int i=0; i<(int)e_DomainType.MAX; ++i)
			{
				if(string.Compare(txt, ((e_DomainType)i).ToString(), true) == 0)
					return (e_DomainType)i;
			}	// for

			return e_DomainType.MAX;
		}

		public string				m_domain			= "";
		public e_DomainType			m_type				= e_DomainType.dynv6;
		public string				m_input_IPv4		= "";	// 使用这个 IPv4 设置 A 记录
		public string				m_input_IPv6		= "";	// 使用这个 IPv6 设置 AAAA 记录
		public string				m_current_IPv4		= "";	// 当前的 IPv4
		public string				m_current_IPv6		= "";	// 当前的 IPv6

		// Godaddy
		public uint					m_Godaddy__TTL		= 0;	// 0 表示省略

		// dynv6
		public bool					m_dynv6__Auto_IPv4	= false;	// 自动设置 IPv4
		public bool					m_dynv6__Auto_IPv6	= false;	// 自动设置 IPv6

		// 安全信息
		public c_Security_Profile	m_Security_Profile	= null;

		// 出错信息
		public string				m_err_msg_IPv4		= "";
		public string				m_err_msg_IPv6		= "";

		public bool					m_enabled			= true;		// 是否执行更新
	};

	public class LIB
	{
		public class EVENTS
		{
			public delegate void e_Add_Log(string txt, Color c);
			public static event e_Add_Log Event_On_AddLog = null;

			public static void Add_Log(string txt, Color c)
			{
				if(Event_On_AddLog != null)
					Event_On_AddLog(txt, c);
			}

			public delegate void e_Set_Progress(string domain, e_Progress progress);
			public static event e_Set_Progress Event_Set_Progress = null;

			public static void Set_Progress(string domain, e_Progress progress)
			{
				if(Event_Set_Progress != null)
					Event_Set_Progress(domain, progress);
			}
		};

		/// <summary>解析域名</summary>
		/// <param name="domain">要解析的域名</param>
		/// <param name="DNS_Server">DNS服务器（"" 表示使用系统设置）</param>
		/// <param name="same_ipv4">IPv4 是否相同（m_input_IPv4 是否跟解析结果相同）</param>
		/// <param name="same_ipv6">IPv6 是否相同（m_input_IPv6 是否跟解析结果相同）</param>
		/// <returns>解析成功返回 true，否则返回 false</returns>
		static bool DNS_Lookup(	c_Domain	domain,
								string		DNS_Server,
								out bool	same_ipv4,
								out bool	same_ipv6 )
		{
			same_ipv4 = false;
			same_ipv6 = false;

			if(DNS_Server.Length == 0)
			{
				try
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(domain.m_domain);

					foreach(IPAddress ip in hostEntry.AddressList)
					{
						switch(ip.AddressFamily)
						{
						case AddressFamily.InterNetwork:
							if(!same_ipv4)	// 只判断第一个 IPv4
							{
								domain.m_current_IPv4 = ip.ToString();

								if(IPAddress.TryParse(domain.m_input_IPv4, out IPAddress input_IPv4) && ip.Equals(input_IPv4))
									same_ipv4 = true;
							}
							break;

						case AddressFamily.InterNetworkV6:
							if(!same_ipv6)	// 只判断第一个 IPv6
							{
								domain.m_current_IPv6 = ip.ToString();

								if(IPAddress.TryParse(domain.m_input_IPv6, out IPAddress input_IPv6) && ip.Equals(input_IPv6))
									same_ipv6 = true;
							}
							break;
						}	// switch
					}	// for
				}
				catch(Exception ex)
				{
					EVENTS.Add_Log($"[Error] {domain.m_domain} : DNS_Lookup failed ({ex.Message})", Color.Red);
					return false;
				}
			}
			else
			{
				ddns_tool_CLR.c_DNS_Lookup_Result result = new ddns_tool_CLR.c_DNS_Lookup_Result();
				bool res = ddns_tool_CLR.CLR.DNS_Lookup(DNS_Server, domain.m_domain, result);

				if(!res)
				{
					EVENTS.Add_Log($"[Error] {domain.m_domain} : DNS_Lookup failed", Color.Red);
					return false;
				}

				if(result.m_ipv4_error_msg.Length > 0)
					EVENTS.Add_Log($"[Warning] {domain.m_domain} : (IPv4){result.m_ipv4_error_msg}", Color.DarkOrange);

				if(result.m_ipv6_error_msg.Length > 0)
					EVENTS.Add_Log($"[Warning] {domain.m_domain} : (IPv6){result.m_ipv6_error_msg}", Color.DarkOrange);

				foreach(string ip in result.m_ipv4_list)
				{
					if(IPAddress.TryParse(ip, out IPAddress ipv4))
					{
						domain.m_current_IPv4 = ip.ToString();

						if(IPAddress.TryParse(domain.m_input_IPv4, out IPAddress input_IPv4) && ipv4.Equals(input_IPv4))
						{
							same_ipv4 = true;
							break;
						}
					}
				}	// for

				foreach(string ip in result.m_ipv6_list)
				{
					if(IPAddress.TryParse(ip, out IPAddress ipv6))
					{
						domain.m_current_IPv6 = ip.ToString();

						if(IPAddress.TryParse(domain.m_input_IPv6, out IPAddress input_IPv6) && ipv6.Equals(input_IPv6))
						{
							same_ipv6 = true;
							break;
						}
					}
				}	// for
			}

			return true;
		}

		/// <summary>更新域名（Godaddy）</summary>
		/// <param name="domain">要更新的域名</param>
		/// <param name="update_ipv6">true = 更新 ipv6、false = 更新 ipv4</param>
		static void update_domain_Godaddy(c_Domain domain, bool update_ipv6)
		{
			string ip = update_ipv6 ? domain.m_input_IPv6 : domain.m_input_IPv4;

			if(ip.Length == 0)
				return;

			if(!IPAddress.TryParse(ip, out IPAddress address))
			{
				EVENTS.Add_Log($"[Error] {domain.m_domain} : 无效的 IP{(update_ipv6 ? "v6" : "v4")}（{ip}）", Color.Red);
				return;
			}

			if(update_ipv6 && address.AddressFamily != AddressFamily.InterNetworkV6)
			{
				EVENTS.Add_Log($"[Error] {domain.m_domain} : 无效的 IPv6（{ip}）", Color.Red);
				return;
			}

			if(address.AddressFamily != AddressFamily.InterNetwork)
			{
				EVENTS.Add_Log($"[Error] {domain.m_domain} : 无效的 IPv4（{ip}）", Color.Red);
				return;
			}

			string record_type = update_ipv6 ? "AAAA" : "A";

			StringBuilder sb = new StringBuilder();

			string name			= domain.m_domain.Substring(0, domain.m_domain.IndexOf('.'));
			string root_donmain	= domain.m_domain.Substring(domain.m_domain.IndexOf('.') + 1);

			sb.Append("[\n");
			sb.Append("	{\n");
			sb.Append($"		\"name\":\"{name}\",\n");
			sb.Append($"		\"type\":\"{record_type}\",\n");
			sb.Append($"		\"data\":\"{ip}\"");

			if(domain.m_Godaddy__TTL > 0)
			{
				sb.Append(",\n");
				sb.Append($"	\"ttl\":{domain.m_Godaddy__TTL}\n");
			}

			sb.Append("	}\n");
			sb.Append("]");

			string			url	= $"https://api.godaddy.com/v1/domains/{root_donmain}/records/{record_type}/{name}";
			StringContent	sc	= new StringContent(sb.ToString(), Encoding.UTF8, "application/json");

			EVENTS.Add_Log($"连接到 {url}", Color.Black);
			EVENTS.Add_Log($"正在更新 {domain} 的「{record_type} 记录」……", Color.Black);

			HttpClient client = new HttpClient();

			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.TryAddWithoutValidation(	"Authorization",
																	$"sso-key {domain.m_Security_Profile.m_Godaddy__Key}:{domain.m_Security_Profile.m_Godaddy__Secret}" );

			try
			{
				HttpResponseMessage response = client.PutAsync(url, sc).GetAwaiter().GetResult();

				if(response.IsSuccessStatusCode)
				{
					if(update_ipv6)
					{
						domain.m_current_IPv6	= domain.m_input_IPv6;
						domain.m_err_msg_IPv6	= "";
					}
					else
					{
						domain.m_current_IPv4	= domain.m_input_IPv4;
						domain.m_err_msg_IPv4	= "";
					}

					EVENTS.Add_Log(	$"{domain.m_domain} : 更新「{record_type} 记录」成功（{ip}）",
									Color.Green );
				}
				else
				{
					string err_msg = response.Content.ReadAsStringAsync().Result;

					if(update_ipv6)
						domain.m_err_msg_IPv6 = err_msg;
					else
						domain.m_err_msg_IPv4 = err_msg;

					EVENTS.Add_Log(	string.Format(	"[Error] {0:s} : 更新「{1:s} 记录」失败（{2:s}）[ip = {3:s}, StatusCode = {4:s}，ReasonPhrase = {5:s}]",
													domain.m_domain,
													record_type,
													err_msg,
													ip,
													response.StatusCode.ToString(),
													response.ReasonPhrase ),
									Color.Red );
				}
			}
			catch(Exception ex)
			{
				string err_msg = ex.InnerException.Message;

				if(update_ipv6)
					domain.m_err_msg_IPv6 = err_msg;
				else
					domain.m_err_msg_IPv4 = err_msg;

				EVENTS.Add_Log(	string.Format(	"[Error] {0:s} : 更新「{1:s}记录」失败（{2:s}）[ip = {3:s}]",
												domain.m_domain,
												record_type,
												err_msg,
												ip ),
								Color.Red );
			}
		}

		/// <summary>更新域名（dynv6）</summary>
		/// <param name="domain">要更新的域名</param>
		/// <param name="update_ipv6">true = 更新 ipv6、false = 更新 ipv4</param>
		static void update_domain_dynv6(c_Domain domain, bool update_ipv6)
		{
			// https://ipv4.dynv6.com/api/update?zone=xxx.dynv6.net&ipv4=auto&token=...
			// https://ipv6.dynv6.com/api/update?zone=xxx.dynv6.net&ipv6=auto&token=...

			WebClient wc = new WebClient();

			string ip = "auto";

			string record_type	= update_ipv6 ? "AAAA" : "A";
			string ip_type		= update_ipv6 ? "ipv6" : "ipv4";

			if(update_ipv6)
			{
				if(!domain.m_dynv6__Auto_IPv6)
					ip = domain.m_input_IPv6;
			}
			else
			{
				if(!domain.m_dynv6__Auto_IPv4)
					ip = domain.m_input_IPv4;
			}

			if(string.Compare(ip, "auto", true) != 0)
			{
				if(!IPAddress.TryParse(ip, out IPAddress address))
				{
					EVENTS.Add_Log($"[Error] {domain.m_domain} : 无效的 IP 格式（{ip}）", Color.Red);
					return;
				}

				if(update_ipv6)
				{
					if(address.AddressFamily != AddressFamily.InterNetworkV6)
					{
						EVENTS.Add_Log($"[Error] {domain.m_domain} : 无效的 IPv6（{ip}）", Color.Red);
						return;
					}
				}
				else
				{
					if(address.AddressFamily != AddressFamily.InterNetwork)
					{
						EVENTS.Add_Log($"[Error] {domain.m_domain} : 无效的 IPv4（{ip}）", Color.Red);
						return;
					}
				}
			}

			string url = string.Format(	"https://{0:s}.dynv6.com/api/update?zone={1:s}&{0:s}={2:s}&token={3:s}",
										ip_type,
										domain.m_domain,
										ip,
										domain.m_Security_Profile.m_dynv6__token );

			try
			{
				string str = wc.DownloadString(url);

				if(string.Compare(ip, "auto", true) != 0)
				{
					if(update_ipv6)
						domain.m_current_IPv6 = ip;
					else
						domain.m_current_IPv4 = ip;
				}

				EVENTS.Add_Log($"{domain.m_domain} ({ip_type}) : 网站返回结果：{str}", Color.Black);
			}
			catch(Exception ex)
			{
				EVENTS.Add_Log(	string.Format(	"[Error] {0:s} ({1:s}) : 更新「{2:s}记录」失败（{3:s}）[ip = {4:s}]",
												domain.m_domain,
												ip_type,
												record_type,
												ex.InnerException.Message,
												ip ),
								Color.Red );
			}
		}

		/*==============================================================
		 * 更新 IP 记录
		 *==============================================================*/
		public static void update_domains(	List<c_Domain>	domains,
											bool			DNS_Lookup_First,		// IP变动时，先解析域名再执行更新
											List<string>	DNS_Server_List	= null,	// 自定义DNS服务器（列表元素为 "" 时，表示系统默认 DNS）
											int				timeout			= 15 * 1000 )
		{
			List<Thread> threads = new List<Thread>();

			for(int i=0; i<domains.Count; ++i)
			{
				if(!domains[i].m_enabled)
					continue;

				Thread th = new Thread((object o) =>
				{
					c_Domain domain = (c_Domain)o;

					bool same_ipv4 = false, same_ipv6 = false;

					// 先解析域名
					if(DNS_Lookup_First && DNS_Server_List != null)
					{
						EVENTS.Set_Progress(domain.m_domain, e_Progress.DNS_Lookup);

						bool ok = false;

						for(int m=0; m<DNS_Server_List.Count; ++m)
						{
							string dns_server = DNS_Server_List[m];

							if(dns_server == "\"\"")
								dns_server = "";

							if(DNS_Lookup(domain, dns_server, out same_ipv4, out same_ipv6))
							{
								ok = true;
								break;
							}
						}	// for

						if(!ok)
						{
							EVENTS.Set_Progress(domain.m_domain, e_Progress.None);
							return;
						}
					}

					EVENTS.Set_Progress(domain.m_domain, e_Progress.Updating);

					// 更新 IPv4
					if(same_ipv4)
						EVENTS.Add_Log($"{domain.m_domain} : IPv4 没变化，无需更新", Color.Black);
					else
					{
						switch(domain.m_type)
						{
						case e_DomainType.Godaddy:
							update_domain_Godaddy(domain, false);
							break;

						case e_DomainType.dynv6:
							update_domain_dynv6(domain, false);
							break;
						}	// switch
					}

					// 更新 IPv6
					if(same_ipv6)
						EVENTS.Add_Log($"{domain.m_domain} : IPv6 没变化，无需更新", Color.Black);
					else
					{
						switch(domain.m_type)
						{
						case e_DomainType.Godaddy:
							update_domain_Godaddy(domain, true);
							break;

						case e_DomainType.dynv6:
							update_domain_dynv6(domain, true);
							break;
						}	// switch
					}

					EVENTS.Set_Progress(domain.m_domain, e_Progress.Done);
				});

				threads.Add(th);

				th.Start(domains[i]);
			}	// for

			foreach(Thread th in threads)
			{
				if(timeout > 0)
					th.Join(timeout);
				else
					th.Join();
			}	// for
		}
	};
}	// namespace ddns_lib
