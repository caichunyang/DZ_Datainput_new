using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.MODEL;

namespace DZ.DLL
{
   public class DZ_Input_pingan_Dal
    {
       public static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_pa"].ConnectionString;
       public bool Add(dz_input_pingan model)
       {
           string sql = "insert  into  dz_input_pingan (userid,image,inputype,inputlength,returntype,inputime,inputvalue)  values(:userid,:image,:inputype,:inputlength,:returntype,to_date(:inputime,'yyyy/mm/dd HH24:MI:SS'),:inputvalue)";

           //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
           Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                
             new Oracle.ManagedDataAccess.Client.OracleParameter(":userid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":image",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,40),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":inputype",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":inputlength",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":returntype",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
               new Oracle.ManagedDataAccess.Client.OracleParameter(":inputime",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,40),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":inputlvalue",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,40)
             };
           parmss[0].Value = model.userid;
           parmss[1].Value = model.image;
           parmss[2].Value = model.inputype;
           parmss[3].Value = model.inputlength;
           parmss[4].Value = model.returntype;
           parmss[5].Value = model.inputime.ToString();
           parmss[6].Value = model.inputlvalue;
           int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

           return result > 0;
       }
    }
}
