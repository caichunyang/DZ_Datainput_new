using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;

namespace DZ.BLL
{
    public class DZ_EXCEL_EMS_BLL
    {
        DLL.DZ_EXCEL_EMS_DAL dal = new DZ_EXCEL_EMS_DAL();
        public List<string[]> GetBoxNo(string thedate, int objectid)
        {
            return dal.GetBoxNo(thedate, objectid);
        }
        public List<string[]> EXCEL_OUT(string boxno, int objectid, string objname)
        {
            return dal.EXCEL_OUT(boxno, objectid, objname);
        }
        public List<string[]> GetBoxNo_shjj(string thedate, string userid)
        {
            return dal.GetBoxNo_shjj(thedate, userid);
        }
        public List<string[]> GetBoxNo_anwl(string thedate, string userid)
        {
            return dal.GetBoxNo_anwl(thedate, userid);
        }

        public List<string[]> GetRecivNoByUser_anwl(string startdate,string enddate, string userid)
        {
            return dal.GetRecivNoByUser_anwl(startdate,enddate, userid);
        }
    }
}
