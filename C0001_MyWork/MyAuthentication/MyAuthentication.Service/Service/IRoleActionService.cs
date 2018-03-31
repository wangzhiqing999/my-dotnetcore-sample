using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;
using MyFramework.ServiceModel;


namespace MyAuthentication.Service
{

    /// <summary>
    /// 角色-动作关系服务.
    /// </summary>
    public interface IRoleActionService
    {

        /// <summary>
        /// 获取指定角色所关联的动作.
        /// </summary>
        /// <returns></returns>
        List<MyAction> GetActionByRoleCode(string roleCode);


        /// <summary>
        /// 更新指定角色所关联的动作列表.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="actionCodeList"></param>
        /// <returns></returns>
        CommonServiceResult UpdateRolesAction(string roleCode, List<string> actionCodeList);

    }
}
