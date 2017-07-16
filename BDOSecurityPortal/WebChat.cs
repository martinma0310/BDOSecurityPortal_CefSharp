using CefSharp;
using System; 
using System.Runtime.InteropServices; 
using System.Windows.Forms;

namespace BDOSecurityPortal
{ 

    public partial class WebChat : Form
    {

        #region 属性
        CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser = new CefSharp.WinForms.ChromiumWebBrowser("");
        string chaturl = "";
        #endregion

        public WebChat(string  url, string language)
        {
            InitializeComponent();
            chaturl = url;
            #region chrome参数设置  
            chromiumWebBrowser.Dock = DockStyle.Fill; 
            chromiumWebBrowser.BackgroundImageLayout = ImageLayout.Tile;
            chromiumWebBrowser.MenuHandler = new ChatMenuHandler();
            //下载事件
            DownloadHandler downloadHandler = new DownloadHandler();
            downloadHandler.OnBeforeDownloadFired += OnBeforeDownloadFired;
            downloadHandler.OnDownloadUpdatedFired += OnDownloadUpdatedFired;
            chromiumWebBrowser.DownloadHandler = downloadHandler; 
            chromiumWebBrowser.RequestHandler = new RequestHandler(); 
            //注册脚本事件，用于前端调用后台方法
            chromiumWebBrowser.RegisterJsObject("cefsharpCallback", this);
            #endregion
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
                ShowWindow(handle, 0);//不显示下载对话框页面
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
                if (browser.IsPopup)
                {
                    browser.CloseBrowser(true);
                }
            }
        }

        private void WebChat_Load(object sender, EventArgs e)
        { 
            chromiumWebBrowser.Load(chaturl);
            this.Controls.Add(chromiumWebBrowser);
        }

     
    }
}
