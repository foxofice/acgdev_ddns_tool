using System.Diagnostics;
using System.Net;
using System.Text;

namespace ddns_tool
{
	public partial class frm_MainForm : Form
	{
		#region 类型/变量
		enum e_Column_Domains
		{
			Domain,
			Type,	// [Godaddy] TTL = 600；[dynv6] 自动IPv4, 自动IPv6；[dynu] TTL = 600, ID = 12345678
			Profile,
			current_IPv4,
			current_IPv6,
			Status,
		};

		enum e_Column_Log
		{
			Time,
			Log,
		};

		internal static frm_MainForm	m_s_Mainform			= null!;
		internal frm_IP_Change_Popup	m_IP_Change_Popup		= new();

		bool							m_exiting				= false;		// 是否正在退出

		DateTime						m_can_auto_update_time	= DateTime.Now;	// 可以自动执行更新的时间
		bool							m_is_updating			= false;		// 是否正在执行更新

		bool							m_login_server_done		= false;		// 已成功登录 Server

		int								m_next_save_log_idx		= 0;			// 下一个保存日志的索引

		// (url, 支持IPv4, 支持IPv6)
		List<(string m_url, bool m_IPv4, bool m_IPv6)>	m_get_ip_url_List = new List<(string, bool, bool)>
		{
			// m_url,							m_IPv4,	m_IPv6
			("https://icanhazip.com",			true,	true),
			("https://ipinfo.io/ip",			true,	false),
			("https://checkip.amazonaws.com",	true,	false),
			("https://ifconfig.me/ip",			true,	true),
			("https://ipecho.net/plain",		true,	true),
			("https://api.ip.sb/ip",			true,	true),
			("https://a.ident.me",				true,	true),
			("https://4.ident.me",				true,	false),
			("https://6.ident.me",				false,	true),
			("https://ipv4.icanhazip.com",		true,	false),
			("https://ipv6.icanhazip.com",		false,	true),
			("https://ip.3322.net",				true,	false),
			("https://ip.qaros.com",			true,	false),
			("https://test.ipw.cn",				true,	true),
			("https://4.ipw.cn",				true,	false),
			("https://6.ipw.cn",				false,	true),
		};
		#endregion

		#region 函数
		/*==============================================================
		 * 域名 -> LVI
		 *==============================================================*/
		internal ListViewItem? find_LVI__Domain(string domain_name)
		{
			foreach(ListViewItem LVI in listView_Domains.Items)
			{
				if(string.Compare(domain_name, LVI.SubItems[(int)e_Column_Domains.Domain].Text, true) == 0)
					return LVI;
			}	// for

			return null;
		}

		/*==============================================================
		 * 执行委托
		 *==============================================================*/
		public void invoke(Action func)
		{
			if(this.InvokeRequired)
				this.Invoke(func);
			else
				func();
		}

		/*==============================================================
		 * 锁定控件（UI 线程安全）
		 *==============================================================*/
		void lock_controls(bool enabled)
		{
			invoke(() =>
			{
				//【更新方式】
				radioButton_Settings_Type__Local.Enabled		= enabled;
				radioButton_Settings_Type__Remote.Enabled		= enabled;

				// 远程 Server 设置
				textBox_Settings_RemoteServer__Addr.ReadOnly	= !enabled || !radioButton_Settings_Type__Remote.Checked;
				textBox_Settings_RemoteServer__User.ReadOnly	= !enabled || !radioButton_Settings_Type__Remote.Checked;
				textBox_Settings_RemoteServer__Pwd.ReadOnly		= !enabled || !radioButton_Settings_Type__Remote.Checked;

				//【设置 IP】
				radioButton_Settings_IPv6__From_URL.Enabled		= enabled;
				comboBox_Settings_IPv4__From_URL.Enabled		= enabled && radioButton_Settings_IPv4__From_URL.Checked;
				comboBox_Settings_IPv6__From_URL.Enabled		= enabled && radioButton_Settings_IPv6__From_URL.Checked;

				radioButton_Settings_IPv6__Manual.Enabled		= enabled;
				textBox_Settings_IPv4.ReadOnly					= !enabled || !radioButton_Settings_IPv4__Manual.Checked;
				textBox_Settings_IPv6.ReadOnly					= !enabled || !radioButton_Settings_IPv6__Manual.Checked;

				if(enabled)
					UpdateUI_radioButton_Settings_IP__Accept_IP();
				else
				{
					radioButton_Settings_IPv4__Accept_IP.Enabled	= false;
					radioButton_Settings_IPv6__Accept_IP.Enabled	= false;
				}

				//【安全设置】
				textBox_Security_Godaddy__Key.ReadOnly			= !enabled;
				textBox_Security_Godaddy__Secret.ReadOnly		= !enabled;

				textBox_Security_dynv6__token.ReadOnly			= !enabled;

				textBox_Security_dynu__API_Key.ReadOnly			= !enabled;

				//【更新操作】
				checkBox_Action_UpdateIP.Enabled				= enabled;
				checkBox_Action_DNS_Lookup_First.Enabled		= enabled;
				checkBox_Action_Use_Custom_DNS.Enabled			= enabled;
				textBox_Action_Custom_DNS_List.ReadOnly			= !enabled || !checkBox_Action_Use_Custom_DNS.Checked;
				numericUpDown_Action_Timeout.Enabled			= enabled;

				checkBox_Action_IP_Change_Popup.Enabled			= enabled;

				checkBox_Action_IP_Change_PlaySound.Enabled		= enabled;
				textBox_Action_IP_Change_PlaySound.ReadOnly		= !enabled || !checkBox_Action_IP_Change_PlaySound.Checked;
				button_Action_IP_Change_PlaySound.Enabled		= enabled;

				//【域名列表】
				toolStrip_Domains.Enabled						= enabled;

				//【日志记录】
				numericUpDown_Logs_MaxLines.Enabled				= enabled;

				//（主界面）
				button_Update.Enabled							= enabled;
			});
		}
		#endregion

		#region 封包事件
		internal class EVENTS
		{
			/*==============================================================
			 * OnConnected
			 *==============================================================*/
			internal static void OnConnected(string ip, ushort port)
			{
				m_s_Mainform.add_log($"连接到 Server 成功 (client = {ip}:{port})", Color.Green);
			}

			/*==============================================================
			 * OnDisconnecting
			 *==============================================================*/
			internal static void OnDisconnecting()
			{
				m_s_Mainform.add_log("已断开 Server 的连接");
				m_s_Mainform.update__done();

				m_s_Mainform.invoke(() =>
				{
					m_s_Mainform.textBox_Settings_RemoteServer__Ping.Clear();
				});

				if(CONFIG.SET_IP.m_s_type_IPv4 == CONFIG.e_IP_Get_Type.Server_Accept_IP)
					CONFIG.SET_IP.m_s_IPv4 = "";

				if(CONFIG.SET_IP.m_s_type_IPv6 == CONFIG.e_IP_Get_Type.Server_Accept_IP)
					CONFIG.SET_IP.m_s_IPv6 = "";

				m_s_Mainform.m_login_server_done = false;
			}

			/*==============================================================
			 * Recv_Ping
			 *==============================================================*/
			internal static void Recv_Ping(double ping)
			{
				m_s_Mainform.invoke(() =>
				{
					m_s_Mainform.textBox_Settings_RemoteServer__Ping.Text = (ping * 1000).ToString("F3");
					m_s_Mainform.Update_Settings_Preview__Ping();
				});
			}

			/*==============================================================
			 * Recv_LoginResult
			 *==============================================================*/
			internal static void Recv_LoginResult(bool result)
			{
				if(result)
				{
					m_s_Mainform.m_login_server_done = true;

					m_s_Mainform.add_log("登录服务器成功", Color.Green);

					ddns_tool_CLR.CLR.send_Update_Domains(	CONFIG.m_s_domains_list,
															CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First,
															(m_s_Mainform.m_DNS_List.Count == 0) ? null : m_s_Mainform.m_DNS_List,
															CONFIG.UPDATE_ACTION.m_s_Timeout * 1000 );
				}
				else
				{
					m_s_Mainform.add_log("[Error] 登录服务器失败", Color.Red);

					ddns_tool_CLR.CLR.DisConnect();
				}
			}

			/*==============================================================
			 * Recv_Update_Domains_Result
			 *==============================================================*/
			internal static void Recv_Update_Domains_Result(List<ddns_lib.c_Domain> domains)
			{
				foreach(ddns_lib.c_Domain res in domains)
				{
					ddns_lib.c_Domain? domain = CONFIG.find_domain(res.m_domain);

					if(domain == null)
						continue;

					domain.IPv4.m_current_IP	= res.IPv4.m_current_IP;
					domain.IPv6.m_current_IP	= res.IPv6.m_current_IP;
					domain.IPv4.m_err_msg		= res.IPv4.m_err_msg;
					domain.IPv6.m_err_msg		= res.IPv6.m_err_msg;
				}	// for

				m_s_Mainform.update__done();
			}

			/*==============================================================
			 * On_add_log
			 *==============================================================*/
			internal static void On_add_log(string txt, Color c)
			{
				m_s_Mainform.add_log(txt, c);
			}

