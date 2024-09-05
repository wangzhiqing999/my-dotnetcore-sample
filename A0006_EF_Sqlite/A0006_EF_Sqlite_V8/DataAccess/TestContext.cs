using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Microsoft.EntityFrameworkCore;


using A0006_EF_Sqlite_V8.Model;
using A0006_EF_Sqlite_V8.TypeConverters;



namespace A0006_EF_Sqlite_V8.DataAccess
{
    public class TestContext : DbContext
    {

        public TestContext() : base()
        {
        }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }





        /// <summary>
        /// 测试数据.
        /// </summary>
        public DbSet<TestData> TestDatas { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlite(@"Data Source=test.db");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // 对于 JsonDocument 列， 指定转换方式.
            modelBuilder.Entity<TestData>()
                .Property(p => p.DetailData)
                .HasConversion(new JsonDocumentToStringConverter())
                .HasColumnType("TEXT")
                .IsRequired();



            // 对于 DateTime 列， 指定转换方式.
            modelBuilder.Entity<TestData>()
                .Property(p => p.CreatedTime)
                .HasConversion(new DateTimeToStringConverter())
                .HasColumnType("TEXT")
                .HasMaxLength(16)
                .IsRequired();

        }

    }
}
