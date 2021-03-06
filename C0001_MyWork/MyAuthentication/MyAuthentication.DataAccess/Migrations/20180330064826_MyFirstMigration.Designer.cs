﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MyAuthentication.DataAccess;
using System;

namespace MyAuthentication.Migrations
{
    [DbContext(typeof(MyAuthenticationContext))]
    [Migration("20180330064826_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyAuthentication.Model.MyAction", b =>
                {
                    b.Property<string>("ActionCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("action_code")
                        .HasMaxLength(32);

                    b.Property<string>("ActionExpand")
                        .IsRequired()
                        .HasColumnName("action_expand")
                        .HasMaxLength(256);

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasColumnName("action_name")
                        .HasMaxLength(32);

                    b.Property<bool>("DefaultUseable")
                        .HasColumnName("default_useable");

                    b.Property<string>("ModuleCode")
                        .IsRequired()
                        .HasColumnName("module_code")
                        .HasMaxLength(32);

                    b.HasKey("ActionCode");

                    b.HasIndex("ModuleCode");

                    b.ToTable("my_action");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyModule", b =>
                {
                    b.Property<string>("ModuleCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("module_code")
                        .HasMaxLength(32);

                    b.Property<string>("ModuleExpand")
                        .HasColumnName("module_expand")
                        .HasMaxLength(256);

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnName("module_name")
                        .HasMaxLength(32);

                    b.Property<string>("ModuleTypeCode")
                        .IsRequired()
                        .HasColumnName("module_type_code")
                        .HasMaxLength(32);

                    b.Property<string>("SystemCode")
                        .IsRequired()
                        .HasColumnName("system_code")
                        .HasMaxLength(32);

                    b.HasKey("ModuleCode");

                    b.HasIndex("ModuleTypeCode");

                    b.HasIndex("SystemCode");

                    b.ToTable("my_module");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyModuleType", b =>
                {
                    b.Property<string>("ModuleTypeCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("module_type_code")
                        .HasMaxLength(32);

                    b.Property<string>("ModuleTypeName")
                        .HasColumnName("module_type_name")
                        .HasMaxLength(32);

                    b.HasKey("ModuleTypeCode");

                    b.ToTable("my_module_type");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyOrganization", b =>
                {
                    b.Property<long>("OrganizationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("organization_id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("create_time");

                    b.Property<string>("CreateUser")
                        .HasColumnName("create_user")
                        .HasMaxLength(32);

                    b.Property<DateTime>("LastUpdateTime")
                        .IsConcurrencyToken()
                        .HasColumnName("last_update_time");

                    b.Property<string>("LastUpdateUser")
                        .HasColumnName("last_update_user")
                        .HasMaxLength(32);

                    b.Property<string>("LoginOrganizationCode")
                        .IsRequired()
                        .HasColumnName("login_organization_code")
                        .HasMaxLength(32);

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnName("organization_name")
                        .HasMaxLength(32);

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(16);

                    b.HasKey("OrganizationID");

                    b.ToTable("my_organization");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyRole", b =>
                {
                    b.Property<string>("RoleCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_code")
                        .HasMaxLength(32);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("create_time");

                    b.Property<string>("CreateUser")
                        .HasColumnName("create_user")
                        .HasMaxLength(32);

                    b.Property<DateTime>("LastUpdateTime")
                        .IsConcurrencyToken()
                        .HasColumnName("last_update_time");

                    b.Property<string>("LastUpdateUser")
                        .HasColumnName("last_update_user")
                        .HasMaxLength(32);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnName("role_name")
                        .HasMaxLength(32);

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(16);

                    b.Property<string>("SystemCode")
                        .IsRequired()
                        .HasColumnName("system_code")
                        .HasMaxLength(32);

                    b.HasKey("RoleCode");

                    b.HasIndex("SystemCode");

                    b.ToTable("my_role");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyRoleAction", b =>
                {
                    b.Property<string>("RoleCode")
                        .HasColumnName("role_code")
                        .HasMaxLength(32);

                    b.Property<string>("ActionCode")
                        .HasColumnName("action_code")
                        .HasMaxLength(32);

                    b.HasKey("RoleCode", "ActionCode");

                    b.HasIndex("ActionCode");

                    b.ToTable("my_role_action");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyRoleModule", b =>
                {
                    b.Property<string>("RoleCode")
                        .HasColumnName("role_code")
                        .HasMaxLength(32);

                    b.Property<string>("ModuleCode")
                        .HasColumnName("module_code")
                        .HasMaxLength(32);

                    b.HasKey("RoleCode", "ModuleCode");

                    b.HasIndex("ModuleCode");

                    b.ToTable("my_role_module");
                });

            modelBuilder.Entity("MyAuthentication.Model.MySystem", b =>
                {
                    b.Property<string>("SystemCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("system_code")
                        .HasMaxLength(32);

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasColumnName("system_name")
                        .HasMaxLength(64);

                    b.HasKey("SystemCode");

                    b.ToTable("my_system");
                });

            modelBuilder.Entity("MyAuthentication.Model.MySystemUser", b =>
                {
                    b.Property<string>("SystemCode")
                        .HasColumnName("system_code")
                        .HasMaxLength(32);

                    b.Property<long>("UserID")
                        .HasColumnName("user_id");

                    b.HasKey("SystemCode", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("my_system_user");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyUser", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("create_time");

                    b.Property<string>("CreateUser")
                        .HasColumnName("create_user")
                        .HasMaxLength(32);

                    b.Property<DateTime>("LastUpdateTime")
                        .IsConcurrencyToken()
                        .HasColumnName("last_update_time");

                    b.Property<string>("LastUpdateUser")
                        .HasColumnName("last_update_user")
                        .HasMaxLength(32);

                    b.Property<string>("LoginUserCode")
                        .IsRequired()
                        .HasColumnName("login_user_code")
                        .HasMaxLength(32);

                    b.Property<long>("OrganizationID")
                        .HasColumnName("organization_id");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(16);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("user_name")
                        .HasMaxLength(32);

                    b.Property<string>("UserPassword")
                        .HasColumnName("user_password")
                        .HasMaxLength(512);

                    b.HasKey("UserID");

                    b.HasIndex("OrganizationID");

                    b.ToTable("my_user");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyUserRole", b =>
                {
                    b.Property<long>("UserID")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleCode")
                        .HasColumnName("role_code")
                        .HasMaxLength(32);

                    b.HasKey("UserID", "RoleCode");

                    b.HasIndex("RoleCode");

                    b.ToTable("my_user_role");
                });

            modelBuilder.Entity("MyAuthentication.Model.MyAction", b =>
                {
                    b.HasOne("MyAuthentication.Model.MyModule", "Module")
                        .WithMany("Actions")
                        .HasForeignKey("ModuleCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyAuthentication.Model.MyModule", b =>
                {
                    b.HasOne("MyAuthentication.Model.MyModuleType", "ModuleType")
                        .WithMany("Modules")
                        .HasForeignKey("ModuleTypeCode")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyAuthentication.Model.MySystem", "System")
                        .WithMany("Modules")
                        .HasForeignKey("SystemCode")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyAuthentication.Model.MyRole", b =>
                {
                    b.HasOne("MyAuthentication.Model.MySystem", "System")
                        .WithMany("Roles")
                        .HasForeignKey("SystemCode")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyAuthentication.Model.MyRoleAction", b =>
                {
                    b.HasOne("MyAuthentication.Model.MyAction", "Action")
                        .WithMany("RoleActions")
                        .HasForeignKey("ActionCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyAuthentication.Model.MyRole", "Role")
                        .WithMany("RoleActions")
                        .HasForeignKey("RoleCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyAuthentication.Model.MyRoleModule", b =>
                {
                    b.HasOne("MyAuthentication.Model.MyModule", "Module")
                        .WithMany("RoleModules")
                        .HasForeignKey("ModuleCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyAuthentication.Model.MyRole", "Role")
                        .WithMany("RoleModules")
                        .HasForeignKey("RoleCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyAuthentication.Model.MySystemUser", b =>
                {
                    b.HasOne("MyAuthentication.Model.MySystem", "System")
                        .WithMany("SystemUsers")
                        .HasForeignKey("SystemCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyAuthentication.Model.MyUser", "User")
                        .WithMany("SystemUsers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyAuthentication.Model.MyUser", b =>
                {
                    b.HasOne("MyAuthentication.Model.MyOrganization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyAuthentication.Model.MyUserRole", b =>
                {
                    b.HasOne("MyAuthentication.Model.MyRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyAuthentication.Model.MyUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
