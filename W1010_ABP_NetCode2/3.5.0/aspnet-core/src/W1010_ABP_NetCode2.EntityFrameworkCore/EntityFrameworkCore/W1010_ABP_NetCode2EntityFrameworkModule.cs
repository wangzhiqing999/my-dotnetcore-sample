using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using W1010_ABP_NetCode2.EntityFrameworkCore.Seed;

namespace W1010_ABP_NetCode2.EntityFrameworkCore
{
    [DependsOn(
        typeof(W1010_ABP_NetCode2CoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class W1010_ABP_NetCode2EntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<W1010_ABP_NetCode2DbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        W1010_ABP_NetCode2DbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        W1010_ABP_NetCode2DbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(W1010_ABP_NetCode2EntityFrameworkModule).GetAssembly());
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
