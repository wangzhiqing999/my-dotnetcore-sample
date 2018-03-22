using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace W1010_ABP_NetCode2.Controllers
{
    public abstract class W1010_ABP_NetCode2ControllerBase: AbpController
    {
        protected W1010_ABP_NetCode2ControllerBase()
        {
            LocalizationSourceName = W1010_ABP_NetCode2Consts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
