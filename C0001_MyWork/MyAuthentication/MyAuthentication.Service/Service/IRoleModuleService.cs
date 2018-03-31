using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;
using MyFramework.ServiceModel;


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

    }
}
