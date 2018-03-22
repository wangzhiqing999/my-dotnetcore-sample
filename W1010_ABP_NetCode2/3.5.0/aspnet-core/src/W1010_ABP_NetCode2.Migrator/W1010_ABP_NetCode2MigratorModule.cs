using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using W1010_ABP_NetCode2.Configuration;
using W1010_ABP_NetCode2.EntityFrameworkCore;
using W1010_ABP_NetCode2.Migrator.DependencyInjection;

namespace W1010_ABP_NetCode2.Migrator
{
    [DependsOn(typeof(W1010_ABP_NetCode2EntityFrameworkModule))]
    public class W1010_ABP_NetCode2MigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public W1010_ABP_NetCode2MigratorModule(W1010_ABP_NetCode2EntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(W1010_ABP_NetCode2MigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                W1010_ABP_NetCode2Consts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1010_ABP_NetCode2MigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
