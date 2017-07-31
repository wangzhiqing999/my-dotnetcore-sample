using Abp.Application.Navigation;
using Abp.Localization;

namespace W1000_ABP_HelloWorld.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class W1000_ABP_HelloWorldNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "Home/About",
                        icon: "fa fa-info"
                        )
                );




            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.SystemConfigType,
                        L("SystemConfigType"),
                        url: "SystemConfigType/Index",
                        icon: "fa fa-tasks"
                        )
                );

        }



        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, W1000_ABP_HelloWorldConsts.LocalizationSourceName);
        }
    }
}
