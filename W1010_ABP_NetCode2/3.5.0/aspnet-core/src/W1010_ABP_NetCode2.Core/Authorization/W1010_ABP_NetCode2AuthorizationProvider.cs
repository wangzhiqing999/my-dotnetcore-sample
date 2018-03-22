using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace W1010_ABP_NetCode2.Authorization
{
    public class W1010_ABP_NetCode2AuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);


            context.CreatePermission(PermissionNames.Pages_Tasks, L("Tasks"));
            context.CreatePermission(PermissionNames.Pages_Others, L("Others"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, W1010_ABP_NetCode2Consts.LocalizationSourceName);
        }
    }
}
