using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySimpleAccessCount.Module.Models;

namespace MySimpleAccessCount.Module.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("MySimpleAccessCount/Home")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("MySimpleAccessCount/Test")]
        public IActionResult Test(string id)
        {
            ViewData["PageCode"] = id;
            return View();
        }

        
    }
}
