using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

using MySSO.Model;

namespace MySSO.DataAccess
{

    public class MySSOContext : DbContext
    {
        public MySSOContext() : base()
        {
        }

        public MySSOContext(DbContextOptions<MySSOContext> options) : base(options)
        {
        }


        #region 用户体系.


        /// <summary>
        /// 用户分类
        /// </summary>
        public DbSet<SystemUserCategory> SystemUserCategorys { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<SystemUser> SystemUsers { get; set; }


        /// <summary>
        /// 用户Token
        /// </summary>
        public DbSet<SystemUserToken> SystemUserTokens { get; set; }


        #endregion





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Test; Integrated Security=True;");
            }
            base.OnConfiguring(optionsBuilder);

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // 对于一个 用户.
            modelBuilder.Entity<SystemUser>()
                        // 有一个 用户分类.
                        .HasOne(s => s.UserCategory)
                        // 一个用户分类，有多个用户
                        .WithMany(m => m.SystemUserList)
                        // 外键.
                        .HasForeignKey(f => f.UserCategoryCode);

            // 对于一个 用户Token.
            modelBuilder.Entity<SystemUserToken>()
                        // 归属于一个 用户.
                        .HasOne(s => s.SystemUserData)
                        // 一个用户，允许有多个 用户Token
                        .WithMany(m => m.SystemUserTokenList)
                        // 外键.
                        .HasForeignKey(f => f.UserID);

        }



    }
}
