using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using W1000_ABP_HelloWorld.Configuration;
using W1000_ABP_HelloWorld.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace W1000_ABP_HelloWorld.Web.Startup
{
    [DependsOn(
        typeof(W1000_ABP_HelloWorldApplicationModule), 
        typeof(W1000_ABP_HelloWorldEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class W1000_ABP_HelloWorldWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public W1000_ABP_HelloWorldWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(W1000_ABP_HelloWorldConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<W1000_ABP_HelloWorldNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(W1000_ABP_HelloWorldApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1000_ABP_HelloWorldWebModule).GetAssembly());
        }
    }
}