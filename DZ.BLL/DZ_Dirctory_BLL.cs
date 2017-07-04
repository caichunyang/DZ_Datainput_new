using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.BLL
{
   public class DZ_Dirctory_BLL
    {
       DLL.DZ_Dirctory_DAL dal = new DLL.DZ_Dirctory_DAL();
       /// <summary>
       /// 
       /// </summary>
       /// <param name="rowlistid">ems—filetype为11</param>
       /// <returns></returns>
       public List<string[]> GetEntities(string rowlistid)
       {
           return dal.GetEntities(rowlistid);
       }
    /// <summary>
    /// 主要用于平安的 合格率记录
    /// </summary>
    /// <param name="objname"></param>
    /// <param name="name"></param>
    /// <param name="key"></param>
    /// <returns></returns>
       public List<string[]> GetDictionaryEntities(string objname, string name, string key)
       {
           return dal.GetDictionaryEntities(objname, name, key);
       }
    }
}
