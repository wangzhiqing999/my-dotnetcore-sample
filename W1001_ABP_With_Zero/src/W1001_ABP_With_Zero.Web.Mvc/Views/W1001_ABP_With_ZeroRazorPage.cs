using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace W1001_ABP_With_Zero.Web.Views
{
    public abstract class W1001_ABP_With_ZeroRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected W1001_ABP_With_ZeroRazorPage()
        {
            LocalizationSourceName = W1001_ABP_With_ZeroConsts.LocalizationSourceName;
        }
    }
}
