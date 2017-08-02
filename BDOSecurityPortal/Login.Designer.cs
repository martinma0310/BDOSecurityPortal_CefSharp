namespace BDOSecurityPortal
{
    partial class Login
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel1 = new BDOSecurityPortal.PaintPanel();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.languageTools = new System.Windows.Forms.StatusStrip();
            this.langChineseTool = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.langEnglishTool = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblBtnChangePwd = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.cbxUserName = new System.Windows.Forms.ComboBox();
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.chxPassword = new System.Windows.Forms.CheckBox();
            this.chxUser = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblAppName = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.languageTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.languageTools);
            this.panel1.Controls.Add(this.btnHelp);
            this.panel1.Controls.Add(this.lblBtnChangePwd);
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Controls.Add(this.cbxUserName);
            this.panel1.Controls.Add(this.lblCopyRight);
            this.panel1.Controls.Add(this.chxPassword);
            this.panel1.Controls.Add(this.chxUser);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.lblAppName);
            this.panel1.Controls.Add(this.pbLogo);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(1024, 618);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 618);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_MouseMove);
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 17.5F);
            this.txtUserName.Location = new System.Drawing.Point(382, 273);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(295, 38);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.TabIndexChanged += new System.EventHandler(this.txtUserName_TabIndexChanged);
            // 
            // languageTools
            // 
            this.languageTools.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.languageTools.BackColor = System.Drawing.Color.Transparent;
            this.languageTools.Dock = System.Windows.Forms.DockStyle.None;
            this.languageTools.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.languageTools.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.languageTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.langChineseTool,
            this.toolStripStatusLabel4,
            this.langEnglishTool});
            this.languageTools.Location = new System.Drawing.Point(528, 388);
            this.languageTools.Name = "languageTools";
            this.languageTools.Size = new System.Drawing.Size(169, 26);
            this.languageTools.SizingGrip = false;
            this.languageTools.TabIndex = 1006;
            this.languageTools.Text = "statusStrip1";
            // 
            // langChineseTool
            // 
            this.langChineseTool.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.langChineseTool.Name = "langChineseTool";
            this.langChineseTool.Size = new System.Drawing.Size(74, 21);
            this.langChineseTool.Text = "简体中文";
            this.langChineseTool.Click += new System.EventHandler(this.langChineseTool_Click);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(14, 21);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // langEnglishTool
            // 
            this.langEnglishTool.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.langEnglishTool.Name = "langEnglishTool";
            this.langEnglishTool.Size = new System.Drawing.Size(64, 21);
            this.langEnglishTool.Text = "English";
            this.langEnglishTool.Click += new System.EventHandler(this.langEnglishTool_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::BDOSecurityPortal.Properties.Resources.help_blue;
            this.btnHelp.Location = new System.Drawing.Point(926, 22);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 23);
            this.btnHelp.TabIndex = 999;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            this.btnHelp.MouseEnter += new System.EventHandler(this.btnHelp_MouseEnter);
            this.btnHelp.MouseLeave += new System.EventHandler(this.btnHelp_MouseLeave);
            // 
            // lblBtnChangePwd
            // 
            this.lblBtnChangePwd.AutoSize = true;
            this.lblBtnChangePwd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBtnChangePwd.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblBtnChangePwd.Location = new System.Drawing.Point(618, 486);
            this.lblBtnChangePwd.Name = "lblBtnChangePwd";
            this.lblBtnChangePwd.Size = new System.Drawing.Size(56, 17);
            this.lblBtnChangePwd.TabIndex = 6;
            this.lblBtnChangePwd.Text = "修改密码";
            this.lblBtnChangePwd.Click += new System.EventHandler(this.lblBtnChangePwd_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Italic);
            this.labelVersion.ForeColor = System.Drawing.Color.Crimson;
            this.labelVersion.Location = new System.Drawing.Point(869, 599);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(152, 16);
            this.labelVersion.TabIndex = 1002;
            this.labelVersion.Text = "Version: 1.1.00.01";
            // 
            // cbxUserName
            // 
            this.cbxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxUserName.Font = new System.Drawing.Font("微软雅黑", 15.5F);
            this.cbxUserName.FormattingEnabled = true;
            this.cbxUserName.Location = new System.Drawing.Point(381, 215);
            this.cbxUserName.Name = "cbxUserName";
            this.cbxUserName.Size = new System.Drawing.Size(295, 36);
            this.cbxUserName.TabIndex = 33;
            this.cbxUserName.Visible = false;
            this.cbxUserName.SelectedIndexChanged += new System.EventHandler(this.cbxUserName_SelectedIndexChanged);
            this.cbxUserName.TextChanged += new System.EventHandler(this.cbxUserName_TextChanged);
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopyRight.AutoSize = true;
            this.lblCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyRight.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCopyRight.ForeColor = System.Drawing.Color.White;
            this.lblCopyRight.Location = new System.Drawing.Point(242, 594);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(543, 21);
            this.lblCopyRight.TabIndex = 40;
            this.lblCopyRight.Text = "Copyright ©2017 立信会计师事务所（特殊普通合伙） All rights reserved";
            // 
            // chxPassword
            // 
            this.chxPassword.AutoSize = true;
            this.chxPassword.BackColor = System.Drawing.Color.Transparent;
            this.chxPassword.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chxPassword.ForeColor = System.Drawing.Color.White;
            this.chxPassword.Location = new System.Drawing.Point(449, 485);
            this.chxPassword.Name = "chxPassword";
            this.chxPassword.Size = new System.Drawing.Size(75, 21);
            this.chxPassword.TabIndex = 5;
            this.chxPassword.Text = "记住密码";
            this.chxPassword.UseVisualStyleBackColor = false;
            this.chxPassword.CheckedChanged += new System.EventHandler(this.chxPassword_CheckedChanged);
            // 
            // chxUser
            // 
            this.chxUser.AutoSize = true;
            this.chxUser.BackColor = System.Drawing.Color.Transparent;
            this.chxUser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chxUser.ForeColor = System.Drawing.Color.White;
            this.chxUser.Location = new System.Drawing.Point(344, 485);
            this.chxUser.Name = "chxUser";
            this.chxUser.Size = new System.Drawing.Size(87, 21);
            this.chxUser.TabIndex = 4;
            this.chxUser.Text = "记住用户名";
            this.chxUser.UseVisualStyleBackColor = false;
            this.chxUser.CheckedChanged += new System.EventHandler(this.chxUser_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg_btn1;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(382, 424);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(295, 38);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "登  录";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::BDOSecurityPortal.Properties.Resources.mima_blue;
            this.pictureBox2.Location = new System.Drawing.Point(343, 339);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 36;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::BDOSecurityPortal.Properties.Resources.user_blue;
            this.pictureBox1.Location = new System.Drawing.Point(343, 273);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 17.5F);
            this.txtPassword.Location = new System.Drawing.Point(382, 339);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(295, 38);
            this.txtPassword.TabIndex = 2;
            // 
            // lblAppName
            // 
            this.lblAppName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAppName.AutoSize = true;
            this.lblAppName.BackColor = System.Drawing.Color.Transparent;
            this.lblAppName.Font = new System.Drawing.Font("黑体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(413, 155);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(200, 35);
            this.lblAppName.TabIndex = 32;
            this.lblAppName.Text = "立信云办公";
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(303, 84);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(418, 57);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLogo.TabIndex = 31;
            this.pbLogo.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::BDOSecurityPortal.Properties.Resources.esc_n;
            this.btnClose.Location = new System.Drawing.Point(981, 23);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 999;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Image = global::BDOSecurityPortal.Properties.Resources.suoxiao_n;
            this.btnMinimize.Location = new System.Drawing.Point(955, 23);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(20, 20);
            this.btnMinimize.TabIndex = 999;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            this.btnMinimize.MouseEnter += new System.EventHandler(this.btnMinimize_MouseEnter);
            this.btnMinimize.MouseLeave += new System.EventHandler(this.btnMinimize_MouseLeave);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 618);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.languageTools.ResumeLayout(false);
            this.languageTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PaintPanel panel1;
        private System.Windows.Forms.Label labelVersion;
        public System.Windows.Forms.ComboBox cbxUserName;
        private System.Windows.Forms.Label lblCopyRight;
        public System.Windows.Forms.CheckBox chxPassword;
        public System.Windows.Forms.CheckBox chxUser;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Label lblBtnChangePwd;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.StatusStrip languageTools;
        private System.Windows.Forms.ToolStripStatusLabel langChineseTool;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel langEnglishTool;
        public System.Windows.Forms.TextBox txtUserName;
    }
}