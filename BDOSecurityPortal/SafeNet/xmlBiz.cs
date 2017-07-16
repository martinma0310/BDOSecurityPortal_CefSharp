using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace xmlTool
{
    public class xmlBiz
    {
        /// <summary>
        /// 字符串转换为XML文档
        /// </summary>
        /// <param name="xmlString">xml 字符串</param>
        /// <returns>XmlDocument</returns>
        public XmlDocument getXmlDocument(string xmlString)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlString);
            return xml;
        }
        /// <summary>
        /// 读取XML
        /// </summary>
        /// <param name="xmlString">文件名</param>
        /// <returns>XmlDocument</returns>
        public XmlDocument getXmlDocumentByName(string fileName)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(fileName);
            return xml;
        }
        /// <summary>
        /// 按节点名获取XML一级节点集合
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public XmlNodeList getXmlNode(XmlDocument xml, string nodeName)
        {
            return xml.SelectNodes(nodeName);
        }
        /// <summary>
        /// 按节点名获取某节点下的一级子节点集合
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public XmlNodeList getXmlNode(XmlNode node, string nodeName)
        {
            return node.SelectNodes(nodeName);
        }
        /// <summary>
        /// 按节点名获取XML一级节点
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public XmlNode getXmlSingleNode(XmlDocument xml, string nodeName)
        {
            return xml.SelectSingleNode(nodeName);
        }
        /// <summary>
        /// 按节点名获取某节点下的一级子节点
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="node"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public XmlNode getXmlSingleNode(XmlNode node, string nodeName)
        {
            return node.SelectSingleNode(nodeName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="text"></param>
        public void setNodeInnerText(XmlNode node, string text)
        {
            node.InnerText = text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string getXmlAttribute(XmlNode node, string attributeName)
        {
            return node.Attributes[attributeName].Value;
        }
        /// <summary>
        /// 在XML中创建节点
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <returns></returns>
        public XmlNode createXmlNode(XmlDocument xml, string nodeName, string nodeValue)
        {
            XmlElement childNode = xml.CreateElement(nodeName);
            if (!string.IsNullOrEmpty(nodeValue))
            {
                childNode.InnerText = nodeValue;
            }
            return childNode;
        }
        
        /// <summary>
        /// 为节点设置属性
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void createNodeAttribute(XmlElement node, string name, string value)
        {
            node.SetAttribute(name, value);
        }
    }
}
