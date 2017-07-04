using DZ.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.MODEL;

namespace DZ.DLL
{
   public class DZ_IMAGE_EMS_DAL
    {
     public static  string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
       public int Add( MODEL.DZ_IMAGE_EMS model)
       {
           string sql = "insert into DZ_IMAGE_EMS VALUES ()";
           return 0;
       }
       public List< MODEL.DZ_IMAGE_EMS> GetAllList()
       {
           string sql = "select * from DZ_IMAGE_EMS";
           Oracle.ManagedDataAccess.Client.OracleDataReader OraDt = OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, null);
           List<DZ_IMAGE_EMS> modelist = new List<DZ_IMAGE_EMS>();
           while (OraDt.Read())
           {
               modelist.Add(new DZ_IMAGE_EMS() { 
                   BARCODE = OraDt["BARCODE"].ToString(), 
                   BOXID =int.Parse(OraDt["BOXID"].ToString()),
                   IMAGEID = int.Parse(OraDt["IMAGEID"].ToString()),
                   BOXNO = OraDt["BOXNO"].ToString(),
                   IMAGEDATE = OraDt["IMAGEDATE"].ToString(),
                   IMAGESTATE = int.Parse(OraDt["IMAGESTATE"].ToString()),
                   IMAGETYPE = OraDt["IMAGETYPE"].ToString(),
                   FILEDTYPE = OraDt["FILEDTYPE"].ToString(),
                   CHECKUSER = OraDt["CHECKUSER"].ToString(),
                   PAGENUM =int.Parse( OraDt["PAGENUM"].ToString()),
                   OUTSTATE =int.Parse( OraDt["OUTSTATE"].ToString()),
                   FILENAME = OraDt["FILENAME"].ToString()

               });
               //Console.WriteLine("ss" + OraDt[0]);
           }
           return modelist;
       }

    }
}
