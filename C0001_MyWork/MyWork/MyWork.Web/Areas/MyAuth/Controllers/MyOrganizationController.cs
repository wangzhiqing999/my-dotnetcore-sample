using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

using MyFramework.ServiceModel;

using MyAuthentication.Model;
using MyAuthentication.Service;
using MyAuthentication.ServiceModel;


namespace MyWork.Web.Areas.MyAuth.Controllers
{

    /// <summary>
    /// 组织机构服务.
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Area("MyAuth")]
    [Authorize]
    public class MyOrganizationController : Controller
    {

        /// <summary>
        /// 组织机构服务.
        /// </summary>
        private IOrganizationService _OrganizationService;


        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="service"></param>
        public MyOrganizationController(IOrganizationService service)
        {
            this._OrganizationService = service;
        }



        /// <summary>
        /// 查询 组织机构列表
        /// </summary>
        /// <param name="pageNo">第几页</param>
        /// <param name="pageSize">每页几行</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyOrganization")]
        public CommonQueryResult<MyOrganization> Query(int pageNo =1, int pageSize = 10)
        {
            var result = this._OrganizationService.Query(pageNo, pageSize);
            return result;
        }


        /// <summary>
        /// 获取 组织机构.
        /// </summary>
        /// <param name="id">组织机构代码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyOrganization/Get/{id}")]
        public CommonServiceResult Get(long id)
        {
            var result = this._OrganizationService.GetOrganization(id);
            return result;
        }



        /// <summary>
        /// 新增组织机构.
        /// </summary>
        /// <param name="data">组织机构数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyOrganization/Create")]
        public CommonServiceResult Create([FromBody]MyOrganization data)
        {
            var result = this._OrganizationService.NewOrganization(data);
            return result;
        }


        /// <summary>
        /// 更新组织机构.
        /// </summary>
        /// <param name="data">组织机构数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyOrganization/Update")]
        public CommonServiceResult Update([FromBody]MyOrganization data)
        {
            var result = this._OrganizationService.UpdateOrganization(data);
            return result;
        }


        /// <summary>
        /// 删除组织机构.
        /// </summary>
        /// <param name="data">组织机构代码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyOrganization/Delete")]
        public CommonServiceResult Delete([FromBody]RemoveRequest data)
        {
            long id = Convert.ToInt64(data.id);
            var result = this._OrganizationService.RemoveOrganization(id);
            return result;
        }


    }
}