using Abp.AspNetCore.Mvc.ViewComponents;

namespace W1010_ABP_NetCode2.Web.Views
{
    public abstract class W1010_ABP_NetCode2ViewComponent : AbpViewComponent
    {
        protected W1010_ABP_NetCode2ViewComponent()
        {
            LocalizationSourceName = W1010_ABP_NetCode2Consts.LocalizationSourceName;
        }
    }
}
