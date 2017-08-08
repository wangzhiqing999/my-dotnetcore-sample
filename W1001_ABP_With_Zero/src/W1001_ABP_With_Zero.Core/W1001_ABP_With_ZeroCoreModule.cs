using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using W1001_ABP_With_Zero.Localization;
using Abp.Zero.Configuration;
using W1001_ABP_With_Zero.MultiTenancy;
using W1001_ABP_With_Zero.Authorization.Roles;
using W1001_ABP_With_Zero.Authorization.Users;
using W1001_ABP_With_Zero.Configuration;
using W1001_ABP_With_Zero.Timing;

namespace W1001_ABP_With_Zero
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class W1001_ABP_With_ZeroCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            W1001_ABP_With_ZeroLocalizationConfigurer.Configure(Configuration.Localization);

            //Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = W1001_ABP_With_ZeroConsts.MultiTenancyEnabled;

            //Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1001_ABP_With_ZeroCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}