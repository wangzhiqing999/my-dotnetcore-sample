using Abp.AspNetCore.Mvc.Controllers;

namespace W1000_ABP_HelloWorld.Web.Controllers
{
    public abstract class W1000_ABP_HelloWorldControllerBase: AbpController
    {
        protected W1000_ABP_HelloWorldControllerBase()
        {
            LocalizationSourceName = W1000_ABP_HelloWorldConsts.LocalizationSourceName;
        }
    }
}