using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BDOSecurityPortalModel;
using System.Net;
using BDOSecurityPortalBLL;
using System.Configuration;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;
using CefSharp;
using BDOSecurityPortalUtil;
using System.Security.Cryptography;
using System.Text;

namespace BDOSecurityPortal
{
    //[ComVisibleAttribute(true)]
    public partial class MainForm : Form
    {
        #region 实现 任务栏 程序闪烁

        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        public const UInt32 FLASHW_STOP = 0;
        public const UInt32 FLASHW_CAPTION = 1;
        public const UInt32 FLASHW_TRAY = 2;
        public const UInt32 FLASHW_ALL = 3;
        public const UInt32 FLASHW_TIMER = 4;
        public const UInt32 FLASHW_TIMERNOFG = 12;

        //闪动并停留需要使用这个函数：
        [DllImport("user32.dll")]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [DllImport("user32.dll")]
        static extern bool FlashWindow(IntPtr handle, bool invert);

        public enum FLASH_TYPE : uint
        {
            FLASHW_STOP = 0,    //停止闪烁
            FALSHW_CAPTION = 1,  //只闪烁标题
            FLASHW_TRAY = 2,   //只闪烁任务栏
            FLASHW_ALL = 3,     //标题和任务栏同时闪烁
            FLASHW_PARAM1 = 4,
            FLASHW_PARAM2 = 12,
            FLASHW_TIMER = FLASHW_TRAY | FLASHW_PARAM1,   //无条件闪烁任务栏直到发送停止标志，停止后高亮
            FLASHW_TIMERNOFG = FLASHW_TRAY | FLASHW_PARAM2  //未激活时闪烁任务栏直到发送停止标志或者窗体被激活，停止后高亮
        }

        public static bool flashTaskBar(IntPtr hWnd, FLASH_TYPE type)
        {
            FLASHWINFO fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;//要闪烁的窗口的句柄，该窗口可以是打开的或最小化的
            fInfo.dwFlags = (uint)type;//闪烁的类型
            //fInfo.uCount = UInt32.MaxValue;//闪烁窗口的次数
            fInfo.uCount = 5;//闪烁窗口的次数
            fInfo.dwTimeout = 0; //窗口闪烁的频度，毫秒为单位；若该值为0，则为默认图标的闪烁频度
            return FlashWindowEx(ref fInfo);
        }

