namespace ddns
{
	partial class frm_Record
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
			this.label_Name = new System.Windows.Forms.Label();
			this.textBox_Name = new System.Windows.Forms.TextBox();
			this.label_Domain = new System.Windows.Forms.Label();
			this.textBox_Domain = new System.Windows.Forms.TextBox();
			this.label_TTL = new System.Windows.Forms.Label();
			this.button_OK = new System.Windows.Forms.Button();
			this.textBox_TTL = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label_Name
			// 
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new System.Drawing.Point(54, 15);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(149, 12);
			this.label_Name.TabIndex = 1;
			this.label_Name.Text = "子域名名称（例如 www）：";
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(209, 12);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(150, 21);
			this.textBox_Name.TabIndex = 2;
			// 
			// label_Domain
			// 
			this.label_Domain.AutoSize = true;
			this.label_Domain.Location = new System.Drawing.Point(12, 42);
			this.label_Domain.Name = "label_Domain";
			this.label_Domain.Size = new System.Drawing.Size(191, 12);
			this.label_Domain.TabIndex = 3;
			this.label_Domain.Text = "根域名名称（例如 google.com）：";
			// 
			// textBox_Domain
			// 
			this.textBox_Domain.Location = new System.Drawing.Point(209, 39);
			this.textBox_Domain.Name = "textBox_Domain";
			this.textBox_Domain.Size = new System.Drawing.Size(150, 21);
			this.textBox_Domain.TabIndex = 4;
			// 
			// label_TTL
			// 
			this.label_TTL.AutoSize = true;
			this.label_TTL.Location = new System.Drawing.Point(84, 68);
			this.label_TTL.Name = "label_TTL";
			this.label_TTL.Size = new System.Drawing.Size(119, 12);
			this.label_TTL.TabIndex = 5;
			this.label_TTL.Text = "TTL（秒，可省略）：";
			// 
			// button_OK
			// 
			this.button_OK.Location = new System.Drawing.Point(110, 93);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(150, 31);
			this.button_OK.TabIndex = 7;
			this.button_OK.Text = "确定";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
			// 
			// textBox_TTL
			// 
			this.textBox_TTL.Location = new System.Drawing.Point(209, 66);
			this.textBox_TTL.Name = "textBox_TTL";
			this.textBox_TTL.Size = new System.Drawing.Size(51, 21);
			this.textBox_TTL.TabIndex = 6;
			this.textBox_TTL.Text = "600";
			this.textBox_TTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frm_Record
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(371, 136);
			this.Controls.Add(this.button_OK);
			this.Controls.Add(this.textBox_TTL);
			this.Controls.Add(this.textBox_Domain);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.label_Domain);
			this.Controls.Add(this.label_TTL);
			this.Controls.Add(this.label_Name);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frm_Record";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frm_Record_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_Name;
		private System.Windows.Forms.TextBox textBox_Name;
		private System.Windows.Forms.Label label_Domain;
		private System.Windows.Forms.TextBox textBox_Domain;
		private System.Windows.Forms.Label label_TTL;
		private System.Windows.Forms.Button button_OK;
		private System.Windows.Forms.TextBox textBox_TTL;
	}
}