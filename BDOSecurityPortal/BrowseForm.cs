using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CefSharp;
using CefSharp.WinForms;
using System.Text;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;

namespace BDOSecurityPortal
{
    [ComVisibleAttribute(true)]
    public partial class BrowseForm : Form
    {
        MainForm openedMainForm=null;
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
            fInfo.uCount = UInt32.MaxValue;//闪烁窗口的次数
            fInfo.dwTimeout = 0; //窗口闪烁的频度，毫秒为单位；若该值为0，则为默认图标的闪烁频度
            return FlashWindowEx(ref fInfo);
        }

        //未激活时闪烁任务栏直到发送停止标志或者窗体被激活，停止后高亮
        //flashTaskBar(this.Handle, falshType.FLASHW_TIMERNOFG);
        //下面的调用：停止闪烁，停止后如果未激活窗口，窗口高亮
        //flashTaskBar(this.Handle, falshType.FLASHW_STOP);
        /// <summary>
        /// 任务栏闪动
        /// </summary>
        /// <param name="msg"></param>
        //public void newMessageNotify(string message)
        //{
        //    flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
        //    flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
        //    flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
        //    flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);

        //    //TODO: Timer 未运行
        //    try
        //    {
        //        timerFlash.Enabled = true;
        //        timerFlash.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;
        //    }
        //}

        public void closeFormFunc()
        {
            this.Close();
        }

