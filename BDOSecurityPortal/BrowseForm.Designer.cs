namespace BDOSecurityPortal
{
    partial class BrowseForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseForm));
            this.browseImgList = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timerFlash = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // browseImgList
            // 
            this.browseImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("browseImgList.ImageStream")));
            this.browseImgList.TransparentColor = System.Drawing.Color.Transparent;
            this.browseImgList.Images.SetKeyName(0, "CLOSE.png");
            this.browseImgList.Images.SetKeyName(1, "CLOSE-grey.png");
            // 
            // timerFlash
            // 
            this.timerFlash.Interval = 1000;
            this.timerFlash.Tick += new System.EventHandler(this.timerFlash_Tick);
            // 
            // BrowseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BrowseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.BrowseForm_Activated);
            this.Deactivate += new System.EventHandler(this.BrowseForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BrowseForm_FormClosed);
            this.Resize += new System.EventHandler(this.BrowseForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList browseImgList;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Timer timerFlash;
    }
}