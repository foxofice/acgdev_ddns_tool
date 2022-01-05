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

namespace ddns
{
	public partial class frm_MainForm : Form
	{
		public frm_MainForm()
		{
			InitializeComponent();

			m_s_Mainform = this;
		}

		enum e_Config_Header
		{
			get_IP_URL,			// 检查公网 IP 的 URL
			interval,			// 更新的时间间隔（秒）
			auto_update,		// 是否自动更新
			save_Key_Secret,	// 保存 Key/Secret 到配置文件中
			Key,
			Secret,
			Show_Key,
			Show_Secret,

			domain,				// 域名
		};

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

		const string					m_k_CONFIG_FILE			= "config.txt";	// 配置文件的文件名
		bool							m_can_save_concifg		= false;		// 允许保存配置文件
		bool							m_dirty_config			= false;		// 设置是否已改变
		bool							m_exiting				= false;		// 是否正在退出

		// 下次可以获取 IP 的时间
		DateTime						m_next_get_ip_time		= DateTime.Now;
		// 是否正在更新 IP
		bool							m_getting_ip			= false;

		/*==============================================================
		 * 检查是否包含指定域名
		 *==============================================================*/
		internal bool contains_domain(string name, string domain)
		{
			foreach(ListViewItem LVI in listView_Records.Items)
			{
				if(	LVI.SubItems[(int)e_Column_DomainList.Name].Text.ToLower() == name.ToLower().Trim() &&
					LVI.SubItems[(int)e_Column_DomainList.Domain].Text.ToLower() == domain.ToLower().Trim() )
					return true;
			}	// for

			return false;
		}

		/*==============================================================
		 * 添加日志记录
		 *==============================================================*/
		void add_log(string txt, Color c = default)
		{
			ListViewItem LVI = new ListViewItem();

			while(LVI.SubItems.Count < listView_Logs.Columns.Count)
				LVI.SubItems.Add("");

			LVI.SubItems[(int)e_Column_Log.Time].Text	= DateTime.Now.ToString("G").Replace("/", ".");
			LVI.SubItems[(int)e_Column_Log.Log].Text	= txt;

			LVI.ForeColor = c;

			listView_Logs.Items.Add(LVI);

			LVI.EnsureVisible();
		}

