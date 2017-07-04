using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{


    public class DZ_EXCEL_EMS_DAL
    {
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public List<string[]> EXCEL_OUT(string boxno, int objectid, string objname)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select a.barcode,")
                .Append(" max(decode(b.inputname,'寄件人姓名',b.values3)) jjrxm,")
                  .Append(" max(decode(b.inputname,'寄件人电话',b.values3)) jjrdh,")
                 .Append(" '" + objname + "' jjrdz,")
                .Append(" max(decode(b.inputname,'收件人姓名',b.values3)) sjrxm,")
                 .Append(" max(decode(b.inputname,'收件人电话',b.values3)) sjrdh,")
                 .Append(" max(decode(b.inputname,'收件人地址',b.values3)) sjrdz,")
                .Append("  max(decode(b.inputname,'物品重量',b.values3)) wpzl,")
                 .Append(" max(decode(a.imagestate,0,a.guid)) guid")
                .Append("  from dz_input_ems b,dz_image_ems a,dz_user c")
                .Append("  where a.imageid=b.imageid and a.userid=c.userid ")
                 .Append(" and a.boxno=:boxno and  c.objectid=:objectid")
                .Append("  group by a.barcode ");


            //sb.Append(" ORDER BY userid DESC, username) tt ")
            //.Append(" WHERE ROWNUM <= :biger) table_alias").Append(" WHERE table_alias.rowno > :small ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":boxno",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":objectid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = boxno;
            parmss[1].Value = objectid;
            DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, parmss);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[9];
                    array[0] = item[0].ToString();
                    array[1] = item[1].ToString();
                    array[2] = item[2].ToString();
                    array[3] = item[3].ToString();
                    array[4] = item[4].ToString();
                    array[5] = item[5].ToString();
                    array[6] = item[6].ToString();
                    array[7] = item[7].ToString();
                    array[8] = item[8].ToString();

                    resultlist.Add(array);
                }

            }
            return resultlist;
        }
        public List<string[]> GetBoxNo(string thedate, int objectid)
        {
            //startdate += " 00:00:00";
            //enddate += " 23:59:59";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  a.boxno ,")
              .Append("   count(decode(b.inputstate,0,1)) noinput,")
                .Append(" count(decode(b.inputstate,88,1)) input,")
               .Append("  count(decode(b.inputstate,4,1)) checkpass,")
               .Append("  count(decode(b.inputstate,5,1)) checkerr,")
               .Append("  count(distinct a.barcode) thenum ")
               .Append("  from dz_image_ems a,dz_input_ems b,dz_user c where ")
               .Append("  a.imageid =b.imageid and  to_char(a.createtime,'yyyy-mm-dd')=:thedate")
               .Append("  and a.userid=c.userid and c.objectid=:objectid")
               .Append("  group by  a.boxno  order by a.boxno  ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":thedate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":objectid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = thedate;
            parmss[1].Value = objectid;
            DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, parmss);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[6];
                    array[0] = item[0].ToString();
                    array[1] = item[1].ToString();
                    array[2] = item[2].ToString();
                    array[3] = item[3].ToString();
                    array[4] = item[4].ToString();
                    array[5] = item[5].ToString();
                    resultlist.Add(array);
                }

            }
            return resultlist;
        }
        public List<string[]> GetBoxNo_shjj(string thedate, string userid)
        {
            //startdate += " 00:00:00";
            //enddate += " 23:59:59";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  a.boxno ,")
              .Append("   count(decode(b.inputstate,0,1)) noinput,")
                .Append(" count(decode(b.inputstate,88,1)) input,")
               .Append("  count(decode(b.inputstate,4,1)) checkpass,")
               .Append("  count(decode(b.inputstate,5,1)) checkerr,")
               .Append("  count(distinct a.barcode) thenum, ")
                .Append("  count(decode(b.values3,'0',1)) recknum,a.userid ")
                .Append("  from dz_image_shjj a,dz_input_shjj b  where")//
               .Append("  a.imageid =b.imageid and  to_char(a.createtime,'yyyy-mm-dd')=:thedate");
            if (userid != "")
            {
                sb.Append("  and a.userid='" + userid + "'");//and c.objectid=:objectid
            }

            sb.Append("  group by  a.boxno,a.userid  order by a.boxno ,a.userid ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":thedate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100)
               // new Oracle.ManagedDataAccess.Client.OracleParameter(":objectid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = thedate;
            // parmss[1].Value = objectid;
            DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, parmss);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[10];
                    array[0] = item[0].ToString();
                    array[1] = item[1].ToString();
                    array[2] = item[2].ToString();
                    array[3] = item[3].ToString();
                    array[4] = item[4].ToString();
                    array[5] = item[5].ToString();
                    array[6] = item[6].ToString();
                    array[7] = item[7].ToString();
                    resultlist.Add(array);
                }

            }
            return resultlist;
        }
        //      select  a.boxno ,  count(b.imageid) allinput, count(decode(b.inputstate,0,1)) noinput, count(decode(b.inputstate,88,1)) input,  count(decode(b.inputstate,4,1)) checkpass,  count(decode(b.inputstate,5,1)) checkerr,  count(distinct a.barcode) thenum,   count(decode(b.values3,'0',1)) recknum  
        //from dz_image_anwl a,dz_input_anwl b
        //where   a.imageid =b.imageid and  to_char(a.createtime,'yyyy-mm-dd')='2017-01-07'  group by  a.boxno  order by a.boxno 
        public List<string[]> GetBoxNo_anwl(string thedate, string userid)
        {
            //startdate += " 00:00:00";
            //enddate += " 23:59:59";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  a.boxno ,")
              .Append("   count(decode(b.inputstate,0,1)) noinput,")
                .Append(" count(decode(b.inputstate,88,1)) input,")
               .Append("  count(decode(b.inputstate,4,1)) checkpass,")
               .Append("  count(decode(b.inputstate,5,1)) checkerr,")
               .Append("  count(distinct a.barcode) thenum, ")
                .Append("  count(decode(b.values3,'0',1)) recknum, ")
                .Append("   count(b.imageid) allinput,")
                .Append(" count(decode(a.fillin,1,1)) fillinnum ,a.userid")
               .Append("  from dz_image_anwl a,dz_input_anwl b where ")//,dz_user c
               .Append("  a.imageid =b.imageid and  to_char(a.createtime,'yyyy-mm-dd')=:thedate");
            if (userid != "")
            {
                sb.Append("  and a.userid='" + userid + "'");//and c.objectid=:objectid
            }
            // .Append("  and a.userid=c.userid ")//and c.objectid=:objectid
            sb.Append("  group by  a.boxno ,a.userid order by a.boxno ,a.userid ");
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":thedate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100)
              //  new Oracle.ManagedDataAccess.Client.OracleParameter(":objectid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            };
            parmss[0].Value = thedate;
            //  parmss[1].Value = objectid;
            DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, parmss);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                int colscount = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colscount];
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    resultlist.Add(array);
                }

            }
            return resultlist;
        }
        public List<string[]> GetRecivNoByUser_anwl(string startdate,string enddate, string userid)
        {
            //startdate += " 00:00:00";
            //enddate += " 23:59:59";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select  a.userid ,")
              .Append("   count(decode(b.inputstate,0,1)) noinput,")
                .Append(" count(decode(b.inputstate,88,1)) input,")
               .Append("  count(decode(b.inputstate,4,1)) checkpass,")
               .Append("  count(decode(b.inputstate,5,1)) checkerr,")
               .Append("  count(distinct a.barcode) thenum, ")
                .Append("  count(decode(b.values3,'0',1)) recknum, ")
                .Append("   count(b.imageid) allinput,")
                .Append(" count(decode(a.fillin,1,1)) fillinnum,u.groupname ")
               .Append("  from dz_image_anwl a,dz_input_anwl b,dz_user u where ")//,dz_user c
               .Append("  a.imageid =b.imageid and a.userid=u.userid and  a.createtime>to_date('" + startdate + " 00:00:00','yyyy-mm-dd hh24:mi:ss') ")
               .Append(" and  a.createtime<to_date('" + enddate + " 23:59:59','yyyy-mm-dd hh24:mi:ss')");
            if (userid != "")
            {
                sb.Append("  and a.userid='" + userid + "'");//and c.objectid=:objectid
            }
            // .Append("  and a.userid=c.userid ")//and c.objectid=:objectid
            sb.Append("  group by  a.userid,u.groupname order by a.userid ,u.groupname");
            //Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
            //     new Oracle.ManagedDataAccess.Client.OracleParameter(":thedate",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,100)
            //  //  new Oracle.ManagedDataAccess.Client.OracleParameter(":objectid",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
            //};
            //parmss[0].Value = thedate;
            //  parmss[1].Value = objectid;
            DataSet ds = Common.OracleHelper.Query(conn, sb.ToString(), System.Data.CommandType.Text, null);
            List<string[]> resultlist = new List<string[]>();
            if (ds != null && ds.Tables.Count > 0)
            {
                int colscount = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colscount];
                    for (int i = 0; i < array.Length; i++)
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
