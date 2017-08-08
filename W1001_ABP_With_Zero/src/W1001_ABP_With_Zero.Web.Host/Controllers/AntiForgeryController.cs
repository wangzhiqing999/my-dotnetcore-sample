using W1001_ABP_With_Zero.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace W1001_ABP_With_Zero.Web.Host.Controllers
{
    public class AntiForgeryController : W1001_ABP_With_ZeroControllerBase
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