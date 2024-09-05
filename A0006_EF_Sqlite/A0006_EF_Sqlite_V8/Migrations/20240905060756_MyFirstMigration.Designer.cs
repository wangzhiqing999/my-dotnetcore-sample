﻿// <auto-generated />
using A0006_EF_Sqlite_V8.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace A0006_EF_Sqlite_V8.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20240905060756_MyFirstMigration")]
    partial class MyFirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("A0006_EF_Sqlite_V8.Model.TestData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT")
                        .HasColumnName("address");

                    b.Property<string>("CreatedTime")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT")
                        .HasColumnName("created_time");

                    b.Property<string>("DetailData")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("detail_data");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT")
                        .HasColumnName("phone");

                    b.HasKey("ID");

                    b.ToTable("test_data");
                });
#pragma warning restore 612, 618
        }
    }
}
