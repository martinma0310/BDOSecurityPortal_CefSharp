namespace BDOSecurityPortal
{
    partial class ShowQRCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowQRCode));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelKeyCode = new System.Windows.Forms.Label();
            this.pbBtnClose = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelKeyCode);
            this.panel1.Controls.Add(this.pbBtnClose);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 318);
            this.panel1.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg_qr_code;
            this.pictureBox1.Image = global::BDOSecurityPortal.Properties.Resources.bdo_cloud_app;
            this.pictureBox1.Location = new System.Drawing.Point(55, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(229, 229);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(169)))), ((int)(((byte)(237)))));
            this.label1.Location = new System.Drawing.Point(102, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "扫一扫立即下载客户端";
            // 
            // labelKeyCode
            // 
            this.labelKeyCode.AutoSize = true;
            this.labelKeyCode.BackColor = System.Drawing.Color.Transparent;
            this.labelKeyCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelKeyCode.Location = new System.Drawing.Point(124, 30);
            this.labelKeyCode.Name = "labelKeyCode";
            this.labelKeyCode.Size = new System.Drawing.Size(93, 27);
            this.labelKeyCode.TabIndex = 13;
            this.labelKeyCode.Text = "立信APP";
            // 
            // pbBtnClose
            // 
            this.pbBtnClose.BackColor = System.Drawing.Color.Transparent;
            this.pbBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBtnClose.Image = global::BDOSecurityPortal.Properties.Resources.btn_close_hover;
            this.pbBtnClose.Location = new System.Drawing.Point(298, 15);
            this.pbBtnClose.Name = "pbBtnClose";
            this.pbBtnClose.Size = new System.Drawing.Size(25, 25);
            this.pbBtnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbBtnClose.TabIndex = 12;
            this.pbBtnClose.TabStop = false;
            this.pbBtnClose.Click += new System.EventHandler(this.pbBtnClose_Click);
            // 
            // ShowQRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.box_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(338, 318);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowQRCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowQRCode";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelKeyCode;
        private System.Windows.Forms.PictureBox pbBtnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}