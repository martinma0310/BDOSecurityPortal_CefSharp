using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using xmlTool;

namespace safeNetTool
{
    public class safeNetBiz
    {
        public bool login(string slCode)
        {
            safeNetDll sd = new safeNetDll();
            return sd.login(slCode);
        }

        public string getSLInfo(string slCode)
        {
            safeNetDll sd = new safeNetDll();
            return sd.getSLInfo(slCode);
        }

        public string loginstate(string slCode)
        {
            safeNetDll sd = new safeNetDll();
            return sd.loginstate(slCode);
        }

        public bool logout()
        {
            safeNetDll sd = new safeNetDll();
            return sd.logout();
        }

        public string rehost(string slcode,string recipient)
        {            
            safeNetDll sd = new safeNetDll();
            return sd.rehost(slcode, recipient);
        }
        public string getSL()
        {
            safeNetDll sd = new safeNetDll();
            //return sd.getSLXML(false);
            return sd.getSLXML();
        }

        public string getC2V()
        {
            safeNetDll sd = new safeNetDll();
            return sd.getC2VXML();
        }

        public string getImmigration()
        {
            safeNetDll sd = new safeNetDll();
            return sd.getImmigrationXML();
        }

        public bool installSafeNet(string pk, out string slId,out string msg)
        {
            safeNetDll sd = new safeNetDll();
            //return sd.installSafeNet(false, pk,out slId, out msg);
            return sd.installSafeNet(pk, out slId, out msg);
        }
        public string transferSafeNet(string h2h)
        { 
            safeNetDll sd = new safeNetDll();
            return sd.update(h2h);
        }

        public string TransferToolupdate(string h2h)
        {
            safeNetDll sd = new safeNetDll();
            return sd.TransferToolupdate(h2h);
        }

        /// <summary>
        /// 获取密锁锁号
        /// </summary>
        /// <returns></returns>
        public List<string> getSlCode()
        {
            List<string> rtn = new List<string>();
            safeNetBiz sb = new safeNetBiz();
            string xmlString = sb.getSL();
            if (!string.IsNullOrEmpty(xmlString))
            {
                string id;
                xmlBiz biz = new xmlBiz();
                XmlDocument doc = biz.getXmlDocument(xmlString);
                XmlDocument infoDoc = biz.getXmlDocument(xmlString);
                XmlNode root = biz.getXmlSingleNode(doc, "hasp_info");
                if (root != null)
                {
                    XmlNodeList nodeList = biz.getXmlNode(root, "hasp");
                    foreach (XmlNode node in nodeList)
                    {
                        id = biz.getXmlAttribute(node, "id");
                        //if (sb.login(id))
                        //{
                        infoDoc.LoadXml(sb.getSLInfo(id));
                        XmlNode infoRoot = infoDoc.SelectSingleNode("hasp_info");
                        if (infoRoot != null)
                        {
                            XmlNodeList infoNodeList = infoRoot.SelectNodes("feature");
                            bool mark = true;
                            foreach (XmlNode infoNode in infoNodeList)
                            {
                                if ("false" == infoNode.Attributes["locked"].Value
                                    || "true" == infoNode.Attributes["disabled"].Value
                                    || "false" == infoNode.Attributes["usable"].Value)
                                {
                                    mark = false;
                                }
                            }
                            if (mark)
                            {
                                rtn.Add(id);
                            }
                        }
                        //    sb.logout();
                        //}
                    }
                }
            }
            return rtn;
        }

    }
}
