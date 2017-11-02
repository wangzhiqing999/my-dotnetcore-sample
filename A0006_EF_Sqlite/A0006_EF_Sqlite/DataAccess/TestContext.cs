using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.EntityFrameworkCore;


using A0006_EF_Sqlite.Model;


namespace A0006_EF_Sqlite.DataAccess
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

    }
}
