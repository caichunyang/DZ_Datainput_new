using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;

namespace DZ.BLL
{
   public class DZ_INPUT_SHJJ_BLL
    {
       DZ_INPUT_SHJJ_DAL dal = new DZ_INPUT_SHJJ_DAL();
       public int RestartCheck(string startdate, string enddate)
       {
           return dal.RestartCheck(startdate, enddate);
       }
       /// <summary>
       /// 重新质检shjj
       /// </summary>
       /// <param name="startdate"></param>
       /// <param name="enddate"></param>
       /// <param name="wherestr"></param>
       /// <returns></returns>
       public int RestartCheck2_shjj(string boxno)
       {
           return dal.RestartCheck2_shjj(boxno);
       }
    }
}
