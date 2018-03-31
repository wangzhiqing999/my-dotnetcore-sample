using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

using Microsoft.EntityFrameworkCore;

using MyAuthentication.Model;

namespace MyAuthentication.DataAccess
{
    public class MyAuthenticationContext : DbContext
    {
        public MyAuthenticationContext() : base()
        {
        }

        public MyAuthenticationContext(DbContextOptions<MyAuthenticationContext> options) : base(options)
        {
        }


        /// <summary>
        /// 组织.
        /// </summary>
        public DbSet<MyOrganization> MyOrganizations { get; set; }

        /// <summary>
        /// 系统.
        /// </summary>
        public DbSet<MySystem> MySystems { get; set; }

        /// <summary>
        /// 用户.
        /// </summary>
        public DbSet<MyUser> MyUsers { get; set; }

        /// <summary>
        /// 系统-用户关系.
        /// </summary>
        public DbSet<MySystemUser> MySystemUsers { get; set; }



        /// <summary>
        /// 角色.
        /// </summary>
        public DbSet<MyRole> MyRoles { get; set; }

        /// <summary>
        /// 用户-角色关系.
        /// </summary>
        public DbSet<MyUserRole> MyUserRoles { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public DbSet<MyModuleType> MyModuleTypes { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public DbSet<MyModule> MyModules { get; set; }

        /// <summary>
        /// 模块动作
        /// </summary>
        public DbSet<MyAction> MyActions { get; set; }


        /// <summary>
        /// 角色 - 功能模块 关系.
        /// </summary>
        public DbSet<MyRoleModule> MyRoleModules { get; set; }


        /// <summary>
        /// 角色 - 模块动作 关系.
        /// </summary>
        public DbSet<MyRoleAction> MyRoleActions { get; set; }



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
                provider.TryGet("ConnectionStrings:MyAuthenticationConnection", out connString);

                optionsBuilder.UseSqlServer(connString);
            }

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ----- 组织 - 用户 一对多关系.
            // 对于一个 用户
            modelBuilder.Entity<MyUser>()
                        // 归属一个 组织.
                        .HasOne(s => s.Organization)
                        // 一个组织，允许有多个 用户
                        .WithMany(m => m.Users)
                        // 外键.
                        .HasForeignKey(f => f.OrganizationID)
                        // 删除组织时，如果组织下存在有用户， 拒绝删除.
                        .OnDelete(DeleteBehavior.Restrict);



            // ----- 系统 - 角色 一对多关系.
            // 对于一个 角色
            modelBuilder.Entity<MyRole>()
                        // 有一个 系统.
                        .HasOne(s => s.System)
                        // 一个系统，允许有多个 角色
                        .WithMany(m => m.Roles)
                        // 外键.
                        .HasForeignKey(f => f.SystemCode)
                        // 删除系统时，如果系统下存在有角色， 拒绝删除.
                        .OnDelete(DeleteBehavior.Restrict);



            // ------ 系统 - 用户 多对多关系.
            // 系统-用户关系表 :  复合主键.
            modelBuilder.Entity<MySystemUser>()
                        .HasKey(t => new { t.SystemCode, t.UserID });
            // 对于一个 系统-用户关系
            modelBuilder.Entity<MySystemUser>()
                        // 有一个 系统.
                        .HasOne(s => s.System)
                        // 一个系统，允许有多个 系统-用户关系
                        .WithMany(m => m.SystemUsers)
                        // 外键.
                        .HasForeignKey(f => f.SystemCode)
                        // 删除系统时，如果系统存在有 系统 - 用户 关系， 自动删除.
                        .OnDelete(DeleteBehavior.Cascade);
            // 对于一个 系统-用户关系
            modelBuilder.Entity<MySystemUser>()
                        // 有一个 用户.
                        .HasOne(s => s.User)
                        // 一个用户，允许有多个 系统-用户关系
                        .WithMany(m => m.SystemUsers)
                        // 外键.
                        .HasForeignKey(f => f.UserID)
                        // 删除用户时，如果用户存在有 系统 - 用户 关系， 自动删除.
                        .OnDelete(DeleteBehavior.Cascade);



            // ----- 用户 - 角色 多对多关系.
            // 用户-角色关系表 ： 复合主键.
            modelBuilder.Entity<MyUserRole>()
                        .HasKey(t => new { t.UserID, t.RoleCode });
            // 对于一个 用户-角色关系
            modelBuilder.Entity<MyUserRole>()
                        // 有一个 用户.
                        .HasOne(s => s.User)
                        // 一个用户，允许有多个 用户-角色关系
                        .WithMany(m => m.UserRoles)
                        // 外键.
                        .HasForeignKey(f => f.UserID)
                        // 删除用户时，如果用户存在有 用户-角色 关系， 自动删除.
                        .OnDelete(DeleteBehavior.Cascade);

            // 对于一个 用户-角色关系
            modelBuilder.Entity<MyUserRole>()
                        // 有一个 角色.
                        .HasOne(s => s.Role)
                        // 一个角色，允许有多个 用户-角色关系
                        .WithMany(m => m.UserRoles)
                        // 外键.
                        .HasForeignKey(f => f.RoleCode)
                        // 删除角色时，如果角色存在有 用户-角色 关系， 自动删除.
                        .OnDelete(DeleteBehavior.Cascade);



            // ----- 系统 - 功能模块 一对多关系.
            // 对于一个 功能模块
            modelBuilder.Entity<MyModule>()
                        // 有一个 系统.
                        .HasOne(s => s.System)
                        // 一个系统，允许有多个 功能模块
                        .WithMany(m => m.Modules)
                        // 外键.
                        .HasForeignKey(f => f.SystemCode)
                        // 删除系统时，如果系统下存在有功能模块， 拒绝删除.
                        .OnDelete(DeleteBehavior.Restrict);

            // ----- 功能模块类型 - 功能模块 一对多关系.
            // 对于一个 功能模块
            modelBuilder.Entity<MyModule>()
                        // 有一个 功能模块类型.
                        .HasOne(s => s.ModuleType)
                        // 一个功能模块类型，允许有多个 功能模块
                        .WithMany(m => m.Modules)
                        // 外键.
                        .HasForeignKey(f => f.ModuleTypeCode)
                        // 删除功能模块类型时，如果功能模块类型下存在有功能模块， 拒绝删除.
                        .OnDelete(DeleteBehavior.Restrict);


            // ----- 功能模块 - 模块动作  一对多关系.
            // 对于一个 模块动作
            modelBuilder.Entity<MyAction>()
                       // 有一个 功能模块.
                       .HasOne(s => s.Module)
                       // 一个功能模块，允许有多个 模块动作
                       .WithMany(m => m.Actions)
                       // 外键.
                       .HasForeignKey(f => f.ModuleCode)
                        // 删除功能模块时，如果功能模块下存在有模块动作， 连带删除.
                        .OnDelete(DeleteBehavior.Cascade);



            // ----- 角色 - 功能模块 多对多关系.
            // 角色 - 功能模块 关系表 ： 复合主键.
            modelBuilder.Entity<MyRoleModule>()
                        .HasKey(t => new { t.RoleCode, t.ModuleCode });
            // 对于一个 角色 - 功能模块 关系
            modelBuilder.Entity<MyRoleModule>()
                        // 有一个 功能模块.
                        .HasOne(s => s.Module)
                        // 一个功能模块，允许有多个 角色 - 功能模块关系
                        .WithMany(m => m.RoleModules)
                        // 外键.
                        .HasForeignKey(f => f.ModuleCode)
                        // 删除功能模块时，如果功能模块下存在有 角色 - 功能模块 关系， 连带删除.
                        .OnDelete(DeleteBehavior.Cascade);

            // 对于一个 角色 - 功能模块 关系
            modelBuilder.Entity<MyRoleModule>()
                        // 有一个 角色.
                        .HasOne(s => s.Role)
                        // 一个角色，允许有多个 角色 - 功能模块 关系
                        .WithMany(m => m.RoleModules)
                        // 外键.
                        .HasForeignKey(f => f.RoleCode)
                        // 删除 角色 时，如果角色 存在有 角色 - 功能模块 关系， 连带删除.
                        .OnDelete(DeleteBehavior.Cascade);



            // ----- 角色 - 模块动作 多对多关系.
            // 角色 - 模块动作 关系表 ： 复合主键.
            modelBuilder.Entity<MyRoleAction>()
                        .HasKey(t => new { t.RoleCode, t.ActionCode });
            // 对于一个 角色 - 模块动作 关系
            modelBuilder.Entity<MyRoleAction>()
                        // 有一个 模块动作.
                        .HasOne(s => s.Action)
                        // 一个 模块动作，允许有多个 角色 - 模块动作 关系
                        .WithMany(m => m.RoleActions)
                        // 外键.
                        .HasForeignKey(f => f.ActionCode)
                        // 删除模块动作时，如果功能模块下存在有 角色 - 模块动作 关系， 连带删除.
                        .OnDelete(DeleteBehavior.Cascade);

            // 对于一个 角色 - 模块动作 关系
            modelBuilder.Entity<MyRoleAction>()
                        // 有一个 角色.
                        .HasOne(s => s.Role)
                        // 一个角色，允许有多个 角色 - 模块动作 关系
                        .WithMany(m => m.RoleActions)
                        // 外键.
                        .HasForeignKey(f => f.RoleCode)
                        // 删除 角色 时，如果角色 存在有 角色 - 模块动作 关系， 连带删除.
                        .OnDelete(DeleteBehavior.Cascade);


        }


    }
}
