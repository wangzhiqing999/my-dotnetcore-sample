using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MySample.Model;
using MySample.Service;

namespace MySample2.Module.Controllers
{

    /// <summary>
    /// api/MySample2/TestTeacher
    /// </summary>
    [Route("api/MySample2/[controller]")]
    [ApiController]
    public class TestTeacherController : ControllerBase
    {


        private readonly ITestTeacherService _TestTeacherService;


        public TestTeacherController(ITestTeacherService testTeacherService)
        {
            this._TestTeacherService = testTeacherService;
        }



        [HttpGet]
        public List<TestTeacher> Get()
        {
            List<TestTeacher> dataList = this._TestTeacherService.GetTestTeacher();
            return dataList;
        }

    }
}