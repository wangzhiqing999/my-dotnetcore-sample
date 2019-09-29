using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

using MySample.Model;


namespace MySample.DataAccess
{
    public class MySampleContext : DbContext
    {
        public MySampleContext() : base()
        {
        }

        public MySampleContext(DbContextOptions<MySampleContext> options) : base(options)
        {
        }


        /// <summary>
        /// 测试学校.
        /// </summary>
        public DbSet<TestSchool> TestSchools { get; set; }

        /// <summary>
        /// 测试老师.
        /// </summary>
        public DbSet<TestTeacher> TestTeachers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Test; Integrated Security=True;");
                }
                else
                {
                    base.OnConfiguring(optionsBuilder);
                }                
            }
        }



        /// <summary>
        /// 指定如何创建 表的处理.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ----- 学校 - 老师 一对多关系.
            // 对于一个 老师
            modelBuilder.Entity<TestTeacher>()
                        // 归属一个 学校.
                        .HasOne(s => s.InSchool)
                        // 一个学校，允许有多个 老师
                        .WithMany(m => m.SchoolTeachers)
                        // 外键.
                        .HasForeignKey(f => f.SchoolID)
                        // 删除学校时，如果学校下存在有老师， 拒绝删除.
                        .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
