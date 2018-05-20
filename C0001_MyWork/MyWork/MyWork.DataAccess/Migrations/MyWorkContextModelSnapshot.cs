﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MyWork.DataAccess;
using System;

namespace MyWork.Migrations
{
    [DbContext(typeof(MyWorkContext))]
    partial class MyWorkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyWork.Model.Account", b =>
                {
                    b.Property<long>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("account_id");

                    b.Property<decimal>("AccountBalance")
                        .HasColumnName("account_balance");

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

                    b.Property<long>("OrganizationID")
                        .HasColumnName("organization_id");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("row_version");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(16);

                    b.HasKey("AccountID");

                    b.ToTable("mw_account");
                });

            modelBuilder.Entity("MyWork.Model.AccountDailyReport", b =>
                {
                    b.Property<long>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("report_id");

                    b.Property<long>("AccountID")
                        .HasColumnName("account_id");

                    b.Property<decimal>("BeginningMoney")
                        .HasColumnName("beginning_money");

                    b.Property<int>("DealCount")
                        .HasColumnName("deal_count");

                    b.Property<decimal>("EndingMoney")
                        .HasColumnName("ending_money");

                    b.Property<decimal>("MoneyChange")
                        .HasColumnName("money_change");

                    b.Property<decimal>("PositionValue")
                        .HasColumnName("position_value");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnName("report_date");

                    b.HasKey("ReportID");

                    b.HasIndex("AccountID");

                    b.ToTable("mw_account_daily_report");
                });

            modelBuilder.Entity("MyWork.Model.AccountOperationLog", b =>
                {
                    b.Property<long>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("log_id");

                    b.Property<long>("AccountID")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("AccountingDate")
                        .HasColumnName("accounting_date");

                    b.Property<decimal>("AfterAccountBalance")
                        .HasColumnName("after_account_balance");

                    b.Property<decimal>("BeforeAccountBalance")
                        .HasColumnName("before_account_balance");

                    b.Property<string>("OperationDesc")
                        .HasColumnName("operation_desc");

                    b.Property<decimal>("OperationMoney")
                        .HasColumnName("operation_money");

                    b.Property<DateTime>("OperationTime")
                        .HasColumnName("operation_time");

                    b.HasKey("LogID");

                    b.HasIndex("AccountID");

                    b.ToTable("mw_account_operation_log");
                });

            modelBuilder.Entity("MyWork.Model.DailySummary", b =>
                {
                    b.Property<long>("DailySummaryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("daily_summary_id");

                    b.Property<long>("AccountID")
                        .HasColumnName("account_id");

                    b.Property<decimal>("ClosePrice")
                        .HasColumnName("close_price");

                    b.Property<DateTime>("DailySummaryDate")
                        .HasColumnName("daily_summary_date");

                    b.Property<int>("PositionQuantity")
                        .HasColumnName("position_quantity");

                    b.Property<decimal>("PositionValue")
                        .HasColumnName("position_value");

                    b.Property<string>("StockCode")
                        .IsRequired()
                        .HasColumnName("stock_code")
                        .HasMaxLength(16);

                    b.Property<decimal>("StopLossPrice")
                        .HasColumnName("stop_loss_price");

                    b.Property<string>("Todo")
                        .HasColumnName("todo")
                        .HasMaxLength(64);

                    b.HasKey("DailySummaryID");

                    b.HasIndex("AccountID");

                    b.HasIndex("StockCode");

                    b.ToTable("mw_daily_summary");
                });

            modelBuilder.Entity("MyWork.Model.Position", b =>
                {
                    b.Property<long>("PositionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("position_id");

                    b.Property<long>("AccountID")
                        .HasColumnName("account_id");

                    b.Property<decimal>("Cost")
                        .HasColumnName("cost");

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

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(16);

                    b.Property<string>("StockCode")
                        .IsRequired()
                        .HasColumnName("stock_code")
                        .HasMaxLength(16);

                    b.HasKey("PositionID");

                    b.HasIndex("AccountID");

                    b.HasIndex("StockCode");

                    b.ToTable("mw_position");
                });

            modelBuilder.Entity("MyWork.Model.StockInfo", b =>
                {
                    b.Property<string>("StockCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("stock_code")
                        .HasMaxLength(16);

                    b.Property<string>("Market")
                        .HasColumnName("market")
                        .HasMaxLength(32);

                    b.Property<string>("StockName")
                        .HasColumnName("stock_name")
                        .HasMaxLength(32);

                    b.Property<string>("StockNamePinyin")
                        .HasColumnName("stock_name_pinyin")
                        .HasMaxLength(32);

                    b.HasKey("StockCode");

                    b.ToTable("mw_stock_info");
                });

            modelBuilder.Entity("MyWork.Model.StockPool", b =>
                {
                    b.Property<long>("StockPoolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("stock_pool_id");

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

                    b.Property<long>("OrganizationID")
                        .HasColumnName("organization_id");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(16);

                    b.Property<string>("StockPoolName")
                        .HasColumnName("stock_pool_name");

                    b.HasKey("StockPoolID");

                    b.ToTable("mw_stock_pool");
                });

            modelBuilder.Entity("MyWork.Model.StockPoolDetail", b =>
                {
                    b.Property<long>("StockPoolID")
                        .HasColumnName("stock_pool_id");

                    b.Property<string>("StockCode")
                        .HasColumnName("stock_code")
                        .HasMaxLength(16);

                    b.HasKey("StockPoolID", "StockCode");

                    b.HasIndex("StockCode");

                    b.ToTable("mw_stock_pool_detail");
                });

            modelBuilder.Entity("MyWork.Model.Trading", b =>
                {
                    b.Property<long>("TradingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("trading_id");

                    b.Property<long>("AccountID")
                        .HasColumnName("account_id");

                    b.Property<decimal>("Fees")
                        .HasColumnName("trading_fees");

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity");

                    b.Property<string>("StockCode")
                        .IsRequired()
                        .HasColumnName("stock_code")
                        .HasMaxLength(16);

                    b.Property<DateTime>("TradingDateTime")
                        .HasColumnName("trading_datetime");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnName("unit_price");

                    b.HasKey("TradingID");

                    b.HasIndex("AccountID");

                    b.HasIndex("StockCode");

                    b.ToTable("mw_trading");
                });

            modelBuilder.Entity("MyWork.Model.AccountDailyReport", b =>
                {
                    b.HasOne("MyWork.Model.Account", "AccountData")
                        .WithMany("DailyReportList")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyWork.Model.AccountOperationLog", b =>
                {
                    b.HasOne("MyWork.Model.Account", "AccountData")
                        .WithMany("OperationLogList")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyWork.Model.DailySummary", b =>
                {
                    b.HasOne("MyWork.Model.Account", "AccountData")
                        .WithMany("DailySummaryList")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyWork.Model.StockInfo", "StockInfoData")
                        .WithMany("DailySummaryList")
                        .HasForeignKey("StockCode")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyWork.Model.Position", b =>
                {
                    b.HasOne("MyWork.Model.Account", "AccountData")
                        .WithMany("PositionList")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyWork.Model.StockInfo", "StockInfoData")
                        .WithMany("PositionList")
                        .HasForeignKey("StockCode")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MyWork.Model.StockPoolDetail", b =>
                {
                    b.HasOne("MyWork.Model.StockInfo", "Stock")
                        .WithMany("StockPoolDetails")
                        .HasForeignKey("StockCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyWork.Model.StockPool", "Pool")
                        .WithMany("StockPoolDetails")
                        .HasForeignKey("StockPoolID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyWork.Model.Trading", b =>
                {
                    b.HasOne("MyWork.Model.Account", "AccountData")
                        .WithMany("TradingList")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyWork.Model.StockInfo", "StockInfoData")
                        .WithMany("TradingList")
                        .HasForeignKey("StockCode")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
