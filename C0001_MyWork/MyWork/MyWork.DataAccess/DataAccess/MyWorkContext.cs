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


        #region 股票基本信息.

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


        #endregion




        #region 账户基本信息.


        /// <summary>
        /// 账户.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// 账户操作类型.
        /// </summary>
        //public DbSet<AccountOperationType> AccountOperationTypes { get; set; }

        /// <summary>
        /// 账户操作日志.
        /// </summary>
        public DbSet<AccountOperationLog> AccountOperationLogs { get; set; }

        /// <summary>
        /// 日结报表.
        /// </summary>
        public DbSet<AccountDailyReport> AccountDailyReports { get; set; }


        #endregion



        #region 交易/持仓信息.

        /// <summary>
        /// 交易.
        /// </summary>
        public DbSet<Trading> Tradings { get; set; }


        /// <summary>
        /// 持仓.
        /// </summary>
        public DbSet<Position> Positions { get; set; }


        /// <summary>
        /// 每日总结.
        /// </summary>
        public DbSet<DailySummary> DailySummarys { get; set; }


        #endregion





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
                        // 删除股票时，如果股票存在有 股票池 -- 股票 关系， 自动删除.
                        .OnDelete(DeleteBehavior.Cascade);



            // ----- 账户表 时间戳
            modelBuilder.Entity<Account>().Property("RowVersion").IsRowVersion();


            // ----- 账户 -- 账户操作日志 一对多关系.
            // 对于一个 账户操作日志
            modelBuilder.Entity<AccountOperationLog>()
                        // 有一个 账户.
                        .HasOne(s => s.AccountData)
                        // 一个账户， 允许有多个 账户操作日志
                        .WithMany(m => m.OperationLogList)
                        // 外键.
                        .HasForeignKey(f => f.AccountID)
                        // 删除账户的情况下， 如果存在有操作日志. 那么拒绝操作
                        .OnDelete(DeleteBehavior.Restrict);

            // ----- 账户 -- 日结报表 一对多关系.
            // 对于一个 日结报表.
            modelBuilder.Entity<AccountDailyReport>()
                        // 有一个 账户.
                        .HasOne(s => s.AccountData)
                        // 一个账户， 允许有多个 日结报表
                        .WithMany(m => m.DailyReportList)
                        // 外键.
                        .HasForeignKey(f => f.AccountID)
                        // 删除账户的情况下， 如果存在有日结报表. 那么拒绝操作
                        .OnDelete(DeleteBehavior.Restrict);



            // ----- 账户操作类型 -- 账户操作日志 一对多关系.
            // 对于一个 账户操作日志
            //modelBuilder.Entity<AccountOperationLog>()
            //            // 有一个 账户操作类型.
            //            .HasOne(s => s.OperationType)
            //            // 一个账户操作类型， 允许有多个 账户操作日志
            //            .WithMany(m => m.OperationLogList)
            //            // 外键.
            //            .HasForeignKey(f => f.OperationTypeCode)
            //            // 删除账户操作类型的情况下， 如果存在有操作日志. 那么拒绝操作
            //            .OnDelete(DeleteBehavior.Restrict);




            // ----- 账户 -- 交易 一对多关系.
            // 对于一个 交易
            modelBuilder.Entity<Trading>()
                        // 有一个 账户.
                        .HasOne(s => s.AccountData)
                        // 一个账户， 允许有多个 交易
                        .WithMany(m => m.TradingList)
                        // 外键.
                        .HasForeignKey(f => f.AccountID)
                        // 删除账户的情况下， 如果存在有交易. 那么拒绝操作
                        .OnDelete(DeleteBehavior.Restrict);

            // ----- 股票 -- 交易 一对多关系.
            // 对于一个 交易
            modelBuilder.Entity<Trading>()
                        // 有一个 股票.
                        .HasOne(s => s.StockInfoData)
                        // 一个股票， 允许有多个 交易
                        .WithMany(m => m.TradingList)
                        // 外键.
                        .HasForeignKey(f => f.StockCode)
                        // 删除股票的情况下， 如果存在有交易. 那么拒绝操作
                        .OnDelete(DeleteBehavior.Restrict);








            // ----- 账户 -- 持仓 一对多关系.
            // 对于一个 持仓
            modelBuilder.Entity<Position>()
                        // 有一个 账户.
                        .HasOne(s => s.AccountData)
                        // 一个账户， 允许有多个 持仓
                        .WithMany(m => m.PositionList)
                        // 外键.
                        .HasForeignKey(f => f.AccountID)
                        // 删除账户的情况下， 如果存在有持仓. 那么拒绝操作
                        .OnDelete(DeleteBehavior.Restrict);

            // ----- 股票 -- 持仓 一对多关系.
            // 对于一个 持仓
            modelBuilder.Entity<Position>()
                        // 有一个 股票.
                        .HasOne(s => s.StockInfoData)
                        // 一个股票， 允许有多个 持仓
                        .WithMany(m => m.PositionList)
                        // 外键.
                        .HasForeignKey(f => f.StockCode)
                        // 删除股票的情况下， 如果存在有持仓. 那么拒绝操作
                        .OnDelete(DeleteBehavior.Restrict);




            // ----- 账户 -- 每日总结 一对多关系.
            // 对于一个 每日总结
            modelBuilder.Entity<DailySummary>()
                        // 有一个 账户.
                        .HasOne(s => s.AccountData)
                        // 一个账户， 允许有多个 每日总结
                        .WithMany(m => m.DailySummaryList)
                        // 外键.
                        .HasForeignKey(f => f.AccountID)
                        // 删除账户的情况下， 如果存在有每日总结. 那么拒绝操作
                        .OnDelete(DeleteBehavior.Restrict);

            // ----- 股票 -- 每日总结 一对多关系.
            // 对于一个 每日总结
            modelBuilder.Entity<DailySummary>()
                        // 有一个 股票.
                        .HasOne(s => s.StockInfoData)
                        // 一个股票， 允许有多个 每日总结
                        .WithMany(m => m.DailySummaryList)
                        // 外键.
                        .HasForeignKey(f => f.StockCode)
                        // 删除股票的情况下， 如果存在有每日总结. 那么拒绝操作
                        .OnDelete(DeleteBehavior.Restrict);

        }




    }
}
