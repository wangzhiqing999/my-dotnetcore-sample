using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Web.Models;

namespace Test.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        /// <summary>
        /// 这个用于测试， 
        /// 当不允许匿名访问，必须要登录的情况下， 是否能自动跳转至登录页。
        /// 以及登录页登录完毕后， 是否还能正常的跳转至业务页面。
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Business()
        {
            // 这里获取 用户登录的信息.
            string UserID = User.FindFirst(ClaimTypes.Name).Value;
            ViewBag.UserID = UserID;

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
