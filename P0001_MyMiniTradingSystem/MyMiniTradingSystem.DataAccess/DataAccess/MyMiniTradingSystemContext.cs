using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.EntityFrameworkCore;

using MyMiniTradingSystem.Model;


namespace MyMiniTradingSystem.DataAccess
{

    // Enable-Migrations -ProjectName MyMiniTradingSystem.DataAccess  -EnableAutomaticMigrations
    public class MyMiniTradingSystemContext : DbContext
    {


        public MyMiniTradingSystemContext() : base()
        {
        }

        public MyMiniTradingSystemContext(DbContextOptions<MyMiniTradingSystemContext> options) : base(options)
        {
        }



        /// <summary>
        /// 用户帐户.
        /// </summary>
        public DbSet<UserAccount> UserAccounts { get; set; }

        /// <summary>
        /// 交易商品.
        /// </summary>
        public DbSet<TradableCommodity> TradableCommoditys { get; set; }


        /// <summary>
        /// 商品行情(日).
        /// </summary>
        public DbSet<CommodityPrice> CommodityPrices { get; set; }


        /// <summary>
        /// 仓位.
        /// </summary>
        public DbSet<Position> Positions { get; set; }


        /// <summary>
        /// 每日小结
        /// </summary>
        public DbSet<DailySummary> DailySummarys { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlite(@"Data Source=MyMiniTradingSystem.db");
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



            // ##########  商品行情 

            // 复合主键.
            modelBuilder.Entity<CommodityPrice>().HasKey(p => new { p.CommodityCode, p.TradingStartDate });
            // 开盘.
            modelBuilder.Entity<CommodityPrice>().Property(p => p.OpenPrice).HasColumnType("decimal(10, 3)");
            // 收盘
            modelBuilder.Entity<CommodityPrice>().Property(p => p.ClosePrice).HasColumnType("decimal(10, 3)");
            // 最高.
            modelBuilder.Entity<CommodityPrice>().Property(p => p.HighestPrice).HasColumnType("decimal(10, 3)");
            // 最低.
            modelBuilder.Entity<CommodityPrice>().Property(p => p.LowestPrice).HasColumnType("decimal(10, 3)");

            modelBuilder.Entity<CommodityPrice>().Property(p => p.Tr).HasColumnType("decimal(10, 3)");
            modelBuilder.Entity<CommodityPrice>().Property(p => p.Atr).HasColumnType("decimal(10, 3)");



            // 商品行情 -- 产品.
            modelBuilder.Entity<CommodityPrice>()
                // 归属于一个产品.
                .HasOne(s => s.TradableCommodityData)
                // 一个 产品  有 多个 商品行情.
                .WithMany(m => m.CommodityPrices)
                // 外键的列是  CommodityCode
                .HasForeignKey(m => m.CommodityCode);



            // ########## 仓位

            // 仓位 -- 账户.
            modelBuilder.Entity<Position>()
                // 归属于一个账户.
                .HasOne(s => s.UserAccountData)
                // 一个 账户  有 多个 仓位.
                .WithMany(m => m.Positions)
                // 外键的列是  UserCode
                .HasForeignKey(m => m.UserCode);

            // 仓位 -- 产品.
            modelBuilder.Entity<Position>()
                // 归属于一个产品.
                .HasOne(s => s.TradableCommodityData)
                // 一个 产品  有 多个 仓位.
                .WithMany(m => m.Positions)
                // 外键的列是  CommodityCode
                .HasForeignKey(m => m.CommodityCode);





            // ########## 每日总结

            // 收盘
            modelBuilder.Entity<DailySummary>().Property(p => p.ClosePrice).HasColumnType("decimal(10, 3)");

            // 持仓市值
            modelBuilder.Entity<DailySummary>().Property(p => p.PositionValue).HasColumnType("decimal(10, 3)");


            // 每日总结 -- 账户.
            modelBuilder.Entity<DailySummary>()
                // 归属于一个账户.
                .HasOne(s => s.UserAccountData)
                // 一个 账户  有 多个 每日总结.
                .WithMany(m => m.DailySummarys)
                // 外键的列是  UserCode
                .HasForeignKey(m => m.UserCode);


            // 每日总结 -- 产品.
            modelBuilder.Entity<DailySummary>()
                // 归属于一个产品.
                .HasOne(s => s.PositionTradableCommodity)
                // 一个 产品  有 多个 每日总结.
                .WithMany(m => m.DailySummarys)
                // 外键的列是  PositionCommodityCode
                .HasForeignKey(m => m.PositionCommodityCode);

        }


    }


}
