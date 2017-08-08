using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using W1001_ABP_With_Zero.Configuration;
using W1001_ABP_With_Zero.Configuration.Ui;
using Microsoft.AspNetCore.Mvc;

namespace W1001_ABP_With_Zero.Web.Views.Shared.Components.RightSideBar
{
    public class RightSideBarViewComponent : W1001_ABP_With_ZeroViewComponent
    {
        private readonly ISettingManager _settingManager;

        public RightSideBarViewComponent(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var themeName = await _settingManager.GetSettingValueAsync(AppSettingNames.UiTheme);

            var viewModel = new RightSideBarViewModel
            {
                CurrentTheme = UiThemes.All.FirstOrDefault(t => t.CssClass == themeName)
            };

            return View(viewModel);
        }
    }
}
