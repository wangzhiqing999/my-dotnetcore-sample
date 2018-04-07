using System;
using System.Collections.Generic;
using System.Text;

using MyFramework.ServiceModel;
using MyAuthentication.Model;
using MyAuthentication.ServiceModel;

namespace MyAuthentication.Service
{

    /// <summary>
    /// 角色-功能模块关系服务.
    /// </summary>
    public interface IRoleModuleService
    {

        /// <summary>
        /// 获取指定角色所关联的模块.
        /// </summary>
        /// <returns></returns>
        List<MyModule> GetModuleByRoleCode(string roleCode);


        /// <summary>
        /// 更新指定角色所关联的模块列表.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="moduleCodeList"></param>
        /// <returns></returns>
        CommonServiceResult UpdateRolesModule(string roleCode, List<string> moduleCodeList);




        /// <summary>
        /// 获取指定角色的 可管理模块列表.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        List<ManagerAbleModule> GetManagerAbleModuleByRoleCode(string roleCode);


        /// <summary>
        /// 更新指定角色的 可管理模块列表.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        CommonServiceResult UpdateManagerAbleModule(string roleCode, List<ManagerAbleModule> dataList);
    }
}
