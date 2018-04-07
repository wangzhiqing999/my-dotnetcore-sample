using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;


using MyAuthentication.Service;
using MyAuthentication.ServiceModel;


namespace MyWork.Web.Controllers
{

    /// <summary>
    /// 权限服务.
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Authorize]
    public class PermissionController : TokenAbleController
    {

        /// <summary>
        /// 权限认证服务
        /// </summary>
        private IAuthenticationService _AuthenticationService;


        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="authenticationService"></param>
        public PermissionController(IAuthenticationService authenticationService)
        {
            this._AuthenticationService = authenticationService;
        }



        /// <summary>
        /// 获取当前用户可访问的系统.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Permission/AccessAbleSystems")]
        public IActionResult AccessAbleSystems()
        {
            BasicUserInfo userInfo = base.GetUserInfoFromToken();
            if(userInfo == null)
            {
                return BadRequest();
            }

            var result = this._AuthenticationService.GetUserAccessAbleSystems(userInfo.UserID);
            return Ok(result);
        }


        /// <summary>
        /// 获取当前用户可访问的模块
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Permission/AccessAbleModules")]
        public IActionResult AccessAbleModules()
        {
            BasicUserInfo userInfo = base.GetUserInfoFromToken();
            if (userInfo == null)
            {
                return BadRequest();
            }

            var result = this._AuthenticationService.GetUserAccessAbleModules(userInfo.UserID);
            return Ok(result);
        }



        /// <summary>
        /// 获取当前用户可访问的动作.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Permission/AccessAbleActions")]
        public IActionResult AccessAbleActions()
        {
            BasicUserInfo userInfo = base.GetUserInfoFromToken();
            if (userInfo == null)
            {
                return BadRequest();
            }

            var result = this._AuthenticationService.GetUserAccessAbleActions(userInfo.UserID);
            return Ok(result);
        }

    }
}