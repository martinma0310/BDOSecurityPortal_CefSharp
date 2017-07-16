using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using xmlTool;

using Aladdin.HASP;
namespace safeNetTool
{
    internal class safeNetDll
    {

        private string jSessionId;
        private int registrationStat;
        private string redirectToUserReg;
        public bool login(string slCode)
        {
            bool rtn = false;
            HaspFeature feature = HaspFeature.Default;

            Hasp hasp = new Hasp(feature);
            HaspStatus status = hasp.Login(safeNetEntity.vendorCode, safeNetEntity.loginScope.Replace("{slcode}", slCode));

            if (HaspStatus.StatusOk == status)
            {
                rtn = true;
            }
            return rtn;
        }

        public string loginstate(string slCode)
        {
            HaspFeature feature = HaspFeature.Default;

            Hasp hasp = new Hasp(feature);
            HaspStatus status = hasp.Login(safeNetEntity.vendorCode, safeNetEntity.loginScope.Replace("{slcode}", slCode));

            return status.ToString();
        }
        public bool logout()
        {
            bool rtn = false;
            HaspFeature feature = HaspFeature.Default;

            Hasp hasp = new Hasp(feature);
            HaspStatus status = hasp.Logout();

            if (HaspStatus.StatusOk == status)
            {
                rtn = true;
            }
            return rtn;
        }
        internal string getSLInfo(string slCode)
        {
            string info = null;
            Hasp.GetInfo(safeNetEntity.loginScope.Replace("{slcode}",slCode), safeNetEntity.formatInfo, safeNetEntity.vendorCode, ref info);
            return info;
        }
        internal string update(string v2c)
        {
            string refXml = null;
            HaspStatus status = Hasp.Update(v2c, ref refXml);
            if (status == HaspStatus.UpdateAlreadyAdded)
            {
                refXml = "T1";
            }
            else if (status == HaspStatus.InvalidUpdateData)
            {
                refXml = "T2";
            }
            else if (status == HaspStatus.StatusOk)
            {
                refXml = "ok";
            }
            else
            {
                refXml = ((int)status + status.ToString());
            }
            return refXml;
        }

        internal string TransferToolupdate(string v2c)
        {
            string refXml = null;
            HaspStatus status = Hasp.Update(v2c, ref refXml);
            if (status == HaspStatus.StatusOk)
            {
                refXml = "ok";
            }
            else
            {
                refXml = ((int)status + status.ToString());
            }
            return refXml;
        }

        internal string rehost(string slcode, string recipient)
        {
            string info = null;
            HaspStatus status = Hasp.Transfer(safeNetEntity.actionRehost.Replace("{slcode}", slcode), safeNetEntity.scopeRehost.Replace("{slcode}", slcode), safeNetEntity.vendorCode, recipient, ref info);
            return status == HaspStatus.StatusOk ? info : ((int)status + status.ToString());
        }

        /// <summary>
        /// 获取SAFENET SLCODE XML
        /// </summary>
        /// <returns></returns>
        internal string getSLXML()
        {
            string info = null;
            Hasp.GetInfo(safeNetEntity.scope, safeNetEntity.format, safeNetEntity.vendorCode, ref info);
            return info;
        }

        /// <summary>
        /// 获取C2V XML
        /// </summary>
        /// <returns></returns>
        internal string getC2VXML()
        {
            string info = null;
            HaspStatus status = Hasp.GetInfo(safeNetEntity.scope, safeNetEntity.formatC2V, safeNetEntity.vendorCode, ref info);
            return info;
        }
        /// <summary>
        /// 获取C2V XML
        /// </summary>
        /// <returns></returns>
        internal string getImmigrationXML()
        {
            string info = null;
            HaspStatus status = Hasp.GetInfo(safeNetEntity.scope, safeNetEntity.formatImmigration, safeNetEntity.vendorCode, ref info);
            if (status == HaspStatus.StatusOk)
            {
                return info;
            }
            else
            {
                return (int)status + status.ToString();
            }
        }

