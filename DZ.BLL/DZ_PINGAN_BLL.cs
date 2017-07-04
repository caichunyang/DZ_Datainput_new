using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;
namespace DZ.BLL
{
   public class DZ_PINGAN_BLL
    {
       DZ_INPUT_PA_DAL dal = new DZ_INPUT_PA_DAL();
        /// <summary>
        /// 平安产量查询
        /// </summary>
        /// <param name="wherestr"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<string[]> LoadPageEntities_PA(string wherestr, int pagesize, int page, out int count)
        {
            return dal.LoadPageEntities_PA(wherestr, pagesize, page, out count);
        }
        public List<string[]> LoadPageEntitiesAllCen_PA(string wherestr, int pagesize, int page, out int count)//wherestr 均未使用参数化查询
        {
            return dal.LoadPageEntitiesAllCen_PA(wherestr, pagesize, page, out count);
        }
        /// <summary>
        /// 查询平安 分中心 最近time_m分钟 录入的人数 和提交速度
        /// </summary>
        /// <param name="objid"></param>
        /// <param name="time_m"></param>
        /// <returns></returns>
        public string[] AllCen_PA_TJ(int objid, int time_m)
        {
            return dal.AllCen_PA_TJ(objid, time_m);
        }
       /// <summary>
       /// 转移阿里平安数据
       /// </summary>
        public void TransferrData()
        {
             dal.TransferrData();
        }
    }
}
