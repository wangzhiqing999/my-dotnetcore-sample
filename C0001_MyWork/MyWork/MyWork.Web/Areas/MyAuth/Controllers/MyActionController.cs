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

    /// <summary>
    /// 动作服务.
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Route("api/MyAuth/MyAction")]
    public class MyActionController : Controller
    {

        /// <summary>
        /// 动作服务.
        /// </summary>
        private IActionService _ActionService;


        public MyActionController(IActionService actionService)
        {
            this._ActionService = actionService;
        }


        /// <summary>
        /// 获取动作.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MyAction> Get(string moduleCode)
        {
            var result = this._ActionService.GetActionList(moduleCode);
            return result;
        }

    }
}