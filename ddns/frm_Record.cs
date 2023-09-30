using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ddns
{
	public partial class frm_Record : Form
	{
		/*==============================================================
		 * 构造函数
		 *
		 * TTL			- 秒计算（<=0 时表示省略）
		 * edit_index	- 修改的索引（<0 时，表示添加）
		 *==============================================================*/
		public frm_Record(string name, string domain, int ttl = 0, int edit_index = -1)
		{
			InitializeComponent();

			m_name			= name;
			m_domain		= domain;
			m_ttl			= ttl;
			m_edit_index	= edit_index;
		}

		internal string			m_name			= "";
		internal string			m_domain		= "";
		internal int			m_ttl			= 0;
		int						m_edit_index	= -1;

		/*==============================================================
		 * 窗口加载/关闭
		 *==============================================================*/
		private void frm_Record_Load(object sender, EventArgs e)
		{
			this.Icon = IMAGE.image_to_ico((m_edit_index < 0) ? res_Main.Add : res_Main.Edit);
			this.Text = (m_edit_index < 0) ? "添加" : "修改";

			textBox_Name.Text	= m_name;
			textBox_Domain.Text	= m_domain;
			textBox_TTL.Text	= (m_ttl > 0) ? m_ttl.ToString() : "";
		}


		/*==============================================================
		 * 确定
		 *==============================================================*/
		private void button_OK_Click(object sender, EventArgs e)
		{
			if(textBox_Name.Text.Length == 0)
			{
				MessageBox.Show("请输入 <Name>");
				textBox_Name.Focus();
				return;
			}

			if(textBox_Domain.Text.Length == 0)
			{
				MessageBox.Show("请输入 <Domain>");
				textBox_Domain.Focus();
				return;
			}

			if(CONFIG.find_domain(textBox_Name.Text, textBox_Domain.Text) != null)
			{
				bool is_exists =	(m_edit_index < 0)	||															// 添加
									(m_name != textBox_Name.Text.Trim() || m_domain != textBox_Domain.Text.Trim());	// 修改

				if(is_exists)
				{
					MessageBox.Show($"{textBox_Name.Text}.{textBox_Domain.Text} 已存在，请重新输入");
					textBox_Name.Focus();
					return;
				}
			}

			m_name		= textBox_Name.Text.Trim();
			m_domain	= textBox_Domain.Text.Trim();

			if(!int.TryParse(textBox_TTL.Text, out m_ttl))
				m_ttl = 0;

			CONFIG.c_Domain new_domain = new CONFIG.c_Domain();
			new_domain.m_name			= m_name;
			new_domain.m_root_domain	= m_domain;
			new_domain.m_TTL			= m_ttl;

			CONFIG.m_s_domain_list.Add(new_domain);

			this.Close();
		}
	};
}	// namespace ddns
