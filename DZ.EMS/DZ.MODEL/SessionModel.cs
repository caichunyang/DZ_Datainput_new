using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
   public class SessionModel
    {
       public DZ_USER User { get; set; }
       /// <summary>
       /// 模块权限集合
       /// </summary>
        public List<DZ_MODULE> ModuleList { get; set; }
    }
}
