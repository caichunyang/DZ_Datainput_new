using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DZ.DLL
{
   public class DZ_ROLEMODULE_DAL
    {
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public List<int[]> GetRoleModule(int roleid, int moduleid)
        {
            string sql = "select * from dz_rolemodule where roleid=:roleid and moduleid=:moduleid";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":roleid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
             };
            parmss[0].Value = roleid;
            parmss[1].Value = moduleid;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, parmss);
            List<int[]> list = new List<int[]>();
            while (dr.Read())
            {
                int[] temparray = new int[2];
                temparray[0] = int.Parse(dr["roleid"].ToString());
                temparray[1] = int.Parse(dr["moduleid"].ToString());
                list.Add(temparray);
            }

            dr.Close();
            return list;
        }
        public bool AddRoleModule(int roleid,int moduleid)
        {
            string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":roleid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
             };
            parmss[0].Value = roleid;
            parmss[1].Value = moduleid;
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;
        }
        public bool DeleteRoleModule(int roleid, int moduleid)
        {
            string sql = "delete from dz_rolemodule where roleid=:roleid and moduleid=:moduleid";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":roleid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
             };
            parmss[0].Value = roleid;
            parmss[1].Value = moduleid;
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;
        }
    }
}
