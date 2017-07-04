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
        public List<MODEL.DZ_IMAGE_EMS> GetAllList()
        { 
            DZ_IMAGE_EMS_DAL dal=new DZ_IMAGE_EMS_DAL ();
            return dal.GetAllList();
        }
    }
}
