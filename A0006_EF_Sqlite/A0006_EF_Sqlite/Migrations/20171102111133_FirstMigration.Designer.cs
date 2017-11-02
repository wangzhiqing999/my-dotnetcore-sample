﻿// <auto-generated />
using A0006_EF_Sqlite.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace A0006_EF_Sqlite.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20171102111133_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("A0006_EF_Sqlite.Model.TestData", b =>
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
#pragma warning restore 612, 618
        }
    }
}
