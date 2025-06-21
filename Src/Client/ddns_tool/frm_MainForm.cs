using System.Diagnostics;
using System.Net;
using System.Text;

namespace ddns_tool
{
	public partial class frm_MainForm : Form
	{
		#region ����/����
		enum e_Column_Domains
		{
			Domain,
			Type,	// [Godaddy] TTL = 600��[dynv6] �Զ�IPv4, �Զ�IPv6��[dynu] TTL = 600, ID = 12345678
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

		bool							m_exiting				= false;		// �Ƿ������˳�

		DateTime						m_can_auto_update_time	= DateTime.Now;	// �����Զ�ִ�и��µ�ʱ��
		bool							m_is_updating			= false;		// �Ƿ�����ִ�и���

		bool							m_login_server_done		= false;		// �ѳɹ���¼ Server

		int								m_next_save_log_idx		= 0;			// ��һ��������־������

		// (url, ֧��IPv4, ֧��IPv6)
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

		#region ����
		/*==============================================================
		 * ���� -> LVI
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
		 * ִ��ί��
		 *==============================================================*/
		public void invoke(Action func)
		{
			if(this.InvokeRequired)
				this.Invoke(func);
			else
				func();
		}

		/*==============================================================
		 * �����ؼ���UI �̰߳�ȫ��
		 *==============================================================*/
		void lock_controls(bool enabled)
		{
			invoke(() =>
			{
				//�����·�ʽ��
				radioButton_Settings_Type__Local.Enabled		= enabled;
				radioButton_Settings_Type__Remote.Enabled		= enabled;

				// Զ�� Server ����
				textBox_Settings_RemoteServer__Addr.ReadOnly	= !enabled || !radioButton_Settings_Type__Remote.Checked;
				textBox_Settings_RemoteServer__User.ReadOnly	= !enabled || !radioButton_Settings_Type__Remote.Checked;
				textBox_Settings_RemoteServer__Pwd.ReadOnly		= !enabled || !radioButton_Settings_Type__Remote.Checked;

				//������ IP��
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

				//����ȫ���á�
				textBox_Security_Godaddy__Key.ReadOnly			= !enabled;
				textBox_Security_Godaddy__Secret.ReadOnly		= !enabled;

				textBox_Security_dynv6__token.ReadOnly			= !enabled;

				textBox_Security_dynu__API_Key.ReadOnly			= !enabled;

				//�����²�����
				checkBox_Action_UpdateIP.Enabled				= enabled;
				checkBox_Action_DNS_Lookup_First.Enabled		= enabled;
				checkBox_Action_Use_Custom_DNS.Enabled			= enabled;
				textBox_Action_Custom_DNS_List.ReadOnly			= !enabled || !checkBox_Action_Use_Custom_DNS.Checked;
				numericUpDown_Action_Timeout.Enabled			= enabled;

				checkBox_Action_IP_Change_Popup.Enabled			= enabled;

				checkBox_Action_IP_Change_PlaySound.Enabled		= enabled;
				textBox_Action_IP_Change_PlaySound.ReadOnly		= !enabled || !checkBox_Action_IP_Change_PlaySound.Checked;
				button_Action_IP_Change_PlaySound.Enabled		= enabled;

				//�������б�
				toolStrip_Domains.Enabled						= enabled;

				//����־��¼��
				numericUpDown_Logs_MaxLines.Enabled				= enabled;

				//�������棩
				button_Update.Enabled							= enabled;
			});
		}
		#endregion

		#region ����¼�
		internal class EVENTS
		{
			/*==============================================================
			 * OnConnected
			 *==============================================================*/
			internal static void OnConnected(string ip, ushort port)
			{
				m_s_Mainform.add_log($"���ӵ� Server �ɹ� (client = {ip}:{port})", Color.Green);
			}

