using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace W1000_ABP_HelloWorld
{
    [DependsOn(
        typeof(W1000_ABP_HelloWorldCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class W1000_ABP_HelloWorldApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1000_ABP_HelloWorldApplicationModule).GetAssembly());
        }
    }
}