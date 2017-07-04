using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
    public class DZ_ShowOutPut_DAL
    {
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public bool CkeckUser(string username, string password)
        {
            string sql = "select * from dz_user where userid='" + username + "' and userpwd='" + password + "'";
            MyData MyData = new MyData();
            long result = 0;
            if (MyData.OpenBase() == true)
            {
                // Oracle.ManagedDataAccess.Client.OracleDataReader OraDt = MyData.RunSQLDr(sql.ToString());
                result = MyData.RunSQL(sql);

            }
            MyData.CloseBase();
            return result > 0;
        }
        public List< string[]> UserOutPutTj(string startdate, string enddate)//2016-10-01 00:00:00
        {
            startdate += " 00:00:00";
            enddate += " 23:59:59";
            string sql = "select count(1),t.userid1 from dz_input_yd t where  t.thedate1> to_date(:statedate,'yyyy-mm-dd HH24:MI:SS') " +
"and  t.thedate1 < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') group by t.userid1 order by t.userid1";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, parmss);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array= new string[2];
                array[0] = dr[0].ToString();
                array[1]=dr[1].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        public List<string[]> CenterOutPutTJ(string startdate, string enddate)
        {
            startdate += " 00:00:00";
            enddate += " 23:59:59";
       StringBuilder sql=new StringBuilder ();
          sql.Append(" select o.objectid, o.objectname, count(t.imageid) from dz_input_yd t join dz_user u on t.userid1=u.userid")
              .Append(" join dz_object o on u.objectid=o.objectid ")
              .Append(" where  t.thedate1 > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
              .Append("and  t.thedate1 < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')")
              .Append("group by o.objectid,o.objectname order by o.objectid");
          Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
          parmss[0].Value = startdate;
          parmss[1].Value = enddate;
          OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql.ToString(), parmss);
          List<string[]> arraylist = new List<string[]>();
          while (dr.Read())
          {
              string[] array = new string[3];
              array[0] = dr[0].ToString();
              array[1] = dr[1].ToString();
              array[2] = dr[2].ToString();
              arraylist.Add(array);
          }

          dr.Close();
          return arraylist;  
        }
        /// <summary>
        /// image的接收上传查询
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="casekey"></param>
        /// <returns></returns>
        public List<string[]>[] Image_rc_upTJ(string startdate, string enddate,string casekey)
        {
            startdate += " 00:00:00";
            enddate += " 23:59:59";
            List<string[]>[] resultarray = new List<string[]>[2];
            StringBuilder sql = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            switch (casekey)
            {
                case "yd": sql.Append(" select count(*) c,TO_CHAR(t.createtime，'yyyy-mm-dd') dd from dz_image_yd t where t.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
               .Append(" and  t.createtime < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')").Append(" GROUP BY TO_CHAR(t.createtime，'yyyy-mm-dd')");
                    sql2.Append(" select count(*) c,TO_CHAR(t.createtime，'yyyy-mm-dd') dd from dz_image_yd t where t.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                     .Append(" and  t.createtime < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') and t.outstate=1").Append(" GROUP BY TO_CHAR(t.createtime，'yyyy-mm-dd')"); break;
                case "ems": sql.Append(" select count(*) c,TO_CHAR(t.createtime，'yyyy-mm-dd') dd from dz_image_ems t where t.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                  .Append(" and  t.createtime < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')").Append(" GROUP BY TO_CHAR(t.createtime，'yyyy-mm-dd')");
                    sql2.Append(" select count(*) c,TO_CHAR(t.createtime，'yyyy-mm-dd') dd from dz_image_ems t where t.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                     .Append(" and  t.createtime < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') and t.outstate=1").Append(" GROUP BY TO_CHAR(t.createtime，'yyyy-mm-dd')");
                    break;
                default:
                    break;
            }
           
            //sql.Append(" select o.objectid, o.objectname, count(t.imageid) from dz_input_yd t join dz_user u on t.userid1=u.userid")
            //    .Append(" join dz_object o on u.objectid=o.objectid ")
            //    .Append(" where  t.thedate1 > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
            //    .Append("and  t.thedate1 < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')")
            //    .Append("group by o.objectid,o.objectname order by o.objectid");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[2];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                //array[2] = dr[2].ToString();
                arraylist.Add(array);
            }
          dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql2.ToString(), parmss);
            List<string[]> arraylist2 = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[2];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                //array[2] = dr[2].ToString();
                arraylist2.Add(array);
            }
            dr.Close();
            resultarray[0] = arraylist;
            resultarray[1] = arraylist2;
            return resultarray;  
        }

        public List<List<string>> SelectOutPut(string startdate, string enddate, string casekey,string where,string orderstr)
        {
          

            StringBuilder sql = new StringBuilder();
            switch (casekey)
            {
                case "yd": sql.Append("select * from dz_output_yd t");
              break;
                case "ems": sql.Append("select * from dz_output_ems t");
          break;
                default:
                    break;
            }
            sql .Append(" where  to_date(t.thedate,'yyyy-mm-dd') >= to_date(:startdate,'yyyy-mm-dd') ")
             .Append(" and  to_date(t.thedate,'yyyy-mm-dd') <= to_date(:enddate,'yyyy-mm-dd') ")
             .Append(!string.IsNullOrEmpty(where) ? (" and "+where) : "").Append(orderstr);

            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
          //   new Oracle.ManagedDataAccess.Client.OracleParameter(":where",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,50)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
          //  parmss[2].Value = where;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql.ToString(), parmss);
            List<List<string>> arraylist = new List<List<string>>();
            while (dr.Read())
            {
                List<string> array =new List<string>();
                array.Add( dr[0].ToString());
                array.Add(dr[1].ToString());
                array.Add(dr[2].ToString());
                array.Add(dr[3].ToString());
                array.Add(dr[4].ToString());
                array.Add(dr[5].ToString());
                array.Add(dr[6].ToString());
                array.Add(dr[7].ToString());
                array.Add(dr[8].ToString());
                array.Add(dr[9].ToString());
                //array[2] = dr[2].ToString();
                arraylist.Add(array);
            }
          
            dr.Close();
            return arraylist;  
            
        }
    }
}
