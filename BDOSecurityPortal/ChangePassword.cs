using System;
using System.Windows.Forms;
using BDOSecurityPortalBLL;
using CefSharp;
using System.Drawing;
using System.Threading;

namespace BDOSecurityPortal
{
    public partial class ChangePassword : Form
    {
        bool IsDefaultSet;
        string UserName;
        string Password;
        public ChangePassword(string language, string userName = "", string password = "", bool isDefaultReset = false)
        {
            InitializeComponent();

            UserName = userName;
            Password = password;
            IsDefaultSet = isDefaultReset;

            Internationalization(language);

            if (!string.IsNullOrEmpty(userName))
            {
                txtUserName.Text = userName;
                txtUserName.ReadOnly = true;
            }
        }

        private void pbBtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsDefaultSet)//登录强制更新初始密码
                {
                    this.Close();
                    return;
                }
                else
                {
                    DialogResult result = MessageBox.Show(ResourceCulture.GetString("GeneralMsg_ExitApp"), ResourceCulture.GetString("GeneralTitle_ExitApp"), MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)//如果点击“确定”按钮
                    {
                        //Cef.RunMessageLoop();
                        Cef.Shutdown();
                        this.Hide();
                        Program.Run.Close();
                        Thread.Sleep(2000);
                        //Application.Exit();
                        Environment.Exit(0);
                    }
                    else//如果点击“取消”按钮
                    {
                        //DoNothing
                    }
                }
            }
            catch (Exception ex)
            {
                string exMessage = ResourceCulture.GetString("GeneralMsg_AppException");
                if (Program.ReleaseType == "DEBUG")
                {
                    exMessage = ex.Message + ex.StackTrace;
                }
                MessageBox.Show(exMessage, ResourceCulture.GetString("GeneralTitle_Prompt"));
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                MessageBox.Show(ResourceCulture.GetString("GeneralMsg_EmptyUser"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }
            if (string.IsNullOrEmpty(txtOldPwd.Text.Trim()))
            {
                MessageBox.Show(ResourceCulture.GetString("GeneralMsg_EmptyPwd"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }
            if (string.IsNullOrEmpty(txtNewPwd.Text.Trim()))
            {
                MessageBox.Show(ResourceCulture.GetString("ResetPwd_EmptyNewPwd"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }
            if (string.IsNullOrEmpty(txtConfirmPwd.Text.Trim()))
            {
                MessageBox.Show(ResourceCulture.GetString("ResetPwd_EmptyConfirm"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }
            if (!string.IsNullOrEmpty(Password) && txtOldPwd.Text != Password)
            {
                MessageBox.Show(ResourceCulture.GetString("ResetPwd_PwdNotMatch"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }
            if (txtNewPwd.Text != txtConfirmPwd.Text)
            {
                MessageBox.Show(ResourceCulture.GetString("ResetPwd_PwdNotMatch"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }
            string userName = txtUserName.Text;
            string oldPassword = txtOldPwd.Text;
            string newPassword = txtConfirmPwd.Text;
            string message = string.Empty;
            try
            {
                bool result = EoopService.ResetPassword(userName, oldPassword, newPassword, out message);
                if (!result)
                {
                    MessageBox.Show(message, ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }
                MessageBox.Show(ResourceCulture.GetString("ResetPwd_ResetSuccess"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                this.Close();
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.Name == "Login")
                    {
                        openForm.WindowState = FormWindowState.Normal;
                        openForm.Show();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                string exMessage = ResourceCulture.GetString("GeneralMsg_AppException");
                if (Program.ReleaseType == "DEBUG")
                {
                    exMessage = ex.Message + ex.StackTrace;
                }
                MessageBox.Show(exMessage, ResourceCulture.GetString("GeneralTitle_Prompt"));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pbBtnClose_Click(sender, e);
        }

        /// <summary>
        /// 页面国际化
        /// </summary>
        private void Internationalization(string language)
        {
            //国际化
            this.Text = ResourceCulture.GetString("AppName");
            labelResetPassword.Text = ResourceCulture.GetString("ResetPwd_Title");
            labelUserName.Location = language == "en-US" ? new Point(146, 64) : new Point(160, 64);

            labelUserName.Text = ResourceCulture.GetString("ResetPwd_UserName");
            labelUserName.Location = language =="en-US" ? new Point(6, 105) : new Point(40, 105);

            labelOldPwd.Text = ResourceCulture.GetString("ResetPwd_OldPwd");
            labelOldPwd.Location = language == "en-US" ? new Point(16, 155) : new Point(40, 155);

            labelNewPwd.Text = ResourceCulture.GetString("ResetPwd_NewPwd");
            labelNewPwd.Location = language == "en-US" ? new Point(16, 205) : new Point(40, 205);

            labelConfirmPwd.Text = ResourceCulture.GetString("ResetPwd_ConfirmPwd");
            labelConfirmPwd.Location = language == "en-US" ? new Point(26, 255) : new Point(21, 255);

            btnChangePassword.Text = ResourceCulture.GetString("ResetPwd_Submit");
            btnCancel.Text = ResourceCulture.GetString("ResetPwd_Cancel");
        }

        #region 窗体可拖动

        private Point mPoint = new Point();

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        #endregion
    }
}
