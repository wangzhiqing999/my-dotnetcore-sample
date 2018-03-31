using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;
using MyFramework.ServiceModel;

namespace MyAuthentication.Service
{

    /// <summary>
    /// 角色服务.
    /// </summary>
    public interface IRoleService
    {

        /// <summary>
        /// 获取全部角色.
        /// </summary>
        /// <returns></returns>
        List<MyRole> GetAllRoles();


        /// <summary>
        /// 获取角色.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MyRole GetRole(string id);


        /// <summary>
        /// 新增角色.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        CommonServiceResult NewRole(MyRole role);


        /// <summary>
        /// 编辑角色.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        CommonServiceResult UpdateRole(MyRole role);


        /// <summary>
        /// 删除角色.
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        CommonServiceResult RemoveRole(string roleCode);
    }
}
