using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using PhotoSprite.Plugins;

namespace PhotoSprite.Plugins
{
    public class JsonUtil
    {
        /// <summary>
        /// 判断jison串是否包含相应的key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool IsDataContainkeys(JsonData data, params string[] keys)
        {
            string ret = string.Empty;
            return IsDataContainkeys(data, out ret, keys);
        }

        /// <summary>
        /// 判断jison串是否包含相应的key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ret"> 返回不存在的key字符串 </param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool IsDataContainkeys(JsonData data, out string ret, params string[] keys)
        {
            ret = string.Empty;
            if (keys == null || keys.Length == 0) return false;
            for (int i = 0; i < keys.Length; ++i)
            {
                if (!((IDictionary)data).Contains(keys[i]))
                {
                    ret += keys[i];
                    if (i != keys.Length - 1)
                        ret += ", ";
                }
            }

            return string.IsNullOrEmpty(ret);
        }

        /// <summary>
        /// 尝试获取key值对应的value
        /// </summary>
        public static string TryGetValue(JsonData data, string key, string defaultValue)
        {
            if (IsDataContainkeys(data, key))
            {
                return data[key].ToString();
            }
            return defaultValue;
        }

        public static float TryGetValue(JsonData data, string key, float defaultValue)
        {
            if (IsDataContainkeys(data, key))
            {
                return System.Convert.ToSingle(data[key].ToString());
            }
            return defaultValue;
        }

        public static List<T> ConvetJsonToList<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new List<T>();
            }

            return JsonMapper.ToObject<List<T>>(json);
        }

        public static string ConvertListToJsonStr<T>(List<T> list)
        {
            JsonData jsonData = new JsonData();
            for (int i = 0; i < list.Count; i++)
            {
                jsonData.Add(list[i]);
            }
            return jsonData.ToJson();
        }
    }
}