using DZ.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.MODEL;
using System.Data;

namespace DZ.DLL
{
    public class DZ_IMAGE_EMS_DAL
    {
        public static string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public int Add(MODEL.DZ_IMAGE_EMS model)
        {
            string sql = "insert into DZ_IMAGE_EMS VALUES ()";
            return 0;
        }
        public List<MODEL.DZ_IMAGE_EMS> GetAllList()
        {
            string sql = "select * from DZ_IMAGE_EMS";
            using (Oracle.ManagedDataAccess.Client.OracleDataReader OraDt = OracleHelper.ExecuteReader(conn, System.Data.CommandType.Text, sql, null))
            {
                List<DZ_IMAGE_EMS> modelist = new List<DZ_IMAGE_EMS>();
                while (OraDt.Read())
                {
                    modelist.Add(new DZ_IMAGE_EMS()
                    {
                        BARCODE = OraDt["BARCODE"].ToString(),
                        BOXID = int.Parse(OraDt["BOXID"].ToString()),
                        IMAGEID = int.Parse(OraDt["IMAGEID"].ToString()),
                        BOXNO = OraDt["BOXNO"].ToString(),
                        IMAGEDATE = OraDt["IMAGEDATE"].ToString(),
                        IMAGESTATE = int.Parse(OraDt["IMAGESTATE"].ToString()),
                        IMAGETYPE = OraDt["IMAGETYPE"].ToString(),
                        FILEDTYPE = OraDt["FILEDTYPE"].ToString(),
                        CHECKUSER = OraDt["CHECKUSER"].ToString(),
                        PAGENUM = int.Parse(OraDt["PAGENUM"].ToString()),
                        OUTSTATE = int.Parse(OraDt["OUTSTATE"].ToString()),
                        FILENAME = OraDt["FILENAME"].ToString()

                    });
                    //Console.WriteLine("ss" + OraDt[0]);
                }
                OraDt.Close();
                return modelist;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wherestr"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<string[]> SelectImage_EMS(string wherestr, int pagesize, int page, out int count)
        {
            if (string.IsNullOrEmpty(wherestr))
            {
                wherestr = "";
            }
            count = 0;
            string sqlstr = " select count(1) from dz_image_ems i, dz_input_ems t where t.imageid=i.imageid " + wherestr;
            object res = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlstr, null);
            int.TryParse(res.ToString(), out count);
            StringBuilder sb = new StringBuilder();
            //  sb.Append(" select * from dz_image_yd i ,dz_input_yd t where t.imageid=i.imageid and :where");
            //i.filename like '2b13afc04a5e4283897cffc28de42486%'

            //  SELECT * FROM ( SELECT t.imageid,t.inputname,t.values1,t.values2,t.values3,t.inputstate,t.userid1,t.userid2,t.userid3,t.thedate1,t.thedate2,t.thedate3
            // ,i.boxno,i.imagedate,i.imagestate,i.imagetype,i.guid,i.outstate,i.filedtype ,ROWNUM RN 
            //FROM   dz_image_ems i, dz_input_ems t where t.imageid=i.imageid and   ROWNUM <= 50000 order by i.imageid desc ) WHERE RN >= 49990
            sb.Append("  SELECT * FROM ( SELECT t.imageid,t.inputname,t.values1,t.values2,t.values3,t.inputstate,t.userid1,t.userid2,t.userid3,t.thedate1,t.thedate2,t.thedate3")
                .Append(" ,i.boxno,i.imagedate,i.imagestate,i.imagetype,i.guid,i.outstate,i.filedtype ,ROWNUM RN  ")
                .Append(" FROM   dz_image_ems i, dz_input_ems t where t.imageid=i.imageid  " + wherestr + "  and   ROWNUM <= :bigger order by i.imageid desc ) WHERE RN >= :smaller ");

            //sb.Append(" ORDER BY userid DESC, username) tt ")
            //.Append(" WHERE ROWNUM <= :biger) table_alias").Append(" WHERE table_alias.rowno > :small ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 //new Oracle.ManagedDataAccess.Client.OracleParameter(":wherestr",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":bigger",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":smaller",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            //parmss[0].Value = where;
            parmss[0].Value = pagesize * page;
            parmss[1].Value = pagesize * (page - 1);
            DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, parmss);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                int length = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[length];
                    for (int i = 0; i < length; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    resultlist.Add(array);
                }
            }
            return resultlist;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wherestr"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<string[]> SelectCenterImage_EMS(string wherestr, int pagesize, int page, out int count)
        {
            if (string.IsNullOrEmpty(wherestr))
            {
                wherestr = "";
            }
            count = 0;
            string sqlstr = " select count(1) from dz_image_ems i, dz_input_ems t,dz_user u where t.imageid=i.imageid and u.userid=t.userid1 " + wherestr;
            object res = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlstr, null);
            int.TryParse(res.ToString(), out count);
            StringBuilder sb = new StringBuilder();
            //  sb.Append(" select * from dz_image_yd i ,dz_input_yd t where t.imageid=i.imageid and :where");
            //i.filename like '2b13afc04a5e4283897cffc28de42486%'

            //  SELECT * FROM ( SELECT t.imageid,t.inputname,t.values1,t.values2,t.values3,t.inputstate,t.userid1,t.userid2,t.userid3,t.thedate1,t.thedate2,t.thedate3
            // ,i.boxno,i.imagedate,i.imagestate,i.imagetype,i.guid,i.outstate,i.filedtype ,ROWNUM RN 
            //FROM   dz_image_ems i, dz_input_ems t where t.imageid=i.imageid and   ROWNUM <= 50000 order by i.imageid desc ) WHERE RN >= 49990
            sb.Append("  SELECT * FROM ( SELECT t.imageid,t.inputname,t.values1,t.values2,t.values3,t.inputstate,t.userid1,t.userid2,t.userid3,t.thedate1,t.thedate2,t.thedate3")
                .Append(" ,i.boxno,i.imagedate,i.imagestate,i.imagetype,i.guid,i.outstate,i.filedtype ,ROWNUM RN  ")
                .Append(" FROM   dz_image_ems i, dz_input_ems t ,dz_user u where t.imageid=i.imageid and  u.userid=t.userid1 " + wherestr + "  and   ROWNUM <= :bigger order by i.imageid desc ) WHERE RN >= :smaller ");

            //sb.Append(" ORDER BY userid DESC, username) tt ")
            //.Append(" WHERE ROWNUM <= :biger) table_alias").Append(" WHERE table_alias.rowno > :small ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 //new Oracle.ManagedDataAccess.Client.OracleParameter(":wherestr",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":bigger",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":smaller",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            //parmss[0].Value = where;
            parmss[0].Value = pagesize * page;
            parmss[1].Value = pagesize * (page - 1);
            DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, parmss);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                int length = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[length];
                    for (int i = 0; i < length; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    resultlist.Add(array);
                }
            }
            return resultlist;

        }
    }
    
}