		/*==============================================================
		 * 添加新的域名 LVI
		 *==============================================================*/
		void add_domain_LVI(string name, string domain, int ttl)
		{
			ListViewItem LVI = new ListViewItem();

			while(LVI.SubItems.Count < listView_Records.Columns.Count)
				LVI.SubItems.Add("");

			LVI.SubItems[(int)e_Column_DomainList.Name].Text	= name;
			LVI.SubItems[(int)e_Column_DomainList.Domain].Text	= domain;
			LVI.SubItems[(int)e_Column_DomainList.TTL].Text		= (ttl > 0) ? ttl.ToString() : "";

			listView_Records.Items.Add(LVI);

			LVI.UseItemStyleForSubItems = false;
			LVI.EnsureVisible();
		}

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
		 * 更新 A/AAAA 记录
		 *==============================================================*/
		async void update_records()
		{
			if(DateTime.Now < m_next_get_ip_time)
				return;

			if(m_getting_ip)
				return;

			int interval = (int)numericUpDown_Settings_Interval.Value;

			void exit_func()
			{
				m_next_get_ip_time	= DateTime.Now.AddSeconds(interval);
				m_getting_ip		= false;

				add_log($"下次更新时间：{m_next_get_ip_time.ToString("G").Replace("/", ".")}", Color.FromArgb(0, 162, 232));
			}

			m_getting_ip = true;

			if(comboBox_Settings_Get_IP_URL.Text.Length == 0)
			{
				add_log("「检查公网IP的URL」为空，无法获取", Color.Red);

				exit_func();
				return;
			}

			add_log("正在获取当前公网 IP 地址……");

			string ip = "";

			WebClient wc = new WebClient();

			try
			{
				ip = await wc.DownloadStringTaskAsync(comboBox_Settings_Get_IP_URL.Text);
				ip = ip.Replace("\n", "").Trim();

				textBox_Settings_Last_IP.Text = ip;

				add_log($"当前的公网 IP：{ip}");
			}
			catch(Exception ex)
			{
				add_log(ex.Message, Color.Red);

				exit_func();
				return;
			}

			if(ip.Length == 0)
			{
				exit_func();
				return;
			}

			IPAddress address;
			if(!IPAddress.TryParse(ip, out address))
			{
				add_log($"无效的 IP：{ip}", Color.Red);

				exit_func();
				return;
			}

			if(	address.AddressFamily != AddressFamily.InterNetwork &&
				address.AddressFamily != AddressFamily.InterNetworkV6 )
			{
				add_log($"无效的 IPv4 或 IPv6：{ip}", Color.Red);

				exit_func();
				return;
			}

			string record_type	= (address.AddressFamily == AddressFamily.InterNetwork) ? "A" : "AAAA";
			string Key			= textBox_Settings_Key.Text;
			string Secret		= textBox_Settings_Secret.Text;

			int count			= 0;
			int failed_count	= 0;

			foreach(ListViewItem LVI in listView_Records.Items)
			{
				string name		= LVI.SubItems[(int)e_Column_DomainList.Name].Text;
				string domain	= LVI.SubItems[(int)e_Column_DomainList.Domain].Text;
				string ttl_str	= LVI.SubItems[(int)e_Column_DomainList.TTL].Text;

				if(LVI.SubItems[(int)e_Column_DomainList.Last_IP].Text == ip)
				{
					add_log($"{name}.{domain} 的「{record_type} 记录」已经是最新 IP，无需更新");
					continue;
				}

				StringBuilder sb_json = new StringBuilder();

				sb_json.Append("[\n");
				sb_json.Append("	{\n");
				sb_json.Append($"		\"name\":\"{name}\",\n");
				sb_json.Append($"		\"type\":\"{record_type}\",\n");
				sb_json.Append($"		\"data\":\"{ip}\"");

				if(ttl_str.Length > 0)
				{
					sb_json.Append(",\n");
					sb_json.Append($"	\"ttl\":{ttl_str}\n");
				}

				sb_json.Append("	}\n");
				sb_json.Append("]");

				string			url	= $"https://api.godaddy.com/v1/domains/{domain}/records/{record_type}/{name}";
				StringContent	sc	= new StringContent(sb_json.ToString(), Encoding.UTF8, "application/json");

				add_log($"正在更新 {name}.{domain} 的「{record_type} 记录」……");

				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"sso-key {Key}:{Secret}");

				HttpResponseMessage response = await client.PutAsync(url, sc);

				if(!response.IsSuccessStatusCode)
				{
					LVI.SubItems[(int)e_Column_DomainList.Last_Result].Text			= "失败";
					LVI.SubItems[(int)e_Column_DomainList.Last_Result].ForeColor	= Color.Red;

					add_log($"更新 {name}.{domain} 的「{record_type} 记录」失败（StatusCode = {response.StatusCode}，ReasonPhrase = {response.ReasonPhrase}）",
							Color.Red);

					++failed_count;
					continue;
				}

				LVI.SubItems[(int)e_Column_DomainList.Last_Result].Text			= "成功";
				LVI.SubItems[(int)e_Column_DomainList.Last_Result].ForeColor	= Color.Green;

				LVI.SubItems[(int)e_Column_DomainList.Last_IP].Text				= ip;

				add_log($"更新 {name}.{domain} 的「{record_type} 记录」成功", Color.Green);

				++count;
			}	// for

			add_log($"成功：{count} 条记录，失败：{failed_count} 条记录", (failed_count == 0) ? Color.DarkOrange : Color.Red);
			exit_func();
		}

