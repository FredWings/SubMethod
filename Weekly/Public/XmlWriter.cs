using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Weekly.Public
{
    public class XmlWriter : IXmlWriter
    {
        /// <summary>
        /// 无参构造器，创建一个新的Xml编写器实例
        /// </summary>
        public XmlWriter()
        {
            this.Document = new XmlDocument();
            this.hasRootNode = false;
            this.hasDecl = false;
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
        /// 获取Xml对象编写器
        /// </summary>
        public XmlDocument Document { get; set; }

        /// <summary>
        /// 取当前XmlNode节点
        /// </summary>
        public XmlNode CurrentNode { get; private set; }

        /// <summary>
        /// 最后一次异常信息
        /// </summary>
        public string LastErrInfo { get; private set; }

        /// <summary>
        /// 从指定的 URL 加载 XML 文档
        /// 异常：
        /// XmlWriterException   XML 中有加载或分析错误
        /// </summary>
        /// <param name="xmlFileName">文件的 URL，该文件包含要加载的 XML 文档。 
        /// URL 既可以是本地文件，也可以是 HTTP URL（Web 地址）。 </param>
        /// <returns>成功加载Xml文档后的XmlDocument</returns>
        public XmlDocument Load(string xmlFileName)
        {
            XmlDocument result;
            try
            {
                this.Document.Load(xmlFileName);
                CurrentNode = Document.DocumentElement;
                hasRootNode = true;
                result = this.Document;
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "XML 中有加载或分析错误";
                throw new XmlWriterException(1, expInfo);
            }
            return result;
        }

        /// <summary>
        /// 从指定的字符串加载 XML 文档
        /// 异常：
        /// XmlWriterException   XML 中有加载或分析错误
        /// </summary>
        /// <param name="strXml">包含要加载的 XML 文档的字符串</param>
        /// <returns>成功加载Xml文档字符串后的XmlDocument</returns>
        public XmlDocument LoadXml(string strXml)
        {
            XmlDocument result;
            try
            {
                this.Document.LoadXml(strXml);
                CurrentNode = Document.DocumentElement;
                hasRootNode = true;
                result = this.Document;
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "XML 中有加载或分析错误";
                throw new XmlWriterException(1, expInfo);
            }
            return result;
        }

        /// <summary>
        /// 将 XML 文档保存到指定的文件
        /// 异常：
        /// XmlWriterException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="filename">要将文档保存到其中的文件的位置</param>
        public void Save(string filename)
        {
            try
            {
                this.Document.Save(filename);
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "此操作不产生格式良好的 XML 文档";
                throw new XmlWriterException(2, expInfo);
            }
        }

        /// <summary>
        /// 将 XML 文档保存到指定的流
        /// 异常：
        /// XmlWriterException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="outStream">要保存到其中的流</param>
        public void Save(Stream outStream)
        {
            try
            {
                this.Document.Save(outStream);
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "此操作不产生格式良好的 XML 文档";
                throw new XmlWriterException(2, expInfo);
            }
        }

        /// <summary>
        /// 将 XML 文档保存到指定的 TextWriter
        /// 异常：
        /// XmlWriterException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="outStream">要向其中保存内容的 TextWriter</param>
        public void Save(TextWriter writer)
        {
            try
            {
                this.Document.Save(writer);
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "此操作不产生格式良好的 XML 文档";
                throw new XmlWriterException(2, expInfo);
            }
        }

        /// <summary>
        /// 获取当前操纵的Xml文档的string格式串
        /// </summary>
        /// <returns>当前Xml文档的string格式串</returns>
        public string SaveToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(this.Document.OuterXml);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 添加一个具有指定值的 XmlDeclaration 节点
        /// xml描述信息必须置于文档首，在创建写入器以后应最先调用该方法添加描述信息
        /// version必须为“1.0”，standalone值必须是“yes”或“no”。 
        /// 异常：
        /// XmlWriterException   已添加描述节点或version，standalone 的值非法
        /// </summary>
        /// <param name="version">Xml版本信息，必须为"1.0"</param>
        public void AddXmlDeclaration(string version)
        {
            this.AddXmlDeclaration(version, null, null);
        }

        /// <summary>
        /// 添加一个具有指定值的 XmlDeclaration 节点
        /// xml描述信息必须置于文档首，在创建写入器以后应最先调用该方法添加描述信息
        /// version必须为“1.0”，standalone值必须是“yes”或“no”。 
        /// 异常：
        /// XmlWriterException   已添加描述节点或version，standalone 的值非法
        /// </summary>
        /// <param name="version">Xml版本信息，必须为"1.0"</param>
        /// <param name="encoding">编码方式特性，默认的编码方式为 UTF-8</param>
        public void AddXmlDeclaration(string version, string encoding)
        {
            this.AddXmlDeclaration(version, encoding, null);
        }

        /// <summary>
        /// 添加一个具有指定值的 XmlDeclaration 节点
        /// xml描述信息必须置于文档首，在创建写入器以后应最先调用该方法添加描述信息
        /// version必须为“1.0”，standalone值必须是“yes”或“no”。 
        /// 异常：
        /// XmlWriterException   已添加描述节点或version，standalone 的值非法
        /// </summary>
        /// <param name="version">Xml版本信息，必须为"1.0"</param>
        /// <param name="encoding">编码方式特性，默认的编码方式为 UTF-8</param>
        /// <param name="standalone">该值必须是 "yes" 或 "no"</param>
        public void AddXmlDeclaration(string version, string encoding, string standalone)
        {
            if (this.hasDecl)
            {
                throw new XmlWriterException(33, "has added XmlDeclaration");
            }
            try
            {
                XmlDeclaration xmlDecl = this.Document.CreateXmlDeclaration(version, encoding, standalone);
                this.Document.AppendChild(xmlDecl);
                this.hasDecl = true;
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "version 或 standalone 的值不合法";
                throw new XmlWriterException(39, expInfo);
            }
        }

        /// <summary>
        /// 添加根节点
        /// 一个Xml文档只有一个根节点
        /// 异常：
        /// XmlWriterException   已添加根节点或创建节点失败
        /// </summary>
        /// <param name="nodeName">节点名</param>
        public void AddXmlRootNode(string nodeName)
        {
            this.AddXmlRootNode(nodeName, null);
        }

        /// <summary>
        /// 添加根节点
        /// 一个Xml文档只有一个根节点
        /// 异常：
        /// XmlWriterException   已添加根节点或创建节点失败
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点文本信息</param>
        public void AddXmlRootNode(string nodeName, string innerText)
        {
            if (this.hasRootNode)
            {
                throw new XmlWriterException(34, "has added root node");
            }
            try
            {
                XmlNode xmlNode = this.Document.CreateElement(nodeName);
                if (innerText != null && "" != innerText)
                {
                    xmlNode.InnerText = innerText;
                }
                this.Document.AppendChild(xmlNode);
                this.parentNode = null;
                this.CurrentNode = xmlNode;
                this.hasRootNode = true;
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "根节点创建出错";
                throw new XmlWriterException(40, expInfo);
            }
        }

        /// <summary>
        /// 在当前节点下添加Xml子节点
        /// 当前节点即最后一次添加/操作的节点
        /// 异常：
        /// XmlWriterException   无当前节点或子节点创建出错
        /// </summary>
        /// <param name="nodeName">节点名</param>
        public void AddSubXmlNode(string nodeName)
        {
            this.AddSubXmlNode(nodeName, null);
        }

        /// <summary>
        /// 在当前节点下添加Xml子节点
        /// 当前节点即最后一次添加/操作的节点
        /// 异常：
        /// XmlWriterException   无当前节点或子节点创建出错
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点文本信息</param>
        public void AddSubXmlNode(string nodeName, string innerText)
        {
            if (this.CurrentNode == null)
            {
                throw new XmlWriterException(36, "no target node");
            }
            try
            {
                XmlNode sunNode = this.Document.CreateElement(nodeName);
                if (innerText != null)
                {
                    sunNode.InnerText = innerText;
                }
                this.CurrentNode.AppendChild(sunNode);
                this.parentNode = this.CurrentNode;
                this.CurrentNode = sunNode;
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "子节点创建出错";
                throw new XmlWriterException(41, expInfo);
            }
        }

        /// <summary>
        /// 在指定节点下添加Xml子节点
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或子节点创建出错
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要指定第几个节点</param>
        /// <param name="nodeName">节点名</param>
        public void AddSubXmlNode(string xpath, int offset, string nodeName)
        {
            this.AddSubXmlNode(xpath, offset, nodeName, null);
        }

        /// <summary>
        /// 在指定节点下添加Xml子节点
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或子节点创建出错
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要指定第几个节点</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点文本信息</param>
        public void AddSubXmlNode(string xpath, int offset, string nodeName, string innerText)
        {
            this.MoveToNode(xpath, offset);
            this.AddSubXmlNode(nodeName, innerText);
        }

        /// <summary>
        /// 在当前节点下添加Xml兄弟节点
        /// 当前节点即最后一次添加/操作的节点
        /// 异常：
        /// XmlWriterException   父节点不存在或兄弟节点创建出错
        /// </summary>
        /// <param name="nodeName">节点名</param>
        public void AddSibXmlNode(string nodeName)
        {
            this.AddSibXmlNode(nodeName, null);
        }

        /// <summary>
        /// 在当前节点下添加Xml兄弟节点
        /// 当前节点即最后一次添加/操作的节点
        /// 异常：
        /// XmlWriterException   父节点不存在或兄弟节点创建出错
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点文本信息</param>
        public void AddSibXmlNode(string nodeName, string innerText)
        {
            if (this.parentNode == null)
            {
                throw new XmlWriterException(35, "parent node not found");
            }
            try
            {
                XmlNode sunNode = this.Document.CreateElement(nodeName);
                if (innerText != null && string.Empty != innerText)
                {
                    sunNode.InnerText = innerText;
                }
                this.parentNode.AppendChild(sunNode);
                this.CurrentNode = sunNode;
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "兄弟节点创建出错";
                throw new XmlWriterException(42, expInfo);
            }
        }

        /// <summary>
        /// 在指定节点下添加Xml兄弟节点
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或父节点不存在或兄弟节点创建出错
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点信息</param>
        public void AddSibXmlNode(string xpath, string nodeName, string innerText)
        {
            this.MoveToNode(xpath, 0);
            this.AddSibXmlNode(nodeName, innerText);
        }

        /// <summary>
        /// 在当前节点下添加属性
        /// 当前节点即最后一次添加/操作的节点 
        /// 异常：
        /// XmlWriterException   当前节点不存在或参数不合法或节点属性创建出错
        /// </summary>
        /// <param name="attrName">属性名，属性名不能为空</param>
        /// <param name="attrValue">属性值</param>
        public void AddXmlAtrribute(string attrName, string attrValue)
        {
            if (this.CurrentNode == null)
            {
                throw new XmlWriterException(36, "no target node");
            }
            if (attrName == null || "" == attrName)
            {
                throw new XmlWriterException(37, "illegal parameter");
            }
            try
            {
                XmlAttribute xmlAttr = this.Document.CreateAttribute(attrName);
                xmlAttr.Value = attrValue;
                this.CurrentNode.Attributes.Append(xmlAttr);
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "节点属性创建出错";
                throw new XmlWriterException(43, expInfo);
            }
        }

        /// <summary>
        /// 在指定节点下添加属性
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或参数不合法或节点属性创建出错
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要在第几个节点下添加属性</param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性值</param>
        public void AddXmlAtrribute(string xpath, int offset, string attrName, string attrValue)
        {
            if (attrName == null || string.Empty == attrName)
            {
                throw new XmlWriterException(37, "illegal parameter");
            }
            this.MoveToNode(xpath, offset);
            this.AddXmlAtrribute(attrName, attrValue);
        }

        /// <summary>
        /// 更新指定节点指定属性
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或参数不合法
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要修改第几个节点的属性</param>
        /// <param name="attrname">属性名</param>
        /// <param name="attrvalue">属性值</param>
        public void UpdateXmlAttribute(string xpath, int offset, string attrName, string attrValue)
        {
            if (attrName == null || string.Empty == attrName)
            {
                throw new XmlWriterException(37, "illegal parameter");
            }
            this.MoveToNode(xpath, offset);
            foreach (object obj in this.CurrentNode.Attributes)
            {
                XmlAttribute attribute = (XmlAttribute)obj;
                if (attribute.Name.ToLower() == attrName.ToLower())
                {
                    attribute.Value = attrValue;
                    break;
                }
            }
        }

        /// <summary>
        /// 更新指定节点指定属性
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或参数不合法
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要修改第几个节点的属性</param>
        /// <param name="index">要修改第几个属性</param>
        /// <param name="attrvalue">属性值</param>
        public void UpdateXmlAttribute(string xpath, int offset, int index, string attrvalue)
        {
            if (attrvalue == null)
            {
                throw new XmlWriterException(37, "illegal parameter");
            }
            this.MoveToNode(xpath, offset);
            if (this.CurrentNode.Attributes.Count > index)
            {
                this.CurrentNode.Attributes[index].Value = attrvalue;
                return;
            }
        }

        /// <summary>
        /// 更新当前节点文本信息节点
        /// </summary>
        /// <param name="innerText">节点文本信息</param>
        public void UpdateInnertext(string innerText)
        {
            if (this.CurrentNode != null && innerText != null)
            {
                this.CurrentNode.InnerText = innerText;
            }
        }
        
        public void UpdateInnertext(string xpath, string innerText)
        {
            this.MoveToNode(xpath, 0);
            this.UpdateInnertext(innerText);
        }


        public void UpdateInnertext(string xpath, int offset, string innerText)
        {
            this.MoveToNode(xpath, offset);
            this.UpdateInnertext(innerText);
        }

        /// <summary>
        /// 在当前节点后添加注释信息
        /// 当前节点即最后一次添加/操作的节点 
        /// 异常：
        /// XmlWriterException   注释创建出错
        /// <备注>
        /// 若刚创建xml写入器实例还未添加根节点，则在根节点之前添加注释
        /// 若需要Xml描述信息，请确保在调用该方法之前使用相关方法添加
        /// </备注>
        /// </summary>
        /// <param name="comment">注释内容</param>
        public void AddComment(string comment)
        {
            try
            {
                XmlComment xmlComment = this.Document.CreateComment(comment);
                if (this.CurrentNode == null)
                {
                    this.Document.AppendChild(xmlComment);
                }
                else
                {
                    this.CurrentNode.AppendChild(xmlComment);
                }
            }
            catch (Exception ex)
            {
                string expInfo = ex.ToString();
                this.SaveErrInfo(expInfo);
                expInfo = "注释创建出错";
                throw new XmlWriterException(44, expInfo);
            }
        }

        /// <summary>
        /// 在指定节点下添加注释
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或注释创建出错
        /// </summary>
        /// <备注>
        /// 若刚创建xml写入器实例还未添加根节点，则在根节点之前添加注释
        /// 若需要Xml描述信息，请确保在调用该方法之前使用相关方法添加
        /// </备注>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要在第几个节点下添加注释</param>
        /// <param name="comment">注释内容</param>
        public void AddComment(string xpath, int offset, string comment)
        {
            this.MoveToNode(xpath, offset);
            this.AddComment(comment);
        }

        /// <summary>
        /// 移除当前Xml的所有子节点和/或特性
        /// 将保留根节点和描述信息
        /// </summary>
        public void RemoveAll()
        {
            XmlNode root = this.Document.DocumentElement;
            if (root == null)
            {
                return;
            }
            this.parentNode = null;
            this.CurrentNode = root;
            root.RemoveAll();
        }

        /// <summary>
        /// 移除指定节点及其所有子节点和/或特性
        /// 若指定节点为根节点，则只移除根节点所有子节点及其特性
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">第几个节点</param>
        public void RemoveChild(string xpath, int offset)
        {
            this.MoveToNode(xpath, offset);
            if (this.parentNode != null)
            {
                this.parentNode.RemoveChild(this.CurrentNode);
                this.CurrentNode = this.parentNode;
                this.parentNode = this.CurrentNode.ParentNode;
            }
            else
            {
                this.RemoveAll();
                this.CurrentNode = null;
            }
        }

        /// <summary>
        /// 移动到指定节点
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要移动到第几个节点</param>
        public void MoveToNode(string xpath, int offset)
        {
            this.CurrentNode = null;
            XmlNodeList nodeList = this.Document.SelectNodes(xpath);
            if (nodeList == null || nodeList.Count <= offset)
            {
                string errorInfo = "xpath illeagal or no target node";
                this.SaveErrInfo(errorInfo);
                throw new XmlWriterException(36, errorInfo);
            }
            this.CurrentNode = nodeList[offset];
            if (this.CurrentNode.ParentNode.NodeType != XmlNodeType.Document)
            {
                this.parentNode = this.CurrentNode.ParentNode;
                return;
            }
            this.parentNode = null;
        }

        private XmlNode parentNode;

        private bool hasRootNode;

        private bool hasDecl;
    }
}
