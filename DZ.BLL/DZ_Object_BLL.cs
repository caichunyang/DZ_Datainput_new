using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.BLL
{
    public class DZ_Object_BLL
    {
        DLL.DZ_Object_DAL DAL = new DLL.DZ_Object_DAL();
        public List<string[]> GetAllList()
        {
            return DAL.GetAllList();
        }
    }
}
