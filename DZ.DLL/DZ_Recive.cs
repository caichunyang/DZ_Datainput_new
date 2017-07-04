using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
    public class DZ_Recive
    {
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public bool CheckExist(string[] dataobj, string objname)
        {
            string sql = "select * from dz_recive where datestr='" + dataobj[0] + "' and objname='" + objname + "'";
            //  MyData MyData = new MyData();
            long result = 0;
            //if (MyData.OpenBase() == true)
            //{
            //    // Oracle.ManagedDataAccess.Client.OracleDataReader OraDt = MyData.RunSQLDr(sql.ToString());
            //    result = MyData.RunSQL(sql);

            //}
            //MyData.CloseBase();
            Common.OracleHelper.ExecuteNonQuery(conn, CommandType.Text, sql, null);
            return result > 0;
        }

        public string[] GetModel(string datastr, string objname)
        {
            string sql = "select * from dz_recive where datestr='" + datastr + "' and objname='" + objname + "'";

            DataSet ds = Common.OracleHelper.Query(conn, sql, CommandType.Text, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                int lenth = ds.Tables[0].Columns.Count;
              
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string[] strarray = new string[lenth];

                    for (int i = 0; i < lenth; i++)
                    {
                        strarray[i] = row[i].ToString();
                    }
                    return strarray;
                }
            }
            return null;
        }
        public bool Insert(string[] data)
        {
            string sql = "insert  into  dz_recive (datestr,data1,data2,data3,data4,objname,creattime)" +
                "  values(:datestr,:data1,:data2,:data3,:data4,:objname,to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','yyyy/mm/dd hh24:mi:ss'))";

            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                new Oracle.ManagedDataAccess.Client.OracleParameter(":datestr",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":data1",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                   new Oracle.ManagedDataAccess.Client.OracleParameter(":data2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                     new Oracle.ManagedDataAccess.Client.OracleParameter(":data3",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                       new Oracle.ManagedDataAccess.Client.OracleParameter(":data4",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                        new Oracle.ManagedDataAccess.Client.OracleParameter(":objname",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)
                  
             };
            DateTime datenow = DateTime.Now;

            parmss[0].Value = data[0];
            parmss[1].Value =int.Parse(data[1].ToString());
            parmss[2].Value = int.Parse(data[2].ToString());
            parmss[3].Value = int.Parse(data[3].ToString());
            parmss[4].Value = 0;
            parmss[5].Value = data[5];
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;
        }

          public bool Update(string[] data)
        {
            string sql = "update  dz_recive t set t.data1="+data[1]+",t.data2="+data[2]+",t.data3="+data[3]+",t.data4=0,t.objname='"+data[5]+"',t.creattime= to_date('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "','yyyy/mm/dd hh24:mi:ss')" +
                " where t.datestr='"+data[0]+"' ";

            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            //Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
            //    new Oracle.ManagedDataAccess.Client.OracleParameter(":datestr",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
            //     new Oracle.ManagedDataAccess.Client.OracleParameter(":data1",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
            //       new Oracle.ManagedDataAccess.Client.OracleParameter(":data2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
            //         new Oracle.ManagedDataAccess.Client.OracleParameter(":data3",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
            //           new Oracle.ManagedDataAccess.Client.OracleParameter(":data4",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
            //            new Oracle.ManagedDataAccess.Client.OracleParameter(":objname",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)
                  
            // };
            //DateTime datenow = DateTime.Now;

            //parmss[0].Value = data[0];
            //parmss[1].Value =int.Parse(data[1].ToString());
            //parmss[2].Value = int.Parse(data[2].ToString());
            //parmss[3].Value = int.Parse(data[3].ToString());
            //parmss[4].Value = 0;
            //parmss[5].Value = data[5];
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, null);

            return result > 0;
        }
    }
}
