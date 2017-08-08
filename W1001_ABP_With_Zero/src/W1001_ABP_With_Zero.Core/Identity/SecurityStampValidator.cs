using Abp.Authorization;
using W1001_ABP_With_Zero.Authorization.Roles;
using W1001_ABP_With_Zero.Authorization.Users;
using W1001_ABP_With_Zero.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace W1001_ABP_With_Zero.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<IdentityOptions> options, 
            SignInManager signInManager) 
            : base(options, signInManager)
        {
        }
    }
}