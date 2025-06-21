using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

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

		public string		m_dynu__API_Key				= "";
		public bool			m_dynu__API_Key_Visible		= false;

		public bool			m_Save_To_Config			= true;
	};

	public enum e_DomainType
	{
		Godaddy,
		dynv6,
		dynu,
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
		public class c_AddressFamily
		{
			public string	m_input_IP		= "";	// 使用这个 IP 设置 A/AAAA 记录
			public string	m_current_IP	= "";	// 当前的 IP

			public bool		m_enabled		= true;	// 是否允许 IP 更新
			public bool		m_same_ip		= true;	// 最后一次更新的 IP 是否跟解析的 IP 相同
			public string	m_err_msg		= "";	// 出错信息
		};

		// 复制数据
		public void	CopyTo(c_Domain domain)
		{
			domain.m_domain				= m_domain;
			domain.m_type				= m_type;

			domain.IPv4.m_input_IP		= IPv4.m_input_IP;
			domain.IPv4.m_current_IP	= IPv4.m_current_IP;
			domain.IPv4.m_enabled		= IPv4.m_enabled;
			domain.IPv4.m_err_msg		= IPv4.m_err_msg;

			domain.IPv6.m_input_IP		= IPv6.m_input_IP;
			domain.IPv6.m_current_IP	= IPv6.m_current_IP;
			domain.IPv6.m_enabled		= IPv6.m_enabled;
			domain.IPv6.m_err_msg		= IPv6.m_err_msg;

			domain.m_Godaddy__TTL		= m_Godaddy__TTL;

			domain.m_dynv6__Auto_IPv4	= m_dynv6__Auto_IPv4;
			domain.m_dynv6__Auto_IPv6	= m_dynv6__Auto_IPv6;

			domain.m_dynu__ID			= m_dynu__ID;
			domain.m_dynu__TTL			= m_dynu__TTL;

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

			return e_DomainType.Godaddy;
		}

		public string				m_domain			= "";
		public e_DomainType			m_type				= e_DomainType.Godaddy;

		public c_AddressFamily		IPv4				= new();
		public c_AddressFamily		IPv6				= new();

		// Godaddy
		public int					m_Godaddy__TTL		= 0;		// <=0 表示省略

		// dynv6
		public bool					m_dynv6__Auto_IPv4	= false;	// 自动设置 IPv4
		public bool					m_dynv6__Auto_IPv6	= false;	// 自动设置 IPv6

		// dynu
		public int					m_dynu__ID			= 0;		// 域名 ID（通过 dynu API 查询）
		public int					m_dynu__TTL			= 0;		// <=0 表示省略

		// 安全信息
		public c_Security_Profile?	m_Security_Profile	= null;
	};

	public class LIB
	{
		public class EVENTS
		{
			public delegate void e_Add_Log(string txt, Color c);
			public static event e_Add_Log? Event_On_AddLog = null;

			public static void Add_Log(string txt, Color c)
			{
				if(Event_On_AddLog != null)
					Event_On_AddLog(txt, c);
			}

			public delegate void e_Set_Progress(string domain, e_Progress progress);
			public static event e_Set_Progress? Event_Set_Progress = null;

			public static void Set_Progress(string domain, e_Progress progress)
			{
				if(Event_Set_Progress != null)
					Event_Set_Progress(domain, progress);
			}
		};

		private static SocketsHttpHandler	m_s_HttpHandler	= new SocketsHttpHandler();

		class c_DNS_Lookup_af
		{
			internal bool								m_same_ip	= false;
			internal AddressFamily						m_af;
			internal required c_Domain.c_AddressFamily	m_domain_af;
			internal required string					m_af_name;
			internal string								m_error_msg	= "";
			internal List<string>?						m_ip_list	= null;
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

			var af_list = new c_DNS_Lookup_af[]
			{
				new c_DNS_Lookup_af { m_af = AddressFamily.InterNetwork,	m_domain_af = domain.IPv4,	m_af_name = "IPv4" },
				new c_DNS_Lookup_af { m_af = AddressFamily.InterNetworkV6,	m_domain_af = domain.IPv6,	m_af_name = "IPv6" },
			};

			if(DNS_Server.Length == 0)
			{
				try
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(domain.m_domain);

					foreach(IPAddress ip in hostEntry.AddressList)
					{
						for(int i=0; i<af_list.Length; ++i)
						{
							var af = af_list[i];

							if(ip.AddressFamily == af.m_af)
							{
								if(!af.m_same_ip)	// 只判断第一个 IP
								{
									af.m_domain_af.m_current_IP = ip.ToString();

									if(IPAddress.TryParse(af.m_domain_af.m_input_IP, out IPAddress? input_IP) && ip.Equals(input_IP))
										af.m_same_ip = true;
								}

								break;
							}
						}	// for
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
				ddns_lib_CLR.c_DNS_Lookup_Result result = new();
				bool res = ddns_lib_CLR.CLR.DNS_Lookup(DNS_Server, domain.m_domain, result);

				if(!res)
				{
					EVENTS.Add_Log($"[Error] {domain.m_domain} : DNS_Lookup failed", Color.Red);
					return false;
				}

				var af_0 = af_list[0];
				var af_1 = af_list[1];

				af_0.m_error_msg	= result.m_ipv4_error_msg;
				af_1.m_error_msg	= result.m_ipv6_error_msg;

				af_0.m_ip_list		= result.m_ipv4_list;
				af_1.m_ip_list		= result.m_ipv6_list;

				for(int i=0; i<af_list.Length; ++i)
				{
					var af = af_list[i];

					if(af.m_error_msg.Length > 0)
						EVENTS.Add_Log($"[Warning] {domain.m_domain} : DNS_Lookup({af.m_af_name}) {af.m_error_msg}", Color.DarkOrange);

					foreach(string ip in af.m_ip_list!)
					{
						if(IPAddress.TryParse(ip, out IPAddress? ip_addr))
						{
							af.m_domain_af.m_current_IP = ip.ToString();

							if(IPAddress.TryParse(af.m_domain_af.m_input_IP, out IPAddress? input_IP) && ip_addr.Equals(input_IP))
							{
								af.m_same_ip = true;
								break;
							}
						}
					}	// for
				}	// for
			}

			same_ipv4 = af_list[0].m_same_ip;
			same_ipv6 = af_list[1].m_same_ip;

			return true;
		}

		/// <summary>检查 IP 合法性</summary>
		/// <param name="ip">IP 地址</param>
		/// <param name="domain">域名</param>
		/// <param name="update_ipv6">true = 更新 ipv6、false = 更新 ipv4</param>
		/// <returns>IP有效则返回 true；否则返回 false</returns>
		static bool check_IP(string ip, string domain, bool update_ipv6)
		{
			if(ip.Length == 0)
				return false;

			if(!IPAddress.TryParse(ip, out IPAddress? address))
			{
				EVENTS.Add_Log($"[Error] {domain} : 无效的 IP 格式（{ip}）", Color.Red);
				return false;
			}

			if(update_ipv6)
			{
				if(address.AddressFamily != AddressFamily.InterNetworkV6)
				{
					EVENTS.Add_Log($"[Error] {domain} : 无效的 IPv6（{ip}）", Color.Red);
					return false;
				}
			}
			else
			{
				if(address.AddressFamily != AddressFamily.InterNetwork)
				{
					EVENTS.Add_Log($"[Error] {domain} : 无效的 IPv4（{ip}）", Color.Red);
					return false;
				}
			}

			return true;
		}

		class c_update_domain_Godaddy_setting
		{
			internal bool								m_update;
			internal required c_Domain.c_AddressFamily	m_domain_af;
			internal required string					m_record_type;
		};

		/// <summary>更新域名（Godaddy）</summary>
		/// <param name="domain">要更新的域名</param>
		/// <param name="update_ipv4">是否更新 ipv4</param>
		/// <param name="update_ipv6">是否更新 ipv6</param>
		static void update_domain_Godaddy(c_Domain domain, bool update_ipv4, bool update_ipv6)
		{
			c_update_domain_Godaddy_setting[] settings = new[]
			{
				new c_update_domain_Godaddy_setting { m_update = update_ipv4,	m_domain_af = domain.IPv4,	m_record_type = "A" },
				new c_update_domain_Godaddy_setting { m_update = update_ipv6,	m_domain_af = domain.IPv6,	m_record_type = "AAAA" },
			};

			string name			= domain.m_domain.Substring(0, domain.m_domain.IndexOf('.'));
			string root_donmain	= domain.m_domain.Substring(domain.m_domain.IndexOf('.') + 1);

			HttpClient client = new(m_s_HttpHandler);

			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.TryAddWithoutValidation(	"Authorization",
																	$"sso-key {domain.m_Security_Profile!.m_Godaddy__Key}:{domain.m_Security_Profile.m_Godaddy__Secret}" );

			for(int i=0; i<settings.Length; ++i)
			{
				var setting = settings[i];

				if(!setting.m_update)
					continue;

				if(!check_IP(setting.m_domain_af.m_input_IP, domain.m_domain, i == 1))
					continue;

				StringBuilder sb_json = new();

				sb_json.Append("[\n");
				sb_json.Append("{\n");
				sb_json.Append($"	\"name\":\"{name}\",\n");
				sb_json.Append($"	\"type\":\"{setting.m_record_type}\",\n");
				sb_json.Append($"	\"data\":\"{setting.m_domain_af.m_input_IP}\"");

				if(domain.m_Godaddy__TTL > 0)
				{
					sb_json.Append(",\n");
					sb_json.Append($"	\"ttl\":{domain.m_Godaddy__TTL}\n");
				}

				sb_json.Append("}\n");
				sb_json.Append("]");

				string			url	= $"https://api.godaddy.com/v1/domains/{root_donmain}/records/{setting.m_record_type}/{name}";
				StringContent	sc	= new(sb_json.ToString(), Encoding.UTF8, "application/json");

				EVENTS.Add_Log($"连接到 {url}", Color.Black);
				EVENTS.Add_Log($"正在更新 {domain} 的「{setting.m_record_type} 记录」……", Color.Black);

				try
				{
					HttpResponseMessage response = client.PutAsync(url, sc).GetAwaiter().GetResult();

					if(response.IsSuccessStatusCode)
					{
						setting.m_domain_af.m_current_IP	= setting.m_domain_af.m_input_IP;
						setting.m_domain_af.m_err_msg		= "";

						EVENTS.Add_Log(	$"{domain.m_domain} : 更新「{setting.m_record_type} 记录」成功（{setting.m_domain_af.m_input_IP}）",
										Color.Green );
					}
					else
					{
						string err_msg = response.Content.ReadAsStringAsync().Result;

						setting.m_domain_af.m_err_msg = err_msg;

						EVENTS.Add_Log(	string.Format(	"[Error] {0:s} : 更新「{1:s} 记录」失败（{2:s}）[ip = {3:s}, StatusCode = {4:s}，ReasonPhrase = {5:s}]",
														domain.m_domain,
														setting.m_record_type,
														err_msg,
														setting.m_domain_af.m_input_IP,
														response.StatusCode.ToString(),
														response.ReasonPhrase ),
										Color.Red );
					}
				}
				catch(Exception ex)
				{
					string err_msg = (ex.InnerException == null) ? "" : ex.InnerException.Message;

					setting.m_domain_af.m_err_msg = err_msg;

					EVENTS.Add_Log(	string.Format(	"[Error] {0:s} : 更新「{1:s}记录」失败（{2:s}）[ip = {3:s}]",
													domain.m_domain,
													setting.m_record_type,
													err_msg,
													setting.m_domain_af.m_input_IP ),
									Color.Red );
				}
			}   // for
		}

		class c_update_domain_dynv6_setting
		{
			internal bool								m_update;
			internal required c_Domain.c_AddressFamily	m_domain_af;
			internal bool								m_auto;
			internal required string					m_record_type;
			internal required string					m_af_name;
		};

		/// <summary>更新域名（dynv6）</summary>
		/// <param name="domain">要更新的域名</param>
		/// <param name="update_ipv4">是否更新 ipv4</param>
		/// <param name="update_ipv6">是否更新 ipv6</param>
		static void update_domain_dynv6(c_Domain domain, bool update_ipv4, bool update_ipv6)
		{
			// https://ipv4.dynv6.com/api/update?zone=xxx.dynv6.net&ipv4=auto&token=...
			// https://ipv6.dynv6.com/api/update?zone=xxx.dynv6.net&ipv6=auto&token=...

			HttpClient client = new(m_s_HttpHandler);

			c_update_domain_dynv6_setting[] settings = new[]
			{
				new c_update_domain_dynv6_setting { m_update = update_ipv4,	m_domain_af = domain.IPv4,	m_auto = domain.m_dynv6__Auto_IPv4,	m_record_type = "A",	m_af_name = "ipv4" },
				new c_update_domain_dynv6_setting { m_update = update_ipv6,	m_domain_af = domain.IPv6,	m_auto = domain.m_dynv6__Auto_IPv6,	m_record_type = "AAAA",	m_af_name = "ipv6" },
			};

			for(int i=0; i<settings.Length; ++i)
			{
				var setting = settings[i];

				if(!setting.m_update)
					continue;

				string ip = "auto";

				if(!setting.m_auto)
				{
					ip = setting.m_domain_af.m_input_IP;

					if(!check_IP(ip, domain.m_domain, i == 1))
						return;
				}

				string url = string.Format(	"https://{0:s}.dynv6.com/api/update?zone={1:s}&{0:s}={2:s}&token={3:s}",
											setting.m_af_name,
											domain.m_domain,
											ip,
											domain.m_Security_Profile!.m_dynv6__token );

				try
				{
					string str = client.GetStringAsync(url).GetAwaiter().GetResult();

					if(!setting.m_auto)
						setting.m_domain_af.m_current_IP = ip;

					EVENTS.Add_Log($"{domain.m_domain} ({setting.m_af_name}) : 网站返回更新结果：{str}", Color.Black);
				}
				catch(Exception ex)
				{
					EVENTS.Add_Log(	string.Format(	"[Error] {0:s} ({1:s}) : 更新「{2:s}记录」失败（{3:s}）[ip = {4:s}]",
													domain.m_domain,
													setting.m_af_name,
													setting.m_record_type,
													(ex.InnerException == null) ? "" : ex.InnerException.Message,
													ip ),
									Color.Red );
				}
			}	// for
		}

		/// <summary>更新域名（dynu）</summary>
		/// <param name="domain">要更新的域名</param>
		/// <param name="update_ipv4">是否更新 ipv4</param>
		/// <param name="update_ipv6">是否更新 ipv6</param>
		static void update_domain_dynu(c_Domain domain, bool update_ipv4, bool update_ipv6)
		{
			// 文档：https://www.dynu.com/zh-CN/Support/API#/dns/dnsIdPost

			if(!update_ipv4 && !update_ipv6)
				return;

			HttpClient client = new(m_s_HttpHandler);

			// 请求头
			client.DefaultRequestHeaders.Add("accept", "application/json");
			client.DefaultRequestHeaders.Add("API-Key", $"{domain.m_Security_Profile!.m_dynu__API_Key}");

			// 先获取 <域名ID>
			// curl -X GET "https://api.dynu.com/v2/dns/getroot/acgdev.ddnsfree.com" -H "accept: application/json" -H "API-Key: 123456789"
			// 返回：{"statusCode":200,"id":11815363,"domainName":"acgdev.ddnsfree.com","hostname":"acgdev.ddnsfree.com","node":""}
			if(domain.m_dynu__ID == 0)
			{
				try
				{
					string res = client.GetStringAsync($"https://api.dynu.com/v2/dns/getroot/{domain.m_domain}").GetAwaiter().GetResult();

					const string k_ID	= "\"id\":";

					int		idx		= res.IndexOf(k_ID);
					string	str_ID	= res.Substring(idx + k_ID.Length);
					str_ID = str_ID.Substring(0, str_ID.IndexOf(","));

					domain.m_dynu__ID = int.Parse(str_ID);
				}
				catch(Exception ex)
				{
					EVENTS.Add_Log(	$"[Error] {domain.m_domain} : 获取「域名ID」失败（{ex.Message}）",
									Color.Red );
					return;
				}
			}

			if(!check_IP(domain.IPv4.m_input_IP, domain.m_domain, false))
				return;

			if(!check_IP(domain.IPv6.m_input_IP, domain.m_domain, true))
				return;

			StringBuilder sb_json = new();

			sb_json.Append("{\n");
			sb_json.Append($"	\"name\":\"{domain.m_domain}\"\n");

			if(update_ipv4)
			{
				sb_json.Append($",\"ipv4Address\":\"{domain.IPv4.m_input_IP}\",\n");
				sb_json.Append("\"ipv4\":true");
			}

			if(update_ipv6)
			{
				sb_json.Append($",\"ipv6Address\":\"{domain.IPv6.m_input_IP}\",\n");
				sb_json.Append("\"ipv6\":true");
			}

			if(domain.m_dynu__TTL > 0)
				sb_json.Append($",\"ttl\":{domain.m_dynu__TTL}\n");

			sb_json.Append("}");

			// curl：curl -X POST "https://api.dynu.com/v2/dns/<域名ID>" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"name\":\"acgdev.ddnsfree.com\",\"ipv4Address\":\"127.0.0.1\",\"ttl\":90,\"ipv4\":true}" -H "API-Key: 123456789"
			string			url	= $"https://api.dynu.com/v2/dns/{domain.m_dynu__ID}";
			StringContent	sc	= new(sb_json.ToString(), Encoding.UTF8, "application/json");

			string all_record_type	= "";
			string all_ip			= "";

			if(update_ipv4)
			{
				all_record_type	= "A";
				all_ip			= domain.IPv4.m_input_IP;
			}

			if(update_ipv6)
			{
				if(all_record_type.Length > 0)
				{
					all_record_type	+= "/";
					all_ip			+= "/";
				}

				all_record_type	+= "AAAA";
				all_ip			+= domain.IPv6.m_input_IP;
			}

			try
			{
				HttpResponseMessage response = client.PostAsync(url, sc).GetAwaiter().GetResult();

				string msg = response.Content.ReadAsStringAsync().Result;

				if(response.IsSuccessStatusCode)
				{
					if(update_ipv4)
					{
						domain.IPv4.m_current_IP	= domain.IPv4.m_input_IP;
						domain.IPv4.m_err_msg		= "";
					}

					if(update_ipv6)
					{
						domain.IPv6.m_current_IP	= domain.IPv6.m_input_IP;
						domain.IPv6.m_err_msg		= "";
					}

					EVENTS.Add_Log(	$"{domain.m_domain} : 更新「{all_record_type} 记录」成功（{all_ip}）（远程返回结果：{msg}）",
									Color.Green );
				}
				else
				{
					if(update_ipv4)
						domain.IPv4.m_err_msg = msg;

					if(update_ipv6)
						domain.IPv6.m_err_msg = msg;

					EVENTS.Add_Log(	string.Format(	"[Error] {0:s} : 更新「{1:s} 记录」失败（{2:s}）[ip = {3:s}, StatusCode = {4:s}，ReasonPhrase = {5:s}]",
													domain.m_domain,
													all_record_type,
													msg,
													all_ip,
													response.StatusCode.ToString(),
													response.ReasonPhrase ),
									Color.Red );
				}
			}
			catch(Exception ex)
			{
				string err_msg = (ex.InnerException == null) ? "" : ex.InnerException.Message;

				if(update_ipv4)
					domain.IPv4.m_err_msg = err_msg;

				if(update_ipv6)
					domain.IPv6.m_err_msg = err_msg;

				EVENTS.Add_Log(	string.Format(	"[Error] {0:s} : 更新「{1:s}记录」失败（{2:s}）[ip = {3:s}]",
												domain.m_domain,
												all_record_type,
												err_msg,
												all_ip ),
								Color.Red );
			}
		}


		/*==============================================================
		 * 更新 IP 记录
		 *==============================================================*/
		public static void update_domains(	List<c_Domain>	domains,
											bool			DNS_Lookup_First,		// IP变动时，先解析域名再执行更新
											List<string>?	DNS_Server_List	= null,	// 自定义DNS服务器（列表元素为 "" 时，表示系统默认 DNS）
											int				timeout			= 15 * 1000 )
		{
			//m_s_HttpHandler.MaxConnectionsPerServer = 1000;
			//m_s_HttpHandler.SslOptions.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13;

			List<Thread> threads = new();

			if(DNS_Server_List == null)
				DNS_Server_List = new List<string> { "" };

			for(int i=0; i<domains.Count; ++i)
			{
				if(!domains[i].IPv4.m_enabled && !domains[i].IPv6.m_enabled)
					continue;

				Thread th = new((object? o) =>
				{
					c_Domain domain = (c_Domain)o!;

					domain.IPv4.m_same_ip = false;
					domain.IPv6.m_same_ip = false;

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

							if(DNS_Lookup(domain, dns_server, out domain.IPv4.m_same_ip, out domain.IPv6.m_same_ip))
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

					if(domain.IPv4.m_enabled && domain.IPv4.m_same_ip)
						EVENTS.Add_Log($"{domain.m_domain} : IPv4 没变化，无需更新", Color.Black);

					if(domain.IPv6.m_enabled && domain.IPv6.m_same_ip)
						EVENTS.Add_Log($"{domain.m_domain} : IPv6 没变化，无需更新", Color.Black);

					switch(domain.m_type)
					{
					case e_DomainType.Godaddy:
						update_domain_Godaddy(	domain,
												domain.IPv4.m_enabled && !domain.IPv4.m_same_ip,
												domain.IPv6.m_enabled && !domain.IPv6.m_same_ip );
						break;

					case e_DomainType.dynv6:
						update_domain_dynv6(domain,
											domain.IPv4.m_enabled && !domain.IPv4.m_same_ip,
											domain.IPv6.m_enabled && !domain.IPv6.m_same_ip);
						break;

					case e_DomainType.dynu:
						update_domain_dynu(	domain,
											domain.IPv4.m_enabled && !domain.IPv4.m_same_ip,
											domain.IPv6.m_enabled && !domain.IPv6.m_same_ip );
						break;
					}	// switch

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
