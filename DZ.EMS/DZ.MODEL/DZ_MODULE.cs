using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
   public class DZ_MODULE
    {
   
       public int Moduleid { get; set; }
       public int Parentid { get; set; }
       public string Moduleaction { get; set; }
       /// <summary>
       /// 模块描述
       /// </summary>
       public string Modulename { get; set; }
       public int Disablestatus { get; set; }
       public string Controler { get; set; }
       public string Action { get; set; }
       /// <summary>
       /// 是否为菜单
       /// </summary>
       public int Isview { get; set; }
       /// <summary>
       /// 标题
       /// </summary>
       public string Title { get; set; }
       /// <summary>
       /// 图标
       /// </summary>
       public string iconCls { get; set; }
       /// <summary>
       /// ischecked
       /// </summary>
       public bool checkedinfo { get; set; }
    }
}
