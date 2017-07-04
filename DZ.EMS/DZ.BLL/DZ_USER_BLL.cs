using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;
using DZ.MODEL;


namespace DZ.BLL
{
    public class DZ_USER_BLL
    {
        public bool CheckUser(string username, string password)
        {
            DZ_USER_DAL dal = new DZ_USER_DAL();
            return dal.CkeckUser(username, password);
        }
        public DZ_USER CheckUserAndPwd(string username, string password)
        {
            DZ_USER_DAL dal = new DZ_USER_DAL();
            return dal.LoginObj(username, password);
        }
        /// <summary>
        /// 根据用户ID查 模块权限ID列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<DZ_MODULE> GetMuduleByUserId(string userid)
        {
            DZ_USER_DAL dal = new DZ_USER_DAL();
            return dal.GetMuduleByUserId(userid);
        }
    }
}
