using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DZ.Common
{
    public static class JSON
    {
        public static string SetJson(object TheObject)
        {
            JsonSerializerSettings Json = new JsonSerializerSettings();
            Json.NullValueHandling = NullValueHandling.Ignore;
            //忽略 为Null的值
            //Formatting.Indented 良好的显示格式,带换行和空格
            return (Newtonsoft.Json.JsonConvert.SerializeObject(TheObject, Formatting.None, Json));
        }
        public static T DeserializeObject<T>(string json)
        {
          return  Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        //public string GetJson(string TheStr)
        //{
        //    return (Newtonsoft.Json.JsonConvert.DeserializeObject(TheStr));
        //}
        //把输出结果JSON序列化
        //string GetResPonseJsonStr(string Result, string ReturnStr)
        //{
        //    string TheStr;
        //    if (Result != "success")
        //    {
        //        TheStr = "{<|>Result<|>:<|>" + Result + "<|>,<|>ReturnStr<|>:<|>" + ReturnStr + "<|>}";
        //        TheStr = Replace(TheStr, "<|>", "\"");
        //    }
        //    else
        //    {
        //        TheStr = "{<|>Result<|>:<|>" + Result + "<|>,<|>ReturnStr<|>:[" + ReturnStr + "]}";
        //        TheStr = Replace(TheStr, "<|>", "\"");
        //    }
        //    TheStr = Replace(TheStr, "[[", "[");
        //    TheStr = Replace(TheStr, "]]", "]");
        //    return TheStr;
        //}

    }
}
