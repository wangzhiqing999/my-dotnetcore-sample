using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace W1100_Page.Web.Controllers
{
    public class FinanceVueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 更多 的方式翻页.
        /// </summary>
        /// <returns></returns>
        public IActionResult More()
        {
            return View();
        }


        /// <summary>
        /// 滚动条 的方式翻页.
        /// </summary>
        /// <returns></returns>
        public IActionResult Scroll()
        {
            return View();
        }


        /// <summary>
        /// 标准翻页 的方式.
        /// </summary>
        /// <returns></returns>
        public IActionResult Page()
        {
            return View();
        }



    }
}