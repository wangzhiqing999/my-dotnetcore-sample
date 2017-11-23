using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace A0030_Authentication.Controllers
{

    /// <summary>
    /// 测试控制器.
    /// 下面的  [Authorize]  ， 意味着， 本控制器的所有处理， 都要求登录之后， 才可以访问.
    /// </summary>
    [Authorize]
    public class TestController : Controller
    {



        #region Simple Authorization

        /// <summary>
        /// 虽然当前控制器前面，定义了 [Authorize] 
        /// 但是
        /// 由于下面的  [AllowAnonymous]， 意味着， 本方法，可以匿名访问。 也就是没登录， 也可以访问的处理。
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 此方法下，没有加任何标记， 意味着， 根据本控制器的 [Authorize]
        /// 本方法， 需要登录后， 才能访问。
        /// </summary>
        /// <returns></returns>
        public IActionResult Default()
        {
            return View();
        }

        #endregion Simple Authorization






        #region Role based Authorization

        /// <summary>
        /// 此方法下， 有 [Authorize(Roles = "Employee")]
        /// 意味着， 本方法， 需要有 Employee  角色的用户， 才能访问， 否则不能.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeInfo()
        {
            return View();
        }


        /// <summary>
        /// 此方法下， 有 [Authorize(Roles = "Admin")]
        /// 意味着， 本方法， 需要有 Admin  角色的用户， 才能访问， 否则不能.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Manager()
        {
            return View();
        }

        #endregion Role based Authorization








        #region Claims-Based Authorization


        /// <summary>
        /// 此方法下， 有 [Authorize(Policy = "EmployeeOnly")]
        /// 意味着 本方法， 当前的用户， 要满足 EmployeeOnly 的条件， 才能访问。
        /// EmployeeOnly 的具体条件， 定义在 Startup 的 public void ConfigureServices(IServiceCollection services) 方法中.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "EmployeeOnly")]
        public ActionResult Payslip()
        {
            return View();
        }




        /// <summary>
        /// 此方法下， 有 [Authorize(Policy = "Founders")]
        /// 意味着 本方法， 当前的用户， 要满足 Founders 的条件， 才能访问。
        /// Founders 的具体条件， 定义在 Startup 的 public void ConfigureServices(IServiceCollection services) 方法中.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "Founders")]
        public ActionResult SystemData()
        {
            return View();
        }


        #endregion Claims-Based Authorization
    }
}