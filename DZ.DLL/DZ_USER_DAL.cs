using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using DZ.MODEL;
using System.Data;

namespace DZ.DLL
{
    public class DZ_USER_DAL
    {
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public bool CkeckUser(string username, string password)
        {
            string sql = "select * from dz_user where userid='" + username + "' and userpwd='" + password + "'";
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
        public DZ_USER LoginObj(string username, string password)
        {
            string sql = "select * from dz_user where userid=:username and userpwd=:password";

            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                new Oracle.ManagedDataAccess.Client.OracleParameter(":username",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
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
                new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
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
                model.Disablestatus = int.Parse(dr["Disablestatus"].ToString());
                model.Controler = dr["Controler"].ToString();
                model.Action = dr["Action"].ToString();
                model.Moduleid = int.Parse(dr["moduleid"].ToString());
                model.Isview = dr["isview"].ToString() == "" ? 0 : int.Parse(dr["isview"].ToString());
                model.Title = dr["title"].ToString();
                modelist.Add(model);
            }

            dr.Close();
            return modelist;
            //  return Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql, parmss);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="oldpwd"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChangePwd(string userid, string oldpwd, string pwd)
        {
            string sql = "update dz_user t set t.userpwd=:pwd where t.userid=:userid and t.userpwd=:oldpwd";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":pwd",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":oldpwd",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
            };
            parmss[0].Value = pwd;
            parmss[1].Value = userid;
            parmss[2].Value = oldpwd;
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="oldpwd"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(DZ_USER model)
        {
            string sql = "update dz_user t set t.userpwd=:pwd,t.username=:username,t.groupname=:groupname ,t.state=:state where t.userid=:userid ";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":pwd",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":username",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":groupname",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,50),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":state",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                   new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
            };
            parmss[0].Value = model.Userpwd;
            parmss[1].Value = model.Username;
            parmss[2].Value = model.Groupname;
            parmss[3].Value = model.state;
            parmss[4].Value = model.Userid;
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;
        }
        public List<DZ_USER> LoadEntities(string objid, string userid, int pagesize, int page,string where)
        {
            //         string sql=" SELECT * FROM (SELECT tt.*, ROWNUM AS rowno FROM (SELECT t.* FROM dz_user t  where t.objectid=2 and t.userid like '%1%' ORDER BY userid DESC, username) tt
            //  WHERE ROWNUM <= 20) table_alias
            //WHERE table_alias.rowno >= 10;
            //"
            List<DZ_USER> resultlist = new List<DZ_USER>();
            int biger = page * pagesize;
            int small = (page - 1) * pagesize;
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * FROM (SELECT tt.*, ROWNUM AS rowno FROM (SELECT t.* FROM dz_user t ")
                .Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(userid))
            {
                sb.Append(" and t.userid like '%" + userid + "%'");
            } if (!string.IsNullOrEmpty(objid))
            {
                sb.Append(" and t.objectid=" + objid);
            }
            if (!string.IsNullOrEmpty(where))
            {
                sb.Append(" and "+where);
              //  sb.Append(" and t.userid not in (select userid from dz_userrole where roleid=1)");
            }
           
            sb.Append(" ORDER BY userid DESC, username) tt ")
            .Append(" WHERE ROWNUM <= :biger) table_alias").Append(" WHERE table_alias.rowno > :small ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":biger",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":small",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = biger;
            parmss[1].Value = small;
            DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    DZ_USER user = new DZ_USER();
                    user.Userid = item["userid"].ToString();
                    user.Userpwd = item["userpwd"].ToString();
                    user.Username = item["username"].ToString();
                    int a = 0, b = -1, c = 0, d = 0, e = 0, f = 0; ;
                    int.TryParse(item["state"].ToString(), out a);
                    user.state = a;
                    user.Groupname = item["groupname"].ToString();

                    int.TryParse(item["objectid"].ToString(), out b);
                    user.Objectid = b;
                    int.TryParse(item["userlevel"].ToString(), out c);
                    user.Userlevel = c;
                    int.TryParse(item["integral"].ToString(), out d);
                    user.Integral = d;
                    int.TryParse(item["exp"].ToString(), out e);
                    user.EXP = e;
                    user.TEL = item["tel"].ToString();
                    user.ZFB = item["zfb"].ToString();
                    user.ZQL = item["zql"].ToString();
                    int.TryParse(item["roleid"].ToString(), out f);
                    user.Roleid = f;
                    resultlist.Add(user);
                }

            }
            return resultlist;
        }
        public int selectpagecount(string objid, string userid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) from dz_user t where 1=1");
            if (!string.IsNullOrEmpty(userid))
            {
                sql.Append(" and t.userid like '%" + userid + "%'");
            } if (!string.IsNullOrEmpty(objid))
            {
                sql.Append(" and t.objectid=" + objid);
            }

            object obj = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql.ToString(), null);
            if (obj != null)
            {
              return int.Parse(obj.ToString());

            }
            return 0;
        }

        public string GetModulnameliststr(string userid)
        {
            string sql = "select distinct moduleaction from dz_userrole a,dz_role b,dz_rolemodule c,dz_module d where a.roleid=b.roleid and b.roleid=c.roleid and c.moduleid=d.moduleid and a.userid='" + userid + "'";
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text);
            string result = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    result += item[0] + "|";
                }
                
            }
            return result;
        }
    }
}
