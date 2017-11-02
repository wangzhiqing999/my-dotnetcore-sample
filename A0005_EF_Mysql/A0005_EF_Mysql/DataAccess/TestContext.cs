using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.EntityFrameworkCore;


using A0005_EF_Mysql.Model;


namespace A0005_EF_Mysql.DataAccess
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
                    optionsBuilder.UseMySQL(@"Server=172.19.30.57;Database=test2;Uid=test;Pwd=123456;CharSet=utf8");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }

    }
}
