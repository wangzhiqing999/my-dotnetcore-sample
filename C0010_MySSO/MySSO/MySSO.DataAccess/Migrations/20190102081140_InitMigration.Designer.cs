﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySSO.DataAccess;

namespace MySSO.Migrations
{
    [DbContext(typeof(MySSOContext))]
    [Migration("20190102081140_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MySSO.Model.SystemUser", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnName("user_id");

                    b.Property<string>("UserCategoryCode")
                        .HasColumnName("user_category_code")
                        .HasMaxLength(32);

                    b.Property<string>("UserName")
                        .HasColumnName("user_name")
                        .HasMaxLength(32);

                    b.Property<string>("UserPassword")
                        .HasColumnName("user_password")
                        .HasMaxLength(128);

                    b.HasKey("UserID");

                    b.HasIndex("UserCategoryCode");

                    b.ToTable("sso_user");
                });

            modelBuilder.Entity("MySSO.Model.SystemUserCategory", b =>
                {
                    b.Property<string>("UserCategoryCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_category_code")
                        .HasMaxLength(32);

                    b.Property<string>("UserCategoryName")
                        .HasColumnName("user_category_name")
                        .HasMaxLength(32);

                    b.HasKey("UserCategoryCode");

                    b.ToTable("sso_user_category");
                });

            modelBuilder.Entity("MySSO.Model.SystemUserToken", b =>
                {
                    b.Property<Guid>("TokenID")
                        .HasColumnName("token_id");

                    b.Property<DateTime>("ExpiredTime")
                        .HasColumnName("expired_time");

                    b.Property<DateTime>("StartTime")
                        .HasColumnName("start_time");

                    b.Property<Guid>("UserID")
                        .HasColumnName("user_id");

                    b.HasKey("TokenID");

                    b.HasIndex("UserID");

                    b.ToTable("sso_user_token");
                });

            modelBuilder.Entity("MySSO.Model.SystemUser", b =>
                {
                    b.HasOne("MySSO.Model.SystemUserCategory", "UserCategory")
                        .WithMany("SystemUserList")
                        .HasForeignKey("UserCategoryCode");
                });

            modelBuilder.Entity("MySSO.Model.SystemUserToken", b =>
                {
                    b.HasOne("MySSO.Model.SystemUser", "SystemUserData")
                        .WithMany("SystemUserTokenList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
