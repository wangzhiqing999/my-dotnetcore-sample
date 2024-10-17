using AntDesign.Extensions.Localization;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Json;

namespace W4101_AntDesignApp.Layouts
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
                },


                new MenuDataItem
                {
                    Path = "/helloworld",
                    Name = "helloworld",
                    Key = "helloworld",
                    Icon = "cloud",
                },

                new MenuDataItem
                {
                    Path = "/counter",
                    Name = "counter",
                    Key = "counter",
                    Icon = "tool",
                },


                new MenuDataItem
                {
                    Path = "",
                    Name = "测试主/子菜单",
                    Key = "test_main_sub",
                    Icon = "file",
                    Children = new[]
                    {
                        new MenuDataItem
                        {
                            Path = "/testpage1",
                            Name = "测试子菜单1-全小写",
                            Key = "testpage1",
                            Icon = "file-excel",
                        },
                        new MenuDataItem
                        {
                            Path = "/TESTPAGE2",
                            Name = "测试子菜单2-全大写",
                            Key = "TESTPAGE2",
                            Icon = "file-gif",
                        },
                        new MenuDataItem
                        {
                            Path = "/TestPage3",
                            Name = "测试子菜单3-大小写",
                            Key = "TestPage3",
                            Icon = "file-jpg",
                        },
                    }
                },

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
