using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using MyFramework.ServiceModel;

using MyAuthentication.Model;
using MyAuthentication.Service;
using MyAuthentication.ServiceModel;


namespace MyWork.Web.Areas.MyAuth.Controllers
{
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    public class MyOrganizationController : Controller
    {

        /// <summary>
        /// 组织机构服务.
        /// </summary>
        private IOrganizationService _OrganizationService;


        public MyOrganizationController(IOrganizationService service)
        {
            this._OrganizationService = service;
        }



        /// <summary>
        /// 查询 组织机构.
        /// </summary>
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
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyOrganization/Get/{id}")]
        public MyOrganization Get(long id)
        {
            var result = this._OrganizationService.GetOrganization(id);
            return result;
        }



        /// <summary>
        /// 新增组织机构.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyOrganization/Insert")]
        public CommonServiceResult Insert([FromBody]MyOrganization data)
        {
            var result = this._OrganizationService.NewOrganization(data);
            return result;
        }


        /// <summary>
        /// 更新.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyOrganization/Update")]
        public CommonServiceResult Update([FromBody]MyOrganization data)
        {
            var result = this._OrganizationService.UpdateOrganization(data);
            return result;
        }


        /// <summary>
        /// 删除.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyOrganization/Delete")]
        public CommonServiceResult Delete(long id)
        {
            var result = this._OrganizationService.RemoveOrganization(id);
            return result;
        }


    }
}