		#region 读写配置文件
		/*==============================================================
		 * 读写配置文件
		 *==============================================================*/
		void load_config()
		{
			if(!File.Exists(m_k_CONFIG_FILE))
				return;

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

				if(w1.ToLower() == e_Config_Header.get_IP_URL.ToString().ToLower())
				{
					comboBox_Settings_Get_IP_URL.Text = w2.Trim();
					continue;
				}

				if(w1.ToLower() == e_Config_Header.interval.ToString().ToLower())
				{
					int interval;

					if(int.TryParse(w2, out interval))
						numericUpDown_Settings_Interval.Value = interval;

					continue;
				}

				if(w1.ToLower() == e_Config_Header.auto_update.ToString().ToLower())
				{
					bool val;

					if(bool.TryParse(w2, out val))
						checkBox_Settings_AutoUpdate.Checked = val;

					continue;
				}

				if(w1.ToLower() == e_Config_Header.save_Key_Secret.ToString().ToLower())
				{
					bool val;

					if(bool.TryParse(w2, out val))
						checkBox_Settings_Save_Key_and_Secret.Checked = val;

					continue;
				}

				if(w1.ToLower() == e_Config_Header.Key.ToString().ToLower())
				{
					textBox_Settings_Key.Text = w2.Trim();
					continue;
				}

				if(w1.ToLower() == e_Config_Header.Secret.ToString().ToLower())
				{
					textBox_Settings_Secret.Text = w2.Trim();
					continue;
				}

				if(w1.ToLower() == e_Config_Header.Show_Key.ToString().ToLower())
				{
					bool val;

					if(bool.TryParse(w2, out val))
						checkBox_Settings_Show_Key.Checked = val;

					continue;
				}

				if(w1.ToLower() == e_Config_Header.Show_Secret.ToString().ToLower())
				{
					bool val;

					if(bool.TryParse(w2, out val))
						checkBox_Settings_Show_Secret.Checked = val;

					continue;
				}

				if(w1.ToLower() == e_Config_Header.domain.ToString().ToLower())
				{
					string[] vals = w2.Split(',');

					if(vals.Length < 2)
						continue;

					string	name	= vals[0].Trim();
					string	domain	= vals[1].Trim();
					int		ttl		= 0;

					if(vals.Length >= 3)
					{
						if(!int.TryParse(vals[2], out ttl))
							ttl = 0;
					}

					add_domain_LVI(name, domain, ttl);
				}
			}	// for

