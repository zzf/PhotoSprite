using System;
using System.Collections.Generic;
using System.Xml;
using PhotoSprite.Plugins;

/// <summary>
/// 统一文本读取
/// </summary>
namespace PhotoSprite.Plugins
{
    public class I18NS
    {
        public void initialize()
        {
            if (m_isInit) return;
            var reader = XMLUtil.GetXMLFromString("i18nAppStrings");
            if (null == reader)
            {
                Logger.Instance().Log("i18nAppStrings reader == null");
                return;
            }
            while (reader.Read())
            {
                if (XMLUtil.filterElement(reader)) continue; ;
                uint key = XmlConvert.ToUInt32(reader.GetAttribute("key"));
                string strValue = reader.GetAttribute("value");
                m_stringMap.Add(key, strValue);
            }
            m_isInit = true;
        }

        public static string GetStr(uint key)
        {
            return instance.GetString(key);
        }
        public string GetSeparateResString(uint key, int key2, char sp1 = ';', char sp2 = ':')
        {
            string str = string.Empty;
            if (m_stringMap.TryGetValue(key, out str))
            {
                string[] strarray = str.Split(sp1);
                for (int i = 0; i < strarray.Length; ++i)
                {
                    string[] substrarray = strarray[i].Trim().Split(sp2);
                    if (substrarray.Length.Equals(2))
                    {
                        int first = int.Parse(substrarray[0].Trim());
                        if (first.Equals(key2))
                        {
                            return substrarray[1].Trim();
                        }
                    }
                }

                return str;
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetResString(uint key, params System.Object[] args)
        {
            return string.Format(GetString(key), args);
        }

        public static string GetStrFormat(uint key, params object[] args)
        {
            return string.Format(GetStr(key), args);
        }

        public string GetString(uint key)
        {
            if (m_stringMap.ContainsKey(key))
                return m_stringMap[key];
            else
                return string.Empty;
        }

        private Dictionary<uint, string> m_stringMap;

        private I18NS()
        {
            m_stringMap = new Dictionary<uint, string>();
        }

        private bool m_isInit = false;

        private static I18NS s_instance;
        public static I18NS instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new I18NS();
                }
                return s_instance;
            }
        }
    }
}

