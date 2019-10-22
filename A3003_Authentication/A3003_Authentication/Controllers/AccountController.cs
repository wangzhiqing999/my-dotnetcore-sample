using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

using A3003_Authentication.Models;


namespace A3003_Authentication.Controllers
{
    public class AccountController : Controller
    {

        /// <summary>
        /// 日志.
        /// </summary>
		private readonly ILogger<AccountController> _Logger;



        public AccountController(ILogger<AccountController> logger)
        {
            this._Logger = logger;
        }




        // 进入登录页.
        // GET: /Account/Login
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        /// <summary>
        /// 提交登录.
        /// </summary>
        /// <param name="model">登录数据</param>
        /// <param name="returnUrl">返回地址</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                this._Logger.LogDebug("登录失败！");

                ViewBag.Message = "提供的用户名或密码不正确！";
                return View(model);
            }


            if (!(model.UserName == "test" && model.Password == "123456"))
            {
                this._Logger.LogDebug("登录失败！");

                ViewBag.Message = "您输入的用户名或密码不正确。";
                return View(model);
            }


            // 登录成功的情况下.
            var userPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "test"),
                    new Claim(ClaimTypes.Sid, Guid.Empty.ToString())
                },
                CookieAuthenticationDefaults.AuthenticationScheme));


            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    // AllowRefresh = false,
                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)) // 有效时间
                }).Wait();

            this._Logger.LogDebug($"登录成功！页面跳转：{returnUrl}");

            return Redirect(returnUrl);
        }



        /// <summary>
        /// 登出.
        /// </summary>
        /// <returns></returns>
        public IActionResult LogOff()
        {
            string userNameString = User.FindFirst(ClaimTypes.Name).Value;
            string tokenString = User.FindFirst(ClaimTypes.Sid).Value;

            this._Logger.LogDebug($"用户 {userNameString} 登出， Token = {tokenString}");

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            // 回到本网站首页.
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }



    }
}