using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
   public  class DZ_ROLE
    {
        public string roleid { get; set; } 
       public string rolename { get; set; }
       public string rolememo { get; set; }
       public DateTime createtime { get; set; }
       public int disablestatus { get; set; }
    }
}
