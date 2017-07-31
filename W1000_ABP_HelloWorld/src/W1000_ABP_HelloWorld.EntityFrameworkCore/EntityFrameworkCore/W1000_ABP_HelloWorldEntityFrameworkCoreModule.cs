using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace W1000_ABP_HelloWorld.EntityFrameworkCore
{
    [DependsOn(
        typeof(W1000_ABP_HelloWorldCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class W1000_ABP_HelloWorldEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1000_ABP_HelloWorldEntityFrameworkCoreModule).GetAssembly());
        }
    }
}