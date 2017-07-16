using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using BDOSecurityPortalModel;
using BDOSecurityPortalBLL;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Globalization;

namespace BDOSecurityPortal
{
    static class Program
    {
        public static string ReleaseType;
        public static System.Threading.Mutex Run;


        //[DllImport("user32.dll")]
        //private static extern void SetProcessDPIAware();
        //[DllImport("kernel32.dll")]
        //private static extern IntPtr GetModuleHandle(string name);
        //// 这个函数只能接受ASCII，所以一定要设置CharSet = CharSet.Ansi，不然会失败
        //[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        //private static extern IntPtr GetProcAddress(IntPtr hmod, string name);
        //private delegate void FarProc();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //DPI 适配
                //SetProcessDPIAware();

                ReleaseType = ConfigurationManager.AppSettings["RELEASE_TYPE"].Trim().ToUpper();

                #region MessageBox 按钮国际化

                string language = ConfigurationManager.AppSettings["DEFAULT_LANGUAGE"].Trim();
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
                ResourceCulture.SetCurrentCulture(language);

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

                #region 保证同时只有一个客户端在运行 

                bool noRun = false;
                Run = new System.Threading.Mutex(true, "BDOSecurityPortal", out noRun);
                if (!noRun)
                {
                    Process instance = RunningInstance();

                    string productName = ResourceCulture.GetString("AppName");  //Application.ProductName
                    string exMsg = string.Format(ResourceCulture.GetString("GeneralMsg_AppAlreadyRun"), productName);
                    MessageBox.Show(exMsg, productName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HandleRunningInstance(instance);
                    return;
                }

                #endregion

                #region 检测软锁(hasplms)服务
                ServiceController[] localServices = ServiceController.GetServices();
                var hasplmsServices = localServices.SingleOrDefault(s => s.ServiceName == "hasplms");
                if (hasplmsServices == null)//软锁服务未安装成功
                {
                    MessageBox.Show(ResourceCulture.GetString("GeneralMsg_HasplmsNotExist"), ResourceCulture.GetString("GeneralTitle_Prompt"));
                    Application.Exit();
                    return;
                }
                if (hasplmsServices.Status != ServiceControllerStatus.Running)
                {
                    hasplmsServices.Start();
                } 
                #endregion

                #region 检测网络连接是否正常

                if (!CommonService.IsConnectInternet())
                {
                    string productName = ResourceCulture.GetString("AppName");  //Application.ProductName
                    string exMsg = string.Format(ResourceCulture.GetString("GeneralMsg_NetworkOffline"), Application.ProductName);
                    MessageBox.Show(exMsg, productName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #endregion

                #region 程序升级 
                string localVersion = string.Empty, serverVersion = string.Empty;
                var shouldUpdateApp = ShouldUpdateApp(out localVersion, out serverVersion);
                if (shouldUpdateApp)
                {
                    Process.Start(Application.StartupPath + @"\AutoUpdate.exe");
                    //Application.Exit();
                    Environment.Exit(0);
                    return;
                }
                #endregion

                //SetProcessDPIAware(); // 不兼容XP
                //IntPtr hUser32 = GetModuleHandle("user32.dll");
                //IntPtr addrSetProcessDPIAware = GetProcAddress(hUser32, "SetProcessDPIAware");
                //if (addrSetProcessDPIAware != IntPtr.Zero)
                //{
                //    FarProc SetProcessDPIAware = (FarProc)Marshal.GetDelegateForFunctionPointer(addrSetProcessDPIAware, typeof(FarProc));
                //    SetProcessDPIAware();
                //}
                //// C#的原有代码
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Login());
            }
            catch (Exception ex)
            {
                string exMsg = ResourceCulture.GetString("GeneralMsg_AppException");
                if (ReleaseType == "DEBUG")
                {
                    exMsg = ex.Message + ex.StackTrace;
                }
                MessageBox.Show(exMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();
                Environment.Exit(0);
            }
        }

        //[DllImport("User32.dll")]
        //private static extern void SetProcessDPIAware();

        private static bool ShouldUpdateApp(out string localVersion, out string serverVersion)
        {
            bool shouldUpdate = false;
            try
            {
                Run.ReleaseMutex();
                //AutoUpdate.XmlFiles localXmlFiles = new AutoUpdate.XmlFiles(Path.GetDirectoryName(Application.ExecutablePath) + @"\UpdateList.xml");
                AutoUpdate.XmlFiles localXmlFiles = new AutoUpdate.XmlFiles(Application.StartupPath + @"\UpdateList.xml");
                localVersion = localXmlFiles.GetNodeValue("//Version");
                AutoUpdate.XmlFiles serverXmlFiles = new AutoUpdate.XmlFiles(localXmlFiles.GetNodeValue("//Url") + @"UpdateList.xml");
                serverVersion = serverXmlFiles.GetNodeValue("//Version");
                shouldUpdate = Convert.ToInt32(serverVersion.Replace(".", "")) > Convert.ToInt32(localVersion.Replace(".", ""));
            }
            catch
            {
                localVersion = string.Empty;
                serverVersion = string.Empty;
            }
            return shouldUpdate;
        }

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        private static void HandleRunningInstance(Process instance)
        {
            // 确保窗口没有被最小化或最大化   
            ShowWindowAsync(instance.MainWindowHandle, 4);
            // 设置真实例程为foreground  window    
            SetForegroundWindow(instance.MainWindowHandle);// 放到最前端   
        }
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    // 确保例程从EXE文件运行   
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        } 
    }
}
