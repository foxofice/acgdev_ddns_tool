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
		internal ddns_lib.c_Domain	m_domain	= new();
		bool						m_add		= false;	// true = 添加、false = 修改

		List<TabPage>				m_TabPages	= new();

		/*==============================================================
		 * 构造函数
		 *
		 * domain	- 初始化数据
		 *==============================================================*/
		public frm_Domain(ddns_lib.c_Domain? domain = null)
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

			var list = new List<(string m_domain, ddns_lib.e_DomainType m_type)>
			{
				// m_domain,		m_type
				("dns.army",		ddns_lib.e_DomainType.dynv6),
				("dns.navy",		ddns_lib.e_DomainType.dynv6),
				("dynv6.net",		ddns_lib.e_DomainType.dynv6),
				("v6.army",			ddns_lib.e_DomainType.dynv6),
				("v6.navy",			ddns_lib.e_DomainType.dynv6),
				("v6.rocks",		ddns_lib.e_DomainType.dynv6),

				("accesscam.org",	ddns_lib.e_DomainType.dynu),
				("camdvr.org",		ddns_lib.e_DomainType.dynu),
				("casacam.net",		ddns_lib.e_DomainType.dynu),
				("ddnsfree.com",	ddns_lib.e_DomainType.dynu),
				("ddnsgeek.com",	ddns_lib.e_DomainType.dynu),
				("freeddns.org",	ddns_lib.e_DomainType.dynu),
				("giize.com",		ddns_lib.e_DomainType.dynu),
				("gleeze.com",		ddns_lib.e_DomainType.dynu),
				("kozow.com",		ddns_lib.e_DomainType.dynu),
				("loseyourip.com",	ddns_lib.e_DomainType.dynu),
				("mywire.org",		ddns_lib.e_DomainType.dynu),
				("ooguy.com",		ddns_lib.e_DomainType.dynu),
				("theworkpc.com",	ddns_lib.e_DomainType.dynu),
				("webredirect.org",	ddns_lib.e_DomainType.dynu),
				("1cooldns.com",	ddns_lib.e_DomainType.dynu),
				("4cloud.click",	ddns_lib.e_DomainType.dynu),
				("bumbleshrimp.com",ddns_lib.e_DomainType.dynu),
				("dynuddns.com",	ddns_lib.e_DomainType.dynu),
				("dynuddns.net",	ddns_lib.e_DomainType.dynu),
				("ddnsguru.com",	ddns_lib.e_DomainType.dynu),
				("mysynology.net",	ddns_lib.e_DomainType.dynu),
			};

			foreach(var val in list)
			{
				if(	domain.Length > val.m_domain.Length &&
					string.Compare(domain.Substring(domain.Length - val.m_domain.Length), val.m_domain, true) == 0 )
				{
					return val.m_type;
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
			button_OK.Enabled =	(textBox_Domain.Text.Trim().Length > 0)	&&
								(comboBox_Type.SelectedIndex >= 0)		&&
								(comboBox_Security_Profile.SelectedIndex >= 0);
		}

		/*==============================================================
		 * 窗口加载/关闭
		 *==============================================================*/
		private void frm_Domain_Load(object sender, EventArgs e)
		{
			update_language_text();

			if(CONFIG.find_domain(m_domain.m_domain) == null)
			{
				this.Text = ddns_lib.LANGUAGES.txt(200);	// 200: 添加
				this.Icon = IMAGE.image_to_ico(res_Main.Add);
			}
			else
			{
				this.Text = ddns_lib.LANGUAGES.txt(201);	// 201: 修改
				this.Icon = IMAGE.image_to_ico(res_Main.Edit);
			}

			//========== 初始化 UI ==========(Start)
			for(int i = 0; i < (int)ddns_lib.e_DomainType.MAX; ++i)
				comboBox_Type.Items.Add(((ddns_lib.e_DomainType)i).ToString());

			for(int i = 0; i < CONFIG.SECURITY.m_s_profiles.Count; ++i)
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

			textBox_dynu__ID.Text				= (m_domain.m_dynu__ID > 0) ? m_domain.m_dynu__ID.ToString() : "";
			textBox_dynu__TTL.Text				= (m_domain.m_dynu__TTL > 0) ? m_domain.m_dynu__TTL.ToString() : "";

			checkBox_IPv4_Enable.Checked		= m_domain.IPv4.m_enabled;
			checkBox_IPv6_Enable.Checked		= m_domain.IPv6.m_enabled;
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
			ddns_lib.c_Domain? domain = CONFIG.find_domain(textBox_Domain.Text);

			if(domain != null)
			{
				bool is_exists =	m_add	||																// 添加
									(string.Compare(m_domain.m_domain, textBox_Domain.Text, true) != 0);	// 修改

				if(is_exists)
				{
					// 252: {0:s} 已存在，请重新输入
					MessageBox.Show(string.Format(ddns_lib.LANGUAGES.txt(252), textBox_Domain.Text));
					textBox_Domain.Focus();
					return;
				}
			}

			m_domain.m_domain	= textBox_Domain.Text.Trim();
			m_domain.m_type		= (ddns_lib.e_DomainType)comboBox_Type.SelectedIndex;

			if(!int.TryParse(textBox_Godaddy__TTL.Text, out m_domain.m_Godaddy__TTL))
				m_domain.m_Godaddy__TTL = 0;

			m_domain.m_dynv6__Auto_IPv4	= checkBox_dynv6__Auto_IPv4.Checked;
			m_domain.m_dynv6__Auto_IPv6	= checkBox_dynv6__Auto_IPv6.Checked;

			if(!int.TryParse(textBox_dynu__TTL.Text, out m_domain.m_dynu__TTL))
				m_domain.m_dynu__TTL = 0;

			m_domain.m_Security_Profile	= CONFIG.SECURITY.m_s_profiles[comboBox_Security_Profile.SelectedIndex];

			m_domain.IPv4.m_enabled	= checkBox_IPv4_Enable.Checked;
			m_domain.IPv6.m_enabled	= checkBox_IPv6_Enable.Checked;

			// 更新 m_s_domains_list
			if(domain == null)
				CONFIG.m_s_domains_list.Add(m_domain);
			else
				m_domain.CopyTo(domain);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		#region 多语言
		/*==============================================================
		 * 更新多语言文本
		 *==============================================================*/
		void update_language_text()
		{
			label_Domain.Text				= ddns_lib.LANGUAGES.txt(202);	// 202: 域名：
			label_Type.Text					= ddns_lib.LANGUAGES.txt(203);	// 203: 类型：
			label_Godaddy__TTL.Text			= ddns_lib.LANGUAGES.txt(250);	// 250: TTL（秒，可省略）：

			// 251: 自动 IP{0:s}
			checkBox_dynv6__Auto_IPv4.Text	= string.Format(ddns_lib.LANGUAGES.txt(251), "v4");
			checkBox_dynv6__Auto_IPv6.Text	= string.Format(ddns_lib.LANGUAGES.txt(251), "v6");

			label_dynu__TTL.Text			= ddns_lib.LANGUAGES.txt(250);	// 250: TTL（秒，可省略）：

			label_Security_Profile.Text		= ddns_lib.LANGUAGES.txt(204);	// 204: 安全配置：

			// 205: 是否更新 IP{0:s}
			checkBox_IPv4_Enable.Text		= string.Format(ddns_lib.LANGUAGES.txt(205), "v4");
			checkBox_IPv6_Enable.Text		= string.Format(ddns_lib.LANGUAGES.txt(205), "v6");

			button_OK.Text					= ddns_lib.LANGUAGES.txt(50);	// 50: 确定
		}
		#endregion
	};
}	// namespace ddns_tool
