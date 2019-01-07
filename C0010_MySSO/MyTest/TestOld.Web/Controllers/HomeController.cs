using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestOld.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }




        /// <summary>
        /// 这个用于测试， 
        /// 当不允许匿名访问，必须要登录的情况下， 是否能自动跳转至登录页。
        /// 以及登录页登录完毕后， 是否还能正常的跳转至业务页面。
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Business()
        {
            // 这里获取 用户登录的信息.
            ViewBag.UserID = Session["USER_ID"];

            return View();
        }

    }
}