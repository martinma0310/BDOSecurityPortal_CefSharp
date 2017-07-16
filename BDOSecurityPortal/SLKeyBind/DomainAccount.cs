using System;
using System.Windows.Forms;
using BDOSecurityPortalBLL;
using CefSharp;
using System.Drawing;
using System.Threading;

namespace BDOSecurityPortal
{
    public partial class DomainAccount : Form
    {
        public DomainAccount(string userName = "")
        {
            InitializeComponent();

            #region 国际化

            labelBindStep1.Text = ResourceCulture.GetString("BindStep1");
            btnNextStep.Text = ResourceCulture.GetString("NextStep");

            #endregion

            txtUser.Text = !string.IsNullOrEmpty(userName) ? userName : "";
        }

        private void pbBtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult result = MessageBox.Show("请问您确定退出“立信安全客户端”程序吗？", "退出系统", MessageBoxButtons.OKCancel);
                DialogResult result = MessageBox.Show(ResourceCulture.GetString("GeneralMsg_ExitApp"), ResourceCulture.GetString("GeneralTitle_ExitApp"), MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)//如果点击“确定”按钮
                {
                    //Cef.RunMessageLoop();
                    Cef.Shutdown();
                    Program.Run.Close();
                    this.Hide();
                    Thread.Sleep(2000);
                    //Application.Exit();
                    Environment.Exit(0);
                }
                else//如果点击“取消”按钮
                {
                    //DoNothing
                }
            }
            catch (Exception ex)
            {
                string exMessage = ResourceCulture.GetString("GeneralMsg_AppException");
                if (Program.ReleaseType == "DEBUG")
                {
                    exMessage = ex.Message + ex.StackTrace;
                }
                MessageBox.Show(exMessage, ResourceCulture.GetString("LoginMsg_Title"));
            }
        }

        private void pbBtnClose_MouseLeave(object sender, EventArgs e)
        {
            pbBtnClose.Image = Properties.Resources.btn_close2;
        }

        private void pbBtnClose_MouseEnter(object sender, EventArgs e)
        {
            pbBtnClose.Image = Properties.Resources.btn_close_hover;
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(userName))
            {
                //MessageBox.Show("请输入用户名。", "提示信息");
                MessageBox.Show(ResourceCulture.GetString("GeneralMsg_EmptyUser"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                //MessageBox.Show("请输入密码信息。", "提示信息");
                MessageBox.Show(ResourceCulture.GetString("GeneralMsg_EmptyPwd"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }

            try
            {
                //UserBLL userBll = new UserBLL();
                BDOSecurityPortalModel.UserProfile userProfile = new BDOSecurityPortalModel.UserProfile();
                string loginUserId = string.Empty;
                string loginMessage = string.Empty;
                bool loginResult = EoopService.LoginVerify(userName, password, out loginUserId, out loginMessage);
                if (!loginResult)
                {
                    MessageBox.Show(loginMessage, ResourceCulture.GetString("LoginMsg_Title"));
                    return;
                }

                this.Hide();
                PersonalAssets bindStep2Form = new PersonalAssets(loginUserId, userName, password);
                bindStep2Form.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                string exMessage = ResourceCulture.GetString("GeneralMsg_AppException");
                if (Program.ReleaseType == "DEBUG")
                {
                    exMessage = ex.Message + ex.StackTrace;
                }
                MessageBox.Show(exMessage, ResourceCulture.GetString("LoginMsg_Title"));
            }
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