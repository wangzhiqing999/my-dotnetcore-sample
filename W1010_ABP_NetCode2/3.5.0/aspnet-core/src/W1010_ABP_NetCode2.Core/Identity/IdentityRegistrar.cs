using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using W1010_ABP_NetCode2.Authorization;
using W1010_ABP_NetCode2.Authorization.Roles;
using W1010_ABP_NetCode2.Authorization.Users;
using W1010_ABP_NetCode2.Editions;
using W1010_ABP_NetCode2.MultiTenancy;

namespace W1010_ABP_NetCode2.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
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
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
