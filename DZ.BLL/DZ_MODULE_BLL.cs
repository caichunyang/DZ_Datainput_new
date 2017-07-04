using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.MODEL;
using DZ.DLL;

namespace DZ.BLL
{
    public class DZ_MODULE_BLL
    {
        public bool Add(DZ_MODULE modle)
        {
            DZ_MODULE_DAL dal = new DZ_MODULE_DAL();
            return dal.Add(modle);
        }
        public List<DZ_MODULE> GetEntites()
        {
            DZ_MODULE_DAL dal = new DZ_MODULE_DAL();
            return dal.GetEntites();
        }
        public List<MODEL.DZ_MODULE> LoadEntites(string where)
        {
            DZ_MODULE_DAL dal = new DZ_MODULE_DAL();
            return dal.LoadEntites(where);
        }
        public bool Update(DZ_MODULE modle)
        {
            DZ_MODULE_DAL dal = new DZ_MODULE_DAL();
            return dal.Update(modle);
        }
        public List<MODEL.DZ_MODULE> GetRoleRightByRoleid(int roleid)
        {
            DZ_MODULE_DAL dal = new DZ_MODULE_DAL();
            return dal.GetRoleRightByRoleid(roleid);
        }
       
    }
}
