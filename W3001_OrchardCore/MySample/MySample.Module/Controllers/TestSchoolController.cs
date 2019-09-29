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
    public class TestSchoolController : Controller
    {

        private readonly ITestSchoolService _TestSchoolService;


        public TestSchoolController(ITestSchoolService testSchoolService)
        {
            this._TestSchoolService = testSchoolService;
        }



        public IActionResult Index()
        {
            List<TestSchool> dataList = this._TestSchoolService.GetTestSchoolList();

            return View(model: dataList);
        }




    }
}
