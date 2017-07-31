using Abp.AspNetCore.Mvc.Views;

namespace W1000_ABP_HelloWorld.Web.Views
{
    public abstract class W1000_ABP_HelloWorldRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected W1000_ABP_HelloWorldRazorPage()
        {
            LocalizationSourceName = W1000_ABP_HelloWorldConsts.LocalizationSourceName;
        }
    }
}
