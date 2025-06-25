namespace ddns_tool
{
	partial class frm_Domain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label_Domain = new Label();
			textBox_Domain = new TextBox();
			comboBox_Type = new ComboBox();
			label_Type = new Label();
			tabControl_Type = new TabControl();
			tabPage_Godaddy = new TabPage();
			textBox_Godaddy__TTL = new TextBox();
			label_Godaddy__TTL = new Label();
			tabPage_dynv6 = new TabPage();
			checkBox_dynv6__Auto_IPv6 = new CheckBox();
			checkBox_dynv6__Auto_IPv4 = new CheckBox();
			tabPage_dynu = new TabPage();
			textBox_dynu__TTL = new TextBox();
			label_dynu__TTL = new Label();
			textBox_dynu__ID = new TextBox();
			label_dynu__ID = new Label();
			comboBox_Security_Profile = new ComboBox();
			label_Security_Profile = new Label();
			checkBox_IPv4_Enable = new CheckBox();
			checkBox_IPv6_Enable = new CheckBox();
			button_OK = new Button();
			tabControl_Type.SuspendLayout();
			tabPage_Godaddy.SuspendLayout();
			tabPage_dynv6.SuspendLayout();
			tabPage_dynu.SuspendLayout();
			SuspendLayout();
			// 
			// label_Domain
			// 
			label_Domain.AutoSize = true;
			label_Domain.Location = new Point(12, 14);
			label_Domain.Name = "label_Domain";
			label_Domain.Size = new Size(41, 12);
			label_Domain.TabIndex = 1;
			label_Domain.Text = "域名：";
			// 
			// textBox_Domain
			// 
			textBox_Domain.Location = new Point(83, 11);
			textBox_Domain.Margin = new Padding(3, 2, 3, 2);
			textBox_Domain.Name = "textBox_Domain";
			textBox_Domain.Size = new Size(126, 21);
			textBox_Domain.TabIndex = 2;
			textBox_Domain.TextChanged += textBox_Domain_TextChanged;
			// 
			// comboBox_Type
			// 
			comboBox_Type.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox_Type.FormattingEnabled = true;
			comboBox_Type.Location = new Point(83, 36);
			comboBox_Type.Margin = new Padding(3, 2, 3, 2);
			comboBox_Type.Name = "comboBox_Type";
			comboBox_Type.Size = new Size(126, 20);
			comboBox_Type.TabIndex = 4;
			comboBox_Type.SelectedIndexChanged += comboBox_Type_SelectedIndexChanged;
			// 
			// label_Type
			// 
			label_Type.AutoSize = true;
			label_Type.Location = new Point(12, 39);
			label_Type.Name = "label_Type";
			label_Type.Size = new Size(41, 12);
			label_Type.TabIndex = 3;
			label_Type.Text = "类型：";
			// 
			// tabControl_Type
			// 
			tabControl_Type.Controls.Add(tabPage_Godaddy);
			tabControl_Type.Controls.Add(tabPage_dynv6);
			tabControl_Type.Controls.Add(tabPage_dynu);
			tabControl_Type.Location = new Point(12, 60);
			tabControl_Type.Margin = new Padding(3, 2, 3, 2);
			tabControl_Type.Name = "tabControl_Type";
			tabControl_Type.SelectedIndex = 0;
			tabControl_Type.Size = new Size(197, 83);
			tabControl_Type.TabIndex = 5;
			// 
			// tabPage_Godaddy
			// 
			tabPage_Godaddy.Controls.Add(textBox_Godaddy__TTL);
			tabPage_Godaddy.Controls.Add(label_Godaddy__TTL);
			tabPage_Godaddy.Location = new Point(4, 22);
			tabPage_Godaddy.Margin = new Padding(3, 2, 3, 2);
			tabPage_Godaddy.Name = "tabPage_Godaddy";
			tabPage_Godaddy.Padding = new Padding(3, 2, 3, 2);
			tabPage_Godaddy.Size = new Size(189, 57);
			tabPage_Godaddy.TabIndex = 0;
			tabPage_Godaddy.Text = "Godaddy";
			tabPage_Godaddy.UseVisualStyleBackColor = true;
			// 
			// textBox_Godaddy__TTL
			// 
			textBox_Godaddy__TTL.Location = new Point(131, 6);
			textBox_Godaddy__TTL.Margin = new Padding(3, 2, 3, 2);
			textBox_Godaddy__TTL.Name = "textBox_Godaddy__TTL";
			textBox_Godaddy__TTL.Size = new Size(52, 21);
			textBox_Godaddy__TTL.TabIndex = 7;
			textBox_Godaddy__TTL.Text = "600";
			textBox_Godaddy__TTL.TextAlign = HorizontalAlignment.Center;
			// 
			// label_Godaddy__TTL
			// 
			label_Godaddy__TTL.AutoSize = true;
			label_Godaddy__TTL.Location = new Point(6, 9);
			label_Godaddy__TTL.Name = "label_Godaddy__TTL";
			label_Godaddy__TTL.Size = new Size(119, 12);
			label_Godaddy__TTL.TabIndex = 6;
			label_Godaddy__TTL.Text = "TTL（秒，可省略）：";
			// 
			// tabPage_dynv6
			// 
			tabPage_dynv6.Controls.Add(checkBox_dynv6__Auto_IPv6);
			tabPage_dynv6.Controls.Add(checkBox_dynv6__Auto_IPv4);
			tabPage_dynv6.Location = new Point(4, 22);
			tabPage_dynv6.Margin = new Padding(3, 2, 3, 2);
			tabPage_dynv6.Name = "tabPage_dynv6";
			tabPage_dynv6.Padding = new Padding(3, 2, 3, 2);
			tabPage_dynv6.Size = new Size(189, 57);
			tabPage_dynv6.TabIndex = 1;
			tabPage_dynv6.Text = "dynv6";
			tabPage_dynv6.UseVisualStyleBackColor = true;
			// 
			// checkBox_dynv6__Auto_IPv6
			// 
			checkBox_dynv6__Auto_IPv6.AutoSize = true;
			checkBox_dynv6__Auto_IPv6.Location = new Point(6, 26);
			checkBox_dynv6__Auto_IPv6.Margin = new Padding(3, 2, 3, 2);
			checkBox_dynv6__Auto_IPv6.Name = "checkBox_dynv6__Auto_IPv6";
			checkBox_dynv6__Auto_IPv6.Size = new Size(78, 16);
			checkBox_dynv6__Auto_IPv6.TabIndex = 9;
			checkBox_dynv6__Auto_IPv6.Text = "自动 IPv6";
			checkBox_dynv6__Auto_IPv6.UseVisualStyleBackColor = true;
			// 
			// checkBox_dynv6__Auto_IPv4
			// 
			checkBox_dynv6__Auto_IPv4.AutoSize = true;
			checkBox_dynv6__Auto_IPv4.Checked = true;
			checkBox_dynv6__Auto_IPv4.CheckState = CheckState.Checked;
			checkBox_dynv6__Auto_IPv4.Location = new Point(6, 6);
			checkBox_dynv6__Auto_IPv4.Margin = new Padding(3, 2, 3, 2);
			checkBox_dynv6__Auto_IPv4.Name = "checkBox_dynv6__Auto_IPv4";
			checkBox_dynv6__Auto_IPv4.Size = new Size(78, 16);
			checkBox_dynv6__Auto_IPv4.TabIndex = 8;
			checkBox_dynv6__Auto_IPv4.Text = "自动 IPv4";
			checkBox_dynv6__Auto_IPv4.UseVisualStyleBackColor = true;
			// 
			// tabPage_dynu
			// 
			tabPage_dynu.Controls.Add(textBox_dynu__TTL);
			tabPage_dynu.Controls.Add(label_dynu__TTL);
			tabPage_dynu.Controls.Add(textBox_dynu__ID);
			tabPage_dynu.Controls.Add(label_dynu__ID);
			tabPage_dynu.Location = new Point(4, 22);
			tabPage_dynu.Margin = new Padding(3, 2, 3, 2);
			tabPage_dynu.Name = "tabPage_dynu";
			tabPage_dynu.Padding = new Padding(3, 2, 3, 2);
			tabPage_dynu.Size = new Size(189, 57);
			tabPage_dynu.TabIndex = 2;
			tabPage_dynu.Text = "dynu";
			tabPage_dynu.UseVisualStyleBackColor = true;
			// 
			// textBox_dynu__TTL
			// 
			textBox_dynu__TTL.Location = new Point(131, 31);
			textBox_dynu__TTL.Margin = new Padding(3, 2, 3, 2);
			textBox_dynu__TTL.Name = "textBox_dynu__TTL";
			textBox_dynu__TTL.Size = new Size(52, 21);
			textBox_dynu__TTL.TabIndex = 13;
			textBox_dynu__TTL.Text = "600";
			textBox_dynu__TTL.TextAlign = HorizontalAlignment.Center;
			// 
			// label_dynu__TTL
			// 
			label_dynu__TTL.AutoSize = true;
			label_dynu__TTL.Location = new Point(6, 34);
			label_dynu__TTL.Name = "label_dynu__TTL";
			label_dynu__TTL.Size = new Size(119, 12);
			label_dynu__TTL.TabIndex = 12;
			label_dynu__TTL.Text = "TTL（秒，可省略）：";
			// 
			// textBox_dynu__ID
			// 
			textBox_dynu__ID.Location = new Point(41, 6);
			textBox_dynu__ID.Margin = new Padding(3, 2, 3, 2);
			textBox_dynu__ID.Name = "textBox_dynu__ID";
			textBox_dynu__ID.ReadOnly = true;
			textBox_dynu__ID.Size = new Size(142, 21);
			textBox_dynu__ID.TabIndex = 11;
			// 
			// label_dynu__ID
			// 
			label_dynu__ID.AutoSize = true;
			label_dynu__ID.Location = new Point(6, 9);
			label_dynu__ID.Name = "label_dynu__ID";
			label_dynu__ID.Size = new Size(29, 12);
			label_dynu__ID.TabIndex = 10;
			label_dynu__ID.Text = "ID：";
			// 
			// comboBox_Security_Profile
			// 
			comboBox_Security_Profile.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox_Security_Profile.FormattingEnabled = true;
			comboBox_Security_Profile.Location = new Point(83, 147);
			comboBox_Security_Profile.Margin = new Padding(3, 2, 3, 2);
			comboBox_Security_Profile.Name = "comboBox_Security_Profile";
			comboBox_Security_Profile.Size = new Size(126, 20);
			comboBox_Security_Profile.TabIndex = 51;
			comboBox_Security_Profile.SelectedIndexChanged += comboBox_Security_Profile_SelectedIndexChanged;
			// 
			// label_Security_Profile
			// 
			label_Security_Profile.AutoSize = true;
			label_Security_Profile.Location = new Point(12, 150);
			label_Security_Profile.Name = "label_Security_Profile";
			label_Security_Profile.Size = new Size(65, 12);
			label_Security_Profile.TabIndex = 50;
			label_Security_Profile.Text = "安全配置：";
			// 
			// checkBox_IPv4_Enable
			// 
			checkBox_IPv4_Enable.AutoSize = true;
			checkBox_IPv4_Enable.Location = new Point(12, 171);
			checkBox_IPv4_Enable.Margin = new Padding(3, 2, 3, 2);
			checkBox_IPv4_Enable.Name = "checkBox_IPv4_Enable";
			checkBox_IPv4_Enable.Size = new Size(102, 16);
			checkBox_IPv4_Enable.TabIndex = 52;
			checkBox_IPv4_Enable.Text = "是否更新 IPv4";
			checkBox_IPv4_Enable.UseVisualStyleBackColor = true;
			// 
			// checkBox_IPv6_Enable
			// 
			checkBox_IPv6_Enable.AutoSize = true;
			checkBox_IPv6_Enable.Location = new Point(12, 191);
			checkBox_IPv6_Enable.Margin = new Padding(3, 2, 3, 2);
			checkBox_IPv6_Enable.Name = "checkBox_IPv6_Enable";
			checkBox_IPv6_Enable.Size = new Size(102, 16);
			checkBox_IPv6_Enable.TabIndex = 53;
			checkBox_IPv6_Enable.Text = "是否更新 IPv6";
			checkBox_IPv6_Enable.UseVisualStyleBackColor = true;
			// 
			// button_OK
			// 
			button_OK.Location = new Point(134, 211);
			button_OK.Margin = new Padding(3, 2, 3, 2);
			button_OK.Name = "button_OK";
			button_OK.Size = new Size(75, 23);
			button_OK.TabIndex = 54;
			button_OK.Text = "确定";
			button_OK.UseVisualStyleBackColor = true;
			button_OK.Click += button_OK_Click;
			// 
			// frm_Domain
			// 
			AutoScaleDimensions = new SizeF(6F, 12F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(221, 245);
			Controls.Add(button_OK);
			Controls.Add(checkBox_IPv6_Enable);
			Controls.Add(checkBox_IPv4_Enable);
			Controls.Add(label_Security_Profile);
			Controls.Add(comboBox_Security_Profile);
			Controls.Add(tabControl_Type);
			Controls.Add(comboBox_Type);
			Controls.Add(textBox_Domain);
			Controls.Add(label_Type);
			Controls.Add(label_Domain);
			Font = new Font("新宋体", 9F);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Margin = new Padding(3, 2, 3, 2);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "frm_Domain";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "添加";
			Load += frm_Domain_Load;
			tabControl_Type.ResumeLayout(false);
			tabPage_Godaddy.ResumeLayout(false);
			tabPage_Godaddy.PerformLayout();
			tabPage_dynv6.ResumeLayout(false);
			tabPage_dynv6.PerformLayout();
			tabPage_dynu.ResumeLayout(false);
			tabPage_dynu.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label_Domain;
		private TextBox textBox_Domain;
		private ComboBox comboBox_Type;
		private Label label_Type;
		private TabControl tabControl_Type;
		private TabPage tabPage_Godaddy;
		private TabPage tabPage_dynv6;
		private TabPage tabPage_dynu;
		private TextBox textBox_Godaddy__TTL;
		private Label label_Godaddy__TTL;
		private CheckBox checkBox_dynv6__Auto_IPv6;
		private CheckBox checkBox_dynv6__Auto_IPv4;
		private TextBox textBox_dynu__ID;
		private Label label_dynu__ID;
		private TextBox textBox_dynu__TTL;
		private Label label_dynu__TTL;
		private ComboBox comboBox_Security_Profile;
		private Label label_Security_Profile;
		private CheckBox checkBox_IPv4_Enable;
		private CheckBox checkBox_IPv6_Enable;
		private Button button_OK;
	}
}