using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MySSO.Service;
using MySSO.ServiceModel;

using TestDomain.Web.Extension;
using TestDomain.Web.Models;

namespace TestDomain.Web.Controllers
{

    public class AccountController : Controller
    {

        /// <summary>
        /// 登录服务.
        /// </summary>
        private ILoginService _LoginService;


        public AccountController(ILoginService loginService)
        {
            this._LoginService = loginService;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Login(string returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            // 将 returnUrl 存储在 Session 中， 这个 url 不跳转给 SSO 登录页了.
            HttpContext.Session.Set<string>("RETURN_URL", returnUrl);


            // 最终要从 登录网站（reg.test.com）， 跳转至本网站（c.test123.com）的地址.
            string myUrl = "http://c.test123.com/Account/LoginResult";

            string gotoUrl = String.Format("http://reg.test.com/Account/Login?crossdomain=true&returnUrl={0}", WebUtility.UrlEncode(myUrl));

            // 直接跳转至 登录网站的 登录页
            return Redirect(gotoUrl);
        }



        /// <summary>
        /// 登录结果.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult LoginResult(string authToken)
        {
            // 检查 token 是否有效.
            Guid tokenID;
            if (!Guid.TryParse(authToken, out tokenID))
            {
                // 数据无效.
                return Content("无效的Token数据。");
            }

            CommonServiceResult<LoginResultData> loginResult = this._LoginService.IsLogin(tokenID);
            if(!loginResult.IsSuccess)
            {
                // 未登录.
                return Content("未登录。");
            }

            // 写入本网站的 Cookie.
            var userPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, loginResult.ResultData.UserID.ToString()),
                    new Claim(ClaimTypes.Sid, loginResult.ResultData.TokenID.ToString())
                },
                CookieAuthenticationDefaults.AuthenticationScheme));

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    // AllowRefresh = false,
                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)) // 有效时间
                });

            // 跳转至目标页.
            string returnUrl = HttpContext.Session.Get<string>("RETURN_URL");
            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }
            return Redirect(returnUrl);
        }



        public IActionResult LogOff()
        {

            // 本机登出.
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            // SSO 登出.
            string gotoUrl = String.Format("http://reg.test.com/Account/LogOff");

            // 直接跳转至 登录网站的 登出页
            return Redirect(gotoUrl);
        }

    }
}