using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ddns_tool
{
	public partial class frm_IP_Change_Popup : Form
	{
		public frm_IP_Change_Popup()
		{
			InitializeComponent();
		}

		/*==============================================================
		 * 窗口加载/关闭
		 *==============================================================*/
		private void frm_IP_Change_Popup_Load(object sender, EventArgs e)
		{
			this.Icon = IMAGE.get_exe_icon();

			update_language_text();
		}
		//--------------------------------------------------
		private void frm_IP_Change_Popup_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}

		/*==============================================================
		 * 调整列宽
		 *==============================================================*/
		private void listView_Main_Resize(object sender, EventArgs e)
		{
			int[] widths = { 0, 66, 66 }; 

			for(int i = 1; i < widths.Length; ++i)
				listView_Main.Columns[i].Width = widths[i];

			columnHeader_Domain.Width = listView_Main.Width - 21 - widths.Sum();
		}

		/*==============================================================
		 * 设置域名列表
		 *==============================================================*/
		internal void set_domains(List<ddns_lib.c_Domain> IP_change_domains)
		{
			listView_Main.Items.Clear();

			foreach(ddns_lib.c_Domain domain in IP_change_domains)
			{
				ListViewItem LVI = new();

				while(LVI.SubItems.Count < listView_Main.Columns.Count)
					LVI.SubItems.Add("");

				LVI.SubItems[0].Text = domain.m_domain;
				LVI.SubItems[1].Text = domain.IPv4.m_same_ip ? "" : COMMON.STR_TRUE;
				LVI.SubItems[2].Text = domain.IPv6.m_same_ip ? "" : COMMON.STR_TRUE;

				listView_Main.Items.Add(LVI);
			}	// for
		}

		#region 多语言
		/*==============================================================
		 * 更新多语言文本
		 *==============================================================*/
		void update_language_text()
		{
			this.Text					= ddns_lib.LANGUAGES.txt(300);	// 300: IP发生变化的域名
			columnHeader_Domain.Text	= ddns_lib.LANGUAGES.txt(301);	// 301: 域名

			// 302: IP{0:s}变化
			columnHeader_IPv4.Text		= string.Format(ddns_lib.LANGUAGES.txt(302), "v4");
			columnHeader_IPv6.Text		= string.Format(ddns_lib.LANGUAGES.txt(302), "v6");
		}
		#endregion
	};
}	// namespace ddns_tool
