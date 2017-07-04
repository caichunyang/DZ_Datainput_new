using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
   public class DZ_INPUT_SHJJ_DAL
    {
         public static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public static string conn_pa = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_pa"].ConnectionString;

        public int RestartCheck(string startdate, string enddate)
        {
            string sql = " update dz_input_shjj t set t.inputstate=88 , t.userid3=null where t.thedate1  >=to_date('" + startdate + "','yyyy-mm-dd HH24:MI:SS') and  t.thedate1  <=to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS') and  t.values3='0'";
            return Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
        }
        public int RestartCheck2_shjj(string boxno)
        {
            string sql = "  UPDATE  dz_input_shjj t SET t.inputstate=88 , t.userid3=null WHERE  EXISTS ( SELECT 1 FROM  dz_image_shjj i,dz_box_shjj b  WHERE b.boxno='" + boxno + "' and  t.imageid=i.imageid  and i.barcode=lower(b.barcode))  and  t.values3='0'";
            string sql2 = "UPDATE  dz_input_shjj t SET t.inputstate=88 , t.userid3=null WHERE  t.imageid in( SELECT i.imageid FROM  dz_image_shjj i  WHERE i.boxno='" + boxno + "')  and  t.values3='0'";
            return Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql2);
        }
        public List<string[]> UserOutPutTj(string startdate, string enddate)//2016-10-01 00:00:00
        {
            string sql = " select i.imageid,p.values3,p.inputname, b.thedate||'/'||b.boxno||'/'||i.imageid||'.jpg' from dz_box_shjj b,dz_image_shjj i,dz_input_shjj p"+
                " where lower(b.barcode)=i.barcode and p.imageid=i.imageid order by p.imageid desc";
             
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[6];
                    array[0] = item[0].ToString();
                    array[1] = item[1].ToString();
                    array[2] = item[2].ToString();
                    array[3] = item[3].ToString();
                    resultlist.Add(array);
                }

            }
            return resultlist;
        }
        public bool updateinput(string[] inputmodel)
        {
            string sql = "update dz_input_shjj p set t.values3=:,t.userid3=:user" +
                " where p.imageid=:imageid ";

            return Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql) > 0;
        }
    }
}
