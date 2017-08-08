using W1001_ABP_With_Zero.Authorization;
using W1001_ABP_With_Zero.Authorization.Roles;
using W1001_ABP_With_Zero.Authorization.Users;
using W1001_ABP_With_Zero.Editions;
using W1001_ABP_With_Zero.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;

namespace W1001_ABP_With_Zero.Identity
{
    public static class IdentityRegistrar
    {
        public static void Register(IServiceCollection services)
        {
            services.AddLogging();

            services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();
        }
    }
}
