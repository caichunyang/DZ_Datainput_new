using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.MODEL;
using DZ.DLL;

namespace DZ.BLL
{
   public class DZ_ROLE_BLL
   {
       DZ_ROLE_DAL dal = new DZ_ROLE_DAL();
       public List<DZ_ROLE> GetEntities()
       {
           return dal.GetEntities();
       }
       public List<int> GetUserrole(string userid)
       {
           return dal.GetUserrole(userid);
       }
    }
}
