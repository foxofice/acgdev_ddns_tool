using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Sockets;
using System.Net;

namespace ddns_lib
{
	public class c_Record
	{
		//（必须单独设置）
		public string	m_name		= "";
		public string	m_domain	= "";
		public int		m_TTL		= 0;
		public int		m_user_idx	= 0;	// 索引（用户自定义数据）

		//（可以全局设置）
		public string	m_Key		= "";
		public string	m_Secret	= "";
		public string	m_ip		= "";	// 使用这个 IP 设置 A/AAAA 记录

		//（输出结果）
		public string	m_result_ip	= "";	// 更新成功后的 IP
		public string	m_err_msg	= "";
	};

	public class LIB
	{
		public class EVENTS
		{
			public delegate void e_Add_Log(string txt, Color c);
			public static event e_Add_Log	Event_On_AddLog	= null;

			public static void Add_Log(string txt, Color c)
			{
				if(Event_On_AddLog != null)
					Event_On_AddLog(txt, c);
			}
		};

		/*==============================================================
		 * 更新 IP 记录
		 *==============================================================*/
		public static void update_records(	ref List<c_Record>	records,
											int					timeout = 15 * 1000 )
		{
			List<Thread> threads = new List<Thread>();

			for(int i=0; i<records.Count; ++i)
			{
				Thread th = new Thread(TH_update_record);
				threads.Add(th);

				th.Start(records[i]);
			}	// for

			if(timeout > 0)
			{
				foreach(Thread th in threads)
					th.Join(timeout);
			}
		}


		/*==============================================================
		 * 更新 A/AAAA 记录的线程
		 *==============================================================*/
		public static void TH_update_record(object param)
		{
			c_Record record = (c_Record)param;

			IPAddress address;
			if(!IPAddress.TryParse(record.m_ip, out address))
			{
				EVENTS.Add_Log($"无效的 IP：{record.m_ip}", Color.Red);
				return;
			}

			if(address.AddressFamily != AddressFamily.InterNetwork &&
				address.AddressFamily != AddressFamily.InterNetworkV6)
			{
				EVENTS.Add_Log($"无效的 IPv4 或 IPv6：{record.m_ip}", Color.Red);
				return;
			}

			string record_type = (address.AddressFamily == AddressFamily.InterNetwork) ? "A" : "AAAA";

			StringBuilder sb_json = new StringBuilder();

			sb_json.Append("[\n");
			sb_json.Append("	{\n");
			sb_json.Append($"		\"name\":\"{record.m_name}\",\n");
			sb_json.Append($"		\"type\":\"{record_type}\",\n");
			sb_json.Append($"		\"data\":\"{record.m_ip}\"");

			if(record.m_TTL > 0)
			{
				sb_json.Append(",\n");
				sb_json.Append($"	\"ttl\":{record.m_TTL}\n");
			}

			sb_json.Append("	}\n");
			sb_json.Append("]");

			string			url	= $"https://api.godaddy.com/v1/domains/{record.m_domain}/records/{record_type}/{record.m_name}";
			StringContent	sc	= new StringContent(sb_json.ToString(), Encoding.UTF8, "application/json");

			EVENTS.Add_Log($"连接到 {url}", Color.Black);
			EVENTS.Add_Log($"正在更新 {record.m_name}.{record.m_domain} 的「{record_type} 记录」……", Color.Black);

			HttpClient client = new HttpClient();

			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"sso-key {record.m_Key}:{record.m_Secret}");

			try
			{
				HttpResponseMessage response = client.PutAsync(url, sc).GetAwaiter().GetResult();

				record.m_result_ip = response.IsSuccessStatusCode ? record.m_ip : "";

				if(!response.IsSuccessStatusCode)
				{
					EVENTS.Add_Log(	$"更新 {record.m_name}.{record.m_domain} 的「{record_type} 记录」失败（ip = {record.m_ip}, StatusCode = {response.StatusCode}，ReasonPhrase = {response.ReasonPhrase}）",
									Color.Red );
				}
				else
				{
					EVENTS.Add_Log($"更新 {record.m_name}.{record.m_domain} 的「{record_type} 记录」成功（ip = {record.m_ip}）", Color.Green);
				}
			}
			catch(Exception ex)
			{
				record.m_err_msg = ex.InnerException.Message;
				EVENTS.Add_Log($"{record.m_name}.{record.m_domain} -> {record.m_ip} : {ex.InnerException.Message}", Color.Red);
			}
		}
	};
}	// namespace ddns_lib
