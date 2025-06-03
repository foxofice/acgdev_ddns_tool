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
			int[] widths = { 66, 66 };	// 从 [1] 开始

			for(int i=0; i<widths.Length; ++i)
				listView_Main.Columns[i + 1].Width = widths[i];

			columnHeader_Domain.Width = listView_Main.Width - 21 - widths.Sum();
		}

		/*==============================================================
		 * 设置域名列表
		 *==============================================================*/
		internal void set_domains(List<(string, bool, bool)> IP_change_domains)
		{
			listView_Main.Items.Clear();

			foreach(var vals in IP_change_domains)
			{
				ListViewItem LVI = new ListViewItem();

				while(LVI.SubItems.Count < listView_Main.Columns.Count)
					LVI.SubItems.Add("");

				LVI.SubItems[0].Text = vals.Item1;
				LVI.SubItems[1].Text = vals.Item2 ? frm_MainForm.STR_TRUE : "";
				LVI.SubItems[2].Text = vals.Item3 ? frm_MainForm.STR_TRUE : "";

				listView_Main.Items.Add(LVI);
			}	// for
		}
	};
}	// namespace ddns_tool
