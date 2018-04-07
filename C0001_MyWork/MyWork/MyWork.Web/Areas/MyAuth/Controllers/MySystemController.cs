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


namespace MyWork.Web.Areas.MyAuth.Controllers
{

    /// <summary>
    /// 系统服务
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Area("MyAuth")]
    [Authorize]
    public class MySystemController : Controller
    {
        /// <summary>
        /// 系统服务.
        /// </summary>
        private ISystemService _SystemService;

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="systemService"></param>
        public MySystemController(ISystemService systemService)
        {
            this._SystemService = systemService;
        }



        /// <summary>
        /// 获取系统.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MySystem")]
        public CommonQueryResult<MySystem> Query(int pageNo = 1, int pageSize = 10)
        {
            var result = this._SystemService.Query(pageNo, pageSize);
            return result;
        }

    }
}