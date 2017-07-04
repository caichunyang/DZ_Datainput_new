using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
   public class DZ_INPUT_EMS_DAL
    {
       public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
       public List<string[]> LoadEntities()
       {
//           select userid1,'2016-10-16',inputname,count(inputname) from dz_input_ems where
//thedate1>=to_date('2016-10-16 00:00:01','yyyy-mm-dd hh24:mi:ss') and 
//thedate1<=to_date('2016-10-16 23:23:59','yyyy-mm-dd hh24:mi:ss')
//group by userid1,inputname order by userid1

           StringBuilder sb = new StringBuilder();
           sb.Append(" select userid1,to_char(thedate1,'yyyy-mm-dd'),inputname,count(inputname) from dz_input_ems")
               .Append(" where thedate1>=to_date('2016-10-15 00:00:00','yyyy-mm-dd hh24:mi:ss') and ")
               .Append(" thedate1<=to_date('2016-10-15 23:23:59','yyyy-mm-dd hh24:mi:ss')")  // and userid1='al0001'
               .Append(" group by userid1,to_char(thedate1,'yyyy-mm-dd'),inputname order by userid1,to_char(thedate1,'yyyy-mm-dd')");
           //List<string[]> resultlist = new List<string[]>();
       
           //Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
           //      new Oracle.ManagedDataAccess.Client.OracleParameter(":biger",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
           //     new Oracle.ManagedDataAccess.Client.OracleParameter(":small",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
           // };
           //parmss[0].Value = biger;
           //parmss[1].Value = small;
           List<string[]> reusultlist = new List<string[]>();
           DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, null);
           if (ds != null && ds.Tables.Count > 0)
           {
               foreach (DataRow item in ds.Tables[0].Rows)
               {
                   string[] array = new string[4];
                   array[0] = item[0].ToString();
                   array[1] = item[1].ToString();
                   array[2] = item[2].ToString();
                   array[3] = item[3].ToString();
                   reusultlist.Add(array);
               }
               return reusultlist;
           }
           else
           {
               return null;
           }
          
       }
    }
}
