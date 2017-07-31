using Abp.Modules;
using Abp.Reflection.Extensions;
using W1000_ABP_HelloWorld.Localization;

namespace W1000_ABP_HelloWorld
{
    public class W1000_ABP_HelloWorldCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            W1000_ABP_HelloWorldLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1000_ABP_HelloWorldCoreModule).GetAssembly());
        }
    }
}