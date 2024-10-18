using AntDesign.Extensions.Localization;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Json;

namespace W4112_AntDesignProWasm.Layouts
{
    public partial class BasicLayout : LayoutComponentBase, IDisposable
    {
        private MenuDataItem[] _menuData;

        [Inject] private ReuseTabsService TabService { get; set; }

        [Inject] private HttpClient HttpClient { get; set; }

        [Inject] private ILocalizationService LocalizationService { get; set; }

        private EventHandler<CultureInfo> _localizationChanged;


        protected override async Task OnInitializedAsync()
        {
            _localizationChanged = (sender, args) => InvokeAsync(StateHasChanged);
            LocalizationService.LanguageChanged += _localizationChanged;
            _menuData = await HttpClient.GetFromJsonAsync<MenuDataItem[]>("data/menu.json");
        }

        void Reload()
        {
            TabService.ReloadPage();
        }

        public void Dispose()
        {
            LocalizationService.LanguageChanged -= _localizationChanged;
        }

    }
}
