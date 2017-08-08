using System;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Configuration.Startup;
using Abp.Net.Mail;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Abp.Zero.EntityFrameworkCore;
using W1001_ABP_With_Zero.EntityFrameworkCore;
using W1001_ABP_With_Zero.Tests.DependencyInjection;
using Castle.MicroKernel.Registration;
using NSubstitute;

namespace W1001_ABP_With_Zero.Tests
{
    [DependsOn(
        typeof(W1001_ABP_With_ZeroApplicationModule),
        typeof(W1001_ABP_With_ZeroEntityFrameworkModule),
        typeof(AbpTestBaseModule)
        )]
    public class W1001_ABP_With_ZeroTestModule : AbpModule
    {
        public W1001_ABP_With_ZeroTestModule(W1001_ABP_With_ZeroEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        }

        public override void PreInitialize()
        {
            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);

            //Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            //Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            RegisterFakeService<AbpZeroDbMigrator<W1001_ABP_With_ZeroDbContext>>();

            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            ServiceCollectionRegistrar.Register(IocManager);
        }

        private void RegisterFakeService<TService>() where TService : class
        {
            IocManager.IocContainer.Register(
                Component.For<TService>()
                    .UsingFactoryMethod(() => Substitute.For<TService>())
                    .LifestyleSingleton()
            );
        }
    }
}