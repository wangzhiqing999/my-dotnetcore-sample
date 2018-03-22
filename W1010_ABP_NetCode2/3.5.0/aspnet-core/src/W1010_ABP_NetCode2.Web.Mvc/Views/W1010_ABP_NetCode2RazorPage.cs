using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace W1010_ABP_NetCode2.Web.Views
{
    public abstract class W1010_ABP_NetCode2RazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected W1010_ABP_NetCode2RazorPage()
        {
            LocalizationSourceName = W1010_ABP_NetCode2Consts.LocalizationSourceName;
        }
    }
}
