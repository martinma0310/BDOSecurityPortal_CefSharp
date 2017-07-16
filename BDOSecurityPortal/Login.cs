using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using BDOSecurityPortalBLL;
using BDOSecurityPortalModel;
using System.Configuration;
using System.Threading;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using safeNetTool;
using System.ServiceProcess;
using CefSharp;
using System.Net;

namespace BDOSecurityPortal
{
    public partial class Login : Form
    {
        string DefaultLanguage = string.Empty;
        List<string> CurrentSLCodeList = new List<string>();            //锁号列表
        string CurrentSLCode = string.Empty;                            //锁号
        Property CurrentAsset = new Property();                         //资产信息
        HrmResource CurrentAssetHrmUser = new HrmResource();            //资产所属用户
        HrmResource LoginHrmUser = new HrmResource();                   //登录用户
        private void InitializeFormInfo()
        {
            string language = ConfigurationManager.AppSettings["DEFAULT_LANGUAGE"].ToString();

            //初始化 CefSharp 浏览器内核组件
            //InitializeCefSharp(language);

            //国际化
            Internationalization(language);

            #region 获取网络背景图片

            try
            {
                string backgroundUrl = ConfigurationManager.AppSettings["BDO_FILE_SERVER_PATH"].ToString() + @"/LoginBackground/login_background.jpg";
                Uri backgroundURI = new Uri(backgroundUrl);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(backgroundURI);
                request.Timeout = 3000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();                                    //得到验证码数据流  
                this.BackgroundImage = new Bitmap(resStream);                                       //初始化Bitmap图片
            }
            catch
            {
            }

            #endregion

            labelVersion.Show();
        }
        private void ApplicationExit()
        {
            Cef.Shutdown();
            this.Hide();
            Program.Run.Close();
            Thread.Sleep(2000);
            //Application.Exit();
            Environment.Exit(0);
        }
        private void FormDisabled()
        {
            this.txtUserName.Enabled = false;
            this.txtPassword.Enabled = false;
            this.btnLogin.Enabled = false;
        }

