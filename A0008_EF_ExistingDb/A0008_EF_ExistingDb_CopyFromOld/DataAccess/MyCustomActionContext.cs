using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using A0008_EF_ExistingDb_CopyFromOld.Model;


namespace A0008_EF_ExistingDb_CopyFromOld.DataAccess
{


    // Enable-Migrations -ProjectName MyCustomAction.DataAccess  -EnableAutomaticMigrations
    public class MyCustomActionContext : DbContext
    {

        public MyCustomActionContext()
        {
        }

        public MyCustomActionContext(DbContextOptions<MyCustomActionContext> options)
            : base(options)
        {
        }



        /// <summary>
        /// 服务模块.
        /// </summary>
        public DbSet<ServiceModule> ServiceModules { get; set; }


        /// <summary>
        /// 客户行为.
        /// </summary>
        public DbSet<CustomAction> CustomActions { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Initial Catalog=MyCustomAction;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            }
        }



        /// <summary>
        /// 指定如何创建 表的处理.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<CustomAction>(entity =>
            {

                entity.HasOne(d => d.AccessServiceModule)
                   .WithMany(p => p.CustomActionList)
                   .HasForeignKey(d => d.ModuleCode);

            });


        }
    }
}
