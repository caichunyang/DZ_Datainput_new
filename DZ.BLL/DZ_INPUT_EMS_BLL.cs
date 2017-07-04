using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.BLL
{
  public  class DZ_INPUT_EMS_BLL
    {
      DLL.DZ_INPUT_EMS_DAL dal =new  DLL.DZ_INPUT_EMS_DAL();
      public List<string[]> LoadEntities()
      {
          return dal.LoadEntities();
      }
    }
}
