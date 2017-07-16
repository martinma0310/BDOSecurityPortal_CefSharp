using System;
using System.Windows.Forms;
using BDOSecurityPortalModel;
using CefSharp;
using BDOSecurityPortalBLL;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Threading;

namespace BDOSecurityPortal
{
    public partial class AuthorizationNO : Form
    {
        string UserID = string.Empty;
        string UserName = string.Empty;
        string Password = string.Empty;
        string Language = ConfigurationManager.AppSettings["DEFAULT_LANGUAGE"].ToString();

        Property BindAsset = null;

        public AuthorizationNO(string userId, string userName, string password, Property bindAsset)
        {
            InitializeComponent();

            showMaskPanel.Visible = false;
            this.Refresh();

            #region 国际化

            labelBindStep3.Text = ResourceCulture.GetString("BindStep3");
            btnLastStep.Text = ResourceCulture.GetString("LastStep");
            btnComplete.Text = ResourceCulture.GetString("Complete");

            #endregion

            UserID = userId;
            UserName = userName;
            Password = password;
            BindAsset = bindAsset;
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

        private void pbBtnClose_MouseLeave(object sender, EventArgs e)
        {
            pbBtnClose.Image = Properties.Resources.btn_close2;
        }

        private void pbBtnClose_MouseEnter(object sender, EventArgs e)
        {
            pbBtnClose.Image = Properties.Resources.btn_close_hover;
        }

        private void btnLastStep_Click(object sender, EventArgs e)
        {
            this.Hide();

            PersonalAssets bindStep2Form = new PersonalAssets(UserID, UserName, Password, BindAsset);
            bindStep2Form.ShowDialog();

            this.Close();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            string authorNO = txtAuthorNO.Text.Trim();
            if (string.IsNullOrWhiteSpace(authorNO))
            {
                MessageBox.Show(ResourceCulture.GetString("BindMsg_EmptAuthorNO"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }

            try
            {
                #region 显示 Loading 等待

                showMaskPanel.Visible = true;
                this.Refresh();

                #endregion

                string slKeyId = string.Empty;
                string slKeyCode = string.Empty;
                string message = string.Empty;

                safeNetTool.safeNetBiz safetBiz = new safeNetTool.safeNetBiz();
                bool installSafeNetResult = safetBiz.installSafeNet(authorNO, out slKeyId, out message);
                if (!installSafeNetResult)
                {
                    showMaskPanel.Visible = false;
                    this.Refresh();

                    MessageBox.Show(message, ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }
                //获取密锁锁号
                List<string> slKeyCodes = (new safeNetTool.safeNetBiz()).getSlCode();
                slKeyCode = slKeyCodes.Count > 0 ? slKeyCodes[0] : string.Empty;
                bool bindResult = EoopService.BindUserAsset(slKeyCode, BindAsset.id, 1);
                if (!bindResult)
                {
                    showMaskPanel.Visible = false;
                    this.Refresh();
                    //MessageBox.Show("资产绑定失败，请重新绑定。", "提示信息");
                    MessageBox.Show(ResourceCulture.GetString("BindMsg_BindFailed"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }
                MainForm mainForm = new MainForm(UserID, UserName, Password, Language, slKeyCode);
                foreach (Form openForm in Application.OpenForms)
                {
                    openForm.Hide();
                }
                mainForm.WindowState = FormWindowState.Maximized;
                mainForm.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                showMaskPanel.Hide();
                string exMessage = ResourceCulture.GetString("GeneralMsg_AppException");
                if (Program.ReleaseType == "DEBUG")
                {
                    exMessage = ex.Message + ex.StackTrace;
                }
                MessageBox.Show(exMessage, ResourceCulture.GetString("GeneralTitle_Prompt"));
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