			/*==============================================================
			 * OnDisconnecting
			 *==============================================================*/
			internal static void OnDisconnecting()
			{
				m_s_Mainform.add_log("�ѶϿ� Server ������");
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

					m_s_Mainform.add_log("��¼�������ɹ�", Color.Green);

					ddns_tool_CLR.CLR.send_Update_Domains(	CONFIG.m_s_domains_list,
															CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First,
															(m_s_Mainform.m_DNS_List.Count == 0) ? null : m_s_Mainform.m_DNS_List,
															CONFIG.UPDATE_ACTION.m_s_Timeout * 1000 );
				}
				else
				{
					m_s_Mainform.add_log("[Error] ��¼������ʧ��", Color.Red);

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
						case ddns_lib.e_Progress.Starting:		LVI.SubItems[(int)e_Column_Domains.Status].Text = "��ʼ����";	break;
						case ddns_lib.e_Progress.DNS_Lookup:	LVI.SubItems[(int)e_Column_Domains.Status].Text = "��������";	break;
						case ddns_lib.e_Progress.Updating:		LVI.SubItems[(int)e_Column_Domains.Status].Text = "���ڸ���";	break;
						case ddns_lib.e_Progress.Done:			LVI.SubItems[(int)e_Column_Domains.Status].Text = "�������";	break;
						case ddns_lib.e_Progress.Failed:		LVI.SubItems[(int)e_Column_Domains.Status].Text = "����ʧ��";	break;
						}	// switch

