using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;


namespace DZ.BLL
{
   public class DZ_ShowOutPut_BLL
    {
       /// <summary>
       /// 各用户产量统计
       /// </summary>
       /// <param name="startdate"></param>
       /// <param name="enddate"></param>
       /// <returns></returns>
       public List<string[]> UserOutPutTj(string startdate, string enddate) {
           DZ_ShowOutPut_DAL dal = new DZ_ShowOutPut_DAL();
           return dal.UserOutPutTj(startdate, enddate);
       }
       /// <summary>
       /// 各中心统计
       /// </summary>
       /// <param name="startdate"></param>
       /// <param name="enddate"></param>
       /// <returns></returns>
       public List<string[]> CenterOutPutTJ(string startdate, string enddate)
       {
           DZ_ShowOutPut_DAL dal = new DZ_ShowOutPut_DAL();
           return dal.CenterOutPutTJ(startdate, enddate);
       }
       public List<string[]>[] Image_rc_upTJ(string startdate, string enddate, string casekey)
       {
           DZ_ShowOutPut_DAL dal = new DZ_ShowOutPut_DAL();
           return dal.Image_rc_upTJ(startdate, enddate,casekey);
       }
       public List<List<string>> SelectOutPut(string startdate, string enddate, string casekey, string where,string orderstr)
       {
           DZ_ShowOutPut_DAL dal = new DZ_ShowOutPut_DAL();
           return dal.SelectOutPut(startdate, enddate, casekey,where, orderstr);
       }
    }
}
