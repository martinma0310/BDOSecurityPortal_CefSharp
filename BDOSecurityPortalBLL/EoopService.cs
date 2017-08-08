using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BDOSecurityPortalUtil;
using BDOSecurityPortalModel;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Web;
using System.Threading;

namespace BDOSecurityPortalBLL
{
    public static class EoopService
    {
        /// <summary>
        /// 获取WebChat聊天系统URL
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetEoopWebChatLoginURL(string userName, string password)
        {
            
            string returnUrl = string.Format("{0}?username={1}&password={2}&logintype=winform", GetEoopChatAppSettings("EOOP_WEB_CHAT_AUTH"), userName, password);
            returnUrl = HttpUtility.UrlDecode(returnUrl);
            return returnUrl;
        }

        public static string GetEoopChatAppSettings(string appSettingName)
        {
            return GetAppSettings("EOOP_Chat_SERVER_PATH") + GetAppSettings(appSettingName);
        }

        /// <summary>
        /// 获取用户未读消息记录
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static int GetUserWebContactList(string userName)
        {
            try
            {
                string eoopWebContactUrl = string.Format("{0}?userName={1}", GetEoopAppSettings("EOOP_WEB_CONTACT"), userName);
                string result = GETEoopHttpRequest(eoopWebContactUrl);
                ChatContentResult chatContent = (ChatContentResult)JsonConvert.DeserializeObject(result, typeof(ChatContentResult));

                //var resultList = chatContent.objValue.Where(m => m.from != userName && m.unreadCount != 0).ToList();
                return chatContent.result ? chatContent.objValue:0;//.Where(m => m.from != userName && m.unreadCount != 0).Count() : 0;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取用户未读信息失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 子系统统一跳转登录URL
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetSubsystemLoginURL(string userName, string password, string systemKey, string systemName, string systemUrl)
        {
            string returnUrl = string.Empty;
            EncryptBiz encryptBiz = new EncryptBiz();
            string enctPwd = encryptBiz.EncryptDES(password, null, true);
            string md5Pwd = Md5Hash(password + systemKey);
            //string token = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userName + DateTime.Now.ToString("yyyyMMdd") + systemKey, "MD5").ToLower();
            string token = Md5Hash(userName + DateTime.Now.ToString("yyyyMMdd") + systemKey).ToLower();
            //string token = Md5HashPwd(userName + DateTime.Now.ToString("yyyyMMdd") + systemKey);
            string eoopServiceUrl = GetEoopAppSettings("BDO_USER_SUBSYSTEM_LOGIN_VERIFY");
            returnUrl = string.Format("{0}?username={1}&password={2}&systemid={3}&name={4}&url={5}&mid={6}&token={7}", eoopServiceUrl, userName, enctPwd, systemKey, systemName, systemUrl, md5Pwd, token);
            return returnUrl;
        }

        //private static string Md5HashPwd(string input)
        //{
        //    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        //    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        //    StringBuilder sBuilder = new StringBuilder();
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        sBuilder.Append(data[i].ToString("x2"));
        //    }
        //    return sBuilder.ToString();
        //}

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool LoginVerify(string userName, string password, out string userId, out string message)
        {
            try
            {
                string eoopLoginVerify = GetEoopAppSettings("EOOP_LOGIN_VERIFY");
                eoopLoginVerify = string.Format("{0}?loginId={1}&passwd={2}", eoopLoginVerify, userName, password);
                string result = GETEoopHttpRequest(eoopLoginVerify);
                LoginResult loginResult = (LoginResult)JsonConvert.DeserializeObject(result, typeof(LoginResult));
                userId = loginResult.ok ? loginResult.objValue : null;
                message = loginResult.ok ? loginResult.value : loginResult.objValue;
                return loginResult.ok;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("用户登录失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 获取用户相关信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static UserProfile GetUserProfile(string userId)
        {
            try
            {
                string eoopUserProfileUrl = string.Format("{0}?userId={1}", GetEoopAppSettings("EOOP_USER_PROFILE"), userId);
                string result = GETEoopHttpRequest(eoopUserProfileUrl);
                UserProfileResult userProfileResult = (UserProfileResult)JsonConvert.DeserializeObject(result, typeof(UserProfileResult));

                return userProfileResult.ok ? userProfileResult.objValue : new UserProfile();
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取用户详情信息失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        public static bool ResetPassword(string userName, string oldPassword, string newPassword, out string message)
        {
            try
            {
                string eoopResetPwdUrl = GetEoopAppSettings("EOOP_RESET_PASSWORD");
                //string postData = "{\"username\":\"" + userName + "\",\"oldPassword\":\"" + oldPassword + "\",\"newPassword\":\"" + newPassword + "\"}";
                eoopResetPwdUrl = string.Format("{0}?loginId={1}&oldPassword={2}&newPassword={3}", eoopResetPwdUrl, userName, oldPassword, newPassword);
                string result = GETEoopHttpRequest(eoopResetPwdUrl);
                LoginResult resetPwdResult = (LoginResult)JsonConvert.DeserializeObject(result, typeof(LoginResult));
                message = resetPwdResult.objValue;
                return resetPwdResult.ok;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("用户密码重置失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 获取用户资产列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<Property> GetUserAssetList(string userName)
        {
            try
            {
                string eoopUserAssetUrl = string.Format("{0}?userName={1}", GetEoopAppSettings("EOOP_USER_ASSET"), userName);
                string result = GETEoopHttpRequest(eoopUserAssetUrl);

                UserAssetResult userAssetResult = (UserAssetResult)JsonConvert.DeserializeObject(result, typeof(UserAssetResult));
                foreach (Property property in userAssetResult.objValue)
                {
                    string comboDisplay = property.tinyintfield1 == "1" ? "【已注册】" : "【未注册】";
                    property.ComboValue = property.id + "_" + property.tinyintfield1;
                    property.ComboDisplay = comboDisplay + "【" + property.mark + "】【" + property.name + "】";
                }
                return userAssetResult.ok ? userAssetResult.objValue : null;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取用户资产列表信息失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 获取用户资产信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Property GetUserAsset(string id)
        {
            try
            {
                string eoopUserAssetUrl = string.Format("{0}?id={1}", GetEoopAppSettings("EOOP_USER_ASSET"), id);
                string result = GETEoopHttpRequest(eoopUserAssetUrl);

                UserAssetResult userAssetResult = (UserAssetResult)JsonConvert.DeserializeObject(result, typeof(UserAssetResult));
                return userAssetResult.ok && userAssetResult.objValue.Count > 0 ? userAssetResult.objValue[0] : null;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取用户资产信息失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 绑定用户资产信息
        /// </summary>
        /// <param name="slKeyCode"></param>
        /// <param name="propertyId"></param>
        /// <param name="isBind"></param>
        /// <returns></returns>
        public static bool BindUserAsset(string slKeyCode, int propertyId, int isBind = 1)
        {
            try
            {
                string eoopBindAssetUrl = string.Format("{0}?slKeyCode={1}&propertyId={2}&isBind={3}", GetEoopAppSettings("EOOP_USER_ASSET_BIND"), slKeyCode, propertyId, isBind);
                string result = GETEoopHttpRequest(eoopBindAssetUrl);

                BaseEoopResponse resultResponse = (BaseEoopResponse)JsonConvert.DeserializeObject(result, typeof(BaseEoopResponse));
                return resultResponse.ok;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("用户资产绑定失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 获取资产绑定信息
        /// </summary>
        /// <param name="slKeyCode"></param>
        /// <returns></returns>
        public static UserSLCodes GetUserSLCodes(string slKeyCode)
        {
            try
            {
                string eoopUserAssetUrl = string.Format("{0}?slKeyCode={1}", GetEoopAppSettings("EOOP_USER_SLCODES"), slKeyCode);
                string result = GETEoopHttpRequest(eoopUserAssetUrl);

                UserSLCodesResult resultResponse = (UserSLCodesResult)JsonConvert.DeserializeObject(result, typeof(UserSLCodesResult));
                return resultResponse.ok && resultResponse.objValue.Count > 0 ? resultResponse.objValue[0] : null;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取用户资产绑定失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 获取用户HrmResource信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<HrmResource> GetHrmResource(string userName)
        {
            try
            {
                string eoopUserAssetUrl = string.Format("{0}?userName={1}", GetEoopAppSettings("EOOP_HRM_RESOURCE"), userName);
                string result = GETEoopHttpRequest(eoopUserAssetUrl);

                HrmResourceResult resultResponse = (HrmResourceResult)JsonConvert.DeserializeObject(result, typeof(HrmResourceResult));
                return resultResponse.ok ? resultResponse.objValue : new List<HrmResource>();
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取用户组织架构信息失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 获取用户子系统列表信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<Link2System> GetUserSubsystemList(string userId)
        {
            try
            {
                string eoopUserSubsystemUrl = string.Format("{0}?userId={1}", GetEoopAppSettings("EOOP_USER_SUBSYSTEM"), userId);
                string result = GETEoopHttpRequest(eoopUserSubsystemUrl);

                SubsystemResult resultResponse = (SubsystemResult)JsonConvert.DeserializeObject(result, typeof(SubsystemResult));
                return resultResponse.ok ? resultResponse.objValue : null;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取用户子系统列表失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

        public static List<UserRoleMapping> GetGroupMembers(string userId, string loginId)
        {
            try
            {
                string eoopGroupMembersUrl = string.Format("{0}?userId={1}&loginId={2}", GetEoopAppSettings("EOOP_GROUP_MEMBERS"), userId, loginId);
                string result = GETEoopHttpRequest(eoopGroupMembersUrl);

                UserRoleMappingResult resultResponse = (UserRoleMappingResult)JsonConvert.DeserializeObject(result, typeof(UserRoleMappingResult));
                return resultResponse.ok ? resultResponse.objValue : null;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取UserRoleMapping异常：" + ex.Message, ex);
                throw apiEx;
            }
        }

        public static List<DepartRoleMapping> GetDepartRoleMapping(string departId)
        {
            try
            {
                string eoopDepartRoleMappingUrl = string.Format("{0}?departId={1}", GetEoopAppSettings("EOOP_DEPART_ROLES"), departId);
                string result = GETEoopHttpRequest(eoopDepartRoleMappingUrl);

                DepartRoleMappingResult resultResponse = (DepartRoleMappingResult)JsonConvert.DeserializeObject(result, typeof(DepartRoleMappingResult));
                return resultResponse.ok ? resultResponse.objValue : null;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取DepartRoleMapping异常：" + ex.Message, ex);
                throw apiEx;
            }
        }

        /// <summary>
        /// 添加 BDO 客户端日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public static bool AddBDOClientLogs(HrmResource loginUser, string userName, string slKeyCode, string itMark, string logType, string logData)
        {
            try
            {
                ClientLogsV2 clientLogsV2 = new ClientLogsV2();
                clientLogsV2.company = !string.IsNullOrEmpty(loginUser.subcompanyname) ? loginUser.subcompanyname : "";
                clientLogsV2.department = !string.IsNullOrEmpty(loginUser.departmentname) ? loginUser.departmentname : "";
                //clientLogsV2.userName = !string.IsNullOrEmpty(userName) ? userName : "";
                //clientLogsV2.loginId = !string.IsNullOrEmpty(loginUser.userId) ? loginUser.userId : "";
                clientLogsV2.userName = !string.IsNullOrEmpty(loginUser.name) ? loginUser.name : "";
                clientLogsV2.loginId = !string.IsNullOrEmpty(loginUser.loginid) ? loginUser.loginid : "";
                clientLogsV2.userSlCode = !string.IsNullOrEmpty(slKeyCode) ? slKeyCode : "";
                clientLogsV2.itMark = !string.IsNullOrEmpty(itMark) ? itMark : "";

                HardwareHelper hardwareInfo = HardwareHelper.Instance();
                clientLogsV2.pcName = hardwareInfo.ComputerName;
                clientLogsV2.cpuId = hardwareInfo.CpuID;
                clientLogsV2.cpuName = hardwareInfo.CpuName;
                clientLogsV2.diskName = hardwareInfo.DiskID;
                clientLogsV2.mcAddress = hardwareInfo.MacAddress;
                clientLogsV2.ip = hardwareInfo.IpAddress;
                clientLogsV2.ram = Math.Ceiling(Convert.ToDecimal(hardwareInfo.TotalPhysicalMemory) / 1024 / 1024 / 1024).ToString() + "G";
                clientLogsV2.logDate = DateTime.Now.ToString("yyyy-MM-dd");
                clientLogsV2.logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                clientLogsV2.logType = logType;                 //TODO: ....
                clientLogsV2.logTypeValue = "1";                //TODO: ....
                clientLogsV2.logData = logData;                 //

                string bdoClientLogURL = GetEoopAppSettings("EOOP_BDO_CLIENT_LOG");
                string requesstBody = JsonConvert.SerializeObject(clientLogsV2);
                string result = POSTEoopHttpRequest(bdoClientLogURL, requesstBody);
                BaseEoopResponse resultResponse = (BaseEoopResponse)JsonConvert.DeserializeObject(result, typeof(BaseEoopResponse));
                return resultResponse.ok;

            }
            catch (Exception ex)
            {
                //Exception clientEx = new Exception("添加客户端日志失败：" + ex.Message, ex);
                //throw clientEx;
                return false;
            }
        }

        /// <summary>
        /// 添加 BDO 客户端升级日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public static bool AddBDOUpgradeLogs(UpgradeLogs logs)
        {
            try
            {
                string bdoUpgradeLogURL = GetEoopAppSettings("EOOP_BDO_CLIENT_LOG");
                string requesstBody = JsonConvert.SerializeObject(logs);
                string result = POSTEoopHttpRequest(bdoUpgradeLogURL, requesstBody);
                BaseEoopResponse resultResponse = (BaseEoopResponse)JsonConvert.DeserializeObject(result, typeof(BaseEoopResponse));
                return resultResponse.ok;
            }
            catch (Exception ex)
            {
                //Exception clientEx = new Exception("添加客户端升级日志失败：" + ex.Message, ex);
                //throw clientEx;
                return false;
            }
        }

        /// <summary>
        /// 获取 Eoop URL
        /// </summary>
        /// <param name="appSettingName"></param>
        /// <returns></returns>
        public static string GetEoopAppSettings(string appSettingName)
        {
            return GetAppSettings("EOOP_SERVER_PATH") + GetAppSettings(appSettingName);
        }

        /// <summary>
        /// 获取 AppSetting 配置信息
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static string GetAppSettings(string appSettingName)
        {
            appSettingName = appSettingName.ToUpper();
            return ConfigurationManager.AppSettings[appSettingName.ToUpper()].ToString();
        }

        /// <summary>
        /// Send GET HttpRequest
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        private static string GETEoopHttpRequest(string requestUrl)
        {
            requestUrl = HttpUtility.UrlDecode(requestUrl, Encoding.UTF8);
            System.Net.HttpWebRequest myRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(requestUrl);
            myRequest.Headers.Set(System.Net.HttpRequestHeader.AcceptLanguage, Thread.CurrentThread.CurrentCulture.Name);
            myRequest.Method = "GET";
            myRequest.Timeout = 12000;
            myRequest.ContentType = "application/json;charset=UTF-8";
            myRequest.Accept = "application/json;charset=UTF-8";
            System.Net.HttpWebResponse myResponse = (System.Net.HttpWebResponse)myRequest.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            int status = (int)myResponse.StatusCode;
            reader.Close();
            return result;
        }

        /// <summary>
        /// Send POST HttpRequest
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        private static string POSTEoopHttpRequest(string requestUrl, string requestBody)
        {
            requestUrl = HttpUtility.UrlDecode(requestUrl, Encoding.UTF8);

            byte[] data = Encoding.UTF8.GetBytes(requestBody);
            System.Net.HttpWebRequest myRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(requestUrl);
            myRequest.Method = "POST";
            string language = Thread.CurrentThread.CurrentCulture.Name;
            myRequest.Headers.Set(System.Net.HttpRequestHeader.AcceptLanguage, Thread.CurrentThread.CurrentCulture.Name);
            myRequest.Timeout = 8000;
            myRequest.ContentType = "application/json;charset=UTF-8";
            myRequest.Accept = "application/json;charset=UTF-8";
            myRequest.ContentLength = data.Length;
            System.IO.Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            System.Net.HttpWebResponse myResponse = (System.Net.HttpWebResponse)myRequest.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            int status = (int)myResponse.StatusCode;
            reader.Close();
            return result;
        }


        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="inputSTR"></param>
        /// <returns></returns>
        private static string Md5Hash(string inputSTR)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.ASCII.GetBytes(inputSTR));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            return sBuilder.ToString();
        }

        public static bool CheckNewMessage(string userID)
        {
            try
            {
                string eoopGetChatUnreadCountUrl = string.Format("{0}?userName={1}", GetEoopChatAppSettings("EOOP_CHAT_UNREADCOUNT"), userID);
                string result = GETEoopHttpRequest(eoopGetChatUnreadCountUrl);

                ChatUnreadCountResult resultResponse = (ChatUnreadCountResult)JsonConvert.DeserializeObject(result, typeof(ChatUnreadCountResult));
                int? unReadCount = resultResponse.result ? resultResponse.objValue : 0;
                unReadCount = unReadCount == null ? 0 : unReadCount;

                return unReadCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Exception apiEx = new Exception("获取聊天未读信息失败：" + ex.Message, ex);
                throw apiEx;
            }
        }

    }
}
