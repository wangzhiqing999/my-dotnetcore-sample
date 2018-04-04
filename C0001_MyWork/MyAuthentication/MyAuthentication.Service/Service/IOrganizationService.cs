using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;
using MyFramework.ServiceModel;
using MyFramework.Util;

namespace MyAuthentication.Service
{
    /// <summary>
    /// 组织机构服务.
    /// </summary>
    public interface IOrganizationService
    {
        /// <summary>
        /// 查询.
        /// </summary>
        /// <returns></returns>
        CommonQueryResult<MyOrganization> Query(int pageNo=1, int pageSize=10);


        /// <summary>
        /// 获取组织机构.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CommonServiceResult GetOrganization(long id);



        /// <summary>
        /// 新增组织机构.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        CommonServiceResult NewOrganization(MyOrganization organization);


        /// <summary>
        /// 编辑组织机构.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        CommonServiceResult UpdateOrganization(MyOrganization organization);


        /// <summary>
        /// 删除组织机构.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CommonServiceResult RemoveOrganization(long id);
    }
}
