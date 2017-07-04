using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace DZ.DLL
{
    public class DZ_ROLE_DAL
    {
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public List<DZ_ROLE> GetEntities()
        {
            string sql = "select * from dz_role order by roleid";

            //Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
            //     new Oracle.ManagedDataAccess.Client.OracleParameter(":username",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,10),
            // new Oracle.ManagedDataAccess.Client.OracleParameter(":password",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
            // };
            //parmss[0].Value = username;
            //parmss[1].Value = password;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, null);
            List<DZ_ROLE> modelist = new List<DZ_ROLE>();
            while (dr.Read())
            {
                var tempmodel = new DZ_ROLE();
                tempmodel.roleid =dr["roleid"].ToString();
                tempmodel.rolename = dr["rolename"].ToString();
                tempmodel.rolememo =dr["rolememo"].ToString();
                tempmodel.createtime =DateTime.Parse( dr["createtime"].ToString());
                tempmodel.disablestatus = int.Parse(dr["disablestatus"].ToString());
                modelist.Add(tempmodel);
            }

            dr.Close();
            return modelist;
            //  return Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql, parmss);
        }

      

    }
}
