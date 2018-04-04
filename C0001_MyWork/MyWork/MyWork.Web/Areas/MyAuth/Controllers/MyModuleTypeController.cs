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

    /// <summary>
    /// 模块类型服务
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]    
    public class MyModuleTypeController : Controller
    {
        /// <summary>
        /// 模块类型服务.
        /// </summary>
        private IModuleTypeService _ModuleTypeService;


        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="moduleTypeService"></param>
        public MyModuleTypeController(IModuleTypeService moduleTypeService)
        {
            this._ModuleTypeService = moduleTypeService;
        }



        /// <summary>
        /// 获取模块类型列表.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyModuleType")]
        public CommonQueryResult<MyModuleType> Query(int pageNo = 1, int pageSize = 10)
        {
            var result = this._ModuleTypeService.GetModuleTypeList(pageNo, pageSize);
            return result;
        }

    }
}