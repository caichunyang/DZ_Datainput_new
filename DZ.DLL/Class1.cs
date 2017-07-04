using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.Common;
using System.Configuration;

namespace DZ.DLL
{
    public class Class1
    {
        public void sdfsd()
        {
            string sql = "select * from dz_image_ems";
             //configurationmanager
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
          //  sssf = sssf + 1;
          //  string conn = "Data Source=119.97.244.109/sbcj;User ID=sbcj;PassWord=whdztech";
           Oracle.ManagedDataAccess.Client.OracleDataReader OraDt =OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, null);
           while (OraDt.Read())
           {
               Console.WriteLine("ss"+ OraDt[0]);
           }
           OraDt.Close();
        }
    }
}
