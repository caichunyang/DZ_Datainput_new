using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
    public class dz_output_dal
    {
        public static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public static string conn_pa = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_pa"].ConnectionString;
        public bool insert(string datenum, string userid, float bili, int rightlength)
        {
            string sql = "insert  into  dz_output_pa (datestr,userid,bili,rightlength)" +
                "  values(:datestr,:userid,:bili,:rightlength)";

            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":datestr",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":bili",Oracle.ManagedDataAccess.Client.OracleDbType.BinaryFloat),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":rightlength",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
                  
             };
            DateTime datenow = DateTime.Now;

            parmss[0].Value = datenum;
            parmss[1].Value = userid;
            parmss[2].Value = bili;
            parmss[3].Value = rightlength;
            int result = Common.OracleHelper.ExecuteNonQuery(conn_pa, System.Data.CommandType.Text, sql, parmss);

            return result > 0;

        }
        public int GetRightlength(string datenum, string userid)
        {
            string sql = "select t.rightlength from dz_output_pa t where t.userid='" + userid + "' and t.datestr='" + datenum + "'";
            //object obj=Common.OracleHelper.ExecuteScalar(conn_pa, System.Data.CommandType.Text, sql);
            DataSet ds = Common.OracleHelper.Query(conn_pa, sql, System.Data.CommandType.Text);
            try
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        int right = int.Parse(item[0].ToString());
                        if (right > 0)
                        {
                            return right;
                        }
                    }
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #region  上海捷记导出
        public List<string[]> ShjjExcelGenart(string startdate, string enddate)
        {
            string sql = "select  i.barcode,p.values3,i.filedtype from dz_image_shjj i,dz_input_shjj p  where p.imageid=i.imageid and  i.createtime > to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS')  and  i.createtime <= to_date('" + enddate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')";
            //  string sqlbarcode = "select distinct i.barcode from dz_image_shjj i where i.createtime > to_date('2016-11-01 00:00:00','yyyy-mm-dd HH24:MI:SS')  and  t.inputime < to_date('2016-12-01 00:00:00','yyyy-mm-dd HH24:MI:SS')";

            //Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
            //    new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)
            //};
            //parmss[0].Value = key;
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            List<string[]> list = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
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
                return list;
            }
            return null;
        }
        public List<string[]> ShjjExcelGenart_y(string boxno,string userid)
        {
            string sql = " select  i.barcode,p.values3,i.filedtype from dz_image_shjj i,dz_box_shjj b, dz_input_shjj p  where p.imageid=i.imageid and lower(b.barcode)=lower(i.barcode) and b.boxno='" + boxno + "' and i.userid='"+userid+"'" ;

            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            List<string[]> list = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
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
                return list;
            }
            return null;
        }
           public List<string[]> AnwlExcelGenart_y(string boxno,string userid)
        {
            string sql = " select  i.barcode,p.values3,i.filedtype from dz_image_anwl i,dz_box_anwl b, dz_input_anwl p  where p.imageid=i.imageid and lower(b.barcode)=lower(i.barcode) and b.boxno='" + boxno + "' and i.userid='"+userid+"'" ;

            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            List<string[]> list = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
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
                return list;
            }
            return null;
        }
        #endregion
    }
}
