using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            httppost();
            Console.ReadKey();
        }
        public static void httppost()
        {
         //  string url = "http://localhost:52323/home/CheckuserAndPwd?userid=yang&pwd=yang123";
           // string url = "http://localhost:52323/PingAnInput/Insert?model=E2B905D9803BBA1BDB6CB2730BDA0C88C895E4808E2E8485C6270709F5E4A078A2FC0556120D263A7451A22B1FA5E1A0E6193BE9BC2C0FC9C963663688B9DFF336B709758513485585F9A4A435A46EAB8E4B9869E67A47D1CD7CF5340E3A20CE778EBF4C6196DA53F783176B5926E0F5DB4A90173395CB8FECC82535C67E8DE4C2935DD42F3C2FDE442298DFBCF3433962F8332B787102CA6104D10F4DF55D72A10E0A47E64671F39F2EB1F903CAFFC8";
            string url = "http://localhost:52323/PingAnInput/Insert?model=E2B905D9803BBA1BDB6CB2730BDA0C88C895E4808E2E8485C6270709F5E4A078A2FC0556120D263A7451A22B1FA5E1A0E6193BE9BC2C0FC9C963663688B9DFF336B709758513485585F9A4A435A46EAB8E4B9869E67A47D1CD7CF5340E3A20CE778EBF4C6196DA53F783176B5926E0F5DB4A90173395CB8FECC82535C67E8DE4C2935DD42F3C2FDE442298DFBCF3433962F8332B787102CA6104D10F4DF55D72A10E0A47E64671F39F2EB1F903CAFFC8";
            string sendmsg = "{\"userid\":\"yang\",\"pwd\":\"yang123\"}";
            var result = HttpPost(url, sendmsg);
          Console.WriteLine(result.ToString());
        }

       
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Flush();
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            
            return retString;
        }
    }
}
