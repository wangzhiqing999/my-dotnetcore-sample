using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

using Microsoft.EntityFrameworkCore;

using MyWork.Model;


namespace MyWork.DataAccess
{
    public class MyWorkContext : DbContext
    {

        public MyWorkContext() : base()
        {
        }

        public MyWorkContext(DbContextOptions<MyWorkContext> options) : base(options)
        {
        }



        /// <summary>
        /// 股票池.
        /// </summary>
        public DbSet<StockPool> StockPools { get; set; }

        /// <summary>
        /// 股票.
        /// </summary>
        public DbSet<StockInfo> StockInfos { get; set; }

        /// <summary>
        /// 股票池 -- 股票  多对多 中间表.
        /// </summary>
        public DbSet<StockPoolDetail> StockPoolDetails { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {

                IConfigurationBuilder builder = new ConfigurationBuilder();
                JsonConfigurationSource source = new JsonConfigurationSource();
                source.Build(builder);
                source.Path = "appsettings.json";
                JsonConfigurationProvider provider = new JsonConfigurationProvider(source);
                provider.Load();


                // 数据库连接字符串，定义在 appsettings.json 文件中.
                string connString = null;
                provider.TryGet("ConnectionStrings:MyWorkConnection", out connString);

                optionsBuilder.UseSqlServer(connString);
            }
            base.OnConfiguring(optionsBuilder);
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ------ 股票池 -- 股票 多对多关系.
            // 股票池 -- 股票 多对多 中间表 :  复合主键.
            modelBuilder.Entity<StockPoolDetail>()
                        .HasKey(t => new { t.StockPoolID, t.StockCode });
            // 对于一个 股票池 -- 股票 多对多 中间表
            modelBuilder.Entity<StockPoolDetail>()
                        // 有一个 股票池.
                        .HasOne(s => s.Pool)
                        // 一个股票池，允许有多个 股票池 -- 股票 多对多 关系
                        .WithMany(m => m.StockPoolDetails)
                        // 外键.
                        .HasForeignKey(f => f.StockPoolID)
                        // 删除股票池时，如果股票池存在有 股票池 -- 股票 关系， 自动删除.
                        .OnDelete(DeleteBehavior.Cascade);

            // 对于一个 股票池 -- 股票 多对多 中间表
            modelBuilder.Entity<StockPoolDetail>()
                        // 有一个 股票.
                        .HasOne(s => s.Stock)
                        // 一个股票，允许有多个  股票池 -- 股票 多对多 关系
                        .WithMany(m => m.StockPoolDetails)
                        // 外键.
                        .HasForeignKey(f => f.StockCode)
                        // 删除股票时，如果用户存在有 系统 - 用户 关系， 自动删除.
                        .OnDelete(DeleteBehavior.Cascade);
        }




    }
}
