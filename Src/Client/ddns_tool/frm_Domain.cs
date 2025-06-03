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
	public partial class frm_Domain : Form
	{
		internal ddns_lib.c_Domain	m_domain	= new ddns_lib.c_Domain();
		bool						m_add		= false;	// true = 添加、false = 修改

		List<TabPage>				m_TabPages	= new List<TabPage>();

		/*==============================================================
		 * 构造函数
		 *
		 * domain	- 初始化数据
		 *==============================================================*/
		public frm_Domain(ddns_lib.c_Domain domain = null)
		{
			InitializeComponent();

			if(domain != null)
				domain.CopyTo(m_domain);
		}

		/*==============================================================
		 * 域名 -> type
		 *==============================================================*/
		ddns_lib.e_DomainType guess_type()
		{
			string domain = textBox_Domain.Text.Trim();

			// dynv6
			string[] dynv6_domains =
			{
				"dns.army",
				"dns.navy",
				"dynv6.net",
				"v6.army",
				"v6.navy",
				"v6.rocks",
			};

			foreach(string dynv6_domain in dynv6_domains)
			{
				if(	domain.Length > dynv6_domain.Length &&
					string.Compare(domain.Substring(domain.Length - dynv6_domain.Length), dynv6_domain, true) == 0 )
				{
					return ddns_lib.e_DomainType.dynv6;
				}
			}	// for

			// Godaddy
			return ddns_lib.e_DomainType.Godaddy;
		}

		/*==============================================================
		 * 更新「确定」UI
		 *==============================================================*/
		void update_OK_UI()
		{
			button_OK.Enabled =	(textBox_Domain.Text.Trim().Length > 0) &&
								(comboBox_Type.SelectedIndex >= 0)		&&
								(comboBox_Security_Profile.SelectedIndex >= 0);
		}

		/*==============================================================
		 * 窗口加载/关闭
		 *==============================================================*/
		private void frm_Domain_Load(object sender, EventArgs e)
		{
			if(CONFIG.find_domain(m_domain.m_domain) == null)
			{
				this.Text = "添加";
				this.Icon = IMAGE.image_to_ico(res_Main.Add);
			}
			else
			{
				this.Text = "修改";
				this.Icon = IMAGE.image_to_ico(res_Main.Edit);
			}

			//========== 初始化 UI ==========(Start)
			for(int i=0; i<(int)ddns_lib.e_DomainType.MAX; ++i)
				comboBox_Type.Items.Add(((ddns_lib.e_DomainType)i).ToString());

			for(int i=0; i<CONFIG.SECURITY.m_s_profiles.Count; ++i)
			{
				ddns_lib.c_Security_Profile profile = CONFIG.SECURITY.m_s_profiles[i];
				comboBox_Security_Profile.Items.Add(profile.m_Name);

				if(m_domain.m_Security_Profile == profile)
					comboBox_Security_Profile.SelectedIndex = i;
			}	// for
			//========== 初始化 UI ==========(End)

			foreach(TabPage tp in tabControl_Type.TabPages)
				m_TabPages.Add(tp);

			while(tabControl_Type.TabPages.Count > 1)
				tabControl_Type.TabPages.RemoveAt(1);

			textBox_Domain.Text					= m_domain.m_domain.Trim();

			textBox_Godaddy__TTL.Text			= (m_domain.m_Godaddy__TTL > 0) ? m_domain.m_Godaddy__TTL.ToString() : "";

			checkBox_dynv6__Auto_IPv4.Checked	= m_domain.m_dynv6__Auto_IPv4;
			checkBox_dynv6__Auto_IPv6.Checked	= m_domain.m_dynv6__Auto_IPv6;
		}

		/*==============================================================
		 * 域名
		 *==============================================================*/
		private void textBox_Domain_TextChanged(object sender, EventArgs e)
		{
			comboBox_Type.SelectedIndex = (int)guess_type();

			textBox_Domain.Focus();

			update_OK_UI();
		}

		/*==============================================================
		 * 类型
		 *==============================================================*/
		private void comboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
		{
			tabControl_Type.TabPages.Clear();
			tabControl_Type.TabPages.Add(m_TabPages[comboBox_Type.SelectedIndex]);

			comboBox_Type.Focus();

			update_OK_UI();
		}

		/*==============================================================
		 * 安全配置
		 *==============================================================*/
		private void comboBox_Security_Profile_SelectedIndexChanged(object sender, EventArgs e)
		{
			update_OK_UI();
		}

		/*==============================================================
		 * 确定
		 *==============================================================*/
		private void button_OK_Click(object sender, EventArgs e)
		{
			ddns_lib.c_Domain domain = CONFIG.find_domain(textBox_Domain.Text);

			if(domain != null)
			{
				bool is_exists =	m_add	||																// 添加
									(string.Compare(m_domain.m_domain, textBox_Domain.Text, true) != 0);	// 修改

				if(is_exists)
				{
					MessageBox.Show($"{textBox_Domain.Text} 已存在，请重新输入");
					textBox_Domain.Focus();
					return;
				}
			}

			m_domain.m_domain	= textBox_Domain.Text.Trim();
			m_domain.m_type		= (ddns_lib.e_DomainType)comboBox_Type.SelectedIndex;

			if(!uint.TryParse(textBox_Godaddy__TTL.Text, out m_domain.m_Godaddy__TTL))
				m_domain.m_Godaddy__TTL = 0;

			m_domain.m_dynv6__Auto_IPv4	= checkBox_dynv6__Auto_IPv4.Checked;
			m_domain.m_dynv6__Auto_IPv6	= checkBox_dynv6__Auto_IPv6.Checked;

			m_domain.m_Security_Profile	= CONFIG.SECURITY.m_s_profiles[comboBox_Security_Profile.SelectedIndex];

			// 更新 m_s_domains_list
			if(domain == null)
				CONFIG.m_s_domains_list.Add(m_domain);
			else
				m_domain.CopyTo(domain);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	};
}	// namespace ddns_tool
