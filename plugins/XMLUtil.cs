using System;
using System.Xml;
using System.IO;
using PhotoSprite.Plugins;

namespace PhotoSprite.Plugins
{
    /// <summary>
    /// 提供操作XML的辅助函数
    /// </summary>
    public class XMLUtil
    {
        /// <summary>
        /// 将xmlContent解析为XmlDocument并返回
        /// </summary>
        public static XmlDocument GetXmlDocFromString(string content)
        {
            XmlDocument ret = null;

            ret = new XmlDocument();
            XmlReaderSettings setttings = new XmlReaderSettings();
            setttings.IgnoreComments = true;
            setttings.IgnoreWhitespace = true;

            TextReader reader = new StringReader(content);
            XmlReader xmlReader = XmlReader.Create(reader, setttings);

            ret.Load(xmlReader);
            return ret;
        }

        public static XmlDocument GetXmlDocFromFile(string fileName)
        {
            if (!fileName.EndsWith(".xml") && !fileName.EndsWith(".bytes"))
                fileName += ".bytes";

            if (!File.Exists(fileName))
            {
                Logger.Instance().Log("getXMLFromFile error, file not exist. Path: " + fileName);
                return null;
            }

            XmlDocument ret = new XmlDocument();
            ret.Load(fileName);
            return ret;
        }

        public static XmlTextReader GetXmlReaderFromFile(string fileName)
        {
            if (!fileName.EndsWith(".xml") && !fileName.EndsWith(".bytes"))
                fileName += ".bytes";

            if (!File.Exists(fileName))
            {
                Logger.Instance().Log("getXMLFromFile error, file not exist. Path: " + fileName);
                return null;
            }

            return new XmlTextReader(fileName);
        }

        /// <summary>
        /// 将xmlContent解析为XmlDocument并返回
        /// </summary>
        public static XmlTextReader GetXMLFromString(string content)
        {
            TextReader reader = new StringReader(content);
            return new XmlTextReader(reader);
        }


        public static int tryGetInt32(XmlNode node, string key, int defaultValue)
        {
            if (null == node.Attributes[key]) return defaultValue;
            return XmlConvert.ToInt32(node.Attributes[key].Value);
        }

        public static float tryGetFloat(XmlNode node, string key, float defaultValue)
        {
            if (null == node.Attributes[key]) return defaultValue;
            return XmlConvert.ToSingle(node.Attributes[key].Value);
        }

        public static string tryGetString(XmlNode node, string key, string defaultValue)
        {
            if (null == node.Attributes[key]) return defaultValue;
            return node.Attributes[key].Value;
        }


        public static int tryGetInt32(XmlTextReader node, string key, int defaultValue)
        {
            string node_att = node.GetAttribute(key);
            if (null == node_att) return defaultValue;
            return XmlConvert.ToInt32(node_att);
        }

        public static long tryGetInt64(XmlTextReader node, string key, long defaultValue)
        {
            string node_att = node.GetAttribute(key);
            if (null == node_att) return defaultValue;
            return XmlConvert.ToInt64(node_att);
        }

        public static float tryGetFloat(XmlTextReader node, string key, float defaultValue)
        {
            string node_att = node.GetAttribute(key);
            if (null == node_att) return defaultValue;
            return XmlConvert.ToSingle(node_att);
        }

        public static string tryGetString(XmlTextReader node, string key, string defaultValue)
        {
            string node_att = node.GetAttribute(key);
            if (null == node_att) return defaultValue;
            return node_att;
        }


        public static bool filterElement(XmlTextReader reader)
        {
            if (reader.NodeType != XmlNodeType.Element) return true;
            if (reader.Name != "item") return true;
            return false;
        }

        /// <summary>
        /// 设置xml的XmlAttribute的值。
        /// 如果没有则创建xml的XmlAttribute。
        /// 如果ignoreNullAndEmpty = true，则检查删除对应的XmlAttribute节点。
        /// </summary>
        /// <param name="xmlEle"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <param name="ignoreNullAndEmpty">忽视value为空，如果忽视：则不创建xml的XmlAttribute节点，并且删除对应的节点</param>
        public static void SetXmlAttribute(XmlElement xmlEle, string attribute, string value, bool ignoreNullAndEmpty)
        {
            if (ignoreNullAndEmpty && string.IsNullOrEmpty(value))
            {
                xmlEle.RemoveAttribute(attribute);
            }
            else
            {
                xmlEle.SetAttribute(attribute, value);
            }
        }

        /// <summary>
        /// 写节点属性
        /// </summary>
        public static void WriteXmlAttributes(XmlDocument doc, XmlNode node, string attributes, string value)
        {
            if (node.Attributes[attributes] != null)
            {
                node.Attributes[attributes].Value = value;
            }
            else
            {
                XmlAttribute attr = doc.CreateAttribute(attributes);
                attr.Value = value;
                node.Attributes.Append(attr);
            }
        }
    }
}