using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MySSO.Service;
using MySSO.ServiceModel;

using MySSO.Web.Models;

namespace MySSO.Web.Controllers
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
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }



        /// <summary>
        /// 提交登录.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            // 登录.
            CommonServiceResult<LoginResultData> result = this._LoginService.DoLogin(model.UserName, model.Password);
            ViewBag.LoginResult = result;


            // 登录失败的情况下， 停留在本页面.
            if (!result.IsSuccess)
            {                
                return View();
            }


            // 登录成功的情况下.
            var userPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, result.ResultData.UserID.ToString()),
                    new Claim(ClaimTypes.Sid, result.ResultData.TokenID.ToString())
                },
                CookieAuthenticationDefaults.AuthenticationScheme));


            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, 
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    // AllowRefresh = false,
                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)) // 有效时间
                });

            return View();
        }





        public IActionResult LogOff()
        {
            string tokenString = User.FindFirst(ClaimTypes.Sid).Value;
            Guid tokenID;
            if(Guid.TryParse(tokenString, out tokenID))
            {
                this._LoginService.Logout(tokenID);
            }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 根据来源， 判断是跳转到 登录网站首页， 还是业务网站首页.
            string fromUrl = Request.Headers["Referer"];

            if (String.IsNullOrEmpty(fromUrl))
            {
                // 未知的情况下，简单返回本网站首页.
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            Uri siteUri = new Uri(fromUrl);

            string host = siteUri.Host;

            if(siteUri.Scheme == Uri.UriSchemeHttp)
            {
                string httpUrl = $"http://{host}";
                // 跳回来源网站首页.
                return Redirect(httpUrl);
            }
            else if (siteUri.Scheme == Uri.UriSchemeHttps)
            {
                string httpsUrl = $"https://{host}";
                // 跳回来源网站首页.
                return Redirect(httpsUrl);
            }

            // 不是 http / https 的情况下， 还是回到本网站首页.
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }


    }
}