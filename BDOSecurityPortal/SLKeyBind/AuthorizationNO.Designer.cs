namespace BDOSecurityPortal
{
    partial class AuthorizationNO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizationNO));
            this.pbBtnClose = new System.Windows.Forms.PictureBox();
            this.labelBindStep3 = new System.Windows.Forms.Label();
            this.txtAuthorNO = new System.Windows.Forms.TextBox();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnLastStep = new System.Windows.Forms.Button();
            this.showMaskPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).BeginInit();
            this.showMaskPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBtnClose
            // 
            this.pbBtnClose.BackColor = System.Drawing.Color.Transparent;
            this.pbBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBtnClose.Image = global::BDOSecurityPortal.Properties.Resources.btn_close2;
            this.pbBtnClose.Location = new System.Drawing.Point(547, 31);
            this.pbBtnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbBtnClose.Name = "pbBtnClose";
            this.pbBtnClose.Size = new System.Drawing.Size(33, 31);
            this.pbBtnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbBtnClose.TabIndex = 0;
            this.pbBtnClose.TabStop = false;
            this.pbBtnClose.Click += new System.EventHandler(this.pbBtnClose_Click);
            this.pbBtnClose.MouseEnter += new System.EventHandler(this.pbBtnClose_MouseEnter);
            this.pbBtnClose.MouseLeave += new System.EventHandler(this.pbBtnClose_MouseLeave);
            // 
            // labelBindStep3
            // 
            this.labelBindStep3.AutoSize = true;
            this.labelBindStep3.BackColor = System.Drawing.Color.Transparent;
            this.labelBindStep3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBindStep3.Location = new System.Drawing.Point(116, 88);
            this.labelBindStep3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBindStep3.Name = "labelBindStep3";
            this.labelBindStep3.Size = new System.Drawing.Size(330, 33);
            this.labelBindStep3.TabIndex = 1;
            this.labelBindStep3.Text = "3、请输入授权码，生成锁号";
            // 
            // txtAuthorNO
            // 
            this.txtAuthorNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthorNO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAuthorNO.Location = new System.Drawing.Point(80, 156);
            this.txtAuthorNO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAuthorNO.Name = "txtAuthorNO";
            this.txtAuthorNO.Size = new System.Drawing.Size(438, 39);
            this.txtAuthorNO.TabIndex = 2;
            // 
            // btnComplete
            // 
            this.btnComplete.BackColor = System.Drawing.Color.Transparent;
            this.btnComplete.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg_btn1;
            this.btnComplete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnComplete.FlatAppearance.BorderSize = 0;
            this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComplete.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(313, 238);
            this.btnComplete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(155, 56);
            this.btnComplete.TabIndex = 3;
            this.btnComplete.Text = "完成";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnLastStep
            // 
            this.btnLastStep.BackColor = System.Drawing.Color.Transparent;
            this.btnLastStep.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg_btn2;
            this.btnLastStep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLastStep.FlatAppearance.BorderSize = 0;
            this.btnLastStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastStep.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLastStep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(169)))), ((int)(((byte)(237)))));
            this.btnLastStep.Location = new System.Drawing.Point(128, 238);
            this.btnLastStep.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLastStep.Name = "btnLastStep";
            this.btnLastStep.Size = new System.Drawing.Size(155, 56);
            this.btnLastStep.TabIndex = 14;
            this.btnLastStep.Text = "上一步";
            this.btnLastStep.UseVisualStyleBackColor = false;
            this.btnLastStep.Click += new System.EventHandler(this.btnLastStep_Click);
            // 
            // showMaskPanel
            // 
            this.showMaskPanel.BackColor = System.Drawing.Color.Transparent;
            this.showMaskPanel.Controls.Add(this.pictureBox1);
            this.showMaskPanel.Location = new System.Drawing.Point(0, 150);
            this.showMaskPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.showMaskPanel.Name = "showMaskPanel";
            this.showMaskPanel.Size = new System.Drawing.Size(597, 236);
            this.showMaskPanel.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(259, 79);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // AuthorizationNO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.box;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(597, 426);
            this.Controls.Add(this.showMaskPanel);
            this.Controls.Add(this.btnLastStep);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.txtAuthorNO);
            this.Controls.Add(this.labelBindStep3);
            this.Controls.Add(this.pbBtnClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AuthorizationNO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "授权编号";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).EndInit();
            this.showMaskPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBtnClose;
        private System.Windows.Forms.Label labelBindStep3;
        private System.Windows.Forms.TextBox txtAuthorNO;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnLastStep;
        private System.Windows.Forms.Panel showMaskPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}