using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;


using P0002_MyGrid.Model;


namespace P0002_MyGrid.DataAccess
{
    public class MyGridContext : DbContext
    {

        public MyGridContext() : base()
        {
        }

        public MyGridContext(DbContextOptions<MyGridContext> options) : base(options)
        {
        }




        /// <summary>
        /// 网格.
        /// </summary>
        public DbSet<Grid> Grids { get; set; }


        /// <summary>
        /// 网格成交.
        /// </summary>
        public DbSet<GridTransaction> GridTransactions { get; set; }




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
                        x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_grid"));
                }
                base.OnConfiguring(optionsBuilder);
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Grid>(entity =>
            {
                entity.HasKey(e => new { e.ItemCode, e.GridNo });
            });
        }



        // Add-Migration MyGridInit
    }
}
