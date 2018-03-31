using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using MyAuthentication.Model;
using MyAuthentication.Service;
using MyAuthentication.ServiceModel;

namespace MyWork.Web.Areas.MyAuth.Controllers
{
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Route("api/MyAuth/MyModule")]
    public class MyModuleController : Controller
    {
        /// <summary>
        /// 模块服务.
        /// </summary>
        private IModuleService _ModuleService;


        public MyModuleController(IModuleService moduleService)
        {
            this._ModuleService = moduleService;
        }


        /// <summary>
        /// 获取模块.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MyModule> Get(string systemCode, string moduleType)
        {
            var result = this._ModuleService.GetModuleList(systemCode, moduleType);
            return result;
        }

    }
}