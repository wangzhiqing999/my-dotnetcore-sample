using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using W1001_ABP_With_Zero.EntityFrameworkCore.Seed;

namespace W1001_ABP_With_Zero.EntityFrameworkCore
{
    [DependsOn(
        typeof(W1001_ABP_With_ZeroCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class W1001_ABP_With_ZeroEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }


        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<W1001_ABP_With_ZeroDbContext>(configuration =>
                {
                    W1001_ABP_With_ZeroDbContextConfigurer.Configure(configuration.DbContextOptions, configuration.ConnectionString);
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1001_ABP_With_ZeroEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}