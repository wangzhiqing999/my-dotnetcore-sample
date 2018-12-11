using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.EntityFrameworkCore;


using A0007_EF_QueryFilter.Model;


namespace A0007_EF_QueryFilter.DataAccess
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




        #region 用于测试 一对多 的2个表.

        /// <summary>
        /// 文档类型.
        /// </summary>
        public DbSet<DocumentType> DocumentTypes { get; set; }

        /// <summary>
        /// 文档.
        /// </summary>
        public DbSet<Document> Documents { get; set; }

        #endregion 用于测试 一对多 的2个表.






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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 用于测试 一对多 的2个表.
            // 对于一个文档.
            modelBuilder.Entity<Document>()
                        // 有一个文档类型.
                        .HasOne(s => s.DocumentType)
                        // 一个文档类型，有多个文档
                        .WithMany(m => m.DocumentList)
                        // 外键.
                        .HasForeignKey(f => f.DocumentTypeCode);


            // 查询的时候， 自动增加的查询条件， 也就是排除掉 逻辑删除的数据.
            modelBuilder.Entity<DocumentType>().HasQueryFilter(e => e.Status == CommonData.STATUS_IS_ACTIVE);
            modelBuilder.Entity<Document>().HasQueryFilter(e => e.Status == CommonData.STATUS_IS_ACTIVE);

        }



    }
}
