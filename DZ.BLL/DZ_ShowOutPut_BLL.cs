using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;


namespace DZ.BLL
{
    public class DZ_ShowOutPut_BLL
    {
       private  DZ_ShowOutPut_DAL dal = new DZ_ShowOutPut_DAL();
        public  DZ_ShowOutPut_BLL()
        {
            if (dal==null)
            {
                dal = new DZ_ShowOutPut_DAL();
            }
        }
        /// <summary>
        /// 各用户产量统计
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> UserOutPutTj(string startdate, string enddate)
        {

            return dal.UserOutPutTj(startdate, enddate);
        }
        /// <summary>
        /// 各中心统计
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> CenterOutPutTJ(string startdate, string enddate)
        {
            return dal.CenterOutPutTJ(startdate, enddate);
        }
        public List<string[]>[] Image_rc_upTJ(string startdate, string enddate, string casekey)
        {
            return dal.Image_rc_upTJ(startdate, enddate, casekey);
        }
        //分时段统计接收上传量
        public string[] Image_rc_upTJ2(string startdate, string enddate)
        {
            return dal.Image_rc_upTJ2(startdate, enddate);
        }
        /// <summary>
        /// 待录及 待上传量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public string[] Image_dailu_daichuan(string startdate, string enddate)
        {
            string[] array = dal.Image_dailu_daichuan(startdate, enddate);
            string overdue = dal.SelectOverdueZhonghe_history_yd(startdate, enddate).ToString();
            List<string> templist = array.ToList();
            templist.Add(overdue);
            return templist.ToArray();
        }
        /// <summary>
        /// 韵达录入在线人数 及待录单量
        /// </summary>
        /// <returns></returns>
        public string[] SelectOnlinePersonAndInput(string tab_key,int m)
        {
            return dal.SelectOnlinePersonAndInput(tab_key,m);
        }
        /// <summary>
        /// 韵达录入在线人数
        /// </summary>
        /// <returns></returns>
        public List<string> GetInputUserCount_yd()
        {
            return dal.GetInputUserCount();
        }
        public string[] InputOverdueTJ_YD(string startdate)//string enddate, 
        {
            return dal.InputOverdueTJ_YD(startdate);
        }
        public string[] InputOverdueTJ_YD2(string startdate)//string enddate, 
        {
            return dal.InputOverdueTJ_YD2(startdate);
        }
        public string[] InputOverdueTJ_EMS(string startdate)//string enddate, 
        {
            return dal.InputOverdueTJ_EMS(startdate);
        }
        public List<List<string>> SelectOutPut(string startdate, string enddate, string casekey, string where, string orderstr)
        {
            return dal.SelectOutPut(startdate, enddate, casekey, where, orderstr);
        }

        public List<List<string>> SelectOutPut_back(string startdate, string enddate, string casekey, string where)
        {
            return dal.SelectOutPut_back(startdate, enddate, casekey, where);
        }
        /// <summary>
        /// 邮政 按类别(filetype) 各中心 统计
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> center_recv_ems(string startdate, string enddate)
        {
            return dal.center_recv_ems(startdate, enddate);
        }

