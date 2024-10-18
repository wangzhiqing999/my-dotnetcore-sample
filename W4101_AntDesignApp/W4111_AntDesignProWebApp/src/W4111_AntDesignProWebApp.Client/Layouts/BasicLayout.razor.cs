using AntDesign.Extensions.Localization;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Json;

namespace W4111_AntDesignProWebApp.Layouts
{
    public partial class BasicLayout : LayoutComponentBase, IDisposable
    {
        private MenuDataItem[] _menuData;

        [Inject] private ReuseTabsService TabService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _menuData = new[] {
                new MenuDataItem
                {
                    Path = "/",
                    Name = "welcome",
                    Key = "welcome",
                    Icon = "smile",
                }
            };
        }

        void Reload()
        {
            TabService.ReloadPage();
        }

        public void Dispose()
        {

        }

    }
}
