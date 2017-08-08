using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using W1001_ABP_With_Zero.MultiTenancy;
using Abp.Runtime.Session;
using Abp.IdentityFramework;
using W1001_ABP_With_Zero.Authorization.Users;
using Microsoft.AspNetCore.Identity;

namespace W1001_ABP_With_Zero
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class W1001_ABP_With_ZeroAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected W1001_ABP_With_ZeroAppServiceBase()
        {
            LocalizationSourceName = W1001_ABP_With_ZeroConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}