        private IntPtr GetChatFormHandle()
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Text.Equals("Web聊天-Eoop WebChat"))
                        return form.Handle;
                }
                return IntPtr.Zero;
            }
            catch (Exception ex) { return IntPtr.Zero; }
        }

        //未激活时闪烁任务栏直到发送停止标志或者窗体被激活，停止后高亮
        //flashTaskBar(this.Handle, falshType.FLASHW_TIMERNOFG);
        //下面的调用：停止闪烁，停止后如果未激活窗口，窗口高亮
        //flashTaskBar(this.Handle, falshType.FLASHW_STOP);

        /// <summary>
        /// 任务栏闪动
        /// </summary>
        /// <param name="msg"></param>
        //public void newMessageNotify(string msg)
        //{
        //    flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
        //    flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
        //    flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
        //    flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);

        //    //TODO: Timer 未运行
        //    try
        //    {
        //        changeStatusTimer.Enabled = true;
        //        changeStatusTimer.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //    }
        //}

        //private bool IsNewMessage = false;
        public void newMessageNotify()
        {
            //获取未读信息，实现窗体闪动 ,这一句必不可少
            CheckForIllegalCrossThreadCalls = false;
            IsNewMessage = true;
            //this.backgroundWorker1.RunWorkerAsync();  
            if(isChatFormLoseFocus)
                flashTaskBar(GetChatFormHandle(), FLASH_TYPE.FLASHW_TIMER);
            //FlashIcon();
            //changeStatusTimer.Enabled = true;
            //changeStatusTimer.Start();
        }


        /// <summary>
        /// 取消任务栏闪动
        /// </summary>
        /// <param name="msg"></param>
        public void stopNotify()
        {
            //获取未读信息，实现窗体闪动 ,这一句必不可少
            CheckForIllegalCrossThreadCalls = false;
            IsNewMessage = false;
            //this.backgroundWorker1.RunWorkerAsync();  
            flashTaskBar(GetChatFormHandle(), FLASH_TYPE.FLASHW_STOP);
            //FlashIcon(); 
        }

        #endregion

        private bool IsClose = false;
        /// <summary>
        /// 网页调用注销
        /// </summary>
        public void webLogout()
        {
            //这一句必不可少
            CheckForIllegalCrossThreadCalls = false;
            //btnLogout_Click(null, null);
            //IsWebLogout = true;
            IsClose = true;
        }

        #region FormBorderStyle = none 最大化窗体处理

        //private const long WM_GETMINMAXINFO = 0x24; 
        //public struct POINTAPI
        //{
        //    public int x;
        //    public int y;
        //}

        //public struct MINMAXINFO
        //{
        //    public POINTAPI ptReserved;
        //    public POINTAPI ptMaxSize;
        //    public POINTAPI ptMaxPosition;
        //    public POINTAPI ptMinTrackSize;
        //    public POINTAPI ptMaxTrackSize;
        //}

        /// <summary>
        /// 重绘窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void MainForm_Paint(object sender, PaintEventArgs e)
        //{
        //    Rectangle myRectangle = new Rectangle(0, 0, this.Width, this.Height);
        //    ControlPaint.DrawBorder(e.Graphics, myRectangle, Color.Blue, ButtonBorderStyle.Solid);//画个边框   
        //    ControlPaint.DrawBorder(e.Graphics, myRectangle,
        //        Color.Black, 3, ButtonBorderStyle.Solid,
        //        Color.Black, 3, ButtonBorderStyle.Solid,
        //        Color.Black, 3, ButtonBorderStyle.Solid,
        //        Color.Blue, 3, ButtonBorderStyle.Solid
        //    );
        //}

        #endregion

        int SubsystemPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["SUBSYSTEM_PAGE_SIZE"]);
        string UserName = string.Empty;                         //登录用户
        string Password = string.Empty;                         //
        string DefaultLanguage = string.Empty;                  //当前语言

        string ThisSLCode = string.Empty;                       //密锁锁号
        List<string> SLCodeList = new List<string>();           //锁号列表
        Property ThisAsset;                                     //资产信息
        HrmResource Login_HrmUser;                              //登录用户
        UserProfile Login_UserProfile;                          //登录用户详细信息

        List<Link2System> UserSubsystemList = new List<Link2System>();

        //private void ApplicationExit()
        //{
        //    //Cef.RunMessageLoop();
        //    Cef.Shutdown();
        //    this.Hide();
        //    Program.Run.Close();
        //    Thread.Sleep(2000);
        //    //Application.Exit();
        //    Environment.Exit(0);
        //}

        private void ApplicationExit()
        {
            Cef.Shutdown();
            Application.Exit();
            Environment.Exit(0);
        }


        /// <summary>
        /// 防止窗体闪烁
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }

        public MainForm(string userId, string userName, string password, string language, string slKeyCode)
        {

            try
            {
                //this.DoubleBuffered = true;
                //SetStyle(ControlStyles.UserPaint, true);
                //SetStyle(ControlStyles.AllPaintingInWmPaint, true);   // 禁止擦除背景.
                //SetStyle(ControlStyles.DoubleBuffer, true);           // 双缓冲

                //FormBorderStyle = none 最大化窗体处理：必要
                //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

                InitializeComponent();

                UserName = userName;
                Password = password;
                ThisSLCode = slKeyCode;
                SLCodeList.Add(slKeyCode);
                ThisAsset = EoopService.GetUserAsset((EoopService.GetUserSLCodes(slKeyCode)).propertyId);

                List<HrmResource> loginHrmUsers = EoopService.GetHrmResource(userName);
                if (loginHrmUsers.Count != 1)//"AD 人事系统登录用户({userName}不存在/不唯一";
                {
                    string showMsg = loginHrmUsers.Count == 0 ? string.Format(ResourceCulture.GetString("LoginMsg_NotExistUser"), userName)
                                                              : string.Format(ResourceCulture.GetString("LoginMsg_NotUniqueUser"), userName);
                    MessageBox.Show(showMsg, ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }
                Login_HrmUser = loginHrmUsers[0];
                Login_UserProfile = EoopService.GetUserProfile(userId);

                if (string.IsNullOrWhiteSpace(Login_UserProfile.loginId))
                {
                    MessageBox.Show(ResourceCulture.GetString("MainForm_NotExistLoginUser"), ResourceCulture.GetString("GeneralTitle_Prompt"));

                    CloseOpenBrowseForms();

                    //Cef.RunMessageLoop();
                    Cef.Shutdown();
                    Program.Run.Close();
                    Thread.Sleep(1000);
                    //Application.Exit();
                    Application.Restart();
                }

                //页面国际化
                Internationalization(language);

                //绑定用户相关信息
                LoadUserProfile(Login_UserProfile);

                //载入子系统列表信息
                UserSubsystemList = EoopService.GetUserSubsystemList(Login_HrmUser.userId);
                LoadUserSubsystem(UserSubsystemList);

                //获取未读信息，实现窗体闪动
                //chatTimer.Enabled = true;
                //chatTimer.Start();


                //tsBtnChat.Visible = false;

                FormSize();
                //解决Winform中鼠标滚轮无法操作Panel滚动条的问题 
                InitializePanelScroll(panelRight);
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

        public MainForm(string userId, string userName, string password, string language, string slKeyCode, List<string> slKeyCodeList, Property assetInfo, HrmResource loginHrmResource)
        {

            try
            {
                //this.DoubleBuffered = true;
                //SetStyle(ControlStyles.UserPaint, true);
                //SetStyle(ControlStyles.AllPaintingInWmPaint, true);   // 禁止擦除背景.
                //SetStyle(ControlStyles.DoubleBuffer, true);           // 双缓冲

                //FormBorderStyle = none 最大化窗体处理：必要
                //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

                InitializeComponent();
                ResetAllControlLayout();
                //存储登录用户相关信息
                ThisSLCode = slKeyCode;
                SLCodeList = slKeyCodeList;
                UserName = userName;
                Password = password;
                ThisAsset = assetInfo;
                Login_HrmUser = loginHrmResource;
                Login_UserProfile = EoopService.GetUserProfile(userId);

                //页面国际化
                Internationalization(language);

                //绑定用户相关信息
                LoadUserProfile(Login_UserProfile);

                //载入子系统列表信息
                UserSubsystemList = EoopService.GetUserSubsystemList(Login_HrmUser.userId);
                LoadUserSubsystem(UserSubsystemList);

                //获取未读信息，实现窗体闪动
                //chatTimer.Enabled = true;
                //chatTimer.Start();
                //TODO: 添加聊天功能
                //tsBtnChat.Visible = false;

                FormSize();
                //解决Winform中鼠标滚轮无法操作Panel滚动条的问题 
                InitializePanelScroll(panelRight);
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

        private void ResetAllControlLayout()
        {
            Rectangle rect = Screen.GetWorkingArea(this);
            //this.Size = new Size(rect.Width, rect.Height);
            int mainCenterHeight = panelMain.Height - toolStripBottom.Height;
            panelLeft.Location = new Point(0, 0);
            panelRight.Location = new Point(panelLeft.Width - 5, 0);

            panelLeft.Height = mainCenterHeight;
            panelRight.Height = mainCenterHeight;
            panelRight.Width = panelMain.Width - panelRight.Location.X;
        }
        /// <summary>
        /// 绑定用户相关信息
        /// </summary>
        /// <param name="userProfile"></param>
        private void LoadUserProfile(UserProfile userProfile)
        {
            if (!string.IsNullOrEmpty(Login_UserProfile.avatarURL))
            {
                try
                {
                    Uri avatarURI = new Uri(Login_UserProfile.avatarURL);
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(avatarURI);
                    request.Timeout = 5000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream resStream = response.GetResponseStream();                        //得到验证码数据流  
                    pbUserImg.Image = new Bitmap(resStream);                                //初始化Bitmap图片
                }
                catch
                {
                    if (userProfile.gender == "男")
                    {
                        pbUserImg.Image = Properties.Resources.male_1x;
                    }
                    else if (userProfile.gender == "女")
                    {
                        pbUserImg.Image = Properties.Resources.female_1x;
                    }
                }
            }
            else
            {
                if (userProfile.gender == "男")
                {
                    pbUserImg.Image = Properties.Resources.male_1x;
                }
                else if (userProfile.gender == "女")
                {
                    pbUserImg.Image = Properties.Resources.female_1x;
                }
            }

            lblEmpCname.Text = userProfile.name;
            lblEmpAdname.Text = userProfile.loginId;
            lblCityname.Text = userProfile.city;
            lblOrgName.Text = userProfile.departmentName;
            labelEmpId.Text = userProfile.no;
            lblLevel.Text = userProfile.level;
            lblSuperior.Text = userProfile.superiorName;
            lblMphone.Text = userProfile.mobile;
            lblPhone.Text = userProfile.phone;
            lblShortNum.Text = userProfile.shortNum;
            lblMail.Text = userProfile.email;
        }

        #region 页面子系统载入相关事件

        /// <summary>
        /// 载入登录用户子系统列表
        /// </summary>
        /// <param name="subsystemList"></param>
        private void LoadUserSubsystem(List<Link2System> subsystemList)
        {
            int rowCount = Convert.ToInt32(Math.Ceiling((double)SubsystemPageSize / 3.0));
            while (tlpSysList.RowStyles.Count < rowCount)
            {
                //tlpSysList.RowStyles.Insert(tlpSysList.RowStyles.Count, new RowStyle(SizeType.AutoSize, 157)); 
                tlpSysList.RowStyles.Add(new RowStyle());
            }

            //tlpSysList.Height = rowCount * 157; 
            //tlpSysList.Refresh();
            tlpSysList.AutoSize = true;

            bool shouldHidden = subsystemList.Count > SubsystemPageSize;   //隐藏部分子系统信息

            if (shouldHidden)
                SubsystemPageSize = 11;//隐藏，第12个panel放显示更多panel，如果小于等12个，则不第12个仍然放子系统图标

            foreach (Link2System subsystem in subsystemList)
            {
                int sysIndex = subsystemList.IndexOf(subsystem);
                if (shouldHidden && sysIndex == SubsystemPageSize)
                {
                    addMorePanel(tlpSysList);
                    break;
                }
                else
                {
                    //string imgURL = @"http://a.hiphotos.baidu.com/zhidao/pic/item/f9dcd100baa1cd11aa2ca018bf12c8fcc3ce2d74.jpg";
                    //Image sysImage = Image.FromStream(WebRequest.Create(imgURL).GetResponse().GetResponseStream());

                    //直接使用子系统配置的图标
                    //string imgPath = string.Format(@"\SystemImage\1.2_home\icon\ico_{0}.png", sysIndex % 9 != 0 ? sysIndex % 9 + 1 : 1); 

                    if (string.IsNullOrEmpty(subsystem.SystemIconURL))
                    {
                        //如果没有配置，则使用默认图标
                        string imgPath = string.Format(@"\SystemImage\1.2_home\icon\ico_{0}.png", sysIndex % 9 != 0 ? sysIndex % 9 + 1 : 1);
                        Image sysImage = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + imgPath);
                        addSubsystemPanel(tlpSysList, subsystem, sysImage);
                    }
                    else
                    {
                        try
                        {
                            Uri avatarURI = new Uri(subsystem.SystemIconURL);
                            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(avatarURI);
                            request.Timeout = 5000;
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            Stream resStream = response.GetResponseStream();                        //得到验证码数据流  
                            Image sysImage = new Bitmap(resStream);
                            addSubsystemPanel(tlpSysList, subsystem, sysImage);
                        }
                        catch
                        {
                            //异常处理
                            string imgPath = string.Format(@"\SystemImage\1.2_home\icon\ico_{0}.png", sysIndex % 9 != 0 ? sysIndex % 9 + 1 : 1);
                            Image sysImage = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + imgPath);
                            addSubsystemPanel(tlpSysList, subsystem, sysImage);
                        }

                    }
                }
            }
        }

        #region 解决Winform中鼠标滚轮无法操作Panel滚动条的问题 

        /// <summary>
        /// 初始化Panel
        /// </summary>
        /// <param name="panel"></param>
        public void InitializePanelScroll(Panel panel)
        {
            panel.Click += (obj, arg) => { panel.Select(); };
            InitializePanelScroll(panel, panel);
            return;
        }

        /// <summary>
        /// 递归初始化Panel内部各容器和控件
        /// </summary>
        /// <param name="container"></param>
        /// <param name="panelRoot"></param>
        private void InitializePanelScroll(Control container, Control panelRoot)
        {
            foreach (Control control in container.Controls)
            {
                if (control is Panel || control is GroupBox || control is SplitContainer ||
                    control is TabControl || control is UserControl)
                {
                    control.Click += (obj, arg) => { panelRoot.Select(); };
                    InitializePanelScroll(control, panelRoot);
                }
                else if (control is Label)
                {
                    control.Click += (obj, arg) => { panelRoot.Select(); };
                }
            }
        }

        #endregion

        /// <summary>
        /// 添加子系统至子系统展示列表
        /// </summary>
        /// <param name="tlpSystemList">tlpSystemList</param>
        /// <param name="systemName">web子系统名称</param>
        /// <param name="systemDesc">web子系统描述</param>
        /// <param name="systemImage">web子系统图标</param>
        /// <param name="systemUrl">web子系统 URL</param>
        private void addSubsystemPanel(TableLayoutPanel tlpSystemList, Link2System system, Image systemImage)
        {
            Panel systemPanel = new Panel();
            systemPanel.Dock = DockStyle.Fill;
            systemPanel.BackColor = Color.White;
            systemPanel.Tag = system.ID.ToString() + "|" + system.Keyword;
            //systemPanel.AutoSize = true;
            //systemPanel.Height = 157;

            //初始化“系统名称”标签            
            Label lblSysName = new Label();
            lblSysName.Tag = system.ID.ToString() + "|" + system.Keyword;
            lblSysName.Width = 320;
            lblSysName.Parent = systemPanel;
            lblSysName.BackColor = Color.Transparent;
            lblSysName.Text = system.Name;
            lblSysName.Font = new Font("微软雅黑", 14F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            lblSysName.Location = new Point(20, 25);
            lblSysName.Cursor = Cursors.Hand;
            lblSysName.AutoSize = true;

            //初始化“系统描述”标签
            Label lblSysDesc = new Label();
            lblSysDesc.Tag = system.ID.ToString() + "|" + system.Keyword;
            lblSysDesc.Parent = systemPanel;
            lblSysDesc.AutoSize = true;                //设置AutoSize
            //lblSysDesc.Width = 165;
            lblSysDesc.Width = 190;
            lblSysDesc.Height = 80;
            lblSysDesc.BackColor = Color.Transparent;
            lblSysDesc.ForeColor = Color.Gray;
            lblSysDesc.Text = !string.IsNullOrEmpty(system.Remark) ? system.Remark.Replace("\\r\\n", "\r\n") : string.Empty;
            lblSysDesc.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            lblSysDesc.Location = new Point(15, 65);

            //初始化“系统标记”图片
            PictureBox pbSysImg = new PictureBox();
            pbSysImg.Tag = system.ID.ToString() + "|" + system.Keyword;
            pbSysImg.Parent = systemPanel;
            pbSysImg.BackColor = Color.Transparent;
            pbSysImg.Width = 90;
            pbSysImg.Height = 90;
            pbSysImg.SizeMode = PictureBoxSizeMode.StretchImage;
            pbSysImg.AutoSize = false;
            pbSysImg.Image = systemImage;
            //pbSysImg.Location = new Point(180, 50);
            pbSysImg.Location = new Point(210, 50);

            systemPanel.Click += new EventHandler(subsystem_Click);
            lblSysName.Click += new EventHandler(subsystem_Click);
            lblSysDesc.Click += new EventHandler(subsystem_Click);
            pbSysImg.Click += new EventHandler(subsystem_Click);

            tlpSystemList.Controls.Add(systemPanel);

            lblSysName.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right)));
            lblSysDesc.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right)));
            pbSysImg.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));

            pbList.Add(pbSysImg);
        }

        private List<PictureBox> pbList = new List<PictureBox>();

        #region 缩放事件
        /// <summary>
        /// 缩放事件 neo 2017年2月20日17:53:31
        /// </summary>
        private void FormSize()
        {
            //绑定所有控件双击事件
            EventHandler handler = (o, e1) => //MessageBox.Show("鼠标双击");  
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;

                    //this.StartPosition = FormStartPosition.CenterScreen;

                    //控制子系统panel大小
                    //tlpSysList.SuspendLayout();
                    //foreach (Panel p in tlpSysList.Controls)
                    //{
                    //    p.Width = panelRight.Width / 3; 
                    //}
                    //tlpSysList.ResumeLayout();
                    //tlpSysList.Refresh(); 

                    //tlpSysList.AutoScrollMinSize = new Size(0  , Screen.PrimaryScreen.WorkingArea.Height); 

                    CenterToScreen();
                }
                else if (WindowState == FormWindowState.Normal)
                {
                    //this.Location = new Point(SystemInformation.WorkingArea.Width, 0);
                    //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                    WindowState = FormWindowState.Maximized;
                    this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                }
            };

            Action<Control.ControlCollection> addListener = null;
            addListener = (cs) =>
            {
                foreach (Control item in cs)
                {
                    //双击事件
                    item.DoubleClick += handler;
                    //增加拖动事件
                    //item.MouseDown += this.Form_MouseDown;
                    addListener(item.Controls);
                }
            };

            addListener(Controls);

            //初始化，计算任务栏
            //this.Location = new Point(SystemInformation.WorkingArea.Width, 0);
            //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height); 
            //this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;  
            //this.Top = 0;
            //this.Left = SystemInformation.WorkingArea.Width;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;    
            //任务栏大小
            //Size OutTaskBarSize = new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height); 
            //Size ScreenSize = new Size( Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height); 
            //Size TaskBarSize; 
            //TaskBarSize = new Size(
            //                (ScreenSize.Width - OutTaskBarSize.Width),
            //                (ScreenSize.Height - OutTaskBarSize.Height)
            //                );
            // this.Left = TaskBarSize.Width;
            //this.Location = new Point(78, 0);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            //计算窗体最大化后的大小
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            //this.AutoScaleMode = AutoScaleMode.Dpi;
            //设定按字体来缩放控件  
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //设定字体大小为12px       
            //this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));

        }
        #endregion


        /// <summary>
        /// 子系统 Click 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subsystem_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                string tag = ((Control)sender).Tag.ToString();
                string ID = tag.Substring(0, tag.IndexOf("|"));
                Link2System subsystem = UserSubsystemList.SingleOrDefault(s => s.ID.ToString() == ID);
                if (subsystem == null)
                {
                    MessageBox.Show(ResourceCulture.GetString("MainForm_NotExistSubsystem"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }
                GotoSubsystem(subsystem);
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

        private void addMorePanel(TableLayoutPanel tlpSystemList)
        {

            Panel morePanel = new Panel();
            morePanel.Name = "moreSystemPanel";
            morePanel.Dock = DockStyle.Fill;
            morePanel.BackgroundImage = Properties.Resources.bg_blue__h;

            //初始化“系统名称”标签            
            Label lblMore = new Label();
            lblMore.Name = "lblDisplayAll";
            lblMore.Parent = morePanel;
            //lblMore.AutoSize = false;                //设置AutoSize
            //lblMore.Width = DefaultLanguage == "zh-CN" ? 50 : 150;
            //lblMore.Height = DefaultLanguage == "zh-CN" ? 52 : 52;

            lblMore.BackColor = Color.Transparent;
            lblMore.ForeColor = Color.White;
            //lblMore.Text = ResourceCulture.GetString("DisplayAll");
            string lblText = ResourceCulture.GetString("DisplayAll");
            lblMore.Text = lblText.Substring(0, lblText.Length / 2) + "\r\n" + lblText.Substring(lblText.Length / 2);
            lblMore.Font = new Font("宋体", 14F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            //lblMore.Location = DefaultLanguage == "zh-CN" ? new Point(141, 50) : new Point(111, 50);

            morePanel.Click += new EventHandler(moreSystem_Click);
            lblMore.Click += new EventHandler(moreSystem_Click);

            tlpSystemList.Controls.Add(morePanel);
            lblMore.Size = morePanel.Size;
            lblMore.TextAlign = ContentAlignment.MiddleCenter;

            lblMore.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Bottom)));
        }

        /// <summary>
        /// 显示全部 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moreSystem_Click(object sender, EventArgs e)
        {
            try
            {
                //Control morePanel = tlpSysList.Controls.Find("moreSystemPanel", false).FirstOrDefault();
                tlpSysList.Controls.RemoveByKey("moreSystemPanel");
                //morePanel.Dispose();
               
                var subsystemList = UserSubsystemList;
                int rowCount = Convert.ToInt32(Math.Ceiling((double)subsystemList.Count() / 3.0));
                tlpSysList.Height = rowCount * 157;
                while (tlpSysList.RowStyles.Count < rowCount)
                {
                    tlpSysList.RowStyles.Insert(tlpSysList.RowStyles.Count, new RowStyle(SizeType.Absolute, 157));
                }

                if (subsystemList.Count > SubsystemPageSize)
                {
                    //this.WindowState = FormWindowState.Normal;
                    for (int sysIndex = SubsystemPageSize; sysIndex < subsystemList.Count; sysIndex++)
                    {
                        var subsystem = subsystemList[sysIndex];
                        string imgPath = string.Format(@"\SystemImage\1.2_home\icon\ico_{0}.png", sysIndex % 9 != 0 ? sysIndex % 9 + 1 : 1);
                        Image sysImage = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + imgPath);
                        addSubsystemPanel(tlpSysList, subsystem, sysImage);
                    }

                    foreach (PictureBox pb in pbList)
                    {
                        pb.Location = new Point((int)(pb.Parent.Width * 0.72), 50);
                    }
                    tlpSysList.Refresh();

                    //this.WindowState = FormWindowState.Maximized;
                }

                //显示滚动条
                //tlpSysList.AutoScrollMinSize = new Size(993, Screen.PrimaryScreen.WorkingArea.Height);  
            }
            catch (Exception ex)
            {
                string exMessage = ResourceCulture.GetString("GeneralMsg_AppException");
                exMessage = "ex.Message: " + ex.Message + "; ex.StackTrace: " + ex.StackTrace;
                MessageBox.Show(exMessage, ResourceCulture.GetString("GeneralTitle_Prompt"));
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsystem"></param>
        private void GotoSubsystem(Link2System subsystem)
        {
            string sysLoginUrl = EoopService.GetSubsystemLoginURL(UserName, Password, subsystem.Keyword, subsystem.Name, subsystem.Link);
            
            //如果是配置的IE打开，则直接使用IE
            if (subsystem.IsUseIE == 1)
            {
                //调用IE浏览器    
                System.Diagnostics.Process.Start("iexplore.exe", sysLoginUrl);
            }
            else
            {
                //string sysLoginUrl = EoopService.GetBDOSubsystemLoginURL(UserName, Password, subsystem.Keyword, subsystem.Name, subsystem.Link); 
                OpenBrowseForm(subsystem.Name, sysLoginUrl);
            }

            string logData = string.Format("BDO立信安全客户端 - 用户 {0} 登录子系统：{1}。", UserName, subsystem.Name);
            EoopService.AddBDOClientLogs(Login_HrmUser, UserName, string.Join(",", SLCodeList.ToArray()), ThisAsset.mark, "新版登录", logData);
        }

        private void OpenBrowseForm(string webPageTitle, string webPageURL, string formTag = "")
        {
            bool hasOpenForm = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (!hasOpenForm)
                {
                    hasOpenForm = openForm.Text == webPageTitle;
                }
                openForm.WindowState = hasOpenForm ? FormWindowState.Maximized : FormWindowState.Minimized;
            }

            if (!hasOpenForm)
            {
                //webPageURL = System.Web.HttpUtility.UrlEncode(webPageURL, Encoding.UTF8);
                BrowseForm browseForm = new BrowseForm(webPageTitle, webPageURL, ThisSLCode, UserName, Password);
                browseForm.Tag = !string.IsNullOrEmpty(formTag) ? formTag : string.Empty;
                browseForm.Show();
                browseForm.WindowState = FormWindowState.Maximized;     //重绘 BrowseControl 需要确定初始大小
            }
        }

        private void OpenChatBrowseForm(string webPageTitle, string webPageURL, string formTag = "")
        {
            bool hasOpenForm = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (!hasOpenForm)
                {
                    hasOpenForm = openForm.Text == webPageTitle;
                }
                openForm.WindowState = hasOpenForm ? FormWindowState.Normal : FormWindowState.Minimized;
            }

            if (!hasOpenForm)
            {
                //webPageURL = System.Web.HttpUtility.UrlEncode(webPageURL, Encoding.UTF8);
                BrowseForm browseForm = new BrowseForm(webPageTitle, webPageURL, ThisSLCode, UserName, Password, this);
                browseForm.Tag = !string.IsNullOrEmpty(formTag) ? formTag : string.Empty;
                browseForm.Icon = new Icon(Application.StartupPath + @"\SystemImage\APPChaticon.ico");
                browseForm.Width = 870;
                browseForm.Height = 654;
                //browseForm.MinimumSize = new System.Drawing.Size(860, 616);
                browseForm.Show();
                browseForm.TopMost = false;
                browseForm.BringToFront();
                //browseForm.WindowState = FormWindowState.Normal;     //重绘 BrowseControl 需要确定初始大小
            }
        }


        #region 语言切换事件

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

        /// <summary>
        /// 页面国际化
        /// </summary>
        private void Internationalization(string language)
        {
            DefaultLanguage = !string.IsNullOrEmpty(language) ? language : DefaultLanguage;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            ResourceCulture.SetCurrentCulture(language);

            //
            //TODO: CefSharp Change Language
            //s
            //Cef.Shutdown();
            //InitializeCefSharp(language);
            //

            //国际化
            this.Text = ResourceCulture.GetString("AppName");
            string userName = DefaultLanguage == "en-US" ? Login_UserProfile.loginId : Login_UserProfile.name;
            lblWelcomeMsg.Text = string.Format(ResourceCulture.GetString("WelcomeMsg"), userName);

            this.langChineseTool.ForeColor = DefaultLanguage == "en-US" ? Color.White : Color.FromArgb(192, 192, 255);
            this.langEnglishTool.ForeColor = DefaultLanguage == "en-US" ? Color.FromArgb(192, 192, 255) : Color.White;

            labelCity.Text = ResourceCulture.GetString("City");
            labelDepartment.Text = ResourceCulture.GetString("Department");
            labelEmpNum.Text = ResourceCulture.GetString("EmpNum");
            labelRank.Text = ResourceCulture.GetString("Rank");
            labelLineManager.Text = ResourceCulture.GetString("LineManager");
            labelMobile.Text = ResourceCulture.GetString("MobilePhone");
            labelTelphone.Text = ResourceCulture.GetString("Telphone");
            labelShortNum.Text = ResourceCulture.GetString("ShortNum");
            labelMail.Text = ResourceCulture.GetString("EMail");
           
            var labelCtrls = this.Controls.Find("lblDisplayAll", true);
            if (labelCtrls.Count() > 0)
            {
                var panelCtrls = this.Controls.Find("moreSystemPanel", true);
                var moreSystemPanel = panelCtrls[0] as Panel;
                Label lblMore = labelCtrls[0] as Label;
                lblMore.Text = ResourceCulture.GetString("DisplayAll");
                lblMore.Width = DefaultLanguage == "zh-CN" ? 50 : 150;
                //lblMore.Height = DefaultLanguage == "zh-CN" ? 52 : 52;
                //lblMore.Location = DefaultLanguage == "zh-CN" ? new Point(111, 50) : new Point(71, 50);
                int locationX = DefaultLanguage == "zh-CN" ? (moreSystemPanel.Width - 50) / 2 : (moreSystemPanel.Width - 150) / 2;
                lblMore.Location = new Point(locationX, 50);
            }
        }

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

        #endregion 

        bool IsLogout = false;

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult result = MessageBox.Show("请问您确定要注销用户吗？", "退出系统", MessageBoxButtons.OKCancel);
                DialogResult result = MessageBox.Show(string.Format(ResourceCulture.GetString("Msg_Logout"), UserName), ResourceCulture.GetString("MsgTitle_Logout"), MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)//如果点击“确定”按钮
                {
                    IsLogout = true;

                    #region 删除本地记录的用户信息

                    //using (FileStream fStream = new FileStream(Application.StartupPath + @"\data.bin", FileMode.Create))
                    //{
                    //    BinaryFormatter bFormat = new BinaryFormatter();
                    //    List<LogUser> users = fStream.Length > 0 ? bFormat.Deserialize(fStream) as List<LogUser> : new List<LogUser>();

                    //    var containsUsers = users.Where(u => u.Username == UserName);
                    //    if (containsUsers != null && containsUsers.Count() > 0)          //选在集合中是否存在用户名 
                    //    {
                    //        int index = users.IndexOf(containsUsers.SingleOrDefault());
                    //        users.RemoveAt(index);
                    //    }

                    //    bFormat.Serialize(fStream, users);               //要先将User类先设为可以序列化(即在类的前面加[Serializable])
                    //}

                    Properties.Settings.Default.UserName = "";
                    Properties.Settings.Default.Password = "";
                    Properties.Settings.Default.Save();
                    #endregion

                    CloseOpenBrowseForms();

                    //Cef.RunMessageLoop();
                    Cef.Shutdown();
                    Program.Run.Close();
                    Thread.Sleep(1000);
                    //Application.Exit();
                    Application.Restart();
                }
                else//如果点击“取消”按钮
                {
                    //DoNothing
                }
            }
            catch (Exception ex)
            {
                string exMessage = ResourceCulture.GetString("GeneralMsg_AppException");
                if (Program.ReleaseType == "BEBUG")
                {
                    exMessage = ex.Message + ex.StackTrace;
                }
                MessageBox.Show(exMessage, ResourceCulture.GetString("GeneralTitle_Prompt"));
            }
        }

        #region 缩小按钮相关事件 
        private void btnMinimize_MouseEnter(object sender, EventArgs e)
        {
            this.btnMinimize.Image = Properties.Resources.suoxiao_h1;
        }
        private void btnMinimize_MouseLeave(object sender, EventArgs e)
        {
            this.btnMinimize.Image = Properties.Resources.suoxiao_n1;
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            //this.ShowInTaskbar = false;
            this.notifyIcon.Icon = this.Icon;
            this.WindowState = FormWindowState.Minimized;
            //this.Hide();
        }
        #endregion 

        #region 最大化相关事件 
        /// <summary>
        /// 最大化相关事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaximum_Click(object sender, EventArgs e)
        {
            this.Focus();
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                this.btnMaximum.Image = Properties.Resources.huanyuan_b; 
            }
            else
            {
                WindowState = FormWindowState.Normal;
                this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                this.btnMaximum.Image = Properties.Resources.zuida_b;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Left = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.05);
                this.Top = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.05);
            }
        }

        private void ReloadMenuAndLeftPanel()
        {
            panelTop.Refresh();
            panel3.Refresh();
            languageTools.Refresh();
            btnLogout.Refresh();
            lblWelcomeMsg.Refresh();
            btnCloseApp.Refresh();
            btnMaximum.Refresh();
            btnMinimize.Refresh();
            panel3.BackgroundImage = Properties.Resources.bg_nav_blue;
            panelTop.BackgroundImage = Properties.Resources.bg_nav;
            panelLeft.Refresh();
            panelLeft.BackgroundImage = Properties.Resources.bg_nav;
            panel4.Refresh();
            pbUserImg.Refresh();
            panelRight.Refresh();
            tlpSysList.Refresh();
            toolStripBottom.Refresh();
        }

        private void btnMaximum_MouseLeave(object sender, EventArgs e)
        { 
            if (WindowState == FormWindowState.Normal)
            {
                this.btnMaximum.Image = Properties.Resources.zuida_b;
            }
            else
            {
                this.btnMaximum.Image = Properties.Resources.huanyuan_b;
            }
        }

        private void btnMaximum_MouseEnter(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.btnMaximum.Image = Properties.Resources.zuida;
            }
            else
            {
                this.btnMaximum.Image = Properties.Resources.huanyuan;
            }
        }

        #endregion


        #region 关闭按钮相关事件    
        private void btnCloseApp_MouseEnter(object sender, EventArgs e)
        {
            this.btnCloseApp.Image = Properties.Resources.esc_h1;
        }
        private void btnCloseApp_MouseLeave(object sender, EventArgs e)
        {
            this.btnCloseApp.Image = Properties.Resources.esc_n1;
        }
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult result = MessageBox.Show("请问您确定退出“立信安全客户端”程序吗？", "退出系统", MessageBoxButtons.OKCancel);
                DialogResult result = MessageBox.Show(ResourceCulture.GetString("GeneralMsg_ExitApp"), ResourceCulture.GetString("GeneralTitle_ExitApp"), MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)//如果点击“确定”按钮
                {
                    CloseOpenBrowseForms();
                    ApplicationExit();
                }
                else//如果点击“取消”按钮
                {
                    //DoNothing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ResourceCulture.GetString("GeneralMsg_AppException"), ResourceCulture.GetString("LoginMsg_Title"));
                ApplicationExit();
            }
        }

        #endregion

        /// <summary>
        /// 显示秘锁锁号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnKeyNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (SLCodeList.Count == 0)
                {
                    MessageBox.Show(ResourceCulture.GetString("MainForm_NotExistSLCode"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                    return;
                }
                ShowSLKeyCode showForm = new ShowSLKeyCode(SLCodeList);
                showForm.ShowDialog();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 即时聊天子系统信息查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnChat_Click(object sender, EventArgs e)
        {
            try
            {
                string webChatUrl = EoopService.GetEoopWebChatLoginURL(UserName, Password); 
                OpenChatBrowseForm("Web聊天-Eoop WebChat", webChatUrl, "Eoop_WebChat");
                //解决性能问题，使用新窗体打开
                //WebChat myWebChat = new WebChat(webChatUrl, DefaultLanguage);
                //myWebChat.Show();  

                //chatTimer.Enabled = false;
                //chatTimer.Stop();
                //changeStatusTimer.Enabled = false;
                //changeStatusTimer.Stop(); 
                IsNewMessage = false;//正常状态下不闪烁
                notifyIcon.Icon = this.Icon;
                chatNewMessageCheckTimer.Stop();
                checkNewMessageResult = false;
                isChatFormOpened = true;
                tsBtnChat.Image = Properties.Resources.chat;
                //tsBtnChat.Image = Properties.Resources.talk2;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

    //private static int inTimer = 0; //防止线程重入
    //int i = 0;//先设置一个全局变量 i ,用来控制图片索引,然后创建定时事件,双击定时控件就可以编辑 
    //private void timer1_Tick(object sender, EventArgs e)
    //{
    //    //if (Interlocked.Exchange(ref inTimer, 1) == 0)
    //    //{

    //    //如果最小化显示在任务栏，则右下角不闪烁
    //    //if (this.WindowState != FormWindowState.Normal && 
    //    //    this.WindowState != FormWindowState.Minimized && 
    //    //    IsNewMessage)

    //    //右下角闪烁
    //    if (!this.Visible && IsNewMessage)
    //    {
    //        //如果i=0则让任务栏图标变为透明的图标并且退出    
    //        if (i < 1)
    //        {
    //            this.notifyIcon.Icon = this.Icon;
    //            i++;
    //            return;
    //        }
    //        //如果i!=0,就让任务栏图标变为ico1,并将i置为0;    
    //        else
    //            this.notifyIcon.Icon = new Icon(Application.StartupPath + @"\SystemImage\setup_gray@48x48.ico");
    //        i = 0;
    //    }

    //    if (IsNewMessage)
    //    {
    //        //任务栏闪烁
    //        flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
    //    }

    //    //    Interlocked.Exchange(ref inTimer, 0);
    //    //}  
    //}



    private bool IsNewMessage = false;
        int i = 0;//先设置一个全局变量 i ,用来控制图片索引,然后创建定时事件,双击定时控件就可以编辑 
        /// <summary>
        /// 闪动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatTimer_Tick(object sender, EventArgs e)
        {
            //int contactCount = EoopService.GetUserWebContactList(UserName);
            //if (contactCount > 0)
            //{
            //    tsBtnChat.Image = Properties.Resources.talk1;
            //}
            //else
            //{
            //    tsBtnChat.Image = Properties.Resources.talk2;
            //} 

            //if ( IsNewMessage)
            //{
            //    //如果i=0则让任务栏图标变为透明的图标并且退出    
            //    if (i < 1)
            //    {
            //        this.notifyIcon.Icon = this.Icon;
            //        i++;
            //        return;
            //    }
            //    //如果i!=0,就让任务栏图标变为ico1,并将i置为0;    
            //    else
            //        this.notifyIcon.Icon = new Icon(Application.StartupPath + @"\SystemImage\setup_gray@48x48.ico");
            //    i = 0;
            //}
            if (checkNewMessageResult&& !isChatFormOpened)
                FlashChatEnter();
             
            #region 关闭聊天窗体
            if (IsClose)
            {
                //关闭CEF窗体
                int browseFormCount = 1;
                while (browseFormCount > 0)
                {
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm.Tag!=null&&
                            openForm.Tag.ToString() == "Eoop_WebChat")
                        {
                            openForm.Close();
                            browseFormCount -= 1;
                            IsClose = false;
                            //KillProcess("CefSharp.BrowserSubprocess");
                            break;
                        }
                    }
                } 
            }
            #endregion

        }

        private void KillProcess(string processName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程   
            try
            {
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(processName))
                {
                    thisproc.Kill();
                }
                //隐藏右下角icon
                //this.notifyIcon.Visible = false;
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.Message);
            }
        }


        //private bool isFlashTaskBar = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void chatTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        isFlashTaskBar = isFlashTaskBar || this.WindowState == FormWindowState.Minimized;
        //        int contactCount = CommonService.IsConnectInternet() ? EoopService.GetUserWebContactList(UserName) : 0;
        //        if (contactCount > 0)
        //        {
        //            if (!changeStatusTimer.Enabled)
        //            {
        //                changeStatusTimer.Enabled = true;
        //                changeStatusTimer.Start();
        //            }
        //        }
        //        else
        //        {
        //            isFlashTaskBar = false;
        //            tsBtnChat.Image = Properties.Resources.talk2;
        //            if (changeStatusTimer.Enabled)
        //            {
        //                changeStatusTimer.Stop();
        //            }
        //        }
        //    }
        //    catch//(Exception ex)
        //    {
        //        isFlashTaskBar = false;
        //        tsBtnChat.Image = Properties.Resources.talk1;
        //        if (changeStatusTimer.Enabled)
        //        {
        //            changeStatusTimer.Stop();
        //        }
        //    }
        //}


        /// <summary>
        /// 窗体界面里图标闪动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void changeStatusTimer_Tick(object sender, EventArgs e)
        //{
        //    tsBtnChat.Image = Convert.ToInt32(tsBtnChat.Tag) == 1 ? Properties.Resources.talk1 : Properties.Resources.talk2;
        //    tsBtnChat.Tag = Convert.ToInt32(tsBtnChat.Tag) == 1 ? 2 : 1;

        //    if (!this.Visible || this.WindowState == FormWindowState.Minimized)
        //    {
        //        bool shouldNotify = true;
        //        foreach (Form openForm in Application.OpenForms)
        //        {
        //            if (openForm.Tag != null && openForm.Tag.ToString() == "Eoop_WebChat" && openForm.WindowState != FormWindowState.Minimized)
        //            {
        //                shouldNotify = false;
        //                notifyIcon.Icon = this.Icon;
        //                break;
        //            }
        //        }

        //        if (shouldNotify)
        //        {
        //            notifyIcon.Icon = Convert.ToInt32(tsBtnChat.Tag) != 1 ? this.Icon : new Icon(Application.StartupPath + @"\SystemImage\setup_gray@48x48.ico");

        //            if (!isFlashTaskBar)
        //            {
        //                isFlashTaskBar = true;
        //                flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
        //            }
        //        }
        //    }
        //}

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                notifyMenu.Show();
            }

            if (e.Button == MouseButtons.Left)
            {
                //if (changeStatusTimer.Enabled)
                //{
                //    changeStatusTimer.Stop(); 
                //    this.ShowInTaskbar = true;
                //    this.notifyIcon.Icon = this.Icon;
                //    this.Show();
                //    this.WindowState = FormWindowState.Maximized; 
                //    //弹出聊天系统
                //    tsBtnChat_Click(null, null);
                //}
                //else
                //{
                //    this.Visible = true;
                //    this.WindowState = FormWindowState.Maximized;
                //}
            }
        }

        private void notifyMenu_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(ResourceCulture.GetString("GeneralMsg_ExitApp"), ResourceCulture.GetString("GeneralTitle_ExitApp"), MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)//如果点击“确定”按钮
                {
                    CloseOpenBrowseForms();
                    ApplicationExit();
                }
                //else//如果点击“取消”按钮
                //{
                //    //DoNothing 
                //}
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ReloadMenuAndLeftPanel();
        }

        private void CloseOpenBrowseForms()
        {
            int browseFormCount = 0;
            foreach (Form openForm in Application.OpenForms)
            {
                browseFormCount += openForm.Name == "BrowseForm" ? 1 : 0;
            }
            while (browseFormCount > 0)
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.Name == "BrowseForm")
                    {
                        openForm.Close();
                        browseFormCount -= 1;
                        break;
                    }
                }
            }
        }

        #region 窗体拖动事件 

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        //private Point mPoint = new Point();

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            //mPoint.X = e.X;
            //mPoint.Y = e.Y; 
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }


        //private void Form_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        Point myPosittion = MousePosition;
        //        myPosittion.Offset(-mPoint.X, -mPoint.Y);
        //        Location = myPosittion;
        //    }
        //}


        #endregion

        //#region 窗体可拖动

        //private Point mPoint = new Point();

        //private void Form_MouseDown(object sender, MouseEventArgs e)
        //{
        //    mPoint.X = e.X;
        //    mPoint.Y = e.Y;
        //}

        //private void Form_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        Point myPosittion = MousePosition;
        //        myPosittion.Offset(-mPoint.X, -mPoint.Y);
        //        Location = myPosittion;
        //    }
        //}
        //#endregion


        /// <summary>
        /// 打开二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnQRCode_Click(object sender, EventArgs e)
        {
            ShowQRCode showQRCode = new ShowQRCode();
            showQRCode.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {    
            if (!IsLogout)
            { 
                //不是注销，处理右键关闭，进程仍然存在问题，注销的情况下，需要切换到登录页面，不退出，
                this.FormClosing -= new FormClosingEventHandler(this.MainForm_FormClosing);//为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
                CloseOpenBrowseForms();
                ApplicationExit();
            }
        }

        public System.Windows.Forms.Timer chatNewMessageCheckTimer;
        bool checkNewMessageResult = false;
        public bool isChatFormOpened = false;
        public bool isChatFormLoseFocus = false;
        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                checkNewMessageResult = CheckNewMessage();
                chatNewMessageCheckTimer = new System.Windows.Forms.Timer();
                chatNewMessageCheckTimer.Interval = 2 * 60 * 1000;
                chatNewMessageCheckTimer.Tick += new EventHandler(chatCheckNewMessageTimer_Tick);
                chatNewMessageCheckTimer.Start();

            }
            catch(Exception ex)
            {
                
            }
        }

        delegate bool CheckNewMessageDelegate(string userName);

        private bool CheckNewMessage()
        {
            try
            {
                CheckNewMessageDelegate newMessageDelegate = new CheckNewMessageDelegate(EoopService.CheckNewMessage);
                IAsyncResult result = newMessageDelegate.BeginInvoke(UserName, null, null);
                return newMessageDelegate.EndInvoke(result);
            }
            catch (Exception ex) { return false; }
        }
        private void chatCheckNewMessageTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                checkNewMessageResult = CheckNewMessage();
            }
            catch(Exception ex)
            { }
        }

        bool flag = false;
        private void FlashChatEnter()
        {
            if(flag)
            {
                tsBtnChat.Image = Properties.Resources.chat;
                flag = false;

                this.notifyIcon.Icon = this.Icon;
            }
            else
            {
                tsBtnChat.Image = null;
                flag = true;

                this.notifyIcon.Icon = new Icon(Application.StartupPath + @"\SystemImage\setup_gray@48x48.ico");
            }


        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsBtnChat_Click(sender, e);
        }
    }
}
