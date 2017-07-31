using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


using W1000_ABP_HelloWorld.SystemConfig;




namespace W1000_ABP_HelloWorld.EntityFrameworkCore
{
    public class W1000_ABP_HelloWorldDbContext : AbpDbContext
    {

        //Add DbSet properties for your entities...


        /// <summary>
        /// 系统配置类型
        /// </summary>
        public DbSet<SystemConfigType> SystemConfigTypes { get; set; }


        /// <summary>
        /// 系统配置属性
        /// </summary>
        public DbSet<SystemConfigProperty> SystemConfigPropertys { get; set; }


        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<SystemConfigValue> SystemConfigValues { get; set; }



        public W1000_ABP_HelloWorldDbContext(DbContextOptions<W1000_ABP_HelloWorldDbContext> options)
            : base(options)
        {

        }



        // Add-Migration Init
        // Update-Database
    }
}
