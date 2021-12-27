using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using P0002_MyEtf.Model;


namespace P0002_MyEtf.DataAccess
{
    public class MyEtfContext : DbContext
    {
        public MyEtfContext() : base()
        {
        }

        public MyEtfContext(DbContextOptions<MyEtfContext> options) : base(options)
        {
        }



        /// <summary>
        /// ETF主数据.
        /// </summary>
        public DbSet<EtfMaster> EtfMasters { get; set; }





        /// <summary>
        /// ETF日线.
        /// </summary>
        public DbSet<EtfDayLine> EtfDayLines { get; set; }

        /// <summary>
        /// ETF日波幅.
        /// </summary>
        public DbSet<EtfDayTr> EtfDayTrs { get; set; }

        /// <summary>
        /// ETF 日 EMA.
        /// </summary>
        public DbSet<EtfDayEma> EtfDayEmas { get; set; }

        /// <summary>
        /// ETF 日 MACD.
        /// </summary>
        public DbSet<EtfDayMacd> EtfDayMacds { get; set; }






        /// <summary>
        /// ETF周线.
        /// </summary>
        public DbSet<EtfWeekLine> EtfWeekLines { get; set; }

        /// <summary>
        /// ETF 周 EMA.
        /// </summary>
        public DbSet<EtfWeekEma> EtfWeekEmas { get; set; }

        /// <summary>
        /// ETF 周 MACD.
        /// </summary>
        public DbSet<EtfWeekMacd> EtfWeekMacds { get; set; }




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
                        @"Server=192.168.1.153;Port=5432;User ID=postgres;Password=123456;Database=postgres;",
                        x=>x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_etf"));
                }
                base.OnConfiguring(optionsBuilder);
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ETF主数据 -- ETF日线 一对多
            // 对于一个 ETF日线
            modelBuilder.Entity<EtfDayLine>()
                        // 有一个 ETF主数据.
                        .HasOne(s => s.EtfMasterData)
                        // 一个 ETF主数据，有多个 ETF日线
                        .WithMany(m => m.EtfDayLineList)
                        // 外键.
                        .HasForeignKey(f => f.EtfCode);

            // ETF日线 是 复合主键.
            modelBuilder.Entity<EtfDayLine>()
                        .HasKey(t => new { t.EtfCode, t.TradingDate });


            // ETF主数据 -- ETF日波幅 一对多
            // 对于一个 ETF日波幅
            modelBuilder.Entity<EtfDayTr>()
                        // 有一个 ETF主数据.
                        .HasOne(s => s.EtfMasterData)
                        // 一个 ETF主数据，有多个 ETF日波幅
                        .WithMany(m => m.EtfDayTrList)
                        // 外键.
                        .HasForeignKey(f => f.EtfCode);

            // ETF日波幅 是 复合主键.
            modelBuilder.Entity<EtfDayTr>()
                        .HasKey(t => new { t.EtfCode, t.TradingDate });


            // ETF主数据 -- ETF日EMA 一对多
            // 对于一个 ETF日EMA
            modelBuilder.Entity<EtfDayEma>()
                        // 有一个 ETF主数据.
                        .HasOne(s => s.EtfMasterData)
                        // 一个 ETF主数据，有多个 ETF日EMA
                        .WithMany(m => m.EtfDayEmaList)
                        // 外键.
                        .HasForeignKey(f => f.EtfCode);

            // ETF日EMA 是 复合主键.
            modelBuilder.Entity<EtfDayEma>()
                        .HasKey(t => new { t.EtfCode, t.TradingDate });


            // ETF主数据 -- ETF日MACD 一对多
            // 对于一个 ETF日MACD
            modelBuilder.Entity<EtfDayMacd>()
                        // 有一个 ETF主数据.
                        .HasOne(s => s.EtfMasterData)
                        // 一个 ETF主数据，有多个 ETF日MACD
                        .WithMany(m => m.EtfDayMacdList)
                        // 外键.
                        .HasForeignKey(f => f.EtfCode);

            // ETF日MACD 是 复合主键.
            modelBuilder.Entity<EtfDayMacd>()
                        .HasKey(t => new { t.EtfCode, t.TradingDate });









            // ETF主数据 -- ETF周线 一对多
            // 对于一个 ETF周线
            modelBuilder.Entity<EtfWeekLine>()
                        // 有一个 ETF主数据.
                        .HasOne(s => s.EtfMasterData)
                        // 一个 ETF主数据，有多个 ETF周线
                        .WithMany(m => m.EtfWeekLineList)
                        // 外键.
                        .HasForeignKey(f => f.EtfCode);

            // ETF周线 是 复合主键.
            modelBuilder.Entity<EtfWeekLine>()
                        .HasKey(t => new { t.EtfCode, t.TradingDate });




            // ETF主数据 -- ETF周EMA 一对多
            // 对于一个 ETF周EMA
            modelBuilder.Entity<EtfWeekEma>()
                        // 有一个 ETF主数据.
                        .HasOne(s => s.EtfMasterData)
                        // 一个 ETF主数据，有多个 ETF周EMA
                        .WithMany(m => m.EtfWeekEmaList)
                        // 外键.
                        .HasForeignKey(f => f.EtfCode);

            // ETF周EMA 是 复合主键.
            modelBuilder.Entity<EtfWeekEma>()
                        .HasKey(t => new { t.EtfCode, t.TradingDate });



            // ETF主数据 -- ETF周MACD 一对多
            // 对于一个 ETF周MACD
            modelBuilder.Entity<EtfWeekMacd>()
                        // 有一个 ETF主数据.
                        .HasOne(s => s.EtfMasterData)
                        // 一个 ETF主数据，有多个 ETF周MACD
                        .WithMany(m => m.EtfWeekMacdList)
                        // 外键.
                        .HasForeignKey(f => f.EtfCode);

            // ETF日MACD 是 复合主键.
            modelBuilder.Entity<EtfWeekMacd>()
                        .HasKey(t => new { t.EtfCode, t.TradingDate });


        }


    }
    }
