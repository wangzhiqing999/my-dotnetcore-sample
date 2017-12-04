using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using A0040_React.Model;


namespace A0040_React.DataAccess
{


    public class MyReportContext : DbContext
    {

        public MyReportContext() : base()
        {
        }

        public MyReportContext(DbContextOptions<MyReportContext> options) : base(options)
        {
        }


        /// <summary>
        /// 报表
        /// </summary>
        public DbSet<Report> Reports { get; set; }


        /// <summary>
        /// 自动报表.
        /// </summary>
        public DbSet<AutoReportMaster> AutoReportMasters { get; set; }

        /// <summary>
        /// 自动报表明细.
        /// </summary>
        public DbSet<AutoReportDetail> AutoReportDetails { get; set; }

        /// <summary>
        /// 自动报表明细文件.
        /// </summary>
        public DbSet<AutoReportDetailFile> AutoReportDetailFiles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    // optionsBuilder.UseInMemoryDatabase("TestReport");
                    optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=TestReport; Integrated Security=True;");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 对于一个 【自动报表明细文件】.
            modelBuilder.Entity<AutoReportDetailFile>()
                        // 有一个 【自动报表明细】.
                        .HasOne(s => s.AutoReportDetailData)
                        // 一个 【自动报表明细】，有多个 【自动报表明细文件】
                        .WithMany(m => m.AutoReportDetailFileList)
                        // 外键.
                        .HasForeignKey(f => f.AutoReportDetailID);


            // 对于一个 【自动报表明细】.
            modelBuilder.Entity<AutoReportDetail>()
                        // 有一个 【自动报表】.
                        .HasOne(s => s.AutoReportMasterData)
                        // 一个 【自动报表】，有多个 【自动报表明细】
                        .WithMany(m => m.AutoReportDetailList)
                        // 外键.
                        .HasForeignKey(f => f.AutoReportMasterID);


            // 对于一个 【自动报表明细】.
            modelBuilder.Entity<AutoReportDetail>()
                        // 有一个 【报表】.
                        .HasOne(s => s.ReportData)
                        // 一个 【报表】，有多个 【自动报表明细】
                        .WithMany(m => m.AutoReportDetailList)
                        // 外键.
                        .HasForeignKey(f => f.ReportID);
        }




    }


}
