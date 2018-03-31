using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MyAuthentication.Service;
using MyAuthentication.ServiceModel;


namespace MyWork.Web.Controllers
{

    [Produces("application/json")]
    [Route("api/Permission")]
    public class PermissionController : TokenAbleController
    {

        /// <summary>
        /// 权限认证服务
        /// </summary>
        private IAuthenticationService _AuthenticationService;

        public PermissionController(IAuthenticationService authenticationService)
        {
            this._AuthenticationService = authenticationService;
        }



        [HttpGet]
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


        [HttpGet]
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



        [HttpGet]
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