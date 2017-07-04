using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
   public class DZ_OutputMonitor_YD
    {
       /// <summary>
       /// 批次号
       /// </summary>
        public string BoxNo { get; set; }
        public string TheDate { get; set; }
        public int Noinput { get; set; }
        public int Input { get; set; }
        /// <summary>
        /// 当天录入总字段量
        /// </summary>
        public long TheNum { get; set; }
        
        public long checkpass { get; set; }

        public long checkerr { get; set; }
        ///// <summary>
        ///// 做检查发现错误的
        ///// </summary>
        //public long CheckErrNum { get; set; }
    }
}
