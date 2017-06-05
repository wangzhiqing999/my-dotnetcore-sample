using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using A0003_EF.DataAccess;

namespace A0003_EF.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20170605050819_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("A0003_EF.Model.TestData", b =>
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
        }
    }
}