			/*==============================================================
			 * On_Set_Progress
			 *==============================================================*/
			internal static void On_Set_Progress(string domain, ddns_lib.e_Progress progress)
			{
				m_s_Mainform.invoke(() =>
				{
					ListViewItem? LVI = m_s_Mainform.find_LVI__Domain(domain);

					if(LVI != null)
					{
						switch(progress)
						{
						case ddns_lib.e_Progress.None:			LVI.SubItems[(int)e_Column_Domains.Status].Text = "";			break;
						case ddns_lib.e_Progress.Starting:		LVI.SubItems[(int)e_Column_Domains.Status].Text = "开始更新";	break;
						case ddns_lib.e_Progress.DNS_Lookup:	LVI.SubItems[(int)e_Column_Domains.Status].Text = "域名解析";	break;
						case ddns_lib.e_Progress.Updating:		LVI.SubItems[(int)e_Column_Domains.Status].Text = "正在更新";	break;
						case ddns_lib.e_Progress.Done:			LVI.SubItems[(int)e_Column_Domains.Status].Text = "更新完成";	break;
						case ddns_lib.e_Progress.Failed:		LVI.SubItems[(int)e_Column_Domains.Status].Text = "更新失败";	break;
						}	// switch

						LVI.SubItems[(int)e_Column_Domains.Status].ForeColor = (progress == ddns_lib.e_Progress.Failed) ? Color.Red : Color.Black;
					}
				});
			}
		};
		#endregion

		#region LVI.Tag
		/*==============================================================
		 * 设置/获取 LVI.Tag（Domain）
		 *==============================================================*/
		void Set_LVI_Tag__Domain(ListViewItem LVI, ddns_lib.c_Domain domain)
		{
			LVI.Tag = domain;
		}
		//--------------------------------------------------
		ddns_lib.c_Domain Get_LVI_Tag__Domain(ListViewItem LVI)
		{
			return (ddns_lib.c_Domain)LVI.Tag!;
		}

		/*==============================================================
		 * 设置/获取 LVI.Tag（Security_Profile）
		 *==============================================================*/
		void Set_LVI_Tag__Security_Profile(ListViewItem LVI, ddns_lib.c_Security_Profile profile)
		{
			LVI.Tag = profile;
		}
		//--------------------------------------------------
		ddns_lib.c_Security_Profile Get_LVI_Tag__Security_Profile(ListViewItem LVI)
		{
			return (ddns_lib.c_Security_Profile)LVI.Tag!;
		}
		#endregion

		public frm_MainForm()
		{
			InitializeComponent();

			m_s_Mainform = this;
		}

		#region Winform 事件
		/*==============================================================
		 * 窗口加载/窗口关闭
		 *==============================================================*/
		private void frm_MainForm_Load(object sender, EventArgs e)
		{
			this.Icon				= Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule!.FileName);
			notifyIcon_Main.Icon	= this.Icon;
			notifyIcon_Main.Text	= this.Text;

			// 初始化 DDNS_CLR
			ddns_tool_CLR.CLR.DoInit();

			// 设置回调函数
			ddns_tool_CLR.CLR.Event_OnConnected					+= EVENTS.OnConnected;
			ddns_tool_CLR.CLR.Event_OnDisconnecting				+= EVENTS.OnDisconnecting;
			ddns_tool_CLR.CLR.Event_Recv_Ping					+= EVENTS.Recv_Ping;
			ddns_tool_CLR.CLR.Event_Recv_LoginResult			+= EVENTS.Recv_LoginResult;
			ddns_tool_CLR.CLR.Event_Recv_Update_Domains_Result	+= EVENTS.Recv_Update_Domains_Result;
			ddns_tool_CLR.CLR.Event_On_add_log					+= EVENTS.On_add_log;

			ddns_lib.LIB.EVENTS.Event_On_AddLog					+= EVENTS.On_add_log;
			ddns_lib.LIB.EVENTS.Event_Set_Progress				+= EVENTS.On_Set_Progress;

			CONFIG.read_config();

			//==================== 更新 UI ====================(Start)
			//【域名列表】
			foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
				add_LVI__Domain(domain);

			//【日志记录】
			numericUpDown_Logs_MaxLines.Value	= CONFIG.LOG.m_s_MaxLines;
			checkBox_Logs__Save_To_File.Checked	= CONFIG.LOG.m_s_Save_To_File;

			//【更新方式】
			switch(CONFIG.m_s_update_type)
			{
			case CONFIG.e_Update_Type.Local:	radioButton_Settings_Type__Local.Checked	= true;	break;
			case CONFIG.e_Update_Type.Remote:	radioButton_Settings_Type__Remote.Checked	= true;	break;
			}	// switch

			// 远程 Server 设置
			textBox_Settings_RemoteServer__Addr.Text		= CONFIG.REMOTE_SERVER.m_s_addr;
			textBox_Settings_RemoteServer__User.Text		= CONFIG.REMOTE_SERVER.m_s_user;
			textBox_Settings_RemoteServer__Pwd.Text			= CONFIG.REMOTE_SERVER.m_s_pwd;
			checkBox_Settings_RemoteServer__Pwd.Checked		= CONFIG.REMOTE_SERVER.m_s_show_pwd;
			checkBox_Settings_RemoteServer__Ping.Checked	= CONFIG.REMOTE_SERVER.m_s_auto_ping;

			//【设置 IP】
			foreach(var get_ip_url in m_get_ip_url_List)
			{
				if(get_ip_url.m_IPv4)
					comboBox_Settings_IPv4__From_URL.Items.Add(get_ip_url.m_url);

				if(get_ip_url.m_IPv6)
					comboBox_Settings_IPv6__From_URL.Items.Add(get_ip_url.m_url);
			}	// for

			radioButton_Settings_IPv4__From_URL.Checked		= (CONFIG.SET_IP.m_s_type_IPv4 == CONFIG.e_IP_Get_Type.Get_IP_From_URL);
			comboBox_Settings_IPv4__From_URL.Text			= CONFIG.SET_IP.m_s_Get_IPv4_URL;
			radioButton_Settings_IPv4__Manual.Checked		= (CONFIG.SET_IP.m_s_type_IPv4 == CONFIG.e_IP_Get_Type.Manual_IP);
			textBox_Settings_IPv4.Text						= CONFIG.SET_IP.m_s_IPv4;
			radioButton_Settings_IPv4__Accept_IP.Checked	= (CONFIG.SET_IP.m_s_type_IPv4 == CONFIG.e_IP_Get_Type.Server_Accept_IP);

			radioButton_Settings_IPv6__From_URL.Checked		= (CONFIG.SET_IP.m_s_type_IPv6 == CONFIG.e_IP_Get_Type.Get_IP_From_URL);
			comboBox_Settings_IPv6__From_URL.Text			= CONFIG.SET_IP.m_s_Get_IPv6_URL;
			radioButton_Settings_IPv6__Manual.Checked		= (CONFIG.SET_IP.m_s_type_IPv6 == CONFIG.e_IP_Get_Type.Manual_IP);
			textBox_Settings_IPv6.Text						= CONFIG.SET_IP.m_s_IPv6;
			radioButton_Settings_IPv6__Accept_IP.Checked	= (CONFIG.SET_IP.m_s_type_IPv6 == CONFIG.e_IP_Get_Type.Server_Accept_IP);

			if(CONFIG.SET_IP.m_s_Get_IPv4_URL.Length == 0)
				comboBox_Settings_IPv4__From_URL.SelectedIndex = 0;

			if(CONFIG.SET_IP.m_s_Get_IPv6_URL.Length == 0)
				comboBox_Settings_IPv6__From_URL.SelectedIndex = 0;

			//【安全设置】
			foreach(ddns_lib.c_Security_Profile profile in CONFIG.SECURITY.m_s_profiles)
			{
				ListViewItem LVI = new(profile.m_Name);
				listView_Security.Items.Add(LVI);

				Set_LVI_Tag__Security_Profile(LVI, profile);
			}	// for

			//【更新操作】
			checkBox_Action_UpdateIP.Checked				= CONFIG.UPDATE_ACTION.m_s_UpdateIP;

			// 自动更新
			checkBox_Action_AutoAction_Interval.Checked		= CONFIG.ACTION.m_s_AutoAction;
			numericUpDown_Action_AutoAction_Interval.Value	= CONFIG.ACTION.m_s_AutoAction_interval;

			// 先解析域名
			checkBox_Action_DNS_Lookup_First.Checked		= CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First;

			// 自定义 DNS 服务器
			checkBox_Action_Use_Custom_DNS.Checked			= CONFIG.UPDATE_ACTION.m_s_Use_Custom_DNS;
			textBox_Action_Custom_DNS_List.Lines			= CONFIG.UPDATE_ACTION.m_s_Custom_DNS_List.ToArray();

			// 自动更新超时（单位：秒。0 = 无限等待）
			numericUpDown_Action_Timeout.Value				= CONFIG.UPDATE_ACTION.m_s_Timeout;

			// IP变动时，弹出提示窗口
			checkBox_Action_IP_Change_Popup.Checked			= CONFIG.UPDATE_ACTION.m_s_IP_Change_Popup;

			// IP变动时，播放音乐
			checkBox_Action_IP_Change_PlaySound.Checked		= CONFIG.UPDATE_ACTION.m_s_IP_Change_Play_Sound;
			textBox_Action_IP_Change_PlaySound.Text			= CONFIG.UPDATE_ACTION.m_s_IP_Change_Sound_Path;
			//==================== 更新 UI ====================(End)
			CONFIG.m_s_dirty = false;

