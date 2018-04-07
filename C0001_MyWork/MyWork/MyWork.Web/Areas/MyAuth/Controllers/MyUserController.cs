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
    /// 用户服务.
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Area("MyAuth")]
    [Authorize]
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

        /// <summary>
        /// 获取用户.
        /// </summary>
        /// <param name="id">用户代码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyAuth/MyUser/Get/{id}")]
        public CommonServiceResult Get(long id)
        {
            var result = this._UserService.GetUser(id);
            return result;
        }


        /// <summary>
        /// 新增用户.
        /// </summary>
        /// <param name="data">用户数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyUser/Create")]
        public CommonServiceResult Create([FromBody]MyUser data)
        {
            var result = this._UserService.NewUser(data);
            return result;
        }



        /// <summary>
        /// 更新用户.
        /// </summary>
        /// <param name="data">用户数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyUser/Update")]
        public CommonServiceResult Update([FromBody]MyUser data)
        {
            var result = this._UserService.UpdateUser(data);
            return result;
        }



        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="data">用户代码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyAuth/MyUser/Delete")]
        public CommonServiceResult Delete([FromBody]RemoveRequest data)
        {
            long id = Convert.ToInt64(data.id);
            var result = this._UserService.RemoveUser(id);
            return result;
        }

    }
}