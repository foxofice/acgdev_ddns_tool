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
			this.label_Domain = new System.Windows.Forms.Label();
			this.textBox_Domain = new System.Windows.Forms.TextBox();
			this.label_Godaddy__TTL = new System.Windows.Forms.Label();
			this.textBox_Godaddy__TTL = new System.Windows.Forms.TextBox();
			this.comboBox_Type = new System.Windows.Forms.ComboBox();
			this.label_Type = new System.Windows.Forms.Label();
			this.checkBox_dynv6__Auto_IPv6 = new System.Windows.Forms.CheckBox();
			this.checkBox_dynv6__Auto_IPv4 = new System.Windows.Forms.CheckBox();
			this.button_OK = new System.Windows.Forms.Button();
			this.tabControl_Type = new System.Windows.Forms.TabControl();
			this.tabPage_Godaddy = new System.Windows.Forms.TabPage();
			this.tabPage_dynv6 = new System.Windows.Forms.TabPage();
			this.tabPage_dynu = new System.Windows.Forms.TabPage();
			this.textBox_dynu__ID = new System.Windows.Forms.TextBox();
			this.textBox_dynu__TTL = new System.Windows.Forms.TextBox();
			this.label_dynu__ID = new System.Windows.Forms.Label();
			this.label_dynu__TTL = new System.Windows.Forms.Label();
			this.label_Security_Profile = new System.Windows.Forms.Label();
			this.comboBox_Security_Profile = new System.Windows.Forms.ComboBox();
			this.checkBox_IPv4_Enable = new System.Windows.Forms.CheckBox();
			this.checkBox_IPv6_Enable = new System.Windows.Forms.CheckBox();
			this.tabControl_Type.SuspendLayout();
			this.tabPage_Godaddy.SuspendLayout();
			this.tabPage_dynv6.SuspendLayout();
			this.tabPage_dynu.SuspendLayout();
			this.SuspendLayout();
			// 
			// label_Domain
			// 
			this.label_Domain.AutoSize = true;
			this.label_Domain.Location = new System.Drawing.Point(12, 15);
			this.label_Domain.Name = "label_Domain";
			this.label_Domain.Size = new System.Drawing.Size(41, 12);
			this.label_Domain.TabIndex = 0;
			this.label_Domain.Text = "域名：";
			// 
			// textBox_Domain
			// 
			this.textBox_Domain.Location = new System.Drawing.Point(83, 12);
			this.textBox_Domain.Name = "textBox_Domain";
			this.textBox_Domain.Size = new System.Drawing.Size(126, 21);
			this.textBox_Domain.TabIndex = 1;
			this.textBox_Domain.TextChanged += new System.EventHandler(this.textBox_Domain_TextChanged);
			// 
			// label_Godaddy__TTL
			// 
			this.label_Godaddy__TTL.AutoSize = true;
			this.label_Godaddy__TTL.Location = new System.Drawing.Point(6, 9);
			this.label_Godaddy__TTL.Name = "label_Godaddy__TTL";
			this.label_Godaddy__TTL.Size = new System.Drawing.Size(119, 12);
			this.label_Godaddy__TTL.TabIndex = 0;
			this.label_Godaddy__TTL.Text = "TTL（秒，可省略）：";
			// 
			// textBox_Godaddy__TTL
			// 
			this.textBox_Godaddy__TTL.Location = new System.Drawing.Point(131, 6);
			this.textBox_Godaddy__TTL.Name = "textBox_Godaddy__TTL";
			this.textBox_Godaddy__TTL.Size = new System.Drawing.Size(52, 21);
			this.textBox_Godaddy__TTL.TabIndex = 1;
			this.textBox_Godaddy__TTL.Text = "600";
			this.textBox_Godaddy__TTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// comboBox_Type
			// 
			this.comboBox_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Type.FormattingEnabled = true;
			this.comboBox_Type.Location = new System.Drawing.Point(83, 39);
			this.comboBox_Type.Name = "comboBox_Type";
			this.comboBox_Type.Size = new System.Drawing.Size(126, 20);
			this.comboBox_Type.TabIndex = 2;
			this.comboBox_Type.SelectedIndexChanged += new System.EventHandler(this.comboBox_Type_SelectedIndexChanged);
			// 
			// label_Type
			// 
			this.label_Type.AutoSize = true;
			this.label_Type.Location = new System.Drawing.Point(12, 42);
			this.label_Type.Name = "label_Type";
			this.label_Type.Size = new System.Drawing.Size(41, 12);
			this.label_Type.TabIndex = 0;
			this.label_Type.Text = "类型：";
			// 
			// checkBox_dynv6__Auto_IPv6
			// 
			this.checkBox_dynv6__Auto_IPv6.AutoSize = true;
			this.checkBox_dynv6__Auto_IPv6.Location = new System.Drawing.Point(90, 6);
			this.checkBox_dynv6__Auto_IPv6.Name = "checkBox_dynv6__Auto_IPv6";
			this.checkBox_dynv6__Auto_IPv6.Size = new System.Drawing.Size(78, 16);
			this.checkBox_dynv6__Auto_IPv6.TabIndex = 72;
			this.checkBox_dynv6__Auto_IPv6.Text = "自动 IPv6";
			this.checkBox_dynv6__Auto_IPv6.UseVisualStyleBackColor = true;
			// 
			// checkBox_dynv6__Auto_IPv4
			// 
			this.checkBox_dynv6__Auto_IPv4.AutoSize = true;
			this.checkBox_dynv6__Auto_IPv4.Checked = true;
			this.checkBox_dynv6__Auto_IPv4.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_dynv6__Auto_IPv4.Location = new System.Drawing.Point(6, 6);
			this.checkBox_dynv6__Auto_IPv4.Name = "checkBox_dynv6__Auto_IPv4";
			this.checkBox_dynv6__Auto_IPv4.Size = new System.Drawing.Size(78, 16);
			this.checkBox_dynv6__Auto_IPv4.TabIndex = 73;
			this.checkBox_dynv6__Auto_IPv4.Text = "自动 IPv4";
			this.checkBox_dynv6__Auto_IPv4.UseVisualStyleBackColor = true;
			// 
			// button_OK
			// 
			this.button_OK.Enabled = false;
			this.button_OK.Location = new System.Drawing.Point(134, 224);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(75, 23);
			this.button_OK.TabIndex = 5;
			this.button_OK.Text = "确定";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
			// 
			// tabControl_Type
			// 
			this.tabControl_Type.Controls.Add(this.tabPage_Godaddy);
			this.tabControl_Type.Controls.Add(this.tabPage_dynv6);
			this.tabControl_Type.Controls.Add(this.tabPage_dynu);
			this.tabControl_Type.Location = new System.Drawing.Point(12, 65);
			this.tabControl_Type.Name = "tabControl_Type";
			this.tabControl_Type.SelectedIndex = 0;
			this.tabControl_Type.Size = new System.Drawing.Size(197, 83);
			this.tabControl_Type.TabIndex = 6;
			// 
			// tabPage_Godaddy
			// 
			this.tabPage_Godaddy.Controls.Add(this.textBox_Godaddy__TTL);
			this.tabPage_Godaddy.Controls.Add(this.label_Godaddy__TTL);
			this.tabPage_Godaddy.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Godaddy.Name = "tabPage_Godaddy";
			this.tabPage_Godaddy.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_Godaddy.Size = new System.Drawing.Size(189, 57);
			this.tabPage_Godaddy.TabIndex = 0;
			this.tabPage_Godaddy.Text = "Godaddy";
			this.tabPage_Godaddy.UseVisualStyleBackColor = true;
			// 
			// tabPage_dynv6
			// 
			this.tabPage_dynv6.Controls.Add(this.checkBox_dynv6__Auto_IPv6);
			this.tabPage_dynv6.Controls.Add(this.checkBox_dynv6__Auto_IPv4);
			this.tabPage_dynv6.Location = new System.Drawing.Point(4, 22);
			this.tabPage_dynv6.Name = "tabPage_dynv6";
			this.tabPage_dynv6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_dynv6.Size = new System.Drawing.Size(189, 57);
			this.tabPage_dynv6.TabIndex = 1;
			this.tabPage_dynv6.Text = "dynv6";
			this.tabPage_dynv6.UseVisualStyleBackColor = true;
			// 
			// tabPage_dynu
			// 
			this.tabPage_dynu.Controls.Add(this.textBox_dynu__ID);
			this.tabPage_dynu.Controls.Add(this.textBox_dynu__TTL);
			this.tabPage_dynu.Controls.Add(this.label_dynu__ID);
			this.tabPage_dynu.Controls.Add(this.label_dynu__TTL);
			this.tabPage_dynu.Location = new System.Drawing.Point(4, 22);
			this.tabPage_dynu.Name = "tabPage_dynu";
			this.tabPage_dynu.Size = new System.Drawing.Size(189, 57);
			this.tabPage_dynu.TabIndex = 2;
			this.tabPage_dynu.Text = "dynu";
			this.tabPage_dynu.UseVisualStyleBackColor = true;
			// 
			// textBox_dynu__ID
			// 
			this.textBox_dynu__ID.Location = new System.Drawing.Point(43, 6);
			this.textBox_dynu__ID.Name = "textBox_dynu__ID";
			this.textBox_dynu__ID.ReadOnly = true;
			this.textBox_dynu__ID.Size = new System.Drawing.Size(140, 21);
			this.textBox_dynu__ID.TabIndex = 3;
			this.textBox_dynu__ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox_dynu__TTL
			// 
			this.textBox_dynu__TTL.Location = new System.Drawing.Point(131, 33);
			this.textBox_dynu__TTL.Name = "textBox_dynu__TTL";
			this.textBox_dynu__TTL.Size = new System.Drawing.Size(52, 21);
			this.textBox_dynu__TTL.TabIndex = 3;
			this.textBox_dynu__TTL.Text = "600";
			this.textBox_dynu__TTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label_dynu__ID
			// 
			this.label_dynu__ID.AutoSize = true;
			this.label_dynu__ID.Location = new System.Drawing.Point(6, 9);
			this.label_dynu__ID.Name = "label_dynu__ID";
			this.label_dynu__ID.Size = new System.Drawing.Size(29, 12);
			this.label_dynu__ID.TabIndex = 2;
			this.label_dynu__ID.Text = "ID：";
			// 
			// label_dynu__TTL
			// 
			this.label_dynu__TTL.AutoSize = true;
			this.label_dynu__TTL.Location = new System.Drawing.Point(6, 36);
			this.label_dynu__TTL.Name = "label_dynu__TTL";
			this.label_dynu__TTL.Size = new System.Drawing.Size(119, 12);
			this.label_dynu__TTL.TabIndex = 2;
			this.label_dynu__TTL.Text = "TTL（秒，可省略）：";
			// 
			// label_Security_Profile
			// 
			this.label_Security_Profile.AutoSize = true;
			this.label_Security_Profile.Location = new System.Drawing.Point(12, 157);
			this.label_Security_Profile.Name = "label_Security_Profile";
			this.label_Security_Profile.Size = new System.Drawing.Size(65, 12);
			this.label_Security_Profile.TabIndex = 7;
			this.label_Security_Profile.Text = "安全配置：";
			// 
			// comboBox_Security_Profile
			// 
			this.comboBox_Security_Profile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Security_Profile.FormattingEnabled = true;
			this.comboBox_Security_Profile.Location = new System.Drawing.Point(83, 154);
			this.comboBox_Security_Profile.Name = "comboBox_Security_Profile";
			this.comboBox_Security_Profile.Size = new System.Drawing.Size(126, 20);
			this.comboBox_Security_Profile.TabIndex = 2;
			this.comboBox_Security_Profile.SelectedIndexChanged += new System.EventHandler(this.comboBox_Security_Profile_SelectedIndexChanged);
			// 
			// checkBox_IPv4_Enable
			// 
			this.checkBox_IPv4_Enable.AutoSize = true;
			this.checkBox_IPv4_Enable.Location = new System.Drawing.Point(12, 180);
			this.checkBox_IPv4_Enable.Name = "checkBox_IPv4_Enable";
			this.checkBox_IPv4_Enable.Size = new System.Drawing.Size(102, 16);
			this.checkBox_IPv4_Enable.TabIndex = 8;
			this.checkBox_IPv4_Enable.Text = "是否更新 IPv4";
			this.checkBox_IPv4_Enable.UseVisualStyleBackColor = true;
			// 
			// checkBox_IPv6_Enable
			// 
			this.checkBox_IPv6_Enable.AutoSize = true;
			this.checkBox_IPv6_Enable.Location = new System.Drawing.Point(12, 202);
			this.checkBox_IPv6_Enable.Name = "checkBox_IPv6_Enable";
			this.checkBox_IPv6_Enable.Size = new System.Drawing.Size(102, 16);
			this.checkBox_IPv6_Enable.TabIndex = 8;
			this.checkBox_IPv6_Enable.Text = "是否更新 IPv6";
			this.checkBox_IPv6_Enable.UseVisualStyleBackColor = true;
			// 
			// frm_Domain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(221, 259);
			this.Controls.Add(this.checkBox_IPv6_Enable);
			this.Controls.Add(this.checkBox_IPv4_Enable);
			this.Controls.Add(this.label_Security_Profile);
			this.Controls.Add(this.comboBox_Security_Profile);
			this.Controls.Add(this.comboBox_Type);
			this.Controls.Add(this.textBox_Domain);
			this.Controls.Add(this.tabControl_Type);
			this.Controls.Add(this.label_Type);
			this.Controls.Add(this.button_OK);
			this.Controls.Add(this.label_Domain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frm_Domain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "添加域名";
			this.Load += new System.EventHandler(this.frm_Domain_Load);
			this.tabControl_Type.ResumeLayout(false);
			this.tabPage_Godaddy.ResumeLayout(false);
			this.tabPage_Godaddy.PerformLayout();
			this.tabPage_dynv6.ResumeLayout(false);
			this.tabPage_dynv6.PerformLayout();
			this.tabPage_dynu.ResumeLayout(false);
			this.tabPage_dynu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_Domain;
		private System.Windows.Forms.TextBox textBox_Domain;
		private System.Windows.Forms.Label label_Godaddy__TTL;
		private System.Windows.Forms.TextBox textBox_Godaddy__TTL;
		private System.Windows.Forms.ComboBox comboBox_Type;
		private System.Windows.Forms.Label label_Type;
		private System.Windows.Forms.CheckBox checkBox_dynv6__Auto_IPv6;
		private System.Windows.Forms.CheckBox checkBox_dynv6__Auto_IPv4;
		private System.Windows.Forms.Button button_OK;
		private System.Windows.Forms.TabControl tabControl_Type;
		private System.Windows.Forms.TabPage tabPage_Godaddy;
		private System.Windows.Forms.TabPage tabPage_dynv6;
		private System.Windows.Forms.Label label_Security_Profile;
		private System.Windows.Forms.ComboBox comboBox_Security_Profile;
		private System.Windows.Forms.TabPage tabPage_dynu;
		private System.Windows.Forms.TextBox textBox_dynu__TTL;
		private System.Windows.Forms.Label label_dynu__TTL;
		private System.Windows.Forms.TextBox textBox_dynu__ID;
		private System.Windows.Forms.Label label_dynu__ID;
		private System.Windows.Forms.CheckBox checkBox_IPv4_Enable;
		private System.Windows.Forms.CheckBox checkBox_IPv6_Enable;
	}
}