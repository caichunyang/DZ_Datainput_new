using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;

namespace DZ.BLL
{
  public  class DZ_ROLEMODULE_BLL
    {
         public List<int[]> GetRoleModule(int roleid, int moduleid)
        {
            DLL.DZ_ROLEMODULE_DAL dal = new DZ_ROLEMODULE_DAL();
            return dal.GetRoleModule(roleid,moduleid);
        }
        public bool AddRoleModule(int roleid,int moduleid)
        {
            DLL.DZ_ROLEMODULE_DAL dal = new DZ_ROLEMODULE_DAL();
            if (dal.GetRoleModule(roleid, moduleid).Count<=0)
            {
                return dal.AddRoleModule(roleid, moduleid);
            }
            else
            {
                return false;
            }
        }
        public bool DeleteRoleModule(int roleid, int moduleid)
        {
            DLL.DZ_ROLEMODULE_DAL dal = new DZ_ROLEMODULE_DAL();
            return dal.DeleteRoleModule(roleid, moduleid);
        }
    }
}
