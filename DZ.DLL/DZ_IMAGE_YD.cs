using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
    public class DZ_IMAGE_YD
    {
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public List<string[]> SelectImage_YD(string wherestr, int pagesize, int page,out int count)
        {
            if (string.IsNullOrEmpty(wherestr))
            {
                wherestr = "";
            }
            count=0;
            string sqlstr=" select count(1) from dz_history_image_yd i, dz_history_input_yd t where t.imageid=i.imageid " + wherestr ;
            object res = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlstr, null);
            int.TryParse(res.ToString(),out count);
            StringBuilder sb = new StringBuilder();
            //  sb.Append(" select * from dz_image_yd i ,dz_input_yd t where t.imageid=i.imageid and :where");
            //i.filename like '2b13afc04a5e4283897cffc28de42486%'

            //                SELECT * FROM ( SELECT t.imageid,t.inputname,t.values1,t.values2,t.values3,t.inputstate,t.userid1,t.userid2,t.userid3,t.thedate1,t.thedate2,t.thedate3
            //,i.boxno,i.imagedate,i.imagestate,i.imagetype,i.filename,i.outstate, ROWNUM RN 
            //FROM   dz_image_yd i, dz_input_yd t where t.imageid=i.imageid and   ROWNUM <= 40 ) WHERE RN >= 21
            sb.Append(" SELECT * FROM ( SELECT t.imageid,t.inputname,t.values1,t.values2,t.values3,")
                .Append("  t.inputstate,t.userid1,t.userid2,t.userid3,t.thedate1,t.thedate2,t.thedate3 ")
                .Append(" ,i.boxno,i.imagedate,i.imagestate,i.imagetype,i.filename,i.outstate, ROWNUM RN  ")
                .Append(" FROM   dz_history_image_yd i, dz_history_input_yd t where t.imageid=i.imageid " + wherestr + " and   ROWNUM <= :bigger ) WHERE RN > :smaller ");

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
        public List<string[]> SelectCenterImage_YD(string wherestr, int pagesize, int page, out int count)
        {
            if (string.IsNullOrEmpty(wherestr))
            {
                wherestr = "";
            }
            count = 0;
            string sqlstr = " select count(1) from dz_history_image_yd i, dz_history_input_yd t,dz_user u  where t.imageid=i.imageid and t.userid1=u.userid " + wherestr;
            object res = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sqlstr, null);
            int.TryParse(res.ToString(), out count);
            StringBuilder sb = new StringBuilder();
            //  sb.Append(" select * from dz_image_yd i ,dz_input_yd t where t.imageid=i.imageid and :where");
            //i.filename like '2b13afc04a5e4283897cffc28de42486%'

            //                SELECT * FROM ( SELECT t.imageid,t.inputname,t.values1,t.values2,t.values3,t.inputstate,t.userid1,t.userid2,t.userid3,t.thedate1,t.thedate2,t.thedate3
            //,i.boxno,i.imagedate,i.imagestate,i.imagetype,i.filename,i.outstate, ROWNUM RN 
            //FROM   dz_image_yd i, dz_input_yd t where t.imageid=i.imageid and   ROWNUM <= 40 ) WHERE RN >= 21
            sb.Append(" SELECT * FROM ( SELECT t.imageid,t.inputname,t.values1,t.values2,t.values3,")
                .Append("  t.inputstate,t.userid1,t.userid2,t.userid3,t.thedate1,t.thedate2,t.thedate3 ")
                .Append(" ,i.boxno,i.imagedate,i.imagestate,i.imagetype,i.filename,i.outstate, ROWNUM RN  ")
                .Append(" FROM   dz_history_image_yd i, dz_history_input_yd t,dz_user u  where t.imageid=i.imageid  and t.userid1=u.userid " + wherestr + " and   ROWNUM <= :bigger ) WHERE RN > :smaller ");

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
