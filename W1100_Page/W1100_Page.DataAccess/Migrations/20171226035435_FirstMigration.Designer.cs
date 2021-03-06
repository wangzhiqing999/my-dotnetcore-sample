﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using W1100_Page.DataAccess;

namespace W1100_Page.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20171226035435_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("W1100_Page.Model.FinanceData", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("finance_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("Content")
                        .HasMaxLength(32);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnName("CountryName")
                        .HasMaxLength(32);

                    b.Property<string>("CurrentValue")
                        .HasColumnName("CurrentValue")
                        .HasMaxLength(32);

                    b.Property<DateTime>("FinanceDateTime")
                        .HasColumnName("FinanceDateTime");

                    b.Property<string>("MoreUrl")
                        .HasColumnName("more_url")
                        .HasMaxLength(64);

                    b.Property<string>("Predict")
                        .HasColumnName("Predict")
                        .HasMaxLength(32);

                    b.Property<string>("Previous")
                        .HasColumnName("Previous")
                        .HasMaxLength(32);

                    b.Property<string>("Profitable")
                        .HasColumnName("Profitable")
                        .HasMaxLength(32);

                    b.Property<string>("Unprofitable")
                        .HasColumnName("Unprofitable")
                        .HasMaxLength(32);

                    b.Property<int>("Weightiness")
                        .HasColumnName("Weightiness");

                    b.HasKey("ID");

                    b.ToTable("FinanceDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
