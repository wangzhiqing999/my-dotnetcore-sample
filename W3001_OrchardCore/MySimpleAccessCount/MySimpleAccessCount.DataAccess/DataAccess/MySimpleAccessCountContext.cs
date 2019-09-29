using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.EntityFrameworkCore;

using MySimpleAccessCount.Model;


namespace MySimpleAccessCount.DataAccess
{
    public class MySimpleAccessCountContext : DbContext
    {


        public MySimpleAccessCountContext() : base()
        {
        }

        public MySimpleAccessCountContext(DbContextOptions<MySimpleAccessCountContext> options) : base(options)
        {
        }



        /// <summary>
        /// 页面访问计数.
        /// </summary>
        public DbSet<PageAccessCount> PageAccessCounts { get; set; }

        /// <summary>
        /// 页面每日访问计数.
        /// </summary>
        public DbSet<PageDailyAccessCount> PageDailyAccessCounts { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlite(@"Data Source=mySAC.db");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }



        /// <summary>
        /// 指定如何创建 表的处理.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 页面每日访问计数
            modelBuilder.Entity<PageDailyAccessCount>(entity =>
            {
                // 复合主键
                entity.HasKey(e => new { e.PageCode, e.AccessDate });

                // ----- 页面访问计数 - 页面每日访问计数 一对多关系.
                // 归属一个 页面访问计数.
                entity.HasOne(s => s.PageAccessCountData)
                        // 一个页面访问计数，允许有多个 页面每日访问计数
                        .WithMany(m => m.PageDailyAccessCountList)
                        // 外键.
                        .HasForeignKey(f => f.PageCode)
                        // 删除 页面访问计数 时，如果 页面访问计数 下存在有 页面每日访问计数， 拒绝删除.
                        .OnDelete(DeleteBehavior.Restrict);
            });

        }


    }
}
