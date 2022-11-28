using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


using A0005_EF_Mysql_V6.Model;
using MySql.EntityFrameworkCore.Extensions;

namespace A0005_EF_Mysql_V6.DataAccess
{
    internal class TestContext : DbContext
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





        #region 用于测试 两个一对多，形成一个 多对多的 3个表.


        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<MrRole> MrRoles { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<MrUser> MrUsers { get; set; }


        /// <summary>
        /// 用户-角色关系
        /// </summary>
        public DbSet<MrUserRole> MrUserRoles { get; set; }


        #endregion 用于测试 两个一对多，形成一个 多对多的 3个表.





        #region 一对一的表，  其中. 必选的表是前面多对多的  MrUser，  可选的表是这里的 MrUserExp


        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<MrUserExp> MrUserExps { get; set; }


        #endregion





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseMySQL(connectionString: @"Server=pve002;Database=test2;Uid=root;Pwd=123456;CharSet=utf8");
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



            // 用于测试 两个一对多，形成一个 多对多的 3个表.

            // 中间表是 复合主键.
            modelBuilder.Entity<MrUserRole>()
                        .HasKey(t => new { t.UserCode, t.RoleCode });


            // 对于一个 用户-角色关系
            modelBuilder.Entity<MrUserRole>()
                        // 有一个 用户.
                        .HasOne(s => s.User)
                        // 一个用户，允许有多个 用户-角色关系
                        .WithMany(m => m.UserRoles)
                        // 外键.
                        .HasForeignKey(f => f.UserCode);

            // 对于一个 用户-角色关系
            modelBuilder.Entity<MrUserRole>()
                        // 有一个 角色.
                        .HasOne(s => s.Role)
                        // 一个角色，允许有多个 用户-角色关系
                        .WithMany(m => m.UserRoles)
                        // 外键.
                        .HasForeignKey(f => f.RoleCode);



            // 尝试设置字符串的字符集，失败了。
            // Add-Migration 之后，生成的代码，编译不过。
            /*
            modelBuilder.Entity<MrUser>(e =>
            {
                e.Property(p => p.UserName).ForMySQLHasCharset("utf8mb4"); // defining charset in a property
            });
            */

        }


    }
}
