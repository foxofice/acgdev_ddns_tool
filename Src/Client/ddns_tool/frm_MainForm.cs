using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ddns_tool
{
	public partial class frm_MainForm : Form
	{
		public frm_MainForm()
		{
			InitializeComponent();

			m_s_Mainform = this;
		}

		enum e_Column_Domains
		{
			Domain,
			Type,	// [Godaddy] TTL = 600；[dynv6] 自动IPv4, 自动IPv6
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

		internal const string			STR_TRUE				= "√";
		internal const string			STR_FALSE				= "×";

		internal static frm_MainForm	m_s_Mainform			= null;
		internal frm_IP_Change_Popup	m_IP_Change_Popup		= new frm_IP_Change_Popup();

		bool							m_exiting				= false;		// 是否正在退出

		DateTime						m_can_auto_update_time	= DateTime.Now;	// 可以自动执行更新的时间
		bool							m_is_updating			= false;		// 是否正在执行更新

		bool							m_login_server_done		= false;		// 已成功登录 Server

		/*==============================================================
		 * 域名 -> LVI
		 *==============================================================*/
		internal ListViewItem find_LVI__Domain(string domain_name)
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
				radioButton_Settings_IP__From_URL.Enabled		= enabled;
				comboBox_Settings_IPv4__From_URL.Enabled		= enabled && radioButton_Settings_IP__From_URL.Checked;
				comboBox_Settings_IPv6__From_URL.Enabled		= enabled && radioButton_Settings_IP__From_URL.Checked;

				radioButton_Settings_IP__Manual.Enabled			= enabled;
				textBox_Settings_IP__IPv4.ReadOnly				= !enabled || !radioButton_Settings_IP__Manual.Checked;
				textBox_Settings_IP__IPv6.ReadOnly				= !enabled || !radioButton_Settings_IP__Manual.Checked;

				radioButton_Settings_IP__Accept_IP.Enabled		= enabled;

				//【安全设置】
				textBox_Security_Godaddy__Key.ReadOnly			= !enabled;
				textBox_Security_Godaddy__Secret.ReadOnly		= !enabled;

				textBox_Security_dynv6__token.ReadOnly			= !enabled;

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

				if(CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Server_Accept_IP)
				{
					CONFIG.SET_IP.m_s_IPv4 = "";
					CONFIG.SET_IP.m_s_IPv6 = "";
				}

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
					ddns_lib.c_Domain domain = CONFIG.find_domain(res.m_domain);

					if(domain == null)
						continue;

					domain.m_current_IPv4	= res.m_current_IPv4;
					domain.m_current_IPv6	= res.m_current_IPv6;
					domain.m_err_msg_IPv4	= res.m_err_msg_IPv4;
					domain.m_err_msg_IPv6	= res.m_err_msg_IPv6;
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
					ListViewItem LVI = m_s_Mainform.find_LVI__Domain(domain);

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
			return (ddns_lib.c_Domain)LVI.Tag;
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
			return (ddns_lib.c_Security_Profile)LVI.Tag;
		}
		#endregion

		#region Winform 事件
		/*==============================================================
		 * 窗口加载/窗口关闭
		 *==============================================================*/
		private void frm_MainForm_Load(object sender, EventArgs e)
		{
			this.Icon				= Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);
			notifyIcon_Main.Icon	= this.Icon;

			notifyIcon_Main.Text	= this.Text;

			ServicePointManager.DefaultConnectionLimit = 1000;

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
			numericUpDown_Logs_MaxLines.Value				= CONFIG.LOG.m_s_MaxLines;
			checkBox_Logs__Save_To_File.Checked				= CONFIG.LOG.m_s_Save_To_File;

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
			radioButton_Settings_IP__From_URL.Checked	= (CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Get_IP_From_URL);
			comboBox_Settings_IPv4__From_URL.Text		= CONFIG.SET_IP.m_s_Get_IPv4_URL;
			comboBox_Settings_IPv6__From_URL.Text		= CONFIG.SET_IP.m_s_Get_IPv6_URL;

			if(CONFIG.SET_IP.m_s_Get_IPv4_URL.Length == 0)
				comboBox_Settings_IPv4__From_URL.SelectedIndex = 0;

			if(CONFIG.SET_IP.m_s_Get_IPv6_URL.Length == 0)
				comboBox_Settings_IPv6__From_URL.SelectedIndex = 0;

			radioButton_Settings_IP__Manual.Checked		= (CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Manual_IP);
			textBox_Settings_IP__IPv4.Text				= CONFIG.SET_IP.m_s_IPv4;
			textBox_Settings_IP__IPv6.Text				= CONFIG.SET_IP.m_s_IPv6;

			radioButton_Settings_IP__Accept_IP.Checked	= (CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Server_Accept_IP);

			//【安全设置】
			foreach(ddns_lib.c_Security_Profile profile in CONFIG.SECURITY.m_s_profiles)
			{
				ListViewItem LVI = new ListViewItem(profile.m_Name);
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
			Update_Settings_Preview__Set_IP();
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
			ddns_lib.c_Domain domain = Get_LVI_Tag__Domain(LVI);

			LVI.SubItems[(int)e_Column_Domains.Domain].Text	= domain.m_domain;

			StringBuilder sb_type = new StringBuilder();

			sb_type.Append(domain.m_type.ToString());

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
			}	// switch

			LVI.SubItems[(int)e_Column_Domains.Type].Text			= sb_type.ToString();
			LVI.SubItems[(int)e_Column_Domains.Profile].Text		= (domain.m_Security_Profile != null) ? domain.m_Security_Profile.m_Name : "";

			LVI.SubItems[(int)e_Column_Domains.current_IPv4].Text	= domain.m_current_IPv4;
			LVI.SubItems[(int)e_Column_Domains.current_IPv6].Text	= domain.m_current_IPv6;

			if(domain.m_enabled)
			{
				LVI.Font		= new Font(LVI.Font, LVI.Font.Style & ~FontStyle.Strikeout);
				LVI.BackColor	= Color.White;
			}
			else
			{
				LVI.Font		= new Font(LVI.Font, LVI.Font.Style | FontStyle.Strikeout);
				LVI.BackColor	= Color.Gray;
			}
		}

		/*==============================================================
		 * 更新所有 LVI
		 *==============================================================*/
		void update_All_LVI__Domain()
		{
			foreach(ListViewItem  LVI in listView_Domains.Items)
				update_LVI__Domain(LVI);
		}

		/*==============================================================
		 * 添加 LVI
		 *==============================================================*/
		void add_LVI__Domain(ddns_lib.c_Domain domain)
		{
			ListViewItem LVI = new ListViewItem();

			while(LVI.SubItems.Count < listView_Domains.Columns.Count)
				LVI.SubItems.Add("");

			listView_Domains.Items.Add(LVI);
			Set_LVI_Tag__Domain(LVI, domain);

			LVI.UseItemStyleForSubItems = true;
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

			frm_Domain dlg = new frm_Domain(domain);

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
			toolStripButton_Domains_Modify.Enabled		= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_Delete.Enabled		= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_Enable.Enabled		= (listView_Domains.SelectedItems.Count > 0);
			toolStripButton_Domains_Disable.Enabled		= (listView_Domains.SelectedItems.Count > 0);

			ToolStripMenuItem_Domains_Modify.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_Delete.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_Enable.Enabled	= (listView_Domains.SelectedItems.Count > 0);
			ToolStripMenuItem_Domains_Disable.Enabled	= (listView_Domains.SelectedItems.Count > 0);
		}

		/*==============================================================
		 * 调整列宽
		 *==============================================================*/
		private void listView_Domains_Resize(object sender, EventArgs e)
		{
			int[] widths = { 168, 60, 102, 273, 66 };

			for(int i=0; i<widths.Length; ++i)
				listView_Domains.Columns[i + 1].Width = widths[i];

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
			frm_Domain dlg = new frm_Domain();

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
		 * 允许更新
		 *==============================================================*/
		private void toolStripButton_Domains_Enable_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem LVI in listView_Domains.SelectedItems)
			{
				ddns_lib.c_Domain domain = Get_LVI_Tag__Domain(LVI);
				domain.m_enabled = true;

				update_LVI__Domain(LVI);

				CONFIG.m_s_dirty = true;
			}	// for
		}

		/*==============================================================
		 * 禁止更新
		 *==============================================================*/
		private void toolStripButton_Domains_Disable_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem LVI in listView_Domains.SelectedItems)
			{
				ddns_lib.c_Domain domain = Get_LVI_Tag__Domain(LVI);
				domain.m_enabled = false;

				update_LVI__Domain(LVI);

				CONFIG.m_s_dirty = true;
			}	// for
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
		 * 允许更新
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_Enable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_Enable.PerformClick();
		}

		/*==============================================================
		 * 禁止更新
		 *==============================================================*/
		private void ToolStripMenuItem_Domains_Disable_Click(object sender, EventArgs e)
		{
			toolStripButton_Domains_Disable.PerformClick();
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
				ListViewItem LVI = new ListViewItem();

				while(LVI.SubItems.Count < listView_Logs.Columns.Count)
					LVI.SubItems.Add("");

				LVI.SubItems[(int)e_Column_Log.Time].Text	= DateTime.Now.ToString("G").Replace("/", ".");
				LVI.SubItems[(int)e_Column_Log.Log].Text	= txt;

				LVI.ForeColor = c;

				listView_Logs.Items.Add(LVI);

				LVI.EnsureVisible();
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
			int[] widths = { 122 };

			for(int i=0; i<widths.Length; ++i)
				listView_Logs.Columns[i].Width = widths[i];

			columnHeader_Logs_Log.Width = listView_Logs.Width - 21 - widths.Sum();
		}

		/*==============================================================
		 * 尺寸改变
		 *==============================================================*/
		private void listView_Logs_SizeChanged(object sender, EventArgs e)
		{
			columnHeader_Logs_Time.Width	= 122;
			columnHeader_Logs_Log.Width		= listView_Logs.Width - columnHeader_Logs_Time.Width - 21;
		}

		/*==============================================================
		 * 日志最大行数
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

			StringBuilder sb = new StringBuilder();

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

			radioButton_Settings_IP__Accept_IP.Enabled		= (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote);

			// 取消 Accept_IP 选择
			if(	CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local	&&
				radioButton_Settings_IP__Accept_IP.Checked )
			{
				radioButton_Settings_IP__From_URL.Checked = true;
			}

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
		 * 改变【设置 IP】
		 *==============================================================*/
		private void radioButton_Settings_IP__CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_type = CONFIG.e_IP_Get_Type.Get_IP_From_URL;

			if(radioButton_Settings_IP__Manual.Checked)
				CONFIG.SET_IP.m_s_type = CONFIG.e_IP_Get_Type.Manual_IP;
			else if(radioButton_Settings_IP__Accept_IP.Checked)
				CONFIG.SET_IP.m_s_type = CONFIG.e_IP_Get_Type.Server_Accept_IP;

			comboBox_Settings_IPv4__From_URL.Enabled	= (CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Get_IP_From_URL);
			comboBox_Settings_IPv6__From_URL.Enabled	= (CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Get_IP_From_URL);
			textBox_Settings_IP__IPv4.ReadOnly			= (CONFIG.SET_IP.m_s_type != CONFIG.e_IP_Get_Type.Manual_IP);
			textBox_Settings_IP__IPv6.ReadOnly			= (CONFIG.SET_IP.m_s_type != CONFIG.e_IP_Get_Type.Manual_IP);

			Update_Settings_Preview__Set_IP();

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
		 * 通过互联网获取公网 IPv6
		 *==============================================================*/
		private void comboBox_Settings_IPv6__From_URL_SelectedIndexChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_Get_IPv6_URL = comboBox_Settings_IPv6__From_URL.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 手动指定 IPv4
		 *==============================================================*/
		private void textBox_Settings_IP__IPv4_TextChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_IPv4 = textBox_Settings_IP__IPv4.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 手动指定 IPv6
		 *==============================================================*/
		private void textBox_Settings_IP__IPv6_TextChanged(object sender, EventArgs e)
		{
			CONFIG.SET_IP.m_s_IPv6 = textBox_Settings_IP__IPv6.Text.Trim();
			CONFIG.m_s_dirty = true;
		}
		#endregion

		#region 安全设置
		/*==============================================================
		 * 获取当前选择的 profile
		 *==============================================================*/
		ddns_lib.c_Security_Profile	get_current_security_profile()
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

			ddns_lib.c_Security_Profile profile = get_current_security_profile();

			bool dirty = CONFIG.m_s_dirty;

			if(profile == null)
			{
				textBox_Security__Property__Name.Clear();
				textBox_Security__Property__Name.ReadOnly		= true;

				textBox_Security_Godaddy__Key.Clear();
				textBox_Security_Godaddy__Key.ReadOnly			= true;

				textBox_Security_Godaddy__Secret.Clear();
				textBox_Security_Godaddy__Secret.ReadOnly		= true;

				textBox_Security_dynv6__token.Clear();
				textBox_Security_dynv6__token.ReadOnly			= true;

				checkBox_Security__Save_To_Config.Enabled		= false;
			}
			else
			{
				textBox_Security__Property__Name.Text			= profile.m_Name;
				textBox_Security__Property__Name.ReadOnly		= false;

				textBox_Security_Godaddy__Key.Text				= profile.m_Godaddy__Key;
				textBox_Security_Godaddy__Key.ReadOnly			= false;

				checkBox_Security_Godaddy__Key.Checked			= profile.m_Godaddy__Key_Visible;

				textBox_Security_Godaddy__Secret.Text			= profile.m_Godaddy__Secret;
				textBox_Security_Godaddy__Secret.ReadOnly		= false;

				checkBox_Security_Godaddy__Secret.Checked		= profile.m_Godaddy__Secret_Visible;

				textBox_Security_dynv6__token.Text				= profile.m_dynv6__token;
				textBox_Security_dynv6__token.ReadOnly			= false;

				checkBox_Security_dynv6__token.Checked			= profile.m_dynv6__token_Visible;

				checkBox_Security__Save_To_Config.Enabled		= true;
				checkBox_Security__Save_To_Config.Checked		= profile.m_Save_To_Config;
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
			ddns_lib.c_Security_Profile profile = new ddns_lib.c_Security_Profile();
			profile.m_Name = $"{CONFIG.SECURITY.m_s_profiles.Count + 1}";

			CONFIG.SECURITY.m_s_profiles.Add(profile);

			ListViewItem LVI = new ListViewItem(profile.m_Name);
			listView_Security.Items.Add(LVI);

			LVI.Selected	= true;
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
			if(	MessageBox.Show($"是否要删除「{listView_Security.SelectedItems[0].Text}」？",
								this.Text,
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question,
								MessageBoxDefaultButton.Button2) == DialogResult.No )
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
			ddns_lib.c_Security_Profile profile = get_current_security_profile();

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
			Process.Start((sender as LinkLabel).Text);
		}

		/*==============================================================
		 * 属性 - [Godaddy] Key
		 *==============================================================*/
		private void textBox_Security_Godaddy__Key_TextChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile profile = get_current_security_profile();

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
			ddns_lib.c_Security_Profile profile = get_current_security_profile();

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
			ddns_lib.c_Security_Profile profile = get_current_security_profile();

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
			ddns_lib.c_Security_Profile profile = get_current_security_profile();

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
			ddns_lib.c_Security_Profile profile = get_current_security_profile();

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
			ddns_lib.c_Security_Profile profile = get_current_security_profile();

			if(profile == null)
				return;

			textBox_Security_dynv6__token.PasswordChar	= checkBox_Security_dynv6__token.Checked ? '\0' : '*';
			profile.m_dynv6__token_Visible				= checkBox_Security_dynv6__token.Checked;

			if(profile.m_Save_To_Config)
				CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 属性 - 保存到 Config 文件
		 *==============================================================*/
		private void checkBox_Security__Save_To_Config_CheckedChanged(object sender, EventArgs e)
		{
			ddns_lib.c_Security_Profile profile = get_current_security_profile();

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
		private void checkBox_AutoAction_CheckedChanged(object sender, EventArgs e)
		{
			numericUpDown_Action_AutoAction_Interval.Enabled	= checkBox_Action_AutoAction_Interval.Checked;
			timer_Update.Enabled								= checkBox_Action_AutoAction_Interval.Checked;

			CONFIG.ACTION.m_s_AutoAction = checkBox_Action_AutoAction_Interval.Checked;

			Update_Settings_Preview__AutoUpdate();

			CONFIG.m_s_dirty = true;
		}
		//--------------------------------------------------
		private void numericUpDown_AutoUpdate_Interval_ValueChanged(object sender, EventArgs e)
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

			CONFIG.UPDATE_ACTION.m_s_Use_Custom_DNS = checkBox_Action_Use_Custom_DNS.Checked;

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
			OpenFileDialog dlg = new OpenFileDialog();

			string dir = Path.GetDirectoryName(textBox_Action_IP_Change_PlaySound.Text);

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
		 * 设置IP
		 *==============================================================*/
		void Update_Settings_Preview__Set_IP()
		{
			switch(CONFIG.SET_IP.m_s_type)
			{
			case CONFIG.e_IP_Get_Type.Get_IP_From_URL:
				label_Settings_Preview__Set_IP_Val.Text = "通过 URL 获取";
				break;

			case CONFIG.e_IP_Get_Type.Manual_IP:
				label_Settings_Preview__Set_IP_Val.Text = "手动指定 IP";
				break;

			case CONFIG.e_IP_Get_Type.Server_Accept_IP:
				label_Settings_Preview__Set_IP_Val.Text = "Server 接受连接的 IP";
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
			label_Settings_Preview__Action_UpdateIP_Val.Text = CONFIG.UPDATE_ACTION.m_s_UpdateIP ? STR_TRUE : STR_FALSE;
		}

		/*==============================================================
		 * 自动更新
		 *==============================================================*/
		void Update_Settings_Preview__AutoUpdate()
		{
			label_Settings_Preview__Action_AutoUpdate_Val.Text = CONFIG.ACTION.m_s_AutoAction ? $"每 {CONFIG.ACTION.m_s_AutoAction_interval}s" : STR_FALSE;
		}

		/*==============================================================
		 * 先解析域名
		 *==============================================================*/
		void Update_Settings_Preview__DNS_Lookup_First()
		{
			label_Settings_Preview__DNS_Lookup_First_Val.Text = CONFIG.UPDATE_ACTION.m_s_DNS_Lookup_First ? STR_TRUE : STR_FALSE;
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
		// domain -> (ipv4, ipv6)
		Dictionary<string, (string, string)>	m_domains_old_IP	= new Dictionary<string, (string, string)>();

		List<string>							m_DNS_List			=  new List<string>();

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

			Thread th = new Thread(update__do_update);
			th.Start();
		}

		/*==============================================================
		 * 设置即将更新的 IP 地址
		 *==============================================================*/
		bool update__Set_IP()
		{
			CONFIG.SET_IP.m_s_IPv4 = textBox_Settings_IP__IPv4.Text;
			CONFIG.SET_IP.m_s_IPv6 = textBox_Settings_IP__IPv6.Text;

			if(	CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote			&&
				CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Server_Accept_IP	&&
				!ddns_tool_CLR.CLR.is_connected() )
			{
				CONFIG.SET_IP.m_s_IPv4 = "";
				CONFIG.SET_IP.m_s_IPv6 = "";
			}

			// 获取 IP
			if(CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Get_IP_From_URL)
			{
				string comboBox_Settings_IPv4__From_URL_Text = (string)this.Invoke((Func<string>)delegate { return comboBox_Settings_IPv4__From_URL.Text; });
				string comboBox_Settings_IPv6__From_URL_Text = (string)this.Invoke((Func<string>)delegate { return comboBox_Settings_IPv6__From_URL.Text; });

				if(	(comboBox_Settings_IPv4__From_URL_Text.Length == 0)	&&
					(comboBox_Settings_IPv6__From_URL_Text.Length == 0) )
				{
					add_log("[Error] 请输入「检查公网IP的URL」", Color.Red);
					return false;
				}

				const string k_GET_IP_EXE = "get_ip_from_URL.exe";
				if(!File.Exists(k_GET_IP_EXE))
				{
					add_log($"[Error] 找不到 {k_GET_IP_EXE}", Color.Red);
					return false;
				}

				add_log("正在获取当前公网 IP 地址……");

				bool get_ipv4_ok = true;
				bool get_ipv6_ok = true;

				// 由于 ServicePointManager 缓存问题，在应用程序生命周期无法切换 IP 地址族，这里使用外部进程获取 IP
				Thread th_ipv4 = new Thread(() =>
				{
					ProcessStartInfo psi = new ProcessStartInfo();
					psi.FileName				= k_GET_IP_EXE;
					psi.Arguments				= comboBox_Settings_IPv4__From_URL_Text + " v4";
					psi.RedirectStandardOutput	= true;
					psi.UseShellExecute			= false;
					psi.CreateNoWindow			= true;

					Process p = Process.Start(psi);

					StreamReader reader = p.StandardOutput;

					p.WaitForExit();
					p.Close();

					string ip = reader.ReadLine().Trim();

					if(IPAddress.TryParse(ip, out IPAddress addr) && addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
					{
						add_log($"通过互联网获取公网 IPv4 成功 ({ip})");
					}
					else
					{
						ip = "";
						add_log("[Error] 通过互联网获取公网 IPv4 失败", Color.Red);
						get_ipv4_ok = false;
					}

					CONFIG.SET_IP.m_s_IPv4 = ip;

					invoke(() =>
					{
						textBox_Settings_IP__IPv4.Text = ip;
					});
				});

				Thread th_ipv6 = new Thread(() =>
				{
					ProcessStartInfo psi = new ProcessStartInfo();
					psi.FileName				= k_GET_IP_EXE;
					psi.Arguments				= comboBox_Settings_IPv6__From_URL_Text;
					psi.RedirectStandardOutput	= true;
					psi.UseShellExecute			= false;
					psi.CreateNoWindow			= true;

					Process p = Process.Start(psi);

					StreamReader reader = p.StandardOutput;

					p.WaitForExit();
					p.Close();

					string ip = reader.ReadLine().Trim();

					if(IPAddress.TryParse(ip, out IPAddress addr) && addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
					{
						add_log($"通过互联网获取公网 IPv6 成功 ({ip})");
					}
					else
					{
						ip = "";
						add_log("[Error] 通过互联网获取公网 IPv6 失败", Color.Red);
						get_ipv6_ok = false;
					}

					CONFIG.SET_IP.m_s_IPv6 = ip;

					invoke(() =>
					{
						textBox_Settings_IP__IPv6.Text = ip;
					});
				});

				th_ipv4.Start();
				th_ipv6.Start();

				th_ipv4.Join();
				th_ipv6.Join();

				return get_ipv4_ok && get_ipv6_ok;
			}

			return true;
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

			m_domains_old_IP.Clear();

			List<string> domains_list = new List<string>();

			// 更新 IP
			foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
			{
				if(!domain.m_enabled)
					continue;

				domain.m_input_IPv4	= CONFIG.SET_IP.m_s_IPv4;
				domain.m_input_IPv6	= CONFIG.SET_IP.m_s_IPv6;

				// 记录旧 IP
				m_domains_old_IP.Add(domain.m_domain, (domain.m_current_IPv4, domain.m_current_IPv6));

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
								if(!domain.m_enabled)
									continue;

								EVENTS.On_Set_Progress(domain.m_domain, ddns_lib.e_Progress.None);
							}	// for

							update__done();
						}

						string[] vals = CONFIG.REMOTE_SERVER.m_s_addr.Split(':');
						if(vals.Length != 2)
						{
							add_log("[Error] Server 地址/端口 错误", Color.Red);
							reset_domains_status();
							return;
						}

						string ip = vals[0];
						ushort port;
						if(!ushort.TryParse(vals[1], out port))
						{
							add_log("[Error] Server 端口错误", Color.Red);
							reset_domains_status();
							return;
						}

						bool res = ddns_tool_CLR.CLR.Connect(	ip,
																port,
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

			bool update_current_IP = false;

			// IP 发生变化的域名（domain, IPv4_change, IPv6_change）[]
			List<(string, bool, bool)> IP_change_domains = new List<(string, bool, bool)>();

			foreach(ddns_lib.c_Domain domain in CONFIG.m_s_domains_list)
			{
				if(!domain.m_enabled)
					continue;

				(string, string) vals;

				if(!m_domains_old_IP.TryGetValue(domain.m_domain, out vals))
					continue;

				bool IPv4_change = (vals.Item1 != domain.m_current_IPv4);
				bool IPv6_change = (vals.Item2 != domain.m_current_IPv6);

				if(IPv4_change || IPv6_change)
					IP_change_domains.Add((domain.m_domain, IPv4_change, IPv6_change));

				if(domain.m_err_msg_IPv4.Length > 0 || domain.m_err_msg_IPv6.Length > 0)
				{
					++failed_count;

					EVENTS.On_Set_Progress(domain.m_domain, ddns_lib.e_Progress.Failed);

					if(domain.m_err_msg_IPv4.Length > 0)
						add_log($"[Error] {domain.m_domain} : 更新 IPv4 失败（{domain.m_err_msg_IPv4}）", Color.Red);

					if(domain.m_err_msg_IPv6.Length > 0)
						add_log($"[Error] {domain.m_domain} : 更新 IPv6 失败（{domain.m_err_msg_IPv6}）", Color.Red);
				}
				else
				{
					EVENTS.On_Set_Progress(domain.m_domain, ddns_lib.e_Progress.Done);

					if(IPv4_change || IPv6_change)
					{
						add_log($"{domain.m_domain} : 更新成功。IPv4 = {domain.m_current_IPv4}, IPv6 = {domain.m_current_IPv6}", Color.Green);

						if(!update_current_IP)
						{
							if(	CONFIG.m_s_update_type == CONFIG.e_Update_Type.Remote	&&
								CONFIG.SET_IP.m_s_type == CONFIG.e_IP_Get_Type.Server_Accept_IP )
							{
								update_current_IP = true;

								CONFIG.SET_IP.m_s_IPv4	= domain.m_current_IPv4;
								CONFIG.SET_IP.m_s_IPv6	= domain.m_current_IPv6;

								invoke(() =>
								{
									textBox_Settings_IP__IPv4.Text	= domain.m_current_IPv4;
									textBox_Settings_IP__IPv6.Text	= domain.m_current_IPv6;
								});
							}
						}
					}
					else
						++skip_count;
				}
			}	// for

			add_log($"全部：{m_domains_old_IP.Count}。成功/失败/跳过：{IP_change_domains.Count}/{failed_count}/{skip_count}",
					(failed_count == 0) ? Color.DarkOrange : Color.Red);

			if(IP_change_domains.Count > 0)
			{
				// IP变动时，弹出提示窗口
				if(CONFIG.UPDATE_ACTION.m_s_IP_Change_Popup)
				{
					m_IP_Change_Popup.set_domains(IP_change_domains);
					FORMS.active_form(m_IP_Change_Popup);
				}

				// IP变动时，播放音乐
				if(CONFIG.UPDATE_ACTION.m_s_IP_Change_Play_Sound)
				{
					SOUND.Stop();
					SOUND.Play(CONFIG.UPDATE_ACTION.m_s_IP_Change_Sound_Path);
				}

				invoke(update_All_LVI__Domain);
				CONFIG.m_s_dirty = true;
			}

			// 设置下次自动更新的时间
			set_next_Auto_Update_Time();
			add_log($"下次自动更新时间：{m_can_auto_update_time.ToString("G").Replace("/", "-")}", Color.FromArgb(0, 162, 232));

			lock_controls(true);
			m_is_updating = false;
		}
		#endregion
	};
}	// namespace ddns_tool
