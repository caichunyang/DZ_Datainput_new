using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.MODEL;
using Oracle.ManagedDataAccess.Client;


namespace DZ.DLL
{
    public class DZ_MODULE_DAL
    {
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public List<DZ_MODULE> GetEntites()
        {
            string sql = "select * from dz_module order by Moduleid";

            //Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
            //     new Oracle.ManagedDataAccess.Client.OracleParameter(":username",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,10),
            // new Oracle.ManagedDataAccess.Client.OracleParameter(":password",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
            // };
            //parmss[0].Value = username;
            //parmss[1].Value = password;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, null);
            List<DZ_MODULE> modelist = new List<DZ_MODULE>();
            while (dr.Read())
            {
                var tempmodel = new DZ_MODULE();
                tempmodel.Moduleid = int.Parse(dr["Moduleid"].ToString());
                tempmodel.Parentid = int.Parse(dr["Parentid"].ToString());
                tempmodel.Modulename = dr["Modulename"].ToString();
                tempmodel.Disablestatus = int.Parse(dr["Disablestatus"].ToString());
                tempmodel.Moduleaction = dr["Moduleaction"].ToString();
                tempmodel.Controler = dr["Controler"].ToString();
                tempmodel.Action = dr["Action"].ToString();
                tempmodel.Title = dr["Title"].ToString();
                tempmodel.Isview = dr["Isview"].ToString() == "" ? 0 : int.Parse(dr["Isview"].ToString());
                modelist.Add(tempmodel);
            }

            dr.Close();
            return modelist;
            //  return Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql, parmss);
        }
        public bool Add(DZ_MODULE modle)
        {
            string sql = "insert  into  dz_module (moduleid,parentid,moduleaction,modulename,disablestatus,controler,action,isview,title)  values(:moduleid,:parentid,:moduleaction,:modulename,:disablestatus,:controler,:action,:isview,:title)";

            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":parentid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleaction",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,50),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":modulename",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,50),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":disablestatus",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":controler",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":action",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,50),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":isview",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
               new Oracle.ManagedDataAccess.Client.OracleParameter(":title",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,50)
             };
            parmss[0].Value = modle.Moduleid;
            parmss[1].Value = modle.Parentid;
                  parmss[2].Value = modle.Moduleaction;
              parmss[3].Value = modle.Modulename;
            parmss[4].Value = modle.Disablestatus;
              parmss[5].Value = modle.Controler;
            parmss[6].Value = modle.Action;
            parmss[7].Value = modle.Isview;
            parmss[8].Value = modle.Title;
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;
            //  return Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql, parmss);
        }
        public List< DZ_MODULE> LoadEntites(string where)
        {
            string sql = "select * from dz_module";
            if (!string.IsNullOrEmpty(where))
            {
                sql += " where " + where;
            }
            sql += " order by Moduleid";
            //Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
            //     new Oracle.ManagedDataAccess.Client.OracleParameter(":username",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,10),
            // new Oracle.ManagedDataAccess.Client.OracleParameter(":password",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
            // };
            //parmss[0].Value = username;
            //parmss[1].Value = password;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, null);
            List<DZ_MODULE> modelist = new List<DZ_MODULE>();
            while (dr.Read())
            {
                var tempmodel = new DZ_MODULE();
                tempmodel.Moduleid = int.Parse(dr["Moduleid"].ToString());
                tempmodel.Parentid = int.Parse(dr["Parentid"].ToString());
                tempmodel.Modulename = dr["Modulename"].ToString();
                tempmodel.Disablestatus = int.Parse(dr["Disablestatus"].ToString());
                tempmodel.Moduleaction = dr["Moduleaction"].ToString();
                tempmodel.Controler = dr["Controler"].ToString();
                tempmodel.Action = dr["Action"].ToString();
                tempmodel.Title = dr["Title"].ToString();
                tempmodel.Isview = dr["Isview"].ToString() == "" ? 0 : int.Parse(dr["Isview"].ToString());
                modelist.Add(tempmodel);
            }

            dr.Close();
            return modelist;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(DZ_MODULE model)
        {// a.disablestatus=to_number(:disablestatus),
            string sql = "update dz_module a set  a.moduleaction=:moduleaction,a.modulename=:modulename,a.controler=:controler,a.action=:action ,a.title=:title,a.isview=:isview ,a.disablestatus=:disablestatus where a.moduleid=:moduleid";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
             //    new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2),
           
              new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleaction",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,50),
               new Oracle.ManagedDataAccess.Client.OracleParameter(":modulename",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,50),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":controler",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":action",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,50),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":title",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,50),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":isview",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                    new Oracle.ManagedDataAccess.Client.OracleParameter(":disablestatus",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
                 
                 
             };
            
            //parmss[0].Value = model.Moduleid;
            //parmss[1].Value = model.Disablestatus;
            parmss[0].Value = model.Moduleaction;
            parmss[1].Value = model.Modulename;
            parmss[2].Value = model.Controler;
            parmss[3].Value = model.Action;
            parmss[4].Value = model.Title;
            parmss[5].Value = model.Isview;
            parmss[6].Value = model.Disablestatus; 
            parmss[7].Value = model.Moduleid;
           
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result>0;
        }
        public List<DZ_MODULE> GetRoleRightByRoleid(int roleid)
        {
            string sql = "select * from dz_module t join dz_rolemodule b on t.moduleid=b.moduleid and b.roleid=:roleid";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                new Oracle.ManagedDataAccess.Client.OracleParameter(":controler",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
             };

            parmss[0].Value = roleid;
            OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, parmss);
            List<DZ_MODULE> modelist = new List<DZ_MODULE>();
            while (dr.Read())
            {
                var tempmodel = new DZ_MODULE();
                tempmodel.Moduleid = int.Parse(dr["Moduleid"].ToString());
                tempmodel.Parentid = int.Parse(dr["Parentid"].ToString());
                tempmodel.Modulename = dr["Modulename"].ToString();
                tempmodel.Disablestatus = int.Parse(dr["Disablestatus"].ToString());
                tempmodel.Moduleaction = dr["Moduleaction"].ToString();
                tempmodel.Controler = dr["Controler"].ToString();
                tempmodel.Action = dr["Action"].ToString();
                tempmodel.Title = dr["Title"].ToString();
                tempmodel.Isview =dr["Isview"].ToString()==""?0: int.Parse(dr["Isview"].ToString());
                modelist.Add(tempmodel);
            }

            dr.Close();
            return modelist;
        }

    }
}