        public List<string[]> OutPutAnalysis_emsTJ(string startdate, string enddate, string userid)
        {
            return dal.OutPutAnalysis_emsTJ(startdate, enddate, userid);
        }
        /// <summary>
        /// 员工韵达产量分析
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> OutPutAnalysis_ydTJ(string startdate, string enddate, string userid)
        {
            return dal.OutPutAnalysis_ydTJ(startdate, enddate, userid);
        }
        /// <summary>
        /// 各中心产量 韵达
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> CenterProduction_yd(string startdate, string enddate)
        {
            return dal.CenterProduction_yd(startdate, enddate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startdate">yyyy-mm-dd</param>
        /// <returns></returns>
        public List<string[]> SelectInputAndOverdue_yd(string startdate)
        {
            List<string[]> list = dal.SelectInputAndOverdue_yd(startdate, startdate);
            if (startdate == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                return list;
            }
            else if (!IsHadSameInPutAndOverdue("yd", startdate))
            {
                InsertInPutAndOverdue("yd", list);
            }

            return list;
        }
        public List<string[]> SelectZhongHe_history_yd(string startdate, string enddate)
        {
            return dal.SelectZhongHe_history_yd(startdate, enddate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<string[]> SelectZhongHe_historyDeal_yd(string datestr, List<string[]> list)
        {
            list = list.OrderBy(a => a[6]).ToList();
            int start_h = int.Parse(list[0][6].Split(' ')[2]);
            int end_h = int.Parse(list[list.Count - 1][6].Split(' ')[2]);
            List<string> list_not_had = new List<string>();
            for (int i = start_h + 1; i < end_h; i++)
            {
                bool swich = false;
                foreach (var item in list)
                {
                    if (int.Parse(item[6].Split(' ')[2]) == i)
                    {
                        swich = true;
                        break;
                    }
                }
                if (!swich)
                {
                    list_not_had.Add(datestr + " " + i.ToString());
                    //  list.Add(new string[] {"0","0","0","0","0","0",datestr+" "+i.ToString()});
                }
            }
            foreach (string item in list_not_had)
            {
                string[] newstr = item.Split(' ');
                if (newstr[1].Length == 1)
                {
                    newstr[1] = "0" + newstr[1];
                }
                list.Add(new string[] { "0", "0", "0", "0", "0", "0", string.Join("  ", newstr) });
            }
            return list.OrderBy(a => a[6]).ToList();
        }
        public int IsHadSameZhongHe_history_yd(string projectname, string datestr)
        {
            return dal.IsHadSameZhongHe_history_yd(projectname, datestr);
        }
        public int SelectOverdueZhonghe_history_yd(string startdate, string enddate)
        {
            return dal.SelectOverdueZhonghe_history_yd(startdate, enddate);
        }
        /// <summary>
        /// 统计每小时的（未逾期量 逾期量 总上传量 退单量 修改量 
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> SelectZhongHe_2table_yd(string startdate, string enddate)
        {

            if (DateTime.Parse(startdate).CompareTo(DateTime.Parse(enddate)) <= 0)
            {
                List<string[]> historylist = dal.SelectHourse_record("yd", startdate, enddate);
                if (DateTime.Now.CompareTo(DateTime.Parse(enddate + " 00:00:00")) > 0 && DateTime.Now.ToString("yyyy-MM-dd") != enddate)
                {
                    //查日 统计表
                }
                else
                {
                    string datastrno = DateTime.Now.ToString("yyyy-MM-dd");
                    List<string[]> list_histroy = dal.SelectZhongHe_history_yd(datastrno, datastrno);
                    List<string[]> list_today = dal.SelectZhongHe_yd(datastrno, datastrno);
                    foreach (string[] item in list_histroy)
                    {
                        foreach (string[] item2 in list_today)
                        {
                            if (item[6] == item2[6])
                            {
                                for (int i = 0; i < item.Length; i++)
                                {
                                    if (i >= 6)
                                    {
                                        break;
                                    }
                                    item[i] = (int.Parse(item[i]) + int.Parse(item2[i])).ToString();
                                }
                            }
                        }
                    }
                    //整理
                    List<string[]> new_list_histroy = new List<string[]>();
                    foreach (var item in list_histroy)
                    {
                        string[] array = new string[10];
                        var datestr = item[6].Split(' ')[0];
                        var hours = item[6].Split(' ')[2];
                        array[0] = "yd";
                        array[1] = item[0];
                        array[2] = item[1];
                        array[3] = item[2];
                        array[4] = item[2];
                        array[5] = item[3];
                        array[6] = item[5];
                        array[7] = datestr;
                        array[8] = hours;
                        array[9] = item[4];
                        new_list_histroy.Add(array);
                    }
                    historylist.AddRange(new_list_histroy);
                }

                return historylist;
            }
            return null;
        }
        public List<string[]> SelectHourse_record(string projectname, string startdate, string enddate)
        {
            return dal.SelectHourse_record(projectname, startdate, enddate);
        }
        public int InsertHourse_record(string projectname, List<string[]> list)
        {
            return dal.InsertHourse_record(projectname, list);
        }
        /// <summary>
        /// 统计每小时的（未逾期量 逾期量 总上传量 退单量 修改量 
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> SelectZhongHe_yd(string startdate, string enddate)
        {
            List<string[]> list = dal.SelectZhongHe_yd(startdate, startdate);

            return list;
        }
        public List<string[]> SelectOverdue(string projectname, string startdate, string enddate)
        {
            List<string[]> todaylist = new List<string[]>();
            List<string[]> resultlist = new List<string[]>();
            if (DateTime.Compare(DateTime.Now, DateTime.Parse(enddate + " 23:59:59")) <= 0)
            {
                todaylist = SelectInputAndOverdue_yd(enddate).Select(a => (new[] { "yd", a[0], a[1], a[2].Split(' ')[0], a[2].Split(' ')[2] })).ToList();
                resultlist.AddRange(dal.SelectOverdue(projectname, startdate, enddate));
                resultlist.AddRange(todaylist);
                return resultlist;
            }
            else
            {
                return dal.SelectOverdue(projectname, startdate, enddate);
            }

        }
        public int InsertInPutAndOverdue(string projectname, List<string[]> list)
        {
            return dal.InsertInPutAndOverdue(projectname, list);
        }
        public bool IsHadSameInPutAndOverdue(string projectname, string datestr)
        {
            return dal.IsHadSameInPutAndOverdue(projectname, datestr) > 0;
        }
        /// <summary>
        /// 各中心退单产量 韵达 A组
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="objid">所有中心* 本中心objectid 或传userid</param>
        /// <returns></returns>
        public List<string[]> CenterPro_back_yd(string startdate, string enddate, string objid)
        {
            return dal.CenterPro_back_yd(startdate, enddate, objid);
        }
        /// <summary>
        /// 查询韵达中心产量 及中心录单速率
        /// </summary>
        /// <param name="objid"></param>
        /// <param name="time_m"></param>
        /// <returns></returns>
        public string[] AllCen_YD_TJ(int objid, int time_m)
        {
            return dal.AllCen_YD_TJ(objid, time_m);
        }
        /// <summary>
        /// 各中心产量 邮政
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> CenterProduction_ems(string startdate, string enddate, string objectid)
        {
            return dal.CenterProduction_ems(startdate, enddate, objectid);
        }

        /// <summary>
        /// 本中心产量 韵达
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> MyCenterProduction_yd(string startdate, string enddate, string objectid)
        {
            return dal.MyCenterProduction_yd(startdate, enddate, objectid);
        }
        public List<string[]> SelectCenterProduction_yd(string startdate, string enddate, string objectid, string userid)
        {
            return dal.SelectCenterProduction_yd(startdate, enddate, objectid, userid);
        }
        /// <summary>
        /// 本中心产量 邮政
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> MyCenterProduction_ems(string startdate, string enddate, string objectid)
        {

            return dal.MyCenterProduction_ems(startdate, enddate, objectid);
        }
        /// <summary>
        /// 各中心邮政统计单量 barcode
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="objectid"></param>
        /// <returns></returns>
        public List<string[]> AllCenterBarcodeNum_ems(string startdate, string enddate, string objectid)
        {
            return dal.AllCenterBarcodeNum_ems(startdate, enddate, objectid);
        }
        public bool updateoutputems15_16(int thenum, string userid, string datestr, string filename)
        {
            return dal.updateoutputems15_16(thenum, userid, datestr, filename);
        }
        // public List<string[]> SelectPAvalueLength()
        //{
        //    return dal.SelectPAvalueLength();
        //}
        // public bool upsdf(string[] array)
        // {
        //     return dal.updatePAvalueLength(array);
        // }
        ///// <summary>
        ///// 平安产量查询
        ///// </summary>
        ///// <param name="wherestr"></param>
        ///// <param name="pagesize"></param>
        ///// <param name="page"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public List<string[]> LoadPageEntities_PA(string wherestr, int pagesize, int page, out int count)
        //{
        //    return dal.LoadPageEntities_PA(wherestr, pagesize, page, out count);
        //}
        //public List<string[]> LoadPageEntitiesAllCen_PA(string wherestr, int pagesize, int page, out int count)//wherestr 均未使用参数化查询
        //{
        //    return dal.LoadPageEntitiesAllCen_PA(wherestr, pagesize, page, out count);
        //}
        ///// <summary>
        ///// 查询平安 分中心 最近time_m分钟 录入的人数 和提交速度
        ///// </summary>
        ///// <param name="objid"></param>
        ///// <param name="time_m"></param>
        ///// <returns></returns>
        //public string[] AllCen_PA_TJ(int objid, int time_m)
        //{
        //    return dal.AllCen_PA_TJ(objid,time_m);
        //}

        /// <summary>
        /// 每月韵达 接收上传 退单统计 按 天分
        /// </summary>
        public string[] EveryMonthYD_TJ(string zipname_s, string zipname_b)
        {
            return dal.EveryMonthYD_TJ(zipname_s, zipname_b);
        }
        public void updatePA_newlength(string start, string end, double nums)
        {
            dal.updatePA_newlength(start, end, nums);

        }
        public void repireOutput()
        {
            dal.repireOutput();
        }
        /// <summary>
        /// 上海捷记各中心产量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> AllCenterOutput_shjj(string startdate, string enddate)
        {
            return dal.AllCenterOutput_shjj(startdate, enddate);
        }
        /// <summary>
        /// 上海捷记各中心上传单量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="objectid"></param>
        /// <returns></returns>
        public List<string[]> AllCenterBarcodeNum_shjj(string startdate, string enddate, string objectid)
        {
            return dal.AllCenterBarcodeNum_shjj(startdate, enddate, objectid);
        }
        public List<string[]> MyCenterProduction_shjj(string startdate, string enddate, string objectid)
        {
            return dal.MyCenterProduction_shjj(startdate, enddate, objectid);
        }
        #region 安能

        /// <summary>
        /// 安能物流各中心产量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<string[]> AllCenterOutput_anwl(string startdate, string enddate)
        {
            return dal.AllCenterOutput_anwl(startdate, enddate);
        }

        public List<string[]> AllCenterBarcodeNum_anwl(string startdate, string enddate, string objectid)
        {
            return dal.AllCenterBarcodeNum_anwl(startdate, enddate, objectid);
        }
        /// <summary>
        /// 本中心产量 anwl
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<string[]> MyCenterProduction_anwl(string startdate, string enddate, string objectid)
        {
            return dal.MyCenterProduction_anwl(startdate, enddate, objectid);
        }
        #endregion

        /// <summary>
        /// 更新output退单量
        /// </summary>
        public void UpdateYD_output()
        {
            dal.UpdateYD_output();
        }
         public List<string[]> GetRecordUpRecv_yd(string start,string end)
        { 
              return dal.GetRecordUpRecv_yd(start,end);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thedate">日期</param>
        /// <param name="liststr">已下划线分割的集合字串 </param>
        /// <returns></returns>
         public bool GetInsertUpRecv_yd(string thedate, string liststr)
         {
             return dal. GetInsertUpRecv_yd(thedate, liststr);
         }
    }
}
