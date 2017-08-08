using W1001_ABP_With_Zero.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace W1001_ABP_With_Zero.Web.Controllers
{
    public class AboutController : W1001_ABP_With_ZeroControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}