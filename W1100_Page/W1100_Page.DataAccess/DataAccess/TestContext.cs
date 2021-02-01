using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.EntityFrameworkCore;

using W1100_Page.Model;

namespace W1100_Page.DataAccess
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
        /// 财经数据.
        /// </summary>
        public DbSet<FinanceData> FinanceDatas { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=MyTest;User Id=sa;Password=123456;");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }

    }
}
