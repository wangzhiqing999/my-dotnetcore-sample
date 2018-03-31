using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.ServiceModel;

namespace MyAuthentication.Service
{

    /// <summary>
    /// 权限认证服务.
    /// </summary>
    public interface IAuthenticationService
    {

        /// <summary>
        /// 获取用户可访问的 系统 列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<PermissionInfo> GetUserAccessAbleSystems(long userID);


        /// <summary>
        /// 获取用户可访问的 功能模块 列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<PermissionInfo> GetUserAccessAbleModules(long userID);


        /// <summary>
        /// 获取用户可访问的 功能模块动作 列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<PermissionInfo> GetUserAccessAbleActions(long userID);






        /// <summary>
        /// 获取角色可访问的 功能模块 列表.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        List<PermissionInfo> GetRoleAccessAbleModules(string roleCode);


        /// <summary>
        /// 获取角色可访问的 功能模块动作 列表.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        List<PermissionInfo> GetRoleAccessAbleActions(string roleCode);

    }
}
