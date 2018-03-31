using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;
using MyFramework.ServiceModel;

namespace MyAuthentication.Service
{
    /// <summary>
    /// 用户-角色关系服务.
    /// </summary>
    public interface IUserRoleService
    {

        /// <summary>
        /// 获取指定用户所关联的角色.
        /// </summary>
        /// <returns></returns>
        List<MyRole> GetRoleByUserID(long userID);


        /// <summary>
        /// 更新指定用户所关联的角色列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleCodeList"></param>
        /// <returns></returns>
        CommonServiceResult UpdateUsersRole(long userID, List<string> roleCodeList);


    }
}
