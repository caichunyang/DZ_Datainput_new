using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;

namespace DZ.BLL
{
   public class DZ_IMAGE_EMS_BLL
    {
       DZ_IMAGE_EMS_DAL dal = new DZ_IMAGE_EMS_DAL();
        public List<MODEL.DZ_IMAGE_EMS> GetAllList()
        { 
            return dal.GetAllList();
        }
        public List<string[]> SelectImage_EMS(string wherestr, int pagesize, int page, out int count)
        {
           
            return dal.SelectImage_EMS(wherestr, pagesize, page, out count);
        }
        public List<string[]> SelectCenterImage_EMS(string wherestr, int pagesize, int page, out int count)
        {

            return dal.SelectCenterImage_EMS(wherestr, pagesize, page, out count);
        }
       
    }
}
