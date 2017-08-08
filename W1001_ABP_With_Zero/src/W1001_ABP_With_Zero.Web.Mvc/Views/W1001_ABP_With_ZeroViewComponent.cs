using Abp.AspNetCore.Mvc.ViewComponents;

namespace W1001_ABP_With_Zero.Web.Views
{
    public abstract class W1001_ABP_With_ZeroViewComponent : AbpViewComponent
    {
        protected W1001_ABP_With_ZeroViewComponent()
        {
            LocalizationSourceName = W1001_ABP_With_ZeroConsts.LocalizationSourceName;
        }
    }
}