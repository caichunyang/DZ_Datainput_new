using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.BLL
{
   public class DZ_IMAGE_YD_BLL
    {
       DLL.DZ_IMAGE_YD dal = new DLL.DZ_IMAGE_YD();

       public List<string[]> SelectImage_YD(string where, int pagesize, int page,out int count)
       {
           return dal.SelectImage_YD(where, pagesize, page,out count);
       }
       public List<string[]> SelectCenterImage_YD(string wherestr, int pagesize, int page, out int count)
       {
           return dal.SelectCenterImage_YD(wherestr, pagesize, page, out count);
       }
    }
}