						LVI.SubItems[(int)e_Column_Domains.Status].ForeColor = (progress == ddns_lib.e_Progress.Failed) ? Color.Red : Color.Black;
					}
				});
			}
		};
		#endregion

		#region LVI.Tag
		/*==============================================================
		 * ����/��ȡ LVI.Tag��Domain��
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
		 * ����/��ȡ LVI.Tag��Security_Profile��
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

		#region Winform �¼�
		/*==============================================================
		 * ���ڼ���/���ڹر�
		 *==============================================================*/
		private void frm_MainForm_Load(object sender, EventArgs e)
		{
			this.Icon				= Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule!.FileName);
			notifyIcon_Main.Icon	= this.Icon;
			notifyIcon_Main.Text	= this.Text;

			// ��ʼ�� DDNS_CLR
			ddns_tool_CLR.CLR.DoInit();

			// ���ûص�����
			ddns_tool_CLR.CLR.Event_OnConnected					+= EVENTS.OnConnected;
			ddns_tool_CLR.CLR.Event_OnDisconnecting				+= EVENTS.OnDisconnecting;
			ddns_tool_CLR.CLR.Event_Recv_Ping					+= EVENTS.Recv_Ping;
			ddns_tool_CLR.CLR.Event_Recv_LoginResult			+= EVENTS.Recv_LoginResult;
			ddns_tool_CLR.CLR.Event_Recv_Update_Domains_Result	+= EVENTS.Recv_Update_Domains_Result;
			ddns_tool_CLR.CLR.Event_On_add_log					+= EVENTS.On_add_log;

			ddns_lib.LIB.EVENTS.Event_On_AddLog					+= EVENTS.On_add_log;
			ddns_lib.LIB.EVENTS.Event_Set_Progress				+= EVENTS.On_Set_Progress;

			CONFIG.read_config();

			//==================== ���� UI ====================(Start)
			//�������б�
			foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
				add_LVI__Domain(domain);

			//����־��¼��
			numericUpDown_Logs_MaxLines.Value	= CONFIG.LOG.m_s_MaxLines;
			checkBox_Logs__Save_To_File.Checked	= CONFIG.LOG.m_s_Save_To_File;

			//�����·�ʽ��
			switch(CONFIG.m_s_update_type)
			{
			case CONFIG.e_Update_Type.Local:	radioButton_Settings_Type__Local.Checked	= true;	break;
			case CONFIG.e_Update_Type.Remote:	radioButton_Settings_Type__Remote.Checked	= true;	break;
			}	// switch

			// Զ�� Server ����
			textBox_Settings_RemoteServer__Addr.Text		= CONFIG.REMOTE_SERVER.m_s_addr;
			textBox_Settings_RemoteServer__User.Text		= CONFIG.REMOTE_SERVER.m_s_user;
			textBox_Settings_RemoteServer__Pwd.Text			= CONFIG.REMOTE_SERVER.m_s_pwd;
			checkBox_Settings_RemoteServer__Pwd.Checked		= CONFIG.REMOTE_SERVER.m_s_show_pwd;
			checkBox_Settings_RemoteServer__Ping.Checked	= CONFIG.REMOTE_SERVER.m_s_auto_ping;

			//������ IP��
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

			//����ȫ���á�
			foreach(ddns_lib.c_Security_Profile profile in CONFIG.SECURITY.m_s_profiles)
			{
				ListViewItem LVI = new(profile.m_Name);
				listView_Security.Items.Add(LVI);

				Set_LVI_Tag__Security_Profile(LVI, profile);
			}	// for

			//�����²�����
			checkBox_Action_UpdateIP.Checked				= CONFIG.UPDATE_ACTION.m_s_UpdateIP;

			// �Զ�����
			checkBox_Action_AutoAction_Interval.Checked		= CONFIG.ACTION.m_s_AutoAction;
			numericUpDown_Action_AutoAction_Interval.Value	= CONFIG.ACTION.m_s_AutoAction_interval;

			// �Ƚ�������
			checkBox_Action_DNS_Lookup_First.Checked		= CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First;

			// �Զ��� DNS ������
			checkBox_Action_Use_Custom_DNS.Checked			= CONFIG.UPDATE_ACTION.m_s_Use_Custom_DNS;
			textBox_Action_Custom_DNS_List.Lines			= CONFIG.UPDATE_ACTION.m_s_Custom_DNS_List.ToArray();

			// �Զ����³�ʱ����λ���롣0 = ���޵ȴ���
			numericUpDown_Action_Timeout.Value				= CONFIG.UPDATE_ACTION.m_s_Timeout;

			// IP�䶯ʱ��������ʾ����
			checkBox_Action_IP_Change_Popup.Checked			= CONFIG.UPDATE_ACTION.m_s_IP_Change_Popup;

			// IP�䶯ʱ����������
			checkBox_Action_IP_Change_PlaySound.Checked		= CONFIG.UPDATE_ACTION.m_s_IP_Change_Play_Sound;
			textBox_Action_IP_Change_PlaySound.Text			= CONFIG.UPDATE_ACTION.m_s_IP_Change_Sound_Path;
			//==================== ���� UI ====================(End)
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

				// ���� DDNS_CLR
				ddns_tool_CLR.CLR.DoFinal();
			}
		}

		/*==============================================================
		 * ����
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

		#region ��ʱ��
		/*==============================================================
		 * ���������ļ�
		 *==============================================================*/
		private void timer_Save_Config_Tick(object sender, EventArgs e)
		{
			CONFIG.save_config();
		}

		/*==============================================================
		 * ���� IP
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
		 * ������־
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

		#region ����ͼ��
		/*==============================================================
		 * ˫������ͼ��
		 *==============================================================*/
		private void notifyIcon_Main_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if(this.Visible)
				this.Hide();
			else
				FORMS.active_form(this);
		}
		#endregion
		#region ����ͼ�� - �����Ĳ˵�
		/*==============================================================
		 * ��
		 *==============================================================*/
		private void ToolStripMenuItem_Open_Click(object sender, EventArgs e)
		{
			FORMS.active_form(this);
		}

		/*==============================================================
		 * �˳�����
		 *==============================================================*/
		private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	"�Ƿ��˳�����",
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

		#region �����б�
		/*==============================================================
		 * ���� LVI
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
					sb_type.Append(" �Զ�IPv4");

				if(domain.m_dynv6__Auto_IPv6)
				{
					if(domain.m_dynv6__Auto_IPv4)
						sb_type.Append("+IPv6");
					else
						sb_type.Append(" �Զ�IPv6");
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
		 * �������� LVI
		 *==============================================================*/
		void update_All_LVI__Domain()
		{
			foreach(ListViewItem LVI in listView_Domains.Items)
				update_LVI__Domain(LVI);
		}

		/*==============================================================
		 * ��� LVI
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
		 * �޸�����
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
		 * ѡ����ı�
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
		 * �����п�
		 *==============================================================*/
		private void listView_Domains_Resize(object sender, EventArgs e)
		{
			int[] widths = { 0, 198, 60, 102, 273, 66 };

			for(int i = 1; i < widths.Length; ++i)
				listView_Domains.Columns[i].Width = widths[i];

			columnHeader_Domains_Domain.Width = listView_Domains.Width - 21 - widths.Sum();
		}

		/*==============================================================
		 * ˫���޸�
		 *==============================================================*/
		private void listView_Domains_DoubleClick(object sender, EventArgs e)
		{
			edit_domain();
		}
		#endregion
		#region �����б� - �����Ĳ˵�
		/*==============================================================
		 * ���
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
		 * �޸�
		 *==============================================================*/
		private void toolStripButton_Domains_Modify_Click(object sender, EventArgs e)
		{
			edit_domain();
		}

		/*==============================================================
		 * ɾ��
		 *==============================================================*/
		private void toolStripButton_Domains_Delete_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	$"�Ƿ�Ҫɾ��ѡ���� {listView_Domains.SelectedItems.Count} ����¼��",
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
		 * ������� IPv4
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
		 * ������� IPv6
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
		 * ��ֹ���� IPv4
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
		 * ��ֹ���� IPv6
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
		 * �����ı�
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
		 * ���
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_Add_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_Add.PerformClick();
		}

		/*==============================================================
		 * �޸�
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_Modify_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_Modify.PerformClick();
		}

		/*==============================================================
		 * ɾ��
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_Delete_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_Delete.PerformClick();
		}

		/*==============================================================
		 * ������� IPv4
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_IPv4_Enable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_IPv4_Enable.PerformClick();
		}

		/*==============================================================
		 * ������� IPv6
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_IPv6_Enable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_IPv6_Enable.PerformClick();
		}

		/*==============================================================
		 * ��ֹ���� IPv4
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_IPv4_Disable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_IPv4_Disable.PerformClick();
		}

		/*==============================================================
		 * ��ֹ���� IPv6
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_IPv6_Disable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_IPv6_Disable.PerformClick();
		}

		/*==============================================================
		 * �����ı�
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_CopyText_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_CopyText.PerformClick();
		}
		#endregion

		#region ��־
		/*==============================================================
		 * �����־��¼��UI �̰߳�ȫ��
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
		 * ѡ����ı�
		 *==============================================================*/
		private void listView_Logs_SelectedIndexChanged(object sender, EventArgs e)
		{
			ToolStripMenuItem_Logs_Copy.Enabled		= (listView_Logs.SelectedItems.Count > 0);
			ToolStripMenuItem_Logs_Delete.Enabled	= (listView_Logs.SelectedItems.Count > 0);
		}

		/*==============================================================
		 * �����п�
		 *==============================================================*/
		private void listView_Logs_Resize(object sender, EventArgs e)
		{
			int[] widths = { 122, 0 };

			for(int i = 0; i < widths.Length - 1; ++i)
				listView_Logs.Columns[i].Width = widths[i];

			columnHeader_Logs_Log.Width = listView_Logs.Width - 21 - widths.Sum();
		}

		/*==============================================================
		 * �����ʾ����
		 *==============================================================*/
		private void numericUpDown_Logs_MaxLines_ValueChanged(object sender, EventArgs e)
		{
			CONFIG.LOG.m_s_MaxLines = (int)numericUpDown_Logs_MaxLines.Value;
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * ���浽��־�ļ�
		 *==============================================================*/
		private void checkBox_Logs__Save_To_File_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.LOG.m_s_Save_To_File = checkBox_Logs__Save_To_File.Checked;
			CONFIG.m_s_dirty = true;
		}
		#endregion
		#region ��־ - �����Ĳ˵�
		/*==============================================================
		 * �����ı�
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
		 * ɾ��ѡ����¼
		 *==============================================================*/
		private void ToolStripMenuItem_Logs_Delete_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	$"�Ƿ�Ҫɾ��ѡ���� {listView_Logs.SelectedItems.Count} ����¼��",
								this.Text,
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question,
								MessageBoxDefaultButton.Button2 ) == DialogResult.No)
				return;

			while(listView_Logs.SelectedItems.Count > 0)
				listView_Logs.Items.Remove(listView_Logs.SelectedItems[0]);
		}

		/*==============================================================
		 * ȫѡ
		 *==============================================================*/
		private void ToolStripMenuItem_Logs_SelectAll_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem LVI in listView_Logs.Items)
				LVI.Selected = true;
		}
		#endregion

		#region ���·�ʽ
		/*==============================================================
		 * ���·�ʽ�ı�
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

		#region Զ�� Server ����
		/*==============================================================
		 * Server ��ַ/�˿�
		 *==============================================================*/
		private void textBox_Settings_RemoteServer__Addr_TextChanged(object sender, EventArgs e)
		{
			CONFIG.REMOTE_SERVER.m_s_addr = textBox_Settings_RemoteServer__Addr.Text.Trim();

			UpdateUI_radioButton_Settings_IP__Accept_IP();
			Update_Settings_Preview__Update_Type();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * ��¼�� Server ���û���
		 *==============================================================*/
		private void textBox_Settings_RemoteServer__User_TextChanged(object sender, EventArgs e)
		{
			CONFIG.REMOTE_SERVER.m_s_user = textBox_Settings_RemoteServer__User.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * ��¼�� Server ������
		 *==============================================================*/
		private void textBox_Settings_RemoteServer__Pwd_TextChanged(object sender, EventArgs e)
		{
			CONFIG.REMOTE_SERVER.m_s_pwd = textBox_Settings_RemoteServer__Pwd.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * ��ʾ����
		 *==============================================================*/
		private void checkBox_Settings_RemoteServer__Pwd_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Settings_RemoteServer__Pwd.PasswordChar = checkBox_Settings_RemoteServer__Pwd.Checked ? '\0' : '*';

			CONFIG.REMOTE_SERVER.m_s_show_pwd = checkBox_Settings_RemoteServer__Pwd.Checked;
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * �Զ� ping ������
		 *==============================================================*/
		private void checkBox_Settings_RemoteServer__Ping_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.REMOTE_SERVER.m_s_auto_ping = checkBox_Settings_RemoteServer__Ping.Checked;
			CONFIG.m_s_dirty = true;
		}
		#endregion

		#region ���� IP
		/*==============================================================
		 * ���� UI��Server �������ӵĿͻ��� IP��
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
		 * �ı䡾���� IPv4��
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
		 * ͨ����������ȡ���� IPv4
		 *==============================================================*/
		private void comboBox_Settings_IPv4__From_URL_SelectedIndexChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_Get_IPv4_URL = comboBox_Settings_IPv4__From_URL.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * �ֶ�ָ�� IPv4
		 *==============================================================*/
		private void textBox_Settings_IPv4_TextChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_IPv4 = textBox_Settings_IPv4.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * �ı䡾���� IPv6��
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
		 * ͨ����������ȡ���� IPv6
		 *==============================================================*/
		private void comboBox_Settings_IPv6__From_URL_SelectedIndexChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_Get_IPv6_URL	= comboBox_Settings_IPv6__From_URL.Text.Trim();
			CONFIG.m_s_dirty				= true;
		}

		/*==============================================================
		 * �ֶ�ָ�� IPv6
		 *==============================================================*/
		private void textBox_Settings_IPv6_TextChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_IPv6	= textBox_Settings_IPv6.Text.Trim();
			CONFIG.m_s_dirty		= true;
		}
		#endregion

		#region ��ȫ����
		/*==============================================================
		 * ��ȡ��ǰѡ��� profile
		 *==============================================================*/
		ddns_lib.c_Security_Profile? get_current_security_profile()
		{
			if(listView_Security.SelectedItems.Count == 0)
				return null;

			return Get_LVI_Tag__Security_Profile(listView_Security.SelectedItems[0]);
		}

		/*==============================================================
		 * ѡ��ı�
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
		 * �����п�
		 *==============================================================*/
		private void listView_Security_Resize(object sender, EventArgs e)
		{
			columnHeader_Security.Width = listView_Security.Width - 21;
		}

		/*==============================================================
		 * ���
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
		 * ɾ��
		 *==============================================================*/
		private void button_Security_Del_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	$"�Ƿ�Ҫɾ����{listView_Security.SelectedItems[0].Text}����",
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

			// ɾ�������� profile
			foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
			{
				if(domain.m_Security_Profile == profile)
					domain.m_Security_Profile = null;
			}	// for

			Update_Settings_Preview__Security();

			// ���������б�
			update_All_LVI__Domain();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * ���� - ���õ�����
		 *==============================================================*/
		private void textBox_Security__Property__Name_TextChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile? profile = get_current_security_profile();

			if(profile == null)
				return;

			profile.m_Name							= textBox_Security__Property__Name.Text;
			listView_Security.SelectedItems[0].Text	= textBox_Security__Property__Name.Text;

			// ���������б�
			update_All_LVI__Domain();

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * ���� - API ��ַ
		 *==============================================================*/
		private void linkLabel_Security__LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start((sender as LinkLabel)!.Text);
		}

		/*==============================================================
		 * ���� - [Godaddy] Key
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
		 * ���� - [Godaddy] Secret
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
		 * ���� - [Godaddy] ��ʾ Key
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
		 * ���� - [Godaddy] ��ʾ Secret
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
		 * ���� - [dynv6] token
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
		 * ���� - [dynv6] ��ʾ token
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
		 * ���� - [dynu] API_Key
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
		 * ���� - [dynu] ��ʾ API_Key
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
		 * ���� - ���浽 Config �ļ�
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
		#region ��ȫ���� - �����Ĳ˵�
		/*==============================================================
		 * ���
		 *==============================================================*/
		private void ToolStripMenuItem_Security_Add_Click(object sender, EventArgs e)
		{
			button_Security_Add.PerformClick();
		}

		/*==============================================================
		 * ɾ��
		 *==============================================================*/
		private void ToolStripMenuItem__Security_Del_Click(object sender, EventArgs e)
		{
			button_Security_Del.PerformClick();
		}
		#endregion

		#region ���²���
		/*==============================================================
		 * ����������IP
		 *==============================================================*/
		private void checkBox_Action_UpdateIP_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_UpdateIP = checkBox_Action_UpdateIP.Checked;

			Update_Settings_Preview__UpdateIP();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * �Զ�ִ�в�����ʱ�������룩
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
		 * IP�䶯ʱ����ִ�и��£��Ƚ���������
		 *==============================================================*/
		private void checkBox_Action_DNS_Lookup_First_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First = checkBox_Action_DNS_Lookup_First.Checked;

			Update_Settings_Preview__DNS_Lookup_First();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * �趨����������DNS������
		 *==============================================================*/
		private void checkBox_Action_Use_Custom_DNS_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Action_Custom_DNS_List.ReadOnly	= !checkBox_Action_Use_Custom_DNS.Checked;

			CONFIG.UPDATE_ACTION.m_s_Use_Custom_DNS	= checkBox_Action_Use_Custom_DNS.Checked;

			Update_Settings_Preview__DNS_Server();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * DNS�������б�
		 *==============================================================*/
		private void textBox_Action_Custom_DNS_List_TextChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_Custom_DNS_List.Clear();
			CONFIG.UPDATE_ACTION.m_s_Custom_DNS_List.AddRange(textBox_Action_Custom_DNS_List.Lines);

			Update_Settings_Preview__DNS_Server();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * �Զ����³�ʱ����λ���롣0 = ���޵ȴ���
		 *==============================================================*/
		private void numericUpDown_Action_Timeout_ValueChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_Timeout = (int)numericUpDown_Action_Timeout.Value;

			Update_Settings_Preview__timeout();

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * IP�䶯ʱ��������ʾ����
		 *==============================================================*/
		private void checkBox_Action_IP_Change_Popup_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.UPDATE_ACTION.m_s_IP_Change_Popup = checkBox_Action_IP_Change_Popup.Checked;
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * IP�䶯ʱ����������
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
			dlg.Filter				= "��Ƶ�ļ� (*.wav;*.mid;*.mp3)|*.wav;*.mid;*.mp3|�����ļ� (*.*)|*.*";
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

		#region ���� hosts
		/*==============================================================
		 * ��Ŀ¼
		 *==============================================================*/
		private void button_Fix_hosts__Path_Browser_Click(object sender, EventArgs e)
		{
			string dir = textBox_Fix_hosts__Path.Text.Substring(0, textBox_Fix_hosts__Path.Text.LastIndexOf("\\"));
			Process.Start("explorer.exe", dir);
		}
		#endregion

		#region Ԥ������
		/*==============================================================
		 * ���·�ʽ
		 *==============================================================*/
		void Update_Settings_Preview__Update_Type()
		{
			if(CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local)
				label_Settings_Preview__Update_Type_Val.Text = "���ظ��£�ֱ����";
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
		 * ���� IPv4
		 *==============================================================*/
		void Update_Settings_Preview__Set_IPv4()
		{
			switch(CONFIG.SET_IP.m_s_type_IPv4)
			{
			case CONFIG.e_IP_Get_Type.Get_IP_From_URL:
				label_Settings_Preview__Set_IPv4_Val.Text = "ͨ�� URL ��ȡ";
				break;

			case CONFIG.e_IP_Get_Type.Manual_IP:
				label_Settings_Preview__Set_IPv4_Val.Text = "�ֶ�ָ�� IP";
				break;

			case CONFIG.e_IP_Get_Type.Server_Accept_IP:
				label_Settings_Preview__Set_IPv4_Val.Text = "Server �������ӵ� IP";
				break;
			}	// switch
		}

		/*==============================================================
		 * ���� IPv6
		 *==============================================================*/
		void Update_Settings_Preview__Set_IPv6()
		{
			switch(CONFIG.SET_IP.m_s_type_IPv6)
			{
			case CONFIG.e_IP_Get_Type.Get_IP_From_URL:
				label_Settings_Preview__Set_IPv6_Val.Text = "ͨ�� URL ��ȡ";
				break;

			case CONFIG.e_IP_Get_Type.Manual_IP:
				label_Settings_Preview__Set_IPv6_Val.Text = "�ֶ�ָ�� IP";
				break;

			case CONFIG.e_IP_Get_Type.Server_Accept_IP:
				label_Settings_Preview__Set_IPv6_Val.Text = "Server �������ӵ� IP";
				break;
			}	// switch
		}

		/*==============================================================
		 * ��ȫ����
		 *==============================================================*/
		void Update_Settings_Preview__Security()
		{
			label_Settings_Preview__Security_Val.Text = $"{CONFIG.SECURITY.m_s_profiles.Count} �������ļ�";
		}

		/*==============================================================
		 * �������� IP
		 *==============================================================*/
		void Update_Settings_Preview__UpdateIP()
		{
			label_Settings_Preview__Action_UpdateIP_Val.Text = CONFIG.UPDATE_ACTION.m_s_UpdateIP ? COMMON.STR_TRUE : COMMON.STR_FALSE;
		}

		/*==============================================================
		 * �Զ�����
		 *==============================================================*/
		void Update_Settings_Preview__AutoUpdate()
		{
			label_Settings_Preview__Action_AutoUpdate_Val.Text = CONFIG.ACTION.m_s_AutoAction ? $"ÿ {CONFIG.ACTION.m_s_AutoAction_interval}s" : COMMON.STR_FALSE;
		}

		/*==============================================================
		 * �Ƚ�������
		 *==============================================================*/
		void Update_Settings_Preview__DNS_Lookup_First()
		{
			label_Settings_Preview__DNS_Lookup_First_Val.Text = CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First ? COMMON.STR_TRUE : COMMON.STR_FALSE;
		}

		/*==============================================================
		 * DNS ������
		 *==============================================================*/
		void Update_Settings_Preview__DNS_Server()
		{
			label_Settings_Preview__DNS_Server_Val.Text = CONFIG.UPDATE_ACTION.m_s_Use_Custom_DNS ? "�Զ���" : "ϵͳĬ��";
		}

		/*==============================================================
		 * ���³�ʱ
		 *==============================================================*/
		void Update_Settings_Preview__timeout()
		{
			label_Settings_Preview__Timeout_Val.Text = CONFIG.UPDATE_ACTION.m_s_Timeout.ToString();
		}
		#endregion

		#region ����
		// ȫ�������µ���������
		int				m_all_domains_for_update	= 0;

		List<string>	m_DNS_List					= new();

		/*==============================================================
		 * ִ�и��²���
		 *==============================================================*/
		private void button_Update_Click(object sender, EventArgs e)
		{
			update__start_update();
		}

		/*==============================================================
		 * �����´��Զ����µ�ʱ��
		 *==============================================================*/
		void set_next_Auto_Update_Time()
		{
			m_can_auto_update_time = DateTime.Now.AddSeconds((int)numericUpDown_Action_AutoAction_Interval.Value);

			add_log($"�´��Զ�����ʱ�䣺{m_can_auto_update_time.ToString("G").Replace("/", "-")}", Color.FromArgb(0, 162, 232));
		}

		/*==============================================================
		 * ��ʼ���� A/AAAA ��¼
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
		 * ���ü������µ� IP ��ַ
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

			// ��ȡ IP
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
					add_log($"[Error] �Ҳ��� {k_GET_IP_EXE}", Color.Red);
					return false;
				}

				add_log($"���ڻ�ȡ��ǰ���� {th.m_ip_type} ��ַ����");

				// ���� ServicePointManager �������⣬��Ӧ�ó������������޷��л� IP ��ַ�壬����ʹ���ⲿ���̻�ȡ IP
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
						add_log($"ͨ����������ȡ���� {th.m_ip_type} �ɹ� ({ip})");
						th.m_get_ip_ok = true;

						invoke(() =>
						{
							th.m_textBox_Settings_IP.Text = ip.Trim();
						});
					}
					else
					{
						ip = "";
						add_log($"[Error] ͨ����������ȡ���� {th.m_ip_type} ʧ�ܣ�{psi.FileName} {psi.Arguments}��", Color.Red);

						string? str_err = reader_err.ReadLine();
						if(!string.IsNullOrEmpty(str_err))
							add_log($"[Error] {str_err}��{psi.FileName} {psi.Arguments}��", Color.Red);
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
		 * ִ�и���
		 *==============================================================*/
		void update__do_update()
		{
			// ���ü������µ� IP ��ַ
			if(!update__Set_IP())
			{
				update__done();
				return;
			}

			m_all_domains_for_update = 0;

			// ���� IP
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
					// �Զ����ӵ�������
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
								add_log("[Error] Server ��ַ/�˿� ����", Color.Red);
								reset_domains_status();
								return;
							}

							if(server_port == 0)
							{
								add_log("[Error] Server �˿ڴ���", Color.Red);
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
							add_log($"[Error] ���ӵ�Զ�̷����� ({CONFIG.REMOTE_SERVER.m_s_addr}) ʧ��", Color.Red);
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
		 * ��ɸ��� A/AAAA ��¼
		 *==============================================================*/
		void update__done()
		{
			int failed_count	= 0;
			int skip_count		= 0;

			// �Ƿ��Ѹ��µ�ǰ IP������Զ�̸��¡���
			bool update_current_IPv4_remote = false;
			bool update_current_IPv6_remote = false;

			// IP �����仯������
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
						add_log($"[Error] {domain.m_domain} : ���� IPv4 ʧ�ܣ�{domain.IPv4.m_err_msg}��", Color.Red);

					if(domain.IPv6.m_err_msg.Length > 0)
						add_log($"[Error] {domain.m_domain} : ���� IPv6 ʧ�ܣ�{domain.IPv6.m_err_msg}��", Color.Red);
				}
				else
				{
					EVENTS.On_Set_Progress(domain.m_domain, ddns_lib.e_Progress.Done);

					if(	(domain.IPv4.m_enabled && !domain.IPv4.m_same_ip)	||
						(domain.IPv6.m_enabled && !domain.IPv6.m_same_ip) )
					{
						add_log($"{domain.m_domain} : ���³ɹ���IPv4 = {domain.IPv4.m_current_IP}, IPv6 = {domain.IPv6.m_current_IP}", Color.Green);

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

			add_log($"{IP_change_domains.Count} �ɹ���{failed_count} ʧ�ܣ�{skip_count} ��������{m_all_domains_for_update} �ܼ�",
					(failed_count == 0) ? Color.DarkOrange : Color.Red);

			if(IP_change_domains.Count > 0)
			{
				// IP�䶯ʱ��������ʾ����
				if(CONFIG.UPDATE_ACTION.m_s_IP_Change_Popup)
				{
					invoke(() =>
					{
						m_IP_Change_Popup.set_domains(IP_change_domains);
						FORMS.active_form(m_IP_Change_Popup);
					});
				}

				// IP�䶯ʱ����������
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

			// �����´��Զ����µ�ʱ��
			set_next_Auto_Update_Time();

			lock_controls(true);
			m_is_updating = false;
		}
		#endregion
	};
}	// namespace ddns_tool
