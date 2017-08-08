using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using W1001_ABP_With_Zero.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace W1001_ABP_With_Zero.Web.Startup
{
    [DependsOn(typeof(W1001_ABP_With_ZeroWebCoreModule))]
    public class W1001_ABP_With_ZeroWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public W1001_ABP_With_ZeroWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<W1001_ABP_With_ZeroNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1001_ABP_With_ZeroWebMvcModule).GetAssembly());
        }
    }
}