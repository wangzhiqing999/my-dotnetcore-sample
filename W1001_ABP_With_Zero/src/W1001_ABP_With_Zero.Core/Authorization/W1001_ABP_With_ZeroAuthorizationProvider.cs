using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace W1001_ABP_With_Zero.Authorization
{
    public class W1001_ABP_With_ZeroAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, W1001_ABP_With_ZeroConsts.LocalizationSourceName);
        }
    }
}
