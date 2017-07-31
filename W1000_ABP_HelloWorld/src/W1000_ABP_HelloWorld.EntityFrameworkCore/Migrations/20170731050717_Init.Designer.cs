using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using W1000_ABP_HelloWorld.EntityFrameworkCore;

namespace W1000_ABP_HelloWorld.Migrations
{
    [DbContext(typeof(W1000_ABP_HelloWorldDbContext))]
    [Migration("20170731050717_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("W1000_ABP_HelloWorld.SystemConfig.SystemConfigProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConfigTypeCode")
                        .IsRequired()
                        .HasColumnName("config_type_code")
                        .HasMaxLength(32);

                    b.Property<int>("DisplayOrder")
                        .HasColumnName("display_order");

                    b.Property<string>("PropertyDataType")
                        .HasColumnName("property_datatype")
                        .HasMaxLength(64);

                    b.Property<string>("PropertyDesc")
                        .HasColumnName("property_desc")
                        .HasMaxLength(64);

                    b.Property<string>("PropertyName")
                        .HasColumnName("property_name")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("ConfigTypeCode");

                    b.ToTable("sc_system_config_property");
                });

            modelBuilder.Entity("W1000_ABP_HelloWorld.SystemConfig.SystemConfigType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("config_type_code")
                        .HasMaxLength(32);

                    b.Property<string>("ConfigClassName")
                        .HasColumnName("config_type_class_name")
                        .HasMaxLength(128);

                    b.Property<string>("ConfigTypeName")
                        .HasColumnName("config_type_name")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("sc_system_config_type");
                });

            modelBuilder.Entity("W1000_ABP_HelloWorld.SystemConfig.SystemConfigValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConfigCode")
                        .HasColumnName("config_code")
                        .HasMaxLength(32);

                    b.Property<string>("ConfigName")
                        .HasColumnName("config_name")
                        .HasMaxLength(64);

                    b.Property<string>("ConfigTypeCode")
                        .IsRequired()
                        .HasColumnName("config_type_code")
                        .HasMaxLength(32);

                    b.Property<string>("ConfigValue")
                        .HasColumnName("config_value");

                    b.HasKey("Id");

                    b.HasIndex("ConfigTypeCode");

                    b.ToTable("sc_system_config_value");
                });

            modelBuilder.Entity("W1000_ABP_HelloWorld.SystemConfig.SystemConfigProperty", b =>
                {
                    b.HasOne("W1000_ABP_HelloWorld.SystemConfig.SystemConfigType", "SystemConfigTypeData")
                        .WithMany("SystemConfigPropertyList")
                        .HasForeignKey("ConfigTypeCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("W1000_ABP_HelloWorld.SystemConfig.SystemConfigValue", b =>
                {
                    b.HasOne("W1000_ABP_HelloWorld.SystemConfig.SystemConfigType", "SystemConfigTypeData")
                        .WithMany("SystemConfigValueList")
                        .HasForeignKey("ConfigTypeCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
