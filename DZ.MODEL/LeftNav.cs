using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
   public class LeftNav
    {
         //"menuid": "backcheck",
         //       "icon": "icon-sys",
         //       "menuname": "退单审核",
         //       "menus": [{
         //           "menuid": "111",
         //           "menuname": "A组退单审核",
         //           "icon": "icon-nav",
         //           "url": "#"
         //       },
       
       public string menuid { get; set; }
       public string icon { get; set; }
       public string menuname { get; set; }
       public string url { get; set; }
       public List<LeftNav> menus { get; set; }
    }
}
