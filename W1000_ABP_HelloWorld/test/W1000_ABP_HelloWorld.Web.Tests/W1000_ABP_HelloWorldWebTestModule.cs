using System.Reflection;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using W1000_ABP_HelloWorld.Web.Startup;

namespace W1000_ABP_HelloWorld.Web.Tests
{
    [DependsOn(
        typeof(W1000_ABP_HelloWorldWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class W1000_ABP_HelloWorldWebTestModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1000_ABP_HelloWorldWebTestModule).GetAssembly());
        }
    }
}