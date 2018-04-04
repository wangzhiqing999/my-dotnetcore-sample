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


namespace MyWork.Web.Areas.MyAuth.Controllers
{

    /// <summary>
    /// 模块服务
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]    
    public class MyModuleController : Controller
    {
        /// <summary>
        /// 模块服务.
        /// </summary>
        private IModuleService _ModuleService;

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="moduleService"></param>
        public MyModuleController(IModuleService moduleService)
        {
            this._ModuleService = moduleService;
        }


        /// <summary>
        /// 获取模块.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyModule")]
        public CommonQueryResult<MyModule> Query(string systemCode, string moduleType, int pageNo = 1, int pageSize = 10)
        {
            var result = this._ModuleService.GetModuleList(systemCode, moduleType, pageNo, pageSize);
            return result;
        }

    }
}