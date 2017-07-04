using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
   public class dz_input_pingan
    {
       /// <summary>
       /// 用户id
       /// </summary>
       public string userid { get; set; }
       /// <summary>
       /// 图片名称
       /// </summary>
       public string image { get; set; }
       /// <summary>
       /// 录入类别 
       /// </summary>
       public string inputype { get; set; }
       /// <summary>
       /// 录入值
       /// </summary>
       public string inputlvalue { get; set; }
       /// <summary>
       /// 录入字节数
       /// </summary>
       public int inputlength { get; set; }
       /// <summary>
       /// 返回状态
       /// </summary>
       public int returntype { get; set; }
       /// <summary>
       /// 录入时间
       /// </summary>
       public DateTime inputime { get; set; }
    }
}
