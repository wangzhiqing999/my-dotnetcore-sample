using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using A0020_Fundamentals.Extensions;


namespace A0020_Fundamentals.Controllers
{
    public class TestSessionController : Controller
    {

        const string SessionKeyName = "_Name";
        const string SessionKeyDate = "_Date";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SetValue()
        {            
            return View();
        }


        [HttpPost]
        public IActionResult SetValue(string sessionValue)
        {
            HttpContext.Session.SetString(SessionKeyName, sessionValue);
            HttpContext.Session.Set<DateTime>(SessionKeyDate, DateTime.Now);
            return RedirectToAction("Index");
        }


        public IActionResult GetValue()
        {
            var name = HttpContext.Session.GetString(SessionKeyName);
            var date = HttpContext.Session.Get<DateTime>(SessionKeyDate);

            string result = String.Format("{0} @ {1:yyyy-MM-dd HH:mm:ss}", name, date);

            return View(model: result);
        }
    }
}