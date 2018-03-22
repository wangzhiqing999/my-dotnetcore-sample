using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using W1010_ABP_NetCode2.Controllers;

namespace W1010_ABP_NetCode2.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : W1010_ABP_NetCode2ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
