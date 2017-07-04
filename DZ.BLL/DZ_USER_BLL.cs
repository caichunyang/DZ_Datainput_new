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
        DZ_USER_DAL dal = new DZ_USER_DAL();
        //public bool CheckUser(string username, string password)
        //{
        //    return dal.CkeckUser(username, password);
        //}
        public DZ_USER CheckUserAndPwd(string username, string password)
        {
            
            return dal.LoginObj(username, password);
        }
        /// <summary>
        /// 根据用户ID查 模块权限ID列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<DZ_MODULE> GetMuduleByUserId(string userid)
        {
            return dal.GetMuduleByUserId(userid);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChangePwd(string userid,string oldpwd, string newpwd)
        {
            return dal.ChangePwd(userid,oldpwd, newpwd);
        }
        public bool UpdateUserInfo(DZ_USER model)
        { 
         return dal.UpdateUserInfo(model);
        }
        /// <summary>
        /// 员工按条件分页查询
        /// </summary>
        /// <param name="objid"></param>
        /// <param name="userid"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<DZ_USER> LoadEntities(string objid, string userid, int pagesize, int page,string where)
        {
            return dal.LoadEntities(objid, userid, pagesize, page,where);
        }
        /// <summary>
        /// 查分页数量
        /// </summary>
        /// <param name="objid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int selectpagecount(string objid, string userid)
        {
            return dal.selectpagecount(objid,userid);
        }
        /// <summary>
        /// 录入程序（郭）读取的权限 '|'
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetModulnameliststr(string userid)
        {
            return dal.GetModulnameliststr(userid);
        }
    }
}
