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