			add_log($"读取配置文件 {m_k_CONFIG_FILE}", Color.Green);
		}
		//--------------------------------------------------
		void save_config()
		{
			if(!m_can_save_concifg)
				return;

			if(!m_dirty_config)
				return;

			StringBuilder sb = new StringBuilder();

			sb.AppendLine("// 检查公网 IP 的 URL");
			sb.AppendLine($"{e_Config_Header.get_IP_URL}: {comboBox_Settings_Get_IP_URL.Text.Trim()}");
			sb.AppendLine();

			sb.AppendLine("// 更新的时间间隔（秒）");
			sb.AppendLine($"{e_Config_Header.interval}: {numericUpDown_Settings_Interval.Value}");
			sb.AppendLine();

			sb.AppendLine("// 是否自动更新");
			sb.AppendLine($"{e_Config_Header.auto_update}: {checkBox_Settings_AutoUpdate.Checked}");
			sb.AppendLine();

			sb.AppendLine("// 保存 Key/Secret 到配置文件中");
			sb.AppendLine($"{e_Config_Header.save_Key_Secret}: {checkBox_Settings_Save_Key_and_Secret.Checked}");
			sb.AppendLine();

			if(checkBox_Settings_Save_Key_and_Secret.Checked)
			{
				sb.AppendLine("// Key");
				sb.AppendLine($"{e_Config_Header.Key}: {textBox_Settings_Key.Text.Trim()}");
				sb.AppendLine();

				sb.AppendLine("// Secret");
				sb.AppendLine($"{e_Config_Header.Secret}: {textBox_Settings_Secret.Text.Trim()}");
				sb.AppendLine();
			}

			sb.AppendLine("// 显示 Key");
			sb.AppendLine($"{e_Config_Header.Show_Key}: {checkBox_Settings_Show_Key.Checked}");
			sb.AppendLine();

			sb.AppendLine("// 显示 Secret");
			sb.AppendLine($"{e_Config_Header.Show_Secret}: {checkBox_Settings_Show_Secret.Checked}");
			sb.AppendLine();

			sb.AppendLine("// 域名列表");
			foreach(ListViewItem LVI in listView_Records.Items)
			{
				string name		= LVI.SubItems[(int)e_Column_DomainList.Name].Text;
				string domain	= LVI.SubItems[(int)e_Column_DomainList.Domain].Text;
				string ttl		= LVI.SubItems[(int)e_Column_DomainList.TTL].Text;

				sb.AppendLine($"{e_Config_Header.domain}: {name},{domain},{ttl}");
			}	// for

			File.WriteAllText(m_k_CONFIG_FILE, sb.ToString(), Encoding.UTF8);

			add_log($"保存配置文件 {m_k_CONFIG_FILE}", Color.Blue);

			m_dirty_config = false;
		}
		#endregion

		#region Winform 事件
		/*==============================================================
		 * 窗口加载/关闭
		 *==============================================================*/
		private void frm_MainForm_Load(object sender, EventArgs e)
		{
			this.Icon				= res_Main.icon;
			notifyIcon_Main.Icon	= res_Main.icon;

			comboBox_Settings_Get_IP_URL.SelectedIndex = 0;

			load_config();

			m_dirty_config		= false;
			m_can_save_concifg	= true;
		}
		//--------------------------------------------------
		private void frm_MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(!m_exiting)
			{
				e.Cancel = true;
				this.Hide();
			}
		}

		/*==============================================================
		 * 保存配置文件（计时器）
		 *==============================================================*/
		private void timer_Save_Config_Tick(object sender, EventArgs e)
		{
			save_config();
		}

		/*==============================================================
		 * 更新 IP（计时器）
		 *==============================================================*/
		private void timer_Update_Tick(object sender, EventArgs e)
		{
			update_records();
		}

		/*==============================================================
		 * 打开网站
		 *==============================================================*/
		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				LinkLabel ll = (LinkLabel)sender;

				Process.Start(ll.Text);
			}
			catch(Exception)
			{
			}
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

		#region 设置
		/*==============================================================
		 * 检查公网IP的URL
		 *==============================================================*/
		private void comboBox_Settings_Get_IP_URL_TextChanged(object sender, EventArgs e)
		{
			m_dirty_config = true;
		}

		/*==============================================================
		 * 立即更新
		 *==============================================================*/
		private void button_Settings_Update_Click(object sender, EventArgs e)
		{
			m_next_get_ip_time = DateTime.Now;
			update_records();
		}

		/*==============================================================
		 * 时间间隔
		 *==============================================================*/
		private void numericUpDown_Settings_Interval_ValueChanged(object sender, EventArgs e)
		{
			m_dirty_config = true;
		}

		/*==============================================================
		 * 自动更新
		 *==============================================================*/
		private void checkBox_Settings_AutoUpdate_CheckedChanged(object sender, EventArgs e)
		{
			numericUpDown_Settings_Interval.Enabled = checkBox_Settings_AutoUpdate.Checked;
			timer_Update.Enabled					= checkBox_Settings_AutoUpdate.Checked;

			m_dirty_config = true;
		}

		/*==============================================================
		 * Key
		 *==============================================================*/
		private void textBox_Settings_Key_TextChanged(object sender, EventArgs e)
		{
			m_dirty_config = true;
		}

		/*==============================================================
		 * Secret
		 *==============================================================*/
		private void textBox_Settings_Secret_TextChanged(object sender, EventArgs e)
		{
			m_dirty_config = true;
		}

		/*==============================================================
		 * 显示 Key
		 *==============================================================*/
		private void checkBox_Settings_Show_Key_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Settings_Key.PasswordChar = checkBox_Settings_Show_Key.Checked ? '\0' : '*';

			m_dirty_config = true;
		}

		/*==============================================================
		 * 显示 Secret
		 *==============================================================*/
		private void checkBox_Settings_Show_Secret_CheckedChanged(object sender, EventArgs e)
		{
			textBox_Settings_Secret.PasswordChar = checkBox_Settings_Show_Secret.Checked ? '\0' : '*';

			m_dirty_config = true;
		}

		/*==============================================================
		 * 保存 Key/Secret 到配置文件中
		 *==============================================================*/
		private void checkBox_Settings_Save_Key_and_Secret_CheckedChanged(object sender, EventArgs e)
		{
			m_dirty_config = true;
		}
		#endregion

		#region 域名列表
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
			dlg.ShowDialog();

			if(dlg.m_res == DialogResult.OK)
			{
				ttl = dlg.m_ttl;

				LVI.SubItems[(int)e_Column_DomainList.Name].Text	= dlg.m_name;
				LVI.SubItems[(int)e_Column_DomainList.Domain].Text	= dlg.m_domain;
				LVI.SubItems[(int)e_Column_DomainList.TTL].Text		= (ttl > 0) ? ttl.ToString() : "";

				m_dirty_config = true;
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
			dlg.ShowDialog();

			if(dlg.m_res == DialogResult.OK)
			{
				add_domain_LVI(dlg.m_name, dlg.m_domain, dlg.m_ttl);
				m_dirty_config = true;
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

			m_dirty_config = true;
		}

		/*==============================================================
		 * 修改
		 *==============================================================*/
		private void ToolStripMenuItem_Records_Edit_Click(object sender, EventArgs e)
		{
			edit_domain();
		}
		#endregion

		#region 日志
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
	};
}	// namespace ddns
