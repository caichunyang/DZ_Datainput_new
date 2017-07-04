using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.Common
{
    public static class WriteLog
    {
        public static object locker = new object();

        /// <summary>
        /// 将异常打印到LOG文件
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="LogAddress">日志文件地址</param>
        /// <param name="Tag">传入标签（这里用于标识函数由哪个线程调用）</param>
        public static void Write2Log(Exception ex, string Tag = "", string LogAddress = "")
        {
            lock (locker)
            {
                //如果日志文件为空，则默认在Debug目录下新建 YYYY-mm-dd_Log.log文件
                if (LogAddress == "")
                {
                    string addr = Directory.GetCurrentDirectory();
                    LogAddress = Environment.CurrentDirectory + '\\' +
                        DateTime.Now.Year + '-' +
                        DateTime.Now.Month + '-' +
                        DateTime.Now.Day + "_Log.log";
                }
                //if (!File.Exists(LogAddress))
                //{
                //    File.Create(LogAddress);
                //    //Directory.CreateDirectory(LogAddress);
                //}
                //把异常信息输出到文件
                StreamWriter sw = new StreamWriter(LogAddress,true,Encoding.UTF8);
                sw.WriteLine(String.Concat('[', DateTime.Now.ToString(), "] Tag:" + Tag));
                sw.WriteLine("异常信息：" + ex.Message);
                sw.WriteLine("异常对象：" + ex.Source);
                sw.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
                sw.WriteLine("触发方法：" + ex.TargetSite);
                sw.WriteLine();
                sw.Close();
                
            }
        }
    }
}
