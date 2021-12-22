﻿// <auto-generated />
using A0009_EF_Postgres.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace A0009_EF_Postgres.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20211222054525_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("A0009_EF_Postgres.Model.Document", b =>
                {
                    b.Property<long>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("document_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DocumentContent")
                        .HasColumnType("text")
                        .HasColumnName("document_content");

                    b.Property<string>("DocumentTitle")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("document_title");

                    b.Property<string>("DocumentTypeCode")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("document_type_code");

                    b.HasKey("DocumentID");

                    b.HasIndex("DocumentTypeCode");

                    b.ToTable("document");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.DocumentType", b =>
                {
                    b.Property<string>("DocumentTypeCode")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("document_type_code");

                    b.Property<string>("DocumentTypeName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("document_type_name");

                    b.HasKey("DocumentTypeCode");

                    b.ToTable("document_type");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.MrRole", b =>
                {
                    b.Property<string>("RoleCode")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("role_code");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("role_name");

                    b.HasKey("RoleCode");

                    b.ToTable("mr_role");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.MrUser", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("user_code");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("user_name");

                    b.HasKey("UserCode");

                    b.ToTable("mr_user");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.MrUserExp", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("user_code");

                    b.Property<string>("UserExpData")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("user_exp_data");

                    b.HasKey("UserCode");

                    b.ToTable("mr_user_exp");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.MrUserRole", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("user_code");

                    b.Property<string>("RoleCode")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("role_code");

                    b.HasKey("UserCode", "RoleCode");

                    b.HasIndex("RoleCode");

                    b.ToTable("mr_user_role");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.TestData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("phone");

                    b.HasKey("ID");

                    b.ToTable("test_data");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.Document", b =>
                {
                    b.HasOne("A0009_EF_Postgres.Model.DocumentType", "DocumentType")
                        .WithMany("DocumentList")
                        .HasForeignKey("DocumentTypeCode");

                    b.Navigation("DocumentType");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.MrUserExp", b =>
                {
                    b.HasOne("A0009_EF_Postgres.Model.MrUser", "User")
                        .WithOne("UserExp")
                        .HasForeignKey("A0009_EF_Postgres.Model.MrUserExp", "UserCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.MrUserRole", b =>
                {
                    b.HasOne("A0009_EF_Postgres.Model.MrRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("A0009_EF_Postgres.Model.MrUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.DocumentType", b =>
                {
                    b.Navigation("DocumentList");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.MrRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("A0009_EF_Postgres.Model.MrUser", b =>
                {
                    b.Navigation("UserExp");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
