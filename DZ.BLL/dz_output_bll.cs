using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;

namespace DZ.BLL
{
    public class dz_output_bll
    {

        dz_output_dal dal = new dz_output_dal();
        #region 平安产量表查询插入


        public bool insert(string datenum, string userid, float bili, int rightlength)
        {
            return dal.insert(datenum, userid, bili, rightlength);
        }
        public int GetRightlength(string datestr, string userid)
        {
            return dal.GetRightlength(datestr, userid);
        }
        #endregion
        #region 
        public List<string[]> ShjjExcelGenart(string startdate, string enddate)
        {
            return dal.ShjjExcelGenart(startdate,enddate);
        }
        public List<string[]> ShjjExcelGenart_y(string boxno,string userid)
        {
            return dal.ShjjExcelGenart_y(boxno,userid);
        }
        public List<string[]> AnwlExcelGenart_y(string boxno, string userid)
        {
            return dal.AnwlExcelGenart_y(boxno,userid);
        }
        #endregion
    }
}
