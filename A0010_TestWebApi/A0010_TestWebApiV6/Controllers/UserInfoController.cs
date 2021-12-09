using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace A0010_TestWebApiV6.Controllers
{
    [Produces("application/json")]
    [Route("api/UserInfo")]
    [Authorize]
    public class UserInfoController : Controller
    {


        // GET api/UserInfo
        [HttpGet]
        public string Get()
        {

            // 这里主要的测试目标， 是通过令牌， 获取【当前登录用户】的详细信息。

            // 获取令牌内的详细信息.
            var claimsIdentity = User.Identity as ClaimsIdentity;

            // 这里能获取到哪些信息， 取决于 AuthorizeController 那里， 生成 Token 的时候， 都填写了哪些。
            var name = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var role = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            string result = String.Format("UserName={0}; Role={1}",name, role);
            return result;
        }


    }
}