        /// <summary>
        /// 生成SAFENET证书
        /// </summary>
        /// <param name="productKey"></param>
        /// <returns></returns>
        internal bool installSafeNet(string productKey, out string slId, out string msg)
        {
            msg = null;
            slId = null;
            try
            {
                string v2c = getV2C(productKey, out msg); 
                string refXml = null; 
                if (!string.IsNullOrEmpty(v2c))
                {
                    xmlBiz bll = new xmlBiz();
                    XmlDocument xml = bll.getXmlDocument(v2c.ToString());
                    XmlNode haspInfo = bll.getXmlSingleNode(xml, "hasp_info");
                    XmlNode haspscope = bll.getXmlSingleNode(haspInfo, "haspscope");
                    XmlNode vendor = bll.getXmlSingleNode(haspscope, "vendor");
                    XmlNode hasp = bll.getXmlSingleNode(vendor, "hasp");
                    slId = bll.getXmlAttribute(hasp, "id");
                }
                HaspStatus status = Hasp.Update(v2c, ref refXml);
                return status == HaspStatus.StatusOk;
            }
            catch (Exception ex)
            {
                msg += ex.Message + ex.StackTrace;
                return false;
            }
        }
        private string getV2C(string pk, out string msg)
        {
            msg = "";
            String rtn = null;
            HttpWebRequest request = null;
            string url = safeNetEntity.url;
            string version = safeNetEntity.version;
            Encoding encoding = safeNetEntity.encoding;

            try
            {
                if (customerLogin(pk))
                {
                    string c2v = this.getC2VXML();
                    string requestBody = this.generateLicenseRequestXml(c2v);
                    request = (HttpWebRequest)WebRequest.Create(url + "ems/v21/ws/productKey/" + pk + "/activation.ws");


                    request.Method = "post";
                    Cookie appCookie = new Cookie("JSESSIONID", jSessionId);
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(new Uri(url + "ems/v21/ws/productKey/" + pk + "/activation.ws"), appCookie);
                    request.Accept = version;
                    request.ContentType = "application/xml";

                    byte[] buffer = encoding.GetBytes(requestBody);

                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                    request.GetRequestStream().Flush();
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        rtn = this.exctractV2CFromResponseXml(reader.ReadToEnd());
                    }
                }
                else
                {
                    msg = "此授权码不存在";
                }
            }
            catch (Exception ex)
            {
                msg = "授权失败，请检查授权码是否有效！";
            }
            return rtn;
        }


        private bool customerLogin(string pk)
        {
            //logging in
            String[] loginRes = this.customerLogined(pk);
            ////validate login response
            if (loginRes == null)
            {
                return false;
            }
            ////validate and set jsessionid and registration flag
            jSessionId = loginRes[0];
            if (string.IsNullOrEmpty(jSessionId))
            {
                return false;
            }

            if (loginRes[1] != null)
            {
                registrationStat = Int32.Parse(loginRes[1]);
            }
            if (loginRes[2] != null)
            {
                redirectToUserReg = loginRes[2];
            }
            return true;
        }

        private string[] customerLogined(String pk)
        {
            string url = safeNetEntity.url;
            string version = safeNetEntity.version;
            Encoding encoding = safeNetEntity.encoding;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "ems/v21/ws/loginByProductKey.ws");
            request.Method = "post";
            request.Accept = version;
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = encoding.GetBytes("productKey=" + pk);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            request.GetRequestStream().Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string rtn = null;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                rtn = reader.ReadToEnd();

                response.Close();
            }
            if (!string.IsNullOrEmpty(rtn))
            {
                return this.extractJsessionIdFromResponese(rtn);
            }
            return null;
        }

        private string[] extractJsessionIdFromResponese(string xml)
        {
            String[] loginRes = new String[3];
            if (xml.ToLower().Contains("<html>"))
            {
                return null;
            }
            xmlBiz bll = new xmlBiz();
            XmlDocument xmlDoc = bll.getXmlDocument(xml);
            XmlNode root = bll.getXmlSingleNode(xmlDoc, "EMSResponse");
            XmlNode sessionId = bll.getXmlSingleNode(root, "sessionId");
            loginRes[0] = sessionId.InnerText;
            XmlNode regRequired = bll.getXmlSingleNode(root, "regRequired");
            loginRes[1] = regRequired.InnerText;
            XmlNode redirectToUserReg = bll.getXmlSingleNode(root, "redirectToUserReg");
            loginRes[2] = redirectToUserReg.InnerText;
            return loginRes;
        }

        private string generateLicenseRequestXml(string c2v)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n");
            sb.Append("<activation xsi:noNamespaceSchemaLocation=\"License.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\n");
            sb.Append("<activationInput>\n");
            sb.Append("<activationAttribute>\n");
            sb.Append("<attributeValue>\n");
            sb.Append("<![CDATA[");
            sb.Append(c2v);
            sb.Append("]]> \n");
            sb.Append("</attributeValue>\n");
            sb.Append("<attributeName>C2V</attributeName> \n");
            sb.Append(" </activationAttribute>\n");
            sb.Append("<comments></comments> \n");
            sb.Append("</activationInput>\n");
            sb.Append("</activation>\n");

            return sb.ToString();

        }

        private string exctractV2CFromResponseXml(String responseXml)
        {
            xmlBiz bll = new xmlBiz();

            XmlDocument xmlDoc = bll.getXmlDocument(responseXml);
            XmlNode root = bll.getXmlSingleNode(xmlDoc, "activation");
            XmlNode node = bll.getXmlSingleNode(root, "activationOutput");
            XmlNode v2c = bll.getXmlSingleNode(node, "activationString");

            return v2c.InnerText;
        }
    }
}
