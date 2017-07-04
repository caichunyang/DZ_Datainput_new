using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;

namespace DZ.BLL
{
  public  class DZ_OVERDUE_BLL
    {
      DZ_OVERDU_DAL dal = new DZ_OVERDU_DAL();
      public bool insert(string keys, string datenum,string[] array)
      {
          return dal.insert(keys, datenum, array);
      }
      public string[] loadentity(string key, string date)
      {
          return dal.loadentity(key, date);
      }
      public bool IsContainSameDate(string key, string date)
      {
          return dal.IsContainSameDate(key, date);
      }
      /// <summary>
      /// date为主键  包含则更新 否则插入
      /// </summary>
      /// <param name="key"></param>
      /// <param name="date"></param>
      /// <param name="array"></param>
      /// <returns></returns>
        public bool myinsert(string key, string date,string[] array)
      {
          if (dal.IsContainSameDate(key, date))
          {
            return  dal.update(key, date, array);
          }
          else
          {
              return dal.insert(key,date, array);
          }
      }

        public bool insert2(string keys, string datenum, string[] array)
        {
            return dal.insert2(keys, datenum, array);
        }
        public string[] loadentity2(string key, string date)
        {
            return dal.loadentity2(key, date);
        }
        public bool IsContainSameDate2(string key, string date)
        {
            return dal.IsContainSameDate2(key, date);
        }
    }
}
