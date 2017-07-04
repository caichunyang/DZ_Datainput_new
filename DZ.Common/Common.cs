using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.Common
{
    public class Common
    {
        //Oracle 连接语句
        public string OraStr = "";
        //图片存储位置
        public string ImagePath;
        //EMS图片存储位置
        public string EMSImagePath;
        //服务器连接路径
        public string ServerAddress = "";
        //存储服务器的Key
        public string ServerKeyStr = "123";
        //Cookie本地存储时间,单位:小时

        public long CookieTimeOut = 1;
        //public void GetConfigINIFile()
        //{
        //    INIFile INIFile = new INIFile();
        //    INIFile.INIPath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini";
        //    OraStr = INIFile.GetStrFromINI("Server", "OraStr", "");
        //    ServerAddress = INIFile.GetStrFromINI("Server", "ServerAddress", "");
        //    CookieTimeOut = Val(INIFile.GetStrFromINI("Server", "CookieTimeOut", ""));
        //    EMSImagePath = INIFile.GetStrFromINI("Server", "EMSImagePath", "");
        //    EMSImagePath = Replace((EMSImagePath + "\\"), "\\\\", "\\");
        //}
        public string PostHttpResponse(string StrUrl, string PostData)
        {
            try
            {
                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(StrUrl);
                req.Credentials = System.Net.CredentialCache.DefaultCredentials;
                req.Timeout = 8000;
                //8秒超时的设置
                req.Method = "POST";
                byte[] byte1 = System.Text.Encoding.Default.GetBytes(PostData);
                req.ContentLength = byte1.Length;
                req.ContentType = "application/x-www-form-urlencoded";

                System.IO.Stream requestStream = req.GetRequestStream();
                requestStream.Write(byte1, 0, byte1.Length);
                requestStream.Close();

                //get sp 输出并分析成功还是失败
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "->调度请求超时";
            }
        }
        //public string ReplaceStr(string TheStr)
        //{
        //    //对提交的字符串进行处理
        //    string TheResultStr = "";
        //    //繁体转换为简体
        //    TheResultStr = Microsoft.VisualBasic.Strings.StrConv(TheStr, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
        //    TheResultStr = Replace(TheResultStr, "'", "");
        //    TheResultStr = Replace(TheResultStr, "?", "");
        //    TheResultStr = Replace(TheResultStr, "*", "");
        //    TheResultStr = Replace(TheResultStr, "<", "(");
        //    TheResultStr = Replace(TheResultStr, ">", ")");
        //    TheResultStr = Replace(TheResultStr, "(link)", "<link>");
        //    return TheResultStr;
        //}
         //
       
    }
}
