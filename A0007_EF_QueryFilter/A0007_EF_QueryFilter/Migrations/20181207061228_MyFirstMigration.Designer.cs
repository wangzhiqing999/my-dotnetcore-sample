﻿// <auto-generated />
using System;
using A0007_EF_QueryFilter.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace A0007_EF_QueryFilter.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20181207061228_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("A0007_EF_QueryFilter.Model.Document", b =>
                {
                    b.Property<long>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("document_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("create_time");

                    b.Property<string>("CreateUser")
                        .HasColumnName("create_user")
                        .HasMaxLength(32);

                    b.Property<string>("DocumentContent")
                        .HasColumnName("document_content");

                    b.Property<string>("DocumentTitle")
                        .IsRequired()
                        .HasColumnName("document_title")
                        .HasMaxLength(64);

                    b.Property<string>("DocumentTypeCode")
                        .HasColumnName("document_type_code")
                        .HasMaxLength(32);

                    b.Property<DateTime>("LastUpdateTime")
                        .IsConcurrencyToken()
                        .HasColumnName("last_update_time");

                    b.Property<string>("LastUpdateUser")
                        .HasColumnName("last_update_user")
                        .HasMaxLength(32);

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(16);

                    b.HasKey("DocumentID");

                    b.HasIndex("DocumentTypeCode");

                    b.ToTable("document");
                });

            modelBuilder.Entity("A0007_EF_QueryFilter.Model.DocumentType", b =>
                {
                    b.Property<string>("DocumentTypeCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("document_type_code")
                        .HasMaxLength(32);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("create_time");

                    b.Property<string>("CreateUser")
                        .HasColumnName("create_user")
                        .HasMaxLength(32);

                    b.Property<string>("DocumentTypeName")
                        .HasColumnName("document_type_name")
                        .HasMaxLength(64);

                    b.Property<DateTime>("LastUpdateTime")
                        .IsConcurrencyToken()
                        .HasColumnName("last_update_time");

                    b.Property<string>("LastUpdateUser")
                        .HasColumnName("last_update_user")
                        .HasMaxLength(32);

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(16);

                    b.HasKey("DocumentTypeCode");

                    b.ToTable("document_type");
                });

            modelBuilder.Entity("A0007_EF_QueryFilter.Model.TestData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("A0007_EF_QueryFilter.Model.Document", b =>
                {
                    b.HasOne("A0007_EF_QueryFilter.Model.DocumentType", "DocumentType")
                        .WithMany("DocumentList")
                        .HasForeignKey("DocumentTypeCode");
                });
#pragma warning restore 612, 618
        }
    }
}