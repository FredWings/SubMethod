using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Weekly.Public
{
    public interface IXmlReader
    {
        /// <summary>
        /// 当前操作的Xmldocument对象
        /// </summary>
        XmlDocument Document { get; set; }

        /// <summary>
        /// 最后一次异常信息
        /// </summary>
        string LastErrInfo { get; }

        /// <summary>
        /// 从指定的URL加载XML文档
        /// </summary>
        /// <param name="xmlFileName">文件的URL，该文件包含要加载的XML文档
        /// URL既可以是本地文件，也可以是HTTP URL（Web地址）</param>
        /// <returns>成功加载xml文档后的XmlDocument</returns>
        XmlDocument Load(string xmlFileName);

        /// <summary>
        /// 从指定的字符串加载 XML 文档
        /// 异常：
        /// XmlReaderException   XML 中有加载或分析错误
        /// </summary>
        /// <param name="strXml">包含要加载的 XML 文档的字符串</param>
        /// <returns>成功加载Xml文档字符串后的XmlDocument</returns>
        XmlDocument LoadXml(string strXml);


        /// <summary>
        /// 将 XML 文档保存到指定的文件
        /// 异常：
        /// XmlReaderException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="filename">要将文档保存到其中的文件的位置</param>
        void Save(string filename);

        /// <summary>
		/// 将 XML 文档保存到指定的流
		/// 异常：
		/// XmlReaderException  此操作不产生格式良好的 XML 文档
		/// （例如，没有文档节点或重复的 XML 声明）。 
		/// </summary>
		/// <param name="outStream">要保存到其中的流</param>
        void Save(Stream outStream);

        /// <summary>
		/// 将 XML 文档保存到指定的 TextWriter
		/// 异常：
		/// XmlReaderException  此操作不产生格式良好的 XML 文档
		/// （例如，没有文档节点或重复的 XML 声明）。 
		/// </summary>
		/// <param name="outStream">要向其中保存内容的 TextWriter</param>
        void Save(TextWriter writer);


        /// <summary>
		/// 获取当前操纵的Xml文档的string格式串
		/// </summary>
		/// <returns>当前Xml文档的string格式串</returns>
        string SaveToString();

        /// <summary>
		/// 获取匹配XPath表达式的第offset个节点XmlNode.
		/// 取到的节点将保存为当前操作节点
		/// 异常：
		/// XmlReaderException  xpath 表达式错误或目标节点不存在
		/// </summary>
		/// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
		/// <param name="offset">要获取第几个节点</param>
		/// <returns>与 XPath 查询匹配的第offset个 XmlNode</returns>
        XmlNode GetXmlNode(string xpath, int offset);

        /// <summary>
		/// 获取紧接在当前节点之后的节点
		/// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
		/// </summary>
		/// <returns>紧接在当前节点之后的XmlNode</returns>
        XmlNode GetNextSibNode();

        /// <summary>
		/// 获取当前节点（对于可以具有父级的节点）的父级
		/// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
		/// </summary>
		/// <returns>当前节点父节点XmlNode</returns>
        XmlNode GetParentNode();

        /// <summary>
		/// 获取匹配XPath表达式的节点集合XmlNodeList.
		/// 异常：
		/// XmlReaderException  xpath 表达式错误或目标节点不存在
		/// </summary>
		/// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
		/// <returns>包含匹配 XPath 查询的节点集合XmlNodeList</returns>
        XmlNodeList GetXmlNodeList(string xpath);


        /// <summary>
		/// 获取当前节点的第index个属性
		/// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
		/// </summary>
		/// <param name="index">属性索引</param>
		/// <returns>当前节点的第index个XmlAttribute属性</returns>
        XmlAttribute GetXmlAttribute(int index);


        /// <summary>
		/// 获取当前节点的名为attrName的属性
		/// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
		/// </summary>
		/// <param name="xmlAttrName">属性名</param>
		/// <returns>当前节点的名为attrName的XmlAttribute属性</returns>
        XmlAttribute GetXmlAttribute(string xmlAttrName);

        /// <summary>
		/// 获取匹配xpath表达式的第offset个节点下名为xmlAttrName的属性.
		/// 异常：
		/// XmlReaderException  xpath 表达式错误或目标节点不存在
		/// </summary>
		/// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
		/// <param name="offset">要获取第几个节点的属性</param>
		/// <param name="xmlAttributeName">要匹配的属性名称</param>
		/// <returns>匹配xpath表达式的第offset个节点下名为xmlAttrName的属性</returns>
        XmlAttribute GetXmlAttribute(string xpath, int offset, string xmlAttrName);

        /// <summary>
		/// 获取匹配xpath表达式的第offset个节点下第index个属性.
		/// 异常：
		/// XmlReaderException  xpath 表达式错误或目标节点不存在
		/// </summary>
		/// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
		/// <param name="offset">要获取第几个节点的属性</param>
		/// <param name="index">要匹配第几个属性</param>
		/// <returns>匹配xpath表达式的第offset个节点下第index个属性</returns>
        XmlAttribute GetXmlAttribute(string xpath, int offset, int index);

        /// <summary>
		/// 获取当前节点的第index个属性的值
		/// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
		/// </summary>
		/// <param name="index">属性索引</param>
		/// <returns>当前节点的第index个属性的值</returns>
        string GetXmlAttrValue(int index);

        /// <summary>
		/// 获取当前节点的名为attrName的属性的值
		/// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
		/// </summary>
		/// <param name="xmlAttrName">属性名</param>
		/// <returns>当前节点的名为attrName的属性的值</returns>
        string GetXmlAttrValue(string xmlAttrName);

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
        string GetXmlAttrValue(string xpath, int offset, string xmlAttrName);

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
        string GetXmlAttrValue(string xpath, int offset, int index);

        /// <summary>
		/// 读取当前节点文本描述信息
		/// 当前节点即使用GetXmlNode或MoveToNode方法定位到的节点
		/// </summary>
		/// <returns>当前节点文本描述信息</returns>
        string GetInnerText();

        /// <summary>
		/// 获取匹配xpath表达式的第0个节点的文本描述信息
		/// 异常：
		/// XmlReaderException  xpath 表达式错误或目标节点不存在
		/// </summary>
		/// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
		/// <returns>匹配xpath表达式的第offset个节点的文本描述信息</returns>
        string GetInnerText(string xpath);

        /// <summary>
		/// 获取匹配xpath表达式的第offset个节点的文本描述信息
		/// 异常：
		/// XmlReaderException  xpath 表达式错误或目标节点不存在
		/// </summary>
		/// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
		/// <param name="offset">要获取第几个节点的描述信息</param>
		/// <returns>匹配xpath表达式的第offset个节点的文本描述信息</returns>
        string GetInnerText(string xpath, int offset);

        /// <summary>
		/// 移动到指定节点
		/// 异常：
		/// XmlReaderException   xpath 表达式错误或目标节点不存在
		/// </summary>
		/// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
		/// <param name="offset">要移动到第几个节点</param>
        void MoveToNode(string xpath, int offset);

    }
}
