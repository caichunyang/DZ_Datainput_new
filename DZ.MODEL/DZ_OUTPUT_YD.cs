using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
   public class DZ_OUTPUT_YD
   {
       public string UserName { get; set; }
       public string TheDate { get; set; }
       /// <summary>
       /// 当天录入总字段量
       /// </summary>
       public long TheNum { get; set; }
       /// <summary>
       /// 录入被发现有错误的
       /// </summary>
       public long ErrNum { get; set; }
       /// <summary>
       /// 做检查的
       /// </summary>
       public long CheckNum { get; set; }
       /// <summary>
       /// 做检查发现错误的
       /// </summary>
       public long CheckErrNum { get; set; }
  
    }
}
