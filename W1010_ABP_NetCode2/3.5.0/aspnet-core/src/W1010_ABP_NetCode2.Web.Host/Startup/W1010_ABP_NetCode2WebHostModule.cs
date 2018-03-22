using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using W1010_ABP_NetCode2.Configuration;

namespace W1010_ABP_NetCode2.Web.Host.Startup
{
    [DependsOn(
       typeof(W1010_ABP_NetCode2WebCoreModule))]
    public class W1010_ABP_NetCode2WebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public W1010_ABP_NetCode2WebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1010_ABP_NetCode2WebHostModule).GetAssembly());
        }
    }
}
