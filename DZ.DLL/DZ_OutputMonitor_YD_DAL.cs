using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.MODEL;
using DZ.Common;

namespace DZ.DLL
{
  public  class DZ_OutputMonitor_YD_DAL
    {
      //public List<DZ_OutputMonitor_YD> ShowOutPut(string StartDate, string EndDate)
      //{
      //    MyData MyData = new MyData();
          
      //    if (MyData.OpenBase() == true)
      //    {
      //        StringBuilder sql=new StringBuilder ();
      //        sql.Append ("select a.boxno")
      //            .Append(" , count(decode(b.inputstate,0,1)) noinput,")
      //            .Append("  count(decode(b.inputstate,3,1)) input,")
      //            .Append("count(decode(b.inputstate,4,1)) checkpass,")
      //            .Append("count(decode(b.inputstate,5,1)) checkerr")
      //           // .Append(",count(distinct a.barcode) thenum")
      //            .Append(" from dz_image_yd a,dz_input_yd b where  a.imageid =b.imageid and to_char(a.createtime,'dd')=to_char(sysdate-1,'dd') and  a.createtime>=to_date('" + StartDate + "','YYYY-MM-DD') and  a.createtime<=to_date('" + EndDate + "','YYYY-MM-DD')")
      //            .Append(" group by a.boxno  order by a.boxno ");
                
                
      //     //   Oracle.ManagedDataAccess.Client.OracleDataReader OraDt = MyData.RunSQLDr("select userid,thedate,thenum,errnum,checknum,checkerrnum from dz_output_ems where thedate>='" + StartDate + "' and thedate<='" + EndDate + "' and userid='" + UserID + "' order by thedate");
      //           Oracle.ManagedDataAccess.Client.OracleDataReader OraDt = MyData.RunSQLDr(sql.ToString());
              
      //        List<DZ_OutputMonitor_YD> ListOutPut = new List<DZ_OutputMonitor_YD>();
      //        DZ_OutputMonitor_YD DZ_OutputMonitor_YD;
      //        while (OraDt.Read())
      //        {
      //            DZ_OutputMonitor_YD = new DZ_OutputMonitor_YD();
      //            DZ_OutputMonitor_YD.BoxNo= OraDt.GetString(0);
      //           // DZ_OutputMonitor_YD.TheDate = OraDt.GetString(1);
      //            DZ_OutputMonitor_YD.Noinput = OraDt.GetInt32(1);
      //            DZ_OutputMonitor_YD.Input = OraDt.GetInt32(2);
      //            DZ_OutputMonitor_YD.checkpass = OraDt.GetInt32(3);
      //            DZ_OutputMonitor_YD.checkerr = OraDt.GetInt32(4);
      //            DZ_OutputMonitor_YD.TheNum = OraDt.GetInt32(2);
      //            ListOutPut.Add(DZ_OutputMonitor_YD);
      //        }
      //        OraDt.Dispose();
      //        MyData.CloseBase();

      //        return ListOutPut;

      //        //Dim Json As JsonSerializerSettings = New JsonSerializerSettings
      //        //Json.NullValueHandling = NullValueHandling.Ignore  '忽略 为Null的值
      //        //'Formatting.Indented 良好的显示格式,带换行和空格
      //        //Return (Newtonsoft.Json.JsonConvert.SerializeObject(ListOutPut, Formatting.None, Json))

             
      //    }
      //    else
      //    {
      //        return null;
      //    }
      //}
    }
}
