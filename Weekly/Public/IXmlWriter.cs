﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Weekly.Public
{
    public interface IXmlWriter
    {
        /// <summary>
        /// 当前操纵的Xmldocument对象
        /// </summary>
        XmlDocument Document { get; }

        /// <summary>
        /// 当前正在操作的XmlNode节点
        /// </summary>
        XmlNode CurrentNode { get; }

        /// <summary>
        /// 最后一次异常信息
        /// </summary>
        string LastErrInfo { get; }

        /// <summary>
        /// 从指定的 URL 加载 XML 文档
        /// 异常：
        /// XmlWriterException   XML 中有加载或分析错误
        /// </summary>
        /// <param name="xmlFileName">文件的 URL，该文件包含要加载的 XML 文档。 
        /// URL 既可以是本地文件，也可以是 HTTP URL（Web 地址）。 </param>
        /// <returns>成功加载Xml文档后的XmlDocument</returns>
        XmlDocument Load(string xmlFileName);

        /// <summary>
        /// 从指定的字符串加载 XML 文档
        /// 异常：
        /// XmlWriterException   XML 中有加载或分析错误
        /// </summary>
        /// <param name="strXml">包含要加载的 XML 文档的字符串</param>
        /// <returns>成功加载Xml文档字符串后的XmlDocument</returns>
        XmlDocument LoadXml(string strXml);

        /// <summary>
        /// 将 XML 文档保存到指定的文件
        /// 异常：
        /// XmlWriterException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="filename">要将文档保存到其中的文件的位置</param>
        void Save(string filename);

        /// <summary>
        /// 将 XML 文档保存到指定的流
        /// 异常：
        /// XmlWriterException  此操作不产生格式良好的 XML 文档
        /// （例如，没有文档节点或重复的 XML 声明）。 
        /// </summary>
        /// <param name="outStream">要保存到其中的流</param>
        void Save(Stream outStream);

        /// <summary>
        /// 将 XML 文档保存到指定的 TextWriter
        /// 异常：
        /// XmlWriterException  此操作不产生格式良好的 XML 文档
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
        /// 添加一个具有指定值的 XmlDeclaration 节点
        /// xml描述信息必须置于文档首，在创建写入器以后应最先调用该方法添加描述信息
        /// version必须为“1.0”，standalone值必须是“yes”或“no”。 
        /// 异常：
        /// XmlWriterException   已添加描述节点或version，standalone 的值非法
        /// </summary>
        /// <param name="version">Xml版本信息，必须为"1.0"</param>
        void AddXmlDeclaration(string version);

        /// <summary>
        /// 添加一个具有指定值的 XmlDeclaration 节点
        /// xml描述信息必须置于文档首，在创建写入器以后应最先调用该方法添加描述信息
        /// version必须为“1.0”，standalone值必须是“yes”或“no”。 
        /// 异常：
        /// XmlWriterException   已添加描述节点或version，standalone 的值非法
        /// </summary>
        /// <param name="version">Xml版本信息，必须为"1.0"</param>
        /// <param name="encoding">编码方式特性，默认的编码方式为 UTF-8</param>
        void AddXmlDeclaration(string version, string encoding);

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
        void AddXmlDeclaration(string version, string encoding, string standalone);

        /// <summary>
        /// 添加根节点
        /// 一个Xml文档只有一个根节点
        /// 异常：
        /// XmlWriterException   已添加根节点或创建节点失败
        /// </summary>
        /// <param name="nodeName">节点名</param>
        void AddXmlRootNode(string nodeName);

        /// <summary>
        /// 添加根节点
        /// 一个Xml文档只有一个根节点
        /// 异常：
        /// XmlWriterException   已添加根节点或创建节点失败
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点文本信息</param>
        void AddXmlRootNode(string nodeName, string innerText);

        /// <summary>
        /// 在当前节点下添加Xml子节点
        /// 当前节点即最后一次添加/操作的节点
        /// 异常：
        /// XmlWriterException   无当前节点或子节点创建出错
        /// </summary>
        /// <param name="nodeName">节点名</param>
        void AddSubXmlNode(string nodeName);

        /// <summary>
        /// 在当前节点下添加Xml子节点
        /// 当前节点即最后一次添加/操作的节点
        /// 异常：
        /// XmlWriterException   无当前节点或子节点创建出错
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点文本信息</param>
        void AddSubXmlNode(string nodeName, string innerText);

        /// <summary>
        /// 在指定节点下添加Xml子节点
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或子节点创建出错
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要指定第几个节点</param>
        /// <param name="nodeName">节点名</param>
        void AddSubXmlNode(string xpath, int offset, string nodeName);

        /// <summary>
        /// 在指定节点下添加Xml子节点
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或子节点创建出错
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要指定第几个节点</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点文本信息</param>
        void AddSubXmlNode(string xpath, int offset, string nodeName, string innerText);

        /// <summary>
        /// 在当前节点下添加Xml兄弟节点
        /// 当前节点即最后一次添加/操作的节点
        /// 异常：
        /// XmlWriterException   父节点不存在或兄弟节点创建出错
        /// </summary>
        /// <param name="nodeName">节点名</param>
        void AddSibXmlNode(string nodeName);

        /// <summary>
        /// 在当前节点下添加Xml兄弟节点
        /// 当前节点即最后一次添加/操作的节点
        /// 异常：
        /// XmlWriterException   父节点不存在或兄弟节点创建出错
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点文本信息</param>
        void AddSibXmlNode(string nodeName, string innerText);

        /// <summary>
        /// 在指定节点下添加Xml兄弟节点
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或父节点不存在或兄弟节点创建出错
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="innerText">节点信息</param>
        void AddSibXmlNode(string xpath, string nodeName, string innerText);

        /// <summary>
        /// 在当前节点下添加属性
        /// 当前节点即最后一次添加/操作的节点 
        /// 异常：
        /// XmlWriterException   当前节点不存在或参数不合法或节点属性创建出错
        /// </summary>
        /// <param name="attrName">属性名，属性名不能为空</param>
        /// <param name="attrValue">属性值</param>
        void AddXmlAtrribute(string attrName, string attrValue);

        /// <summary>
        /// 在指定节点下添加属性
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或参数不合法或节点属性创建出错
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要在第几个节点下添加属性</param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性值</param>
        void AddXmlAtrribute(string xpath, int offset, string attrName, string attrValue);

        /// <summary>
        /// 更新指定节点指定属性
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或参数不合法
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要修改第几个节点的属性</param>
        /// <param name="attrname">属性名</param>
        /// <param name="attrvalue">属性值</param>
        void UpdateXmlAttribute(string xpath, int offset, string attrname, string attrvalue);

        /// <summary>
        /// 更新指定节点指定属性
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在或参数不合法
        /// </summary>
        /// <param name="xpath">要匹配的xpath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要修改第几个节点的属性</param>
        /// <param name="index">要修改第几个属性</param>
        /// <param name="attrvalue">属性值</param>
        void UpdateXmlAttribute(string xpath, int offset, int index, string attrvalue);

        /// <summary>
        /// 更新当前节点文本信息节点
        /// </summary>
        /// <param name="innerText">节点文本信息</param>
        void UpdateInnertext(string innerText);

        void UpdateInnertext(string xpath, string innerText);

        void UpdateInnertext(string xpath, int offset, string innerText);

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
        void AddComment(string comment);

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
        void AddComment(string xpath, int index, string comment);

        /// <summary>
        /// 移除当前Xml的所有子节点和/或特性
        /// 将保留根节点和描述信息
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// 移除指定节点及其所有子节点和/或特性
        /// 若指定节点为根节点，则只移除根节点所有子节点及其特性
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">第几个节点</param>
        void RemoveChild(string xpath, int offset);

        /// <summary>
        /// 移动到指定节点
        /// 异常：
        /// XmlWriterException   xpath 表达式错误或目标节点不存在
        /// </summary>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <param name="offset">要移动到第几个节点</param>
        void MoveToNode(string xpath, int offset);

    }
}