        private void timerFlash_Tick(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                timerFlash.Stop();
            }
            else
            {
                flashTaskBar(this.Handle, FLASH_TYPE.FLASHW_TIMERNOFG);
            }
        }

        #endregion

        string UserName = string.Empty;             //登录用户
        string Password = string.Empty;             //
        string SLKeyCode = string.Empty;            //锁号

        BrowseControl browseControl = new BrowseControl();
        ChromiumWebBrowser chromiumWebBrowser = new ChromiumWebBrowser(string.Empty);

        public BrowseForm(string webPageTitle, string webPageURL)
        {
            try
            {
                InitializeComponent();
                //
                Control.CheckForIllegalCrossThreadCalls = true;
                //

                //国际化
                this.Text = ResourceCulture.GetString("AppName");

                browseControl = new BrowseControl();
                browseControl.Dock = DockStyle.Fill;
                browseControl.ImageList = browseImgList;
                this.Controls.Add(browseControl);
                this.Text = webPageTitle;
                AddBrowserTab(browseControl, webPageTitle, webPageURL);
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

        public BrowseForm(string webPageTitle, string webPageURL, string slKeyCode, string userName, string password)
        {
            try
            {
                InitializeComponent();
                //
                Control.CheckForIllegalCrossThreadCalls = false;
                //
                UserName = userName;
                Password = password;
                SLKeyCode = slKeyCode;

                browseControl = new BrowseControl();
                browseControl.Dock = DockStyle.Fill;
                browseControl.ImageList = browseImgList;
                this.Controls.Add(browseControl);
                this.Text = webPageTitle;

                AddBrowserTab(browseControl, webPageTitle, webPageURL);

                ////国际化
                //this.Text = ResourceCulture.GetString("AppName");
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

        public BrowseForm(string webPageTitle, string webPageURL, string slKeyCode, string userName, string password,object mainform)
        {
            try
            {
                InitializeComponent();
                //
                Control.CheckForIllegalCrossThreadCalls = false;
                //
                UserName = userName;
                Password = password;
                SLKeyCode = slKeyCode;

                //browseControl = new BrowseControl();
                //browseControl.Dock = DockStyle.Fill;
                //browseControl.ImageList = browseImgList;
                //this.Controls.Add(browseControl);
                this.Text = webPageTitle; 
                //AddBrowserTab(browseControl, webPageTitle, webPageURL); 
                ////国际化
                //this.Text = ResourceCulture.GetString("AppName");

                chromiumWebBrowser = new ChromiumWebBrowser(webPageURL)
                {
                    Dock = DockStyle.Fill,
                    Text = webPageTitle,
                    Tag = webPageURL,//存储URL地址
                };

                //chromiumWebBrowser.MenuHandler = new MenuHandler();
                chromiumWebBrowser.MenuHandler = new ChatMenuHandler();
                DownloadHandler downloadHandler = new DownloadHandler();
                downloadHandler.OnBeforeDownloadFired += OnBeforeDownloadFired;
                downloadHandler.OnDownloadUpdatedFired += OnDownloadUpdatedFired;
                chromiumWebBrowser.DownloadHandler = downloadHandler;
                chromiumWebBrowser.RequestHandler = new RequestHandler();

                //注册脚本事件，用于前端调用后台方法
                chromiumWebBrowser.RegisterJsObject("cefsharpCallback", mainform);
                this.Controls.Add(chromiumWebBrowser);
                openedMainForm = (MainForm)mainform;
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

        private void AddBrowserTab(BrowseControl tabControl, string title, string url)
        {
            tabControl.SuspendLayout();
            var tabPage = new TabPage(title)
            {
                Dock = DockStyle.Fill
            };

            chromiumWebBrowser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.Fill,
                Text = title,
                Tag = url,//存储URL地址
            };

            chromiumWebBrowser.BrowserSettings.DefaultEncoding = "UTF8";
            chromiumWebBrowser.BrowserSettings.AcceptLanguageList = "zh-CN,en-US";
            chromiumWebBrowser.RequestHandler = new CefClientHandler();
            //encryptBiz ebiz = new encryptBiz();
            //this.body.Attributes.Add("onload", "LoginToOWA('mail.bdo.com.cn','bdo.com.cn','" + userName + "','" + ebiz.DecryptDES(password, null, true) + "')");

            LifeSpanHandler lifeSpanHandler = new LifeSpanHandler();
            lifeSpanHandler.PopupRequest += new Action<string, IWebBrowser>(AddBrowserTarget);

            chromiumWebBrowser.LifeSpanHandler = lifeSpanHandler;
            chromiumWebBrowser.MenuHandler = new MenuHandler();

            DownloadHandler downloadHandler = new DownloadHandler();
            downloadHandler.OnBeforeDownloadFired += OnBeforeDownloadFired;
            downloadHandler.OnDownloadUpdatedFired += OnDownloadUpdatedFired;
            chromiumWebBrowser.DownloadHandler = downloadHandler;

            JsDialogHandler jsDialogHandler = new JsDialogHandler();
            chromiumWebBrowser.JsDialogHandler = jsDialogHandler;

            KeyboardHandler keyboardHandler = new KeyboardHandler();
            chromiumWebBrowser.KeyboardHandler = keyboardHandler;


            chromiumWebBrowser.ResourceHandlerFactory = new DefaultResourceHandlerFactory();
            chromiumWebBrowser.RegisterJsObject("cefsharpCallback", this);

            //This call isn't required for the sample to work. 
            //It's sole purpose is to demonstrate that #553 has been resolved.
            //browser.CreateControl();
            //
            //chromiumWebBrowser.ShowDevTools();
            //

            tabPage.ImageIndex = 0;
            tabPage.Controls.Add(chromiumWebBrowser);
            tabControl.TabPages.Add(tabPage);

            //Make newly created tab active
            tabControl.SelectedTab = tabPage;
            tabControl.ResumeLayout(true);
        }

        public delegate void OutDelegate(BrowseControl tabControl, string title, string url);

        private void AddBrowserTarget(string url, IWebBrowser browserControl)
        {
            ChromiumWebBrowser webBrowser = (ChromiumWebBrowser)browserControl;
            TabPage tabPage = (TabPage)webBrowser.Parent;
            string title = tabPage.Text;

            if (browseControl.InvokeRequired)
            {
                OutDelegate outdelegate = new OutDelegate(AddBrowserTab);
                this.BeginInvoke(outdelegate, new object[] { browseControl, title, url });
                return;
            }
        }

        private void BrowseForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.Name == @"MainForm")
                    {
                        openForm.WindowState = FormWindowState.Maximized;
                        openForm.Show();
                        break;
                    }
                }
            }
        }

        private void BrowseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseChromeBrowser();
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == @"MainForm")
                {
                    openForm.WindowState = FormWindowState.Maximized;
                    openForm.Show();
                    break;
                }
            }
            if (openedMainForm != null)
            {
                openedMainForm.isChatFormLoseFocus = false;
                openedMainForm.isChatFormOpened = false;
                openedMainForm.chatNewMessageCheckTimer.Start();
            }
        }

        private void CloseChromeBrowser()
        {
            try
            {
                chromiumWebBrowser.CloseDevTools();
                chromiumWebBrowser.GetBrowser().CloseBrowser(true);
            }
            catch { }
        }

        private void OpenBrowseForm(string webPageTitle, string webPageURL)
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
                BrowseForm browseForm = new BrowseForm(webPageTitle, webPageURL, SLKeyCode, UserName, Password);
                browseForm.Show();
                //重绘 BrowseControl 需要确定初始大小
                browseForm.WindowState = FormWindowState.Maximized;
            }
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        private void OnBeforeDownloadFired(object sender, DownloadItem e)
        {
            //DownloadHandler downloadHandler = (DownloadHandler)sender;
            IBrowser browser = (IBrowser)sender;
            if (browser.IsPopup)
            {
                //解决下载弹出框问题 neo 2017年1月24日15:51:39
                IntPtr handle = browser.GetHost().GetWindowHandle();//得到窗口的句柄
                ShowWindow(handle, 1);
            }
            //this.UpdateDownloadAction("OnBeforeDownload", e);
        }

        private void OnDownloadUpdatedFired(object sender, DownloadItem e)
        {
            //DownloadHandler downloadHandler = (DownloadHandler)sender;
            IBrowser browser = (IBrowser)sender;
            //this.UpdateDownloadAction("OnDownloadUpdated", e);

            //
            //TODO: 显示下载进度...
            //

            if (e.IsCancelled || e.IsComplete)
            {
                if (e.IsComplete)
                {
                    string[] fileNames = e.FullPath.Split('\\');
                    string fileName = fileNames.Length > 0 ? fileNames[fileNames.Length - 1] : "";
                    string message = string.Format(ResourceCulture.GetString("Cefsharp_DownloadComplete"), fileName);
                    MessageBox.Show(message, ResourceCulture.GetString("GeneralTitle_Prompt"));
                }
                //if (browser.IsPopup)
                //{
                //    browser.CloseBrowser(true);
                //}
            }
        }

        private void BrowseForm_Activated(object sender, EventArgs e)
        {
            if(openedMainForm!=null)
                 openedMainForm.isChatFormLoseFocus = false;
        }

        private void BrowseForm_Deactivate(object sender, EventArgs e)
        {
            if (openedMainForm != null)
                openedMainForm.isChatFormLoseFocus = true;
        }
    }

    public class LifeSpanHandler : ILifeSpanHandler
    {
        public event Action<string, IWebBrowser> PopupRequest;


        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return false;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
            //NOTE: This is experimental
            if (browser.IsPopup)
            {
                var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
                chromiumWebBrowser.Invoke(new Action(() =>
                {
                    //var owner = Window.GetWindow(chromiumWebBrowser);
                    //if (owner != null && owner.Content == browserControl)
                    //{
                    //    owner.Show();
                    //}
                    Form owner = chromiumWebBrowser.FindForm();
                    if (owner != null && owner.Controls[0] == browserControl)
                    {
                        if (browser.HasDocument)
                        {
                            owner.Show();
                        }
                        else
                        {
                            //IntPtr handle = browser.GetHost().GetWindowHandle();//得到窗口的句柄
                            //ShowWindow(handle, 0);

                            //owner.WindowState = FormWindowState.Minimized;
                            owner.Hide();
                            chromiumWebBrowser.Hide();
                        }
                    }
                }));
            }
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
            try
            {
                //NOTE: This is experimental
                if (!browser.IsDisposed && browser.IsPopup)
                {
                    var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
                    chromiumWebBrowser.Invoke(new Action(() =>
                    {
                    //var owner = Window.GetWindow(chromiumWebBrowser);
                    //if (owner != null && owner.Content == browserControl)
                    //{
                    //    owner.Close();
                    //}
                    var owner = chromiumWebBrowser.FindForm();
                        if (owner != null && owner.Controls[0] == browserControl)
                        {
                            owner.Close();
                        }
                    }));
                }
            }
            catch
            {

            }
        }

        //The other members of this interface I leave empty
        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, 
            string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, 
            IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            #region Marvin's Original Code

            //if (PopupRequest != null)
            //{
            //    PopupRequest(targetUrl, browserControl);

            //    //switch (targetDisposition)
            //    //{
            //    //    case WindowOpenDisposition.NewPopup:
            //    //        //TODO: 
            //    //        break;
            //    //    default:
            //    //        PopupRequest(targetUrl, browserControl);
            //    //        break;
            //    //}
            //}

            //newBrowser = browserControl; //maybe here I think is a problem

            //return true;

            #endregion

            ChromiumWebBrowser chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
            ChromiumWebBrowser chromiumBrowser = null;

            var windowX = windowInfo.X;
            var windowY = windowInfo.Y;
            var windowWidth = (windowInfo.Width == int.MinValue) ? 600 : windowInfo.Width;
            var windowHeight = (windowInfo.Height == int.MinValue) ? 800 : windowInfo.Height;

            chromiumWebBrowser.Invoke(new Action(() =>
            {
                var owner = chromiumWebBrowser.FindForm();
                chromiumBrowser = new ChromiumWebBrowser(targetUrl)
                {
                    Dock = DockStyle.Fill,
                    LifeSpanHandler = this,
                    DownloadHandler = chromiumWebBrowser.DownloadHandler,
                    JsDialogHandler = chromiumWebBrowser.JsDialogHandler,
                    MenuHandler = chromiumWebBrowser.MenuHandler,
                    Left = windowX,
                    Top = windowY,
                    Width = windowWidth,
                    Height = windowHeight,
                    Text = targetFrameName
                };
                chromiumBrowser.BrowserSettings = chromiumWebBrowser.BrowserSettings;
                chromiumBrowser.SetAsPopup();
                var popup = new Form()
                {
                    Left = windowX,
                    Top = windowY,
                    Width = windowWidth,
                    Height = windowHeight,
                    Text = targetFrameName,
                    WindowState = FormWindowState.Maximized,
                };
                //popup.Controls.Add(new Label { Text = "CefSharp Custom Popup" });
                popup.Controls.Add(chromiumBrowser);
                owner.AddOwnedForm(popup);
                popup.Show();
            }));

            newBrowser = chromiumBrowser;

            return false;
        }
    }

    public class DownloadHandler : IDownloadHandler
    {
        public event EventHandler<DownloadItem> OnBeforeDownloadFired;

        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

        public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            var handler = OnBeforeDownloadFired;
            if (handler != null)
            {
                //handler(this, downloadItem);
                handler(browser, downloadItem);
            }

            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                }
            }
        }

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            var handler = OnDownloadUpdatedFired;
            if (handler != null)
            {
                //handler(this, downloadItem);
                handler(browser, downloadItem);
            }
        }
    }

    internal class MenuHandler : IContextMenuHandler
    {
        private const int ShowDevTools = 26501;
        private const int CloseDevTools = 26502;

        void IContextMenuHandler.OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            //To disable the menu then call clear
            // model.Clear();

            //Removing existing menu item
            //bool removed = model.Remove(CefMenuCommand.ViewSource); // Remove "View Source" option

            //Add new custom menu items
            model.AddItem((CefMenuCommand)ShowDevTools, ResourceCulture.GetString("Cefsharp_ShowDevTools"));
            model.AddItem((CefMenuCommand)CloseDevTools, ResourceCulture.GetString("Cefsharp_CloseDevTools"));
            model.AddItem(CefMenuCommand.Reload, ResourceCulture.GetString("Cefsharp_Reload"));

            //model.AddItem(CefMenuCommand.Find, "查找");
            

            //model.AddItem(CefMenuCommand.ReloadNoCache, ResourceCulture.GetString("Cefsharp_Reload"));

        }

        bool IContextMenuHandler.OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            if ((int)commandId == ShowDevTools)
            {
                browser.ShowDevTools();
            }

            if ((int)commandId == CloseDevTools)
            {
                try
                {
                    browser.CloseDevTools();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    string msg = ex.Message;
                }
            }

            if (commandId == CefMenuCommand.Reload)
            {
                //browser.Reload(); 
                var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
                string title = chromiumWebBrowser.Text;
                if (title == "项目管理系统")
                {
                    //解决重新加载刷新问题 neo 2017年1月24日15:51:39
                    string url = chromiumWebBrowser.Tag.ToString();
                    browserControl.Load(url);
                }
                else
                {
                    browser.Reload();
                }
            }

            //查找
            //if ( commandId == CefMenuCommand.Find)
            //{
            //    //browser.Find();
            //    //browser.Find(browser.Identifier, "", true, true, true);

            //    browserControl.ExecuteScriptAsync("alert('搜索')");

            //}


            //if (commandId == CefMenuCommand.ReloadNoCache)
            //{
            //browserControl.Reload();
            //string url = browser.FocusedFrame.Url;
            //browserControl.Load(url); 
            //browser.CloseBrowser(false);  
            //frame.LoadUrl(frame.Parent.Url); 
            //    frame.Parent.Browser.Reload();
            //    frame.Parent.Parent.Browser.Reload(); 
            //}

            return false;
        }

        void IContextMenuHandler.OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        bool IContextMenuHandler.RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }

    public class KeyboardHandler : IKeyboardHandler
    {
        /// <inheritdoc/>>
        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            const int WM_SYSKEYDOWN = 0x104;
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            const int WM_SYSKEYUP = 0x105;
            const int WM_CHAR = 0x102;
            const int WM_SYSCHAR = 0x106;
            const int VK_TAB = 0x9;
            const int VK_LEFT = 0x25;
            const int VK_UP = 0x26;
            const int VK_RIGHT = 0x27;
            const int VK_DOWN = 0x28;

            isKeyboardShortcut = false;

            // Don't deal with TABs by default:
            // TODO: Are there any additional ones we need to be careful of?
            // i.e. Escape, Return, etc...?
            if (windowsKeyCode == VK_TAB || windowsKeyCode == VK_LEFT || windowsKeyCode == VK_UP || windowsKeyCode == VK_DOWN || windowsKeyCode == VK_RIGHT)
            {
                return false;
            }

            var result = false;

            var control = browserControl as Control;
            var msgType = 0;
            switch (type)
            {
                case KeyType.RawKeyDown:
                    if (isSystemKey)
                    {
                        msgType = WM_SYSKEYDOWN;
                    }
                    else
                    {
                        msgType = WM_KEYDOWN;
                    }
                    break;
                case KeyType.KeyUp:
                    if (isSystemKey)
                    {
                        msgType = WM_SYSKEYUP;
                    }
                    else
                    {
                        msgType = WM_KEYUP;
                    }
                    break;
                case KeyType.Char:
                    if (isSystemKey)
                    {
                        msgType = WM_SYSCHAR;
                    }
                    else
                    {
                        msgType = WM_CHAR;
                    }
                    break;
                default:
                    Trace.Assert(false);
                    break;
            }
            // We have to adapt from CEF's UI thread message loop to our fronting WinForm control here.
            // So, we have to make some calls that Application.Run usually ends up handling for us:
            var state = PreProcessControlState.MessageNotNeeded;
            // We can't use BeginInvoke here, because we need the results for the return value
            // and isKeyboardShortcut. In theory this shouldn't deadlock, because
            // atm this is the only synchronous operation between the two threads.
            control.Invoke(new Action(() =>
            {
                var msg = new Message
                {
                    HWnd = control.Handle,
                    Msg = msgType,
                    WParam = new IntPtr(windowsKeyCode),
                    LParam = new IntPtr(nativeKeyCode)
                };

                // First comes Application.AddMessageFilter related processing:
                // 99.9% of the time in WinForms this doesn't do anything interesting.
                var processed = Application.FilterMessage(ref msg);
                if (processed)
                {
                    state = PreProcessControlState.MessageProcessed;
                }
                else
                {
                    // Next we see if our control (or one of its parents)
                    // wants first crack at the message via several possible Control methods.
                    // This includes things like Mnemonics/Accelerators/Menu Shortcuts/etc...
                    state = control.PreProcessControlMessage(ref msg);
                }
            }));

            if (state == PreProcessControlState.MessageNeeded)
            {
                // TODO: Determine how to track MessageNeeded for OnKeyEvent.
                isKeyboardShortcut = true;
            }
            else if (state == PreProcessControlState.MessageProcessed)
            {
                // Most of the interesting cases get processed by PreProcessControlMessage.
                result = true;
            }

            Debug.WriteLine("OnPreKeyEvent: KeyType: {0} 0x{1:X} Modifiers: {2}", type, windowsKeyCode, modifiers);
            Debug.WriteLine("OnPreKeyEvent PreProcessControlState: {0}", state);

            return result;
        }

        /// <inheritdoc/>>
        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            var result = false;
            Debug.WriteLine("OnKeyEvent: KeyType: {0} 0x{1:X} Modifiers: {2}", type, windowsKeyCode, modifiers);
            // TODO: Handle MessageNeeded cases here somehow.
            return result;
        }
    }

    public class JsDialogHandler : IJsDialogHandler
    {
        public bool OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
        {
            switch (dialogType)
            {
                case CefJsDialogType.Alert:
                    MessageBox.Show(messageText, ResourceCulture.GetString("GeneralTitle_Prompt"));
                    suppressMessage = true;
                    return false;
                    break;
                case CefJsDialogType.Confirm:
                    var dr = MessageBox.Show(messageText, ResourceCulture.GetString("GeneralTitle_Prompt"), MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        callback.Continue(true, string.Empty);
                        suppressMessage = false;
                        return true;
                    }
                    else
                    {
                        callback.Continue(false, string.Empty);
                        suppressMessage = false;
                        return true;
                    }
                    break;
                case CefJsDialogType.Prompt:
                    MessageBox.Show("系统不支持prompt形式的提示框", "UTMP系统提示");
                    break;
            }
            suppressMessage = true;
            return false;
        }

        public bool OnJSBeforeUnload(IWebBrowser browserControl, IBrowser browser, string message, bool isReload, IJsDialogCallback callback)
        {
            //NOTE: No need to execute the callback if you return false
            // callback.Continue(true);

            //NOTE: Returning false will trigger the default behaviour, you need to return true to handle yourself.
            return false;
        }

        public void OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public void OnDialogClosed(IWebBrowser browserControl, IBrowser browser)
        {

        }
    }

    public class ResourceHandler : IResourceHandler
    {
        /// <summary>
        /// MimeType to be used if none provided
        /// </summary>
        private const string DefaultMimeType = "text/html";

        /// <summary>
        /// Path of the underlying file
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Gets or sets the Mime Type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the resource stream.
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// Gets or sets the http status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        public string StatusText { get; set; }

        /// <summary>
        /// Gets or sets ResponseLength, when you know the size of your
        /// Stream (Response) set this property. This is optional.
        /// If you use a MemoryStream and don't provide a value
        /// here then it will be cast and it's size used
        /// </summary>
        public long? ResponseLength { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        /// <value>The headers.</value>
        public NameValueCollection Headers { get; private set; }

        /// <summary>
        /// Specify which type of resource handle represnets
        /// </summary>
        public ResourceHandlerType Type { get; private set; }

        /// <summary>
        /// When true the Stream will be Disposed when
        /// this instance is Disposed. The default value for
        /// this property is false.
        /// </summary>
        public bool AutoDisposeStream { get; set; }

        /// <summary>
        /// If the ErrorCode is set then the response will be ignored and
        /// the errorCode returned.
        /// </summary>
        public CefErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ResourceHandler() : this(DefaultMimeType, ResourceHandlerType.Stream)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceHandler"/> class.
        /// </summary>
        private ResourceHandler(string mimeType, ResourceHandlerType type)
        {
            if (string.IsNullOrEmpty(mimeType))
            {
                throw new ArgumentNullException("mimeType", "Please provide a valid mimeType");
            }

            StatusCode = 200;
            StatusText = "OK";
            MimeType = mimeType;
            Headers = new NameValueCollection();
            Type = type;
        }

        /// <summary>
        /// Begin processing the request. If you have the data in memory you can execute the callback
        /// immediately and return true. For Async processing you would typically spawn a Task to perform processing,
        /// then return true. When the processing is complete execute callback.Continue(); In your processing Task, simply set
        /// the StatusCode, StatusText, MimeType, ResponseLength and Stream
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="callback">The callback used to Continue or Cancel the request (async).</param>
        /// <returns>To handle the request return true and call
        /// <see cref="ICallback.Continue"/> once the response header information is available
        /// <see cref="ICallback.Continue"/> can also be called from inside this method if
        /// header information is available immediately).
        /// To cancel the request return false.</returns>
        public virtual bool ProcessRequestAsync(IRequest request, ICallback callback)
        {
            callback.Continue();

            return true;
        }

        /// <summary>
        /// Populate the response stream, response length. When this method is called
        /// the response should be fully populated with data.
        /// It is possible to redirect to another url at this point in time.
        /// NOTE: It's no longer manditory to implement this method, you can simply populate the
        /// properties of this instance and they will be set by the default implementation. 
        /// </summary>
        /// <param name="response">The response object used to set Headers, StatusCode, etc</param>
        /// <param name="responseLength">length of the response</param>
        /// <param name="redirectUrl">If set the request will be redirect to specified Url</param>
        /// <returns>The response stream</returns>
        public virtual Stream GetResponse(IResponse response, out long responseLength, out string redirectUrl)
        {
            redirectUrl = null;
            responseLength = -1;

            response.MimeType = MimeType;
            response.StatusCode = StatusCode;
            response.StatusText = StatusText;
            response.ResponseHeaders = Headers;

            if (ResponseLength.HasValue)
            {
                responseLength = ResponseLength.Value;
            }
            else
            {
                //If no ResponseLength provided then attempt to infer the length
                var memoryStream = Stream as MemoryStream;
                if (memoryStream != null)
                {
                    responseLength = memoryStream.Length;
                }
            }

            return Stream;
        }

        /// <summary>
        /// Called if the request is cancelled
        /// </summary>
        public virtual void Cancel()
        {

        }

        bool IResourceHandler.ProcessRequest(IRequest request, ICallback callback)
        {
            return ProcessRequestAsync(request, callback);
        }

        void IResourceHandler.GetResponseHeaders(IResponse response, out long responseLength, out string redirectUrl)
        {
            if (ErrorCode.HasValue)
            {
                responseLength = 0;
                redirectUrl = null;
                response.ErrorCode = ErrorCode.Value;
            }
            else
            {
                Stream = GetResponse(response, out responseLength, out redirectUrl);

                if (Stream != null && Stream.CanSeek)
                {
                    //Reset the stream position to 0
                    Stream.Position = 0;
                }
            }
        }

        bool IResourceHandler.ReadResponse(Stream dataOut, out int bytesRead, ICallback callback)
        {
            //We don't need the callback, as it's an unmanaged resource we should dispose it (could wrap it in a using statement).
            callback.Dispose();

            if (Stream == null)
            {
                bytesRead = 0;

                return false;
            }

            //Data out represents an underlying buffer (typically 32kb in size).
            var buffer = new byte[dataOut.Length];
            bytesRead = Stream.Read(buffer, 0, buffer.Length);

            dataOut.Write(buffer, 0, buffer.Length);

            return bytesRead > 0;
        }

        bool IResourceHandler.CanGetCookie(Cookie cookie)
        {
            return true;
        }

        bool IResourceHandler.CanSetCookie(Cookie cookie)
        {
            return true;
        }

        void IResourceHandler.Cancel()
        {
            Cancel();

            Stream = null;
        }

        /// <summary>
        /// Gets the resource from the file.
        /// </summary>
        /// <param name="filePath">Location of the file.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>ResourceHandler.</returns>
        [Obsolete("Use FromFilePath instead - to get the mimeType use the GetMimeType helper method")]
        public static ResourceHandler FromFileName(string filePath, string fileExtension = null)
        {
            var mimeType = string.IsNullOrEmpty(fileExtension) ? DefaultMimeType : GetMimeType(fileExtension);

            return FromFilePath(filePath, mimeType);
        }

        /// <summary>
        /// Gets the resource from the file path specified. Use the <see cref="ResourceHandler.GetMimeType"/>
        /// helper method to lookup the mimeType if required.
        /// </summary>
        /// <param name="fileName">Location of the file.</param>
        /// <param name="mimeType">The mimeType if null then text/html is used.</param>
        /// <returns>ResourceHandler.</returns>
        public static ResourceHandler FromFilePath(string fileName, string mimeType = null)
        {
            return new ResourceHandler(mimeType ?? DefaultMimeType, ResourceHandlerType.File) { FilePath = fileName };
        }

        /// <summary>
        /// Gets the resource from the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>ResourceHandler.</returns>
        public static ResourceHandler FromString(string text, string fileExtension)
        {
            var mimeType = GetMimeType(fileExtension);
            return FromString(text, Encoding.UTF8, false, mimeType);
        }

        /// <summary>
        /// Gets a <see cref="ResourceHandler"/> that represents a string.
        /// Without a Preamble, Cef will use BrowserSettings.DefaultEncoding to load the html.
        /// </summary>
        /// <param name="text">The html string</param>
        /// <param name="encoding">Character Encoding</param>
        /// <param name="includePreamble">Include encoding preamble</param>
        /// <param name="mimeType">Mime Type</param>
        /// <returns>ResourceHandler</returns>
        public static ResourceHandler FromString(string text, Encoding encoding = null, bool includePreamble = true, string mimeType = DefaultMimeType)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return new ResourceHandler(mimeType, ResourceHandlerType.Stream) { Stream = GetStream(text, encoding, includePreamble) };
        }

        /// <summary>
        /// Gets the resource from a stream.
        /// </summary>
        /// <param name="stream">A stream of the resource.</param>
        /// <param name="mimeType">Type of MIME.</param>
        /// <returns>ResourceHandler.</returns>
        public static ResourceHandler FromStream(Stream stream, string mimeType = DefaultMimeType)
        {
            return new ResourceHandler(mimeType, ResourceHandlerType.Stream) { Stream = stream };
        }

        private static MemoryStream GetStream(string text, Encoding encoding, bool includePreamble)
        {
            if (includePreamble)
            {
                var preamble = encoding.GetPreamble();
                var bytes = encoding.GetBytes(text);

                var memoryStream = new MemoryStream(preamble.Length + bytes.Length);

                memoryStream.Write(preamble, 0, preamble.Length);
                memoryStream.Write(bytes, 0, bytes.Length);

                memoryStream.Position = 0;

                return memoryStream;
            }

            return new MemoryStream(encoding.GetBytes(text));
        }

        //TODO: Replace with call to CefGetMimeType (little difficult at the moment with no access to the CefSharp.Core class from here)
        private static readonly IDictionary<string, string> Mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            // Combination of values from Windows 7 Registry and  C:\Windows\System32\inetsrv\config\applicationHost.config
            {".323", "text/h323"},
            {".3g2", "video/3gpp2"},
            {".3gp", "video/3gpp"},
            {".3gp2", "video/3gpp2"},
            {".3gpp", "video/3gpp"},
            {".7z", "application/x-7z-compressed"},
            {".aa", "audio/audible"},
            {".AAC", "audio/aac"},
            {".aaf", "application/octet-stream"},
            {".aax", "audio/vnd.audible.aax"},
            {".ac3", "audio/ac3"},
            {".aca", "application/octet-stream"},
            {".accda", "application/msaccess.addin"},
            {".accdb", "application/msaccess"},
            {".accdc", "application/msaccess.cab"},
            {".accde", "application/msaccess"},
            {".accdr", "application/msaccess.runtime"},
            {".accdt", "application/msaccess"},
            {".accdw", "application/msaccess.webapplication"},
            {".accft", "application/msaccess.ftemplate"},
            {".acx", "application/internet-property-stream"},
            {".AddIn", "text/xml"},
            {".ade", "application/msaccess"},
            {".adobebridge", "application/x-bridge-url"},
            {".adp", "application/msaccess"},
            {".ADT", "audio/vnd.dlna.adts"},
            {".ADTS", "audio/aac"},
            {".afm", "application/octet-stream"},
            {".ai", "application/postscript"},
            {".aif", "audio/x-aiff"},
            {".aifc", "audio/aiff"},
            {".aiff", "audio/aiff"},
            {".air", "application/vnd.adobe.air-application-installer-package+zip"},
            {".amc", "application/x-mpeg"},
            {".application", "application/x-ms-application"},
            {".art", "image/x-jg"},
            {".asa", "application/xml"},
            {".asax", "application/xml"},
            {".ascx", "application/xml"},
            {".asd", "application/octet-stream"},
            {".asf", "video/x-ms-asf"},
            {".ashx", "application/xml"},
            {".asi", "application/octet-stream"},
            {".asm", "text/plain"},
            {".asmx", "application/xml"},
            {".aspx", "application/xml"},
            {".asr", "video/x-ms-asf"},
            {".asx", "video/x-ms-asf"},
            {".atom", "application/atom+xml"},
            {".au", "audio/basic"},
            {".avi", "video/x-msvideo"},
            {".axs", "application/olescript"},
            {".bas", "text/plain"},
            {".bcpio", "application/x-bcpio"},
            {".bin", "application/octet-stream"},
            {".bmp", "image/bmp"},
            {".c", "text/plain"},
            {".cab", "application/octet-stream"},
            {".caf", "audio/x-caf"},
            {".calx", "application/vnd.ms-office.calx"},
            {".cat", "application/vnd.ms-pki.seccat"},
            {".cc", "text/plain"},
            {".cd", "text/plain"},
            {".cdda", "audio/aiff"},
            {".cdf", "application/x-cdf"},
            {".cer", "application/x-x509-ca-cert"},
            {".chm", "application/octet-stream"},
            {".class", "application/x-java-applet"},
            {".clp", "application/x-msclip"},
            {".cmx", "image/x-cmx"},
            {".cnf", "text/plain"},
            {".cod", "image/cis-cod"},
            {".config", "application/xml"},
            {".contact", "text/x-ms-contact"},
            {".coverage", "application/xml"},
            {".cpio", "application/x-cpio"},
            {".cpp", "text/plain"},
            {".crd", "application/x-mscardfile"},
            {".crl", "application/pkix-crl"},
            {".crt", "application/x-x509-ca-cert"},
            {".cs", "text/plain"},
            {".csdproj", "text/plain"},
            {".csh", "application/x-csh"},
            {".csproj", "text/plain"},
            {".css", "text/css"},
            {".csv", "text/csv"},
            {".cur", "application/octet-stream"},
            {".cxx", "text/plain"},
            {".dat", "application/octet-stream"},
            {".datasource", "application/xml"},
            {".dbproj", "text/plain"},
            {".dcr", "application/x-director"},
            {".def", "text/plain"},
            {".deploy", "application/octet-stream"},
            {".der", "application/x-x509-ca-cert"},
            {".dgml", "application/xml"},
            {".dib", "image/bmp"},
            {".dif", "video/x-dv"},
            {".dir", "application/x-director"},
            {".disco", "text/xml"},
            {".dll", "application/x-msdownload"},
            {".dll.config", "text/xml"},
            {".dlm", "text/dlm"},
            {".doc", "application/msword"},
            {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".dot", "application/msword"},
            {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
            {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
            {".dsp", "application/octet-stream"},
            {".dsw", "text/plain"},
            {".dtd", "text/xml"},
            {".dtsConfig", "text/xml"},
            {".dv", "video/x-dv"},
            {".dvi", "application/x-dvi"},
            {".dwf", "drawing/x-dwf"},
            {".dwp", "application/octet-stream"},
            {".dxr", "application/x-director"},
            {".eml", "message/rfc822"},
            {".emz", "application/octet-stream"},
            {".eot", "application/octet-stream"},
            {".eps", "application/postscript"},
            {".etl", "application/etl"},
            {".etx", "text/x-setext"},
            {".evy", "application/envoy"},
            {".exe", "application/octet-stream"},
            {".exe.config", "text/xml"},
            {".fdf", "application/vnd.fdf"},
            {".fif", "application/fractals"},
            {".filters", "Application/xml"},
            {".fla", "application/octet-stream"},
            {".flr", "x-world/x-vrml"},
            {".flv", "video/x-flv"},
            {".fsscript", "application/fsharp-script"},
            {".fsx", "application/fsharp-script"},
            {".generictest", "application/xml"},
            {".gif", "image/gif"},
            {".group", "text/x-ms-group"},
            {".gsm", "audio/x-gsm"},
            {".gtar", "application/x-gtar"},
            {".gz", "application/x-gzip"},
            {".h", "text/plain"},
            {".hdf", "application/x-hdf"},
            {".hdml", "text/x-hdml"},
            {".hhc", "application/x-oleobject"},
            {".hhk", "application/octet-stream"},
            {".hhp", "application/octet-stream"},
            {".hlp", "application/winhlp"},
            {".hpp", "text/plain"},
            {".hqx", "application/mac-binhex40"},
            {".hta", "application/hta"},
            {".htc", "text/x-component"},
            {".htm", "text/html"},
            {".html", "text/html"},
            {".htt", "text/webviewhtml"},
            {".hxa", "application/xml"},
            {".hxc", "application/xml"},
            {".hxd", "application/octet-stream"},
            {".hxe", "application/xml"},
            {".hxf", "application/xml"},
            {".hxh", "application/octet-stream"},
            {".hxi", "application/octet-stream"},
            {".hxk", "application/xml"},
            {".hxq", "application/octet-stream"},
            {".hxr", "application/octet-stream"},
            {".hxs", "application/octet-stream"},
            {".hxt", "text/html"},
            {".hxv", "application/xml"},
            {".hxw", "application/octet-stream"},
            {".hxx", "text/plain"},
            {".i", "text/plain"},
            {".ico", "image/x-icon"},
            {".ics", "application/octet-stream"},
            {".idl", "text/plain"},
            {".ief", "image/ief"},
            {".iii", "application/x-iphone"},
            {".inc", "text/plain"},
            {".inf", "application/octet-stream"},
            {".inl", "text/plain"},
            {".ins", "application/x-internet-signup"},
            {".ipa", "application/x-itunes-ipa"},
            {".ipg", "application/x-itunes-ipg"},
            {".ipproj", "text/plain"},
            {".ipsw", "application/x-itunes-ipsw"},
            {".iqy", "text/x-ms-iqy"},
            {".isp", "application/x-internet-signup"},
            {".ite", "application/x-itunes-ite"},
            {".itlp", "application/x-itunes-itlp"},
            {".itms", "application/x-itunes-itms"},
            {".itpc", "application/x-itunes-itpc"},
            {".IVF", "video/x-ivf"},
            {".jar", "application/java-archive"},
            {".java", "application/octet-stream"},
            {".jck", "application/liquidmotion"},
            {".jcz", "application/liquidmotion"},
            {".jfif", "image/pjpeg"},
            {".jnlp", "application/x-java-jnlp-file"},
            {".jpb", "application/octet-stream"},
            {".jpe", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".js", "application/x-javascript"},
            {".json", "application/json"},
            {".jsx", "text/jscript"},
            {".jsxbin", "text/plain"},
            {".latex", "application/x-latex"},
            {".library-ms", "application/windows-library+xml"},
            {".lit", "application/x-ms-reader"},
            {".loadtest", "application/xml"},
            {".lpk", "application/octet-stream"},
            {".lsf", "video/x-la-asf"},
            {".lst", "text/plain"},
            {".lsx", "video/x-la-asf"},
            {".lzh", "application/octet-stream"},
            {".m13", "application/x-msmediaview"},
            {".m14", "application/x-msmediaview"},
            {".m1v", "video/mpeg"},
            {".m2t", "video/vnd.dlna.mpeg-tts"},
            {".m2ts", "video/vnd.dlna.mpeg-tts"},
            {".m2v", "video/mpeg"},
            {".m3u", "audio/x-mpegurl"},
            {".m3u8", "audio/x-mpegurl"},
            {".m4a", "audio/m4a"},
            {".m4b", "audio/m4b"},
            {".m4p", "audio/m4p"},
            {".m4r", "audio/x-m4r"},
            {".m4v", "video/x-m4v"},
            {".mac", "image/x-macpaint"},
            {".mak", "text/plain"},
            {".man", "application/x-troff-man"},
            {".manifest", "application/x-ms-manifest"},
            {".map", "text/plain"},
            {".master", "application/xml"},
            {".mda", "application/msaccess"},
            {".mdb", "application/x-msaccess"},
            {".mde", "application/msaccess"},
            {".mdp", "application/octet-stream"},
            {".me", "application/x-troff-me"},
            {".mfp", "application/x-shockwave-flash"},
            {".mht", "message/rfc822"},
            {".mhtml", "message/rfc822"},
            {".mid", "audio/mid"},
            {".midi", "audio/mid"},
            {".mix", "application/octet-stream"},
            {".mk", "text/plain"},
            {".mmf", "application/x-smaf"},
            {".mno", "text/xml"},
            {".mny", "application/x-msmoney"},
            {".mod", "video/mpeg"},
            {".mov", "video/quicktime"},
            {".movie", "video/x-sgi-movie"},
            {".mp2", "video/mpeg"},
            {".mp2v", "video/mpeg"},
            {".mp3", "audio/mpeg"},
            {".mp4", "video/mp4"},
            {".mp4v", "video/mp4"},
            {".mpa", "video/mpeg"},
            {".mpe", "video/mpeg"},
            {".mpeg", "video/mpeg"},
            {".mpf", "application/vnd.ms-mediapackage"},
            {".mpg", "video/mpeg"},
            {".mpp", "application/vnd.ms-project"},
            {".mpv2", "video/mpeg"},
            {".mqv", "video/quicktime"},
            {".ms", "application/x-troff-ms"},
            {".msi", "application/octet-stream"},
            {".mso", "application/octet-stream"},
            {".mts", "video/vnd.dlna.mpeg-tts"},
            {".mtx", "application/xml"},
            {".mvb", "application/x-msmediaview"},
            {".mvc", "application/x-miva-compiled"},
            {".mxp", "application/x-mmxp"},
            {".nc", "application/x-netcdf"},
            {".nsc", "video/x-ms-asf"},
            {".nws", "message/rfc822"},
            {".ocx", "application/octet-stream"},
            {".oda", "application/oda"},
            {".odc", "text/x-ms-odc"},
            {".odh", "text/plain"},
            {".odl", "text/plain"},
            {".odp", "application/vnd.oasis.opendocument.presentation"},
            {".ods", "application/oleobject"},
            {".odt", "application/vnd.oasis.opendocument.text"},
            {".one", "application/onenote"},
            {".onea", "application/onenote"},
            {".onepkg", "application/onenote"},
            {".onetmp", "application/onenote"},
            {".onetoc", "application/onenote"},
            {".onetoc2", "application/onenote"},
            {".orderedtest", "application/xml"},
            {".osdx", "application/opensearchdescription+xml"},
            {".p10", "application/pkcs10"},
            {".p12", "application/x-pkcs12"},
            {".p7b", "application/x-pkcs7-certificates"},
            {".p7c", "application/pkcs7-mime"},
            {".p7m", "application/pkcs7-mime"},
            {".p7r", "application/x-pkcs7-certreqresp"},
            {".p7s", "application/pkcs7-signature"},
            {".pbm", "image/x-portable-bitmap"},
            {".pcast", "application/x-podcast"},
            {".pct", "image/pict"},
            {".pcx", "application/octet-stream"},
            {".pcz", "application/octet-stream"},
            {".pdf", "application/pdf"},
            {".pfb", "application/octet-stream"},
            {".pfm", "application/octet-stream"},
            {".pfx", "application/x-pkcs12"},
            {".pgm", "image/x-portable-graymap"},
            {".pic", "image/pict"},
            {".pict", "image/pict"},
            {".pkgdef", "text/plain"},
            {".pkgundef", "text/plain"},
            {".pko", "application/vnd.ms-pki.pko"},
            {".pls", "audio/scpls"},
            {".pma", "application/x-perfmon"},
            {".pmc", "application/x-perfmon"},
            {".pml", "application/x-perfmon"},
            {".pmr", "application/x-perfmon"},
            {".pmw", "application/x-perfmon"},
            {".png", "image/png"},
            {".pnm", "image/x-portable-anymap"},
            {".pnt", "image/x-macpaint"},
            {".pntg", "image/x-macpaint"},
            {".pnz", "image/png"},
            {".pot", "application/vnd.ms-powerpoint"},
            {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
            {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
            {".ppa", "application/vnd.ms-powerpoint"},
            {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
            {".ppm", "image/x-portable-pixmap"},
            {".pps", "application/vnd.ms-powerpoint"},
            {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
            {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {".ppt", "application/vnd.ms-powerpoint"},
            {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".prf", "application/pics-rules"},
            {".prm", "application/octet-stream"},
            {".prx", "application/octet-stream"},
            {".ps", "application/postscript"},
            {".psc1", "application/PowerShell"},
            {".psd", "application/octet-stream"},
            {".psess", "application/xml"},
            {".psm", "application/octet-stream"},
            {".psp", "application/octet-stream"},
            {".pub", "application/x-mspublisher"},
            {".pwz", "application/vnd.ms-powerpoint"},
            {".qht", "text/x-html-insertion"},
            {".qhtm", "text/x-html-insertion"},
            {".qt", "video/quicktime"},
            {".qti", "image/x-quicktime"},
            {".qtif", "image/x-quicktime"},
            {".qtl", "application/x-quicktimeplayer"},
            {".qxd", "application/octet-stream"},
            {".ra", "audio/x-pn-realaudio"},
            {".ram", "audio/x-pn-realaudio"},
            {".rar", "application/octet-stream"},
            {".ras", "image/x-cmu-raster"},
            {".rat", "application/rat-file"},
            {".rc", "text/plain"},
            {".rc2", "text/plain"},
            {".rct", "text/plain"},
            {".rdlc", "application/xml"},
            {".resx", "application/xml"},
            {".rf", "image/vnd.rn-realflash"},
            {".rgb", "image/x-rgb"},
            {".rgs", "text/plain"},
            {".rm", "application/vnd.rn-realmedia"},
            {".rmi", "audio/mid"},
            {".rmp", "application/vnd.rn-rn_music_package"},
            {".roff", "application/x-troff"},
            {".rpm", "audio/x-pn-realaudio-plugin"},
            {".rqy", "text/x-ms-rqy"},
            {".rtf", "application/rtf"},
            {".rtx", "text/richtext"},
            {".ruleset", "application/xml"},
            {".s", "text/plain"},
            {".safariextz", "application/x-safari-safariextz"},
            {".scd", "application/x-msschedule"},
            {".sct", "text/scriptlet"},
            {".sd2", "audio/x-sd2"},
            {".sdp", "application/sdp"},
            {".sea", "application/octet-stream"},
            {".searchConnector-ms", "application/windows-search-connector+xml"},
            {".setpay", "application/set-payment-initiation"},
            {".setreg", "application/set-registration-initiation"},
            {".settings", "application/xml"},
            {".sgimb", "application/x-sgimb"},
            {".sgml", "text/sgml"},
            {".sh", "application/x-sh"},
            {".shar", "application/x-shar"},
            {".shtml", "text/html"},
            {".sit", "application/x-stuffit"},
            {".sitemap", "application/xml"},
            {".skin", "application/xml"},
            {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
            {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
            {".slk", "application/vnd.ms-excel"},
            {".sln", "text/plain"},
            {".slupkg-ms", "application/x-ms-license"},
            {".smd", "audio/x-smd"},
            {".smi", "application/octet-stream"},
            {".smx", "audio/x-smd"},
            {".smz", "audio/x-smd"},
            {".snd", "audio/basic"},
            {".snippet", "application/xml"},
            {".snp", "application/octet-stream"},
            {".sol", "text/plain"},
            {".sor", "text/plain"},
            {".spc", "application/x-pkcs7-certificates"},
            {".spl", "application/futuresplash"},
            {".src", "application/x-wais-source"},
            {".srf", "text/plain"},
            {".SSISDeploymentManifest", "text/xml"},
            {".ssm", "application/streamingmedia"},
            {".sst", "application/vnd.ms-pki.certstore"},
            {".stl", "application/vnd.ms-pki.stl"},
            {".sv4cpio", "application/x-sv4cpio"},
            {".sv4crc", "application/x-sv4crc"},
            {".svc", "application/xml"},
            {".svg", "image/svg+xml"},
            {".swf", "application/x-shockwave-flash"},
            {".t", "application/x-troff"},
            {".tar", "application/x-tar"},
            {".tcl", "application/x-tcl"},
            {".testrunconfig", "application/xml"},
            {".testsettings", "application/xml"},
            {".tex", "application/x-tex"},
            {".texi", "application/x-texinfo"},
            {".texinfo", "application/x-texinfo"},
            {".tgz", "application/x-compressed"},
            {".thmx", "application/vnd.ms-officetheme"},
            {".thn", "application/octet-stream"},
            {".tif", "image/tiff"},
            {".tiff", "image/tiff"},
            {".tlh", "text/plain"},
            {".tli", "text/plain"},
            {".toc", "application/octet-stream"},
            {".tr", "application/x-troff"},
            {".trm", "application/x-msterminal"},
            {".trx", "application/xml"},
            {".ts", "video/vnd.dlna.mpeg-tts"},
            {".tsv", "text/tab-separated-values"},
            {".ttf", "application/octet-stream"},
            {".tts", "video/vnd.dlna.mpeg-tts"},
            {".txt", "text/plain"},
            {".u32", "application/octet-stream"},
            {".uls", "text/iuls"},
            {".user", "text/plain"},
            {".ustar", "application/x-ustar"},
            {".vb", "text/plain"},
            {".vbdproj", "text/plain"},
            {".vbk", "video/mpeg"},
            {".vbproj", "text/plain"},
            {".vbs", "text/vbscript"},
            {".vcf", "text/x-vcard"},
            {".vcproj", "Application/xml"},
            {".vcs", "text/plain"},
            {".vcxproj", "Application/xml"},
            {".vddproj", "text/plain"},
            {".vdp", "text/plain"},
            {".vdproj", "text/plain"},
            {".vdx", "application/vnd.ms-visio.viewer"},
            {".vml", "text/xml"},
            {".vscontent", "application/xml"},
            {".vsct", "text/xml"},
            {".vsd", "application/vnd.visio"},
            {".vsi", "application/ms-vsi"},
            {".vsix", "application/vsix"},
            {".vsixlangpack", "text/xml"},
            {".vsixmanifest", "text/xml"},
            {".vsmdi", "application/xml"},
            {".vspscc", "text/plain"},
            {".vss", "application/vnd.visio"},
            {".vsscc", "text/plain"},
            {".vssettings", "text/xml"},
            {".vssscc", "text/plain"},
            {".vst", "application/vnd.visio"},
            {".vstemplate", "text/xml"},
            {".vsto", "application/x-ms-vsto"},
            {".vsw", "application/vnd.visio"},
            {".vsx", "application/vnd.visio"},
            {".vtx", "application/vnd.visio"},
            {".wav", "audio/wav"},
            {".wave", "audio/wav"},
            {".wax", "audio/x-ms-wax"},
            {".wbk", "application/msword"},
            {".wbmp", "image/vnd.wap.wbmp"},
            {".wcm", "application/vnd.ms-works"},
            {".wdb", "application/vnd.ms-works"},
            {".wdp", "image/vnd.ms-photo"},
            {".webarchive", "application/x-safari-webarchive"},
            {".webtest", "application/xml"},
            {".wiq", "application/xml"},
            {".wiz", "application/msword"},
            {".wks", "application/vnd.ms-works"},
            {".WLMP", "application/wlmoviemaker"},
            {".wlpginstall", "application/x-wlpg-detect"},
            {".wlpginstall3", "application/x-wlpg3-detect"},
            {".wm", "video/x-ms-wm"},
            {".wma", "audio/x-ms-wma"},
            {".wmd", "application/x-ms-wmd"},
            {".wmf", "application/x-msmetafile"},
            {".wml", "text/vnd.wap.wml"},
            {".wmlc", "application/vnd.wap.wmlc"},
            {".wmls", "text/vnd.wap.wmlscript"},
            {".wmlsc", "application/vnd.wap.wmlscriptc"},
            {".wmp", "video/x-ms-wmp"},
            {".wmv", "video/x-ms-wmv"},
            {".wmx", "video/x-ms-wmx"},
            {".wmz", "application/x-ms-wmz"},
            {".wpl", "application/vnd.ms-wpl"},
            {".wps", "application/vnd.ms-works"},
            {".wri", "application/x-mswrite"},
            {".wrl", "x-world/x-vrml"},
            {".wrz", "x-world/x-vrml"},
            {".wsc", "text/scriptlet"},
            {".wsdl", "text/xml"},
            {".wvx", "video/x-ms-wvx"},
            {".x", "application/directx"},
            {".xaf", "x-world/x-vrml"},
            {".xaml", "application/xaml+xml"},
            {".xap", "application/x-silverlight-app"},
            {".xbap", "application/x-ms-xbap"},
            {".xbm", "image/x-xbitmap"},
            {".xdr", "text/plain"},
            {".xht", "application/xhtml+xml"},
            {".xhtml", "application/xhtml+xml"},
            {".xla", "application/vnd.ms-excel"},
            {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
            {".xlc", "application/vnd.ms-excel"},
            {".xld", "application/vnd.ms-excel"},
            {".xlk", "application/vnd.ms-excel"},
            {".xll", "application/vnd.ms-excel"},
            {".xlm", "application/vnd.ms-excel"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
            {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".xlt", "application/vnd.ms-excel"},
            {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
            {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
            {".xlw", "application/vnd.ms-excel"},
            {".xml", "text/xml"},
            {".xmta", "application/xml"},
            {".xof", "x-world/x-vrml"},
            {".XOML", "text/plain"},
            {".xpm", "image/x-xpixmap"},
            {".xps", "application/vnd.ms-xpsdocument"},
            {".xrm-ms", "text/xml"},
            {".xsc", "application/xml"},
            {".xsd", "text/xml"},
            {".xsf", "text/xml"},
            {".xsl", "text/xml"},
            {".xslt", "text/xml"},
            {".xsn", "application/octet-stream"},
            {".xss", "application/xml"},
            {".xtp", "application/octet-stream"},
            {".xwd", "image/x-xwindowdump"},
            {".z", "application/x-compress"},
            {".zip", "application/x-zip-compressed"}
        };

        /// <summary>
        /// Gets the MIME type of the content.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">extension</exception>
        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }
            string mime;
            return Mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

        /// <summary>
        /// Dispose of resources here
        /// </summary>
        public virtual void Dispose()
        {
            if (AutoDisposeStream && Stream != null)
            {
                Stream.Dispose();
                Stream = null;
            }
        }
    }
}
