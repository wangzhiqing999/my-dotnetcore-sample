﻿// <auto-generated />
using A0005_EF_Mysql.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace A0005_EF_Mysql.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20180322054708_AddOneToOneCode")]
    partial class AddOneToOneCode
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("A0005_EF_Mysql.Model.Document", b =>
                {
                    b.Property<long>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("document_id");

                    b.Property<string>("DocumentContent")
                        .HasColumnName("document_content");

                    b.Property<string>("DocumentTitle")
                        .IsRequired()
                        .HasColumnName("document_title")
                        .HasMaxLength(64);

                    b.Property<string>("DocumentTypeCode")
                        .HasColumnName("document_type_code")
                        .HasMaxLength(32);

                    b.HasKey("DocumentID");

                    b.HasIndex("DocumentTypeCode");

                    b.ToTable("document");
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.DocumentType", b =>
                {
                    b.Property<string>("DocumentTypeCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("document_type_code")
                        .HasMaxLength(32);

                    b.Property<string>("DocumentTypeName")
                        .HasColumnName("document_type_name")
                        .HasMaxLength(64);

                    b.HasKey("DocumentTypeCode");

                    b.ToTable("document_type");
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.MrRole", b =>
                {
                    b.Property<string>("RoleCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_code")
                        .HasMaxLength(32);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnName("role_name")
                        .HasMaxLength(64);

                    b.HasKey("RoleCode");

                    b.ToTable("mr_role");
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.MrUser", b =>
                {
                    b.Property<string>("UserCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_code")
                        .HasMaxLength(32);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnName("user_name")
                        .HasMaxLength(32);

                    b.HasKey("UserCode");

                    b.ToTable("mr_user");
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.MrUserExp", b =>
                {
                    b.Property<string>("UserCode")
                        .HasColumnName("user_code")
                        .HasMaxLength(32);

                    b.Property<string>("UserExpData")
                        .HasColumnName("user_exp_data")
                        .HasMaxLength(32);

                    b.HasKey("UserCode");

                    b.ToTable("mr_user_exp");
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.MrUserRole", b =>
                {
                    b.Property<string>("UserCode")
                        .HasColumnName("user_code")
                        .HasMaxLength(32);

                    b.Property<string>("RoleCode")
                        .HasColumnName("role_code")
                        .HasMaxLength(32);

                    b.HasKey("UserCode", "RoleCode");

                    b.HasIndex("RoleCode");

                    b.ToTable("mr_user_role");
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.TestData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasMaxLength(64);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(16);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(16);

                    b.HasKey("ID");

                    b.ToTable("test_data");
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.Document", b =>
                {
                    b.HasOne("A0005_EF_Mysql.Model.DocumentType", "DocumentType")
                        .WithMany("DocumentList")
                        .HasForeignKey("DocumentTypeCode");
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.MrUserExp", b =>
                {
                    b.HasOne("A0005_EF_Mysql.Model.MrUser", "User")
                        .WithOne("UserExp")
                        .HasForeignKey("A0005_EF_Mysql.Model.MrUserExp", "UserCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("A0005_EF_Mysql.Model.MrUserRole", b =>
                {
                    b.HasOne("A0005_EF_Mysql.Model.MrRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("A0005_EF_Mysql.Model.MrUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
