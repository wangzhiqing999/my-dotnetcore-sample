using System.Threading.Tasks;
using Abp.AutoMapper;
using W1001_ABP_With_Zero.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace W1001_ABP_With_Zero.Web.Views.Shared.Components.TenantChange
{
    public class TenantChangeViewComponent : W1001_ABP_With_ZeroViewComponent
    {
        private readonly ISessionAppService _sessionAppService;

        public TenantChangeViewComponent(ISessionAppService sessionAppService)
        {
            _sessionAppService = sessionAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginInfo = await _sessionAppService.GetCurrentLoginInformations();
            var model = loginInfo.MapTo<TenantChangeViewModel>();
            return View(model);
        }
    }
}
