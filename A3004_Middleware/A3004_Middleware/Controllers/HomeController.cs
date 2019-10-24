using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using A3004_Middleware.Models;

namespace A3004_Middleware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }





        /// <summary>
        /// 测试不可访问的页面.
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult TestAccessDisable()
        {
            return View();
        }



        /// <summary>
        /// 不可访问时的结果的页面
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDisable(string path)
        {
            ViewBag.Path = path;
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
