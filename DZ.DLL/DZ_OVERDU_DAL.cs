using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
    public class DZ_OVERDU_DAL
    {
        //public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_pa"].ConnectionString;
        public string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        #region overdue1
         public bool insert(string keys,string datenum, string[] array)
        {
            string sql = "insert  into  dz_overdue   values(:objkey,:rec_count,:upload_t,:upload_y,:overduenock,:overdueck,:overduenock2,:overdueck2,:backcount,:createtime,:datenum,:checkednum,:errornum)";

            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":rec_count",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":upload_t",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":upload_y",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":overduenock",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":overdueck",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":overduenock2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":overdueck2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
               new Oracle.ManagedDataAccess.Client.OracleParameter(":backcount",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":createtime",Oracle.ManagedDataAccess.Client.OracleDbType.Date),
                   new Oracle.ManagedDataAccess.Client.OracleParameter(":datenum",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
                      new Oracle.ManagedDataAccess.Client.OracleParameter(":checkednum",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                   new Oracle.ManagedDataAccess.Client.OracleParameter(":errornum",Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
             };
            DateTime datenow = DateTime.Now;
           
            parmss[0].Value = keys;
            parmss[1].Value = int.Parse(array[0]);
            parmss[2].Value = int.Parse(array[1]);
            parmss[3].Value = int.Parse(array[2]);
            parmss[4].Value = int.Parse(array[3]);
            parmss[5].Value = int.Parse(array[4]);
            parmss[6].Value = int.Parse(array[5]);
            parmss[7].Value = int.Parse(array[6]);
            parmss[8].Value = int.Parse(array[7]);
            parmss[9].Value = datenow;
            parmss[10].Value = datenum;
            parmss[11].Value = int.Parse(array[8]); ;
            parmss[12].Value = int.Parse(array[9]); ;
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;

        }
        public string[] loadentity(string key, string datenum)
        {
            // string sql = "insert  into  dz_overdue   values(:objkey,:rec_count,:upload_t,:upload_y,:overduenock,:overdueck,:overduenock2,:overdueck2,:backcount,:createtime)";
            string sql = "select * from dz_overdue t where objkey=:objkey and  t.datenum=:datenum";
            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":datenum",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)
             };
            parmss[0].Value = key;
            parmss[1].Value = datenum;
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colcount = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colcount];
                    for (int i = 0; i < colcount; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    return array;
                }

            }
            return null;
        }

        public bool IsContainSameDate(string key, string date)
        {
            // string sql = "insert  into  dz_overdue   values(:objkey,:rec_count,:upload_t,:upload_y,:overduenock,:overdueck,:overduenock2,:overdueck2,:backcount,:createtime)";
            string sql = "select count(1) from dz_overdue t where objkey=:objkey and t.datenum=:datenum";
            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":datenum",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)
             };
            parmss[0].Value = key;
            parmss[1].Value = date;
           object obj= Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql, parmss);
           //int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);
           // return result > 0;
           return int.Parse(obj.ToString()) > 0;
        }
        public bool update(string key, string datenum,string[] array)
        {
            string sql = "update dz_overdue a set  a.rec_count=:rec_count,a.upload_t=:upload_t,a.upload_y=:upload_y ,a.overduenock=:overduenock,a.overdueck=:overdueck ,a.overduenock2=:overduenock2,a.overdueck2=:overdueck2,a.backcount=:backcount,a.checkednum=:checkednum,a.errornum=:errornum,a.createtime=:createtime where a.datenum=:datenum and a.objkey=:objkey";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
             //    new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2),
           
               new Oracle.ManagedDataAccess.Client.OracleParameter(":rec_count",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":upload_t",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":upload_y",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":overduenock",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,50),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":overdueck",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                    new Oracle.ManagedDataAccess.Client.OracleParameter(":overduenock2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":overdueck2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),

                      new Oracle.ManagedDataAccess.Client.OracleParameter(":backcount",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                        new Oracle.ManagedDataAccess.Client.OracleParameter(":checkednum",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                         new Oracle.ManagedDataAccess.Client.OracleParameter(":errornum",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                        new Oracle.ManagedDataAccess.Client.OracleParameter(":createtime",Oracle.ManagedDataAccess.Client.OracleDbType.Date),
                        new Oracle.ManagedDataAccess.Client.OracleParameter(":datenum",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
                         new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
                
             };

            //parmss[0].Value = model.Moduleid;
            //parmss[1].Value = model.Disablestatus;
           // parmss[0].Value = key;
           
            for (int i = 0; i < array.Length; i++)
            {
                parmss[i].Value = array[i];
            }
            parmss[array.Length].Value = DateTime.Now;
            parmss[array.Length+1].Value =datenum;
            parmss[array.Length + 2].Value = key;
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;
        }
        #endregion

        #region overdue2
        public bool insert2(string keys, string datenum, string[] array)
        {
            string sql = "insert  into  dz_overdue2 (objkey,rec_count,no_overdue1,rec_count1,no_overdue2,rec_count2,usercount,uploadcount1,uploadnum1,uploadcount2,uploadnum2,backcount,waitchekcnum,errornum,createtime,datenum,upstate)" +
                "  values(:objkey,:rec_count,:no_overdue1,:rec_count1,:no_overdue2,:rec_count2,:usercount,:uploadcount1,:uploadnum1,:uploadcount2,:uploadnum2,:backcount,:waitchekcnum,:errornum,:createtime,:datenum,:upstate)";

            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":rec_count",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":no_overdue1",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":rec_count1",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":no_overdue2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),

                 new Oracle.ManagedDataAccess.Client.OracleParameter(":rec_count2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":usercount",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
               new Oracle.ManagedDataAccess.Client.OracleParameter(":uploadcount1",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":uploadnum1",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":uploadcount2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":uploadnum2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),

               new Oracle.ManagedDataAccess.Client.OracleParameter(":backcount",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),    
               new Oracle.ManagedDataAccess.Client.OracleParameter(":waitchekcnum",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                new Oracle.ManagedDataAccess.Client.OracleParameter(":errornum",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":createtime",Oracle.ManagedDataAccess.Client.OracleDbType.Date),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":datenum",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
                  new Oracle.ManagedDataAccess.Client.OracleParameter(":upstate",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)

                   // new Oracle.ManagedDataAccess.Client.OracleParameter(":upstate",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),,:upstate
                  //new Oracle.ManagedDataAccess.Client.OracleParameter(":zipname",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
                  
             };
            DateTime datenow = DateTime.Now;

            parmss[0].Value = keys;
            parmss[1].Value = int.Parse(array[0]);
            parmss[2].Value = int.Parse(array[1]);
            parmss[3].Value = int.Parse(array[2]);
            parmss[4].Value = int.Parse(array[3]);
            parmss[5].Value = int.Parse(array[4]);
            parmss[6].Value = int.Parse(array[5]);
            parmss[7].Value = int.Parse(array[6]);
            parmss[8].Value = int.Parse(array[7]);
            parmss[9].Value = int.Parse(array[8]);//
            parmss[10].Value = int.Parse(array[9]);
            parmss[11].Value = int.Parse(array[10]);
            parmss[12].Value = int.Parse(array[11]);
            parmss[13].Value = int.Parse(array[12]); 
            parmss[14].Value = datenow;
            parmss[15].Value = datenum;
            parmss[16].Value = array[13];
            int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

            return result > 0;

        }
        public string[] loadentity2(string key, string datenum)
        {
            // string sql = "insert  into  dz_overdue   values(:objkey,:rec_count,:upload_t,:upload_y,:overduenock,:overdueck,:overduenock2,:overdueck2,:backcount,:createtime)";
            string sql = "select * from dz_overdue2 t where objkey=:objkey and t.datenum='"+datenum+"'";

            if (DateTime.Now.ToString("yyyyMMdd") == datenum)
            {
                sql = "select * from (select t.*,ROWNUM RN from dz_overdue2 t where  objkey='yd' and  t.datenum is null order by t.createtime desc) where rownum=1";
            }
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)
             };
            parmss[0].Value = key;
            DataSet ds = Common.OracleHelper.Query(conn, sql, System.Data.CommandType.Text, parmss);
            if (ds != null && ds.Tables.Count > 0)
            {
                int colcount = ds.Tables[0].Columns.Count;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string[] array = new string[colcount];
                    for (int i = 0; i < colcount; i++)
                    {
                        array[i] = item[i].ToString();
                    }
                    return array;
                }

            }
            return null;
        }

        public bool IsContainSameDate2(string key, string date)
        {
            // string sql = "insert  into  dz_overdue   values(:objkey,:rec_count,:upload_t,:upload_y,:overduenock,:overdueck,:overduenock2,:overdueck2,:backcount,:createtime)";
            string sql = "select count(1) from dz_overdue t where objkey=:objkey and t.datenum=:datenum";
            //string sql = "insert into dz_rolemodule (roleid,moduleid) values (:roleid ,:moduleid)";
            Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                 new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20),
             new Oracle.ManagedDataAccess.Client.OracleParameter(":datenum",Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2,20)
             };
            parmss[0].Value = key;
            parmss[1].Value = date;
            object obj = Common.OracleHelper.ExecuteScalar(conn, System.Data.CommandType.Text, sql, parmss);
            //int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);
            // return result > 0;
            return int.Parse(obj.ToString()) > 0;
        }
        //public bool update(string key, string datenum, string[] array)
        //{
        //    string sql = "update dz_overdue2 a set  a.rec_count=:rec_count,a.upload_t=:upload_t,a.upload_y=:upload_y ,a.overduenock=:overduenock,a.overdueck=:overdueck ,a.overduenock2=:overduenock2,a.overdueck2=:overdueck2,a.backcount=:backcount,a.checkednum=:checkednum,a.errornum=:errornum,a.createtime=:createtime where a.datenum=:datenum and a.objkey=:objkey";
        //    Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = new Oracle.ManagedDataAccess.Client.OracleParameter[]{
        //     //    new Oracle.ManagedDataAccess.Client.OracleParameter(":moduleid",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2),
           
        //       new Oracle.ManagedDataAccess.Client.OracleParameter(":rec_count",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
        //        new Oracle.ManagedDataAccess.Client.OracleParameter(":upload_t",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
        //        new Oracle.ManagedDataAccess.Client.OracleParameter(":upload_y",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
        //         new Oracle.ManagedDataAccess.Client.OracleParameter(":overduenock",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,50),
        //          new Oracle.ManagedDataAccess.Client.OracleParameter(":overdueck",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
        //            new Oracle.ManagedDataAccess.Client.OracleParameter(":overduenock2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
        //         new Oracle.ManagedDataAccess.Client.OracleParameter(":overdueck2",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),

        //              new Oracle.ManagedDataAccess.Client.OracleParameter(":backcount",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
        //                new Oracle.ManagedDataAccess.Client.OracleParameter(":checkednum",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
        //                 new Oracle.ManagedDataAccess.Client.OracleParameter(":errornum",Oracle.ManagedDataAccess.Client.OracleDbType.Int32),
        //                new Oracle.ManagedDataAccess.Client.OracleParameter(":createtime",Oracle.ManagedDataAccess.Client.OracleDbType.Date),
        //                new Oracle.ManagedDataAccess.Client.OracleParameter(":datenum",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20),
        //                 new Oracle.ManagedDataAccess.Client.OracleParameter(":objkey",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,20)
                
        //     };

        //    //parmss[0].Value = model.Moduleid;
        //    //parmss[1].Value = model.Disablestatus;
        //    // parmss[0].Value = key;

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        parmss[i].Value = array[i];
        //    }
        //    parmss[array.Length].Value = DateTime.Now;
        //    parmss[array.Length + 1].Value = datenum;
        //    parmss[array.Length + 2].Value = key;
        //    int result = Common.OracleHelper.ExecuteNonQuery(conn, System.Data.CommandType.Text, sql, parmss);

        //    return result > 0;
        //}
        #endregion
    }
}
