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
    [Route("api/MyAuth/MyRole")]
    [Area("MyAuth")]
    public class MyRoleController : Controller
    {
        /// <summary>
        /// 角色服务.
        /// </summary>
        private IRoleService _RoleService;


        public MyRoleController(IRoleService roleService)
        {
            this._RoleService = roleService;
        }


        [HttpGet]
        public CommonQueryResult<MyRole> Query(string systemCode, int pageNo = 1, int pageSize = 10)
        {
            var result = this._RoleService.Query(systemCode, pageNo, pageSize);
            return result;
        }

    }
}