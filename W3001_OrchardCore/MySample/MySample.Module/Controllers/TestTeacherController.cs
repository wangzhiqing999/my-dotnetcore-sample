using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MySample.Model;
using MySample.Service;

namespace MySample.Module.Controllers
{

    [Route("MySample/[controller]")]
    public class TestTeacherController : Controller
    {


        private readonly ITestTeacherService _TestTeacherService;


        public TestTeacherController(ITestTeacherService testTeacherService)
        {
            this._TestTeacherService = testTeacherService;
        }



        public IActionResult Index()
        {
            List<TestTeacher> dataList = this._TestTeacherService.GetTestTeacher();

            return View(model: dataList);
        }



    }
}