        /// <summary>
        /// 初始化 CefSharp
        /// </summary>
        private void InitializeCefSharp(string language)
        {
            if (Cef.IsInitialized)
                return;
            var settings = new CefSettings();
            //
            //Load the pepper flash player that comes with Google Chrome - may be possible to load these values from the registry and query the dll for it's version info (Step 2 not strictly required it seems)
            //
            //Load a specific pepper flash version (Step 1 of 2)
            settings.CefCommandLineArgs.Add("ppapi-flash-path", Path.GetDirectoryName(Application.ExecutablePath) + @"\Plugins\PepperFlash\pepflashplayer.dll");
            //settings.CefCommandLineArgs.Add("ppapi-flash-path", Path.GetDirectoryName(Application.ExecutablePath) + @"\pepflashplayer.dll");
            //Load a specific pepper flash version (Step 2 of 2)
            settings.CefCommandLineArgs.Add("ppapi-flash-version", "20.0.0.228");
            //
            //Other Settings
            //
            //settings.LogSeverity = LogSeverity.Verbose;
            settings.LogSeverity = LogSeverity.Info; 
            settings.LogFile = "log.txt";

            //settings.PackLoadingDisabled = true;
            //settings.UserAgent = ""; 
            settings.IgnoreCertificateErrors = true; 
            //settings.CefCommandLineArgs.Add("debug-plugin-loading", "1");
            //settings.cefcommandlineargs.add("allow-outdated-plugins", "1");
            settings.CefCommandLineArgs.Add("always-authorize-plugins", "1");

            //设置语言
            settings.Locale = !string.IsNullOrWhiteSpace(language) ? language : "zh-CN";
            
            Cef.Initialize(settings);
        }
        /// <summary>
        /// 页面国际化
        /// </summary>
        private void Internationalization(string language)
        {
            //国际化
            DefaultLanguage = !string.IsNullOrWhiteSpace(language) ? language : "zh-CN";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            ResourceCulture.SetCurrentCulture(language);

            lblAppName.Text = ResourceCulture.GetString("AppName");
            //lblAppName.Location = language == "zh-CN" ? new Point(413, 155) : new Point(360, 155);

            this.langChineseTool.ForeColor = DefaultLanguage == "en-US" ? Color.White : Color.FromArgb(192, 192, 255);
            this.langEnglishTool.ForeColor = DefaultLanguage == "en-US" ? Color.FromArgb(192, 192, 255) : Color.White;

            btnLogin.Text = ResourceCulture.GetString("Login");
            chxUser.Text = ResourceCulture.GetString("RememberUser");

            chxPassword.Text = ResourceCulture.GetString("RememberPassword");
            //chxPassword.Location = language == "zh-CN" ? new Point(440, 485) : new Point(460, 485);

            lblBtnChangePwd.Text = ResourceCulture.GetString("ChangePassword");
            lblCopyRight.Text = ResourceCulture.GetString("CopyRight");
        }
        /// <summary>
        /// MessageBox 按钮国际化
        /// </summary>
        private void MessageBoxInternationalization()
        {
            #region MessageBox 按钮国际化

            MessageBoxEx.UnRegister();

            //Set button text from resources
            MessageBoxEx.OK = ResourceCulture.GetString("MsgBox_OK");           // LocalResource.OK;
            MessageBoxEx.Cancel = ResourceCulture.GetString("MsgBox_Cancel");   // LocalResource.Cancel;
            MessageBoxEx.Retry = ResourceCulture.GetString("MsgBox_Retry");     // LocalResource.Retry;
            MessageBoxEx.Ignore = ResourceCulture.GetString("MsgBox_Ignore");   // LocalResource.Ignore;
            MessageBoxEx.Abort = ResourceCulture.GetString("MsgBox_Abort");     // LocalResource.Abort;
            MessageBoxEx.Yes = ResourceCulture.GetString("MsgBox_Yes");         // LocalResource.Yes;
            MessageBoxEx.No = ResourceCulture.GetString("MsgBox_No");           // LocalResource.No;

            //Register manager
            MessageBoxEx.Register();

            #endregion
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        public Login()
        {
            try
            {

                InitializeComponent();

                //初始化窗体信息
                InitializeFormInfo();

                #region 验证：锁号 & 资产 & 资产绑定

                List<string> slCodeList = new List<string>();

                #region 获取资产软锁锁号信息

                try
                {
                    slCodeList = (new safeNetBiz()).getSlCode();
                }
                catch
                {
                }

                #endregion

#if DEBUG
                //slCodeList.Clear();
                //slCodeList.Add("256451793977733503");
#endif

                if (slCodeList.Count == 0)//未生成锁号，需进入资产绑定授权界面
                {
                    GotoBindAsset();
                    return;
                }

                List<UserSLCodes> userSlCodeList = new List<UserSLCodes>();
                foreach (var slcode in slCodeList)
                {
                    UserSLCodes userSLCodes = EoopService.GetUserSLCodes(slcode);
                    if (userSLCodes != null)
                        userSlCodeList.Add(userSLCodes);
                }

                //已生成锁号，未绑定资产（锁号未启用，请联系技术部）
                if (userSlCodeList.Count == 0)
                {
                    MessageBox.Show(ResourceCulture.GetString("LoginMsg_Disable"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FormDisabled();
                    return;
                }

                //检测到软锁已绑定多台资产，请联系信息技术部。
                if (userSlCodeList.Where(u => u.propertyId == userSlCodeList[0].propertyId).Count() != userSlCodeList.Count)
                {
                    MessageBox.Show(ResourceCulture.GetString("LoginMsg_MultipleAsset"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FormDisabled();
                    return;
                }

                string propertyId = userSlCodeList[0].propertyId;
                string slCode = userSlCodeList[0].slCode;

                Property currentAsset = EoopService.GetUserAsset(propertyId);
                if (currentAsset == null)
                {
                    //锁号未启用，请联系信息技术部。
                    MessageBox.Show(ResourceCulture.GetString("LoginMsg_Disable"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FormDisabled();
                    return;
                }

                #endregion

                CurrentSLCodeList = slCodeList;
                CurrentAsset = currentAsset;
                CurrentSLCode = slCodeList[0];

                string loginId = CurrentAsset != null ? CurrentAsset.loginid : "";
                List<HrmResource> thisHrmUsers = EoopService.GetHrmResource(loginId);
                if (thisHrmUsers.Count != 1)//AD 人事系统登录用户不存在/不唯一
                {
                    string showMsg = thisHrmUsers.Count == 0 ? ResourceCulture.GetString("LoginMsg_NotExistAssetUser")
                                                             : ResourceCulture.GetString("LoginMsg_NotUniqueAssetUser");
                    MessageBox.Show(showMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ApplicationExit();
                    return;
                }

                CurrentAssetHrmUser = thisHrmUsers.Count > 0 ? thisHrmUsers[0] : new HrmResource();

                #region 读取配置文件: 绑定记住的用户名和密码

                if (cbxUserName.Items.Count > 0)
                {
                    cbxUserName.Items.Clear();
                }

                //数据序列化异常，更换存储方式 neo 2017年4月19日9:55:43
                /*using (FileStream fStream = new FileStream(Application.StartupPath + @"\data.bin", FileMode.OpenOrCreate))
                {
                    if (fStream.Length > 0)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        List<LogUser> users = bf.Deserialize(fStream) as List<LogUser>;

                        #region 下拉框 - 记住密码

                        //foreach (LogUser user in users)
                        //{
                        //    this.cbxUserName.Items.Add(user.Username);
                        //}
                        //if (!string.IsNullOrWhiteSpace(cbxUserName.Text))
                        //{
                        //    var user = users.Where(u => u.Username == cbxUserName.Text).SingleOrDefault();
                        //    if (user != null)
                        //    {
                        //        this.txtPassword.Text = user.Password;
                        //        this.chxUser.Checked = true;
                        //        this.chxPassword.Checked = !string.IsNullOrEmpty(user.Password);
                        //    }
                        //}

                        #endregion

                        #region 输入框 - 记住密码

                        foreach (LogUser user in users)
                        {
                            this.txtUserName.Text = !string.IsNullOrWhiteSpace(user.Username) ? user.Username : string.Empty;
                            this.txtPassword.Text = !string.IsNullOrEmpty(user.Password) ? user.Password : string.Empty;
                            this.chxUser.Checked = !string.IsNullOrWhiteSpace(user.Username);
                            this.chxPassword.Checked = !string.IsNullOrEmpty(user.Password);
                        }

                        #endregion
                    }
                }*/


                string username = Properties.Settings.Default.UserName;
                string password = Properties.Settings.Default.Password;
                this.txtUserName.Text = !string.IsNullOrWhiteSpace(username) ? username : string.Empty;
                this.txtPassword.Text = !string.IsNullOrEmpty(password) ? password : string.Empty;
                this.chxUser.Checked = !string.IsNullOrWhiteSpace(username);
                this.chxPassword.Checked = !string.IsNullOrEmpty(password);

                #region 设置默认(第一个)登录用户

                //if (this.cbxUserName.Items.Count > 0)
                //{
                //    this.cbxUserName.SelectedIndex = this.cbxUserName.Items.Count - 1;
                //}

                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                string exMessage = ResourceCulture.GetString("GeneralMsg_AppException");
                if (Program.ReleaseType == "DEBUG")
                {
                    exMessage = ex.Message + ex.StackTrace;
                }
                MessageBox.Show(exMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                FormDisabled();
            }
        }

        /// <summary>
        /// 绑定资产
        /// </summary>
        private void GotoBindAsset()
        {
            this.Hide();
            DomainAccount bindStep1Form = new DomainAccount();
            bindStep1Form.ShowDialog();
        }

        /// <summary>
        /// 用户名输入控件-SelectedIndexChanged
        /// 废弃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (FileStream fStream = new FileStream(Application.StartupPath + @"\data.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bFormat = new BinaryFormatter();
                    List<LogUser> users = fStream.Length > 0 ? bFormat.Deserialize(fStream) as List<LogUser> : new List<LogUser>();
                    if (!string.IsNullOrWhiteSpace(this.cbxUserName.Text))
                    {
                        var user = users.Where(u => u.Username == cbxUserName.Text).SingleOrDefault();
                        if (user != null)
                        {
                            this.chxUser.Checked = true;
                            this.txtPassword.Text = user.Password;
                            this.chxPassword.Checked = !string.IsNullOrWhiteSpace(user.Password);
                        }
                        else
                        {
                            this.txtPassword.Text = "";
                            this.chxUser.Checked = false;
                            this.chxPassword.Checked = false;
                        }
                    }
                }

            }
            catch
            {
            }
        }
        /// <summary>
        /// 用户名输入控件-TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxUserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtPassword.Text = "";
                this.chxUser.Checked = false;
                this.chxPassword.Checked = false;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 记住用户复选框 Checked 变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chxUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.chxUser.Checked)
                {
                    this.chxPassword.Checked = false;
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 记住密码复选框 Checked 变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chxPassword_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chxPassword.Checked)
                {
                    this.chxUser.Checked = true;
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 登录按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region 检测网络连接是否正常

            if (!CommonService.IsConnectInternet())
            {
                string productName = ResourceCulture.GetString("AppName");  //Application.ProductName
                string exMsg = string.Format(ResourceCulture.GetString("GeneralMsg_NetworkOffline"), Application.ProductName);
                MessageBox.Show(exMsg, productName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #endregion

            //string userName = this.cbxUserName.Text.Trim();
            string userName = this.txtUserName.Text.Trim();
            string password = this.txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show(ResourceCulture.GetString("GeneralMsg_EmptyUser"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(ResourceCulture.GetString("GeneralMsg_EmptyPwd"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                return;
            }

            bool loginResult = false;           //登录结果
            string loginUserId = string.Empty;  //登录用户对应 OA 系统用户 id
            string loginMessage = string.Empty; //登录结果提示信息

            try
            {
                #region LDAP 登录&OA验证&初始密码验证

                loginResult = EoopService.LoginVerify(userName, password, out loginUserId, out loginMessage);

                if (!loginResult)
                {
                    MessageBox.Show(loginMessage, ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }

                if (loginResult && !string.IsNullOrEmpty(loginMessage) && loginMessage.Trim() == "1")//默认密码登录，强制更新密码
                {
                    this.Hide();
                    ChangePassword changePwdForm = new ChangePassword(DefaultLanguage, userName, password, true);
                    changePwdForm.ShowDialog();
                    return;
                }

                #endregion

                #region OA 系统验证登录用户信息

                List<HrmResource> loginHrmUsers = EoopService.GetHrmResource(userName);
                if (loginHrmUsers.Count != 1)//"AD 人事系统登录用户({userName}不存在/不唯一";
                {
                    string showMsg = loginHrmUsers.Count == 0 ? string.Format(ResourceCulture.GetString("LoginMsg_NotExistUser"), userName)
                                                              : string.Format(ResourceCulture.GetString("LoginMsg_NotUniqueUser"), userName);
                    MessageBox.Show(showMsg, ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }
                LoginHrmUser = loginHrmUsers[0];

                #endregion

                #region OA 系统验证资产用户信息

                //string loginId = CurrentAsset != null ? CurrentAsset.loginid : "";
                //List<HrmResource> thisHrmUsers = EoopService.GetHrmResource(loginId);
                //if (thisHrmUsers.Count != 1)//"AD 人事系统登录用户({userName}不存在/不唯一";
                //{
                //    string showMsg = thisHrmUsers.Count == 0 ? string.Format(ResourceCulture.GetString("LoginMsg_NotExistAssetUser"), userName) : string.Format(ResourceCulture.GetString("LoginMsg_NotUniqueAssetUser"), userName);
                //    MessageBox.Show(showMsg, ResourceCulture.GetString("GeneralTitle_Prompt"));
                //    return;
                //}
                //CurrentAssetHrmUser = thisHrmUsers.Count > 0 ? thisHrmUsers[0] : new HrmResource();

                #endregion

                #region 判断用户是否可使用该资产登录

                if (CurrentAsset.tinyintfield2.Trim() != "1")//非公共资产
                {
                    List<Property> loginUserPropertyList = EoopService.GetUserAssetList(userName);
                    if (loginUserPropertyList == null || loginUserPropertyList.Where(p => p.id == CurrentAsset.id).Count() == 0)//非为资产本人登录
                    {
                        //同一用户组判断
                        var groupMembers = EoopService.GetGroupMembers(CurrentAssetHrmUser.userId, LoginHrmUser.loginid);
                        var departRoles = EoopService.GetDepartRoleMapping(LoginHrmUser.departmentid);
                        if (groupMembers.Count == 0 && departRoles.Count == 0)//非资产所属用户组用户登录
                        {
                            MessageBox.Show(ResourceCulture.GetString("LoginMsg_NotSameGroup"), ResourceCulture.GetString("LoginMsg_Title"));
                            return;
                        }
                    }
                }

                #endregion

                #region 记住用户 & 记住密码

                #region 注释代码：记住多个用户

                //List<LogUser> userList = new List<LogUser>();

                //using (FileStream fStream = new FileStream("data.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                //{
                //    BinaryFormatter bFormat = new BinaryFormatter();
                //    List<LogUser> users = fStream.Length > 0 ? bFormat.Deserialize(fStream) as List<LogUser> : new List<LogUser>();
                //    if (chxUser.Checked)
                //    {
                //        LogUser user = new LogUser();
                //        user.Username = userName;
                //        user.Password = chxPassword.Checked ? password : "";        //如果单击了记住密码的功能则在文件中保存密码，否则不在文件中保存密码
                //        var containsUsers = users.Where(u => u.Username == userName);
                //        if (containsUsers != null && containsUsers.Count() > 0)   //选在集合中是否存在用户名 
                //        {
                //            int index = users.IndexOf(containsUsers.SingleOrDefault());
                //            users.RemoveAt(index);
                //        }
                //        users.Add(user);
                //    }
                //    else
                //    {
                //        var containsUsers = users.Where(u => u.Username == userName);
                //        if (containsUsers != null && containsUsers.Count() > 0)          //选在集合中是否存在用户名 
                //        {
                //            int index = users.IndexOf(containsUsers.SingleOrDefault());
                //            users.RemoveAt(index);
                //        }
                //    }
                //    userList = users;
                //}

                //using (FileStream fStream = new FileStream("data.bin", FileMode.Open, FileAccess.ReadWrite))
                //{
                //    BinaryFormatter bFormat = new BinaryFormatter();
                //    bFormat.Serialize(fStream, userList);               //要先将User类先设为可以序列化(即在类的前面加[Serializable])
                //}

                #endregion

                #region 记录最近一次登录用户

                /*using (FileStream fStream = new FileStream(Application.StartupPath + @"\data.bin", FileMode.Create))
                {
                    BinaryFormatter bFormat = new BinaryFormatter();
                    List<LogUser> users = fStream.Length > 0 ? bFormat.Deserialize(fStream) as List<LogUser> : new List<LogUser>();
                    if (chxUser.Checked)
                    {
                        LogUser user = new LogUser();
                        user.Username = userName;
                        user.Password = chxPassword.Checked ? password : "";        //如果单击了记住密码的功能则在文件中保存密码，否则不在文件中保存密码
                        var containsUsers = users.Where(u => u.Username == userName);
                        if (containsUsers != null && containsUsers.Count() > 0)   //选在集合中是否存在用户名 
                        {
                            int index = users.IndexOf(containsUsers.SingleOrDefault());
                            users.RemoveAt(index);
                        }
                        users.Add(user);
                    }
                    else
                    {
                        var containsUsers = users.Where(u => u.Username == userName);
                        if (containsUsers != null && containsUsers.Count() > 0)          //选在集合中是否存在用户名 
                        {
                            int index = users.IndexOf(containsUsers.SingleOrDefault());
                            users.RemoveAt(index);
                        }
                    }

                    bFormat.Serialize(fStream, users);               //要先将User类先设为可以序列化(即在类的前面加[Serializable])
                }*/

                if (chxUser.Checked)
                {
                    Properties.Settings.Default.UserName = userName;
                    Properties.Settings.Default.Password = password;
                }
                else
                {
                    Properties.Settings.Default.UserName = "";
                    Properties.Settings.Default.Password = "";
                }
                Properties.Settings.Default.Save();

                #endregion

                #endregion

                #region 登录，进入程序主界面 MainForm

                //if (!Cef.IsInitialized)
                //{
                //    Cef.RunMessageLoop();
                //    Cef.Shutdown();
                //}

                MainForm mainForm = new MainForm(loginUserId, userName, password, DefaultLanguage, CurrentSLCode, CurrentSLCodeList, CurrentAsset, LoginHrmUser);
                this.Hide();
                mainForm.WindowState = FormWindowState.Maximized;
                mainForm.Show();

                //初始化 CefSharp 浏览器内核组件
                InitializeCefSharp(DefaultLanguage);

                txtPassword.Text = string.Empty;
                chxUser.Checked = false;
                chxPassword.Checked = false;

                #endregion
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

        /// <summary>
        /// 窗体最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnMinimize_MouseEnter(object sender, EventArgs e)
        {
            this.btnMinimize.Image = Properties.Resources.suoxiao_h;
        }
        private void btnMinimize_MouseLeave(object sender, EventArgs e)
        {
            this.btnMinimize.Image = Properties.Resources.suoxiao_n;
        }

        /// <summary>
        /// 关闭窗体，退出应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult result = MessageBox.Show("请问您确定退出“立信安全客户端”程序吗？", "退出系统", MessageBoxButtons.OKCancel);
                DialogResult result = MessageBox.Show(ResourceCulture.GetString("GeneralMsg_ExitApp"), ResourceCulture.GetString("GeneralTitle_ExitApp"), MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)//如果点击“确定”按钮
                {
                    ApplicationExit();
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
        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            this.btnClose.Image = Properties.Resources.esc_h;
        }
        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            this.btnClose.Image = Properties.Resources.esc_n;
        }

        private void langChineseTool_Click(object sender, EventArgs e)
        {
            Internationalization("zh-CN");

            MessageBoxInternationalization();
        }

        private void langEnglishTool_Click(object sender, EventArgs e)
        {
            Internationalization("en-US");

            MessageBoxInternationalization();
        }

        private void lblBtnChangePwd_Click(object sender, EventArgs e)
        {
            ChangePassword changePwd = new ChangePassword(DefaultLanguage);
            changePwd.ShowDialog();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpLinkURL = @"http://localhost:1947/_int_/ACC_help_index.html";
            BrowseForm helpLinkForm = new BrowseForm("SafeNet Help", helpLinkURL);
            helpLinkForm.ShowDialog();
        }

        private void btnHelp_MouseEnter(object sender, EventArgs e)
        {
            //this.btnHelp.Image = Properties.Resources.help_hov;
        }

        private void btnHelp_MouseLeave(object sender, EventArgs e)
        {

            //this.btnHelp.Image = Properties.Resources.help;
        }

        #region 窗体可拖动

        private Point mPoint = new Point();

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        #endregion

        private void txtUserName_TabIndexChanged(object sender, EventArgs e)
        {

            MessageBox.Show("");
        }
    }

    [Serializable]
    public class LogUser
    {
        private string userName;

        public string Username
        {
            get { return userName; }
            set { userName = value; }
        }

        private string passWord;

        public string Password
        {
            get { return passWord; }
            set { passWord = value; }
        }
    }

    public class PaintPanel : Panel
    {
        public PaintPanel()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);   // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true);           // 双缓冲
        }
    }

    public class PaintPictureBox : PictureBox
    {
        public PaintPictureBox()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);   // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true);           // 双缓冲
        }
    }

    public class PaintTableLayoutPanel : TableLayoutPanel
    {
        public PaintTableLayoutPanel()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);   // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true);           // 双缓冲
        }
    }

    public class PaintLabel : Label
    {
        public PaintLabel()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);   // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true);           // 双缓冲
        }
    }
}
