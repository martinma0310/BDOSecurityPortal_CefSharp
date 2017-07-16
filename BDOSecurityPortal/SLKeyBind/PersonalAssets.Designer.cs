namespace BDOSecurityPortal
{
    partial class PersonalAssets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalAssets));
            this.labelBindStep2 = new System.Windows.Forms.Label();
            this.pbBtnClose = new System.Windows.Forms.PictureBox();
            this.btnLastStep = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.cbxAsset = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBindStep2
            // 
            this.labelBindStep2.AutoSize = true;
            this.labelBindStep2.BackColor = System.Drawing.Color.Transparent;
            this.labelBindStep2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBindStep2.Location = new System.Drawing.Point(133, 88);
            this.labelBindStep2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBindStep2.Name = "labelBindStep2";
            this.labelBindStep2.Size = new System.Drawing.Size(280, 33);
            this.labelBindStep2.TabIndex = 5;
            this.labelBindStep2.Text = "2、请选择资产进行绑定";
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
            this.btnLastStep.TabIndex = 13;
            this.btnLastStep.Text = "上一步";
            this.btnLastStep.UseVisualStyleBackColor = false;
            this.btnLastStep.Click += new System.EventHandler(this.btnLastStep_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.BackgroundImage = global::BDOSecurityPortal.Properties.Resources.bg_btn1;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(313, 238);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(155, 56);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = "下一步";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // cbxAsset
            // 
            this.cbxAsset.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cbxAsset.FormattingEnabled = true;
            this.cbxAsset.Location = new System.Drawing.Point(80, 156);
            this.cbxAsset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxAsset.Name = "cbxAsset";
            this.cbxAsset.Size = new System.Drawing.Size(439, 35);
            this.cbxAsset.TabIndex = 14;
            // 
            // PersonalAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(597, 426);
            this.Controls.Add(this.cbxAsset);
            this.Controls.Add(this.btnLastStep);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.labelBindStep2);
            this.Controls.Add(this.pbBtnClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PersonalAssets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PersonalAssets";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PersonalAssets_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelBindStep2;
        private System.Windows.Forms.PictureBox pbBtnClose;
        private System.Windows.Forms.Button btnLastStep;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ComboBox cbxAsset;
    }
}