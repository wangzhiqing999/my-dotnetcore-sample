using Microsoft.AspNetCore.Mvc;

namespace W1000_ABP_HelloWorld.Web.Controllers
{
    public class HomeController : W1000_ABP_HelloWorldControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}