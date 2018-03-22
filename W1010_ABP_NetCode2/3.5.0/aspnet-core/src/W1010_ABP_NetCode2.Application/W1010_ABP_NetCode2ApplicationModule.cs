using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using W1010_ABP_NetCode2.Authorization;

namespace W1010_ABP_NetCode2
{
    [DependsOn(
        typeof(W1010_ABP_NetCode2CoreModule), 
        typeof(AbpAutoMapperModule))]
    public class W1010_ABP_NetCode2ApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<W1010_ABP_NetCode2AuthorizationProvider>();

            Configuration.MultiTenancy.IsEnabled = true;
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(W1010_ABP_NetCode2ApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
