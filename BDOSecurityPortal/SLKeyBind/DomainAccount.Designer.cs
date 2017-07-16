namespace BDOSecurityPortal
{
    partial class DomainAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DomainAccount));
            this.labelBindStep1 = new System.Windows.Forms.Label();
            this.pbBtnClose = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnNextStep = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBindStep1
            // 
            this.labelBindStep1.AutoSize = true;
            this.labelBindStep1.BackColor = System.Drawing.Color.Transparent;
            this.labelBindStep1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBindStep1.Location = new System.Drawing.Point(103, 88);
            this.labelBindStep1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBindStep1.Name = "labelBindStep1";
            this.labelBindStep1.Size = new System.Drawing.Size(355, 33);
            this.labelBindStep1.TabIndex = 5;
            this.labelBindStep1.Text = "1、请输入域账号进行资产绑定";
            // 
            // pbBtnClose
            // 
            this.pbBtnClose.BackColor = System.Drawing.Color.Transparent;
            this.pbBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBtnClose.Image = ((System.Drawing.Image)(resources.GetObject("pbBtnClose.Image")));
            this.pbBtnClose.Location = new System.Drawing.Point(547, 31);
            this.pbBtnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbBtnClose.Name = "pbBtnClose";
            this.pbBtnClose.Size = new System.Drawing.Size(33, 31);
            this.pbBtnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbBtnClose.TabIndex = 4;
            this.pbBtnClose.TabStop = false;
            this.pbBtnClose.Click += new System.EventHandler(this.pbBtnClose_Click);
            this.pbBtnClose.MouseEnter += new System.EventHandler(this.pbBtnClose_MouseEnter);
            this.pbBtnClose.MouseLeave += new System.EventHandler(this.pbBtnClose_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::BDOSecurityPortal.Properties.Resources.user;
            this.pictureBox1.Location = new System.Drawing.Point(68, 152);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(68, 214);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUser.Location = new System.Drawing.Point(107, 150);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(425, 39);
            this.txtUser.TabIndex = 8;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassword.Location = new System.Drawing.Point(107, 211);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(425, 39);
            this.txtPassword.TabIndex = 9;
            // 
            // btnNextStep
            // 
            this.btnNextStep.BackColor = System.Drawing.Color.Transparent;
            this.btnNextStep.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg_btn1;
            this.btnNextStep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNextStep.FlatAppearance.BorderSize = 0;
            this.btnNextStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextStep.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNextStep.ForeColor = System.Drawing.Color.White;
            this.btnNextStep.Location = new System.Drawing.Point(219, 288);
            this.btnNextStep.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(155, 56);
            this.btnNextStep.TabIndex = 10;
            this.btnNextStep.Text = "下一步";
            this.btnNextStep.UseVisualStyleBackColor = false;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // DomainAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(597, 426);
            this.Controls.Add(this.btnNextStep);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelBindStep1);
            this.Controls.Add(this.pbBtnClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DomainAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DomainAccount";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelBindStep1;
        private System.Windows.Forms.PictureBox pbBtnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnNextStep;
    }
}