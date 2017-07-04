using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{

    public class DZ_ShowOutPut_DAL
    {
        public static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public static string conn_pa = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_pa"].ConnectionString;
        public DZ_ShowOutPut_DAL(string str)
        {
            conn = str;
        }
          public DZ_ShowOutPut_DAL()
        {
        }
        /// <summary>
        /// 各员工录入量统计
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> UserOutPutTj(string startdate, string enddate)//2016-10-01 00:00:00
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
                string[] array = new string[2];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        /// <summary>
        /// 中心录入量 韵达
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> CenterOutPutTJ(string startdate, string enddate)
        {
            //startdate += " 00:00:00";
            //enddate += " 23:59:59";
            StringBuilder sql = new StringBuilder();
            sql.Append(" select o.objectid, o.objectname, sum(t.thenum) from dz_output_yd t join dz_user u on t.userid=u.userid")
                .Append(" join dz_object o on u.objectid=o.objectid ")
                .Append(" where  t.thedate >= '" + startdate + "' ")// HH24:MI:SS
                .Append(" and  t.thedate <= '" + enddate + "' ")
                .Append("group by o.objectid,o.objectname order by o.objectid");

            //sql.Append(" select o.objectid, o.objectname, count(t.imageid) from dz_input_yd t join dz_user u on t.userid1=u.userid")
            //   .Append(" join dz_object o on u.objectid=o.objectid ")
            //   .Append(" where  t.thedate1 > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
            //   .Append("and  t.thedate1 < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')")
            //   .Append("group by o.objectid,o.objectname order by o.objectid");
            StringBuilder sq2 = new StringBuilder();
            sq2.Append(" select o.objectid, o.objectname, count(t.imageid) from dz_history_input_yd t join dz_user u on t.userid1=u.userid")
                .Append(" join dz_object o on u.objectid=o.objectid ")
                .Append(" where  t.thedate1 > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                .Append("and  t.thedate1 < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')")
                .Append("group by o.objectid,o.objectname order by o.objectid");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            //parmss[0].Value = startdate;
            //parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql.ToString(), null);

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
        public List<string[]>[] Image_rc_upTJ(string startdate, string enddate, string casekey)
        {

            startdate += " 00:00:00";
            enddate += " 23:59:59";
            List<string[]>[] resultarray = new List<string[]>[2];
            StringBuilder sql = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            switch (casekey)
            {
                case "yd": sql.Append(" select count(*) c,TO_CHAR(t.zipcreatetime，'yyyy-mm-dd') dd from dz_history_image_yd t where t.zipcreatetime  >= to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
               .Append(" and  t.zipcreatetime  <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')").Append(" GROUP BY TO_CHAR(t.zipcreatetime，'yyyy-mm-dd')");
                    sql2.Append(" select count(*) c,TO_CHAR(t.zipcreatetime，'yyyy-mm-dd') dd from dz_history_image_yd t where t.zipcreatetime >=to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                     .Append(" and  t.zipcreatetime  <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') and t.outstate=1").Append(" GROUP BY TO_CHAR(t.zipcreatetime，'yyyy-mm-dd')"); break;
                case "ems": sql.Append(" select count(*) c,TO_CHAR(t.createtime，'yyyy-mm-dd') dd from dz_image_ems t where t.createtime >= to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                  .Append(" and  t.createtime <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')").Append(" GROUP BY TO_CHAR(t.createtime，'yyyy-mm-dd')");
                    sql2.Append(" select count(*) c,to_char(t.thedate1，'yyyy-mm-dd') dd from dz_input_ems t  ")
                     .Append(" where t.thedate1 >= to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') and  t.thedate1 <=to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')  ")
                     .Append(" GROUP BY to_char(t.thedate1，'yyyy-mm-dd')");
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
        public string[] Image_rc_upTJ2(string startdate, string enddate)
        {
            List<string[]>[] resultarray = new List<string[]>[2];
            StringBuilder sql = new StringBuilder();
            StringBuilder sql_1 = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            sql.Append(" select count(*) c from dz_history_image_yd t where t.zipcreatetime >= to_date('" + startdate + "','yyyy-mm-dd HH24:MI:SS') ")
               .Append(" and  t.zipcreatetime <= to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS')");
            sql_1.Append(" select count(*) c from dz_image_yd t,dz_input_yd i where t.imageid=i.imageid and i.inputstate<>88 and t.zipcreatetime >= to_date('" + startdate + "','yyyy-mm-dd HH24:MI:SS') ")
         .Append(" and  t.zipcreatetime <= to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS') ");
            sql2.Append(" select count(*) c from dz_history_image_yd t where t.zipcreatetime >=to_date('" + startdate + "','yyyy-mm-dd HH24:MI:SS') ")
             .Append(" and  t.zipcreatetime <= to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS') and t.outstate=1");
            string[] array = new string[2];
            array[0] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql.ToString()).ToString();
            string result2 = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql_1.ToString()).ToString();
            array[0] = (int.Parse(array[0]) + int.Parse(result2)).ToString();
            array[1] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql2.ToString()).ToString();
            return array;
            //List<string[]> arraylist2 = new List<string[]>();

            // while (dr.Read())
            // {
            //     string[] array = new string[2];
            //     array[0] = dr[0].ToString();
            //     array[1] = dr[1].ToString();
            //     arraylist2.Add(array);
            //     break;
            // }
            // dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql2.ToString());

            // while (dr.Read())
            // {
            //     string[] array = new string[2];
            //     array[0] = dr[0].ToString();
            //     array[1] = dr[1].ToString();
            //     arraylist2.Add(array);
            //     break;

            // }

            // dr.Close();
            // return arraylist2;
        }
        public string[] Image_dailu_daichuan(string startdate, string enddate)
        {
            List<string[]>[] resultarray = new List<string[]>[2];
            StringBuilder sql = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            sql.Append(" select count(*) c from dz_input_yd i,dz_image_yd t where t.imageid=i.imageid and  t.createtime >= to_date('" + startdate + "','yyyy-mm-dd HH24:MI:SS') ")
               .Append(" and  t.createtime <= to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS') and i.inputstate=0");
            sql2.Append(" select count(*) c from dz_image_yd t,dz_input_yd i where t.imageid=i.imageid and t.createtime >=to_date('" + startdate + "','yyyy-mm-dd HH24:MI:SS') ")
             .Append(" and  t.createtime <= to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS') and t.outstate=0 and i.inputstate<>0");
            string[] array = new string[2];

            //             select count(*) c from dz_input_yd i,dz_image_yd t where t.imageid=i.imageid and t.createtime >= to_date('2017-04-18 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //and  t.createtime <= to_date('2017-04-19 23:59:59','yyyy-mm-dd HH24:MI:SS') and  i.inputstate=0
            array[0] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql.ToString()).ToString();
            array[1] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql2.ToString()).ToString();
            return array;

        }
        /// <summary>
        /// 韵达录单在线人数
        /// </summary>
        /// <returns></returns>
        public List<string> GetInputUserCount()
        {
            StringBuilder sb3 = new StringBuilder();
            //StringBuilder sb4 = new StringBuilder();
            DateTime dtnow = DateTime.Now;
            //string dt1 = dtnow.AddMinutes(0 - 5).ToString("yyyy-MM-dd HH:mm:ss");
            //string dt2 = dtnow.ToString("yyyy-MM-dd HH:mm:ss");
            //sb3.Append("select count(distinct p.userid1) from dz_input_yd p,dz_user u where p.userid1=u.userid ");
            //sb3.Append(" and p.thedate1>= to_date('" + dt1 + "','yyyy-mm-dd HH24:MI:SS') ")
            //    .Append("and p.thedate1 <= to_date('" + dt2 + "','yyyy-mm-dd HH24:MI:SS')");

            sb3.Append("select * from (select distinct userid1 From dz_input_yd where thedate1 > sysdate-5/24/60 union select distinct userid1 From dz_history_input_yd where thedate1 > sysdate-5/24/60)");

            // sb4.Append("select count(distinct p.userid1) from dz_history_input_yd p,dz_user u where p.userid1=u.userid ");
            //sb4.Append(" and p.thedate1>= to_date('" + dt1 + "','yyyy-mm-dd HH24:MI:SS') ")
            //    .Append("and p.thedate1 <= to_date('" + dt2 + "','yyyy-mm-dd HH24:MI:SS')");
            DataSet ds = Common.OracleHelper.Query(conn, sb3.ToString(), System.Data.CommandType.Text);
            List<string> onlineuserlist = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    onlineuserlist.Add(item[0].ToString());
                }

            }
            //string result2 = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sb4.ToString()).ToString();
            //return (int.Parse(result) + int.Parse(result2)).ToString();
            return onlineuserlist;
        }
        /// <summary>
        /// 逾期统计 YD
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="casekey"></param>
        /// <returns></returns>
        public string[] InputOverdueTJ_YD(string startdate)//string enddate, 
        {
            string[] finalarray = new string[10];
            string enddate = startdate + " 23:59:59";
            //startdate += " 00:00:00";
            string boxno1 = DateTime.Parse(startdate).ToString("yyyyMMdd");
            string boxno2 = DateTime.Parse(startdate).AddDays(-1).ToString("yyyyMMdd");

            //  List<string[]>[] resultarray = new List<string[]>[2];
            string sqlyd1 = "select count(i.imageid) from dz_image_yd i where i.createtime> to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and i.createtime <= to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS')";
            StringBuilder sqlyd2 = new StringBuilder();
            StringBuilder sqlyd3 = new StringBuilder();

            StringBuilder sqlyd4 = new StringBuilder();
            StringBuilder sqlyd4_1 = new StringBuilder();
            StringBuilder sqlyd5 = new StringBuilder();
            StringBuilder sqlyd5_1 = new StringBuilder();
            StringBuilder sqlyd6 = new StringBuilder();
            StringBuilder sqlyd7 = new StringBuilder();
            StringBuilder sqlyd8 = new StringBuilder();
            sqlyd2.Append("select count(i.imageid),count(p.imageid) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid")
                .Append(" and  p.thedate1 > to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS') ")
.Append(" and  p.thedate1 < to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS')")
.Append(" and i.outstate=1 ")
.Append(" and i.boxno like '" + boxno1 + "%'");
            sqlyd3.Append("select count(i.imageid) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ")
              .Append(" and  p.thedate1 > to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS') ")
.Append(" and  p.thedate1 < to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS')")
.Append(" and i.outstate=1 ")
.Append(" and i.boxno like '" + boxno2 + "%'");

            sqlyd4.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null and p.thedate3 is  null")
            .Append(" and  p.thedate1 > to_date('" + startdate + " 08:00:00','yyyy-mm-dd HH24:MI:SS')  ")
.Append("  and i.zipname  >='" + boxno2 + "230000' and i.zipname  <='" + boxno1 + "075959'");
            sqlyd4_1.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null")
          .Append(" and  p.thedate3 > to_date('" + startdate + " 08:00:00','yyyy-mm-dd HH24:MI:SS')  ")
.Append("  and i.zipname  >='" + boxno2 + "230000' and i.zipname  <='" + boxno1 + "075959'");
            //            select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //and  p.thedate1 > to_date('2016-10-14 08:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and i.zipname  >='20161013230000' and i.zipname  <='20161014075959'
            sqlyd5.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null and p.thedate3 is null")
            .Append(" and   p.thedate1> (to_date(SUBSTR( i.zipname,0,14),'yyyymmddhh24miss')+interval '1' Hour) ")
.Append("   and i.zipname  >='" + boxno1 + "080000' and i.zipname  <='" + boxno1 + "225959'");
            sqlyd5_1.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null ")
        .Append(" and   p.thedate3> (to_date(SUBSTR( i.zipname,0,14),'yyyymmddhh24miss')+interval '1' Hour) ")
.Append("   and i.zipname  >='" + boxno1 + "080000' and i.zipname  <='" + boxno1 + "225959'");
            sqlyd6.Append(" select  count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null ")
           .Append("and p.values1 like '%<link>1%' ")
.Append("   and i.zipname like '" + boxno1 + "%'");
            //   select  count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //and i.zipname is not null
            //and p.values1 like '%<link>1%'
            //  and i.zipname like '20161015%'

            sqlyd7.Append(" select  count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ")
          .Append("and  p.thedate3> to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS') ")
.Append("  and  p.thedate3 <= to_date('" + startdate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");
            //  质检量            select count(i.imageid) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //and  p.thedate3> to_date('2016-10-15 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and  p.thedate3 <= to_date('2016-10-15 23:59:59','yyyy-mm-dd HH24:MI:SS')
            sqlyd8.Append(" select  count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and p.inputstate=5 ")
          .Append("and  p.thedate1> to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS')  ")
.Append(" and  p.thedate1 <= to_date('" + startdate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");
            //            select count(i.imageid) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //   and p.inputstate=5
            //and  p.thedate1> to_date('2016-10-15 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and  p.thedate1 <= to_date('2016-10-15 23:59:59','yyyy-mm-dd HH24:MI:SS')


            finalarray[0] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd1, null).ToString();
            finalarray[1] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd2.ToString(), null).ToString();
            finalarray[2] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd3.ToString(), null).ToString();
            finalarray[3] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd4.ToString(), null).ToString();
            finalarray[4] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd4_1.ToString(), null).ToString();
            finalarray[5] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd5.ToString(), null).ToString();
            finalarray[6] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd5_1.ToString(), null).ToString();
            finalarray[7] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd6.ToString(), null).ToString();
            finalarray[8] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd7.ToString(), null).ToString();
            finalarray[9] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd8.ToString(), null).ToString();
            return finalarray;
        }
        /// <summary>
        /// 逾期统计2 YD
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="casekey"></param>
        /// <returns></returns>
        public string[] InputOverdueTJ_YD2(string startdate)//string enddate, 
        {
            string[] finalarray = new string[14];
            string enddate = startdate + " 23:59:59";
            //startdate += " 00:00:00";
            string boxno1 = DateTime.Parse(startdate).ToString("yyyyMMdd");
            string boxno2 = DateTime.Parse(startdate).AddDays(-1).ToString("yyyyMMdd");

            //  List<string[]>[] resultarray = new List<string[]>[2];
            string sqlyd1 = "select count(i.imageid) from dz_image_yd i where i.createtime> to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and i.createtime <= to_date('" + enddate + "','yyyy-mm-dd HH24:MI:SS')";
            StringBuilder sqlyd2 = new StringBuilder();
            StringBuilder sqlyd3 = new StringBuilder();
            StringBuilder sqlyd4 = new StringBuilder();
            StringBuilder sqlyd5 = new StringBuilder();
            StringBuilder sqlyd6 = new StringBuilder();
            StringBuilder sqlyd7 = new StringBuilder();
            StringBuilder sqlyd8 = new StringBuilder();
            StringBuilder sqlyd9 = new StringBuilder();
            StringBuilder sqlyd10 = new StringBuilder();
            StringBuilder sqlyd11 = new StringBuilder();
            StringBuilder sqlyd12 = new StringBuilder();
            StringBuilder sqlyd13 = new StringBuilder();
            StringBuilder sqlyd14 = new StringBuilder();
            //逾期量1（昨日23:00：00~07:59:59）接收的 为未愈期的  
            sqlyd2.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null")
            .Append(" and  p.thedate1 <= to_date('" + startdate + " 09:00:00','yyyy-mm-dd HH24:MI:SS')  ")
.Append("  and i.zipname  >='" + boxno2 + "230000' and i.zipname  <='" + boxno1 + "075959'");
            // （昨日23:00：00~07:59:59）接收的      
            //select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //  and i.zipname is not null
            //and i.zipname  >='20161014230000' and i.zipname  <='20161015075959'
            sqlyd3.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null")
    .Append("  and i.zipname  >='" + boxno2 + "230000' and i.zipname  <='" + boxno1 + "075959'");
            //           /*逾期量2（07:59:59~22:59:59）接收*/
            //  select  count(1) from dz_input_yd p,dz_image_yd i 
            //where   i.imageid=p.imageid and i.zipname is not null
            //and   p.thedate1> (to_date(SUBSTR( i.zipname,0,14),'yyyymmddhh24miss')+interval '1' Hour)
            //  and i.zipname  >='20161015080000' and i.zipname  <='20161015225959'
            //（07:59:59~22:59:59）接收
            //   select  count(1) from dz_input_yd p,dz_image_yd i 
            //where   i.imageid=p.imageid and i.zipname is not null
            //  and i.zipname  >='20161015080000' and i.zipname  <='20161015225959'
            sqlyd4.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ")
                .Append(" and i.zipname is not null ")
            .Append(" and   p.thedate1<= (to_date(SUBSTR( i.zipname,0,14),'yyyymmddhh24miss')+interval '1' Hour) ")
.Append("   and i.zipname  >='" + boxno1 + "080000' and i.zipname  <='" + boxno1 + "225959'");
            sqlyd5.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null ")
    .Append("   and i.zipname >='" + boxno1 + "080000' and i.zipname  <='" + boxno1 + "225959'");

            //当前人数
            //            select count(distinct t.userid1) from dz_input_yd t
            //  where t.thedate1> to_date('2016-10-07 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //and  t.thedate1 < to_date('2016-10-07 23:59:59','yyyy-mm-dd HH24:MI:SS') 
            string datenow = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            string dateless5m = DateTime.Now.AddMinutes(-5).ToString("yyyy/MM/dd hh:mm:ss");
            sqlyd6.Append("  select count(distinct t.userid1) from dz_input_yd t ").Append("  where t.thedate1> to_date('" + dateless5m + "','yyyy/mm/dd HH24:MI:SS')  ")
           .Append("and  t.thedate1 < to_date('" + datenow + "','yyyy/mm/dd HH24:MI:SS') ");

            //上传接口状态
            //   select  count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //and i.zipname is not null
            //and p.values1 like '%<link>1%'
            //  and i.zipname like '20161015%'

            //             select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //    and i.zipname is not null and i.outstate=1/*及时上传*/
            //and  p.thedate1 <= to_date('2016-10-15 09:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and i.zipname  >='20161014230000' and i.zipname  <='20161015075959'

            //             select  count(1) from dz_input_yd p,dz_image_yd i 
            //where   i.imageid=p.imageid and i.zipname is not null and i.outstate=1 /*及时上传*/
            //and   p.thedate1> (to_date(SUBSTR( i.zipname,0,14),'yyyymmddhh24miss')+interval '1' Hour)
            //  and i.zipname  >='20161015080000' and i.zipname  <='20161015225959'

            sqlyd7.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null and i.outstate=1")
                .Append(" and  p.thedate1 <= to_date('" + startdate + " 09:00:00','yyyy-mm-dd HH24:MI:SS')  ")
    .Append("  and i.zipname  >='" + boxno2 + "230000' and i.zipname  <='" + boxno1 + "075959'");

            sqlyd8.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and i.zipname is not null and i.outstate=1")
             .Append("  and i.zipname  >='" + boxno2 + "230000' and i.zipname  <='" + boxno1 + "075959'");

            sqlyd9.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ")
                 .Append(" and i.zipname is not null and i.outstate=1")
             .Append(" and   p.thedate1<= (to_date(SUBSTR( i.zipname,0,14),'yyyymmddhh24miss')+interval '1' Hour) ")
 .Append("   and i.zipname  >='" + boxno1 + "080000' and i.zipname  <='" + boxno1 + "225959'");

            sqlyd10.Append(" select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ")
              .Append(" and i.zipname is not null and i.outstate=1")
.Append("   and i.zipname  >='" + boxno1 + "080000' and i.zipname  <='" + boxno1 + "225959'");

            //退单
            sqlyd11.Append(" select  count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ")
                .Append(" and p.values3 like '%<link>1' ")
     .Append("and  p.thedate3> to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS') ")
.Append("  and  p.thedate3 <= to_date('" + startdate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");
            //  待检单量    
            sqlyd12.Append(" select  count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and p.inputstate=88 ");

            //错误单量
            sqlyd13.Append(" select  count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid ").Append(" and p.inputstate=5 ")
 .Append("and  p.thedate3> to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS') ")
.Append("  and  p.thedate3 <= to_date('" + startdate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");

            sqlyd14.Append(" select max(t.createtime)  from dz_image_yd t where t.outstate=1  ");


            finalarray[0] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd1, null).ToString();//数量
            finalarray[1] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd2.ToString(), null).ToString();//未愈期1
            finalarray[2] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd3.ToString(), null).ToString();//逾期量1
            finalarray[3] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd4.ToString(), null).ToString();//未愈期2
            finalarray[4] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd5.ToString(), null).ToString();//逾期量2
            finalarray[5] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd6.ToString(), null).ToString();//当前人数
            finalarray[6] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd7.ToString(), null).ToString();//uploadcount1
            finalarray[7] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd8.ToString(), null).ToString();//uploadcount1
            finalarray[8] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd9.ToString(), null).ToString();//uploadcount2
            finalarray[9] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd10.ToString(), null).ToString();//uploadnum2
            finalarray[10] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd11.ToString(), null).ToString();//退单
            finalarray[11] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd12.ToString(), null).ToString();  //  待检单量    
            finalarray[12] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd13.ToString(), null).ToString();//错误单量
            //// finalarray[9] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd8.ToString(), null).ToString();
            finalarray[13] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd14.ToString(), null).ToString();//最后的上传时间

            //    finalarray[14] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd15.ToString(), null).ToString();//最后的上传时间
            return finalarray;
        }


        /// <summary>
        /// 逾期统计 EMS
        /// </summary>
        /// <param name="startdate"></param>
        /// <returns></returns>
        public string[] InputOverdueTJ_EMS(string startdate)//string enddate, 
        {
            string[] finalarray = new string[10];
            string enddate = startdate + " 23:59:59";
            //startdate += " 00:00:00";
            string boxno1 = startdate;// DateTime.Parse(startdate).ToString("yyyy-MM-dd");
            string boxno2 = DateTime.Parse(startdate).AddDays(-1).ToString("yyyy-MM-dd");

            //  List<string[]>[] resultarray = new List<string[]>[2];
            string sqlyd1 = "select count(i.imageid) from dz_image_ems i where i.createtime> to_date('" + boxno1 + " 00:00:00','yyyy-mm-dd HH24:MI:SS')  and i.createtime < to_date('" + boxno1 + " 23:59:59','yyyy-mm-dd HH24:MI:SS')";
            StringBuilder sqlyd2 = new StringBuilder();
            StringBuilder sqlyd3 = new StringBuilder();
            StringBuilder sqlyd4 = new StringBuilder();
            StringBuilder sqlyd4_1 = new StringBuilder();
            StringBuilder sqlyd5 = new StringBuilder();
            StringBuilder sqlyd5_1 = new StringBuilder();
            StringBuilder sqlyd6 = new StringBuilder();
            StringBuilder sqlyd7 = new StringBuilder();
            StringBuilder sqlyd8 = new StringBuilder();
            //            select count(i.imageid) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid
            //and  p.thedate1 > to_date('2016-10-15 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and  p.thedate1 < to_date('2016-10-15 23:59:59','yyyy-mm-dd HH24:MI:SS')
            //and i.createtime> to_date('2016-10-15 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and i.createtime < to_date('2016-10-15 23:59:59','yyyy-mm-dd HH24:MI:SS')
            sqlyd2.Append(" select count(i.imageid) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid ")
                .Append(" and  p.thedate1 > to_date('" + boxno1 + " 00:00:00','yyyy-mm-dd HH24:MI:SS') ")
.Append(" and  p.thedate1 < to_date('" + boxno1 + " 23:59:59','yyyy-mm-dd HH24:MI:SS')")
.Append(" and i.createtime> to_date('" + boxno1 + " 00:00:00','yyyy-mm-dd HH24:MI:SS') ")
.Append(" and i.createtime < to_date('" + boxno1 + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");
            //select count(i.imageid),count(p.imageid) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid
            //and  p.thedate1 > to_date('2016-10-15 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and  p.thedate1 < to_date('2016-10-15 23:59:59','yyyy-mm-dd HH24:MI:SS')
            //and i.createtime> to_date('2016-10-14 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and i.createtime < to_date('2016-10-14 23:59:59','yyyy-mm-dd HH24:MI:SS')
            sqlyd3.Append("select count(i.imageid),count(p.imageid) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid")
              .Append(" and  p.thedate1 > to_date('" + boxno1 + " 00:00:00','yyyy-mm-dd HH24:MI:SS')  ")
.Append("  and  p.thedate1 < to_date('" + boxno1 + " 23:59:59','yyyy-mm-dd HH24:MI:SS')")
.Append(" and i.createtime> to_date('" + boxno2 + " 00:00:00','yyyy-mm-dd HH24:MI:SS')  ")
.Append(" and i.createtime < to_date('" + boxno2 + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");
            //  select count(1) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid
            //and  p.thedate1 > to_date('2016-10-15 08:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and i.createtime>= to_date('2016-10-14 23:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and i.createtime <= to_date('2016-10-15 07:59:59','yyyy-mm-dd HH24:MI:SS')
            sqlyd4.Append("  select count(1) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid ")
                .Append(" and  p.thedate1 > to_date('" + boxno1 + " 08:00:00','yyyy-mm-dd HH24:MI:SS')  ")
            .Append(" and i.createtime>= to_date('" + boxno2 + " 23:00:00','yyyy-mm-dd HH24:MI:SS')   ")
.Append("  and i.createtime <= to_date('" + boxno1 + " 07:59:59','yyyy-mm-dd HH24:MI:SS')");
            sqlyd4_1.Append("  select count(1) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid and p.thedate3 is null")
              .Append(" and  p.thedate1 > to_date('" + boxno1 + " 08:00:00','yyyy-mm-dd HH24:MI:SS')  ")
          .Append(" and i.createtime>= to_date('" + boxno2 + " 23:00:00','yyyy-mm-dd HH24:MI:SS')   ")
.Append("  and i.createtime <= to_date('" + boxno1 + " 07:59:59','yyyy-mm-dd HH24:MI:SS')");
            //select  count(1) from dz_input_ems p,dz_image_ems i 
            //where   i.imageid=p.imageid 
            //and   p.thedate1> (i.createtime+interval '1' Hour)
            //    and i.createtime>= to_date('2016-10-15 08:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and i.createtime <= to_date('2016-10-15 22:59:59','yyyy-mm-dd HH24:MI:SS')
            sqlyd5.Append("  select  count(1) from dz_input_ems p,dz_image_ems i ").Append(" where   i.imageid=p.imageid  ")
           .Append(" and   p.thedate1> (i.createtime+interval '1' Hour) ")
.Append("  and i.createtime>= to_date('" + boxno1 + " 08:00:00','yyyy-mm-dd HH24:MI:SS') ").Append(" and i.createtime <= to_date('" + boxno1 + " 22:59:59','yyyy-mm-dd HH24:MI:SS')");
            sqlyd5_1.Append("  select  count(1) from dz_input_ems p,dz_image_ems i ").Append(" where   i.imageid=p.imageid  ")
         .Append(" and   p.thedate3> (i.createtime+interval '1' Hour) ")
.Append("  and i.createtime>= to_date('" + boxno1 + " 08:00:00','yyyy-mm-dd HH24:MI:SS') ").Append(" and i.createtime <= to_date('" + boxno1 + " 22:59:59','yyyy-mm-dd HH24:MI:SS')");

            sqlyd6.Append("  select  count(1) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid ").Append(" and p.values3 = '1' ")
           .Append("and i.createtime>= to_date('" + boxno1 + " 00:00:00','yyyy-mm-dd HH24:MI:SS')  ")
.Append("   and i.createtime <= to_date('" + boxno1 + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");
            //              select  count(1) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid
            //and p.values3 = '1'
            //  and i.createtime>= to_date('2016-11-05 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and i.createtime <= to_date('2016-11-05 23:59:59','yyyy-mm-dd HH24:MI:SS')

            sqlyd7.Append(" select  count(1) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid ")
        .Append("and  p.thedate3> to_date('" + boxno1 + " 00:00:00','yyyy-mm-dd HH24:MI:SS') ")
.Append("  and  p.thedate3 <= to_date('" + boxno1 + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");
            //  质检量            select count(i.imageid) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //and  p.thedate3> to_date('2016-10-15 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and  p.thedate3 <= to_date('2016-10-15 23:59:59','yyyy-mm-dd HH24:MI:SS')
            sqlyd8.Append(" select  count(1) from dz_input_ems p,dz_image_ems i where   i.imageid=p.imageid ").Append(" and p.inputstate=5 ")
          .Append("and  p.thedate1> to_date('" + boxno1 + " 00:00:00','yyyy-mm-dd HH24:MI:SS')  ")
.Append(" and  p.thedate1 <= to_date('" + boxno1 + " 23:59:59','yyyy-mm-dd HH24:MI:SS')");
            //  差错量          select count(i.imageid) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid
            //   and p.inputstate=5
            //and  p.thedate1> to_date('2016-10-15 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and  p.thedate1 <= to_date('2016-10-15 23:59:59','yyyy-mm-dd HH24:MI:SS')

            finalarray[0] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd1, null).ToString();
            finalarray[1] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd2.ToString(), null).ToString();
            finalarray[2] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd3.ToString(), null).ToString();
            finalarray[3] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd4.ToString(), null).ToString();
            finalarray[4] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd4_1.ToString(), null).ToString();
            finalarray[5] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd5.ToString(), null).ToString();
            finalarray[6] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd5_1.ToString(), null).ToString();
            finalarray[7] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd6.ToString(), null).ToString();
            finalarray[8] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd7.ToString(), null).ToString();
            finalarray[9] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlyd8.ToString(), null).ToString();
            return finalarray;
        }

        public List<List<string>> SelectOutPut(string startdate, string enddate, string casekey, string where, string orderstr)
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
            sql.Append(" where  to_date(t.thedate,'yyyy-mm-dd') >= to_date(:startdate,'yyyy-mm-dd') ")
             .Append(" and  to_date(t.thedate,'yyyy-mm-dd') <= to_date(:enddate,'yyyy-mm-dd') ")
             .Append(!string.IsNullOrEmpty(where) ? (" and " + where) : "").Append(orderstr);

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
                List<string> array = new List<string>();
                array.Add(dr[0].ToString());
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
        public List<List<string>> SelectOutPut_back(string startdate, string enddate, string casekey, string where)
        {

            startdate += " 00:00:00";
            enddate += " 23:59:59";
            StringBuilder sql = new StringBuilder();
            switch (casekey)
            {
                case "yd": sql.Append("select p.userid1, to_char(i.createtime,'yyyy-mm-dd'), count(*) from dz_image_yd i ,dz_input_yd p where i.imageid=p.imageid")
                    .Append(" and p.values3 like '%<link>1'").Append(" and  i.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS')  and  i.createtime <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')").Append(!string.IsNullOrEmpty(where) ? (" and " + where) : "")
                    .Append(" group by to_char(i.createtime,'yyyy-mm-dd'),p.userid1");
                    break;
                case "ems": sql.Append("select * from dz_output_ems t").Append(" where  to_date(t.thedate,'yyyy-mm-dd') >= to_date(:startdate,'yyyy-mm-dd') ")
             .Append(" and  to_date(t.thedate,'yyyy-mm-dd') <= to_date(:enddate,'yyyy-mm-dd') ")
             .Append(!string.IsNullOrEmpty(where) ? (" and " + where) : "");
                    break;
                default:
                    break;
            }


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
                List<string> array = new List<string>();
                array.Add(dr[0].ToString());
                array.Add(dr[1].ToString());
                array.Add(dr[2].ToString());
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;

        }
        /// <summary>
        /// image接收 按中心 类型统计
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="casekey"></param>
        /// <param name="where"></param>
        /// <param name="orderstr"></param>
        /// <returns></returns>
        public List<string[]> center_recv_ems(string startdate, string enddate)
        {
            startdate += " 00:00:00";
            enddate += " 23:59:59";
            StringBuilder sql = new StringBuilder();
            //sql.Append(" select u.objectid,o.objectname, count(t.imageid),t.filedtype from dz_image_ems t,dz_user u,dz_object o")
            //    .Append(" where  t.userid=u.userid and o.objectid=u.objectid  ")
            //    .Append(" and  t.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS' ")
            //    .Append("and  t.createtime < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') ")
            //    .Append(" group by u.objectid ,t.filedtype,o.objectname ").Append(" order by u.objectid");

            sql.Append(" select u.objectid,o.objectname, count(t.imageid),TO_CHAR(t.createtime，'yyyy-mm-dd'),t.filedtype from dz_image_ems t,dz_user u,dz_object o")
                .Append(" where  t.userid=u.userid and o.objectid=u.objectid   ")
                .Append(" and  t.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS')  ")
                .Append("and  t.createtime < to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') ")
                .Append("  group by u.objectid ,o.objectname,TO_CHAR(t.createtime，'yyyy-mm-dd') ,t.filedtype ")
                .Append(" order by TO_CHAR(t.createtime，'yyyy-mm-dd'), u.objectid");

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
                string[] array = new string[5];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }


        /// <summary>
        /// 员工邮政产量分析
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> OutPutAnalysis_emsTJ(string startdate, string enddate, string userid)
        {
            //startdate += " 00:00:00";
            //enddate += " 23:59:59";
            StringBuilder sql = new StringBuilder();

            sql.Append(" select userid,thedate,filename,thenum,checknum,checkerrnum,errnum,backbum from dz_output_ems ")
                .Append(" where thedate>= :startdate and thedate<=:enddate and userid=:userid  ")
                .Append("  order by thedate,filename  ");

            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
              new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            parmss[2].Value = userid;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[8];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                array[7] = dr[7].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        /// <summary>
        /// 员工韵达产量分析
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> OutPutAnalysis_ydTJ(string startdate, string enddate, string userid)
        {
            //startdate += " 00:00:00";
            //enddate += " 23:59:59";
            StringBuilder sql = new StringBuilder();

            sql.Append("  select userid,thedate,thenum,checknum,checkerrnum,errnum,backbum from dz_output_yd  ")
                .Append(" where thedate>= :startdate and thedate<=:enddate  ").Append("   and userid=:userid ")
                .Append("   order by thedate  ");

            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
              new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            parmss[2].Value = userid;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[8];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        /// <summary>
        /// 各中心产量 韵达
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> CenterProduction_yd(string startdate, string enddate)
        {
            //          select o.objectid, o.objectname,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)
            //    from dz_output_yd t 
            //  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid
            //     where  t.thedate >='2016-10-01' 
            //           and  t.thedate <= '2016-10-09' 
            //group by o.objectid,o.objectname order by o.objectid
            StringBuilder sql = new StringBuilder();
            sql.Append("  select o.objectid, o.objectname,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)  ")
                .Append("   from dz_output_yd t  ")
                .Append(" join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid ")
                .Append("  where  t.thedate >=:startdate  ")
                 .Append("  and  t.thedate <= :enddate  ");
            sql.Append(" group by o.objectid,o.objectname order by sum(t.thenum) ");
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
                string[] array = new string[7];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        /// <summary>
        /// 中心退单产量 韵达 A组
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="objid">所有中心* 本中心objectid 或传userid</param>
        /// <returns></returns>
        public List<string[]> CenterPro_back_yd(string startdate, string enddate, string objid)
        {
            //          select o.objectid, o.objectname,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)
            //    from dz_output_yd t 
            //  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid
            //     where  t.thedate >='2016-10-01' 
            //           and  t.thedate <= '2016-10-09' 
            //group by o.objectid,o.objectname order by o.objectid
            string strdate_s = startdate;
            string strdate_e = enddate;
            startdate += " 00:00:00";
            enddate += " 23:59:59";
            StringBuilder sql = new StringBuilder();
            int objectid = -1;
            if (int.TryParse(objid, out objectid))
            {
                //            select  u.userid, count(*) from dz_image_yd i ,dz_input_yd p,dz_user u where i.imageid=p.imageid and p.userid1=u.userid
                //and p.values3 like '%<link>1'
                // and  i.createtime > to_date('2016-12-11 00:00:00','yyyy-mm-dd HH24:MI:SS') 
                //  and  i.createtime < to_date('2016-12-13 00:00:00','yyyy-mm-dd HH24:MI:SS')
                //and u.objectid=23
                //group by u.userid
                //sql.Append("select  u.userid, count(*) from dz_image_yd i ,dz_input_yd p,dz_user u where i.imageid=p.imageid and p.userid1=u.userid")
                //    .Append(" and p.values3 like '%<link>1'")
                //      .Append(" and  i.createtime >to_date(:startdate,'yyyy-mm-dd HH24:MI:SS')")
                //    .Append(" and  i.createtime <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') ")
                //     .Append("and u.objectid=" + objid)
                //      .Append(" group by u.userid ");
                // 170116 该input 一录时间
                //sql.Append("select  u.userid, count(*) from dz_input_yd p,dz_user u where p.userid1=u.userid")
                //    .Append(" and p.values3 like '%<link>1'")
                //      .Append(" and  p.thedate1 >to_date(:startdate,'yyyy-mm-dd HH24:MI:SS')")
                //    .Append(" and   p.thedate1  <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') ")
                //     .Append("and u.objectid=" + objid)
                //      .Append(" group by u.userid ");
                sql.Append("select  u.userid, sum(p.backbum) from dz_output_yd p,dz_user u where p.userid=u.userid")
                    //.Append(" and p.values3 like '%<link>1'")
                      .Append(" and  p.thedate >= '" + strdate_s + "' ")
                    .Append(" and   p.thedate  <= '" + strdate_e + "' ")
                     .Append("and u.objectid=" + objid)
                      .Append(" group by u.userid ");
                //    sql.Append("  select u.objectid,sum(p.thenum) from dz_output_yd p,dz_user u where  p.userid=u.userid ")
                //.Append(" and p.values3 like '%<link>1'  ")
                //.Append(" and  p.thedate  >= '"+strdate_s+"' ")
                // .Append(" and  p.thedate  <= '"+strdate_e+"' ");
                //sql.Append(" group by u.objectid ");

            }
            else if ("*" == objid)
            {
                //sql.Append("  select u.objectid,count(*) from dz_image_yd i ,dz_input_yd p,dz_user u where i.imageid=p.imageid and p.userid1=u.userid ")
                // .Append(" and p.values3 like '%<link>1'   ")
                // .Append("   and  i.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS')  ")
                //  .Append("   and  i.createtime <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') ");
                //sql.Append(" group by u.objectid ");
                sql.Append("  select u.objectid,sum(p.backbum) from dz_output_yd p,dz_user u where  p.userid=u.userid ")
                    //.Append(" and p.values3 like '%<link>1'   ")
                .Append(" and  p.thedate >= '" + strdate_s + "' ")
                 .Append(" and   p.thedate  <= '" + strdate_e + "' ");
                sql.Append(" group by u.objectid ");
            }
            else
            {
                // select count(*) from dz_image_yd i ,dz_input_yd p,dz_user u where i.imageid=p.imageid 
                //and p.values3 like '%<link>1'
                // and  i.createtime > to_date('2016-12-01 00:00:00','yyyy-mm-dd HH24:MI:SS') 
                //  and  i.createtime < to_date('2016-12-13 00:00:00','yyyy-mm-dd HH24:MI:SS')
                //and p.userid1='hbw0050'

                // sql.Append("select  to_char(i.createtime,'yyyy-mm-dd'), count(*) from dz_image_yd i ,dz_input_yd p where i.imageid=p.imageid  ")
                //.Append(" and p.values3 like '%<link>1'")
                //.Append("   and  i.createtime > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS')  ")
                // .Append("   and  i.createtime <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') ");
                // sql.Append(" and p.userid1='"+objid).Append("' group by to_char(i.createtime,'yyyy-mm-dd')");

                //    sql.Append("select  to_char(p.thedate1,'yyyy-mm-dd'), count(*) from dz_input_yd p where  ")
                //.Append("  p.values3 like '%<link>1'  and ")
                //.Append("  p.thedate1 > to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') and ")
                // .Append("  p.thedate1 <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS') ");
                //    sql.Append(" and p.userid1='" + objid).Append("' group by to_char(p.thedate1,'yyyy-mm-dd')");
                sql.Append("select  p.thedate, sum(p.backbum) from dz_output_yd p where  ")
           .Append("  p.thedate  >= '" + strdate_s + "' and ")
            .Append(" p.thedate  <= '" + strdate_e + "' ");
                sql.Append(" and p.userid='" + objid).Append("' group by p.thedate");//??

            }
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
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        /// <summary>
        /// 统计每小时的逾期量
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<string[]> SelectInputAndOverdue_yd(string startdate, string enddate)
        {
            if (!string.IsNullOrEmpty(startdate) && enddate == startdate)
            {
                DateTime end = DateTime.Parse(enddate);
                enddate = end.AddDays(1).ToString("yyyy-MM-dd");
            }
            StringBuilder sql1 = new StringBuilder();
            sql1.Append(" select count(case when t.isoverdue =0 then 1 end),count(case when t.isoverdue =1 then 1 end),to_char(i.zipcreatetime,'YYYY-mm-dd  hh24') ")
                 .Append(" from dz_history_input_yd t ,dz_history_image_yd i  ")
                 .Append(" where t.imageid=i.imageid and i.zipcreatetime >to_date('" + startdate + " 00:00:00','yyyy-mm-dd hh24:mi:ss')   ")
                 .Append(" and  i.zipcreatetime<=to_date('" + enddate + " 00:00:00','yyyy-mm-dd hh24:mi:ss')  ")
                  .Append(" group by to_char(i.zipcreatetime,'YYYY-mm-dd  hh24')  ")
                  .Append(" order by  to_char(i.zipcreatetime,'YYYY-mm-dd  hh24') ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            //parmss[0].Value = startdate;
            //parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), null);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[8];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                arraylist.Add(array);
            }
            dr.Close();
            return arraylist;
        }


        /// <summary>
        /// 统计每小时的（未逾期量 逾期量 总上传量 退单量 修改量  history 表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<string[]> SelectZhongHe_history_yd(string startdate, string enddate)
        {
            if (!string.IsNullOrEmpty(startdate) && enddate == startdate)
            {
                DateTime end = DateTime.Parse(enddate);
                enddate = end.AddDays(1).ToString("yyyy-MM-dd");
            }
            StringBuilder sql1 = new StringBuilder();
            //sql1.Append("  select count(case when t.isoverdue =0 then 1 end),count(case when t.isoverdue =1 then 1 end), ")
            //       .Append(" count(1),count(case when t.values1 like '%<link>1' then 1 end), ")
            //          .Append("count(case when t.inputstate =0 then 1 end), count(case when t.inputstate =5 then 1 end), ")
            //            .Append(" to_char(i.createtime,'YYYY-mm-dd  hh24') ")
            //     .Append(" from dz_history_input_yd t ,dz_history_image_yd i  ")
            //     .Append(" where t.imageid=i.imageid and i.createtime >to_date('" + startdate + " 00:00:00','yyyy-mm-dd hh24:mi:ss')   ")
            //     .Append(" and  i.createtime<=to_date('"+enddate+" 00:00:00','yyyy-mm-dd hh24:mi:ss')  ")
            //      .Append(" group by to_char(i.createtime,'YYYY-mm-dd  hh24')  ")
            //      .Append(" order by  to_char(i.createtime,'YYYY-mm-dd  hh24') ");
            sql1.Append("  select count(case when t.isoverdue =0 then 1 end),count(case when t.isoverdue =1 then 1 end), ")
                .Append(" count(1),count(case when t.values1 like '%<link>1' then 1 end), ")
                   .Append("count(case when t.inputstate =0 then 1 end), count(case when t.inputstate =5 then 1 end), ")
                     .Append(" to_char(i.zipcreatetime,'YYYY-mm-dd  hh24') ")
              .Append(" from dz_history_input_yd t ,dz_history_image_yd i  ")
              .Append(" where t.imageid=i.imageid and i.zipcreatetime >to_date('" + startdate + " 00:00:00','yyyy-mm-dd hh24:mi:ss')   ")
              .Append(" and  i.zipcreatetime<=to_date('" + enddate + " 00:00:00','yyyy-mm-dd hh24:mi:ss')  ")
               .Append(" group by to_char(i.zipcreatetime,'YYYY-mm-dd  hh24')  ")
               .Append(" order by  to_char(i.zipcreatetime,'YYYY-mm-dd  hh24') ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            //parmss[0].Value = startdate;
            //parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), null);
            List<string[]> arraylist = new List<string[]>();
            int arraylength = dr.FieldCount;
            while (dr.Read())
            {
                string[] array = new string[arraylength];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = dr[i].ToString();
                }

                arraylist.Add(array);
            }
            dr.Close();
            return arraylist;
        }
        public int IsHadSameZhongHe_history_yd(string projectname, string datestr)
        {
            StringBuilder sql1 = new StringBuilder();
            int result = 0;
            sql1.Clear().Append("select * from DZ_HOURSR_RECORD t where t.projectname='" + projectname + "' and datestr='" + datestr + "'");

            result = Common.OracleHelper.Query(conn, sql1.ToString(), System.Data.CommandType.Text, null).Tables[0].Rows.Count;

            return result;
        }

        public int SelectOverdueZhonghe_history_yd(string startdate, string enddate)
        {
            //DateTime end = DateTime.Parse(enddate);
            //enddate = end.AddDays(1).ToString("yyyy-MM-dd");
            StringBuilder sql1 = new StringBuilder();
            int result = 0;
            sql1.Clear().Append("  select  count(1) from dz_history_input_yd t,dz_history_image_yd i where t.imageid=i.imageid and i.zipcreatetime >to_date('" + startdate + "','yyyy-mm-dd hh24:mi:ss')  and t.isoverdue=1  and  i.zipcreatetime<=to_date('" + enddate + "','yyyy-mm-dd hh24:mi:ss')");

          string str   = Common.OracleHelper.ExecuteScalar(conn,System.Data.CommandType.Text, sql1.ToString(), null).ToString();
        result=  int.Parse(str);
            return result;
        }

        public string[] SelectOnlinePersonAndInput(string tab_key, int m)
        {
            string ishistory = "";
            if (tab_key=="yd")
            {
                ishistory = "history_";
            }
            string[] array = new string[4];
            StringBuilder sql1 = new StringBuilder();
            sql1.Clear().Append("select count(*) from dz_input_"+tab_key+" where inputstate=0");
          string str   = Common.OracleHelper.ExecuteScalar(conn,System.Data.CommandType.Text, sql1.ToString(), null).ToString();
          array[0] = str;
          sql1.Clear();
          sql1.Append("select count(*) from (select distinct userid1 From dz_"+ishistory+"input_"+tab_key+" where thedate1 > sysdate-"+m+"/24/60 union select distinct userid1 From dz_"+ishistory+"input_"+tab_key+" where thedate1 > sysdate-"+m+"/24/60)");
                 str   = Common.OracleHelper.ExecuteScalar(conn,System.Data.CommandType.Text, sql1.ToString(), null).ToString();
                 array[1] = str;
            sql1.Clear();
          sql1.Append("select count(*) from dz_input_"+tab_key+" where inputstate=88");
                 str   = Common.OracleHelper.ExecuteScalar(conn,System.Data.CommandType.Text, sql1.ToString(), null).ToString();
                 array[2] = str;
            sql1.Clear();
          sql1.Append("select count(*) from (select distinct userid3 From dz_input_"+tab_key+" where thedate1 > sysdate-"+m+"/24/60 union select distinct userid3 From dz_input_"+tab_key+" where thedate3 > sysdate-"+m+"/24/60)");
                 str   = Common.OracleHelper.ExecuteScalar(conn,System.Data.CommandType.Text, sql1.ToString(), null).ToString();
                 array[3] = str;
            return array;
        }

        /// <summary>
        /// 统计每小时的（未逾期量 逾期量 总上传量 退单量 修改量  
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<string[]> SelectZhongHe_yd(string startdate, string enddate)
        {
            if (!string.IsNullOrEmpty(startdate) && enddate == startdate)
            {
                DateTime end = DateTime.Parse(enddate);
                enddate = end.AddDays(1).ToString("yyyy-MM-dd");
            }
            StringBuilder sql1 = new StringBuilder();
            sql1.Append("  select count(case when t.isoverdue =0 then 1 end),count(case when t.isoverdue =1 then 1 end), ")
                   .Append(" count(1),count(case when t.values1 like '%<link>1' then 1 end), ")
                      .Append("count(case when t.inputstate =0 then 1 end), count(case when t.inputstate =5 then 1 end), ")
                        .Append(" to_char(i.zipcreatetime,'YYYY-mm-dd  hh24') ")
                 .Append(" from dz_input_yd t ,dz_image_yd i  ")
                 .Append(" where t.imageid=i.imageid and i.zipcreatetime >to_date('" + startdate + " 00:00:00','yyyy-mm-dd hh24:mi:ss')   ")
                 .Append(" and  i.zipcreatetime<=to_date('" + enddate + " 00:00:00','yyyy-mm-dd hh24:mi:ss')  and t.inputstate<>88")
                  .Append(" group by to_char(i.zipcreatetime,'YYYY-mm-dd  hh24')  ")
                  .Append(" order by  to_char(i.zipcreatetime,'YYYY-mm-dd  hh24') ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            //parmss[0].Value = startdate;
            //parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), null);
            List<string[]> arraylist = new List<string[]>();
            int arraylength = dr.FieldCount;
            while (dr.Read())
            {
                string[] array = new string[arraylength];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = dr[i].ToString();
                }

                arraylist.Add(array);
            }
            dr.Close();
            return arraylist;
        }

        public List<string[]> SelectOverdue(string projectname, string startdate, string enddate)
        {
            string sql = "select * from dz_inputoverdue t where t.datestr>='" + startdate + "' and t.datestr <='" + enddate + "' and t.projectname='" + projectname + "' order by t.datestr,t.hours ";
            var ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
            List<string[]> list = new List<string[]>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int colcount = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colcount];
                    for (int i = 0; i < colcount; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    list.Add(array);
                }
            }
            return list;
        }
        public List<string[]> SelectHourse_record(string projectname, string startdate, string enddate)
        {
            string sql = "select * from DZ_HOURSR_RECORD t where t.datestr>='" + startdate + "' and t.datestr <='" + enddate + "' and t.projectname='" + projectname + "' order by t.datestr,t.hours ";
            var ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, null);
            List<string[]> list = new List<string[]>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int colcount = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colcount];
                    for (int i = 0; i < colcount; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    list.Add(array);
                }
            }
            return list;
        }
        public int InsertHourse_record(string projectname, List<string[]> list)
        {
            StringBuilder sql1 = new StringBuilder();
            string datestr = "";
            int hours = -1;
            int result = 0;
            foreach (var item in list)
            {
                datestr = item[6].Split(' ')[0];
                hours = int.Parse(item[6].Split(' ')[2]);
                sql1.Clear().Append("insert into DZ_HOURSR_RECORD t(t.projectname,t.inputcount,t.overdue,t.RECIVNUM,t.UPLOADNUM,t.BACKNUM,t.CHCKERRNUM,t.WAITNUM,t.datestr,t.hours)")
                    .Append(" values ('" + projectname + "'," + item[0] + "," + item[1] + "," + item[2] + "," + item[2] + "," + item[3] + "," + item[5] + "," + item[4] + ",'" + datestr + "','" + hours + "')");
                result += Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql1.ToString(), null);
            }
            return result;
        }
        public int InsertInPutAndOverdue(string projectname, List<string[]> list)
        {
            StringBuilder sql1 = new StringBuilder();
            string datestr = "";
            int hours = -1;
            int result = 0;
            foreach (var item in list)
            {
                datestr = item[2].Split(' ')[0];
                hours = int.Parse(item[2].Split(' ')[2]);
                sql1.Clear().Append("insert into dz_inputoverdue t(t.projectname,t.inputcount,t.overdue,t.datestr,t.hours)")
                    .Append(" values ('" + projectname + "'," + item[0] + "," + item[1] + ",'" + datestr + "','" + hours + "')");
                result += Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql1.ToString(), null);

            }
            return result;
        }
        public int IsHadSameInPutAndOverdue(string projectname, string datestr)
        {
            StringBuilder sql1 = new StringBuilder();
            int result = 0;
            sql1.Clear().Append("select * from dz_inputoverdue t where t.projectname='" + projectname + "' and datestr='" + datestr + "'");

            result = Common.OracleHelper.Query(conn, sql1.ToString(), System.Data.CommandType.Text, null).Tables[0].Rows.Count;

            return result;
        }
        //public int CenterPro_back_yd(string startdate, string enddate,string objectid)
        //{ 
        //    string sql="select "
        //}
        /// <summary>
        /// 查询韵达在线员工数量及提交字节速率
        /// </summary>
        /// <param name="objid"></param>
        /// <param name="time_m"></param>
        /// <returns></returns>
        public string[] AllCen_YD_TJ(int objid, int time_m)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string[] result = new string[2];
            DateTime dtnow = DateTime.Now;
            string dt1 = dtnow.AddMinutes(0 - time_m).ToString("yyyy-MM-dd HH:mm:ss");
            string dt2 = dtnow.ToString("yyyy-MM-dd HH:mm:ss");
            sb1.Append("select count(distinct p.userid1) from dz_history_input_yd p,dz_user u where p.userid1=u.userid and u.objectid=:objid ");
            sb1.Append(" and p.thedate1>= to_date('" + dt1 + "','yyyy-mm-dd HH24:MI:SS') ")
                .Append("and p.thedate1 <= to_date('" + dt2 + "','yyyy-mm-dd HH24:MI:SS')");

            sb2.Append("select count(1) from dz_history_input_yd p,dz_user u where p.userid1=u.userid and u.objectid=:objid ");
            sb2.Append(" and p.thedate1>= to_date('" + dt1 + "','yyyy-mm-dd HH24:MI:SS') ")
                .Append("and p.thedate1 <= to_date('" + dt2 + "','yyyy-mm-dd HH24:MI:SS')");

            //select * from dz_input_pingan p,dz_user u where p.userid=u.userid and u.objectid=5
            // and p.inputime>= to_date('2016-11-15 08:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and p.inputime <= to_date('2016-11-15 22:59:59','yyyy-mm-dd HH24:MI:SS')

            //  select sum(p.inputlength) from dz_input_pingan p,dz_user u where p.userid=u.userid and u.objectid=5
            // and p.inputime>= to_date('2016-11-15 08:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and p.inputime <= to_date('2016-11-15 22:59:59','yyyy-mm-dd HH24:MI:SS')
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":objid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = objid;
            List<string[]> resultlist = new List<string[]>();
            object obj = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sb1.ToString(), parmss);

            result[0] = obj == null ? "0" : obj.ToString();
            object obj2 = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sb2.ToString(), parmss);
            result[1] = obj2 == null ? "0" : (float.Parse(obj2.ToString() == "" ? "0" : obj2.ToString()) / time_m).ToString("f2");

            return result;
        }
        /// <summary>
        /// 各中心产量 邮政
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> CenterProduction_ems(string startdate, string enddate, string objectid)
        {
            // select o.objectid, o.objectname,t.filename,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)
            //    from dz_output_ems t 
            //  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid
            //     where  t.thedate >='2016-10-01' 
            //           and  t.thedate <= '2016-10-09' 
            //group by o.objectid,o.objectname,t.filename order by o.objectid 

            StringBuilder sql1 = new StringBuilder();
            //sql1.Append("  select o.objectid, o.objectname,t.filename,sum(t.thenum) from dz_output_ems t   ")
            //    .Append("  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid   ")
            //    .Append("  where  t.thedate >=:startdate  ")
            //     .Append("   and  t.thedate <= :enddate  ");

            sql1.Append(" select o.objectid, o.objectname,t.filename,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum) ")
                .Append("  from dz_output_ems t   ")
                .Append("  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid  ")
                .Append(" where  t.thedate >=:startdate ")
                 .Append(" and  t.thedate <= :enddate  ");
            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql1.Append(" and o.objectid=" + objectid);
            }
            sql1.Append(" group by o.objectid,o.objectname,t.filename order by sum(t.thenum)  ");//, o.objectid
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[8];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                array[7] = dr[7].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        /// <summary>
        /// 本中心产量 韵达
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> MyCenterProduction_yd(string startdate, string enddate, string objectid)
        {
            //     select o.objectid, o.objectname,t.userid,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)
            //    from dz_output_yd t 
            //  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid
            //     where  t.thedate >='2016-10-01' 
            //           and  t.thedate <= '2016-10-09' 
            //               and o.objectid=2
            //group by o.objectid,o.objectname,t.userid order by o.objectid ,userid
            StringBuilder sql = new StringBuilder();
            sql.Append("   select o.objectid, o.objectname,t.userid,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)  ")
                .Append("  from dz_output_yd t  ")
                .Append("  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid  ")
                .Append("  where  t.thedate >=:startdate  ")
                 .Append("  and  t.thedate <= :enddate  ");

            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql.Append(" and o.objectid=" + objectid);
            }
            sql.Append(" group by o.objectid,o.objectname,t.userid order by o.objectid ,userid ");
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
                string[] array = new string[8];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                array[7] = dr[7].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        /// <summary>
        /// 中心A组产量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> SelectCenterProduction_yd(string startdate, string enddate, string objectid, string userid)
        {
            //     select o.objectid, o.objectname,t.userid,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)
            //    from dz_output_yd t 
            //  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid
            //     where  t.thedate >='2016-10-01' 
            //           and  t.thedate <= '2016-10-09' 
            //               and o.objectid=2
            //group by o.objectid,o.objectname,t.userid order by o.objectid ,userid
            StringBuilder sql = new StringBuilder();
            sql.Append("   select o.objectid, o.objectname,t.userid,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)  ")
                .Append("  from dz_output_yd t  ")
                .Append("  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid  ")
                .Append("  where  t.thedate >=:startdate  ")
                 .Append("  and  t.thedate <= :enddate  ");

            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql.Append(" and o.objectid=" + objectid);
            }
            if (!string.IsNullOrEmpty(userid))
            {
                sql.Append(" and t.userid like '" + userid + "%' ");
            }
            sql.Append(" group by o.objectid,o.objectname,t.userid order by o.objectid ,userid ");
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
                string[] array = new string[8];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                array[7] = dr[7].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }
        /// <summary>
        /// 本中心产量 邮政
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> MyCenterProduction_ems(string startdate, string enddate, string objectid)
        {
            //         select o.objectid, o.objectname,t.filename,t.userid,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum)
            //    from dz_output_ems t 
            //  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid
            //     where  t.thedate >='2016-10-01' 
            //           and  t.thedate <= '2016-10-09' 
            //               and o.objectid=2
            //group by o.objectid,o.objectname,t.filename,t.userid order by o.objectid ,userid

            StringBuilder sql1 = new StringBuilder();
            //sql1.Append("  select o.objectid, o.objectname,t.filename,sum(t.thenum) from dz_output_ems t   ")
            //    .Append("  join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid   ")
            //    .Append("  where  t.thedate >=:startdate  ")
            //     .Append("   and  t.thedate <= :enddate  ");

            sql1.Append("     select o.objectid, o.objectname,t.filename,t.userid,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ,sum(t.errnum) ,sum(t.backbum) ")
                .Append(" from dz_output_ems t    ")
                .Append("   join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid  ")
                .Append(" where  t.thedate >=:startdate ")
                 .Append("   and  t.thedate <= :enddate  ");
            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql1.Append(" and o.objectid=" + objectid);
            }
            sql1.Append("   group by o.objectid,o.objectname,t.filename,t.userid order by o.objectid ,t.userid");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[9];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                array[7] = dr[7].ToString();
                array[8] = dr[8].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }

        /// <summary>
        /// 各中心邮政统计单量 barcode
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="objectid"></param>
        /// <returns></returns>
        public List<string[]> AllCenterBarcodeNum_ems(string startdate, string enddate, string objectid)
        {
            StringBuilder sql1 = new StringBuilder();
            //            select u.objectid,o.objectname, to_char(t.createtime,'yyyy-mm-dd'), count( t.barcode) from  dz_image_ems t,dz_user u,dz_object o
            // where  t.userid=u.userid and o.objectid=u.objectid
            //  and t.createtime>= to_date('2016-11-01 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //and  t.createtime <=to_date('2016-12-01 00:00:00','yyyy-mm-dd HH24:MI:SS')
            //group by u.objectid,to_char(t.createtime,'yyyy-mm-dd'),o.objectname
            //order by u.objectid,to_char(t.createtime,'yyyy-mm-dd')

            sql1.Append("select u.objectid,o.objectname, to_char(t.createtime,'yyyy-mm-dd'), count(distinct t.barcode) from  dz_image_ems t,dz_user u,dz_object o ")
              .Append("  where t.userid=u.userid and o.objectid=u.objectid  ")
                .Append(" and  t.createtime >=to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                 .Append("   and  t.createtime <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')  ");
            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql1.Append(" and o.objectid=" + objectid);
            }
            sql1.Append(" group by u.objectid,to_char(t.createtime,'yyyy-mm-dd'),o.objectname order by u.objectid,to_char(t.createtime,'yyyy-mm-dd')");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            // OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql1.ToString(), System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    arraylist.Add(array);
                }
                return arraylist;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 产量校对
        /// </summary>
        /// <param name="thenum"></param>
        /// <param name="userid"></param>
        /// <param name="datestr"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool updateoutputems15_16(int thenum, string userid, string datestr, string filename)
        {
            StringBuilder sql1 = new StringBuilder();
            sql1.Append("update dz_output_ems t set t.thenum=" + thenum + " where t.userid='" + userid + "' and t.thedate='" + datestr + "' and t.filename='" + filename + "'");
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql1.ToString(), null);
            if (result == 0)
            {
                return insertoutputems15_16(thenum, userid, datestr, filename);
            }
            return result > 0;
        }
        public bool insertoutputems15_16(int thenum, string userid, string datestr, string filename)
        {
            int type = 0;
            switch (filename)
            {
                case "寄件人姓名": type = 1; break;
                case "寄件人电话": type = 2; break;
                case "寄件人地址": type = 3; break;
                case "收件人姓名": type = 4; break;
                case "收件人电话": type = 5; break;
                case "收件人地址": type = 6; break;
                case "物品重量": type = 7; break;
                default:
                    break;
            }
            if (type == 0)
            {
                return false;
            }
            StringBuilder sql1 = new StringBuilder();
            //sql1.Append("insert into dz_output_ems t set t.userid='" + userid + "',t.thedate='" + datestr + "',t.encode='',t.thenum="+thenum+"")
            //    .Append(" ,t.lv=0,t.integral=0,t.checknum=0,t.checkerrnum=0 ")
            //    .Append(" ,t.errnum=0,t.backbum=0,t.pagetype=11,t.filename='" + filename + "',t.filedtype="+type);
            sql1.Append("").Append("insert into dz_output_ems t(t.userid,t.thedate,t.encode,t.thenum,t.lv,t.integral,t.checknum,t.checkerrnum,t.errnum,t.backbum,t.pagetype,t.filename,t.filedtype)")
                .Append(" Values('" + userid + "','" + datestr + "','','" + thenum + "',0,0,0,0,0,0,11,'" + filename + "'," + type + ")");
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql1.ToString(), null);
            return result > 0;
        }


        public List<string[]> SelectPAvalueLength()
        {
            string sql = "select * from dz_input_pingan t where  t.inputime<=to_date('2016-11-15 16:59:59','yyyy-MM-dd HH24:MI:SS') ";
            List<string[]> resultlist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql.ToString(), System.Data.CommandType.Text, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    resultlist.Add(array);
                }
                return resultlist;
            }
            else
            {
                return null;
            }
        }
        public bool updatePAvalueLength(string[] array)
        {
            string sql = "  update dz_input_pingan t set t.inputlength=:length where t.inputvalue=:value"//to_date(,'yyyy-MM-dd HH24:MI:SS')
              + " and t.inputime=to_date(:datestr,'yyyy-MM-dd HH24:MI:SS')";
            // " and t.inputime<=to_date('2016-11-15 16:59:59','yyyy-MM-dd HH24:MI:SS')";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 //new Oracle.ManagedDataAccess.Client.OracleParameter(":wherestr",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":length",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":value",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,40),//,
                   new Oracle.ManagedDataAccess.Client.OracleParameter(":datestr",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)
                   //   new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
            };
            parmss[0].Value = int.Parse(array[3]);
            parmss[1].Value = array[6];
            parmss[2].Value = array[5];//DateTime.Parse();
            //parmss[3].Value = array[0];
            int a = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);
            return a > 0;

        }

        /// <summary>
        /// 每月韵达 接收上传 退单统计 按 天分
        /// </summary>
        public string[] EveryMonthYD_TJ(string zipname_s, string zipname_b)
        {
            //string  sql="  select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid     and i.zipname  >:smaller and i.zipname  <=:bigger";
            // string sq2="  select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid   and i.outstate=1   and i.zipname  >:smaller and i.zipname  <=:bigger";
            //string  sq3="  select count(1) from dz_input_yd p,dz_image_yd i where   i.imageid=p.imageid  and p.values3 like '%<link>1'   and i.zipname  >:smaller and i.zipname  <=:bigger";
            string sql = "  select count(1) from dz_history_input_yd p,dz_history_image_yd i where   i.imageid=p.imageid     and i.zipname  >='" + zipname_s + "' and i.zipname  <='" + zipname_b + "'";
            string sq2 = "  select count(1) from dz_history_input_yd p,dz_history_image_yd i where   i.imageid=p.imageid   and i.outstate=1   and i.zipname  >='" + zipname_s + "' and i.zipname  <='" + zipname_b + "'";
            string sq3 = "  select count(1) from dz_history_input_yd p,dz_history_image_yd i where   i.imageid=p.imageid  and i.outstate=1 and p.values3 like '%<link>1'   and i.zipname  >='" + zipname_s + "' and i.zipname  <='" + zipname_b + "'";

            string[] array = { "0", "0", "0" };

            array[0] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql).ToString();
            array[1] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sq2).ToString();
            array[2] = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sq3).ToString();
            // List<string[]> resultlist = new List<string[]>();
            return array;
        }
        #region 平安产量查询
        public List<string[]> LoadPageEntities_PA(string wherestr, int pagesize, int page, out int count)
        {
            string sqlcount = " select count(1) from (select 1 from dz_input_pingan t ,dz_user u  where  u.userid=t.userid " + wherestr + " group by to_char(t.inputime,'yyyy-MM-DD'),t.userid) temp";
            count = 0;
            object obj = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlcount.ToString(), null);
            int.TryParse(obj.ToString(), out count);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT *  FROM (SELECT tt.*, ROWNUM AS rowno  FROM (  SELECT t.userid,to_char(t.inputime,'yyyy-MM-dd') ,sum(t.inputlength) ")//,SUBSTR(t.inputype,0, INSTR(t.inputype,'$', 5, 1)-1)
                     .Append("  FROM dz_input_pingan t,dz_object o,dz_user u ")
                     .Append("  WHERE o.objectid=u.objectid and u.userid=t.userid ").Append(wherestr)//and t.userid='gws'
                     .Append("  group by t.userid, to_char(t.inputime,'yyyy-MM-dd') ")//,SUBSTR(t.inputype,0, INSTR(t.inputype,'$', 5, 1)-1)
                      .Append("order by to_char(t.inputime,'yyyy-MM-dd'),t.userid) tt")
                      .Append(" WHERE ROWNUM <= :bigger) table_alias")
                      .Append(" WHERE table_alias.rowno >:smaller");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 //new Oracle.ManagedDataAccess.Client.OracleParameter(":wherestr",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":bigger",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":smaller",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = pagesize * page;
            parmss[1].Value = pagesize * (page - 1);
            List<string[]> resultlist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql.ToString(), System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    resultlist.Add(array);
                }
                return resultlist;
            }
            else
            {
                return null;
            }
        }
        public List<string[]> LoadPageEntitiesAllCen_PA(string wherestr, int pagesize, int page, out int count)//wherestr 均未使用参数化查询
        {

            //        SELECT *  FROM (SELECT tt.*, ROWNUM AS rowno  FROM
            //( SELECT u.objectid ,o.objectname,sum(t.inputlength) 
            //  FROM dz_input_pingan t,dz_object o,dz_user u  
            //   WHERE o.objectid=u.objectid and u.userid=t.userid 
            //   and t.inputime>to_date('2016-11-09 00:00:00','yyyy-MM-dd HH24:MI:SS')
            //   and t.inputime<to_date('2016-11-14 00:00:00','yyyy-MM-dd HH24:MI:SS') 
            //    group by u.objectid ,o.objectname
            //     order by u.objectid, sum(t.inputlength) desc) tt WHERE ROWNUM <= 20) table_alias 
            //     WHERE table_alias.rowno >0

            string sqlcount = "select count(distinct u.objectid ) from dz_input_pingan t ,dz_user u where 1=1 and u.userid=t.userid " + wherestr;
            count = 0;
            object obj = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlcount.ToString(), null);
            int.TryParse(obj.ToString(), out count);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT *  FROM (SELECT tt.*, ROWNUM AS rowno  FROM (SELECT u.objectid ,o.objectname,sum(t.inputlength),count(t.image) ")//,SUBSTR(t.inputype,0, INSTR(t.inputype,'$', 5, 1)-1)
                     .Append("  FROM dz_input_pingan t,dz_object o,dz_user u   ")
                     .Append("  WHERE o.objectid=u.objectid and u.userid=t.userid  ").Append(wherestr)//and t.userid='gws'
                     .Append("  group by u.objectid ,o.objectname ")//,SUBSTR(t.inputype,0, INSTR(t.inputype,'$', 5, 1)-1)
                      .Append(" order by sum(t.inputlength), u.objectid desc) tt")
                      .Append(" WHERE ROWNUM <= :bigger) table_alias")
                      .Append(" WHERE table_alias.rowno >:smaller");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 //new Oracle.ManagedDataAccess.Client.OracleParameter(":wherestr",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":bigger",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":smaller",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = pagesize * page;
            parmss[1].Value = pagesize * (page - 1);
            List<string[]> resultlist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql.ToString(), System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    resultlist.Add(array);
                }
                return resultlist;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="objid"></param>
        /// <param name="time_m"></param>
        /// <returns></returns>
        public string[] AllCen_PA_TJ(int objid, int time_m)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string[] result = new string[2];
            DateTime dtnow = DateTime.Now;
            string dt1 = dtnow.AddMinutes(0 - time_m).ToString("yyyy-MM-dd HH:mm:ss");
            string dt2 = dtnow.ToString("yyyy-MM-dd HH:mm:ss");
            sb1.Append("select count(1) from dz_input_pingan p,dz_user u where p.userid=u.userid and u.objectid=:objid ");
            sb1.Append(" and p.inputime>= to_date('" + dt1 + "','yyyy-mm-dd HH24:MI:SS') ")
                .Append("and p.inputime <= to_date('" + dt2 + "','yyyy-mm-dd HH24:MI:SS')");

            sb2.Append("select sum(p.inputlength) from dz_input_pingan p,dz_user u where p.userid=u.userid and u.objectid=:objid ");
            sb2.Append(" and p.inputime>= to_date('" + dt1 + "','yyyy-mm-dd HH24:MI:SS') ")
                .Append("and p.inputime <= to_date('" + dt2 + "','yyyy-mm-dd HH24:MI:SS')");

            //select * from dz_input_pingan p,dz_user u where p.userid=u.userid and u.objectid=5
            // and p.inputime>= to_date('2016-11-15 08:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and p.inputime <= to_date('2016-11-15 22:59:59','yyyy-mm-dd HH24:MI:SS')

            //  select sum(p.inputlength) from dz_input_pingan p,dz_user u where p.userid=u.userid and u.objectid=5
            // and p.inputime>= to_date('2016-11-15 08:00:00','yyyy-mm-dd HH24:MI:SS') 
            //  and p.inputime <= to_date('2016-11-15 22:59:59','yyyy-mm-dd HH24:MI:SS')
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":objid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = objid;
            List<string[]> resultlist = new List<string[]>();
            object obj = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sb1.ToString(), parmss);

            result[0] = obj == null ? "0" : obj.ToString();
            object obj2 = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sb2.ToString(), parmss);
            result[1] = obj2 == null ? "0" : (float.Parse(obj2.ToString() == "" ? "0" : obj2.ToString()) / time_m).ToString("f2");

            return result;
        }
        #endregion

        public void updatePA_newlength(string start, string end, double nums)
        {
            string sql = "select * from DZ_INPUT_PINGAN t where t.inputime > to_date('" + start + "','yyyy-mm-dd HH24:MI:SS')  and  t.inputime < to_date('" + end + "','yyyy-mm-dd HH24:MI:SS')";
            //List<string[]> resultlist = new List<string[]>();
            //DataSet ds = Common.OracleHelper.Query(conn_pa, sql.ToString(), System.Data.CommandType.Text, null);
            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    int colnum = ds.Tables[0].Columns.Count;
            //    foreach (DataRow item in ds.Tables[0].Rows)
            //    {
            //        string[] array = new string[colnum];
            //        for (int i = 0; i < colnum; i++)
            //        {
            //            array[i] = item[i].ToString();
            //        }
            //        Random r = new Random();
            //        nums = nums - r.NextDouble() * 2;
            //        array[7] =(int.Parse(array[3]) * nums/100).ToString();
            //        string sql2 = "update DZ_INPUT_PINGAN t set t.rightlength='" + array[7] + "'where t.image='" + array[1] + "' and t.userid='" + array[0] + "'";
            //        string a = Common.OracleHelper.ExecuteScalar(conn_pa, System.Data.CommandType.Text, sql2.ToString()).ToString();
            //        resultlist.Add(array);
            //        a += 1;
            //    }

            //}
            //else
            //{

            //}
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn_pa, System.Data.CommandType.Text, sql.ToString(), null);
            List<string[]> arraylist = new List<string[]>();

            Random r = new Random();
            int a = 0;
            while (dr.Read())
            {
                //string[] array = new string[8];
                //array[0] = dr[0].ToString();
                //array[1] = dr[1].ToString();
                //array[2] = dr[2].ToString();
                //array[3] = dr[3].ToString();
                //array[4] = dr[4].ToString();
                //array[5] = dr[5].ToString();
                //array[6] = dr[6].ToString();

                double bili = nums - r.NextDouble() * 2;
                string rightlength = (int.Parse(dr[3].ToString()) * bili / 100).ToString("f0");
                string sql2 = "update DZ_INPUT_PINGAN t set t.rightlength='" + rightlength + "'where t.image='" + dr[1].ToString() + "' and t.userid='" + dr[0].ToString() + "'";
                a += Common.OracleHelper.ExecuteNonQuery(conn_pa, System.Data.CommandType.Text, sql2.ToString());

            }

            dr.Close();
        }


        public void repireOutput()
        {
            List<string[]> list = repireOutput_yd_deal();
            int a = 0;
            foreach (var item in list)
            {
                string sql = "  update dz_output_yd t set t.thenum='" + item[2] + "' where t.userid='" + item[0] + "' and t.thedate='" + item[1] + "' ";
                a += Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql);
            }

        }
        public List<string[]> repireOutput_yd_deal()
        {


            string sql = " select t.userid1,to_char(t.thedate1,'yyyy-mm-dd'), count(*) from DZ_input_yd t ,dz_user u" +
                  " where t.userid1=u.userid and u.objectid=13" +
  " and t.thedate1  > to_date('2016-11-20 00:00:00','yyyy-mm-dd HH24:MI:SS')" +
  " and   t.thedate1 < to_date('2016-11-21 00:00:00','yyyy-mm-dd HH24:MI:SS')" +
  "  group by to_char(t.thedate1,'yyyy-mm-dd'),t.userid1";

            // OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    arraylist.Add(array);
                }
                return arraylist;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 上海捷记各中心产量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> AllCenterOutput_shjj(string startdate, string enddate)
        {
            //             select o.objectid,o.objectname,t.filename,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum) from dz_output_shjj t,dz_user u,dz_object o where
            //u.objectid=o.objectid and t.userid=u.userid
            //and t.thedate>'2016-12-12' and t.thedate<'2016-12-15'
            //group by o.objectid,o.objectname,t.filename
            //order by o.objectid,sum(t.thenum)
            StringBuilder sql = new StringBuilder();
            sql.Append(" select o.objectid,o.objectname,t.filename,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum),sum(t.errnum) ,sum(t.backbum) from dz_output_shjj t,dz_user u,dz_object o where")
                .Append(" u.objectid=o.objectid and t.userid=u.userid")
                .Append(" and t.thedate>=:startdate and t.thedate<=:enddate")
                .Append(" group by o.objectid,o.objectname,t.filename")
                .Append(" order by o.objectid,sum(t.thenum)");


            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            // OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql.ToString(), System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    arraylist.Add(array);
                }
                return arraylist;
            }
            else
            {
                return null;
            }
        }

        public List<string[]> AllCenterBarcodeNum_shjj(string startdate, string enddate, string objectid)
        {
            StringBuilder sql1 = new StringBuilder();
            //            select u.objectid,o.objectname, to_char(t.createtime,'yyyy-mm-dd'), count( t.barcode) from  dz_image_ems t,dz_user u,dz_object o
            // where  t.userid=u.userid and o.objectid=u.objectid
            //  and t.createtime>= to_date('2016-11-01 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //and  t.createtime <=to_date('2016-12-01 00:00:00','yyyy-mm-dd HH24:MI:SS')
            //group by u.objectid,to_char(t.createtime,'yyyy-mm-dd'),o.objectname
            //order by u.objectid,to_char(t.createtime,'yyyy-mm-dd')

            sql1.Append("select u.objectid,o.objectname, to_char(t.createtime,'yyyy-mm-dd'), count(distinct t.barcode) from  dz_image_shjj t,dz_user u,dz_object o ")
              .Append("  where t.userid=u.userid and o.objectid=u.objectid  ")
                .Append(" and  t.createtime >=to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                 .Append("   and  t.createtime <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')  ");
            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql1.Append(" and o.objectid=" + objectid);
            }
            sql1.Append(" group by u.objectid,to_char(t.createtime,'yyyy-mm-dd'),o.objectname order by u.objectid,to_char(t.createtime,'yyyy-mm-dd')");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            // OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql1.ToString(), System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    arraylist.Add(array);
                }
                return arraylist;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 本中心产量 捷记
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> MyCenterProduction_shjj(string startdate, string enddate, string objectid)
        {


            StringBuilder sql1 = new StringBuilder();
            sql1.Append("     select o.objectid, o.objectname,t.filename,t.userid,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum),sum(t.errnum) ,sum(t.backbum) ")
                .Append(" from dz_output_shjj t    ")
                .Append("   join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid  ")
                .Append(" where  t.thedate >=:startdate ")
                 .Append("   and  t.thedate <= :enddate  ");
            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql1.Append(" and o.objectid=" + objectid);
            }
            sql1.Append("   group by o.objectid,o.objectname,t.filename,t.userid order by o.objectid ,t.userid");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[10];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                array[7] = dr[7].ToString();
                array[8] = dr[8].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }

        #region 安能


        /// <summary>
        /// 安能物流各中心产量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> AllCenterOutput_anwl(string startdate, string enddate)
        {
            //             select o.objectid,o.objectname,t.filename,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum) ，sum(t.errnum) ,sum(t.backbum) from dz_output_shjj t,dz_user u,dz_object o where
            //u.objectid=o.objectid and t.userid=u.userid
            //and t.thedate>'2016-12-12' and t.thedate<'2016-12-15'
            //group by o.objectid,o.objectname,t.filename
            //order by o.objectid,sum(t.thenum)
            StringBuilder sql = new StringBuilder();
            sql.Append(" select o.objectid,o.objectname,t.filename,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum),sum(t.errnum) ,sum(t.backbum) from dz_output_anwl t,dz_user u,dz_object o where")
                .Append(" u.objectid=o.objectid and t.userid=u.userid")
                .Append(" and t.thedate>=:startdate and t.thedate<=:enddate")
                .Append(" group by o.objectid,o.objectname,t.filename")
                .Append(" order by o.objectid,sum(t.thenum)");


            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            // OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql.ToString(), System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    arraylist.Add(array);
                }
                return arraylist;
            }
            else
            {
                return null;
            }
        }

        public List<string[]> AllCenterBarcodeNum_anwl(string startdate, string enddate, string objectid)
        {
            StringBuilder sql1 = new StringBuilder();
            //            select u.objectid,o.objectname, to_char(t.createtime,'yyyy-mm-dd'), count( t.barcode) from  dz_image_ems t,dz_user u,dz_object o
            // where  t.userid=u.userid and o.objectid=u.objectid
            //  and t.createtime>= to_date('2016-11-01 00:00:00','yyyy-mm-dd HH24:MI:SS') 
            //and  t.createtime <=to_date('2016-12-01 00:00:00','yyyy-mm-dd HH24:MI:SS')
            //group by u.objectid,to_char(t.createtime,'yyyy-mm-dd'),o.objectname
            //order by u.objectid,to_char(t.createtime,'yyyy-mm-dd')

            sql1.Append("select u.objectid,o.objectname, to_char(t.createtime,'yyyy-mm-dd'), count(distinct t.barcode) from  dz_image_anwl t,dz_user u,dz_object o ")
              .Append("  where t.userid=u.userid and o.objectid=u.objectid  ")
                .Append(" and  t.createtime >=to_date(:startdate,'yyyy-mm-dd HH24:MI:SS') ")
                 .Append("   and  t.createtime <= to_date(:enddate,'yyyy-mm-dd HH24:MI:SS')  ");
            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql1.Append(" and o.objectid=" + objectid);
            }
            sql1.Append(" group by u.objectid,to_char(t.createtime,'yyyy-mm-dd'),o.objectname order by u.objectid,to_char(t.createtime,'yyyy-mm-dd')");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            // OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql1.ToString(), System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colnum = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colnum];
                    for (int i = 0; i < colnum; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    arraylist.Add(array);
                }
                return arraylist;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 本中心产量 anwl
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> MyCenterProduction_anwl(string startdate, string enddate, string objectid)
        {


            StringBuilder sql1 = new StringBuilder();
            sql1.Append("     select o.objectid, o.objectname,t.filename,t.userid,sum(t.thenum),sum(t.checknum) ,sum(t.checkerrnum),sum(t.errnum) ,sum(t.backbum) ")
                .Append(" from dz_output_anwl t    ")
                .Append("   join dz_user u on t.userid=u.userid join dz_object o on u.objectid=o.objectid  ")
                .Append(" where  t.thedate >=:startdate ")
                 .Append("   and  t.thedate <= :enddate  ");
            if (!string.IsNullOrEmpty(objectid) && objectid != "*")
            {
                sql1.Append(" and o.objectid=" + objectid);
            }
            sql1.Append("   group by o.objectid,o.objectname,t.filename,t.userid order by o.objectid ,t.userid");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":startdate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":enddate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,30)
             };
            parmss[0].Value = startdate;
            parmss[1].Value = enddate;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql1.ToString(), parmss);
            List<string[]> arraylist = new List<string[]>();
            while (dr.Read())
            {
                string[] array = new string[10];
                array[0] = dr[0].ToString();
                array[1] = dr[1].ToString();
                array[2] = dr[2].ToString();
                array[3] = dr[3].ToString();
                array[4] = dr[4].ToString();
                array[5] = dr[5].ToString();
                array[6] = dr[6].ToString();
                array[7] = dr[7].ToString();
                array[8] = dr[8].ToString();
                arraylist.Add(array);
            }

            dr.Close();
            return arraylist;
        }


        public string[] anwlwaitoinput(string startdate, string enddate)
        {
            return null;
        }
        #endregion
        public void UpdateYD_output()
        {
            string sql = "select * from dz_output_yd t where t.thedate>='2017-01-06' and t.thedate<='2017-01-31'";
            //    List<string[]> arraylist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            if (ds != null && ds.Tables.Count > 0)
            {
                int count = 0;

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    count++;
                    UpdateYD_output2(item[0].ToString(), item[1].ToString(), int.Parse(item[9].ToString()));
                    //string[] array = new string[colnum];
                    //for (int i = 0; i < colnum; i++)
                    //{
                    //    array[i] = item[i].ToString();
                    //}
                    //arraylist.Add(array);
                }

            }

        }
        public void UpdateYD_output2(string userid, string thedate, int backcount)
        {
            string sql = "select count(1) from dz_input_yd t where t.values1 like '%<link>1' and to_char( t.thedate1,'yyyy-mm-dd')='" + thedate + "'  and t.userid1='" + userid + "'";
            string sqlupdate = "";
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            string result = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                result = ds.Tables[0].Rows[0][0].ToString();
                if (int.Parse(result) != backcount)
                {
                    sqlupdate = "update dz_output_yd t set t.backbum=" + result + " where t.userid='" + userid + "' and t.thedate='" + thedate + "'";
                    Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sqlupdate);
                }

            }
        }
        public List<string[]> GetRecordUpRecv_yd(string start,string end)
        { 
                string sql = "select * from dz_uprec_yd t where t.thedate>='"+start+"' and t.thedate<='"+end+"'";
                List<string[]> arraylist = new List<string[]>();
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            if (ds != null && ds.Tables.Count > 0)
            {
                
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array=new  string[6];
                    array[0] = item["thedate"].ToString();
                    array[1] = item["content"].ToString();
                    arraylist.Add(array);
                }
            }
            return arraylist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thedate">日期</param>
        /// <param name="liststr">已下划线分割的集合字串 </param>
        /// <returns></returns>
        public bool GetInsertUpRecv_yd(string thedate, string liststr)
        { 
             string sql = "select count(1) from dz_uprec_yd t where t.thedate ='"+thedate+"'";// and to_char( t.thedate1,'yyyy-mm-dd')='" + thedate + "'  and t.userid1='" + userid + "'";
            string sqlupdate = "";
            int count =int.Parse( Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text,sql).ToString());
            bool result = false;
            if (count== 0)
            {
                 sqlupdate = "insert into dz_uprec_yd t (t.thedate,t.content) values ('"+thedate+"','"+liststr+"')";
                 result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sqlupdate) > 0;
            }
            return result;
        }
    }
}
