using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
  public   class DZ_Object_DAL
    {
      public static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
      public List<string[]> GetAllList()
      {
          string sql = " select * from dz_object order by objectid ";

          OracleDataReader dr = Common.OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, null);
          List<string[]> modelist = new List<string[]>();
          while (dr.Read())
          {
              string[] arrymodel = new string[5];
              arrymodel[0] = dr[0].ToString();
              arrymodel[1] = dr[1].ToString();
              arrymodel[2] = dr[2].ToString();
              arrymodel[3] = dr[3].ToString();
              arrymodel[4] = dr[4].ToString();
              modelist.Add(arrymodel);
          }

          dr.Close();
          return modelist;
      }
    }
}
