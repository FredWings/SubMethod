using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Weekly.Public
{
    public class XmlReader : IXmlReader
    {
        /// <summary>
        /// 无参构造器，创建一个新的Xml读取器实例
        /// </summary>
        public XmlReader()
        {
            this.xmlDoc = new XmlDocument();
        }

        /// <summary>
        /// 保存当前出错信息
        /// </summary>
        /// <param name="errInfo">出错信息</param>
        private void SaveErrInfo(string errInfo)
        {
            this.LastErrInfo = errInfo;
        }

        /// <summary>
        /// 当前正在操作的XmlDocument
        /// </summary>
        public XmlDocument Document
        {
            get
            {
                return this.xmlDoc;
            }
            set
            {
                this.xmlDoc = value;
                this.curXmlNode = null;
                this.LastErrInfo = null;
            }
        }

        /// <summary>
        /// 最后一次异常信息
        /// </summary>
        public string LastErrInfo { get; private set; }

        /// <summary>
        /// 从指定的 URL 加载 XML 文档
        /// 异常：
        /// XmlReaderException   XML 中有加载或分析错误
        /// </summary>
        /// <param name="xmlFileName">文件的 URL，该文件包含要加载的 XML 文档。 
        /// URL 既可以是本地文件，也可以是 HTTP URL（Web 地址）。 </param>
        /// <returns>成功返回XmlDocument</returns>
        public XmlDocument Load(string xmlFileName)
        {
            XmlDocument result;
            try
            {
                this.xmlDoc.Load(xmlFileName);
                result = this.xmlDoc;
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "XML 中有加载或分析错误";
                throw new XmlReaderException(1, expInfo);
            }
            return result;
        }

        /// <summary>
        /// 从指定的字符串加载 XML 文档
        /// 异常：
        /// XmlReaderException   XML 中有加载或分析错误
        /// </summary>
        /// <param name="strXml">包含要加载的 XML 文档的字符串</param>
        /// <returns>成功返回XmlDocument</returns>
        public XmlDocument LoadXml(string strXml)
        {
            XmlDocument result;
            try
            {
                this.xmlDoc.LoadXml(strXml);
                result = this.xmlDoc;
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "XML 中有加载或分析错误";
                throw new XmlReaderException(1, expInfo);
            }
            return result;
        }

        /// <summary>
        /// 将 XML 文档保存到指定的文件
        /// 异常：
        /// XmlReaderException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="filename">要将文档保存到其中的文件的位置</param>
        public void Save(string filename)
        {
            try
            {
                this.xmlDoc.Save(filename);
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "此操作不产生格式良好的 XML 文档";
                throw new XmlReaderException(2, expInfo);
            }
        }

        /// <summary>
        /// 将 XML 文档保存到指定的流
        /// 异常：
        /// XmlReaderException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="outStream">要保存到其中的流</param>
        public void Save(Stream outStream)
        {
            try
            {
                this.xmlDoc.Save(outStream);
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "此操作不产生格式良好的 XML 文档";
                throw new XmlReaderException(2, expInfo);
            }
        }

        /// <summary>
        /// 将 XML 文档保存到指定的 TextWriter
        /// 异常：
        /// XmlReaderException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="outStream">要向其中保存内容的 TextWriter</param>
        public void Save(TextWriter writer)
        {
            try
            {
                this.xmlDoc.Save(writer);
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "此操作不产生格式良好的 XML 文档";
                throw new XmlReaderException(2, expInfo);
            }
        }

        /// <summary>
        /// 获取当前操纵的Xml文档的string格式串
        /// </summary>
        /// <returns>当前Xml文档的string格式串</returns>
        public string SaveToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(this.xmlDoc.OuterXml);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取匹配XPath表达式的第offset个节点XmlNode.
        /// 取到的节点将保存为当前操作节点
        /// 异常：
        /// XmlReaderException  xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要获取第几个节点</param>
        /// <returns>与 XPath 查询匹配的第offset个 XmlNode</returns>
        public XmlNode GetXmlNode(string xpath, int offset)
        {
            this.MoveToNode(xpath, offset);
            return this.curXmlNode;
        }

        /// <summary>
        /// 获取紧接在当前节点之后的节点
        /// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
        /// </summary>
        /// <returns>紧接在当前节点之后的XmlNode</returns>
        public XmlNode GetNextSibNode()
        {
            if (this.curXmlNode != null)
            {
                return this.curXmlNode.NextSibling;
            }
            return null;
        }

        /// <summary>
        /// 获取当前节点（对于可以具有父级的节点）的父级
        /// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
        /// </summary>
        /// <returns>当前节点父节点XmlNode</returns>
        public XmlNode GetParentNode()
        {
            if (this.curXmlNode != null)
            {
                return this.curXmlNode.ParentNode;
            }
            return null;
        }

        /// <summary>
        /// 获取匹配XPath表达式的节点集合XmlNodeList.
        /// 异常：
        /// XmlReaderException  xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <returns>包含匹配 XPath 查询的节点集合XmlNodeList</returns>
        public XmlNodeList GetXmlNodeList(string xpath)
        {
            XmlNodeList result;
            try
            {
                result = this.xmlDoc.SelectNodes(xpath);
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "xpath 表达式错误或目标节点不存在";
                throw new XmlReaderException(18, expInfo);
            }
            return result;
        }

        /// <summary>
        /// 获取当前节点的第index个属性
        /// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
        /// </summary>
        /// <param name="index">属性索引</param>
        /// <returns>当前节点的第index个XmlAttribute属性</returns>
        public XmlAttribute GetXmlAttribute(int index)
        {
            if (this.curXmlNode != null && this.curXmlNode.Attributes.Count > index)
            {
                return this.curXmlNode.Attributes[index];
            }
            return null;
        }

        /// <summary>
        /// 获取当前节点的名为attrName的属性
        /// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
        /// </summary>
        /// <param name="xmlAttrName">属性名</param>
        /// <returns>当前节点的名为attrName的XmlAttribute属性</returns>
        public XmlAttribute GetXmlAttribute(string xmlAttrName)
        {
            if (this.curXmlNode != null && this.curXmlNode.Attributes[xmlAttrName] != null)
            {
                return this.curXmlNode.Attributes[xmlAttrName];
            }
            return null;
        }

        /// <summary>
        /// 获取匹配xpath表达式的第offset个节点下名为xmlAttrName的属性.
        /// 异常：
        /// XmlReaderException  xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要获取第几个节点的属性</param>
        /// <param name="xmlAttributeName">要匹配的属性名称</param>
        /// <returns>匹配xpath表达式的第offset个节点下名为xmlAttrName的属性</returns>
        public XmlAttribute GetXmlAttribute(string xpath, int offset, string xmlAttrName)
        {
            XmlAttribute xmlAttribute = null;
            XmlNode xmlNode = this.GetXmlNode(xpath, offset);
            if (xmlNode != null)
            {
                this.curXmlNode = xmlNode;
                if (xmlNode.Attributes.Count > 0)
                {
                    xmlAttribute = xmlNode.Attributes[xmlAttrName];
                }
            }
            return xmlAttribute;
        }

        /// <summary>
        /// 获取匹配xpath表达式的第offset个节点下第index个属性.
        /// 异常：
        /// XmlReaderException  xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要获取第几个节点的属性</param>
        /// <param name="index">要匹配第几个属性</param>
        /// <returns>匹配xpath表达式的第offset个节点下第index个属性</returns>
        public XmlAttribute GetXmlAttribute(string xpath, int offset, int index)
        {
            XmlAttribute xmlAttribute = null;
            XmlNode xmlNode = this.GetXmlNode(xpath, offset);
            if (xmlNode != null)
            {
                this.curXmlNode = xmlNode;
                if (xmlNode.Attributes.Count > index)
                {
                    xmlAttribute = xmlNode.Attributes[index];
                }
            }
            return xmlAttribute;
        }

        /// <summary>
        /// 获取当前节点的第index个属性的值
        /// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
        /// </summary>
        /// <param name="index">属性索引</param>
        /// <returns>当前节点的第index个属性的值</returns>
        public string GetXmlAttrValue(int index)
        {
            XmlAttribute xmlAttr = this.GetXmlAttribute(index);
            if (xmlAttr != null)
            {
                return xmlAttr.Value;
            }
            return null;
        }

        /// <summary>
        /// 获取当前节点的名为attrName的属性的值
        /// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
        /// </summary>
        /// <param name="xmlAttrName">属性名</param>
        /// <returns>当前节点的名为attrName的属性的值</returns>
        public string GetXmlAttrValue(string xmlAttrName)
        {
            XmlAttribute xmlAttr = this.GetXmlAttribute(xmlAttrName);
            if (xmlAttr != null)
            {
                return xmlAttr.Value;
            }
            return null;
        }

        /// <summary>
        /// 获取匹配xpath表达式的第offset个节点下名为xmlAttrName的属性的值.
        /// 异常：
        /// XmlReaderException  xpath 表达式错误或目标节点不存在
        /// 详情见LastErrInfo属性
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要获取第几个节点的属性</param>
        /// <param name="xmlAttrName">要匹配的属性名称</param>
        /// <returns>匹配xpath表达式的第offset个节点下
        /// 名为xmlAttrName的属性的值</returns>
        public string GetXmlAttrValue(string xpath, int offset, string xmlAttrName)
        {
            XmlAttribute xmlAttr = this.GetXmlAttribute(xpath, offset, xmlAttrName);
            if (xmlAttr != null)
            {
                return xmlAttr.Value;
            }
            return null;
        }

        /// <summary>
        /// 获取匹配xpath表达式的第offset个节点下第index个属性的值.
        /// 异常：
        /// XmlReaderException  xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要获取第几个节点的属性</param>
        /// <param name="index">要获取第几个属性</param>
        /// <returns>匹配xpath表达式的第offset个节点下
        /// 第index个属性的值</returns>
        public string GetXmlAttrValue(string xpath, int offset, int index)
        {
            XmlAttribute xmlAttr = this.GetXmlAttribute(xpath, offset, index);
            if (xmlAttr != null)
            {
                return xmlAttr.Value;
            }
            return null;
        }

        /// <summary>
        /// 读取当前节点文本描述信息
        /// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
        /// </summary>
        /// <returns>当前节点文本描述信息</returns>
        public string GetInnerText()
        {
            if (this.curXmlNode != null)
            {
                return this.curXmlNode.InnerText;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取匹配xpath表达式的第0个节点的文本描述信息
        /// 异常：
        /// XmlReaderException  xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <returns>匹配xpath表达式的第offset个节点的文本描述信息</returns>
        public string GetInnerText(string xpath)
        {
            this.MoveToNode(xpath, 0);
            return this.GetInnerText();
        }

        /// <summary>
        /// 获取匹配xpath表达式的第offset个节点的文本描述信息
        /// 异常：
        /// XmlReaderException  xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要获取第几个节点的描述信息</param>
        /// <returns>匹配xpath表达式的第offset个节点的文本描述信息</returns>
        public string GetInnerText(string xpath, int offset)
        {
            this.MoveToNode(xpath, offset);
            return this.GetInnerText();
        }

        /// <summary>
        /// 移动到指定节点
        /// 异常：
        /// XmlReaderException   xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要移动到第几个节点</param>
        public void MoveToNode(string xpath, int offset)
        {
            this.curXmlNode = null;
            try
            {
                XmlNodeList nodeList = this.xmlDoc.SelectNodes(xpath);
                if (nodeList != null && nodeList.Count > offset)
                {
                    this.curXmlNode = nodeList[offset];
                    return;
                }
            }
            catch (Exception)
            {
                string errorInfo = "xpath illeagal or no target node";
                this.SaveErrInfo(errorInfo);
                throw new XmlReaderException(36, errorInfo);
            }
            
        }

        private XmlDocument xmlDoc;

        private XmlNode curXmlNode;
    }
}
