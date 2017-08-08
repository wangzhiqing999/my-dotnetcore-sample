using Abp.Authorization;
using W1001_ABP_With_Zero.Authorization.Roles;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace W1001_ABP_With_Zero.Authorization.Users
{
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            RoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor)
        {
        }
    }
}
