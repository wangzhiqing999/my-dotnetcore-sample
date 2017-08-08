using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using W1001_ABP_With_Zero.Configuration;
using W1001_ABP_With_Zero.EntityFrameworkCore;
using W1001_ABP_With_Zero.Migrator.DependencyInjection;

namespace W1001_ABP_With_Zero.Migrator
{
    [DependsOn(typeof(W1001_ABP_With_ZeroEntityFrameworkModule))]
    public class W1001_ABP_With_ZeroMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public W1001_ABP_With_ZeroMigratorModule(W1001_ABP_With_ZeroEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(W1001_ABP_With_ZeroMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                W1001_ABP_With_ZeroConsts.ConnectionStringName
                );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1001_ABP_With_ZeroMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}