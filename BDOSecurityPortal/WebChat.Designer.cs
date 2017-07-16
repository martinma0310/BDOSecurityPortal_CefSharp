namespace BDOSecurityPortal
{
    partial class WebChat
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
            this.SuspendLayout();
            // 
            // WebChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.chatTimer = new System.Windows.Forms.Timer(this.components); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 654);
            this.Name = "WebChat";
            this.Text = "WebChat";
            this.Load += new System.EventHandler(this.WebChat_Load);
            this.ResumeLayout(false);
            // 
            // chatTimer
            // 
            this.chatTimer.Enabled = true;
            this.chatTimer.Interval = 500; 
        }
        private System.Windows.Forms.Timer chatTimer; 
        #endregion
    }
}