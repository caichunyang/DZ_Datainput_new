using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
    public class DZ_INPUT_PA_DAL
    {
        public static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_pa"].ConnectionString;
        public static string conn_114 = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
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
                      .Append(" order by  sum(t.inputlength) desc,u.objectid desc) tt")
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
        /// 查询平安在线员工数量及提交字节速率
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
            sb1.Append("select count(distinct p.userid) from dz_input_pingan p,dz_user u where p.userid=u.userid and u.objectid=:objid ");
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
        static void TestParallelForeach(List<int> testData)
        {
            //记录结果用  
            DateTime time1 = DateTime.Now;
            System.Collections.Concurrent.ConcurrentStack<int> resultData = new System.Collections.Concurrent.ConcurrentStack<int>();
            
            Console.WriteLine(string.Format("Parallel.ForEach:t{0} in {1}", testData.Sum(), (DateTime.Now - time1).TotalMilliseconds));
        }  
        /// <summary>
        /// 将阿里云平安数据转移119
        /// </summary>
        public void TransferrData()
        {
            string sql = "select * from dz_input_pingan t where t.inputime>to_date('2016-11-19 00:00:00','yyyy-mm-dd HH24:MI:SS') and  t.inputime <= to_date('2016-11-21 23:59:59','yyyy-mm-dd HH24:MI:SS') order by t.inputime desc ";
            //string sql = "select * from dz_input_pingan t where t.inputime>to_date('2016-11-19 00:00:00','yyyy-mm-dd HH24:MI:SS') and  t.inputime <= to_date('2016-11-19 23:59:59','yyyy-mm-dd HH24:MI:SS') ";
            string sql2 = "insert into dz_input_pingan t values(:userid,:image,:inputype,:inputlength,:returntype,:inputime,:inputvalue)";
           // string sql_up = "update dz_input_pingan t set t.userid=:userid,t.image=:image,t.inputype=:inputype,t.inputlength=:inputlength,t.returntype=:returntype,t.inputime=:inputime,t.inputvalue=:inputvalue) ";
            DataSet ds = Common.OracleHelper.Query(conn_114, sql, CommandType.Text, null);
          

     

            if (ds != null && ds.Tables[0].Rows.Count> 0)
            {
                int b = 0;
                //Parallel.ForEach<DataRow>(ds.Tables[0].Rows, s => { });
                DataRowCollection drrows = ds.Tables[0].Rows;
                //Parallel.For(0, drcoll.Count, (i) =>
                //{
                //    string sqlexeist = "select count(1) from dz_input_pingan t where t.image='" + drcoll[i][1] + "'";
                //    Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                //  new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                //   new Oracle.ManagedDataAccess.Client.OracleParameter(":image",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,40),
                //    new Oracle.ManagedDataAccess.Client.OracleParameter(":inputype",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                //     new Oracle.ManagedDataAccess.Client.OracleParameter(":inputlength",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                //      new Oracle.ManagedDataAccess.Client.OracleParameter(":returntype",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                //    new Oracle.ManagedDataAccess.Client.OracleParameter(":inputime",Oracle.ManagedDataAccess.Client.OracleDbType.Date),
                //     new Oracle.ManagedDataAccess.Client.OracleParameter(":inputvalue",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,80)};
                //    parmss[0].Value = drcoll[i][0];
                //    parmss[1].Value = drcoll[i][1];
                //    parmss[2].Value = drcoll[i][2];
                //    parmss[3].Value = drcoll[i][3];
                //    parmss[4].Value = drcoll[i][4];
                //    parmss[5].Value = DateTime.Parse(drcoll[i][5].ToString());
                //    parmss[6].Value = drcoll[i][6];
                //    object obj = Common.OracleHelper.ExecuteScalar(conn, CommandType.Text, sqlexeist, null);
                //    string a=(obj==null?"0":obj.ToString());
                //    if (a == "0")
                //    {
                //        //Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql2, parmss);
                //        Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql2, parmss);
                //    }
                //    else if (int.Parse(a) > 1)
                //    {
                //        string sql3 = "delete from dz_input_pingan t where t.image='" + drcoll[i]["image"] + "'";
                //        Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql3, parmss);
                //        Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql2, parmss);
                //    }
                  
                  
                   
                //});
                foreach (DataRow item in drrows)
                {
                    b++;
                    string sqlexeist = "select count(1) from dz_input_pingan t where t.image='" + item[1] + "'";
                    Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                   new Oracle.ManagedDataAccess.Client.OracleParameter(":image",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,40),
                    new Oracle.ManagedDataAccess.Client.OracleParameter(":inputype",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                     new Oracle.ManagedDataAccess.Client.OracleParameter(":inputlength",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                      new Oracle.ManagedDataAccess.Client.OracleParameter(":returntype",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                    new Oracle.ManagedDataAccess.Client.OracleParameter(":inputime",Oracle.ManagedDataAccess.Client.OracleDbType.Date),
                     new Oracle.ManagedDataAccess.Client.OracleParameter(":inputvalue",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,80)};
                    parmss[0].Value = item[0];
                    parmss[1].Value = item[1];
                    parmss[2].Value = item[2];
                    parmss[3].Value = item[3];
                    parmss[4].Value = item[4];
                    parmss[5].Value = DateTime.Parse(item[5].ToString());
                    parmss[6].Value = item[6];
                    object obj = Common.OracleHelper.ExecuteScalar(conn, CommandType.Text, sqlexeist, null);
                        string a=(obj==null?"0":obj.ToString());
                    if (a == "0")
                    {
                        //Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql2, parmss);
                    }
                    else if (int.Parse(a) > 1)
                    {
                        string sql3 = "delete from dz_input_pingan t where t.image='" + item["image"] + "'";
                        Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql3, parmss);
                    }
                    else if (a == "1")
                    {
                        continue;
                    }
                    Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql2, parmss);

                }


            }

        }
    }

}
