namespace ddns_tool
{
	partial class frm_MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.notifyIcon_Main = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip_NotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl_Security__Property = new System.Windows.Forms.TabControl();
			this.tabPage_Security__Godaddy = new System.Windows.Forms.TabPage();
			this.linkLabel_Security_Godaddy__API = new System.Windows.Forms.LinkLabel();
			this.textBox_Security_Godaddy__Key = new System.Windows.Forms.TextBox();
			this.textBox_Security_Godaddy__Secret = new System.Windows.Forms.TextBox();
			this.checkBox_Security_Godaddy__Secret = new System.Windows.Forms.CheckBox();
			this.checkBox_Security_Godaddy__Key = new System.Windows.Forms.CheckBox();
			this.label_Security_Godaddy__Secret = new System.Windows.Forms.Label();
			this.label_Security_Godaddy__Key = new System.Windows.Forms.Label();
			this.tabPage_Security__dynv6 = new System.Windows.Forms.TabPage();
			this.checkBox_Security_dynv6__token = new System.Windows.Forms.CheckBox();
			this.textBox_Security_dynv6__token = new System.Windows.Forms.TextBox();
			this.label_Security_dynv6__token = new System.Windows.Forms.Label();
			this.linkLabel_Security_dynv6__API = new System.Windows.Forms.LinkLabel();
			this.tabPage_Security__dynu = new System.Windows.Forms.TabPage();
			this.checkBox_Security_dynu__API_Key = new System.Windows.Forms.CheckBox();
			this.textBox_Security_dynu__API_Key = new System.Windows.Forms.TextBox();
			this.label_Security_dynu__API_Key = new System.Windows.Forms.Label();
			this.linkLabel_Security_dynu__API = new System.Windows.Forms.LinkLabel();
			this.groupBox_Action_IP_Change_PlaySound = new System.Windows.Forms.GroupBox();
			this.checkBox_Action_IP_Change_PlaySound = new System.Windows.Forms.CheckBox();
			this.textBox_Action_IP_Change_PlaySound = new System.Windows.Forms.TextBox();
			this.button_Action_IP_Change_StopSound = new System.Windows.Forms.Button();
			this.button_Action_IP_Change_PlaySound = new System.Windows.Forms.Button();
			this.groupBox_Action_Set_DNS_Server = new System.Windows.Forms.GroupBox();
			this.textBox_Action_Custom_DNS_List = new System.Windows.Forms.TextBox();
			this.checkBox_Action_Use_Custom_DNS = new System.Windows.Forms.CheckBox();
			this.checkBox_Action_IP_Change_Popup = new System.Windows.Forms.CheckBox();
			this.checkBox_Action_DNS_Lookup_First = new System.Windows.Forms.CheckBox();
			this.checkBox_Action_UpdateIP = new System.Windows.Forms.CheckBox();
			this.textBox_Settings_IPv6 = new System.Windows.Forms.TextBox();
			this.textBox_Settings_IPv4 = new System.Windows.Forms.TextBox();
			this.radioButton_Settings_IPv6__Manual = new System.Windows.Forms.RadioButton();
			this.comboBox_Settings_IPv6__From_URL = new System.Windows.Forms.ComboBox();
			this.comboBox_Settings_IPv4__From_URL = new System.Windows.Forms.ComboBox();
			this.radioButton_Settings_IPv6__From_URL = new System.Windows.Forms.RadioButton();
			this.radioButton_Settings_IPv6__Accept_IP = new System.Windows.Forms.RadioButton();
			this.groupBox_Settings_RemoteServer = new System.Windows.Forms.GroupBox();
			this.checkBox_Settings_RemoteServer__Ping = new System.Windows.Forms.CheckBox();
			this.checkBox_Settings_RemoteServer__Pwd = new System.Windows.Forms.CheckBox();
			this.label_Settings_RemoteServer__User = new System.Windows.Forms.Label();
			this.label_Settings_RemoteServer__Ping = new System.Windows.Forms.Label();
			this.label_Settings_RemoteServer__Pwd = new System.Windows.Forms.Label();
			this.label_Settings_RemoteServer__Addr = new System.Windows.Forms.Label();
			this.textBox_Settings_RemoteServer__User = new System.Windows.Forms.TextBox();
			this.textBox_Settings_RemoteServer__Ping = new System.Windows.Forms.TextBox();
			this.textBox_Settings_RemoteServer__Pwd = new System.Windows.Forms.TextBox();
			this.textBox_Settings_RemoteServer__Addr = new System.Windows.Forms.TextBox();
			this.radioButton_Settings_Type__Remote = new System.Windows.Forms.RadioButton();
			this.radioButton_Settings_Type__Local = new System.Windows.Forms.RadioButton();
			this.checkBox_Security__Save_To_Config = new System.Windows.Forms.CheckBox();
			this.groupBox_Domains = new System.Windows.Forms.GroupBox();
			this.toolStrip_Domains = new System.Windows.Forms.ToolStrip();
			this.toolStripButton_Domains_Add = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton_Domains_Modify = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton_Domains_Delete = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton_Domains_IPv4_Enable = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton_Domains_IPv6_Enable = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton_Domains_IPv4_Disable = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton_Domains_IPv6_Disable = new System.Windows.Forms.ToolStripButton();
			this.listView_Domains = new System.Windows.Forms.ListView();
			this.columnHeader_Domains_Domain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Domains_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Domains_Profile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Domains_IPv4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Domains_IPv6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Domains_Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip_Domains = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItem_Domains_Add = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Domains_Modify = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Domains_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Domains_IPv4_Enable = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Domains_IPv6_Enable = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Domains_IPv4_Disable = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Domains_IPv6_Disable = new System.Windows.Forms.ToolStripMenuItem();
			this.listView_Logs = new System.Windows.Forms.ListView();
			this.columnHeader_Logs_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader_Logs_Log = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip_Logs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItem_Logs_Copy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Logs_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem_Logs_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.button_Update = new System.Windows.Forms.Button();
			this.label_Logs_MaxLines = new System.Windows.Forms.Label();
			this.numericUpDown_Logs_MaxLines = new System.Windows.Forms.NumericUpDown();
			this.groupBox_Logs = new System.Windows.Forms.GroupBox();
			this.checkBox_Logs__Save_To_File = new System.Windows.Forms.CheckBox();
			this.linkLabel_WebSite = new System.Windows.Forms.LinkLabel();
			this.linkLabel_Github = new System.Windows.Forms.LinkLabel();
			this.checkBox_Action_AutoAction_Interval = new System.Windows.Forms.CheckBox();
			this.numericUpDown_Action_AutoAction_Interval = new System.Windows.Forms.NumericUpDown();
			this.timer_Save_Config = new System.Windows.Forms.Timer(this.components);
			this.timer_Update = new System.Windows.Forms.Timer(this.components);
			this.timer_Ping = new System.Windows.Forms.Timer(this.components);
			this.splitContainer_Main = new System.Windows.Forms.SplitContainer();
			this.tabControl_Main = new System.Windows.Forms.TabControl();
			this.tabPage_Update_Type = new System.Windows.Forms.TabPage();
			this.tabPage_Set_IP = new System.Windows.Forms.TabPage();
			this.groupBox_Set_IPv6 = new System.Windows.Forms.GroupBox();
			this.groupBox_Set_IPv4 = new System.Windows.Forms.GroupBox();
			this.radioButton_Settings_IPv4__From_URL = new System.Windows.Forms.RadioButton();
			this.radioButton_Settings_IPv4__Manual = new System.Windows.Forms.RadioButton();
			this.radioButton_Settings_IPv4__Accept_IP = new System.Windows.Forms.RadioButton();
			this.tabPage_Security = new System.Windows.Forms.TabPage();
			this.listView_Security = new System.Windows.Forms.ListView();
			this.columnHeader_Security = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip_Security = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItem_Security_Add = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem__Security_Del = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox_Security__Property = new System.Windows.Forms.GroupBox();
			this.textBox_Security__Property__Name = new System.Windows.Forms.TextBox();
			this.label_Security__Property__Name = new System.Windows.Forms.Label();
			this.button_Security_Del = new System.Windows.Forms.Button();
			this.button_Security_Add = new System.Windows.Forms.Button();
			this.tabPage_Update_Action = new System.Windows.Forms.TabPage();
			this.label_Action_Timeout = new System.Windows.Forms.Label();
			this.numericUpDown_Action_Timeout = new System.Windows.Forms.NumericUpDown();
			this.tabPage_Fix_hosts = new System.Windows.Forms.TabPage();
			this.button_Fix_hosts__Path_Browser = new System.Windows.Forms.Button();
			this.textBox_Fix_hosts__Content = new System.Windows.Forms.TextBox();
			this.textBox_Fix_hosts__Path = new System.Windows.Forms.TextBox();
			this.label_Fix_hosts__Content = new System.Windows.Forms.Label();
			this.label_Fix_hosts__Path = new System.Windows.Forms.Label();
			this.groupBox_Settings_Preview = new System.Windows.Forms.GroupBox();
			this.label_Settings_Preview__Action_AutoUpdate_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Timeout_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__DNS_Server_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__DNS_Lookup_First_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Action_UpdateIP_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Security_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Set_IPv6_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Set_IPv4_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Ping_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Update_Type_Val = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Action_AutoUpdate = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Timeout = new System.Windows.Forms.Label();
			this.label_Settings_Preview__DNS_Server = new System.Windows.Forms.Label();
			this.label_Settings_Preview__DNS_Lookup_First = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Action_UpdateIP = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Security = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Set_IPv6 = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Ping = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Set_IPv4 = new System.Windows.Forms.Label();
			this.label_Settings_Preview__Update_Type = new System.Windows.Forms.Label();
			this.contextMenuStrip_NotifyIcon.SuspendLayout();
			this.tabControl_Security__Property.SuspendLayout();
			this.tabPage_Security__Godaddy.SuspendLayout();
			this.tabPage_Security__dynv6.SuspendLayout();
			this.tabPage_Security__dynu.SuspendLayout();
			this.groupBox_Action_IP_Change_PlaySound.SuspendLayout();
			this.groupBox_Action_Set_DNS_Server.SuspendLayout();
			this.groupBox_Settings_RemoteServer.SuspendLayout();
			this.groupBox_Domains.SuspendLayout();
			this.toolStrip_Domains.SuspendLayout();
			this.contextMenuStrip_Domains.SuspendLayout();
			this.contextMenuStrip_Logs.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Logs_MaxLines)).BeginInit();
			this.groupBox_Logs.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Action_AutoAction_Interval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).BeginInit();
			this.splitContainer_Main.Panel1.SuspendLayout();
			this.splitContainer_Main.Panel2.SuspendLayout();
			this.splitContainer_Main.SuspendLayout();
			this.tabControl_Main.SuspendLayout();
			this.tabPage_Update_Type.SuspendLayout();
			this.tabPage_Set_IP.SuspendLayout();
			this.groupBox_Set_IPv6.SuspendLayout();
			this.groupBox_Set_IPv4.SuspendLayout();
			this.tabPage_Security.SuspendLayout();
			this.contextMenuStrip_Security.SuspendLayout();
			this.groupBox_Security__Property.SuspendLayout();
			this.tabPage_Update_Action.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Action_Timeout)).BeginInit();
			this.tabPage_Fix_hosts.SuspendLayout();
			this.groupBox_Settings_Preview.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon_Main
			// 
			this.notifyIcon_Main.ContextMenuStrip = this.contextMenuStrip_NotifyIcon;
			this.notifyIcon_Main.Visible = true;
			this.notifyIcon_Main.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_Main_MouseDoubleClick);
			// 
			// contextMenuStrip_NotifyIcon
			// 
			this.contextMenuStrip_NotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Open,
            this.ToolStripMenuItem_Exit});
			this.contextMenuStrip_NotifyIcon.Name = "contextMenuStrip_NotifyIcon";
			this.contextMenuStrip_NotifyIcon.Size = new System.Drawing.Size(101, 48);
			// 
			// ToolStripMenuItem_Open
			// 
			this.ToolStripMenuItem_Open.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
			this.ToolStripMenuItem_Open.Name = "ToolStripMenuItem_Open";
			this.ToolStripMenuItem_Open.Size = new System.Drawing.Size(100, 22);
			this.ToolStripMenuItem_Open.Text = "打开";
			this.ToolStripMenuItem_Open.Click += new System.EventHandler(this.ToolStripMenuItem_Open_Click);
			// 
			// ToolStripMenuItem_Exit
			// 
			this.ToolStripMenuItem_Exit.Image = global::ddns_tool.res_Main.Exit;
			this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
			this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(100, 22);
			this.ToolStripMenuItem_Exit.Text = "退出";
			this.ToolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
			// 
			// tabControl_Security__Property
			// 
			this.tabControl_Security__Property.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl_Security__Property.Controls.Add(this.tabPage_Security__Godaddy);
			this.tabControl_Security__Property.Controls.Add(this.tabPage_Security__dynv6);
			this.tabControl_Security__Property.Controls.Add(this.tabPage_Security__dynu);
			this.tabControl_Security__Property.Location = new System.Drawing.Point(6, 47);
			this.tabControl_Security__Property.Name = "tabControl_Security__Property";
			this.tabControl_Security__Property.SelectedIndex = 0;
			this.tabControl_Security__Property.Size = new System.Drawing.Size(353, 95);
			this.tabControl_Security__Property.TabIndex = 0;
			// 
			// tabPage_Security__Godaddy
			// 
			this.tabPage_Security__Godaddy.Controls.Add(this.linkLabel_Security_Godaddy__API);
			this.tabPage_Security__Godaddy.Controls.Add(this.textBox_Security_Godaddy__Key);
			this.tabPage_Security__Godaddy.Controls.Add(this.textBox_Security_Godaddy__Secret);
			this.tabPage_Security__Godaddy.Controls.Add(this.checkBox_Security_Godaddy__Secret);
			this.tabPage_Security__Godaddy.Controls.Add(this.checkBox_Security_Godaddy__Key);
			this.tabPage_Security__Godaddy.Controls.Add(this.label_Security_Godaddy__Secret);
			this.tabPage_Security__Godaddy.Controls.Add(this.label_Security_Godaddy__Key);
			this.tabPage_Security__Godaddy.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Security__Godaddy.Name = "tabPage_Security__Godaddy";
			this.tabPage_Security__Godaddy.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_Security__Godaddy.Size = new System.Drawing.Size(345, 69);
			this.tabPage_Security__Godaddy.TabIndex = 1;
			this.tabPage_Security__Godaddy.Text = "Godaddy";
			this.tabPage_Security__Godaddy.UseVisualStyleBackColor = true;
			// 
			// linkLabel_Security_Godaddy__API
			// 
			this.linkLabel_Security_Godaddy__API.AutoSize = true;
			this.linkLabel_Security_Godaddy__API.Location = new System.Drawing.Point(8, 3);
			this.linkLabel_Security_Godaddy__API.Name = "linkLabel_Security_Godaddy__API";
			this.linkLabel_Security_Godaddy__API.Size = new System.Drawing.Size(185, 12);
			this.linkLabel_Security_Godaddy__API.TabIndex = 66;
			this.linkLabel_Security_Godaddy__API.TabStop = true;
			this.linkLabel_Security_Godaddy__API.Text = "https://developer.godaddy.com/";
			this.linkLabel_Security_Godaddy__API.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Security__LinkClicked);
			// 
			// textBox_Security_Godaddy__Key
			// 
			this.textBox_Security_Godaddy__Key.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Security_Godaddy__Key.Location = new System.Drawing.Point(73, 18);
			this.textBox_Security_Godaddy__Key.Name = "textBox_Security_Godaddy__Key";
			this.textBox_Security_Godaddy__Key.PasswordChar = '*';
			this.textBox_Security_Godaddy__Key.Size = new System.Drawing.Size(212, 21);
			this.textBox_Security_Godaddy__Key.TabIndex = 42;
			this.textBox_Security_Godaddy__Key.TextChanged += new System.EventHandler(this.textBox_Security_Godaddy__Key_TextChanged);
			// 
			// textBox_Security_Godaddy__Secret
			// 
			this.textBox_Security_Godaddy__Secret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Security_Godaddy__Secret.Location = new System.Drawing.Point(73, 45);
			this.textBox_Security_Godaddy__Secret.Name = "textBox_Security_Godaddy__Secret";
			this.textBox_Security_Godaddy__Secret.PasswordChar = '*';
			this.textBox_Security_Godaddy__Secret.Size = new System.Drawing.Size(212, 21);
			this.textBox_Security_Godaddy__Secret.TabIndex = 45;
			this.textBox_Security_Godaddy__Secret.TextChanged += new System.EventHandler(this.textBox_Security_Godaddy__Secret_TextChanged);
			// 
			// checkBox_Security_Godaddy__Secret
			// 
			this.checkBox_Security_Godaddy__Secret.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_Security_Godaddy__Secret.AutoSize = true;
			this.checkBox_Security_Godaddy__Secret.Location = new System.Drawing.Point(291, 47);
			this.checkBox_Security_Godaddy__Secret.Name = "checkBox_Security_Godaddy__Secret";
			this.checkBox_Security_Godaddy__Secret.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Security_Godaddy__Secret.TabIndex = 46;
			this.checkBox_Security_Godaddy__Secret.Text = "显示";
			this.checkBox_Security_Godaddy__Secret.UseVisualStyleBackColor = true;
			this.checkBox_Security_Godaddy__Secret.CheckedChanged += new System.EventHandler(this.checkBox_Security_Godaddy__Secret_CheckedChanged);
			// 
			// checkBox_Security_Godaddy__Key
			// 
			this.checkBox_Security_Godaddy__Key.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_Security_Godaddy__Key.AutoSize = true;
			this.checkBox_Security_Godaddy__Key.Location = new System.Drawing.Point(291, 20);
			this.checkBox_Security_Godaddy__Key.Name = "checkBox_Security_Godaddy__Key";
			this.checkBox_Security_Godaddy__Key.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Security_Godaddy__Key.TabIndex = 43;
			this.checkBox_Security_Godaddy__Key.Text = "显示";
			this.checkBox_Security_Godaddy__Key.UseVisualStyleBackColor = true;
			this.checkBox_Security_Godaddy__Key.CheckedChanged += new System.EventHandler(this.checkBox_Security_Godaddy__Key_CheckedChanged);
			// 
			// label_Security_Godaddy__Secret
			// 
			this.label_Security_Godaddy__Secret.AutoSize = true;
			this.label_Security_Godaddy__Secret.Location = new System.Drawing.Point(8, 48);
			this.label_Security_Godaddy__Secret.Name = "label_Security_Godaddy__Secret";
			this.label_Security_Godaddy__Secret.Size = new System.Drawing.Size(53, 12);
			this.label_Security_Godaddy__Secret.TabIndex = 44;
			this.label_Security_Godaddy__Secret.Text = "Secret：";
			// 
			// label_Security_Godaddy__Key
			// 
			this.label_Security_Godaddy__Key.AutoSize = true;
			this.label_Security_Godaddy__Key.Location = new System.Drawing.Point(8, 21);
			this.label_Security_Godaddy__Key.Name = "label_Security_Godaddy__Key";
			this.label_Security_Godaddy__Key.Size = new System.Drawing.Size(35, 12);
			this.label_Security_Godaddy__Key.TabIndex = 41;
			this.label_Security_Godaddy__Key.Text = "Key：";
			// 
			// tabPage_Security__dynv6
			// 
			this.tabPage_Security__dynv6.Controls.Add(this.checkBox_Security_dynv6__token);
			this.tabPage_Security__dynv6.Controls.Add(this.textBox_Security_dynv6__token);
			this.tabPage_Security__dynv6.Controls.Add(this.label_Security_dynv6__token);
			this.tabPage_Security__dynv6.Controls.Add(this.linkLabel_Security_dynv6__API);
			this.tabPage_Security__dynv6.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Security__dynv6.Name = "tabPage_Security__dynv6";
			this.tabPage_Security__dynv6.Size = new System.Drawing.Size(345, 69);
			this.tabPage_Security__dynv6.TabIndex = 2;
			this.tabPage_Security__dynv6.Text = "dynv6";
			this.tabPage_Security__dynv6.UseVisualStyleBackColor = true;
			// 
			// checkBox_Security_dynv6__token
			// 
			this.checkBox_Security_dynv6__token.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_Security_dynv6__token.AutoSize = true;
			this.checkBox_Security_dynv6__token.Location = new System.Drawing.Point(291, 20);
			this.checkBox_Security_dynv6__token.Name = "checkBox_Security_dynv6__token";
			this.checkBox_Security_dynv6__token.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Security_dynv6__token.TabIndex = 70;
			this.checkBox_Security_dynv6__token.Text = "显示";
			this.checkBox_Security_dynv6__token.UseVisualStyleBackColor = true;
			this.checkBox_Security_dynv6__token.CheckedChanged += new System.EventHandler(this.checkBox_Security_dynv6__token_CheckedChanged);
			// 
			// textBox_Security_dynv6__token
			// 
			this.textBox_Security_dynv6__token.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Security_dynv6__token.Location = new System.Drawing.Point(73, 18);
			this.textBox_Security_dynv6__token.Name = "textBox_Security_dynv6__token";
			this.textBox_Security_dynv6__token.PasswordChar = '*';
			this.textBox_Security_dynv6__token.Size = new System.Drawing.Size(212, 21);
			this.textBox_Security_dynv6__token.TabIndex = 1;
			this.textBox_Security_dynv6__token.TextChanged += new System.EventHandler(this.textBox_Security_dynv6__token_TextChanged);
			// 
			// label_Security_dynv6__token
			// 
			this.label_Security_dynv6__token.AutoSize = true;
			this.label_Security_dynv6__token.Location = new System.Drawing.Point(8, 21);
			this.label_Security_dynv6__token.Name = "label_Security_dynv6__token";
			this.label_Security_dynv6__token.Size = new System.Drawing.Size(47, 12);
			this.label_Security_dynv6__token.TabIndex = 0;
			this.label_Security_dynv6__token.Text = "token：";
			// 
			// linkLabel_Security_dynv6__API
			// 
			this.linkLabel_Security_dynv6__API.AutoSize = true;
			this.linkLabel_Security_dynv6__API.Location = new System.Drawing.Point(8, 3);
			this.linkLabel_Security_dynv6__API.Name = "linkLabel_Security_dynv6__API";
			this.linkLabel_Security_dynv6__API.Size = new System.Drawing.Size(167, 12);
			this.linkLabel_Security_dynv6__API.TabIndex = 68;
			this.linkLabel_Security_dynv6__API.TabStop = true;
			this.linkLabel_Security_dynv6__API.Text = "https://dynv6.com/docs/apis";
			this.linkLabel_Security_dynv6__API.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Security__LinkClicked);
			// 
			// tabPage_Security__dynu
			// 
			this.tabPage_Security__dynu.Controls.Add(this.checkBox_Security_dynu__API_Key);
			this.tabPage_Security__dynu.Controls.Add(this.textBox_Security_dynu__API_Key);
			this.tabPage_Security__dynu.Controls.Add(this.label_Security_dynu__API_Key);
			this.tabPage_Security__dynu.Controls.Add(this.linkLabel_Security_dynu__API);
			this.tabPage_Security__dynu.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Security__dynu.Name = "tabPage_Security__dynu";
			this.tabPage_Security__dynu.Size = new System.Drawing.Size(345, 69);
			this.tabPage_Security__dynu.TabIndex = 3;
			this.tabPage_Security__dynu.Text = "dynu";
			this.tabPage_Security__dynu.UseVisualStyleBackColor = true;
			// 
			// checkBox_Security_dynu__API_Key
			// 
			this.checkBox_Security_dynu__API_Key.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_Security_dynu__API_Key.AutoSize = true;
			this.checkBox_Security_dynu__API_Key.Location = new System.Drawing.Point(291, 20);
			this.checkBox_Security_dynu__API_Key.Name = "checkBox_Security_dynu__API_Key";
			this.checkBox_Security_dynu__API_Key.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Security_dynu__API_Key.TabIndex = 73;
			this.checkBox_Security_dynu__API_Key.Text = "显示";
			this.checkBox_Security_dynu__API_Key.UseVisualStyleBackColor = true;
			this.checkBox_Security_dynu__API_Key.CheckedChanged += new System.EventHandler(this.checkBox_Security_dynu__API_Key_CheckedChanged);
			// 
			// textBox_Security_dynu__API_Key
			// 
			this.textBox_Security_dynu__API_Key.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Security_dynu__API_Key.Location = new System.Drawing.Point(73, 18);
			this.textBox_Security_dynu__API_Key.Name = "textBox_Security_dynu__API_Key";
			this.textBox_Security_dynu__API_Key.PasswordChar = '*';
			this.textBox_Security_dynu__API_Key.Size = new System.Drawing.Size(212, 21);
			this.textBox_Security_dynu__API_Key.TabIndex = 72;
			this.textBox_Security_dynu__API_Key.TextChanged += new System.EventHandler(this.textBox_Security_dynu__API_Key_TextChanged);
			// 
			// label_Security_dynu__API_Key
			// 
			this.label_Security_dynu__API_Key.AutoSize = true;
			this.label_Security_dynu__API_Key.Location = new System.Drawing.Point(8, 21);
			this.label_Security_dynu__API_Key.Name = "label_Security_dynu__API_Key";
			this.label_Security_dynu__API_Key.Size = new System.Drawing.Size(59, 12);
			this.label_Security_dynu__API_Key.TabIndex = 71;
			this.label_Security_dynu__API_Key.Text = "API-Key：";
			// 
			// linkLabel_Security_dynu__API
			// 
			this.linkLabel_Security_dynu__API.AutoSize = true;
			this.linkLabel_Security_dynu__API.Location = new System.Drawing.Point(8, 3);
			this.linkLabel_Security_dynu__API.Name = "linkLabel_Security_dynu__API";
			this.linkLabel_Security_dynu__API.Size = new System.Drawing.Size(233, 12);
			this.linkLabel_Security_dynu__API.TabIndex = 69;
			this.linkLabel_Security_dynu__API.TabStop = true;
			this.linkLabel_Security_dynu__API.Text = "https://www.dynu.com/en-US/Support/API";
			this.linkLabel_Security_dynu__API.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Security__LinkClicked);
			// 
			// groupBox_Action_IP_Change_PlaySound
			// 
			this.groupBox_Action_IP_Change_PlaySound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox_Action_IP_Change_PlaySound.Controls.Add(this.checkBox_Action_IP_Change_PlaySound);
			this.groupBox_Action_IP_Change_PlaySound.Controls.Add(this.textBox_Action_IP_Change_PlaySound);
			this.groupBox_Action_IP_Change_PlaySound.Controls.Add(this.button_Action_IP_Change_StopSound);
			this.groupBox_Action_IP_Change_PlaySound.Controls.Add(this.button_Action_IP_Change_PlaySound);
			this.groupBox_Action_IP_Change_PlaySound.Location = new System.Drawing.Point(3, 231);
			this.groupBox_Action_IP_Change_PlaySound.Name = "groupBox_Action_IP_Change_PlaySound";
			this.groupBox_Action_IP_Change_PlaySound.Size = new System.Drawing.Size(523, 47);
			this.groupBox_Action_IP_Change_PlaySound.TabIndex = 66;
			this.groupBox_Action_IP_Change_PlaySound.TabStop = false;
			// 
			// checkBox_Action_IP_Change_PlaySound
			// 
			this.checkBox_Action_IP_Change_PlaySound.AutoSize = true;
			this.checkBox_Action_IP_Change_PlaySound.Location = new System.Drawing.Point(6, 0);
			this.checkBox_Action_IP_Change_PlaySound.Name = "checkBox_Action_IP_Change_PlaySound";
			this.checkBox_Action_IP_Change_PlaySound.Size = new System.Drawing.Size(132, 16);
			this.checkBox_Action_IP_Change_PlaySound.TabIndex = 1;
			this.checkBox_Action_IP_Change_PlaySound.Text = "IP变动时，播放音乐";
			this.checkBox_Action_IP_Change_PlaySound.UseVisualStyleBackColor = true;
			this.checkBox_Action_IP_Change_PlaySound.CheckedChanged += new System.EventHandler(this.checkBox_Action_IP_Change_PlaySound_CheckedChanged);
			// 
			// textBox_Action_IP_Change_PlaySound
			// 
			this.textBox_Action_IP_Change_PlaySound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Action_IP_Change_PlaySound.Location = new System.Drawing.Point(6, 20);
			this.textBox_Action_IP_Change_PlaySound.Name = "textBox_Action_IP_Change_PlaySound";
			this.textBox_Action_IP_Change_PlaySound.ReadOnly = true;
			this.textBox_Action_IP_Change_PlaySound.Size = new System.Drawing.Size(394, 21);
			this.textBox_Action_IP_Change_PlaySound.TabIndex = 2;
			this.textBox_Action_IP_Change_PlaySound.TextChanged += new System.EventHandler(this.textBox_Action_IP_Change_PlaySound_TextChanged);
			// 
			// button_Action_IP_Change_StopSound
			// 
			this.button_Action_IP_Change_StopSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_Action_IP_Change_StopSound.Location = new System.Drawing.Point(444, 18);
			this.button_Action_IP_Change_StopSound.Name = "button_Action_IP_Change_StopSound";
			this.button_Action_IP_Change_StopSound.Size = new System.Drawing.Size(73, 23);
			this.button_Action_IP_Change_StopSound.TabIndex = 3;
			this.button_Action_IP_Change_StopSound.Text = "停止播放";
			this.button_Action_IP_Change_StopSound.UseVisualStyleBackColor = true;
			this.button_Action_IP_Change_StopSound.Click += new System.EventHandler(this.button_Action_IP_Change_StopSound_Click);
			// 
			// button_Action_IP_Change_PlaySound
			// 
			this.button_Action_IP_Change_PlaySound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_Action_IP_Change_PlaySound.Location = new System.Drawing.Point(406, 18);
			this.button_Action_IP_Change_PlaySound.Name = "button_Action_IP_Change_PlaySound";
			this.button_Action_IP_Change_PlaySound.Size = new System.Drawing.Size(32, 23);
			this.button_Action_IP_Change_PlaySound.TabIndex = 3;
			this.button_Action_IP_Change_PlaySound.Text = "...";
			this.button_Action_IP_Change_PlaySound.UseVisualStyleBackColor = true;
			this.button_Action_IP_Change_PlaySound.Click += new System.EventHandler(this.button_Action_IP_Change_PlaySound_Click);
			// 
			// groupBox_Action_Set_DNS_Server
			// 
			this.groupBox_Action_Set_DNS_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox_Action_Set_DNS_Server.Controls.Add(this.textBox_Action_Custom_DNS_List);
			this.groupBox_Action_Set_DNS_Server.Controls.Add(this.checkBox_Action_Use_Custom_DNS);
			this.groupBox_Action_Set_DNS_Server.Location = new System.Drawing.Point(3, 73);
			this.groupBox_Action_Set_DNS_Server.Name = "groupBox_Action_Set_DNS_Server";
			this.groupBox_Action_Set_DNS_Server.Size = new System.Drawing.Size(523, 104);
			this.groupBox_Action_Set_DNS_Server.TabIndex = 65;
			this.groupBox_Action_Set_DNS_Server.TabStop = false;
			// 
			// textBox_Action_Custom_DNS_List
			// 
			this.textBox_Action_Custom_DNS_List.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Action_Custom_DNS_List.Location = new System.Drawing.Point(6, 20);
			this.textBox_Action_Custom_DNS_List.Multiline = true;
			this.textBox_Action_Custom_DNS_List.Name = "textBox_Action_Custom_DNS_List";
			this.textBox_Action_Custom_DNS_List.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_Action_Custom_DNS_List.Size = new System.Drawing.Size(511, 78);
			this.textBox_Action_Custom_DNS_List.TabIndex = 66;
			this.textBox_Action_Custom_DNS_List.Text = "8.8.8.8\r\n8.8.4.4\r\n114.114.114.114\r\n//2001:4860:4860::8888\r\n//2001:4860:4860::8844" +
    "";
			this.textBox_Action_Custom_DNS_List.TextChanged += new System.EventHandler(this.textBox_Action_Custom_DNS_List_TextChanged);
			// 
			// checkBox_Action_Use_Custom_DNS
			// 
			this.checkBox_Action_Use_Custom_DNS.AutoSize = true;
			this.checkBox_Action_Use_Custom_DNS.Checked = true;
			this.checkBox_Action_Use_Custom_DNS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Action_Use_Custom_DNS.Location = new System.Drawing.Point(6, 0);
			this.checkBox_Action_Use_Custom_DNS.Name = "checkBox_Action_Use_Custom_DNS";
			this.checkBox_Action_Use_Custom_DNS.Size = new System.Drawing.Size(402, 16);
			this.checkBox_Action_Use_Custom_DNS.TabIndex = 62;
			this.checkBox_Action_Use_Custom_DNS.Text = "设定解析域名的DNS服务器（一行一个。//表示注释。\"\"表示系统默认）";
			this.checkBox_Action_Use_Custom_DNS.UseVisualStyleBackColor = true;
			this.checkBox_Action_Use_Custom_DNS.CheckedChanged += new System.EventHandler(this.checkBox_Action_Use_Custom_DNS_CheckedChanged);
			// 
			// checkBox_Action_IP_Change_Popup
			// 
			this.checkBox_Action_IP_Change_Popup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBox_Action_IP_Change_Popup.AutoSize = true;
			this.checkBox_Action_IP_Change_Popup.Location = new System.Drawing.Point(3, 206);
			this.checkBox_Action_IP_Change_Popup.Name = "checkBox_Action_IP_Change_Popup";
			this.checkBox_Action_IP_Change_Popup.Size = new System.Drawing.Size(156, 16);
			this.checkBox_Action_IP_Change_Popup.TabIndex = 1;
			this.checkBox_Action_IP_Change_Popup.Text = "IP变动时，弹出提示窗口";
			this.checkBox_Action_IP_Change_Popup.UseVisualStyleBackColor = true;
			this.checkBox_Action_IP_Change_Popup.CheckedChanged += new System.EventHandler(this.checkBox_Action_IP_Change_Popup_CheckedChanged);
			// 
			// checkBox_Action_DNS_Lookup_First
			// 
			this.checkBox_Action_DNS_Lookup_First.AutoSize = true;
			this.checkBox_Action_DNS_Lookup_First.Checked = true;
			this.checkBox_Action_DNS_Lookup_First.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Action_DNS_Lookup_First.Location = new System.Drawing.Point(3, 51);
			this.checkBox_Action_DNS_Lookup_First.Name = "checkBox_Action_DNS_Lookup_First";
			this.checkBox_Action_DNS_Lookup_First.Size = new System.Drawing.Size(228, 16);
			this.checkBox_Action_DNS_Lookup_First.TabIndex = 62;
			this.checkBox_Action_DNS_Lookup_First.Text = "IP变动时，才执行更新（先解析域名）";
			this.checkBox_Action_DNS_Lookup_First.UseVisualStyleBackColor = true;
			this.checkBox_Action_DNS_Lookup_First.CheckedChanged += new System.EventHandler(this.checkBox_Action_DNS_Lookup_First_CheckedChanged);
			// 
			// checkBox_Action_UpdateIP
			// 
			this.checkBox_Action_UpdateIP.AutoSize = true;
			this.checkBox_Action_UpdateIP.Checked = true;
			this.checkBox_Action_UpdateIP.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Action_UpdateIP.Location = new System.Drawing.Point(3, 3);
			this.checkBox_Action_UpdateIP.Name = "checkBox_Action_UpdateIP";
			this.checkBox_Action_UpdateIP.Size = new System.Drawing.Size(96, 16);
			this.checkBox_Action_UpdateIP.TabIndex = 1;
			this.checkBox_Action_UpdateIP.Text = "更新域名的IP";
			this.checkBox_Action_UpdateIP.UseVisualStyleBackColor = true;
			this.checkBox_Action_UpdateIP.CheckedChanged += new System.EventHandler(this.checkBox_Action_UpdateIP_CheckedChanged);
			// 
			// textBox_Settings_IPv6
			// 
			this.textBox_Settings_IPv6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Settings_IPv6.Location = new System.Drawing.Point(173, 46);
			this.textBox_Settings_IPv6.Name = "textBox_Settings_IPv6";
			this.textBox_Settings_IPv6.ReadOnly = true;
			this.textBox_Settings_IPv6.Size = new System.Drawing.Size(338, 21);
			this.textBox_Settings_IPv6.TabIndex = 37;
			this.textBox_Settings_IPv6.TextChanged += new System.EventHandler(this.textBox_Settings_IPv6_TextChanged);
			// 
			// textBox_Settings_IPv4
			// 
			this.textBox_Settings_IPv4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Settings_IPv4.Location = new System.Drawing.Point(173, 46);
			this.textBox_Settings_IPv4.Name = "textBox_Settings_IPv4";
			this.textBox_Settings_IPv4.ReadOnly = true;
			this.textBox_Settings_IPv4.Size = new System.Drawing.Size(338, 21);
			this.textBox_Settings_IPv4.TabIndex = 37;
			this.textBox_Settings_IPv4.TextChanged += new System.EventHandler(this.textBox_Settings_IPv4_TextChanged);
			// 
			// radioButton_Settings_IPv6__Manual
			// 
			this.radioButton_Settings_IPv6__Manual.AutoSize = true;
			this.radioButton_Settings_IPv6__Manual.Location = new System.Drawing.Point(6, 47);
			this.radioButton_Settings_IPv6__Manual.Name = "radioButton_Settings_IPv6__Manual";
			this.radioButton_Settings_IPv6__Manual.Size = new System.Drawing.Size(101, 16);
			this.radioButton_Settings_IPv6__Manual.TabIndex = 32;
			this.radioButton_Settings_IPv6__Manual.Text = "手动指定 IPv6";
			this.radioButton_Settings_IPv6__Manual.UseVisualStyleBackColor = true;
			this.radioButton_Settings_IPv6__Manual.CheckedChanged += new System.EventHandler(this.radioButton_Settings_IPv6__CheckedChanged);
			// 
			// comboBox_Settings_IPv6__From_URL
			// 
			this.comboBox_Settings_IPv6__From_URL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_Settings_IPv6__From_URL.FormattingEnabled = true;
			this.comboBox_Settings_IPv6__From_URL.Location = new System.Drawing.Point(173, 20);
			this.comboBox_Settings_IPv6__From_URL.Name = "comboBox_Settings_IPv6__From_URL";
			this.comboBox_Settings_IPv6__From_URL.Size = new System.Drawing.Size(338, 20);
			this.comboBox_Settings_IPv6__From_URL.TabIndex = 35;
			this.comboBox_Settings_IPv6__From_URL.SelectedIndexChanged += new System.EventHandler(this.comboBox_Settings_IPv6__From_URL_SelectedIndexChanged);
			// 
			// comboBox_Settings_IPv4__From_URL
			// 
			this.comboBox_Settings_IPv4__From_URL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_Settings_IPv4__From_URL.FormattingEnabled = true;
			this.comboBox_Settings_IPv4__From_URL.Location = new System.Drawing.Point(173, 20);
			this.comboBox_Settings_IPv4__From_URL.Name = "comboBox_Settings_IPv4__From_URL";
			this.comboBox_Settings_IPv4__From_URL.Size = new System.Drawing.Size(338, 20);
			this.comboBox_Settings_IPv4__From_URL.TabIndex = 35;
			this.comboBox_Settings_IPv4__From_URL.SelectedIndexChanged += new System.EventHandler(this.comboBox_Settings_IPv4__From_URL_SelectedIndexChanged);
			// 
			// radioButton_Settings_IPv6__From_URL
			// 
			this.radioButton_Settings_IPv6__From_URL.AutoSize = true;
			this.radioButton_Settings_IPv6__From_URL.Checked = true;
			this.radioButton_Settings_IPv6__From_URL.Location = new System.Drawing.Point(6, 21);
			this.radioButton_Settings_IPv6__From_URL.Name = "radioButton_Settings_IPv6__From_URL";
			this.radioButton_Settings_IPv6__From_URL.Size = new System.Drawing.Size(161, 16);
			this.radioButton_Settings_IPv6__From_URL.TabIndex = 31;
			this.radioButton_Settings_IPv6__From_URL.TabStop = true;
			this.radioButton_Settings_IPv6__From_URL.Text = "通过互联网获取公网 IPv6";
			this.radioButton_Settings_IPv6__From_URL.UseVisualStyleBackColor = true;
			this.radioButton_Settings_IPv6__From_URL.CheckedChanged += new System.EventHandler(this.radioButton_Settings_IPv6__CheckedChanged);
			// 
			// radioButton_Settings_IPv6__Accept_IP
			// 
			this.radioButton_Settings_IPv6__Accept_IP.AutoSize = true;
			this.radioButton_Settings_IPv6__Accept_IP.Enabled = false;
			this.radioButton_Settings_IPv6__Accept_IP.Location = new System.Drawing.Point(6, 73);
			this.radioButton_Settings_IPv6__Accept_IP.Name = "radioButton_Settings_IPv6__Accept_IP";
			this.radioButton_Settings_IPv6__Accept_IP.Size = new System.Drawing.Size(191, 16);
			this.radioButton_Settings_IPv6__Accept_IP.TabIndex = 33;
			this.radioButton_Settings_IPv6__Accept_IP.Text = "Server 接受连接的客户端 IPv6";
			this.radioButton_Settings_IPv6__Accept_IP.UseVisualStyleBackColor = true;
			this.radioButton_Settings_IPv6__Accept_IP.CheckedChanged += new System.EventHandler(this.radioButton_Settings_IPv6__CheckedChanged);
			// 
			// groupBox_Settings_RemoteServer
			// 
			this.groupBox_Settings_RemoteServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox_Settings_RemoteServer.Controls.Add(this.checkBox_Settings_RemoteServer__Ping);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.checkBox_Settings_RemoteServer__Pwd);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.label_Settings_RemoteServer__User);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.label_Settings_RemoteServer__Ping);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.label_Settings_RemoteServer__Pwd);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.label_Settings_RemoteServer__Addr);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.textBox_Settings_RemoteServer__User);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.textBox_Settings_RemoteServer__Ping);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.textBox_Settings_RemoteServer__Pwd);
			this.groupBox_Settings_RemoteServer.Controls.Add(this.textBox_Settings_RemoteServer__Addr);
			this.groupBox_Settings_RemoteServer.Location = new System.Drawing.Point(6, 50);
			this.groupBox_Settings_RemoteServer.Name = "groupBox_Settings_RemoteServer";
			this.groupBox_Settings_RemoteServer.Size = new System.Drawing.Size(517, 128);
			this.groupBox_Settings_RemoteServer.TabIndex = 11;
			this.groupBox_Settings_RemoteServer.TabStop = false;
			this.groupBox_Settings_RemoteServer.Text = "远程 Server 设置";
			// 
			// checkBox_Settings_RemoteServer__Ping
			// 
			this.checkBox_Settings_RemoteServer__Ping.AutoSize = true;
			this.checkBox_Settings_RemoteServer__Ping.Checked = true;
			this.checkBox_Settings_RemoteServer__Ping.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Settings_RemoteServer__Ping.Location = new System.Drawing.Point(227, 103);
			this.checkBox_Settings_RemoteServer__Ping.Name = "checkBox_Settings_RemoteServer__Ping";
			this.checkBox_Settings_RemoteServer__Ping.Size = new System.Drawing.Size(120, 16);
			this.checkBox_Settings_RemoteServer__Ping.TabIndex = 20;
			this.checkBox_Settings_RemoteServer__Ping.Text = "自动 ping 服务器";
			this.checkBox_Settings_RemoteServer__Ping.UseVisualStyleBackColor = true;
			this.checkBox_Settings_RemoteServer__Ping.CheckedChanged += new System.EventHandler(this.checkBox_Settings_RemoteServer__Ping_CheckedChanged);
			// 
			// checkBox_Settings_RemoteServer__Pwd
			// 
			this.checkBox_Settings_RemoteServer__Pwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_Settings_RemoteServer__Pwd.AutoSize = true;
			this.checkBox_Settings_RemoteServer__Pwd.Location = new System.Drawing.Point(463, 76);
			this.checkBox_Settings_RemoteServer__Pwd.Name = "checkBox_Settings_RemoteServer__Pwd";
			this.checkBox_Settings_RemoteServer__Pwd.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Settings_RemoteServer__Pwd.TabIndex = 17;
			this.checkBox_Settings_RemoteServer__Pwd.Text = "显示";
			this.checkBox_Settings_RemoteServer__Pwd.UseVisualStyleBackColor = true;
			this.checkBox_Settings_RemoteServer__Pwd.CheckedChanged += new System.EventHandler(this.checkBox_Settings_RemoteServer__Pwd_CheckedChanged);
			// 
			// label_Settings_RemoteServer__User
			// 
			this.label_Settings_RemoteServer__User.AutoSize = true;
			this.label_Settings_RemoteServer__User.Location = new System.Drawing.Point(6, 50);
			this.label_Settings_RemoteServer__User.Name = "label_Settings_RemoteServer__User";
			this.label_Settings_RemoteServer__User.Size = new System.Drawing.Size(149, 12);
			this.label_Settings_RemoteServer__User.TabIndex = 13;
			this.label_Settings_RemoteServer__User.Text = "登录到 Server 的用户名：";
			// 
			// label_Settings_RemoteServer__Ping
			// 
			this.label_Settings_RemoteServer__Ping.AutoSize = true;
			this.label_Settings_RemoteServer__Ping.Location = new System.Drawing.Point(6, 104);
			this.label_Settings_RemoteServer__Ping.Name = "label_Settings_RemoteServer__Ping";
			this.label_Settings_RemoteServer__Ping.Size = new System.Drawing.Size(89, 12);
			this.label_Settings_RemoteServer__Ping.TabIndex = 18;
			this.label_Settings_RemoteServer__Ping.Text = "Ping 值 (ms)：";
			// 
			// label_Settings_RemoteServer__Pwd
			// 
			this.label_Settings_RemoteServer__Pwd.AutoSize = true;
			this.label_Settings_RemoteServer__Pwd.Location = new System.Drawing.Point(6, 77);
			this.label_Settings_RemoteServer__Pwd.Name = "label_Settings_RemoteServer__Pwd";
			this.label_Settings_RemoteServer__Pwd.Size = new System.Drawing.Size(137, 12);
			this.label_Settings_RemoteServer__Pwd.TabIndex = 15;
			this.label_Settings_RemoteServer__Pwd.Text = "登录到 Server 的密码：";
			// 
			// label_Settings_RemoteServer__Addr
			// 
			this.label_Settings_RemoteServer__Addr.AutoSize = true;
			this.label_Settings_RemoteServer__Addr.Location = new System.Drawing.Point(6, 23);
			this.label_Settings_RemoteServer__Addr.Name = "label_Settings_RemoteServer__Addr";
			this.label_Settings_RemoteServer__Addr.Size = new System.Drawing.Size(113, 12);
			this.label_Settings_RemoteServer__Addr.TabIndex = 11;
			this.label_Settings_RemoteServer__Addr.Text = "Server 地址/端口：";
			// 
			// textBox_Settings_RemoteServer__User
			// 
			this.textBox_Settings_RemoteServer__User.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Settings_RemoteServer__User.Location = new System.Drawing.Point(161, 47);
			this.textBox_Settings_RemoteServer__User.Name = "textBox_Settings_RemoteServer__User";
			this.textBox_Settings_RemoteServer__User.ReadOnly = true;
			this.textBox_Settings_RemoteServer__User.Size = new System.Drawing.Size(296, 21);
			this.textBox_Settings_RemoteServer__User.TabIndex = 14;
			this.textBox_Settings_RemoteServer__User.TextChanged += new System.EventHandler(this.textBox_Settings_RemoteServer__User_TextChanged);
			// 
			// textBox_Settings_RemoteServer__Ping
			// 
			this.textBox_Settings_RemoteServer__Ping.Location = new System.Drawing.Point(161, 101);
			this.textBox_Settings_RemoteServer__Ping.Name = "textBox_Settings_RemoteServer__Ping";
			this.textBox_Settings_RemoteServer__Ping.ReadOnly = true;
			this.textBox_Settings_RemoteServer__Ping.Size = new System.Drawing.Size(60, 21);
			this.textBox_Settings_RemoteServer__Ping.TabIndex = 19;
			// 
			// textBox_Settings_RemoteServer__Pwd
			// 
			this.textBox_Settings_RemoteServer__Pwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Settings_RemoteServer__Pwd.Location = new System.Drawing.Point(161, 74);
			this.textBox_Settings_RemoteServer__Pwd.Name = "textBox_Settings_RemoteServer__Pwd";
			this.textBox_Settings_RemoteServer__Pwd.PasswordChar = '*';
			this.textBox_Settings_RemoteServer__Pwd.ReadOnly = true;
			this.textBox_Settings_RemoteServer__Pwd.Size = new System.Drawing.Size(296, 21);
			this.textBox_Settings_RemoteServer__Pwd.TabIndex = 16;
			this.textBox_Settings_RemoteServer__Pwd.TextChanged += new System.EventHandler(this.textBox_Settings_RemoteServer__Pwd_TextChanged);
			// 
			// textBox_Settings_RemoteServer__Addr
			// 
			this.textBox_Settings_RemoteServer__Addr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Settings_RemoteServer__Addr.Location = new System.Drawing.Point(161, 20);
			this.textBox_Settings_RemoteServer__Addr.Name = "textBox_Settings_RemoteServer__Addr";
			this.textBox_Settings_RemoteServer__Addr.ReadOnly = true;
			this.textBox_Settings_RemoteServer__Addr.Size = new System.Drawing.Size(350, 21);
			this.textBox_Settings_RemoteServer__Addr.TabIndex = 12;
			this.textBox_Settings_RemoteServer__Addr.Text = "127.0.0.1:3333";
			this.textBox_Settings_RemoteServer__Addr.TextChanged += new System.EventHandler(this.textBox_Settings_RemoteServer__Addr_TextChanged);
			// 
			// radioButton_Settings_Type__Remote
			// 
			this.radioButton_Settings_Type__Remote.AutoSize = true;
			this.radioButton_Settings_Type__Remote.Location = new System.Drawing.Point(6, 28);
			this.radioButton_Settings_Type__Remote.Name = "radioButton_Settings_Type__Remote";
			this.radioButton_Settings_Type__Remote.Size = new System.Drawing.Size(227, 16);
			this.radioButton_Settings_Type__Remote.TabIndex = 4;
			this.radioButton_Settings_Type__Remote.Text = "远程更新（由远程 Server 执行更新）";
			this.radioButton_Settings_Type__Remote.UseVisualStyleBackColor = true;
			this.radioButton_Settings_Type__Remote.CheckedChanged += new System.EventHandler(this.radioButton_Settings_Type__CheckedChanged);
			// 
			// radioButton_Settings_Type__Local
			// 
			this.radioButton_Settings_Type__Local.AutoSize = true;
			this.radioButton_Settings_Type__Local.Checked = true;
			this.radioButton_Settings_Type__Local.Location = new System.Drawing.Point(6, 6);
			this.radioButton_Settings_Type__Local.Name = "radioButton_Settings_Type__Local";
			this.radioButton_Settings_Type__Local.Size = new System.Drawing.Size(119, 16);
			this.radioButton_Settings_Type__Local.TabIndex = 3;
			this.radioButton_Settings_Type__Local.TabStop = true;
			this.radioButton_Settings_Type__Local.Text = "本地更新（直连）";
			this.radioButton_Settings_Type__Local.UseVisualStyleBackColor = true;
			this.radioButton_Settings_Type__Local.CheckedChanged += new System.EventHandler(this.radioButton_Settings_Type__CheckedChanged);
			// 
			// checkBox_Security__Save_To_Config
			// 
			this.checkBox_Security__Save_To_Config.AutoSize = true;
			this.checkBox_Security__Save_To_Config.Checked = true;
			this.checkBox_Security__Save_To_Config.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Security__Save_To_Config.Location = new System.Drawing.Point(6, 148);
			this.checkBox_Security__Save_To_Config.Name = "checkBox_Security__Save_To_Config";
			this.checkBox_Security__Save_To_Config.Size = new System.Drawing.Size(132, 16);
			this.checkBox_Security__Save_To_Config.TabIndex = 47;
			this.checkBox_Security__Save_To_Config.Text = "保存到 Config 文件";
			this.checkBox_Security__Save_To_Config.UseVisualStyleBackColor = true;
			this.checkBox_Security__Save_To_Config.CheckedChanged += new System.EventHandler(this.checkBox_Security__Save_To_Config_CheckedChanged);
			// 
			// groupBox_Domains
			// 
			this.groupBox_Domains.BackColor = System.Drawing.Color.White;
			this.groupBox_Domains.Controls.Add(this.toolStrip_Domains);
			this.groupBox_Domains.Controls.Add(this.listView_Domains);
			this.groupBox_Domains.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox_Domains.Location = new System.Drawing.Point(0, 0);
			this.groupBox_Domains.Name = "groupBox_Domains";
			this.groupBox_Domains.Size = new System.Drawing.Size(910, 162);
			this.groupBox_Domains.TabIndex = 51;
			this.groupBox_Domains.TabStop = false;
			this.groupBox_Domains.Text = "【域名列表】";
			// 
			// toolStrip_Domains
			// 
			this.toolStrip_Domains.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Domains_Add,
            this.toolStripButton_Domains_Modify,
            this.toolStripButton_Domains_Delete,
            this.toolStripButton_Domains_IPv4_Enable,
            this.toolStripButton_Domains_IPv6_Enable,
            this.toolStripButton_Domains_IPv4_Disable,
            this.toolStripButton_Domains_IPv6_Disable});
			this.toolStrip_Domains.Location = new System.Drawing.Point(3, 17);
			this.toolStrip_Domains.Name = "toolStrip_Domains";
			this.toolStrip_Domains.Size = new System.Drawing.Size(904, 25);
			this.toolStrip_Domains.TabIndex = 52;
			this.toolStrip_Domains.Text = "域名列表";
			// 
			// toolStripButton_Domains_Add
			// 
			this.toolStripButton_Domains_Add.Image = global::ddns_tool.res_Main.Add;
			this.toolStripButton_Domains_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton_Domains_Add.Name = "toolStripButton_Domains_Add";
			this.toolStripButton_Domains_Add.Size = new System.Drawing.Size(52, 22);
			this.toolStripButton_Domains_Add.Text = "添加";
			this.toolStripButton_Domains_Add.Click += new System.EventHandler(this.toolStripButton_Domains_Add_Click);
			// 
			// toolStripButton_Domains_Modify
			// 
			this.toolStripButton_Domains_Modify.Enabled = false;
			this.toolStripButton_Domains_Modify.Image = global::ddns_tool.res_Main.Edit;
			this.toolStripButton_Domains_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton_Domains_Modify.Name = "toolStripButton_Domains_Modify";
			this.toolStripButton_Domains_Modify.Size = new System.Drawing.Size(52, 22);
			this.toolStripButton_Domains_Modify.Text = "修改";
			this.toolStripButton_Domains_Modify.Click += new System.EventHandler(this.toolStripButton_Domains_Modify_Click);
			// 
			// toolStripButton_Domains_Delete
			// 
			this.toolStripButton_Domains_Delete.Enabled = false;
			this.toolStripButton_Domains_Delete.Image = global::ddns_tool.res_Main.Delete;
			this.toolStripButton_Domains_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton_Domains_Delete.Name = "toolStripButton_Domains_Delete";
			this.toolStripButton_Domains_Delete.Size = new System.Drawing.Size(52, 22);
			this.toolStripButton_Domains_Delete.Text = "删除";
			this.toolStripButton_Domains_Delete.Click += new System.EventHandler(this.toolStripButton_Domains_Delete_Click);
			// 
			// toolStripButton_Domains_IPv4_Enable
			// 
			this.toolStripButton_Domains_IPv4_Enable.Enabled = false;
			this.toolStripButton_Domains_IPv4_Enable.Image = global::ddns_tool.res_Main.On;
			this.toolStripButton_Domains_IPv4_Enable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton_Domains_IPv4_Enable.Name = "toolStripButton_Domains_IPv4_Enable";
			this.toolStripButton_Domains_IPv4_Enable.Size = new System.Drawing.Size(104, 22);
			this.toolStripButton_Domains_IPv4_Enable.Text = "允许更新 IPv4";
			this.toolStripButton_Domains_IPv4_Enable.Click += new System.EventHandler(this.toolStripButton_Domains_IPv4_Enable_Click);
			// 
			// toolStripButton_Domains_IPv6_Enable
			// 
			this.toolStripButton_Domains_IPv6_Enable.Enabled = false;
			this.toolStripButton_Domains_IPv6_Enable.Image = global::ddns_tool.res_Main.On;
			this.toolStripButton_Domains_IPv6_Enable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton_Domains_IPv6_Enable.Name = "toolStripButton_Domains_IPv6_Enable";
			this.toolStripButton_Domains_IPv6_Enable.Size = new System.Drawing.Size(104, 22);
			this.toolStripButton_Domains_IPv6_Enable.Text = "允许更新 IPv6";
			this.toolStripButton_Domains_IPv6_Enable.Click += new System.EventHandler(this.toolStripButton_Domains_IPv6_Enable_Click);
			// 
			// toolStripButton_Domains_IPv4_Disable
			// 
			this.toolStripButton_Domains_IPv4_Disable.Enabled = false;
			this.toolStripButton_Domains_IPv4_Disable.Image = global::ddns_tool.res_Main.Off;
			this.toolStripButton_Domains_IPv4_Disable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton_Domains_IPv4_Disable.Name = "toolStripButton_Domains_IPv4_Disable";
			this.toolStripButton_Domains_IPv4_Disable.Size = new System.Drawing.Size(104, 22);
			this.toolStripButton_Domains_IPv4_Disable.Text = "禁止更新 IPv4";
			this.toolStripButton_Domains_IPv4_Disable.Click += new System.EventHandler(this.toolStripButton_Domains_IPv4_Disable_Click);
			// 
			// toolStripButton_Domains_IPv6_Disable
			// 
			this.toolStripButton_Domains_IPv6_Disable.Enabled = false;
			this.toolStripButton_Domains_IPv6_Disable.Image = global::ddns_tool.res_Main.Off;
			this.toolStripButton_Domains_IPv6_Disable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton_Domains_IPv6_Disable.Name = "toolStripButton_Domains_IPv6_Disable";
			this.toolStripButton_Domains_IPv6_Disable.Size = new System.Drawing.Size(104, 22);
			this.toolStripButton_Domains_IPv6_Disable.Text = "禁止更新 IPv6";
			this.toolStripButton_Domains_IPv6_Disable.Click += new System.EventHandler(this.toolStripButton_Domains_IPv6_Disable_Click);
			// 
			// listView_Domains
			// 
			this.listView_Domains.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView_Domains.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Domains_Domain,
            this.columnHeader_Domains_Type,
            this.columnHeader_Domains_Profile,
            this.columnHeader_Domains_IPv4,
            this.columnHeader_Domains_IPv6,
            this.columnHeader_Domains_Status});
			this.listView_Domains.ContextMenuStrip = this.contextMenuStrip_Domains;
			this.listView_Domains.FullRowSelect = true;
			this.listView_Domains.GridLines = true;
			this.listView_Domains.HideSelection = false;
			this.listView_Domains.Location = new System.Drawing.Point(3, 45);
			this.listView_Domains.Name = "listView_Domains";
			this.listView_Domains.Size = new System.Drawing.Size(904, 111);
			this.listView_Domains.TabIndex = 51;
			this.listView_Domains.UseCompatibleStateImageBehavior = false;
			this.listView_Domains.View = System.Windows.Forms.View.Details;
			this.listView_Domains.SelectedIndexChanged += new System.EventHandler(this.listView_Domains_SelectedIndexChanged);
			this.listView_Domains.DoubleClick += new System.EventHandler(this.listView_Domains_DoubleClick);
			this.listView_Domains.Resize += new System.EventHandler(this.listView_Domains_Resize);
			// 
			// columnHeader_Domains_Domain
			// 
			this.columnHeader_Domains_Domain.Text = "域名";
			this.columnHeader_Domains_Domain.Width = 184;
			// 
			// columnHeader_Domains_Type
			// 
			this.columnHeader_Domains_Type.Text = "类型";
			this.columnHeader_Domains_Type.Width = 198;
			// 
			// columnHeader_Domains_Profile
			// 
			this.columnHeader_Domains_Profile.Text = "安全配置";
			// 
			// columnHeader_Domains_IPv4
			// 
			this.columnHeader_Domains_IPv4.Text = "最新IPv4";
			this.columnHeader_Domains_IPv4.Width = 102;
			// 
			// columnHeader_Domains_IPv6
			// 
			this.columnHeader_Domains_IPv6.Text = "最新IPv6";
			this.columnHeader_Domains_IPv6.Width = 273;
			// 
			// columnHeader_Domains_Status
			// 
			this.columnHeader_Domains_Status.Text = "状态";
			this.columnHeader_Domains_Status.Width = 66;
			// 
			// contextMenuStrip_Domains
			// 
			this.contextMenuStrip_Domains.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Domains_Add,
            this.ToolStripMenuItem_Domains_Modify,
            this.ToolStripMenuItem_Domains_Delete,
            this.ToolStripMenuItem_Domains_IPv4_Enable,
            this.ToolStripMenuItem_Domains_IPv6_Enable,
            this.ToolStripMenuItem_Domains_IPv4_Disable,
            this.ToolStripMenuItem_Domains_IPv6_Disable});
			this.contextMenuStrip_Domains.Name = "contextMenuStrip_Domains";
			this.contextMenuStrip_Domains.Size = new System.Drawing.Size(153, 158);
			// 
			// ToolStripMenuItem_Domains_Add
			// 
			this.ToolStripMenuItem_Domains_Add.Image = global::ddns_tool.res_Main.Add;
			this.ToolStripMenuItem_Domains_Add.Name = "ToolStripMenuItem_Domains_Add";
			this.ToolStripMenuItem_Domains_Add.Size = new System.Drawing.Size(152, 22);
			this.ToolStripMenuItem_Domains_Add.Text = "添加";
			this.ToolStripMenuItem_Domains_Add.Click += new System.EventHandler(this.ToolStripMenuItem_Domains_Add_Click);
			// 
			// ToolStripMenuItem_Domains_Modify
			// 
			this.ToolStripMenuItem_Domains_Modify.Enabled = false;
			this.ToolStripMenuItem_Domains_Modify.Image = global::ddns_tool.res_Main.Edit;
			this.ToolStripMenuItem_Domains_Modify.Name = "ToolStripMenuItem_Domains_Modify";
			this.ToolStripMenuItem_Domains_Modify.Size = new System.Drawing.Size(152, 22);
			this.ToolStripMenuItem_Domains_Modify.Text = "修改";
			this.ToolStripMenuItem_Domains_Modify.Click += new System.EventHandler(this.ToolStripMenuItem_Domains_Modify_Click);
			// 
			// ToolStripMenuItem_Domains_Delete
			// 
			this.ToolStripMenuItem_Domains_Delete.Enabled = false;
			this.ToolStripMenuItem_Domains_Delete.Image = global::ddns_tool.res_Main.Delete;
			this.ToolStripMenuItem_Domains_Delete.Name = "ToolStripMenuItem_Domains_Delete";
			this.ToolStripMenuItem_Domains_Delete.Size = new System.Drawing.Size(152, 22);
			this.ToolStripMenuItem_Domains_Delete.Text = "删除";
			this.ToolStripMenuItem_Domains_Delete.Click += new System.EventHandler(this.ToolStripMenuItem_Domains_Delete_Click);
			// 
			// ToolStripMenuItem_Domains_IPv4_Enable
			// 
			this.ToolStripMenuItem_Domains_IPv4_Enable.Enabled = false;
			this.ToolStripMenuItem_Domains_IPv4_Enable.Image = global::ddns_tool.res_Main.On;
			this.ToolStripMenuItem_Domains_IPv4_Enable.Name = "ToolStripMenuItem_Domains_IPv4_Enable";
			this.ToolStripMenuItem_Domains_IPv4_Enable.Size = new System.Drawing.Size(152, 22);
			this.ToolStripMenuItem_Domains_IPv4_Enable.Text = "允许更新 IPv4";
			this.ToolStripMenuItem_Domains_IPv4_Enable.Click += new System.EventHandler(this.ToolStripMenuItem_Domains_IPv4_Enable_Click);
			// 
			// ToolStripMenuItem_Domains_IPv6_Enable
			// 
			this.ToolStripMenuItem_Domains_IPv6_Enable.Enabled = false;
			this.ToolStripMenuItem_Domains_IPv6_Enable.Image = global::ddns_tool.res_Main.On;
			this.ToolStripMenuItem_Domains_IPv6_Enable.Name = "ToolStripMenuItem_Domains_IPv6_Enable";
			this.ToolStripMenuItem_Domains_IPv6_Enable.Size = new System.Drawing.Size(152, 22);
			this.ToolStripMenuItem_Domains_IPv6_Enable.Text = "允许更新 IPv6";
			this.ToolStripMenuItem_Domains_IPv6_Enable.Click += new System.EventHandler(this.ToolStripMenuItem_Domains_IPv6_Enable_Click);
			// 
			// ToolStripMenuItem_Domains_IPv4_Disable
			// 
			this.ToolStripMenuItem_Domains_IPv4_Disable.Enabled = false;
			this.ToolStripMenuItem_Domains_IPv4_Disable.Image = global::ddns_tool.res_Main.Off;
			this.ToolStripMenuItem_Domains_IPv4_Disable.Name = "ToolStripMenuItem_Domains_IPv4_Disable";
			this.ToolStripMenuItem_Domains_IPv4_Disable.Size = new System.Drawing.Size(152, 22);
			this.ToolStripMenuItem_Domains_IPv4_Disable.Text = "禁止更新 IPv4";
			this.ToolStripMenuItem_Domains_IPv4_Disable.Click += new System.EventHandler(this.ToolStripMenuItem_Domains_IPv4_Disable_Click);
			// 
			// ToolStripMenuItem_Domains_IPv6_Disable
			// 
			this.ToolStripMenuItem_Domains_IPv6_Disable.Enabled = false;
			this.ToolStripMenuItem_Domains_IPv6_Disable.Image = global::ddns_tool.res_Main.Off;
			this.ToolStripMenuItem_Domains_IPv6_Disable.Name = "ToolStripMenuItem_Domains_IPv6_Disable";
			this.ToolStripMenuItem_Domains_IPv6_Disable.Size = new System.Drawing.Size(152, 22);
			this.ToolStripMenuItem_Domains_IPv6_Disable.Text = "禁止更新 IPv6";
			this.ToolStripMenuItem_Domains_IPv6_Disable.Click += new System.EventHandler(this.ToolStripMenuItem_Domains_IPv6_Disable_Click);
			// 
			// listView_Logs
			// 
			this.listView_Logs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView_Logs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Logs_Time,
            this.columnHeader_Logs_Log});
			this.listView_Logs.ContextMenuStrip = this.contextMenuStrip_Logs;
			this.listView_Logs.FullRowSelect = true;
			this.listView_Logs.GridLines = true;
			this.listView_Logs.HideSelection = false;
			this.listView_Logs.Location = new System.Drawing.Point(6, 20);
			this.listView_Logs.Name = "listView_Logs";
			this.listView_Logs.Size = new System.Drawing.Size(898, 225);
			this.listView_Logs.TabIndex = 67;
			this.listView_Logs.UseCompatibleStateImageBehavior = false;
			this.listView_Logs.View = System.Windows.Forms.View.Details;
			this.listView_Logs.SelectedIndexChanged += new System.EventHandler(this.listView_Logs_SelectedIndexChanged);
			this.listView_Logs.SizeChanged += new System.EventHandler(this.listView_Logs_SizeChanged);
			this.listView_Logs.Resize += new System.EventHandler(this.listView_Logs_Resize);
			// 
			// columnHeader_Logs_Time
			// 
			this.columnHeader_Logs_Time.Text = "时间";
			this.columnHeader_Logs_Time.Width = 122;
			// 
			// columnHeader_Logs_Log
			// 
			this.columnHeader_Logs_Log.Text = "日志";
			this.columnHeader_Logs_Log.Width = 755;
			// 
			// contextMenuStrip_Logs
			// 
			this.contextMenuStrip_Logs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Logs_Copy,
            this.ToolStripMenuItem_Logs_Delete,
            this.toolStripMenuItem1,
            this.ToolStripMenuItem_Logs_SelectAll});
			this.contextMenuStrip_Logs.Name = "contextMenuStrip_Logs";
			this.contextMenuStrip_Logs.Size = new System.Drawing.Size(194, 76);
			// 
			// ToolStripMenuItem_Logs_Copy
			// 
			this.ToolStripMenuItem_Logs_Copy.Enabled = false;
			this.ToolStripMenuItem_Logs_Copy.Image = global::ddns_tool.res_Main.Copy;
			this.ToolStripMenuItem_Logs_Copy.Name = "ToolStripMenuItem_Logs_Copy";
			this.ToolStripMenuItem_Logs_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.ToolStripMenuItem_Logs_Copy.Size = new System.Drawing.Size(193, 22);
			this.ToolStripMenuItem_Logs_Copy.Text = "复制文本";
			this.ToolStripMenuItem_Logs_Copy.Click += new System.EventHandler(this.ToolStripMenuItem_Logs_Copy_Click);
			// 
			// ToolStripMenuItem_Logs_Delete
			// 
			this.ToolStripMenuItem_Logs_Delete.Enabled = false;
			this.ToolStripMenuItem_Logs_Delete.Image = global::ddns_tool.res_Main.Delete;
			this.ToolStripMenuItem_Logs_Delete.Name = "ToolStripMenuItem_Logs_Delete";
			this.ToolStripMenuItem_Logs_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ToolStripMenuItem_Logs_Delete.Size = new System.Drawing.Size(193, 22);
			this.ToolStripMenuItem_Logs_Delete.Text = "删除选定记录";
			this.ToolStripMenuItem_Logs_Delete.Click += new System.EventHandler(this.ToolStripMenuItem_Logs_Delete_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
			// 
			// ToolStripMenuItem_Logs_SelectAll
			// 
			this.ToolStripMenuItem_Logs_SelectAll.Name = "ToolStripMenuItem_Logs_SelectAll";
			this.ToolStripMenuItem_Logs_SelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.ToolStripMenuItem_Logs_SelectAll.Size = new System.Drawing.Size(193, 22);
			this.ToolStripMenuItem_Logs_SelectAll.Text = "全选";
			this.ToolStripMenuItem_Logs_SelectAll.Click += new System.EventHandler(this.ToolStripMenuItem_Logs_SelectAll_Click);
			// 
			// button_Update
			// 
			this.button_Update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button_Update.Location = new System.Drawing.Point(822, 746);
			this.button_Update.Name = "button_Update";
			this.button_Update.Size = new System.Drawing.Size(100, 23);
			this.button_Update.TabIndex = 68;
			this.button_Update.Text = "执行更新操作";
			this.button_Update.UseVisualStyleBackColor = true;
			this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
			// 
			// label_Logs_MaxLines
			// 
			this.label_Logs_MaxLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label_Logs_MaxLines.AutoSize = true;
			this.label_Logs_MaxLines.Location = new System.Drawing.Point(6, 253);
			this.label_Logs_MaxLines.Name = "label_Logs_MaxLines";
			this.label_Logs_MaxLines.Size = new System.Drawing.Size(89, 12);
			this.label_Logs_MaxLines.TabIndex = 69;
			this.label_Logs_MaxLines.Text = "日志最大行数：";
			// 
			// numericUpDown_Logs_MaxLines
			// 
			this.numericUpDown_Logs_MaxLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.numericUpDown_Logs_MaxLines.Location = new System.Drawing.Point(101, 251);
			this.numericUpDown_Logs_MaxLines.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.numericUpDown_Logs_MaxLines.Name = "numericUpDown_Logs_MaxLines";
			this.numericUpDown_Logs_MaxLines.Size = new System.Drawing.Size(67, 21);
			this.numericUpDown_Logs_MaxLines.TabIndex = 70;
			this.numericUpDown_Logs_MaxLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_Logs_MaxLines.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDown_Logs_MaxLines.ValueChanged += new System.EventHandler(this.numericUpDown_Logs_MaxLines_ValueChanged);
			// 
			// groupBox_Logs
			// 
			this.groupBox_Logs.BackColor = System.Drawing.Color.Gainsboro;
			this.groupBox_Logs.Controls.Add(this.checkBox_Logs__Save_To_File);
			this.groupBox_Logs.Controls.Add(this.listView_Logs);
			this.groupBox_Logs.Controls.Add(this.numericUpDown_Logs_MaxLines);
			this.groupBox_Logs.Controls.Add(this.label_Logs_MaxLines);
			this.groupBox_Logs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox_Logs.Location = new System.Drawing.Point(0, 0);
			this.groupBox_Logs.Name = "groupBox_Logs";
			this.groupBox_Logs.Size = new System.Drawing.Size(910, 278);
			this.groupBox_Logs.TabIndex = 73;
			this.groupBox_Logs.TabStop = false;
			this.groupBox_Logs.Text = "【日志记录】";
			// 
			// checkBox_Logs__Save_To_File
			// 
			this.checkBox_Logs__Save_To_File.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_Logs__Save_To_File.AutoSize = true;
			this.checkBox_Logs__Save_To_File.Checked = true;
			this.checkBox_Logs__Save_To_File.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Logs__Save_To_File.Location = new System.Drawing.Point(796, 252);
			this.checkBox_Logs__Save_To_File.Name = "checkBox_Logs__Save_To_File";
			this.checkBox_Logs__Save_To_File.Size = new System.Drawing.Size(108, 16);
			this.checkBox_Logs__Save_To_File.TabIndex = 76;
			this.checkBox_Logs__Save_To_File.Text = "保存到日志文件";
			this.checkBox_Logs__Save_To_File.UseVisualStyleBackColor = true;
			this.checkBox_Logs__Save_To_File.CheckedChanged += new System.EventHandler(this.checkBox_Logs__Save_To_File_CheckedChanged);
			// 
			// linkLabel_WebSite
			// 
			this.linkLabel_WebSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabel_WebSite.AutoSize = true;
			this.linkLabel_WebSite.Location = new System.Drawing.Point(555, 757);
			this.linkLabel_WebSite.Name = "linkLabel_WebSite";
			this.linkLabel_WebSite.Size = new System.Drawing.Size(29, 12);
			this.linkLabel_WebSite.TabIndex = 74;
			this.linkLabel_WebSite.TabStop = true;
			this.linkLabel_WebSite.Text = "官网";
			this.linkLabel_WebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_WebSite_LinkClicked);
			// 
			// linkLabel_Github
			// 
			this.linkLabel_Github.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkLabel_Github.AutoSize = true;
			this.linkLabel_Github.Location = new System.Drawing.Point(590, 757);
			this.linkLabel_Github.Name = "linkLabel_Github";
			this.linkLabel_Github.Size = new System.Drawing.Size(41, 12);
			this.linkLabel_Github.TabIndex = 75;
			this.linkLabel_Github.TabStop = true;
			this.linkLabel_Github.Text = "github";
			this.linkLabel_Github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Github_LinkClicked);
			// 
			// checkBox_Action_AutoAction_Interval
			// 
			this.checkBox_Action_AutoAction_Interval.AutoSize = true;
			this.checkBox_Action_AutoAction_Interval.Checked = true;
			this.checkBox_Action_AutoAction_Interval.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_Action_AutoAction_Interval.Location = new System.Drawing.Point(3, 25);
			this.checkBox_Action_AutoAction_Interval.Name = "checkBox_Action_AutoAction_Interval";
			this.checkBox_Action_AutoAction_Interval.Size = new System.Drawing.Size(204, 16);
			this.checkBox_Action_AutoAction_Interval.TabIndex = 76;
			this.checkBox_Action_AutoAction_Interval.Text = "自动执行操作的时间间隔（秒）：";
			this.checkBox_Action_AutoAction_Interval.UseVisualStyleBackColor = true;
			this.checkBox_Action_AutoAction_Interval.CheckedChanged += new System.EventHandler(this.checkBox_AutoAction_CheckedChanged);
			// 
			// numericUpDown_Action_AutoAction_Interval
			// 
			this.numericUpDown_Action_AutoAction_Interval.Location = new System.Drawing.Point(213, 24);
			this.numericUpDown_Action_AutoAction_Interval.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numericUpDown_Action_AutoAction_Interval.Name = "numericUpDown_Action_AutoAction_Interval";
			this.numericUpDown_Action_AutoAction_Interval.Size = new System.Drawing.Size(60, 21);
			this.numericUpDown_Action_AutoAction_Interval.TabIndex = 77;
			this.numericUpDown_Action_AutoAction_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_Action_AutoAction_Interval.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
			this.numericUpDown_Action_AutoAction_Interval.ValueChanged += new System.EventHandler(this.numericUpDown_AutoUpdate_Interval_ValueChanged);
			// 
			// timer_Save_Config
			// 
			this.timer_Save_Config.Enabled = true;
			this.timer_Save_Config.Interval = 5000;
			this.timer_Save_Config.Tick += new System.EventHandler(this.timer_Save_Config_Tick);
			// 
			// timer_Update
			// 
			this.timer_Update.Enabled = true;
			this.timer_Update.Interval = 1000;
			this.timer_Update.Tick += new System.EventHandler(this.timer_Update_Tick);
			// 
			// timer_Ping
			// 
			this.timer_Ping.Enabled = true;
			this.timer_Ping.Interval = 1000;
			this.timer_Ping.Tick += new System.EventHandler(this.timer_Ping_Tick);
			// 
			// splitContainer_Main
			// 
			this.splitContainer_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer_Main.Location = new System.Drawing.Point(12, 12);
			this.splitContainer_Main.Name = "splitContainer_Main";
			this.splitContainer_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer_Main.Panel1
			// 
			this.splitContainer_Main.Panel1.Controls.Add(this.groupBox_Domains);
			// 
			// splitContainer_Main.Panel2
			// 
			this.splitContainer_Main.Panel2.Controls.Add(this.groupBox_Logs);
			this.splitContainer_Main.Size = new System.Drawing.Size(910, 444);
			this.splitContainer_Main.SplitterDistance = 162;
			this.splitContainer_Main.TabIndex = 78;
			// 
			// tabControl_Main
			// 
			this.tabControl_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tabControl_Main.Controls.Add(this.tabPage_Update_Type);
			this.tabControl_Main.Controls.Add(this.tabPage_Set_IP);
			this.tabControl_Main.Controls.Add(this.tabPage_Security);
			this.tabControl_Main.Controls.Add(this.tabPage_Update_Action);
			this.tabControl_Main.Controls.Add(this.tabPage_Fix_hosts);
			this.tabControl_Main.Location = new System.Drawing.Point(12, 462);
			this.tabControl_Main.Name = "tabControl_Main";
			this.tabControl_Main.SelectedIndex = 0;
			this.tabControl_Main.Size = new System.Drawing.Size(537, 307);
			this.tabControl_Main.TabIndex = 80;
			// 
			// tabPage_Update_Type
			// 
			this.tabPage_Update_Type.Controls.Add(this.groupBox_Settings_RemoteServer);
			this.tabPage_Update_Type.Controls.Add(this.radioButton_Settings_Type__Local);
			this.tabPage_Update_Type.Controls.Add(this.radioButton_Settings_Type__Remote);
			this.tabPage_Update_Type.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Update_Type.Name = "tabPage_Update_Type";
			this.tabPage_Update_Type.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_Update_Type.Size = new System.Drawing.Size(529, 281);
			this.tabPage_Update_Type.TabIndex = 0;
			this.tabPage_Update_Type.Text = "更新方式";
			this.tabPage_Update_Type.UseVisualStyleBackColor = true;
			// 
			// tabPage_Set_IP
			// 
			this.tabPage_Set_IP.Controls.Add(this.groupBox_Set_IPv6);
			this.tabPage_Set_IP.Controls.Add(this.groupBox_Set_IPv4);
			this.tabPage_Set_IP.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Set_IP.Name = "tabPage_Set_IP";
			this.tabPage_Set_IP.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_Set_IP.Size = new System.Drawing.Size(529, 281);
			this.tabPage_Set_IP.TabIndex = 1;
			this.tabPage_Set_IP.Text = "设置 IP";
			this.tabPage_Set_IP.UseVisualStyleBackColor = true;
			// 
			// groupBox_Set_IPv6
			// 
			this.groupBox_Set_IPv6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox_Set_IPv6.Controls.Add(this.radioButton_Settings_IPv6__From_URL);
			this.groupBox_Set_IPv6.Controls.Add(this.textBox_Settings_IPv6);
			this.groupBox_Set_IPv6.Controls.Add(this.radioButton_Settings_IPv6__Accept_IP);
			this.groupBox_Set_IPv6.Controls.Add(this.comboBox_Settings_IPv6__From_URL);
			this.groupBox_Set_IPv6.Controls.Add(this.radioButton_Settings_IPv6__Manual);
			this.groupBox_Set_IPv6.Location = new System.Drawing.Point(6, 107);
			this.groupBox_Set_IPv6.Name = "groupBox_Set_IPv6";
			this.groupBox_Set_IPv6.Size = new System.Drawing.Size(517, 95);
			this.groupBox_Set_IPv6.TabIndex = 41;
			this.groupBox_Set_IPv6.TabStop = false;
			this.groupBox_Set_IPv6.Text = "IPv6";
			// 
			// groupBox_Set_IPv4
			// 
			this.groupBox_Set_IPv4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox_Set_IPv4.Controls.Add(this.radioButton_Settings_IPv4__From_URL);
			this.groupBox_Set_IPv4.Controls.Add(this.radioButton_Settings_IPv4__Manual);
			this.groupBox_Set_IPv4.Controls.Add(this.textBox_Settings_IPv4);
			this.groupBox_Set_IPv4.Controls.Add(this.comboBox_Settings_IPv4__From_URL);
			this.groupBox_Set_IPv4.Controls.Add(this.radioButton_Settings_IPv4__Accept_IP);
			this.groupBox_Set_IPv4.Location = new System.Drawing.Point(6, 6);
			this.groupBox_Set_IPv4.Name = "groupBox_Set_IPv4";
			this.groupBox_Set_IPv4.Size = new System.Drawing.Size(517, 95);
			this.groupBox_Set_IPv4.TabIndex = 40;
			this.groupBox_Set_IPv4.TabStop = false;
			this.groupBox_Set_IPv4.Text = "IPv4";
			// 
			// radioButton_Settings_IPv4__From_URL
			// 
			this.radioButton_Settings_IPv4__From_URL.AutoSize = true;
			this.radioButton_Settings_IPv4__From_URL.Checked = true;
			this.radioButton_Settings_IPv4__From_URL.Location = new System.Drawing.Point(6, 21);
			this.radioButton_Settings_IPv4__From_URL.Name = "radioButton_Settings_IPv4__From_URL";
			this.radioButton_Settings_IPv4__From_URL.Size = new System.Drawing.Size(161, 16);
			this.radioButton_Settings_IPv4__From_URL.TabIndex = 31;
			this.radioButton_Settings_IPv4__From_URL.TabStop = true;
			this.radioButton_Settings_IPv4__From_URL.Text = "通过互联网获取公网 IPv4";
			this.radioButton_Settings_IPv4__From_URL.UseVisualStyleBackColor = true;
			this.radioButton_Settings_IPv4__From_URL.CheckedChanged += new System.EventHandler(this.radioButton_Settings_IPv4__CheckedChanged);
			// 
			// radioButton_Settings_IPv4__Manual
			// 
			this.radioButton_Settings_IPv4__Manual.AutoSize = true;
			this.radioButton_Settings_IPv4__Manual.Location = new System.Drawing.Point(6, 47);
			this.radioButton_Settings_IPv4__Manual.Name = "radioButton_Settings_IPv4__Manual";
			this.radioButton_Settings_IPv4__Manual.Size = new System.Drawing.Size(101, 16);
			this.radioButton_Settings_IPv4__Manual.TabIndex = 32;
			this.radioButton_Settings_IPv4__Manual.Text = "手动指定 IPv4";
			this.radioButton_Settings_IPv4__Manual.UseVisualStyleBackColor = true;
			this.radioButton_Settings_IPv4__Manual.CheckedChanged += new System.EventHandler(this.radioButton_Settings_IPv4__CheckedChanged);
			// 
			// radioButton_Settings_IPv4__Accept_IP
			// 
			this.radioButton_Settings_IPv4__Accept_IP.AutoSize = true;
			this.radioButton_Settings_IPv4__Accept_IP.Enabled = false;
			this.radioButton_Settings_IPv4__Accept_IP.Location = new System.Drawing.Point(6, 73);
			this.radioButton_Settings_IPv4__Accept_IP.Name = "radioButton_Settings_IPv4__Accept_IP";
			this.radioButton_Settings_IPv4__Accept_IP.Size = new System.Drawing.Size(191, 16);
			this.radioButton_Settings_IPv4__Accept_IP.TabIndex = 33;
			this.radioButton_Settings_IPv4__Accept_IP.Text = "Server 接受连接的客户端 IPv4";
			this.radioButton_Settings_IPv4__Accept_IP.UseVisualStyleBackColor = true;
			this.radioButton_Settings_IPv4__Accept_IP.CheckedChanged += new System.EventHandler(this.radioButton_Settings_IPv4__CheckedChanged);
			// 
			// tabPage_Security
			// 
			this.tabPage_Security.Controls.Add(this.listView_Security);
			this.tabPage_Security.Controls.Add(this.groupBox_Security__Property);
			this.tabPage_Security.Controls.Add(this.button_Security_Del);
			this.tabPage_Security.Controls.Add(this.button_Security_Add);
			this.tabPage_Security.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Security.Name = "tabPage_Security";
			this.tabPage_Security.Size = new System.Drawing.Size(529, 281);
			this.tabPage_Security.TabIndex = 2;
			this.tabPage_Security.Text = "安全设置";
			this.tabPage_Security.UseVisualStyleBackColor = true;
			// 
			// listView_Security
			// 
			this.listView_Security.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listView_Security.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Security});
			this.listView_Security.ContextMenuStrip = this.contextMenuStrip_Security;
			this.listView_Security.FullRowSelect = true;
			this.listView_Security.GridLines = true;
			this.listView_Security.HideSelection = false;
			this.listView_Security.Location = new System.Drawing.Point(3, 3);
			this.listView_Security.MultiSelect = false;
			this.listView_Security.Name = "listView_Security";
			this.listView_Security.Size = new System.Drawing.Size(152, 246);
			this.listView_Security.TabIndex = 3;
			this.listView_Security.UseCompatibleStateImageBehavior = false;
			this.listView_Security.View = System.Windows.Forms.View.Details;
			this.listView_Security.SelectedIndexChanged += new System.EventHandler(this.listView_Security_SelectedIndexChanged);
			this.listView_Security.Resize += new System.EventHandler(this.listView_Security_Resize);
			// 
			// columnHeader_Security
			// 
			this.columnHeader_Security.Text = "Profile";
			this.columnHeader_Security.Width = 131;
			// 
			// contextMenuStrip_Security
			// 
			this.contextMenuStrip_Security.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Security_Add,
            this.ToolStripMenuItem__Security_Del});
			this.contextMenuStrip_Security.Name = "contextMenuStrip_Security";
			this.contextMenuStrip_Security.Size = new System.Drawing.Size(146, 48);
			// 
			// ToolStripMenuItem_Security_Add
			// 
			this.ToolStripMenuItem_Security_Add.Image = global::ddns_tool.res_Main.Add;
			this.ToolStripMenuItem_Security_Add.Name = "ToolStripMenuItem_Security_Add";
			this.ToolStripMenuItem_Security_Add.Size = new System.Drawing.Size(145, 22);
			this.ToolStripMenuItem_Security_Add.Text = "添加";
			this.ToolStripMenuItem_Security_Add.Click += new System.EventHandler(this.ToolStripMenuItem_Security_Add_Click);
			// 
			// ToolStripMenuItem__Security_Del
			// 
			this.ToolStripMenuItem__Security_Del.Enabled = false;
			this.ToolStripMenuItem__Security_Del.Image = global::ddns_tool.res_Main.Delete;
			this.ToolStripMenuItem__Security_Del.Name = "ToolStripMenuItem__Security_Del";
			this.ToolStripMenuItem__Security_Del.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ToolStripMenuItem__Security_Del.Size = new System.Drawing.Size(145, 22);
			this.ToolStripMenuItem__Security_Del.Text = "删除";
			this.ToolStripMenuItem__Security_Del.Click += new System.EventHandler(this.ToolStripMenuItem__Security_Del_Click);
			// 
			// groupBox_Security__Property
			// 
			this.groupBox_Security__Property.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox_Security__Property.Controls.Add(this.checkBox_Security__Save_To_Config);
			this.groupBox_Security__Property.Controls.Add(this.tabControl_Security__Property);
			this.groupBox_Security__Property.Controls.Add(this.textBox_Security__Property__Name);
			this.groupBox_Security__Property.Controls.Add(this.label_Security__Property__Name);
			this.groupBox_Security__Property.Location = new System.Drawing.Point(161, 3);
			this.groupBox_Security__Property.Name = "groupBox_Security__Property";
			this.groupBox_Security__Property.Size = new System.Drawing.Size(365, 170);
			this.groupBox_Security__Property.TabIndex = 2;
			this.groupBox_Security__Property.TabStop = false;
			this.groupBox_Security__Property.Text = "属性";
			// 
			// textBox_Security__Property__Name
			// 
			this.textBox_Security__Property__Name.Location = new System.Drawing.Point(89, 20);
			this.textBox_Security__Property__Name.Name = "textBox_Security__Property__Name";
			this.textBox_Security__Property__Name.Size = new System.Drawing.Size(118, 21);
			this.textBox_Security__Property__Name.TabIndex = 1;
			this.textBox_Security__Property__Name.TextChanged += new System.EventHandler(this.textBox_Security__Property__Name_TextChanged);
			// 
			// label_Security__Property__Name
			// 
			this.label_Security__Property__Name.AutoSize = true;
			this.label_Security__Property__Name.Location = new System.Drawing.Point(6, 23);
			this.label_Security__Property__Name.Name = "label_Security__Property__Name";
			this.label_Security__Property__Name.Size = new System.Drawing.Size(77, 12);
			this.label_Security__Property__Name.TabIndex = 0;
			this.label_Security__Property__Name.Text = "配置的名称：";
			// 
			// button_Security_Del
			// 
			this.button_Security_Del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_Security_Del.Enabled = false;
			this.button_Security_Del.Location = new System.Drawing.Point(32, 255);
			this.button_Security_Del.Name = "button_Security_Del";
			this.button_Security_Del.Size = new System.Drawing.Size(23, 23);
			this.button_Security_Del.TabIndex = 1;
			this.button_Security_Del.Text = "-";
			this.button_Security_Del.UseVisualStyleBackColor = true;
			this.button_Security_Del.Click += new System.EventHandler(this.button_Security_Del_Click);
			// 
			// button_Security_Add
			// 
			this.button_Security_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_Security_Add.Location = new System.Drawing.Point(3, 255);
			this.button_Security_Add.Name = "button_Security_Add";
			this.button_Security_Add.Size = new System.Drawing.Size(23, 23);
			this.button_Security_Add.TabIndex = 1;
			this.button_Security_Add.Text = "+";
			this.button_Security_Add.UseVisualStyleBackColor = true;
			this.button_Security_Add.Click += new System.EventHandler(this.button_Security_Add_Click);
			// 
			// tabPage_Update_Action
			// 
			this.tabPage_Update_Action.Controls.Add(this.label_Action_Timeout);
			this.tabPage_Update_Action.Controls.Add(this.groupBox_Action_IP_Change_PlaySound);
			this.tabPage_Update_Action.Controls.Add(this.checkBox_Action_UpdateIP);
			this.tabPage_Update_Action.Controls.Add(this.checkBox_Action_IP_Change_Popup);
			this.tabPage_Update_Action.Controls.Add(this.groupBox_Action_Set_DNS_Server);
			this.tabPage_Update_Action.Controls.Add(this.checkBox_Action_DNS_Lookup_First);
			this.tabPage_Update_Action.Controls.Add(this.numericUpDown_Action_Timeout);
			this.tabPage_Update_Action.Controls.Add(this.numericUpDown_Action_AutoAction_Interval);
			this.tabPage_Update_Action.Controls.Add(this.checkBox_Action_AutoAction_Interval);
			this.tabPage_Update_Action.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Update_Action.Name = "tabPage_Update_Action";
			this.tabPage_Update_Action.Size = new System.Drawing.Size(529, 281);
			this.tabPage_Update_Action.TabIndex = 3;
			this.tabPage_Update_Action.Text = "更新操作";
			this.tabPage_Update_Action.UseVisualStyleBackColor = true;
			// 
			// label_Action_Timeout
			// 
			this.label_Action_Timeout.AutoSize = true;
			this.label_Action_Timeout.Location = new System.Drawing.Point(3, 185);
			this.label_Action_Timeout.Name = "label_Action_Timeout";
			this.label_Action_Timeout.Size = new System.Drawing.Size(245, 12);
			this.label_Action_Timeout.TabIndex = 78;
			this.label_Action_Timeout.Text = "自动更新超时（单位：秒。0 = 无限等待）：";
			// 
			// numericUpDown_Action_Timeout
			// 
			this.numericUpDown_Action_Timeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.numericUpDown_Action_Timeout.Location = new System.Drawing.Point(254, 183);
			this.numericUpDown_Action_Timeout.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numericUpDown_Action_Timeout.Name = "numericUpDown_Action_Timeout";
			this.numericUpDown_Action_Timeout.Size = new System.Drawing.Size(60, 21);
			this.numericUpDown_Action_Timeout.TabIndex = 77;
			this.numericUpDown_Action_Timeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown_Action_Timeout.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.numericUpDown_Action_Timeout.ValueChanged += new System.EventHandler(this.numericUpDown_Action_Timeout_ValueChanged);
			// 
			// tabPage_Fix_hosts
			// 
			this.tabPage_Fix_hosts.Controls.Add(this.button_Fix_hosts__Path_Browser);
			this.tabPage_Fix_hosts.Controls.Add(this.textBox_Fix_hosts__Content);
			this.tabPage_Fix_hosts.Controls.Add(this.textBox_Fix_hosts__Path);
			this.tabPage_Fix_hosts.Controls.Add(this.label_Fix_hosts__Content);
			this.tabPage_Fix_hosts.Controls.Add(this.label_Fix_hosts__Path);
			this.tabPage_Fix_hosts.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Fix_hosts.Name = "tabPage_Fix_hosts";
			this.tabPage_Fix_hosts.Size = new System.Drawing.Size(529, 281);
			this.tabPage_Fix_hosts.TabIndex = 4;
			this.tabPage_Fix_hosts.Text = "修正 hosts";
			this.tabPage_Fix_hosts.UseVisualStyleBackColor = true;
			// 
			// button_Fix_hosts__Path_Browser
			// 
			this.button_Fix_hosts__Path_Browser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_Fix_hosts__Path_Browser.Location = new System.Drawing.Point(451, 16);
			this.button_Fix_hosts__Path_Browser.Name = "button_Fix_hosts__Path_Browser";
			this.button_Fix_hosts__Path_Browser.Size = new System.Drawing.Size(75, 23);
			this.button_Fix_hosts__Path_Browser.TabIndex = 2;
			this.button_Fix_hosts__Path_Browser.Text = "打开目录";
			this.button_Fix_hosts__Path_Browser.UseVisualStyleBackColor = true;
			this.button_Fix_hosts__Path_Browser.Click += new System.EventHandler(this.button_Fix_hosts__Path_Browser_Click);
			// 
			// textBox_Fix_hosts__Content
			// 
			this.textBox_Fix_hosts__Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Fix_hosts__Content.Location = new System.Drawing.Point(3, 57);
			this.textBox_Fix_hosts__Content.Multiline = true;
			this.textBox_Fix_hosts__Content.Name = "textBox_Fix_hosts__Content";
			this.textBox_Fix_hosts__Content.ReadOnly = true;
			this.textBox_Fix_hosts__Content.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_Fix_hosts__Content.Size = new System.Drawing.Size(523, 221);
			this.textBox_Fix_hosts__Content.TabIndex = 1;
			this.textBox_Fix_hosts__Content.Text = "72.246.164.14\tapi.godaddy.com\r\n162.216.242.29\twww.dynu.com\r\n162.216.242.253\tapi.d" +
    "ynu.com\r\n172.67.74.152\tapi.ipify.org";
			// 
			// textBox_Fix_hosts__Path
			// 
			this.textBox_Fix_hosts__Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Fix_hosts__Path.Location = new System.Drawing.Point(3, 18);
			this.textBox_Fix_hosts__Path.Name = "textBox_Fix_hosts__Path";
			this.textBox_Fix_hosts__Path.ReadOnly = true;
			this.textBox_Fix_hosts__Path.Size = new System.Drawing.Size(442, 21);
			this.textBox_Fix_hosts__Path.TabIndex = 1;
			this.textBox_Fix_hosts__Path.Text = "C:\\Windows\\System32\\drivers\\etc\\hosts";
			// 
			// label_Fix_hosts__Content
			// 
			this.label_Fix_hosts__Content.AutoSize = true;
			this.label_Fix_hosts__Content.Location = new System.Drawing.Point(3, 42);
			this.label_Fix_hosts__Content.Name = "label_Fix_hosts__Content";
			this.label_Fix_hosts__Content.Size = new System.Drawing.Size(101, 12);
			this.label_Fix_hosts__Content.TabIndex = 0;
			this.label_Fix_hosts__Content.Text = "并添加以下记录：";
			// 
			// label_Fix_hosts__Path
			// 
			this.label_Fix_hosts__Path.AutoSize = true;
			this.label_Fix_hosts__Path.Location = new System.Drawing.Point(3, 3);
			this.label_Fix_hosts__Path.Name = "label_Fix_hosts__Path";
			this.label_Fix_hosts__Path.Size = new System.Drawing.Size(377, 12);
			this.label_Fix_hosts__Path.TabIndex = 0;
			this.label_Fix_hosts__Path.Text = "如果出现访问部分域名不正常，可以尝试修改 hosts。修改以下文件：";
			// 
			// groupBox_Settings_Preview
			// 
			this.groupBox_Settings_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Action_AutoUpdate_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Timeout_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__DNS_Server_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__DNS_Lookup_First_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Action_UpdateIP_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Security_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Set_IPv6_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Set_IPv4_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Ping_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Update_Type_Val);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Action_AutoUpdate);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Timeout);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__DNS_Server);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__DNS_Lookup_First);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Action_UpdateIP);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Security);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Set_IPv6);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Ping);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Set_IPv4);
			this.groupBox_Settings_Preview.Controls.Add(this.label_Settings_Preview__Update_Type);
			this.groupBox_Settings_Preview.Location = new System.Drawing.Point(551, 484);
			this.groupBox_Settings_Preview.Name = "groupBox_Settings_Preview";
			this.groupBox_Settings_Preview.Size = new System.Drawing.Size(371, 222);
			this.groupBox_Settings_Preview.TabIndex = 81;
			this.groupBox_Settings_Preview.TabStop = false;
			this.groupBox_Settings_Preview.Text = "【预览设置】";
			// 
			// label_Settings_Preview__Action_AutoUpdate_Val
			// 
			this.label_Settings_Preview__Action_AutoUpdate_Val.AutoSize = true;
			this.label_Settings_Preview__Action_AutoUpdate_Val.Location = new System.Drawing.Point(95, 137);
			this.label_Settings_Preview__Action_AutoUpdate_Val.Name = "label_Settings_Preview__Action_AutoUpdate_Val";
			this.label_Settings_Preview__Action_AutoUpdate_Val.Size = new System.Drawing.Size(47, 12);
			this.label_Settings_Preview__Action_AutoUpdate_Val.TabIndex = 0;
			this.label_Settings_Preview__Action_AutoUpdate_Val.Text = "每 600s";
			// 
			// label_Settings_Preview__Timeout_Val
			// 
			this.label_Settings_Preview__Timeout_Val.AutoSize = true;
			this.label_Settings_Preview__Timeout_Val.Location = new System.Drawing.Point(95, 197);
			this.label_Settings_Preview__Timeout_Val.Name = "label_Settings_Preview__Timeout_Val";
			this.label_Settings_Preview__Timeout_Val.Size = new System.Drawing.Size(17, 12);
			this.label_Settings_Preview__Timeout_Val.TabIndex = 0;
			this.label_Settings_Preview__Timeout_Val.Text = "15";
			// 
			// label_Settings_Preview__DNS_Server_Val
			// 
			this.label_Settings_Preview__DNS_Server_Val.AutoSize = true;
			this.label_Settings_Preview__DNS_Server_Val.Location = new System.Drawing.Point(95, 177);
			this.label_Settings_Preview__DNS_Server_Val.Name = "label_Settings_Preview__DNS_Server_Val";
			this.label_Settings_Preview__DNS_Server_Val.Size = new System.Drawing.Size(41, 12);
			this.label_Settings_Preview__DNS_Server_Val.TabIndex = 0;
			this.label_Settings_Preview__DNS_Server_Val.Text = "自定义";
			// 
			// label_Settings_Preview__DNS_Lookup_First_Val
			// 
			this.label_Settings_Preview__DNS_Lookup_First_Val.AutoSize = true;
			this.label_Settings_Preview__DNS_Lookup_First_Val.Location = new System.Drawing.Point(95, 157);
			this.label_Settings_Preview__DNS_Lookup_First_Val.Name = "label_Settings_Preview__DNS_Lookup_First_Val";
			this.label_Settings_Preview__DNS_Lookup_First_Val.Size = new System.Drawing.Size(17, 12);
			this.label_Settings_Preview__DNS_Lookup_First_Val.TabIndex = 0;
			this.label_Settings_Preview__DNS_Lookup_First_Val.Text = "√";
			// 
			// label_Settings_Preview__Action_UpdateIP_Val
			// 
			this.label_Settings_Preview__Action_UpdateIP_Val.AutoSize = true;
			this.label_Settings_Preview__Action_UpdateIP_Val.Location = new System.Drawing.Point(95, 117);
			this.label_Settings_Preview__Action_UpdateIP_Val.Name = "label_Settings_Preview__Action_UpdateIP_Val";
			this.label_Settings_Preview__Action_UpdateIP_Val.Size = new System.Drawing.Size(17, 12);
			this.label_Settings_Preview__Action_UpdateIP_Val.TabIndex = 0;
			this.label_Settings_Preview__Action_UpdateIP_Val.Text = "√";
			// 
			// label_Settings_Preview__Security_Val
			// 
			this.label_Settings_Preview__Security_Val.AutoSize = true;
			this.label_Settings_Preview__Security_Val.Location = new System.Drawing.Point(95, 97);
			this.label_Settings_Preview__Security_Val.Name = "label_Settings_Preview__Security_Val";
			this.label_Settings_Preview__Security_Val.Size = new System.Drawing.Size(83, 12);
			this.label_Settings_Preview__Security_Val.TabIndex = 0;
			this.label_Settings_Preview__Security_Val.Text = "xx 个配置文件";
			// 
			// label_Settings_Preview__Set_IPv6_Val
			// 
			this.label_Settings_Preview__Set_IPv6_Val.AutoSize = true;
			this.label_Settings_Preview__Set_IPv6_Val.Location = new System.Drawing.Point(95, 77);
			this.label_Settings_Preview__Set_IPv6_Val.Name = "label_Settings_Preview__Set_IPv6_Val";
			this.label_Settings_Preview__Set_IPv6_Val.Size = new System.Drawing.Size(125, 12);
			this.label_Settings_Preview__Set_IPv6_Val.TabIndex = 0;
			this.label_Settings_Preview__Set_IPv6_Val.Text = "Server 接受连接的 IP";
			// 
			// label_Settings_Preview__Set_IPv4_Val
			// 
			this.label_Settings_Preview__Set_IPv4_Val.AutoSize = true;
			this.label_Settings_Preview__Set_IPv4_Val.Location = new System.Drawing.Point(95, 57);
			this.label_Settings_Preview__Set_IPv4_Val.Name = "label_Settings_Preview__Set_IPv4_Val";
			this.label_Settings_Preview__Set_IPv4_Val.Size = new System.Drawing.Size(125, 12);
			this.label_Settings_Preview__Set_IPv4_Val.TabIndex = 0;
			this.label_Settings_Preview__Set_IPv4_Val.Text = "Server 接受连接的 IP";
			// 
			// label_Settings_Preview__Ping_Val
			// 
			this.label_Settings_Preview__Ping_Val.AutoSize = true;
			this.label_Settings_Preview__Ping_Val.Location = new System.Drawing.Point(95, 37);
			this.label_Settings_Preview__Ping_Val.Name = "label_Settings_Preview__Ping_Val";
			this.label_Settings_Preview__Ping_Val.Size = new System.Drawing.Size(23, 12);
			this.label_Settings_Preview__Ping_Val.TabIndex = 0;
			this.label_Settings_Preview__Ping_Val.Text = "xxx";
			// 
			// label_Settings_Preview__Update_Type_Val
			// 
			this.label_Settings_Preview__Update_Type_Val.AutoSize = true;
			this.label_Settings_Preview__Update_Type_Val.Location = new System.Drawing.Point(95, 17);
			this.label_Settings_Preview__Update_Type_Val.Name = "label_Settings_Preview__Update_Type_Val";
			this.label_Settings_Preview__Update_Type_Val.Size = new System.Drawing.Size(137, 12);
			this.label_Settings_Preview__Update_Type_Val.TabIndex = 0;
			this.label_Settings_Preview__Update_Type_Val.Text = "xxx.xxxx.xxx.xxx:xxxxx";
			// 
			// label_Settings_Preview__Action_AutoUpdate
			// 
			this.label_Settings_Preview__Action_AutoUpdate.AutoSize = true;
			this.label_Settings_Preview__Action_AutoUpdate.Location = new System.Drawing.Point(6, 137);
			this.label_Settings_Preview__Action_AutoUpdate.Name = "label_Settings_Preview__Action_AutoUpdate";
			this.label_Settings_Preview__Action_AutoUpdate.Size = new System.Drawing.Size(65, 12);
			this.label_Settings_Preview__Action_AutoUpdate.TabIndex = 0;
			this.label_Settings_Preview__Action_AutoUpdate.Text = "自动更新：";
			// 
			// label_Settings_Preview__Timeout
			// 
			this.label_Settings_Preview__Timeout.AutoSize = true;
			this.label_Settings_Preview__Timeout.Location = new System.Drawing.Point(6, 197);
			this.label_Settings_Preview__Timeout.Name = "label_Settings_Preview__Timeout";
			this.label_Settings_Preview__Timeout.Size = new System.Drawing.Size(83, 12);
			this.label_Settings_Preview__Timeout.TabIndex = 0;
			this.label_Settings_Preview__Timeout.Text = "更新超时(s)：";
			// 
			// label_Settings_Preview__DNS_Server
			// 
			this.label_Settings_Preview__DNS_Server.AutoSize = true;
			this.label_Settings_Preview__DNS_Server.Location = new System.Drawing.Point(6, 177);
			this.label_Settings_Preview__DNS_Server.Name = "label_Settings_Preview__DNS_Server";
			this.label_Settings_Preview__DNS_Server.Size = new System.Drawing.Size(77, 12);
			this.label_Settings_Preview__DNS_Server.TabIndex = 0;
			this.label_Settings_Preview__DNS_Server.Text = "DNS 服务器：";
			// 
			// label_Settings_Preview__DNS_Lookup_First
			// 
			this.label_Settings_Preview__DNS_Lookup_First.AutoSize = true;
			this.label_Settings_Preview__DNS_Lookup_First.Location = new System.Drawing.Point(6, 157);
			this.label_Settings_Preview__DNS_Lookup_First.Name = "label_Settings_Preview__DNS_Lookup_First";
			this.label_Settings_Preview__DNS_Lookup_First.Size = new System.Drawing.Size(77, 12);
			this.label_Settings_Preview__DNS_Lookup_First.TabIndex = 0;
			this.label_Settings_Preview__DNS_Lookup_First.Text = "先解析域名：";
			// 
			// label_Settings_Preview__Action_UpdateIP
			// 
			this.label_Settings_Preview__Action_UpdateIP.AutoSize = true;
			this.label_Settings_Preview__Action_UpdateIP.Location = new System.Drawing.Point(6, 117);
			this.label_Settings_Preview__Action_UpdateIP.Name = "label_Settings_Preview__Action_UpdateIP";
			this.label_Settings_Preview__Action_UpdateIP.Size = new System.Drawing.Size(83, 12);
			this.label_Settings_Preview__Action_UpdateIP.TabIndex = 0;
			this.label_Settings_Preview__Action_UpdateIP.Text = "更新域名 IP：";
			// 
			// label_Settings_Preview__Security
			// 
			this.label_Settings_Preview__Security.AutoSize = true;
			this.label_Settings_Preview__Security.Location = new System.Drawing.Point(6, 97);
			this.label_Settings_Preview__Security.Name = "label_Settings_Preview__Security";
			this.label_Settings_Preview__Security.Size = new System.Drawing.Size(65, 12);
			this.label_Settings_Preview__Security.TabIndex = 0;
			this.label_Settings_Preview__Security.Text = "安全设置：";
			// 
			// label_Settings_Preview__Set_IPv6
			// 
			this.label_Settings_Preview__Set_IPv6.AutoSize = true;
			this.label_Settings_Preview__Set_IPv6.Location = new System.Drawing.Point(6, 77);
			this.label_Settings_Preview__Set_IPv6.Name = "label_Settings_Preview__Set_IPv6";
			this.label_Settings_Preview__Set_IPv6.Size = new System.Drawing.Size(65, 12);
			this.label_Settings_Preview__Set_IPv6.TabIndex = 0;
			this.label_Settings_Preview__Set_IPv6.Text = "设置IPv6：";
			// 
			// label_Settings_Preview__Ping
			// 
			this.label_Settings_Preview__Ping.AutoSize = true;
			this.label_Settings_Preview__Ping.Location = new System.Drawing.Point(6, 37);
			this.label_Settings_Preview__Ping.Name = "label_Settings_Preview__Ping";
			this.label_Settings_Preview__Ping.Size = new System.Drawing.Size(77, 12);
			this.label_Settings_Preview__Ping.TabIndex = 0;
			this.label_Settings_Preview__Ping.Text = "Ping（ms）：";
			// 
			// label_Settings_Preview__Set_IPv4
			// 
			this.label_Settings_Preview__Set_IPv4.AutoSize = true;
			this.label_Settings_Preview__Set_IPv4.Location = new System.Drawing.Point(6, 57);
			this.label_Settings_Preview__Set_IPv4.Name = "label_Settings_Preview__Set_IPv4";
			this.label_Settings_Preview__Set_IPv4.Size = new System.Drawing.Size(65, 12);
			this.label_Settings_Preview__Set_IPv4.TabIndex = 0;
			this.label_Settings_Preview__Set_IPv4.Text = "设置IPv4：";
			// 
			// label_Settings_Preview__Update_Type
			// 
			this.label_Settings_Preview__Update_Type.AutoSize = true;
			this.label_Settings_Preview__Update_Type.Location = new System.Drawing.Point(6, 17);
			this.label_Settings_Preview__Update_Type.Name = "label_Settings_Preview__Update_Type";
			this.label_Settings_Preview__Update_Type.Size = new System.Drawing.Size(65, 12);
			this.label_Settings_Preview__Update_Type.TabIndex = 0;
			this.label_Settings_Preview__Update_Type.Text = "更新方式：";
			// 
			// frm_MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(934, 781);
			this.Controls.Add(this.groupBox_Settings_Preview);
			this.Controls.Add(this.tabControl_Main);
			this.Controls.Add(this.splitContainer_Main);
			this.Controls.Add(this.linkLabel_WebSite);
			this.Controls.Add(this.linkLabel_Github);
			this.Controls.Add(this.button_Update);
			this.Name = "frm_MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AcgDev DDNS Tool v0.02";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_MainForm_FormClosing);
			this.Load += new System.EventHandler(this.frm_MainForm_Load);
			this.contextMenuStrip_NotifyIcon.ResumeLayout(false);
			this.tabControl_Security__Property.ResumeLayout(false);
			this.tabPage_Security__Godaddy.ResumeLayout(false);
			this.tabPage_Security__Godaddy.PerformLayout();
			this.tabPage_Security__dynv6.ResumeLayout(false);
			this.tabPage_Security__dynv6.PerformLayout();
			this.tabPage_Security__dynu.ResumeLayout(false);
			this.tabPage_Security__dynu.PerformLayout();
			this.groupBox_Action_IP_Change_PlaySound.ResumeLayout(false);
			this.groupBox_Action_IP_Change_PlaySound.PerformLayout();
			this.groupBox_Action_Set_DNS_Server.ResumeLayout(false);
			this.groupBox_Action_Set_DNS_Server.PerformLayout();
			this.groupBox_Settings_RemoteServer.ResumeLayout(false);
			this.groupBox_Settings_RemoteServer.PerformLayout();
			this.groupBox_Domains.ResumeLayout(false);
			this.groupBox_Domains.PerformLayout();
			this.toolStrip_Domains.ResumeLayout(false);
			this.toolStrip_Domains.PerformLayout();
			this.contextMenuStrip_Domains.ResumeLayout(false);
			this.contextMenuStrip_Logs.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Logs_MaxLines)).EndInit();
			this.groupBox_Logs.ResumeLayout(false);
			this.groupBox_Logs.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Action_AutoAction_Interval)).EndInit();
			this.splitContainer_Main.Panel1.ResumeLayout(false);
			this.splitContainer_Main.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).EndInit();
			this.splitContainer_Main.ResumeLayout(false);
			this.tabControl_Main.ResumeLayout(false);
			this.tabPage_Update_Type.ResumeLayout(false);
			this.tabPage_Update_Type.PerformLayout();
			this.tabPage_Set_IP.ResumeLayout(false);
			this.groupBox_Set_IPv6.ResumeLayout(false);
			this.groupBox_Set_IPv6.PerformLayout();
			this.groupBox_Set_IPv4.ResumeLayout(false);
			this.groupBox_Set_IPv4.PerformLayout();
			this.tabPage_Security.ResumeLayout(false);
			this.contextMenuStrip_Security.ResumeLayout(false);
			this.groupBox_Security__Property.ResumeLayout(false);
			this.groupBox_Security__Property.PerformLayout();
			this.tabPage_Update_Action.ResumeLayout(false);
			this.tabPage_Update_Action.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Action_Timeout)).EndInit();
			this.tabPage_Fix_hosts.ResumeLayout(false);
			this.tabPage_Fix_hosts.PerformLayout();
			this.groupBox_Settings_Preview.ResumeLayout(false);
			this.groupBox_Settings_Preview.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon_Main;
		private System.Windows.Forms.TabControl tabControl_Security__Property;
		private System.Windows.Forms.TabPage tabPage_Security__Godaddy;
		private System.Windows.Forms.TabPage tabPage_Security__dynv6;
		private System.Windows.Forms.RadioButton radioButton_Settings_Type__Remote;
		private System.Windows.Forms.RadioButton radioButton_Settings_Type__Local;
		private System.Windows.Forms.GroupBox groupBox_Settings_RemoteServer;
		private System.Windows.Forms.CheckBox checkBox_Settings_RemoteServer__Ping;
		private System.Windows.Forms.CheckBox checkBox_Settings_RemoteServer__Pwd;
		private System.Windows.Forms.Label label_Settings_RemoteServer__User;
		private System.Windows.Forms.Label label_Settings_RemoteServer__Ping;
		private System.Windows.Forms.Label label_Settings_RemoteServer__Pwd;
		private System.Windows.Forms.Label label_Settings_RemoteServer__Addr;
		private System.Windows.Forms.TextBox textBox_Settings_RemoteServer__User;
		private System.Windows.Forms.TextBox textBox_Settings_RemoteServer__Ping;
		private System.Windows.Forms.TextBox textBox_Settings_RemoteServer__Pwd;
		private System.Windows.Forms.TextBox textBox_Settings_RemoteServer__Addr;
		private System.Windows.Forms.RadioButton radioButton_Settings_IPv6__Manual;
		private System.Windows.Forms.RadioButton radioButton_Settings_IPv6__Accept_IP;
		private System.Windows.Forms.RadioButton radioButton_Settings_IPv6__From_URL;
		private System.Windows.Forms.ComboBox comboBox_Settings_IPv4__From_URL;
		private System.Windows.Forms.TextBox textBox_Settings_IPv4;
		private System.Windows.Forms.TextBox textBox_Security_Godaddy__Key;
		private System.Windows.Forms.TextBox textBox_Security_Godaddy__Secret;
		private System.Windows.Forms.CheckBox checkBox_Security_Godaddy__Secret;
		private System.Windows.Forms.CheckBox checkBox_Security__Save_To_Config;
		private System.Windows.Forms.Label label_Security_Godaddy__Key;
		private System.Windows.Forms.Label label_Security_Godaddy__Secret;
		private System.Windows.Forms.CheckBox checkBox_Security_Godaddy__Key;
		private System.Windows.Forms.LinkLabel linkLabel_Security_Godaddy__API;
		private System.Windows.Forms.LinkLabel linkLabel_Security_dynv6__API;
		private System.Windows.Forms.TextBox textBox_Security_dynv6__token;
		private System.Windows.Forms.Label label_Security_dynv6__token;
		private System.Windows.Forms.CheckBox checkBox_Security_dynv6__token;
		private System.Windows.Forms.Button button_Action_IP_Change_PlaySound;
		private System.Windows.Forms.TextBox textBox_Action_IP_Change_PlaySound;
		private System.Windows.Forms.CheckBox checkBox_Action_IP_Change_PlaySound;
		private System.Windows.Forms.CheckBox checkBox_Action_IP_Change_Popup;
		private System.Windows.Forms.CheckBox checkBox_Action_DNS_Lookup_First;
		private System.Windows.Forms.CheckBox checkBox_Action_UpdateIP;
		private System.Windows.Forms.GroupBox groupBox_Domains;
		private System.Windows.Forms.ListView listView_Domains;
		private System.Windows.Forms.ColumnHeader columnHeader_Domains_Domain;
		private System.Windows.Forms.ColumnHeader columnHeader_Domains_IPv6;
		private System.Windows.Forms.ToolStrip toolStrip_Domains;
		private System.Windows.Forms.ToolStripButton toolStripButton_Domains_Add;
		private System.Windows.Forms.ToolStripButton toolStripButton_Domains_Modify;
		private System.Windows.Forms.ToolStripButton toolStripButton_Domains_Delete;
		private System.Windows.Forms.ListView listView_Logs;
		private System.Windows.Forms.ColumnHeader columnHeader_Logs_Time;
		private System.Windows.Forms.ColumnHeader columnHeader_Logs_Log;
		private System.Windows.Forms.Button button_Update;
		private System.Windows.Forms.Label label_Logs_MaxLines;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Logs;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Logs_Copy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Logs_Delete;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Logs_SelectAll;
		private System.Windows.Forms.GroupBox groupBox_Logs;
		private System.Windows.Forms.ColumnHeader columnHeader_Domains_Type;
		private System.Windows.Forms.LinkLabel linkLabel_WebSite;
		private System.Windows.Forms.LinkLabel linkLabel_Github;
		private System.Windows.Forms.CheckBox checkBox_Logs__Save_To_File;
		private System.Windows.Forms.CheckBox checkBox_Action_AutoAction_Interval;
		private System.Windows.Forms.NumericUpDown numericUpDown_Action_AutoAction_Interval;
		private System.Windows.Forms.NumericUpDown numericUpDown_Logs_MaxLines;
		private System.Windows.Forms.ColumnHeader columnHeader_Domains_Status;
		private System.Windows.Forms.Button button_Action_IP_Change_StopSound;
		private System.Windows.Forms.Timer timer_Save_Config;
		private System.Windows.Forms.Timer timer_Update;
		private System.Windows.Forms.Timer timer_Ping;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_NotifyIcon;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Open;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
		private System.Windows.Forms.ComboBox comboBox_Settings_IPv6__From_URL;
		private System.Windows.Forms.TextBox textBox_Settings_IPv6;
		private System.Windows.Forms.CheckBox checkBox_Action_Use_Custom_DNS;
		private System.Windows.Forms.ColumnHeader columnHeader_Domains_IPv4;
		private System.Windows.Forms.SplitContainer splitContainer_Main;
		private System.Windows.Forms.GroupBox groupBox_Action_Set_DNS_Server;
		private System.Windows.Forms.GroupBox groupBox_Action_IP_Change_PlaySound;
		private System.Windows.Forms.TabControl tabControl_Main;
		private System.Windows.Forms.TabPage tabPage_Update_Type;
		private System.Windows.Forms.TabPage tabPage_Set_IP;
		private System.Windows.Forms.TabPage tabPage_Security;
		private System.Windows.Forms.TabPage tabPage_Update_Action;
		private System.Windows.Forms.Button button_Security_Add;
		private System.Windows.Forms.GroupBox groupBox_Security__Property;
		private System.Windows.Forms.TextBox textBox_Security__Property__Name;
		private System.Windows.Forms.Label label_Security__Property__Name;
		private System.Windows.Forms.Button button_Security_Del;
		private System.Windows.Forms.GroupBox groupBox_Settings_Preview;
		private System.Windows.Forms.Label label_Settings_Preview__Set_IPv4_Val;
		private System.Windows.Forms.Label label_Settings_Preview__Update_Type_Val;
		private System.Windows.Forms.Label label_Settings_Preview__Set_IPv4;
		private System.Windows.Forms.Label label_Settings_Preview__Update_Type;
		private System.Windows.Forms.Label label_Settings_Preview__Security_Val;
		private System.Windows.Forms.Label label_Settings_Preview__Security;
		private System.Windows.Forms.Label label_Settings_Preview__Action_UpdateIP_Val;
		private System.Windows.Forms.Label label_Settings_Preview__Action_UpdateIP;
		private System.Windows.Forms.Label label_Settings_Preview__Action_AutoUpdate_Val;
		private System.Windows.Forms.Label label_Settings_Preview__DNS_Server_Val;
		private System.Windows.Forms.Label label_Settings_Preview__DNS_Lookup_First_Val;
		private System.Windows.Forms.Label label_Settings_Preview__Action_AutoUpdate;
		private System.Windows.Forms.Label label_Settings_Preview__DNS_Server;
		private System.Windows.Forms.Label label_Settings_Preview__DNS_Lookup_First;
		private System.Windows.Forms.Label label_Settings_Preview__Ping_Val;
		private System.Windows.Forms.Label label_Settings_Preview__Ping;
		private System.Windows.Forms.ListView listView_Security;
		private System.Windows.Forms.ColumnHeader columnHeader_Security;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Security;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Security_Add;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem__Security_Del;
		private System.Windows.Forms.ColumnHeader columnHeader_Domains_Profile;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Domains;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Domains_Add;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Domains_Modify;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Domains_Delete;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Domains_IPv4_Enable;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Domains_IPv4_Disable;
		private System.Windows.Forms.ToolStripButton toolStripButton_Domains_IPv4_Enable;
		private System.Windows.Forms.ToolStripButton toolStripButton_Domains_IPv4_Disable;
		internal System.Windows.Forms.TextBox textBox_Action_Custom_DNS_List;
		private System.Windows.Forms.NumericUpDown numericUpDown_Action_Timeout;
		private System.Windows.Forms.Label label_Settings_Preview__Timeout_Val;
		private System.Windows.Forms.Label label_Settings_Preview__Timeout;
		private System.Windows.Forms.Label label_Action_Timeout;
		private System.Windows.Forms.TabPage tabPage_Fix_hosts;
		private System.Windows.Forms.Label label_Fix_hosts__Path;
		private System.Windows.Forms.TextBox textBox_Fix_hosts__Path;
		private System.Windows.Forms.TextBox textBox_Fix_hosts__Content;
		private System.Windows.Forms.Label label_Fix_hosts__Content;
		private System.Windows.Forms.Button button_Fix_hosts__Path_Browser;
		private System.Windows.Forms.TabPage tabPage_Security__dynu;
		private System.Windows.Forms.LinkLabel linkLabel_Security_dynu__API;
		private System.Windows.Forms.CheckBox checkBox_Security_dynu__API_Key;
		private System.Windows.Forms.TextBox textBox_Security_dynu__API_Key;
		private System.Windows.Forms.Label label_Security_dynu__API_Key;
		private System.Windows.Forms.ToolStripButton toolStripButton_Domains_IPv6_Enable;
		private System.Windows.Forms.ToolStripButton toolStripButton_Domains_IPv6_Disable;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Domains_IPv6_Enable;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Domains_IPv6_Disable;
		private System.Windows.Forms.GroupBox groupBox_Set_IPv4;
		private System.Windows.Forms.RadioButton radioButton_Settings_IPv4__From_URL;
		private System.Windows.Forms.RadioButton radioButton_Settings_IPv4__Manual;
		private System.Windows.Forms.RadioButton radioButton_Settings_IPv4__Accept_IP;
		private System.Windows.Forms.GroupBox groupBox_Set_IPv6;
		private System.Windows.Forms.Label label_Settings_Preview__Set_IPv6_Val;
		private System.Windows.Forms.Label label_Settings_Preview__Set_IPv6;
	}
}

