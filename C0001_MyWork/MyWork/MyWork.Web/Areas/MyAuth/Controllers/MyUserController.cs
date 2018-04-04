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
    /// 用户服务.
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Area("MyAuth")]
    public class MyUserController : Controller
    {

        /// <summary>
        /// 用户服务.
        /// </summary>
        private IUserService _UserService;


        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="userService"></param>
        public MyUserController(IUserService userService)
        {
            this._UserService = userService;
        }


        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="pageNo">第几页</param>
        /// <param name="pageSize">每页几行</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyUser")]
        public CommonQueryResult<MyUser> Query(int pageNo = 1, int pageSize = 10)
        {
            var result = this._UserService.Query(pageNo, pageSize);
            return result;
        }







    }
}