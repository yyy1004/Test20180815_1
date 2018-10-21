using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    public class JsonHelper<T>
    {
        /// <summary>
        /// 将对象的List集合转换为序列化为json字符串
        /// </summary>
        /// <param name="list">对象集合</param>
        /// <returns>json字符串</returns>
        public static string ListToJsonString(List<T> list)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("total", list.Count);
            d.Add("rows", list);
           
            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 将一个对象序列化为json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
        public static string ObjectToJsonString(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
