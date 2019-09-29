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
    /// api/MySample2/TestSchool
    /// </summary>
    [Route("api/MySample2/[controller]")]
    [ApiController]
    public class TestSchoolController : ControllerBase
    {

        private readonly ITestSchoolService _TestSchoolService;


        public TestSchoolController(ITestSchoolService testSchoolService)
        {
            this._TestSchoolService = testSchoolService;
        }


        [HttpGet]
        public List<TestSchool> Get()
        {
            List<TestSchool> dataList = this._TestSchoolService.GetTestSchoolList();
            return dataList;
        }

    }
}