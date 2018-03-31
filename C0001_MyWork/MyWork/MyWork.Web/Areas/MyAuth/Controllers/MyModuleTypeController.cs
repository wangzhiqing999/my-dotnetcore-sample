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
    [Route("api/MyAuth/MyModuleType")]
    public class MyModuleTypeController : Controller
    {
        /// <summary>
        /// 模块类型服务.
        /// </summary>
        private IModuleTypeService _ModuleTypeService;


        public MyModuleTypeController(IModuleTypeService moduleTypeService)
        {
            this._ModuleTypeService = moduleTypeService;
        }



        /// <summary>
        /// 获取全部的 模块类型.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MyModuleType> GetAll()
        {
            var result = this._ModuleTypeService.GetModuleTypeList();
            return result;
        }

    }
}