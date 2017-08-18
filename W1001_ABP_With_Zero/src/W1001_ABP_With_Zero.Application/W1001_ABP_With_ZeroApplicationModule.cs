using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using W1001_ABP_With_Zero.Authorization;

namespace W1001_ABP_With_Zero
{
    [DependsOn(
        typeof(W1001_ABP_With_ZeroCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class W1001_ABP_With_ZeroApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<W1001_ABP_With_ZeroAuthorizationProvider>();



            // 默认多租户是被禁用的，我们需要在模块的 PreInitialize 方法中开启它
            Configuration.MultiTenancy.IsEnabled = true;

        }

        public override void Initialize()
        {
            Assembly thisAssembly = typeof(W1001_ABP_With_ZeroApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                //Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}