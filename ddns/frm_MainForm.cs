using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ddns
{
	public partial class frm_MainForm : Form
	{
		public frm_MainForm()
		{
			InitializeComponent();

			m_s_Mainform = this;
		}

		enum e_Column_DomainList
		{
			Name,
			Domain,
			TTL,
			Last_Result,
			Last_IP,
		};

		enum e_Column_Log
		{
			Time,
			Log,
		};

		internal static frm_MainForm	m_s_Mainform			= null;

		bool							m_exiting				= false;		// 是否正在退出

		DateTime						m_can_auto_update_time	= DateTime.Now;	// 可以自动更新的时间
		bool							m_is_updating			= false;		// 是否正在更新 IP

		/*==============================================================
		 * 执行委托
		 *==============================================================*/
		public static void invoke(Action func)
		{
			if(m_s_Mainform.InvokeRequired)
				m_s_Mainform.Invoke(func);
			else
				func();
		}
		//--------------------------------------------------
		//public static TResult invoke<TResult>(Func<TResult> func)
		//{
		//	if(m_s_Mainform.InvokeRequired)
		//		return (TResult)m_s_Mainform.Invoke(func);
		//	else
		//		return func();
		//}


		/*==============================================================
		 * 激活窗口
		 *==============================================================*/
		void active_form()
		{
			this.Show();
			this.BringToFront();
			this.Activate();
			this.WindowState = FormWindowState.Normal;
		}


		/*==============================================================
		 * 锁定控件
		 *==============================================================*/
		void lock_controls(bool enabled)
		{
			invoke(() =>
			{
				radioButton_Local.Enabled							= enabled;
				radioButton_Server.Enabled							= enabled;

				textBox_Server_Addr.ReadOnly						= !enabled || !radioButton_Server.Checked;
				textBox_Server_User.ReadOnly						= !enabled || !radioButton_Server.Checked;
				textBox_Server_Pwd.ReadOnly							= !enabled || !radioButton_Server.Checked;

				radioButton_Get_IP_From_URL.Enabled					= enabled;
				radioButton_Specific_IP.Enabled						= enabled;
				radioButton_Server_Accept_IP.Enabled				= enabled;
				comboBox_Settings_Get_IP_URL.Enabled				= enabled && radioButton_Get_IP_From_URL.Checked;
				textBox_Settings_Last_IP.ReadOnly					= !enabled || !radioButton_Specific_IP.Checked;

				textBox_Settings_Key.ReadOnly						= !enabled;
				textBox_Settings_Secret.ReadOnly					= !enabled;

				checkBox_Settings_AutoUpdate.Enabled				= enabled;
				numericUpDown_Settings_AutoUpdate_Interval.Enabled	= enabled;
				checkBox_Settings_Update_Force.Enabled				= enabled;
				button_Settings_Update.Enabled						= enabled;

				listView_Records.ContextMenuStrip					= enabled ? contextMenuStrip_Records : null;
			});
		}

		#region 日志
		/*==============================================================
		 * 添加日志记录
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
		 * 选择项更改
		 *==============================================================*/
		private void listView_Logs_SelectedIndexChanged(object sender, EventArgs e)
		{
			ToolStripMenuItem_Logs_Copy.Enabled		= (listView_Logs.SelectedItems.Count > 0);
			ToolStripMenuItem_Logs_Delete.Enabled	= (listView_Logs.SelectedItems.Count > 0);
		}


		/*==============================================================
		 * 大小更改
		 *==============================================================*/
		private void listView_Logs_SizeChanged(object sender, EventArgs e)
		{
			columnHeader_Logs_Time.Width	= 122;
			columnHeader_Logs_Log.Width		= listView_Logs.Width - columnHeader_Logs_Time.Width - 21;
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


		#region 事件
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

				invoke(() =>
				{
					m_s_Mainform.finish_update_records(true);
					m_s_Mainform.textBox_Server_Ping.Clear();
				});

				if(CONFIG.m_s_IP_get_type == CONFIG.e_IP_Get_Type.Server_Accept_IP)
					CONFIG.m_s_Last_IP = "";
			}

			/*==============================================================
			 * Recv_Ping
			 *==============================================================*/
			internal static void Recv_Ping(double ping)
			{
				invoke(() =>
				{
					m_s_Mainform.textBox_Server_Ping.Text = (ping * 1000).ToString("F3");
				});
			}

			/*==============================================================
			 * Recv_LoginResult
			 *==============================================================*/
			internal static void Recv_LoginResult(bool result)
			{
				if(result)
				{
					m_s_Mainform.add_log("登录服务器成功", Color.Green);

					List<ddns_lib.c_Record> records = null;

					invoke(() =>
					{
						records = m_s_Mainform.make_records();
					});

					if(records == null || records.Count == 0)
					{
						m_s_Mainform.add_log("没有域名需要更新");
						m_s_Mainform.finish_update_records(true);
						return;
					}

					m_s_Mainform.update_records_step3_2_update_by_server(records);
				}
				else
				{
					m_s_Mainform.add_log("登录服务器失败", Color.Red);
					DDNS_CLR.CLR.DisConnect();
				}
			}

			/*==============================================================
			 * Recv_Update_Domains_Result
			 *==============================================================*/
			internal static void Recv_Update_Domains_Result(List<ddns_lib.c_Record> records)
			{
				int failed_count	= 0;
				int succeed_count	= 0;

				invoke(() =>
				{
					for(int i=0; i<records.Count; ++i)
					{
						ddns_lib.c_Record record = records[i];

						ListViewItem LVI = m_s_Mainform.listView_Records.Items[i];

						if(record.m_result_ip.Length > 0)
						{
							++succeed_count;

							LVI.SubItems[(int)e_Column_DomainList.Last_Result].Text			= "成功";
							LVI.SubItems[(int)e_Column_DomainList.Last_Result].ForeColor	= Color.Green;
							LVI.SubItems[(int)e_Column_DomainList.Last_IP].Text				= record.m_result_ip;

							CONFIG.c_Domain domain = CONFIG.find_domain(record.m_name, record.m_domain);

							if(domain != null)
								domain.m_last_ip = record.m_result_ip;

							if(CONFIG.m_s_IP_get_type == CONFIG.e_IP_Get_Type.Server_Accept_IP)
								CONFIG.m_s_Last_IP = record.m_result_ip;

							m_s_Mainform.add_log($"更新 {record.m_name}.{record.m_domain} -> {record.m_result_ip} 成功", Color.Green);
						}
						else
						{
							++failed_count;

							LVI.SubItems[(int)e_Column_DomainList.Last_Result].Text			= "失败";
							LVI.SubItems[(int)e_Column_DomainList.Last_Result].ForeColor	= Color.Red;

							m_s_Mainform.add_log($"更新 {record.m_name}.{record.m_domain} 失败（{record.m_err_msg}）", Color.Red);
						}
					}	// for

					m_s_Mainform.add_log(	$"成功：{succeed_count} 条记录，失败：{failed_count} 条记录",
											(failed_count == 0) ? Color.DarkOrange : Color.Red );

					m_s_Mainform.finish_update_records(true);
				});

				if(succeed_count > 0)
					CONFIG.m_s_dirty = true;
			}

			/*==============================================================
			 * On_add_log
			 *==============================================================*/
			internal static void On_add_log(string txt, Color c)
			{
				m_s_Mainform.add_log(txt, c);
			}
		};
		#endregion


		#region Winform 事件
		/*==============================================================
		 * 窗口加载/关闭
		 *==============================================================*/
		private void frm_MainForm_Load(object sender, EventArgs e)
		{
			this.Icon				= res_Main.icon;
			notifyIcon_Main.Icon	= res_Main.icon;

			ServicePointManager.DefaultConnectionLimit = 1000;

			// 初始化 DDNS_CLR
			DDNS_CLR.CLR.DoInit();

			// 设置回调函数
			DDNS_CLR.CLR.Event_OnConnected					+= EVENTS.OnConnected;
			DDNS_CLR.CLR.Event_OnDisconnecting				+= EVENTS.OnDisconnecting;
			DDNS_CLR.CLR.Event_Recv_Ping					+= EVENTS.Recv_Ping;
			DDNS_CLR.CLR.Event_Recv_LoginResult				+= EVENTS.Recv_LoginResult;
			DDNS_CLR.CLR.Event_Recv_Update_Domains_Result	+= EVENTS.Recv_Update_Domains_Result;
			DDNS_CLR.CLR.Event_On_add_log					+= EVENTS.On_add_log;

			ddns_lib.LIB.EVENTS.Event_On_AddLog				+= EVENTS.On_add_log;

			CONFIG.read_config();

			//==================== 更新 UI ====================(Start)
			//【更新方式】
			switch(CONFIG.m_s_update_type)
			{
			case CONFIG.e_Update_Type.Local:	radioButton_Local.Checked	= true;	break;
			case CONFIG.e_Update_Type.Server:	radioButton_Server.Checked	= true;	break;
			}	// switch

			//【Server 设置】
			textBox_Server_Addr.Text			= CONFIG.m_s_server_addr;
			textBox_Server_User.Text			= CONFIG.m_s_server_user;
			textBox_Server_Pwd.Text				= CONFIG.m_s_server_pwd;
			checkBox_Server_Show_Pwd.Checked	= CONFIG.m_s_show_server_pwd;

			//【IP 设置】
			switch(CONFIG.m_s_IP_get_type)
			{
			case CONFIG.e_IP_Get_Type.Get_IP_From_URL:	radioButton_Get_IP_From_URL.Checked		= true;	break;
			case CONFIG.e_IP_Get_Type.Specific_IP:		radioButton_Specific_IP.Checked			= true;	break;
			case CONFIG.e_IP_Get_Type.Server_Accept_IP:	radioButton_Server_Accept_IP.Checked	= true; break;
			}	// switch

			comboBox_Settings_Get_IP_URL.Text	= CONFIG.m_s_Get_IP_URL;
			textBox_Settings_Last_IP.Text		= CONFIG.m_s_Last_IP;

			if(CONFIG.m_s_Get_IP_URL.Length == 0)
				comboBox_Settings_Get_IP_URL.SelectedIndex = 0;

			//【安全设置】
			textBox_Settings_Key.Text						= CONFIG.m_s_Key;
			checkBox_Settings_Show_Key.Checked				= CONFIG.m_s_show_Key;
			textBox_Settings_Secret.Text					= CONFIG.m_s_Secret;
			checkBox_Settings_Show_Secret.Checked			= CONFIG.m_s_show_Secret;
			checkBox_Settings_Save_Key_and_Secret.Checked	= CONFIG.m_s_save_Key_Secret;

			//【域名列表】
			foreach(CONFIG.c_Domain domain in CONFIG.m_s_domain_list)
				add_LVI(domain.m_name, domain.m_root_domain, domain.m_last_ip, domain.m_TTL);

			//【更新】
			checkBox_Settings_AutoUpdate.Checked				= CONFIG.m_s_AutoUpdate;
			numericUpDown_Settings_AutoUpdate_Interval.Value	= CONFIG.m_s_AutoUpdate_Interval;
			checkBox_Settings_Update_Force.Checked				= CONFIG.m_s_Update_Force;
			//==================== 更新 UI ====================(End)
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
				// 清理 DDNS_CLR
				DDNS_CLR.CLR.DoFinal();
			}
		}



		/*==============================================================
		 * godaddy API 网址
		 *==============================================================*/
		private void linkLabel_godaddy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(linkLabel_godaddy.Text);
		}

		/*==============================================================
		 * github
		 *==============================================================*/
		private void linkLabel_Github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/foxofice/ddns_godaddy");
		}

		/*==============================================================
		 * 官网
		 *==============================================================*/
		private void linkLabel_WebSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://www.AcgDev.com");
		}
		#endregion


		#region 更新方式
		/*==============================================================
		 * 改变 update_type
		 *==============================================================*/
		void change_update_type()
		{
			CONFIG.m_s_update_type = (radioButton_Local.Checked ? CONFIG.e_Update_Type.Local : CONFIG.e_Update_Type.Server);

			textBox_Server_Addr.ReadOnly			= (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local);
			textBox_Server_User.ReadOnly			= (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local);
			textBox_Server_Pwd.ReadOnly				= (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Local);

			radioButton_Server_Accept_IP.Enabled	= (CONFIG.m_s_update_type == CONFIG.e_Update_Type.Server);

			if(CONFIG.m_s_update_type != CONFIG.e_Update_Type.Server)
			{
				if(radioButton_Server_Accept_IP.Checked)
					radioButton_Get_IP_From_URL.Checked = true;
			}

			CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 本地更新（直连）
		 *==============================================================*/
		private void radioButton_Local_CheckedChanged(object sender, EventArgs e)
		{
			change_update_type();
		}


		/*==============================================================
		 * 远程更新（连接到 Server）
		 *==============================================================*/
		private void radioButton_Server_CheckedChanged(object sender, EventArgs e)
		{
			change_update_type();
		}
		#endregion


		#region Server 设置
		/*==============================================================
		 * Server 地址
		 *==============================================================*/
		private void textBox_Server_Addr_TextChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_server_addr = textBox_Server_Addr.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 登录到 Server 的用户名
		 *==============================================================*/
		private void textBox_Server_User_TextChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_server_user = textBox_Server_User.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 登录到 Server 的密码
		 *==============================================================*/
		private void textBox_Server_Pwd_TextChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_server_pwd = textBox_Server_Pwd.Text.Trim();
			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 显示密码
		 *==============================================================*/
		private void checkBox_Server_Show_Pwd_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Server_Pwd.PasswordChar = checkBox_Server_Show_Pwd.Checked ? '\0' : '*';

			CONFIG.m_s_show_server_pwd = checkBox_Server_Show_Pwd.Checked;
			CONFIG.m_s_dirty = true;
		}
		#endregion


		#region IP 设置
		/*==============================================================
		 * 改变 IP_get_type
		 *==============================================================*/
		void change_IP_get_type()
		{
			CONFIG.m_s_IP_get_type = CONFIG.e_IP_Get_Type.Get_IP_From_URL;

			if(radioButton_Specific_IP.Checked)
				CONFIG.m_s_IP_get_type = CONFIG.e_IP_Get_Type.Specific_IP;
			else if(radioButton_Server_Accept_IP.Checked)
				CONFIG.m_s_IP_get_type = CONFIG.e_IP_Get_Type.Server_Accept_IP;

			comboBox_Settings_Get_IP_URL.Enabled	= (CONFIG.m_s_IP_get_type == CONFIG.e_IP_Get_Type.Get_IP_From_URL);
			textBox_Settings_Last_IP.ReadOnly		= (CONFIG.m_s_IP_get_type != CONFIG.e_IP_Get_Type.Specific_IP);

			CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 通过互联网获取公网 IP
		 *==============================================================*/
		private void radioButton_Get_IP_From_URL_CheckedChanged(object sender, EventArgs e)
		{
			change_IP_get_type();
		}


		/*==============================================================
		 * 手动设置 IP
		 *==============================================================*/
		private void radioButton_Specific_IP_CheckedChanged(object sender, EventArgs e)
		{
			change_IP_get_type();
		}


		/*==============================================================
		 * Server 接受连接的客户端 IP
		 *==============================================================*/
		private void radioButton_Server_Accept_IP_CheckedChanged(object sender, EventArgs e)
		{
			change_IP_get_type();
		}


		/*==============================================================
		 * 检查公网IP的URL
		 *==============================================================*/
		private void comboBox_Settings_Get_IP_URL_TextChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_Get_IP_URL = comboBox_Settings_Get_IP_URL.Text.Trim();
			CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 上次获取的IP
		 *==============================================================*/
		private void textBox_Settings_Last_IP_TextChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_Last_IP = textBox_Settings_Last_IP.Text.Trim();
			CONFIG.m_s_dirty = true;
		}
		#endregion


		#region 安全设置
		/*==============================================================
		 * Key
		 *==============================================================*/
		private void textBox_Settings_Key_TextChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_Key = textBox_Settings_Key.Text;

			if(checkBox_Settings_Save_Key_and_Secret.Checked)
				CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * Secret
		 *==============================================================*/
		private void textBox_Settings_Secret_TextChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_Secret = textBox_Settings_Secret.Text;

			if(checkBox_Settings_Save_Key_and_Secret.Checked)
				CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 显示 Key
		 *==============================================================*/
		private void checkBox_Settings_Show_Key_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Settings_Key.PasswordChar = checkBox_Settings_Show_Key.Checked ? '\0' : '*';

			CONFIG.m_s_show_Key = checkBox_Settings_Show_Key.Checked;
			CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 显示 Secret
		 *==============================================================*/
		private void checkBox_Settings_Show_Secret_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Settings_Secret.PasswordChar = checkBox_Settings_Show_Secret.Checked ? '\0' : '*';

			CONFIG.m_s_show_Secret = checkBox_Settings_Show_Secret.Checked;
			CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 保存 Key/Secret 到配置文件中
		 *==============================================================*/
		private void checkBox_Settings_Save_Key_and_Secret_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_save_Key_Secret = checkBox_Settings_Save_Key_and_Secret.Checked;
			CONFIG.m_s_dirty = true;
		}
		#endregion


		#region 域名列表
		/*==============================================================
		 * 添加 LVI
		 *==============================================================*/
		void add_LVI(string name, string root_domain, string ip, int TTL = 0)
		{
			ListViewItem LVI = new ListViewItem();

			while(LVI.SubItems.Count < listView_Records.Columns.Count)
				LVI.SubItems.Add("");

			LVI.SubItems[(int)e_Column_DomainList.Name].Text	= name;
			LVI.SubItems[(int)e_Column_DomainList.Domain].Text	= root_domain;
			LVI.SubItems[(int)e_Column_DomainList.TTL].Text		= (TTL > 0) ? TTL.ToString() : "";
			LVI.SubItems[(int)e_Column_DomainList.Last_IP].Text	= ip;

			listView_Records.Items.Add(LVI);

			LVI.UseItemStyleForSubItems = false;
			LVI.EnsureVisible();
		}


		/*==============================================================
		 * 修改域名
		 *==============================================================*/
		void edit_domain()
		{
			if(listView_Records.SelectedItems.Count == 0)
				return;

			ListViewItem LVI = listView_Records.SelectedItems[0];

			int ttl;
			if(!int.TryParse(LVI.SubItems[(int)e_Column_DomainList.TTL].Text, out ttl))
				ttl = 0;

			frm_Record dlg = new frm_Record(LVI.SubItems[(int)e_Column_DomainList.Name].Text,
											LVI.SubItems[(int)e_Column_DomainList.Domain].Text,
											ttl,
											LVI.Index);

			if(dlg.ShowDialog() == DialogResult.OK)
			{
				ttl = dlg.m_ttl;

				LVI.SubItems[(int)e_Column_DomainList.Name].Text	= dlg.m_name;
				LVI.SubItems[(int)e_Column_DomainList.Domain].Text	= dlg.m_domain;
				LVI.SubItems[(int)e_Column_DomainList.TTL].Text		= (ttl > 0) ? ttl.ToString() : "";

				CONFIG.m_s_dirty = true;
			}
		}

		/*==============================================================
		 * 选择项更改
		 *==============================================================*/
		private void listView_Records_SelectedIndexChanged(object sender, EventArgs e)
		{
			ToolStripMenuItem_Records_Edit.Enabled		= (listView_Records.SelectedItems.Count > 0);
			ToolStripMenuItem_Records_Delete.Enabled	= (listView_Records.SelectedItems.Count > 0);
		}

		/*==============================================================
		 * 双击修改
		 *==============================================================*/
		private void listView_Records_DoubleClick(object sender, EventArgs e)
		{
			edit_domain();
		}
		#endregion
		#region 域名列表 - 上下文菜单
		/*==============================================================
		 * 添加
		 *==============================================================*/
		private void ToolStripMenuItem_Records_Add_Click(object sender, EventArgs e)
		{
			frm_Record dlg = new frm_Record("", "");

			if(dlg.ShowDialog() == DialogResult.OK)
			{
				add_LVI(dlg.m_name, dlg.m_domain, "", dlg.m_ttl);
				CONFIG.m_s_dirty = true;
			}
		}

		/*==============================================================
		 * 删除
		 *==============================================================*/
		private void ToolStripMenuItem_Records_Delete_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show(	$"是否要删除选定的 {listView_Records.SelectedItems.Count} 条记录？",
								this.Text,
								MessageBoxButtons.YesNo,
								MessageBoxIcon.Question,
								MessageBoxDefaultButton.Button2 ) == DialogResult.No)
				return;

			while(listView_Records.SelectedItems.Count > 0)
				listView_Records.Items.Remove(listView_Records.SelectedItems[0]);

			CONFIG.m_s_dirty = true;
		}

		/*==============================================================
		 * 修改
		 *==============================================================*/
		private void ToolStripMenuItem_Records_Edit_Click(object sender, EventArgs e)
		{
			edit_domain();
		}
		#endregion


		#region 计时器
		/*==============================================================
		 * 保存配置文件
		 *==============================================================*/
		private void timer_Save_Config_Tick(object sender, EventArgs e)
		{
			CONFIG.write_config();
		}


		/*==============================================================
		 * 更新 IP
		 *==============================================================*/
		private void timer_Update_Tick(object sender, EventArgs e)
		{
			if(!CONFIG.m_s_AutoUpdate)
				return;

			if(DateTime.Now < m_can_auto_update_time)
				return;

			update_records_step1_start();
		}


		/*==============================================================
		 * ping
		 *==============================================================*/
		private void timer_Ping_Tick(object sender, EventArgs e)
		{
			if(radioButton_Server.Checked && checkBox_Server_Ping.Checked)
				DDNS_CLR.CLR.send_Ping();
		}
		#endregion


		#region 托盘图标
		/*==============================================================
		 * 双击托盘图标
		 *==============================================================*/
		private void notifyIcon_Main_DoubleClick(object sender, EventArgs e)
		{
			if(this.Visible)
				this.Hide();
			else
				active_form();
		}
		#endregion
		#region 托盘图标 - 上下文菜单
		/*==============================================================
		 * 打开
		 *==============================================================*/
		private void ToolStripMenuItem_Open_Click(object sender, EventArgs e)
		{
			active_form();
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


		#region 更新
		/*==============================================================
		 * 设置下次自动更新的时间
		 *==============================================================*/
		void set_next_update_time()
		{
			m_can_auto_update_time	= DateTime.Now.AddSeconds((int)numericUpDown_Settings_AutoUpdate_Interval.Value);
			add_log($"下次更新时间：{m_can_auto_update_time.ToString("G").Replace("/", "-")}", Color.FromArgb(0, 162, 232));
		}


		/*==============================================================
		 * 自动更新时间间隔（秒）
		 *==============================================================*/
		private void checkBox_Settings_AutoUpdate_CheckedChanged(object sender, EventArgs e)
		{
			numericUpDown_Settings_AutoUpdate_Interval.Enabled	= checkBox_Settings_AutoUpdate.Checked;
			timer_Update.Enabled								= checkBox_Settings_AutoUpdate.Checked;

			CONFIG.m_s_AutoUpdate = checkBox_Settings_AutoUpdate.Checked;
			CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 时间间隔
		 *==============================================================*/
		private void numericUpDown_Settings_AutoUpdate_Interval_ValueChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_AutoUpdate_Interval = (uint)numericUpDown_Settings_AutoUpdate_Interval.Value;
			CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 强制更新
		 *==============================================================*/
		private void checkBox_Settings_Update_Force_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.m_s_Update_Force = checkBox_Settings_Update_Force.Checked;
			CONFIG.m_s_dirty = true;
		}


		/*==============================================================
		 * 立即更新
		 *==============================================================*/
		private void button_Settings_Update_Click(object sender, EventArgs e)
		{
			update_records_step1_start();
		}


		/*==============================================================
		 * Step1：开始更新 A/AAAA 记录
		 *==============================================================*/
		void update_records_step1_start()
		{
			if(m_is_updating)
				return;

			m_is_updating = true;
			lock_controls(false);

			if(	CONFIG.m_s_update_type == CONFIG.e_Update_Type.Server			&&
				CONFIG.m_s_IP_get_type == CONFIG.e_IP_Get_Type.Server_Accept_IP	&&
				!DDNS_CLR.CLR.is_connected() )
			{
				CONFIG.m_s_Last_IP = "";
			}

			update_records_step2_get_ip();
		}


		/*==============================================================
		 * Step2：获取 IP
		 *==============================================================*/
		void update_records_step2_get_ip()
		{
			switch(CONFIG.m_s_IP_get_type)
			{
			case CONFIG.e_IP_Get_Type.Get_IP_From_URL:
				if(comboBox_Settings_Get_IP_URL.Text.Length == 0)
				{
					add_log("请输入「检查公网IP的URL」", Color.Red);

					finish_update_records(true);
					return;
				}

				add_log("正在获取当前公网 IP 地址……");

				{
					WebClient wc = new WebClient();
					wc.DownloadStringCompleted += WC_DownloadStringCompleted;

					wc.DownloadStringAsync(new Uri(comboBox_Settings_Get_IP_URL.Text));
				}
				break;

			case CONFIG.e_IP_Get_Type.Specific_IP:
			case CONFIG.e_IP_Get_Type.Server_Accept_IP:
				new Thread(update_records_step3_do_update).Start();
				break;
			}	// switch
		}


		/*==============================================================
		 * 获取公网 IP 完成
		 *==============================================================*/
		private void WC_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			if(e.Error != null)
			{
				add_log(e.Error.Message, Color.Red);

				finish_update_records(true);
				return;
			}

			string ip = e.Result.Replace("\n", "").Trim();
			if(ip.Length == 0)
			{
				add_log("获取 IP 失败（可能是网站提供的数据有问题）", Color.Red);

				finish_update_records(true);
				return;
			}

			textBox_Settings_Last_IP.Text = ip;
			add_log($"当前的公网 IP：{ip}");

			new Thread(update_records_step3_do_update).Start();
		}


		/*==============================================================
		 * 完成更新 A/AAAA 记录
		 *==============================================================*/
		void finish_update_records(bool update_next_time)
		{
			if(update_next_time)
				set_next_update_time();

			m_is_updating = false;
			lock_controls(true);
		}


		/*==============================================================
		 * 创建 records 列表
		 *==============================================================*/
		List<ddns_lib.c_Record> make_records()
		{
			string ip;

			if(CONFIG.m_s_IP_get_type == CONFIG.e_IP_Get_Type.Server_Accept_IP)
				ip = CONFIG.m_s_Last_IP;
			else
				ip = textBox_Settings_Last_IP.Text.Trim();

			string	Key		= textBox_Settings_Key.Text;
			string	Secret	= textBox_Settings_Secret.Text;
			bool	force	= checkBox_Settings_Update_Force.Checked;

			List<ddns_lib.c_Record> records = new List<ddns_lib.c_Record>();

			for(int i=0; i<listView_Records.Items.Count; ++i)
			{
				ListViewItem LVI = listView_Records.Items[i];

				if(!force)
				{
					if(LVI.SubItems[(int)e_Column_DomainList.Last_IP].Text == ip)
						continue;
				}

				ddns_lib.c_Record new_record = new ddns_lib.c_Record();

				new_record.m_name			= LVI.SubItems[(int)e_Column_DomainList.Name].Text;
				new_record.m_domain			= LVI.SubItems[(int)e_Column_DomainList.Domain].Text;

				int.TryParse(LVI.SubItems[(int)e_Column_DomainList.TTL].Text, out new_record.m_TTL);

				new_record.m_ip				= ip;
				new_record.m_Key			= Key;
				new_record.m_Secret			= Secret;
				new_record.m_user_idx		= i;

				records.Add(new_record);
			}	// for

			return records;
		}


		/*==============================================================
		 * Step3：执行更新 IP 记录
		 *==============================================================*/
		void update_records_step3_do_update()
		{
			List<ddns_lib.c_Record> records = null;

			invoke(() =>
			{
				records = make_records();
			});

			if(records == null || records.Count == 0)
			{
				add_log("没有域名需要更新");
				finish_update_records(true);
				return;
			}

			switch(CONFIG.m_s_update_type)
			{
			case CONFIG.e_Update_Type.Local:
				update_records_step3_update_by_local(records);
				break;

			case CONFIG.e_Update_Type.Server:
				update_records_step3_update_by_server(records);
				break;
			}	// switch
		}


		/*==============================================================
		 * Step3：执行更新 IP 记录（本地更新）
		 *==============================================================*/
		void update_records_step3_update_by_local(List<ddns_lib.c_Record> records)
		{
			ThreadStart ts = delegate { ddns_lib.LIB.update_records(ref records); };
			Thread th = new Thread(ts);

			th.Start();
			th.Join();

			int failed_count	= 0;
			int succeed_count	= 0;

			invoke(() =>
			{
				foreach(ddns_lib.c_Record record in records)
				{
					ListViewItem LVI = listView_Records.Items[record.m_user_idx];

					if(record.m_result_ip.Length > 0)
					{
						++succeed_count;

						LVI.SubItems[(int)e_Column_DomainList.Last_Result].Text			= "成功";
						LVI.SubItems[(int)e_Column_DomainList.Last_Result].ForeColor	= Color.Green;
						LVI.SubItems[(int)e_Column_DomainList.Last_IP].Text				= record.m_result_ip;

						CONFIG.c_Domain domain = CONFIG.find_domain(record.m_name, record.m_domain);

						if(domain != null)
							domain.m_last_ip = record.m_result_ip;
					}
					else
					{
						++failed_count;

						LVI.SubItems[(int)e_Column_DomainList.Last_Result].Text			= "失败";
						LVI.SubItems[(int)e_Column_DomainList.Last_Result].ForeColor	= Color.Red;
					}
				}	// for
			});

			add_log($"成功：{succeed_count} 条记录，失败：{failed_count} 条记录",
					(failed_count == 0) ? Color.DarkOrange : Color.Red);

			if(succeed_count > 0)
				CONFIG.m_s_dirty = true;

			finish_update_records(true);
		}


		/*==============================================================
		 * Step3：执行更新 IP 记录（连接到 Server）
		 *==============================================================*/
		void update_records_step3_update_by_server(List<ddns_lib.c_Record> records)
		{
			if(!DDNS_CLR.CLR.is_connected())
			{
				if(!textBox_Server_Addr.Text.Contains(":"))
				{
					add_log("「Server 地址」请使用 <ip>:<port> 的格式", Color.Red);

					finish_update_records(true);
					return;
				}

				add_log("正在连接到 Server……");

				string server_ip	= textBox_Server_Addr.Text.Substring(0, textBox_Server_Addr.Text.LastIndexOf(":"));
				ushort server_port	= ushort.Parse(textBox_Server_Addr.Text.Substring(textBox_Server_Addr.Text.LastIndexOf(":") + 1));

				if(!DDNS_CLR.CLR.Connect(	server_ip,
											server_port,
											textBox_Server_User.Text,
											textBox_Server_Pwd.Text ))
				{
					add_log("连接到 server 失败", Color.Red);

					finish_update_records(true);
					return;
				}
			}
			else
			{
				update_records_step3_2_update_by_server(records);
			}
		}


		/*==============================================================
		 * Step3.2：执行更新 IP 记录（连接到 Server）
		 *==============================================================*/
		void update_records_step3_2_update_by_server(List<ddns_lib.c_Record> records)
		{
			// 发送
			DDNS_CLR.CLR.send_Update_Domains(	CONFIG.m_s_Key,
												CONFIG.m_s_Secret,
												(CONFIG.m_s_IP_get_type == CONFIG.e_IP_Get_Type.Server_Accept_IP) ? "" : CONFIG.m_s_Last_IP,
												records );
		}
		#endregion
	}
}	// namespace ddns
