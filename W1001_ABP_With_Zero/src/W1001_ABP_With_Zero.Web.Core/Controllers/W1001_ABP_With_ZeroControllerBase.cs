using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace W1001_ABP_With_Zero.Controllers
{
    public abstract class W1001_ABP_With_ZeroControllerBase: AbpController
    {
        protected W1001_ABP_With_ZeroControllerBase()
        {
            LocalizationSourceName = W1001_ABP_With_ZeroConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}