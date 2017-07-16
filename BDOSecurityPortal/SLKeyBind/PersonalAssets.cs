using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BDOSecurityPortalBLL;
using BDOSecurityPortalModel;
using CefSharp;
using System.Drawing;
using System.Threading;

namespace BDOSecurityPortal
{
    public partial class PersonalAssets : Form
    {
        string UserID = string.Empty;
        string UserName = string.Empty;
        string Password = string.Empty;
        string Language = string.Empty;
        List<Property> PropertyList = new List<Property>();

        public PersonalAssets(string userId, string userName, string password, Property bindAsset = null)
        {
            try
            {
                InitializeComponent();

                #region 国际化

                labelBindStep2.Text = ResourceCulture.GetString("BindStep2");
                btnLastStep.Text = ResourceCulture.GetString("LastStep");
                btnNext.Text = ResourceCulture.GetString("NextStep");

                #endregion

                UserID = userId;
                UserName = userName;
                Password = password;

                PropertyList = EoopService.GetUserAssetList(userName);
                foreach (Property property in PropertyList)
                {
                    cbxAsset.Items.Add(property.ComboDisplay);
                }
                if (bindAsset != null)
                {
                    cbxAsset.SelectedText = bindAsset.ComboDisplay;
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
                MessageBox.Show(ResourceCulture.GetString("GeneralMsg_AppException"), ResourceCulture.GetString("LoginMsg_Title"));
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

            DomainAccount bindStep1Form = new DomainAccount(UserName);
            bindStep1Form.ShowDialog();

            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Property selectedProperty = null;
                foreach (Property property in PropertyList)
                {
                    if (property.ComboDisplay == cbxAsset.Text)
                    {
                        selectedProperty = property;
                        break;
                    }
                }

                if (selectedProperty == null)
                {
                    MessageBox.Show(ResourceCulture.GetString("BindMsg_EmptyAsset"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }

                if (selectedProperty.tinyintfield1 == "1")
                {
                    MessageBox.Show(ResourceCulture.GetString("BindMsg_AssetBound"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }

                this.Hide();

                AuthorizationNO bindStep3Form = new AuthorizationNO(UserID, UserName, Password, selectedProperty);
                bindStep3Form.ShowDialog();

                this.Close();
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

        #region 窗体可拖动

        private Point mPoint = new Point();
        private bool moveForm = false;

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mPoint.X = e.X;
                mPoint.Y = e.Y;
                moveForm = true;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && moveForm)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        #endregion

        private void PersonalAssets_MouseUp(object sender, MouseEventArgs e)
        {
            moveForm = false;
        }
    }
}
