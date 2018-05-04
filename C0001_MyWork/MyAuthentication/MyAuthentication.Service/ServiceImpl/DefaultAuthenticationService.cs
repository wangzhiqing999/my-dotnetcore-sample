using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MyFramework.ServiceModel;
using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;
using MyAuthentication.ServiceModel;


namespace MyAuthentication.ServiceImpl
{
    /// <summary>
    /// 权限认证服务.
    /// </summary>
    public class DefaultAuthenticationService : IAuthenticationService
    {
        private readonly MyAuthenticationContext context;


        public DefaultAuthenticationService(MyAuthenticationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 获取角色可访问的 功能模块动作 列表.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetRoleAccessAbleActions(string roleCode)
        {

            var query =
                from data in context.MyRoleActions
                where
                    data.RoleCode == roleCode
                select
                    new PermissionInfo()
                    {
                        PermissionCode = data.ActionCode,
                        PermissionName = data.Action.ActionName,
                        PermissionPath = data.Action.ActionExpand
                    };

            List<PermissionInfo> resultList = query.ToList();
            return resultList;

        }


        /// <summary>
        /// 获取角色可访问的 功能模块 列表.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetRoleAccessAbleModules(string roleCode)
        {

            var query =
                from data in context.MyRoleModules
                where
                    data.RoleCode == roleCode
                select
                    new PermissionInfo()
                    {
                        PermissionCode = data.ModuleCode,
                        PermissionName = data.Module.ModuleName,
                        PermissionPath = data.Module.ModuleExpand
                    };

            List<PermissionInfo> resultList = query.ToList();
            return resultList;

        }



        /// <summary>
        /// 获取用户可访问的 功能模块动作 列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetUserAccessAbleActions(long userID)
        {

            var query =
                from data in context.MyUserRoles
                from raData in context.MyRoleActions
                where
                    data.RoleCode == raData.RoleCode
                    && data.UserID == userID
                select
                    new PermissionInfo()
                    {
                        PermissionCode = raData.ActionCode,
                        PermissionName = raData.Action.ActionName,
                        PermissionPath = raData.Action.ActionExpand
                    };

            List<PermissionInfo> resultList = query.ToList();
            return resultList;

        }



        /// <summary>
        /// 获取用户可访问的 功能模块 列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetUserAccessAbleModules(long userID)
        {

            var query =
                from data in context.MyUserRoles
                from rmData in context.MyRoleModules
                where
                    data.RoleCode == rmData.RoleCode
                    && data.UserID == userID
                select
                    new PermissionInfo()
                    {
                        PermissionCode = rmData.ModuleCode,
                        PermissionName = rmData.Module.ModuleName,
                        PermissionPath = rmData.Module.ModuleExpand
                    };

            List<PermissionInfo> resultList = query.ToList();
            return resultList;

        }



        /// <summary>
        /// 获取用户可访问的 系统 列表.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetUserAccessAbleSystems(long userID)
        {

            var query =
                from data in context.MySystemUsers
                where
                    data.UserID == userID
                select
                    new PermissionInfo()
                    {
                        PermissionCode = data.SystemCode,
                        PermissionName = data.System.SystemName,
                        PermissionPath = String.Format("/{0}/", data.SystemCode),
                    };

            List<PermissionInfo> resultList = query.ToList();
            return resultList;

        }
    }
}
