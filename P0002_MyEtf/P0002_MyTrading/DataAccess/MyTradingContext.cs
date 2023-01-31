using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using P0002_MyTrading.Model;



namespace P0002_MyTrading.DataAccess
{
    public class MyTradingContext : DbContext
    {

        public MyTradingContext() : base()
        {
        }

        public MyTradingContext(DbContextOptions<MyTradingContext> options) : base(options)
        {
        }



        /// <summary>
        /// 简单交易记录.
        /// </summary>
        public DbSet<SimpleTrading> SimpleTradings { get; set; }



        /// <summary>
        /// 持仓.
        /// </summary>
        public DbSet<Holding> Holdings { get; set; }


        /// <summary>
        /// 持仓日志.
        /// </summary>
        public DbSet<HoldingLog> HoldingLogs { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    // 注意：
                    // 下面的两行代码，或者说两个参数.
                    // 第一行是指定 默认的 pgsql 数据库连接字符串.
                    // 第二行是指定 "__EFMigrationsHistory" 表使用什么TableName，什么SchemaName.
                    // 第二行如果不写的话，"__EFMigrationsHistory" 表将会创建到“数据库默认的Schema”里面
                    optionsBuilder.UseNpgsql(
                        @"Server=pve001;Port=5432;User ID=postgres;Password=123456;Database=postgres;",
                        x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_trading"));
                }
                base.OnConfiguring(optionsBuilder);
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // 持仓日志
            modelBuilder.Entity<HoldingLog>()
                // 有一个 持仓主数据.
                .HasOne(s => s.HoldingData)
                // 一个 持仓主数据，有多个 持仓日志
                .WithMany(m => m.HoldingLogList)
                // 外键.
                .HasForeignKey(f => f.ItemCode);
        }





        // Add-Migration MyTradingInit
        // Script-Migration
    }
}
