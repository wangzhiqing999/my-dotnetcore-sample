using Microsoft.AspNetCore.Antiforgery;
using W1010_ABP_NetCode2.Controllers;

namespace W1010_ABP_NetCode2.Web.Host.Controllers
{
    public class AntiForgeryController : W1010_ABP_NetCode2ControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
