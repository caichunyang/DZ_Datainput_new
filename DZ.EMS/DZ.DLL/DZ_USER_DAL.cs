using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using DZ.MODEL;

namespace DZ.DLL
{
    public class DZ_USER_DAL
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
        public DZ_USER LoginObj(string username, string password)
        {
            string sql = "select * from dz_user where userid=:username and userpwd=:password";

            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                new Oracle.ManagedDataAccess.Client.OracleParameter(":username",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,10),
            new Oracle.ManagedDataAccess.Client.OracleParameter(":password",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
            };
            parmss[0].Value = username;
            parmss[1].Value = password;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, parmss);
            List<DZ_USER> modelist = new List<DZ_USER>();
            while (dr.Read())
            {
                var tempmodel = new DZ_USER();
                tempmodel.Userid = dr["Userid"].ToString();
                tempmodel.Userpwd = dr["Userpwd"].ToString();
                tempmodel.Username = dr["Username"].ToString();
                tempmodel.state = int.Parse(dr["state"].ToString());
                tempmodel.Groupname = dr["Groupname"].ToString();
                tempmodel.Objectid = int.Parse(dr["Objectid"].ToString());
                tempmodel.Userlevel = int.Parse(dr["Userlevel"].ToString());
                tempmodel.Integral = int.Parse(dr["Integral"].ToString());
                tempmodel.EXP = int.Parse(dr["EXP"].ToString());
                tempmodel.TEL = dr["TEL"].ToString();
                tempmodel.ZFB = dr["ZFB"].ToString();
                tempmodel.ZQL = dr["ZQL"].ToString();
                tempmodel.Roleid = int.Parse(dr["roleid"].ToString());
                modelist.Add(tempmodel);
            }

            dr.Close();
            return modelist.FirstOrDefault();
            //  return Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql, parmss);
        }
        public List<DZ_MODULE> GetMuduleByUserId(string userid)
        {
          //  string sql = "select * from dz_userrole a join dz_rolemodule b on a.userid=@userid and a.roleid=b.roleid";
            string sql2 = "select * from dz_module t where t.moduleid in( select m.moduleid from dz_rolemodule m where m.roleid in (select r.roleid from dz_user u join dz_userrole r on u.userid=r.userid and u.userid=:userid))";

            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,10)
            };
            parmss[0].Value = userid;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql2, parmss);
            List<DZ_MODULE> modelist = new List<DZ_MODULE>();
            while (dr.Read())
            {
                DZ_MODULE model = new DZ_MODULE();
                model.Moduleaction = dr["Moduleaction"].ToString();
                model.Parentid = int.Parse(dr["Parentid"].ToString());
                model.Modulename = dr["Modulename"].ToString();
                model.Disablestatus =  int.Parse(dr["Disablestatus"].ToString());
                model.Controler = dr["Controler"].ToString();
                model.Action = dr["Action"].ToString();
                model.Moduleid=int.Parse(dr["moduleid"].ToString());
                model.Isview = dr["isview"].ToString() == "" ? 0 :int.Parse( dr["isview"].ToString());
                model.Title = dr["title"].ToString();
                modelist.Add(model);
            }

            dr.Close();
            return modelist;
            //  return Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql, parmss);
        }
        
    }
}
