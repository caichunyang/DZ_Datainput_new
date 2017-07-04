using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.DLL
{
    public class MyData
    {
        //    public class MyData
        //{
        // Dim Conn As SqlConnection
        // Dim Cmd As SqlCommand
        // Dim SqlTransaction As SqlTransaction

        OracleConnection Conn;
        OracleCommand Cmd;
        OracleTransaction OraTransaction;
        bool IsTransaction = false;
        public bool OpenBase(string ConnectStr = "")
        {
            if (ConnectStr == "")
            {
                //str = "server=119.97.244.109,20001;database=EMS2015;uid=sa;pwd=saDz2015123!@<->"
                //if (Common.OraStr == "") {
                //    Common.GetConfigINIFile();
                //}
                //ConnectStr = Common.OraStr;
               // ConnectStr = "server=119.97.244.109,20001;database=EMS2015;uid=sa;pwd=saDz2015123!@<->";
                ConnectStr = "Data Source=119.97.244.109/sbcj;User ID=sbcj;PassWord=whdztech";
            }
            Conn = new OracleConnection(ConnectStr);
            try
            {
                Conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                //   MsgBox(Err.Description)
                return false;
            }
        }
        public void CloseBase()
        {
            if (Conn.State != ConnectionState.Closed)
                Conn.Close();
        }
        public OracleDataReader RunSQLDr(string SQLStr)
        {
            //如果是事务日志
            if (IsTransaction == true)
            {
                Cmd = new OracleCommand(SQLStr, Conn);
            }
            else
            {
                Cmd = new OracleCommand(SQLStr, Conn);
            }
            return Cmd.ExecuteReader();
        }
        public long RunSQL(string SQLStr)
        {
            if (IsTransaction == true)
            {
                Cmd = new OracleCommand(SQLStr, Conn);
            }
            else
            {
                Cmd = new OracleCommand(SQLStr, Conn);
            }
            return Cmd.ExecuteNonQuery();
        }
        //void BeginTransaction()
        //{
        //    OraTransaction = Conn.BeginTransaction;
        //    IsTransaction = true;
        //}
        public void Commit()
        {
            OraTransaction.Commit();
        }
        public void RollBack()
        {
            OraTransaction.Rollback();
        }
    }
}
