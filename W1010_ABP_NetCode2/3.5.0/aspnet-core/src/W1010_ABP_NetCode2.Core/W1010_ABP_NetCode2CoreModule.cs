using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using W1010_ABP_NetCode2.Authorization.Roles;
using W1010_ABP_NetCode2.Authorization.Users;
using W1010_ABP_NetCode2.Configuration;
using W1010_ABP_NetCode2.Localization;
using W1010_ABP_NetCode2.MultiTenancy;
using W1010_ABP_NetCode2.Timing;

namespace W1010_ABP_NetCode2
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class W1010_ABP_NetCode2CoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            W1010_ABP_NetCode2LocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = W1010_ABP_NetCode2Consts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1010_ABP_NetCode2CoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
