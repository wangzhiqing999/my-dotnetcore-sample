using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace A0020_Fundamentals.Controllers
{
    public class TestErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Test500()
        {
            string a = null;
            if(a.Length > 0)
            {
                a = a + "123";
            }

            return Content("测试触发一个服务器异常");
        }




        #region 自定义错误页面.

        [Route("errors/{statusCode}")]
        public IActionResult CustomError(int statusCode)
        {
            Response.StatusCode = statusCode;

            if (statusCode == 404)
            {
                return View("~/Views/TestError/404.cshtml");
            }
            return View("~/Views/TestError/500.cshtml");
        }

        #endregion


    }
}