using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Web.Controllers
{


    public class AccountController : Controller
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Login(string returnUrl)
        {
            // 最终要从 登录网站（reg.test.com）， 跳转至本网站（a.test.com）的地址.
            string myUrl = String.Format("http://a.test.com{0}", returnUrl);

            string gotoUrl = String.Format("http://reg.test.com/Account/Login?returnUrl={0}", WebUtility.UrlEncode(myUrl));

            // 直接跳转至 登录网站的 登录页
            return Redirect(gotoUrl);
        }


        public IActionResult LogOff()
        {
            string gotoUrl = String.Format("http://reg.test.com/Account/LogOff");

            // 直接跳转至 登录网站的 登出页
            return Redirect(gotoUrl);
        }


    }
}