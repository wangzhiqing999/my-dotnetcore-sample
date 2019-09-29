using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using MySimpleAccessCount.Service;


namespace MySimpleAccessCount.Module.Controllers
{

    [Produces("application/json")]
    [ApiController]
    public class SimpleAccessCountController : ControllerBase
    {

        private readonly ISimpleAccessCountService _SimpleAccessCountService;


        public SimpleAccessCountController(ISimpleAccessCountService simpleAccessCountService)
        {
            this._SimpleAccessCountService = simpleAccessCountService;
        }



        [HttpGet]
        [Route("api/MySimpleAccessCount/GetAccessCount")]
        public long GetAccessCount(string id)
        {
            long result = this._SimpleAccessCountService.AccessCount(id);
            return result;
        }



    }
}