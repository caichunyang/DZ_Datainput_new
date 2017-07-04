using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;

namespace DZ.BLL
{
   public class DZ_Recive_BLL
    {
       DZ_Recive dal=new DZ_Recive ();
         public bool CheckExist(string[] dataobj, string objname)
        {
           return dal.CheckExist(dataobj,objname);
        }

         public string[] GetModel(string datastr, string objname)
         {
             return dal.GetModel(datastr, objname);
         }
        public bool Insert(string[] data)
        {
            return dal.Insert(data);
        }
        public bool Update(string[] data)
        {
            return dal.Update(data);
        }
    }
}
