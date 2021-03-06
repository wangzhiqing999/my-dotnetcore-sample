﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MyAuthentication.ServiceModel;

namespace MyWork.Web.Controllers
{

    /// <summary>
    /// 支持 Token 处理的控制器.
    /// </summary>
    [Produces("application/json")]
    public abstract class TokenAbleController : Controller
    {


        /// <summary>
        /// 获取登录用户信息.
        /// </summary>
        /// <returns></returns>
        protected BasicUserInfo GetUserInfoFromToken()
        {
            // 获取令牌内的详细信息.
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if(claimsIdentity == null)
            {
                return null;
            }
            BasicUserInfo result = new BasicUserInfo();

            // 用户姓名.
            result.UserName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            // 用户ID.
            result.UserID = Convert.ToInt64(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            // 组织ID.
            result.OrganizationID = Convert.ToInt64(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid).Value);

            return result;
        }


    }


    


}