			set_next_Auto_Update_Time();

			Update_Settings_Preview__Update_Type();
			Update_Settings_Preview__Ping();
			Update_Settings_Preview__Set_IPv4();
			Update_Settings_Preview__Set_IPv6();
			Update_Settings_Preview__Security();
			Update_Settings_Preview__UpdateIP();
			Update_Settings_Preview__AutoUpdate();
			Update_Settings_Preview__DNS_Lookup_First();
			Update_Settings_Preview__DNS_Server();
			Update_Settings_Preview__timeout();
		}
		//--------------------------------------------------
		private void frm_MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(!m_exiting)
			{
				e.Cancel = true;
				this.Hide();
			}
			else
			{
				CONFIG.save_config();

				// 清理 DDNS_CLR
				ddns_tool_CLR.CLR.DoFinal();
			}
		}

		/*==============================================================
		 * 官网
		 *==============================================================*/
		private void linkLabel_WebSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://www.AcgDev.com");
		}

		/*==============================================================
		 * github
		 *==============================================================*/
		private void linkLabel_Github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/foxofice/acgdev_ddns_tool");
		}
		#endregion

		#region 计时器
		/*==============================================================
		 * 保存配置文件
		 *==============================================================*/
		private void timer_Save_Config_Tick(object sender, EventArgs e)
		{
			CONFIG.save_config();
		}

		/*==============================================================
		 * 更新 IP
		 *==============================================================*/
		private void timer_Update_Tick(object sender, EventArgs e)
		{
			if(DateTime.Now < m_can_auto_update_time)
				return;

			update__start_update();
		}

		/*==============================================================
		 * ping
		 *==============================================================*/
		private void timer_Ping_Tick(object sender, EventArgs e)
		{
			if(CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote && CONFIG.REMOTE_SERVER.m_s_auto_ping)
				ddns_tool_CLR.CLR.send_Ping();
		}

		/*==============================================================
		 * 保存日志
		 *==============================================================*/
		private void timer_Save_Log_Tick(object sender, EventArgs e)
		{
			if(!CONFIG.LOG.m_s_Save_To_File)
				return;

			if(m_next_save_log_idx < 0)
				m_next_save_log_idx = 0;

			if(m_next_save_log_idx < listView_Logs.Items.Count)
			{
				string log_filename = $"Logs\\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
				string dir = Path.GetDirectoryName(log_filename)!;
				if(!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				List<string> lines = new(listView_Logs.Items.Count - m_next_save_log_idx + 1);

				for(int i=m_next_save_log_idx; i<listView_Logs.Items.Count; ++i)
				{
					ListViewItem LVI = listView_Logs.Items[i];

					string line = string.Format("{0:s}\t{1:s}",
												LVI.SubItems[(int)e_Column_Log.Time].Text,
												LVI.SubItems[(int)e_Column_Log.Log].Text);
					lines.Add(line);
				}	// for

				File.AppendAllLines(log_filename, lines);
				m_next_save_log_idx = listView_Logs.Items.Count;
			}
		}
		#endregion

		#region 托盘图标
		/*==============================================================
		 * 双击托盘图标
		 *==============================================================*/
		private void notifyIcon_Main_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if(this.Visible)
				this.Hide();
			else
				FORMS.active_form(this);
		}
		#endregion
		#region 托盘图标 - 上下文菜单
		/*==============================================================
		 * 打开
		 *==============================================================*/
		private void ToolStripMenuItem_Open_Click(object sender, EventArgs e)
		{
			FORMS.active_form(this);
		}

		/*==============================================================
		 * 退出程序
		 *==============================================================*/
		private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	"是否退出程序？",
								this.Text,
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question,
								MessageBoxDefaultButton.Button2 ) == DialogResult.No)
				return;

			m_exiting					= true;
			timer_Save_Config.Enabled	= false;
			timer_Update.Enabled		= false;

			this.Close();
		}
		#endregion

		#region 域名列表
		/*==============================================================
		 * 更新 LVI
		 *==============================================================*/
		void update_LVI__Domain(ListViewItem LVI)
		{
			ddns_lib.c_Domain domain	= Get_LVI_Tag__Domain(LVI);

			LVI.SubItems[(int)e_Column_Domains.Domain].Text	= domain.m_domain;

			StringBuilder sb_type = new();

			sb_type.Append($"[{domain.m_type}]");

			switch(domain.m_type)
			{
			case ddns_lib.e_DomainType.Godaddy:
				if(domain.m_Godaddy__TTL > 0)
					sb_type.Append($" TTL = {domain.m_Godaddy__TTL}");
				break;

			case ddns_lib.e_DomainType.dynv6:
				if(domain.m_dynv6__Auto_IPv4)
					sb_type.Append(" 自动IPv4");

				if(domain.m_dynv6__Auto_IPv6)
				{
					if(domain.m_dynv6__Auto_IPv4)
						sb_type.Append("+IPv6");
					else
						sb_type.Append(" 自动IPv6");
				}
				break;

			case ddns_lib.e_DomainType.dynu:
				if(domain.m_dynu__TTL > 0)
					sb_type.Append($" TTL = {domain.m_dynu__TTL},");

				sb_type.Append($" ID = {domain.m_dynu__ID}");
				break;
			}	// switch

			LVI.SubItems[(int)e_Column_Domains.Type].Text			= sb_type.ToString();
			LVI.SubItems[(int)e_Column_Domains.Profile].Text		= (domain.m_Security_Profile != null) ? domain.m_Security_Profile.m_Name : "";

			LVI.SubItems[(int)e_Column_Domains.current_IPv4].Text	= domain.IPv4.m_current_IP;
			LVI.SubItems[(int)e_Column_Domains.current_IPv6].Text	= domain.IPv6.m_current_IP;

			var LVSI_IPv4 = LVI.SubItems[(int)e_Column_Domains.current_IPv4];
			var LVSI_IPv6 = LVI.SubItems[(int)e_Column_Domains.current_IPv6];

			if(domain.IPv4.m_enabled)
			{
				LVSI_IPv4.Font		= new(LVSI_IPv4.Font, LVSI_IPv4.Font.Style & ~FontStyle.Strikeout);
				LVSI_IPv4.BackColor	= Color.White;
			}
			else
			{
				LVSI_IPv4.Font		= new(LVSI_IPv4.Font, LVSI_IPv4.Font.Style | FontStyle.Strikeout);
				LVSI_IPv4.BackColor	= Color.Gray;
			}

			if(domain.IPv6.m_enabled)
			{
				LVSI_IPv6.Font		= new(LVSI_IPv6.Font, LVSI_IPv6.Font.Style & ~FontStyle.Strikeout);
				LVSI_IPv6.BackColor	= Color.White;
			}
			else
			{
				LVSI_IPv6.Font		= new(LVSI_IPv6.Font, LVSI_IPv6.Font.Style | FontStyle.Strikeout);
				LVSI_IPv6.BackColor	= Color.Gray;
			}
		}

		/*==============================================================
		 * 更新所有 LVI
		 *==============================================================*/
		void update_All_LVI__Domain()
		{
			foreach(ListViewItem LVI in listView_Domains.Items)
				update_LVI__Domain(LVI);
		}

		/*==============================================================
		 * 添加 LVI
		 *==============================================================*/
		void add_LVI__Domain(ddns_lib.c_Domain domain)
		{
			ListViewItem LVI = new();

			while(LVI.SubItems.Count < listView_Domains.Columns.Count)
				LVI.SubItems.Add("");

			listView_Domains.Items.Add(LVI);
			Set_LVI_Tag__Domain(LVI, domain);

			LVI.UseItemStyleForSubItems = false;
			LVI.EnsureVisible();

			update_LVI__Domain(LVI);
		}

		/*==============================================================
		 * 修改域名
		 *==============================================================*/
		void edit_domain()
		{
			if(listView_Domains.SelectedItems.Count == 0)
				return;

			ListViewItem LVI = listView_Domains.SelectedItems[0];

			ddns_lib.c_Domain domain = Get_LVI_Tag__Domain(LVI);

			frm_Domain dlg = new(domain);

			if(dlg.ShowDialog() == DialogResult.OK)
			{
				domain = dlg.m_domain;
				Set_LVI_Tag__Domain(LVI, domain);

				if(string.Compare(domain.m_domain, LVI.SubItems[(int)e_Column_Domains.Domain].Text, true) != 0)
					LVI.SubItems[(int)e_Column_Domains.Status].Text = "";

				update_LVI__Domain(LVI);

				CONFIG.m_s_dirty = true;
			}
		}

		/*==============================================================
		 * 选择项改变
		 *==============================================================*/
		private void listView_Domains_SelectedIndexChanged(object sender, EventArgs e)
		{
			toolStripButton_Domains_Modify.Enabled			= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_Delete.Enabled			= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_IPv4_Enable.Enabled		= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_IPv4_Disable.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_IPv6_Enable.Enabled		= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_IPv6_Disable.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_CopyText.Enabled		= (listView_Domains.SelectedItems.Count > 0);

			ToolStripMenuItem_Domains_Modify.Enabled		= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_Delete.Enabled		= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_IPv4_Enable.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_IPv4_Disable.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_IPv6_Enable.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_IPv6_Disable.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_CopyText.Enabled		= (listView_Domains.SelectedItems.Count > 0);
		}

		/*==============================================================
		 * 调整列宽
		 *==============================================================*/
		private void listView_Domains_Resize(object sender, EventArgs e)
		{
			int[] widths = { 0, 198, 60, 102, 273, 66 };

			for(int i = 1; i < widths.Length; ++i)
				listView_Domains.Columns[i].Width = widths[i];

			columnHeader_Domains_Domain.Width = listView_Domains.Width - 21 - widths.Sum();
		}

		/*==============================================================
		 * 双击修改
		 *==============================================================*/
		private void listView_Domains_DoubleClick(object sender, EventArgs e)
		{
			edit_domain();
		}
		#endregion
		#region 域名列表 - 上下文菜单
		/*==============================================================
		 * 添加
		 *==============================================================*/
		private void toolStripButton_Domains_Add_Click(object sender, EventArgs e)
		{
			frm_Domain dlg = new();

			if(dlg.ShowDialog() == DialogResult.OK)
			{
				add_LVI__Domain(dlg.m_domain);
				CONFIG.m_s_dirty = true;
			}
		}

		/*==============================================================
		 * 修改
		 *==============================================================*/
		private void toolStripButton_Domains_Modify_Click(object sender, EventArgs e)
		{
			edit_domain();
		}

		/*==============================================================
		 * 删除
		 *==============================================================*/
		private void toolStripButton_Domains_Delete_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	$"是否要删除选定的 {listView_Domains.SelectedItems.Count} 条记录？",
								this.Text,
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question,
								MessageBoxDefaultButton.Button2 ) == DialogResult.No)
				return;

			while(listView_Domains.SelectedItems.Count > 0)
				listView_Domains.Items.Remove(listView_Domains.SelectedItems[0]);

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 允许更新 IPv4
		 *==============================================================*/
		private void toolStripButton_Domains_IPv4_Enable_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem LVI in listView_Domains.SelectedItems)
			{
				ddns_lib.c_Domain domain = Get_LVI_Tag__Domain(LVI);
				domain.IPv4.m_enabled = true;

				update_LVI__Domain(LVI);

				CONFIG.m_s_dirty = true;
			}	// for
		}

		/*==============================================================
		 * 允许更新 IPv6
		 *==============================================================*/
		private void toolStripButton_Domains_IPv6_Enable_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem LVI in listView_Domains.SelectedItems)
			{
				ddns_lib.c_Domain domain = Get_LVI_Tag__Domain(LVI);
				domain.IPv6.m_enabled = true;

				update_LVI__Domain(LVI);

				CONFIG.m_s_dirty = true;
			}	// for
		}

		/*==============================================================
		 * 禁止更新 IPv4
		 *==============================================================*/
		private void toolStripButton_Domains_IPv4_Disable_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem LVI in listView_Domains.SelectedItems)
			{
				ddns_lib.c_Domain domain = Get_LVI_Tag__Domain(LVI);
				domain.IPv4.m_enabled = false;

				update_LVI__Domain(LVI);

				CONFIG.m_s_dirty = true;
			}	// for
		}

		/*==============================================================
		 * 禁止更新 IPv6
		 *==============================================================*/
		private void toolStripButton_Domains_IPv6_Disable_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem LVI in listView_Domains.SelectedItems)
			{
				ddns_lib.c_Domain domain = Get_LVI_Tag__Domain(LVI);
				domain.IPv6.m_enabled = false;

				update_LVI__Domain(LVI);

				CONFIG.m_s_dirty = true;
			}	// for
		}

		/*==============================================================
		 * 复制文本
		 *==============================================================*/
		private void toolStripButton_Domains_CopyText_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new();

			foreach(ListViewItem LVI in listView_Domains.SelectedItems)
			{
				for(int i = 0; i < listView_Domains.Columns.Count; ++i)
					sb.Append($"{LVI.SubItems[i].Text}\t");

				sb.AppendLine();
			}	// for

			Clipboard.SetText(sb.ToString());
		}

		/*==============================================================
		 * 添加
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_Add_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_Add.PerformClick();
		}

		/*==============================================================
		 * 修改
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_Modify_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_Modify.PerformClick();
		}

		/*==============================================================
		 * 删除
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_Delete_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_Delete.PerformClick();
		}

		/*==============================================================
		 * 允许更新 IPv4
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_IPv4_Enable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_IPv4_Enable.PerformClick();
		}

		/*==============================================================
		 * 允许更新 IPv6
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_IPv6_Enable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_IPv6_Enable.PerformClick();
		}

		/*==============================================================
		 * 禁止更新 IPv4
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_IPv4_Disable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_IPv4_Disable.PerformClick();
		}

		/*==============================================================
		 * 禁止更新 IPv6
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_IPv6_Disable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_IPv6_Disable.PerformClick();
		}

		/*==============================================================
		 * 复制文本
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_CopyText_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_CopyText.PerformClick();
		}
		#endregion

		#region 日志
		/*==============================================================
		 * 添加日志记录（UI 线程安全）
		 *==============================================================*/
		internal void add_log(string txt, Color c = default)
		{
			invoke(() =>
			{
				ListViewItem LVI = new();

				while(LVI.SubItems.Count < listView_Logs.Columns.Count)
					LVI.SubItems.Add("");

				LVI.SubItems[(int)e_Column_Log.Time].Text	= DateTime.Now.ToString("G").Replace("/", ".");
				LVI.SubItems[(int)e_Column_Log.Log].Text	= txt;

				LVI.ForeColor								= c;

				listView_Logs.Items.Add(LVI);

				LVI.EnsureVisible();

				while(listView_Logs.Items.Count > CONFIG.LOG.m_s_MaxLines)
				{
					listView_Logs.Items.RemoveAt(0);
					--m_next_save_log_idx;
				}	// while
			});
		}

		/*==============================================================
		 * 选择项改变
		 *==============================================================*/
		private void listView_Logs_SelectedIndexChanged(object sender, EventArgs e)
		{
			ToolStripMenuItem_Logs_Copy.Enabled		= (listView_Logs.SelectedItems.Count > 0);
			ToolStripMenuItem_Logs_Delete.Enabled	= (listView_Logs.SelectedItems.Count > 0);
		}

		/*==============================================================
		 * 调整列宽
		 *==============================================================*/
		private void listView_Logs_Resize(object sender, EventArgs e)
		{
			int[] widths = { 122, 0 };

			for(int i = 0; i < widths.Length - 1; ++i)
				listView_Logs.Columns[i].Width = widths[i];

			columnHeader_Logs_Log.Width = listView_Logs.Width - 21 - widths.Sum();
		}

		/*==============================================================
		 * 最大显示行数
		 *==============================================================*/
		private void numericUpDown_Logs_MaxLines_ValueChanged(object sender, EventArgs e)
		{
			CONFIG.LOG.m_s_MaxLines = (int)numericUpDown_Logs_MaxLines.Value;
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 保存到日志文件
		 *==============================================================*/
		private void checkBox_Logs__Save_To_File_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.LOG.m_s_Save_To_File = checkBox_Logs__Save_To_File.Checked;
			CONFIG.m_s_dirty = true;
		}
		#endregion
		#region 日志 - 上下文菜单
		/*==============================================================
		 * 复制文本
		 *==============================================================*/
		private void ToolStripMenuItem_Logs_Copy_Click(object sender, EventArgs e)
		{
			if(listView_Logs.SelectedItems.Count == 0)
				return;

			StringBuilder sb = new();

			foreach(ListViewItem LVI in listView_Logs.SelectedItems)
				sb.AppendLine($"{LVI.SubItems[(int)e_Column_Log.Time].Text}\t{LVI.SubItems[(int)e_Column_Log.Log].Text}");

			Clipboard.SetText(sb.ToString());
		}

		/*==============================================================
		 * 删除选定记录
		 *==============================================================*/
		private void ToolStripMenuItem_Logs_Delete_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	$"是否要删除选定的 {listView_Logs.SelectedItems.Count} 条记录？",
								this.Text,
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question,
								MessageBoxDefaultButton.Button2 ) == DialogResult.No)
				return;

			while(listView_Logs.SelectedItems.Count > 0)
				listView_Logs.Items.Remove(listView_Logs.SelectedItems[0]);
		}

		/*==============================================================
		 * 全选
		 *==============================================================*/
		private void ToolStripMenuItem_Logs_SelectAll_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem LVI in listView_Logs.Items)
				LVI.Selected = true;
		}
		#endregion

		#region 更新方式
		/*==============================================================
		 * 更新方式改变
		 *==============================================================*/
		private void radioButton_Settings_Type__CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_update_type = (radioButton_Settings_Type__Local.Checked ? CONFIG.e_Update_Type.Local : CONFIG.e_Update_Type.Remote);

			textBox_Settings_RemoteServer__Addr.ReadOnly	= (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local);
			textBox_Settings_RemoteServer__User.ReadOnly	= (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local);
			textBox_Settings_RemoteServer__Pwd.ReadOnly		= (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local);

			UpdateUI_radioButton_Settings_IP__Accept_IP();

			Update_Settings_Preview__Update_Type();

			CONFIG.m_s_dirty = true;
		}
		#endregion

		#region 远程 Server 设置
		/*==============================================================
		 * Server 地址/端口
		 *==============================================================*/
		private void textBox_Settings_RemoteServer__Addr_TextChanged(object sender, EventArgs e)
		{
			CONFIG.REMOTE_SERVER.m_s_addr = textBox_Settings_RemoteServer__Addr.Text.Trim();

			UpdateUI_radioButton_Settings_IP__Accept_IP();
			Update_Settings_Preview__Update_Type();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 登录到 Server 的用户名
		 *==============================================================*/
		private void textBox_Settings_RemoteServer__User_TextChanged(object sender, EventArgs e)
		{
			CONFIG.REMOTE_SERVER.m_s_user = textBox_Settings_RemoteServer__User.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 登录到 Server 的密码
		 *==============================================================*/
		private void textBox_Settings_RemoteServer__Pwd_TextChanged(object sender, EventArgs e)
		{
			CONFIG.REMOTE_SERVER.m_s_pwd = textBox_Settings_RemoteServer__Pwd.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 显示密码
		 *==============================================================*/
		private void checkBox_Settings_RemoteServer__Pwd_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Settings_RemoteServer__Pwd.PasswordChar = checkBox_Settings_RemoteServer__Pwd.Checked ? '\0' : '*';

			CONFIG.REMOTE_SERVER.m_s_show_pwd = checkBox_Settings_RemoteServer__Pwd.Checked;
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 自动 ping 服务器
		 *==============================================================*/
		private void checkBox_Settings_RemoteServer__Ping_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.REMOTE_SERVER.m_s_auto_ping = checkBox_Settings_RemoteServer__Ping.Checked;
			CONFIG.m_s_dirty = true;
		}
		#endregion

		#region 设置 IP
		/*==============================================================
		 * 更新 UI（Server 接受连接的客户端 IP）
		 *==============================================================*/
		void UpdateUI_radioButton_Settings_IP__Accept_IP()
		{
			void auto_reset_radioButton_IPv4()
			{
				if(CONFIG.SET_IP.m_s_type_IPv4 == CONFIG.e_IP_Get_Type.Server_Accept_IP)
				{
					radioButton_Settings_IPv4__From_URL.Checked		= true;
					radioButton_Settings_IPv4__Accept_IP.Enabled	= false;
				}
			}

			void auto_reset_radioButton_IPv6()
			{
				if(CONFIG.SET_IP.m_s_type_IPv6 == CONFIG.e_IP_Get_Type.Server_Accept_IP)
				{
					radioButton_Settings_IPv6__From_URL.Checked		= true;
					radioButton_Settings_IPv6__Accept_IP.Enabled	= false;
				}
			}

			if(CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local)
			{
				auto_reset_radioButton_IPv4();
				auto_reset_radioButton_IPv6();

				radioButton_Settings_IPv4__Accept_IP.Enabled	= false;
				radioButton_Settings_IPv6__Accept_IP.Enabled	= false;
				return;
			}

			IPAddress?	server_ip;
			ushort		server_port;

			if(!CONFIG.get_server_ip_port(out server_ip, out server_port))
			{
				auto_reset_radioButton_IPv4();
				auto_reset_radioButton_IPv6();
				return;
			}

			switch(server_ip!.AddressFamily)
			{
			case System.Net.Sockets.AddressFamily.InterNetwork:
				auto_reset_radioButton_IPv6();

				radioButton_Settings_IPv4__Accept_IP.Enabled = (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote);
				break;

			case System.Net.Sockets.AddressFamily.InterNetworkV6:
				auto_reset_radioButton_IPv4();

				radioButton_Settings_IPv6__Accept_IP.Enabled = (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote);
				break;
			}	// switch
		}

		/*==============================================================
		 * 改变【设置 IPv4】
		 *==============================================================*/
		private void radioButton_Settings_IPv4__CheckedChanged(object sender, EventArgs e)
		{
			if(radioButton_Settings_IPv4__From_URL.Checked)
				CONFIG.SET_IP.m_s_type_IPv4 = CONFIG.e_IP_Get_Type.Get_IP_From_URL;
			else if(radioButton_Settings_IPv4__Manual.Checked)
				CONFIG.SET_IP.m_s_type_IPv4 = CONFIG.e_IP_Get_Type.Manual_IP;
			else if(radioButton_Settings_IPv4__Accept_IP.Checked)
				CONFIG.SET_IP.m_s_type_IPv4 = CONFIG.e_IP_Get_Type.Server_Accept_IP;

			comboBox_Settings_IPv4__From_URL.Enabled	= (CONFIG.SET_IP.m_s_type_IPv4 == CONFIG.e_IP_Get_Type.Get_IP_From_URL);
			textBox_Settings_IPv4.ReadOnly				= (CONFIG.SET_IP.m_s_type_IPv4 != CONFIG.e_IP_Get_Type.Manual_IP);

			Update_Settings_Preview__Set_IPv4();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 通过互联网获取公网 IPv4
		 *==============================================================*/
		private void comboBox_Settings_IPv4__From_URL_SelectedIndexChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_Get_IPv4_URL = comboBox_Settings_IPv4__From_URL.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 手动指定 IPv4
		 *==============================================================*/
		private void textBox_Settings_IPv4_TextChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_IPv4 = textBox_Settings_IPv4.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 改变【设置 IPv6】
		 *==============================================================*/
		private void radioButton_Settings_IPv6__CheckedChanged(object sender, EventArgs e)
		{
			if(radioButton_Settings_IPv6__From_URL.Checked)
				CONFIG.SET_IP.m_s_type_IPv6 = CONFIG.e_IP_Get_Type.Get_IP_From_URL;
			else if(radioButton_Settings_IPv6__Manual.Checked)
				CONFIG.SET_IP.m_s_type_IPv6 = CONFIG.e_IP_Get_Type.Manual_IP;
			else if(radioButton_Settings_IPv6__Accept_IP.Checked)
				CONFIG.SET_IP.m_s_type_IPv6 = CONFIG.e_IP_Get_Type.Server_Accept_IP;

			comboBox_Settings_IPv6__From_URL.Enabled	= (CONFIG.SET_IP.m_s_type_IPv6 == CONFIG.e_IP_Get_Type.Get_IP_From_URL);
			textBox_Settings_IPv6.ReadOnly				= (CONFIG.SET_IP.m_s_type_IPv6 != CONFIG.e_IP_Get_Type.Manual_IP);

			Update_Settings_Preview__Set_IPv6();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 通过互联网获取公网 IPv6
		 *==============================================================*/
		private void comboBox_Settings_IPv6__From_URL_SelectedIndexChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_Get_IPv6_URL	= comboBox_Settings_IPv6__From_URL.Text.Trim();
			CONFIG.m_s_dirty				= true;
		}

		/*==============================================================
		 * 手动指定 IPv6
		 *==============================================================*/
		private void textBox_Settings_IPv6_TextChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_IPv6	= textBox_Settings_IPv6.Text.Trim();
			CONFIG.m_s_dirty		= true;
		}
		#endregion

		#region 安全设置
		/*==============================================================
		 * 获取当前选择的 profile
		 *==============================================================*/
		ddns_lib.c_Security_Profile? get_current_security_profile()
		{
			if(listView_Security.SelectedItems.Count == 0)
				return null;

			return Get_LVI_Tag__Security_Profile(listView_Security.SelectedItems[0]);
		}

		/*==============================================================
		 * 选项改变
		 *==============================================================*/
		private void listView_Security_SelectedIndexChanged(object sender, EventArgs e)
		{
			button_Security_Del.Enabled				= (listView_Security.SelectedItems.Count > 0);
			ToolStripMenuItem__Security_Del.Enabled	= button_Security_Del.Enabled;

			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			bool dirty = CONFIG.m_s_dirty;

			if(profile == null)
			{
				textBox_Security__Property__Name.Clear();
				textBox_Security__Property__Name.ReadOnly	= true;

				textBox_Security_Godaddy__Key.Clear();
				textBox_Security_Godaddy__Key.ReadOnly		= true;

				textBox_Security_Godaddy__Secret.Clear();
				textBox_Security_Godaddy__Secret.ReadOnly	= true;

				textBox_Security_dynv6__token.Clear();
				textBox_Security_dynv6__token.ReadOnly		= true;

				textBox_Security_dynu__API_Key.Clear();
				textBox_Security_dynu__API_Key.ReadOnly		= true;

				checkBox_Security__Save_To_Config.Enabled	= false;
			}
			else
			{
				textBox_Security__Property__Name.Text		= profile.m_Name;
				textBox_Security__Property__Name.ReadOnly	= false;

				textBox_Security_Godaddy__Key.Text			= profile.m_Godaddy__Key;
				textBox_Security_Godaddy__Key.ReadOnly		= false;

				checkBox_Security_Godaddy__Key.Checked		= profile.m_Godaddy__Key_Visible;

				textBox_Security_Godaddy__Secret.Text		= profile.m_Godaddy__Secret;
				textBox_Security_Godaddy__Secret.ReadOnly	= false;

				checkBox_Security_Godaddy__Secret.Checked	= profile.m_Godaddy__Secret_Visible;

				textBox_Security_dynv6__token.Text			= profile.m_dynv6__token;
				textBox_Security_dynv6__token.ReadOnly		= false;

				checkBox_Security_dynv6__token.Checked		= profile.m_dynv6__token_Visible;

				textBox_Security_dynu__API_Key.Text			= profile.m_dynu__API_Key;
				textBox_Security_dynu__API_Key.ReadOnly		= false;

				checkBox_Security_dynu__API_Key.Checked		= profile.m_dynu__API_Key_Visible;

				checkBox_Security__Save_To_Config.Enabled	= true;
				checkBox_Security__Save_To_Config.Checked	= profile.m_Save_To_Config;
			}

			CONFIG.m_s_dirty = dirty;
		}

		/*==============================================================
		 * 调整列宽
		 *==============================================================*/
		private void listView_Security_Resize(object sender, EventArgs e)
		{
			columnHeader_Security.Width = listView_Security.Width - 21;
		}

		/*==============================================================
		 * 添加
		 *==============================================================*/
		private void button_Security_Add_Click(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile profile = new();
			profile.m_Name = $"{CONFIG.SECURITY.m_s_profiles.Count + 1}";

			CONFIG.SECURITY.m_s_profiles.Add(profile);

			ListViewItem LVI = new(profile.m_Name);
			listView_Security.Items.Add(LVI);

			LVI.Selected = true;
			LVI.EnsureVisible();

			Set_LVI_Tag__Security_Profile(LVI, profile);
			Update_Settings_Preview__Security();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 删除
		 *==============================================================*/
		private void button_Security_Del_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	$"是否要删除「{listView_Security.SelectedItems[0].Text}」？",
								this.Text,
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question,
								MessageBoxDefaultButton.Button2 ) == DialogResult.No)
				return;

			int							idx		= listView_Security.SelectedIndices[0];
			ListViewItem				LVI		= listView_Security.Items[idx];
			ddns_lib.c_Security_Profile	profile	= Get_LVI_Tag__Security_Profile(LVI);

			CONFIG.SECURITY.m_s_profiles.RemoveAt(idx);
			listView_Security.Items.RemoveAt(idx);

			// 删除域名的 profile
			foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
			{
				if(domain.m_Security_Profile == profile)
					domain.m_Security_Profile = null;
			}	// for

			Update_Settings_Preview__Security();

			// 更新域名列表
			update_All_LVI__Domain();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - 配置的名称
		 *==============================================================*/
		private void textBox_Security__Property__Name_TextChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			profile.m_Name							= textBox_Security__Property__Name.Text;
			listView_Security.SelectedItems[0].Text	= textBox_Security__Property__Name.Text;

			// 更新域名列表
			update_All_LVI__Domain();

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - API 网址
		 *==============================================================*/
		private void linkLabel_Security__LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start((sender as LinkLabel)!.Text);
		}

		/*==============================================================
		 * 属性 - [Godaddy] Key
		 *==============================================================*/
		private void textBox_Security_Godaddy__Key_TextChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			profile.m_Godaddy__Key = textBox_Security_Godaddy__Key.Text;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - [Godaddy] Secret
		 *==============================================================*/
		private void textBox_Security_Godaddy__Secret_TextChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			profile.m_Godaddy__Secret = textBox_Security_Godaddy__Secret.Text;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - [Godaddy] 显示 Key
		 *==============================================================*/
		private void checkBox_Security_Godaddy__Key_CheckedChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			textBox_Security_Godaddy__Key.PasswordChar	= checkBox_Security_Godaddy__Key.Checked ? '\0' : '*';
			profile.m_Godaddy__Key_Visible				= checkBox_Security_Godaddy__Key.Checked;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - [Godaddy] 显示 Secret
		 *==============================================================*/
		private void checkBox_Security_Godaddy__Secret_CheckedChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			textBox_Security_Godaddy__Secret.PasswordChar	= checkBox_Security_Godaddy__Secret.Checked ? '\0' : '*';
			profile.m_Godaddy__Secret_Visible				= checkBox_Security_Godaddy__Secret.Checked;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - [dynv6] token
		 *==============================================================*/
		private void textBox_Security_dynv6__token_TextChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			profile.m_dynv6__token = textBox_Security_dynv6__token.Text;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - [dynv6] 显示 token
		 *==============================================================*/
		private void checkBox_Security_dynv6__token_CheckedChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			textBox_Security_dynv6__token.PasswordChar	= checkBox_Security_dynv6__token.Checked ? '\0' : '*';
			profile.m_dynv6__token_Visible				= checkBox_Security_dynv6__token.Checked;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - [dynu] API_Key
		 *==============================================================*/
		private void textBox_Security_dynu__API_Key_TextChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			profile.m_dynu__API_Key = textBox_Security_dynu__API_Key.Text;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - [dynu] 显示 API_Key
		 *==============================================================*/
		private void checkBox_Security_dynu__API_Key_CheckedChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			textBox_Security_dynu__API_Key.PasswordChar	= checkBox_Security_dynu__API_Key.Checked ? '\0' : '*';
			profile.m_dynu__API_Key_Visible				= checkBox_Security_dynu__API_Key.Checked;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - 保存到 Config 文件
		 *==============================================================*/
		private void checkBox_Security__Save_To_Config_CheckedChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			profile.m_Save_To_Config = checkBox_Security__Save_To_Config.Checked;

			CONFIG.m_s_dirty = true;
		}
		#endregion
		#region 安全设置 - 上下文菜单
		/*==============================================================
		 * 添加
		 *==============================================================*/
		private void ToolStripMenuItem_Security_Add_Click(object sender, EventArgs e)
		{
			button_Security_Add.PerformClick();
		}

		/*==============================================================
		 * 删除
		 *==============================================================*/
		private void ToolStripMenuItem__Security_Del_Click(object sender, EventArgs e)
		{
			button_Security_Del.PerformClick();
		}
		#endregion

		#region 更新操作
		/*==============================================================
		 * 更新域名的IP
		 *==============================================================*/
		private void checkBox_Action_UpdateIP_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_UpdateIP = checkBox_Action_UpdateIP.Checked;

			Update_Settings_Preview__UpdateIP();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 自动执行操作的时间间隔（秒）
		 *==============================================================*/
		private void checkBox_Action_AutoAction_Interval_CheckedChanged(object sender, EventArgs e)
		{
			numericUpDown_Action_AutoAction_Interval.Enabled	= checkBox_Action_AutoAction_Interval.Checked;
			timer_Update.Enabled								= checkBox_Action_AutoAction_Interval.Checked;

			CONFIG.ACTION.m_s_AutoAction						= checkBox_Action_AutoAction_Interval.Checked;

			Update_Settings_Preview__AutoUpdate();

			CONFIG.m_s_dirty = true;
		}
		//--------------------------------------------------
		private void numericUpDown_Action_AutoAction_Interval_ValueChanged(object sender, EventArgs e)
		{
			CONFIG.ACTION.m_s_AutoAction_interval = (uint)numericUpDown_Action_AutoAction_Interval.Value;

			Update_Settings_Preview__AutoUpdate();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * IP变动时，才执行更新（先解析域名）
		 *==============================================================*/
		private void checkBox_Action_DNS_Lookup_First_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First = checkBox_Action_DNS_Lookup_First.Checked;

			Update_Settings_Preview__DNS_Lookup_First();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 设定解析域名的DNS服务器
		 *==============================================================*/
		private void checkBox_Action_Use_Custom_DNS_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Action_Custom_DNS_List.ReadOnly	= !checkBox_Action_Use_Custom_DNS.Checked;

			CONFIG.UPDATE_ACTION.m_s_Use_Custom_DNS	= checkBox_Action_Use_Custom_DNS.Checked;

			Update_Settings_Preview__DNS_Server();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * DNS服务器列表
		 *==============================================================*/
		private void textBox_Action_Custom_DNS_List_TextChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_Custom_DNS_List.Clear();
			CONFIG.UPDATE_ACTION.m_s_Custom_DNS_List.AddRange(textBox_Action_Custom_DNS_List.Lines);

			Update_Settings_Preview__DNS_Server();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 自动更新超时（单位：秒。0 = 无限等待）
		 *==============================================================*/
		private void numericUpDown_Action_Timeout_ValueChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_Timeout = (int)numericUpDown_Action_Timeout.Value;

			Update_Settings_Preview__timeout();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * IP变动时，弹出提示窗口
		 *==============================================================*/
		private void checkBox_Action_IP_Change_Popup_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_IP_Change_Popup = checkBox_Action_IP_Change_Popup.Checked;
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * IP变动时，播放音乐
		 *==============================================================*/
		private void checkBox_Action_IP_Change_PlaySound_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_IP_Change_Play_Sound = checkBox_Action_IP_Change_PlaySound.Checked;
			CONFIG.m_s_dirty = true;
		}
		//--------------------------------------------------
		private void textBox_Action_IP_Change_PlaySound_TextChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_IP_Change_Sound_Path = textBox_Action_IP_Change_PlaySound.Text;
			CONFIG.m_s_dirty = true;
		}
		//--------------------------------------------------
		private void button_Action_IP_Change_PlaySound_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new();

			string? dir = Path.GetDirectoryName(textBox_Action_IP_Change_PlaySound.Text);

			if(dir == null)
				dir = "";

			dlg.InitialDirectory	= Path.GetFullPath(Directory.Exists(dir) ? dir : "Sound");
			dlg.Filter				= "音频文件 (*.wav;*.mid;*.mp3)|*.wav;*.mid;*.mp3|所有文件 (*.*)|*.*";
			dlg.RestoreDirectory	= true;

			if(dlg.ShowDialog() == DialogResult.OK)
				textBox_Action_IP_Change_PlaySound.Text = PATH.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, dlg.FileName);
		}
		//--------------------------------------------------
		private void button_Action_IP_Change_StopSound_Click(object sender, EventArgs e)
		{
			SOUND.Stop();
		}
		#endregion

		#region 修正 hosts
		/*==============================================================
		 * 打开目录
		 *==============================================================*/
		private void button_Fix_hosts__Path_Browser_Click(object sender, EventArgs e)
		{
			string dir = textBox_Fix_hosts__Path.Text.Substring(0, textBox_Fix_hosts__Path.Text.LastIndexOf("\\"));
			Process.Start("explorer.exe", dir);
		}
		#endregion

		#region 预览设置
		/*==============================================================
		 * 更新方式
		 *==============================================================*/
		void Update_Settings_Preview__Update_Type()
		{
			if(CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local)
				label_Settings_Preview__Update_Type_Val.Text = "本地更新（直连）";
			else
				label_Settings_Preview__Update_Type_Val.Text = CONFIG.REMOTE_SERVER.m_s_addr;
		}

		/*==============================================================
		 * Ping
		 *==============================================================*/
		void Update_Settings_Preview__Ping()
		{
			label_Settings_Preview__Ping_Val.Text = m_s_Mainform.textBox_Settings_RemoteServer__Ping.Text;
		}

		/*==============================================================
		 * 设置 IPv4
		 *==============================================================*/
		void Update_Settings_Preview__Set_IPv4()
		{
			switch(CONFIG.SET_IP.m_s_type_IPv4)
			{
			case CONFIG.e_IP_Get_Type.Get_IP_From_URL:
				label_Settings_Preview__Set_IPv4_Val.Text = "通过 URL 获取";
				break;

			case CONFIG.e_IP_Get_Type.Manual_IP:
				label_Settings_Preview__Set_IPv4_Val.Text = "手动指定 IP";
				break;

			case CONFIG.e_IP_Get_Type.Server_Accept_IP:
				label_Settings_Preview__Set_IPv4_Val.Text = "Server 接受连接的 IP";
				break;
			}	// switch
		}

		/*==============================================================
		 * 设置 IPv6
		 *==============================================================*/
		void Update_Settings_Preview__Set_IPv6()
		{
			switch(CONFIG.SET_IP.m_s_type_IPv6)
			{
			case CONFIG.e_IP_Get_Type.Get_IP_From_URL:
				label_Settings_Preview__Set_IPv6_Val.Text = "通过 URL 获取";
				break;

			case CONFIG.e_IP_Get_Type.Manual_IP:
				label_Settings_Preview__Set_IPv6_Val.Text = "手动指定 IP";
				break;

			case CONFIG.e_IP_Get_Type.Server_Accept_IP:
				label_Settings_Preview__Set_IPv6_Val.Text = "Server 接受连接的 IP";
				break;
			}	// switch
		}

		/*==============================================================
		 * 安全设置
		 *==============================================================*/
		void Update_Settings_Preview__Security()
		{
			label_Settings_Preview__Security_Val.Text = $"{CONFIG.SECURITY.m_s_profiles.Count} 个配置文件";
		}

		/*==============================================================
		 * 更新域名 IP
		 *==============================================================*/
		void Update_Settings_Preview__UpdateIP()
		{
			label_Settings_Preview__Action_UpdateIP_Val.Text = CONFIG.UPDATE_ACTION.m_s_UpdateIP ? COMMON.STR_TRUE : COMMON.STR_FALSE;
		}

		/*==============================================================
		 * 自动更新
		 *==============================================================*/
		void Update_Settings_Preview__AutoUpdate()
		{
			label_Settings_Preview__Action_AutoUpdate_Val.Text = CONFIG.ACTION.m_s_AutoAction ? $"每 {CONFIG.ACTION.m_s_AutoAction_interval}s" : COMMON.STR_FALSE;
		}

		/*==============================================================
		 * 先解析域名
		 *==============================================================*/
		void Update_Settings_Preview__DNS_Lookup_First()
		{
			label_Settings_Preview__DNS_Lookup_First_Val.Text = CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First ? COMMON.STR_TRUE : COMMON.STR_FALSE;
		}

		/*==============================================================
		 * DNS 服务器
		 *==============================================================*/
		void Update_Settings_Preview__DNS_Server()
		{
			label_Settings_Preview__DNS_Server_Val.Text = CONFIG.UPDATE_ACTION.m_s_Use_Custom_DNS ? "自定义" : "系统默认";
		}

		/*==============================================================
		 * 更新超时
		 *==============================================================*/
		void Update_Settings_Preview__timeout()
		{
			label_Settings_Preview__Timeout_Val.Text = CONFIG.UPDATE_ACTION.m_s_Timeout.ToString();
		}
		#endregion

		#region 更新
		// 全部待更新的域名数量
		int				m_all_domains_for_update	= 0;

		List<string>	m_DNS_List					= new();

		/*==============================================================
		 * 执行更新操作
		 *==============================================================*/
		private void button_Update_Click(object sender, EventArgs e)
		{
			update__start_update();
		}

		/*==============================================================
		 * 设置下次自动更新的时间
		 *==============================================================*/
		void set_next_Auto_Update_Time()
		{
			m_can_auto_update_time = DateTime.Now.AddSeconds((int)numericUpDown_Action_AutoAction_Interval.Value);

			add_log($"下次自动更新时间：{m_can_auto_update_time.ToString("G").Replace("/", "-")}", Color.FromArgb(0, 162, 232));
		}

		/*==============================================================
		 * 开始更新 A/AAAA 记录
		 *==============================================================*/
		void update__start_update()
		{
			if(m_is_updating)
				return;

			m_is_updating = true;
			lock_controls(false);

			Thread th = new(update__do_update);
			th.Start();
		}

		class c_SetIP_th
		{
			internal CONFIG.e_IP_Get_Type				m_type;
			internal required ComboBox					m_ComboBox;
			internal required string					m_exe_Arguments;
			internal System.Net.Sockets.AddressFamily	m_af;
			internal required string					m_ip_type;
			internal required TextBox					m_textBox_Settings_IP;

			internal bool								m_get_ip_ok	= false;
			internal Thread								m_Thread	= null!;
		};

		/*==============================================================
		 * 设置即将更新的 IP 地址
		 *==============================================================*/
		bool update__Set_IP()
		{
			CONFIG.SET_IP.m_s_IPv4 = textBox_Settings_IPv4.Text;
			CONFIG.SET_IP.m_s_IPv6 = textBox_Settings_IPv6.Text;

			if(	CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote	&&
				!ddns_tool_CLR.CLR.is_connected() )
			{
				if(CONFIG.SET_IP.m_s_type_IPv4 == CONFIG.e_IP_Get_Type.Server_Accept_IP)
					CONFIG.SET_IP.m_s_IPv4 = "";

				if(CONFIG.SET_IP.m_s_type_IPv6 == CONFIG.e_IP_Get_Type.Server_Accept_IP)
					CONFIG.SET_IP.m_s_IPv6 = "";
			}

			var threads = new c_SetIP_th[]
			{
				new c_SetIP_th { m_type = CONFIG.SET_IP.m_s_type_IPv4,	m_ComboBox = comboBox_Settings_IPv4__From_URL,	m_exe_Arguments = " v4",	m_af = System.Net.Sockets.AddressFamily.InterNetwork,	m_ip_type = "IPv4",	m_textBox_Settings_IP = textBox_Settings_IPv4,	},
				new c_SetIP_th { m_type = CONFIG.SET_IP.m_s_type_IPv6,	m_ComboBox = comboBox_Settings_IPv6__From_URL,	m_exe_Arguments = "",		m_af = System.Net.Sockets.AddressFamily.InterNetworkV6,	m_ip_type = "IPv6",	m_textBox_Settings_IP = textBox_Settings_IPv6,	},
			};

			// 获取 IP
			foreach(var th in threads)
			{
				if(th.m_type != CONFIG.e_IP_Get_Type.Get_IP_From_URL)
					continue;

				string url = (string)this.Invoke((Func<string>)delegate
				{
					if(th.m_ComboBox.Text.Length == 0)
						th.m_ComboBox.SelectedIndex = 0;

					return th.m_ComboBox.Text;
				});

				const string k_GET_IP_EXE = "get_ip_from_URL.exe";
				if(!File.Exists(k_GET_IP_EXE))
				{
					add_log($"[Error] 找不到 {k_GET_IP_EXE}", Color.Red);
					return false;
				}

				add_log($"正在获取当前公网 {th.m_ip_type} 地址……");

				// 由于 ServicePointManager 缓存问题，在应用程序生命周期无法切换 IP 地址族，这里使用外部进程获取 IP
				th.m_Thread = new(() =>
				{
					ProcessStartInfo psi = new();
					psi.FileName				= k_GET_IP_EXE;
					psi.Arguments				= url + th.m_exe_Arguments;
					psi.RedirectStandardOutput	= true;
					psi.RedirectStandardError	= true;
					psi.UseShellExecute			= false;
					psi.CreateNoWindow			= true;

					Process p = Process.Start(psi)!;

					StreamReader reader		= p.StandardOutput;
					StreamReader reader_err	= p.StandardError;

					p.WaitForExit();
					p.Close();

					string? ip = reader.ReadLine();

					if(	!string.IsNullOrEmpty(ip)					&&
						IPAddress.TryParse(ip, out IPAddress? addr)	&&
						addr.AddressFamily == th.m_af )
					{
						add_log($"通过互联网获取公网 {th.m_ip_type} 成功 ({ip})");
						th.m_get_ip_ok = true;

						invoke(() =>
						{
							th.m_textBox_Settings_IP.Text = ip.Trim();
						});
					}
					else
					{
						ip = "";
						add_log($"[Error] 通过互联网获取公网 {th.m_ip_type} 失败（{psi.FileName} {psi.Arguments}）", Color.Red);

						string? str_err = reader_err.ReadLine();
						if(!string.IsNullOrEmpty(str_err))
							add_log($"[Error] {str_err}（{psi.FileName} {psi.Arguments}）", Color.Red);
					}

					if(th.m_af == System.Net.Sockets.AddressFamily.InterNetwork)
						CONFIG.SET_IP.m_s_IPv4 = ip;
					else
						CONFIG.SET_IP.m_s_IPv6 = ip;
				});
			}	// for

			foreach(var th in threads)
			{
				if(th.m_Thread != null)
					th.m_Thread.Start();
			}	// for

			bool get_ip_ok = true;

			foreach(var th in threads)
			{
				if(th.m_Thread != null)
				{
					th.m_Thread.Join();

					if(!th.m_get_ip_ok)
						get_ip_ok = false;
				}
			}	// for

			return get_ip_ok;
		}

		/*==============================================================
		 * 执行更新
		 *==============================================================*/
		void update__do_update()
		{
			// 设置即将更新的 IP 地址
			if(!update__Set_IP())
			{
				update__done();
				return;
			}

			m_all_domains_for_update = 0;

			// 更新 IP
			foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
			{
				if(!domain.IPv4.m_enabled && !domain.IPv6.m_enabled)
					continue;

				domain.IPv4.m_input_IP	= CONFIG.SET_IP.m_s_IPv4;
				domain.IPv6.m_input_IP	= CONFIG.SET_IP.m_s_IPv6;

				++m_all_domains_for_update;

				EVENTS.On_Set_Progress(domain.m_domain, ddns_lib.e_Progress.Starting);
			}	// for

			if(CONFIG.UPDATE_ACTION.m_s_UpdateIP)
			{
				m_DNS_List.Clear();

				if(CONFIG.UPDATE_ACTION.m_s_Use_Custom_DNS)
				{
					foreach(string dns_server in CONFIG.UPDATE_ACTION.m_s_Custom_DNS_List)
					{
						if(dns_server.Substring(0, 2) == "//")
							continue;

						m_DNS_List.Add(dns_server);
					}	// for
				}

				switch(CONFIG.m_s_update_type)
				{
				case CONFIG.e_Update_Type.Local:
					ddns_lib.LIB.update_domains(CONFIG.m_s_domains_list,
												CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First,
												(m_DNS_List.Count == 0) ? null : m_DNS_List,
												CONFIG.UPDATE_ACTION.m_s_Timeout * 1000);

					update__done();
					break;

				case CONFIG.e_Update_Type.Remote:
					// 自动连接到服务器
					if(!ddns_tool_CLR.CLR.is_connected())
					{
						void reset_domains_status()
						{
							foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
							{
								if(!domain.IPv4.m_enabled && !domain.IPv6.m_enabled)
									continue;

								EVENTS.On_Set_Progress(domain.m_domain, ddns_lib.e_Progress.None);
							}	// for

							update__done();
						}

						IPAddress?	server_ip;
						ushort		server_port;

						if(!CONFIG.get_server_ip_port(out server_ip, out server_port))
						{
							if(server_ip == null)
							{
								add_log("[Error] Server 地址/端口 错误", Color.Red);
								reset_domains_status();
								return;
							}

							if(server_port == 0)
							{
								add_log("[Error] Server 端口错误", Color.Red);
								reset_domains_status();
								return;
							}
						}

						bool res = ddns_tool_CLR.CLR.Connect(	server_ip!.ToString(),
																server_port,
																CONFIG.REMOTE_SERVER.m_s_user,
																CONFIG.REMOTE_SERVER.m_s_pwd );
						if(!res)
						{
							add_log($"[Error] 连接到远程服务器 ({CONFIG.REMOTE_SERVER.m_s_addr}) 失败", Color.Red);
							reset_domains_status();
							return;
						}
					}
					else
					{
						if(m_login_server_done)
						{
							ddns_tool_CLR.CLR.send_Update_Domains(	CONFIG.m_s_domains_list,
																	CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First,
																	(m_DNS_List.Count == 0) ? null : m_DNS_List,
																	CONFIG.UPDATE_ACTION.m_s_Timeout * 1000 );
						}
						else
							update__done();
					}
					break;
				}	// switch
			}
			else
				update__done();
		}

		/*==============================================================
		 * 完成更新 A/AAAA 记录
		 *==============================================================*/
		void update__done()
		{
			int failed_count	= 0;
			int skip_count		= 0;

			// 是否已更新当前 IP（仅「远程更新」）
			bool update_current_IPv4_remote = false;
			bool update_current_IPv6_remote = false;

			// IP 发生变化的域名
			List<ddns_lib.c_Domain> IP_change_domains = new();

			foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
			{
				if(!domain.IPv4.m_enabled && !domain.IPv6.m_enabled)
					continue;

				if(	(domain.IPv4.m_enabled && !domain.IPv4.m_same_ip)	||
					(domain.IPv6.m_enabled && !domain.IPv6.m_same_ip) )
					IP_change_domains.Add(domain);

				if(domain.IPv4.m_err_msg.Length > 0 || domain.IPv6.m_err_msg.Length > 0)
				{
					++failed_count;

					EVENTS.On_Set_Progress(domain.m_domain, ddns_lib.e_Progress.Failed);

					if(domain.IPv4.m_err_msg.Length > 0)
						add_log($"[Error] {domain.m_domain} : 更新 IPv4 失败（{domain.IPv4.m_err_msg}）", Color.Red);

					if(domain.IPv6.m_err_msg.Length > 0)
						add_log($"[Error] {domain.m_domain} : 更新 IPv6 失败（{domain.IPv6.m_err_msg}）", Color.Red);
				}
				else
				{
					EVENTS.On_Set_Progress(domain.m_domain, ddns_lib.e_Progress.Done);

					if(	(domain.IPv4.m_enabled && !domain.IPv4.m_same_ip)	||
						(domain.IPv6.m_enabled && !domain.IPv6.m_same_ip) )
					{
						add_log($"{domain.m_domain} : 更新成功。IPv4 = {domain.IPv4.m_current_IP}, IPv6 = {domain.IPv6.m_current_IP}", Color.Green);

						if(!update_current_IPv4_remote)
						{
							if(	CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote	&&
								CONFIG.SET_IP.m_s_type_IPv4 == CONFIG.e_IP_Get_Type.Server_Accept_IP )
							{
								update_current_IPv4_remote = true;

								CONFIG.SET_IP.m_s_IPv4	= domain.IPv4.m_current_IP;

								invoke(() =>
								{
									textBox_Settings_IPv4.Text	= domain.IPv4.m_current_IP;
								});
							}
						}

						if(!update_current_IPv6_remote)
						{
							if(	CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote	&&
								CONFIG.SET_IP.m_s_type_IPv6 == CONFIG.e_IP_Get_Type.Server_Accept_IP )
							{
								update_current_IPv6_remote = true;

								CONFIG.SET_IP.m_s_IPv6	= domain.IPv6.m_current_IP;

								invoke(() =>
								{
									textBox_Settings_IPv6.Text	= domain.IPv6.m_current_IP;
								});
							}
						}
					}
					else
						++skip_count;
				}
			}	// for

			add_log($"{IP_change_domains.Count} 成功，{failed_count} 失败，{skip_count} 已跳过，{m_all_domains_for_update} 总计",
					(failed_count == 0) ? Color.DarkOrange : Color.Red);

			if(IP_change_domains.Count > 0)
			{
				// IP变动时，弹出提示窗口
				if(CONFIG.UPDATE_ACTION.m_s_IP_Change_Popup)
				{
					invoke(() =>
					{
						m_IP_Change_Popup.set_domains(IP_change_domains);
						FORMS.active_form(m_IP_Change_Popup);
					});
				}

				// IP变动时，播放音乐
				if(CONFIG.UPDATE_ACTION.m_s_IP_Change_Play_Sound)
				{
					invoke(() =>
					{
						SOUND.Stop();
						SOUND.Play(CONFIG.UPDATE_ACTION.m_s_IP_Change_Sound_Path);
					});
				}

				invoke(update_All_LVI__Domain);
				CONFIG.m_s_dirty = true;
			}

			// 设置下次自动更新的时间
			set_next_Auto_Update_Time();

			lock_controls(true);
			m_is_updating = false;
		}
		#endregion
	};
}	// namespace ddns_tool
