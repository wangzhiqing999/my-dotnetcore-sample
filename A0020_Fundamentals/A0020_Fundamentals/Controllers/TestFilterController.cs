using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using A0020_Fundamentals.Filters;
using Microsoft.Extensions.Logging;

namespace A0020_Fundamentals.Controllers
{

    public class TestFilterController : Controller
    {

        private readonly ILogger _logger;


        public TestFilterController(ILogger<TestFilterController> logger)
        {
            this._logger = logger;
        }


        // Filters in ASP.NET Core
        // 文档参考：
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.1

        [ServiceFilter(typeof(HelloWorldResultFilter))]
        [ServiceFilter(typeof(HelloWorldActionFilter))]
        public IActionResult Index()
        {

            _logger.LogInformation("########## Index Start! ########## Action 方法执行！");

            return View();
        }
    }
}