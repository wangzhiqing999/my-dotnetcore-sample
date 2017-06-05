using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.EntityFrameworkCore;


using A0003_EF.Model;


namespace A0003_EF.DataAccess
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
                    optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Test; Integrated Security=True;");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }

    }
}
