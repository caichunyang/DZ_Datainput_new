using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
    public class DZ_Dirctory_DAL
    {
        public static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public static string conn_pa = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_pa"].ConnectionString;
        public List<string[]> GetEntities(string rowlistid)
        {
            string sql = "select * from dz_directory t where  t.rowlist=:rowlist";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":rowlist",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,10)
            };
            parmss[0].Value = rowlistid;
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, parmss);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                int coums = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[coums];
                    for (int i = 0; i < coums; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    resultlist.Add(array);
                }
            }
            return resultlist;
        }
        public List<string[]> GetDictionaryEntities(string objname,string name,string key)
        {
            string sql = "select * from dz_dictionary t where  t.objname=:objname and t.name=:name and t.key=:key";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":objname",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":name",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":key",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
            };
            parmss[0].Value = objname;
            parmss[1].Value = name;
            parmss[2].Value = key;
            DataSet ds = Common.OracleHelper.Query(conn_pa, sql, System.Data.CommandType.Text, parmss);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                int coums = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[coums];
                    for (int i = 0; i < coums; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    resultlist.Add(array);
                }
                return resultlist;
            }
            return null;
        }

    }
}
