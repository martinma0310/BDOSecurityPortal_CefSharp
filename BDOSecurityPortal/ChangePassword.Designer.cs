namespace BDOSecurityPortal
{
    partial class ChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePassword));
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.labelResetPassword = new System.Windows.Forms.Label();
            this.pbBtnClose = new System.Windows.Forms.PictureBox();
            this.labelOldPwd = new System.Windows.Forms.Label();
            this.labelNewPwd = new System.Windows.Forms.Label();
            this.labelConfirmPwd = new System.Windows.Forms.Label();
            this.txtOldPwd = new System.Windows.Forms.TextBox();
            this.txtNewPwd = new System.Windows.Forms.TextBox();
            this.txtConfirmPwd = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.Transparent;
            this.btnChangePassword.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg_btn1;
            this.btnChangePassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(94, 305);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(116, 44);
            this.btnChangePassword.TabIndex = 30;
            this.btnChangePassword.Text = "修改密码";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // labelResetPassword
            // 
            this.labelResetPassword.AutoSize = true;
            this.labelResetPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelResetPassword.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelResetPassword.Location = new System.Drawing.Point(160, 64);
            this.labelResetPassword.Name = "labelResetPassword";
            this.labelResetPassword.Size = new System.Drawing.Size(132, 27);
            this.labelResetPassword.TabIndex = 16;
            this.labelResetPassword.Text = "用户密码修改";
            // 
            // pbBtnClose
            // 
            this.pbBtnClose.BackColor = System.Drawing.Color.Transparent;
            this.pbBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBtnClose.Image = ((System.Drawing.Image)(resources.GetObject("pbBtnClose.Image")));
            this.pbBtnClose.Location = new System.Drawing.Point(410, 25);
            this.pbBtnClose.Name = "pbBtnClose";
            this.pbBtnClose.Size = new System.Drawing.Size(25, 25);
            this.pbBtnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbBtnClose.TabIndex = 15;
            this.pbBtnClose.TabStop = false;
            this.pbBtnClose.Click += new System.EventHandler(this.pbBtnClose_Click);
            // 
            // labelOldPwd
            // 
            this.labelOldPwd.AutoSize = true;
            this.labelOldPwd.BackColor = System.Drawing.Color.Transparent;
            this.labelOldPwd.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOldPwd.Location = new System.Drawing.Point(40, 154);
            this.labelOldPwd.Name = "labelOldPwd";
            this.labelOldPwd.Size = new System.Drawing.Size(88, 25);
            this.labelOldPwd.TabIndex = 18;
            this.labelOldPwd.Text = "旧密码：";
            // 
            // labelNewPwd
            // 
            this.labelNewPwd.AutoSize = true;
            this.labelNewPwd.BackColor = System.Drawing.Color.Transparent;
            this.labelNewPwd.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNewPwd.Location = new System.Drawing.Point(40, 204);
            this.labelNewPwd.Name = "labelNewPwd";
            this.labelNewPwd.Size = new System.Drawing.Size(88, 25);
            this.labelNewPwd.TabIndex = 19;
            this.labelNewPwd.Text = "新密码：";
            // 
            // labelConfirmPwd
            // 
            this.labelConfirmPwd.AutoSize = true;
            this.labelConfirmPwd.BackColor = System.Drawing.Color.Transparent;
            this.labelConfirmPwd.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelConfirmPwd.Location = new System.Drawing.Point(21, 254);
            this.labelConfirmPwd.Name = "labelConfirmPwd";
            this.labelConfirmPwd.Size = new System.Drawing.Size(107, 25);
            this.labelConfirmPwd.TabIndex = 20;
            this.labelConfirmPwd.Text = "密码确认：";
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOldPwd.Location = new System.Drawing.Point(128, 150);
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.PasswordChar = '*';
            this.txtOldPwd.Size = new System.Drawing.Size(271, 33);
            this.txtOldPwd.TabIndex = 26;
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNewPwd.Location = new System.Drawing.Point(128, 200);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.PasswordChar = '*';
            this.txtNewPwd.Size = new System.Drawing.Size(271, 33);
            this.txtNewPwd.TabIndex = 28;
            // 
            // txtConfirmPwd
            // 
            this.txtConfirmPwd.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtConfirmPwd.Location = new System.Drawing.Point(128, 250);
            this.txtConfirmPwd.Name = "txtConfirmPwd";
            this.txtConfirmPwd.PasswordChar = '*';
            this.txtConfirmPwd.Size = new System.Drawing.Size(271, 33);
            this.txtConfirmPwd.TabIndex = 29;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.Location = new System.Drawing.Point(128, 100);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(271, 33);
            this.txtUserName.TabIndex = 26;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.BackColor = System.Drawing.Color.Transparent;
            this.labelUserName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUserName.Location = new System.Drawing.Point(40, 104);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(88, 25);
            this.labelUserName.TabIndex = 25;
            this.labelUserName.Text = "用户名：";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg_btn2;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(169)))), ((int)(((byte)(237)))));
            this.btnCancel.Location = new System.Drawing.Point(239, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 45);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.box;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(450, 360);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.txtConfirmPwd);
            this.Controls.Add(this.txtNewPwd);
            this.Controls.Add(this.txtOldPwd);
            this.Controls.Add(this.labelConfirmPwd);
            this.Controls.Add(this.labelNewPwd);
            this.Controls.Add(this.labelOldPwd);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.labelResetPassword);
            this.Controls.Add(this.pbBtnClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangePassword";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Label labelResetPassword;
        private System.Windows.Forms.PictureBox pbBtnClose;
        private System.Windows.Forms.Label labelOldPwd;
        private System.Windows.Forms.Label labelNewPwd;
        private System.Windows.Forms.Label labelConfirmPwd;
        private System.Windows.Forms.TextBox txtOldPwd;
        private System.Windows.Forms.TextBox txtNewPwd;
        private System.Windows.Forms.TextBox txtConfirmPwd;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Button btnCancel;
    }
}