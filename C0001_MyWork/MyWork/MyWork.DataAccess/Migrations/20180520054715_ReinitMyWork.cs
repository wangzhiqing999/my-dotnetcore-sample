using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyWork.Migrations
{
    public partial class ReinitMyWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mw_account",
                columns: table => new
                {
                    account_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    account_balance = table.Column<decimal>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user = table.Column<string>(maxLength: 32, nullable: true),
                    last_update_time = table.Column<DateTime>(nullable: false),
                    last_update_user = table.Column<string>(maxLength: 32, nullable: true),
                    organization_id = table.Column<long>(nullable: false),
                    row_version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    status = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_account", x => x.account_id);
                });

            migrationBuilder.CreateTable(
                name: "mw_stock_info",
                columns: table => new
                {
                    stock_code = table.Column<string>(maxLength: 16, nullable: false),
                    market = table.Column<string>(maxLength: 32, nullable: true),
                    stock_name = table.Column<string>(maxLength: 32, nullable: true),
                    stock_name_pinyin = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_stock_info", x => x.stock_code);
                });

            migrationBuilder.CreateTable(
                name: "mw_stock_pool",
                columns: table => new
                {
                    stock_pool_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user = table.Column<string>(maxLength: 32, nullable: true),
                    last_update_time = table.Column<DateTime>(nullable: false),
                    last_update_user = table.Column<string>(maxLength: 32, nullable: true),
                    organization_id = table.Column<long>(nullable: false),
                    status = table.Column<string>(maxLength: 16, nullable: true),
                    stock_pool_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_stock_pool", x => x.stock_pool_id);
                });

            migrationBuilder.CreateTable(
                name: "mw_account_daily_report",
                columns: table => new
                {
                    report_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<long>(nullable: false),
                    beginning_money = table.Column<decimal>(nullable: false),
                    deal_count = table.Column<int>(nullable: false),
                    ending_money = table.Column<decimal>(nullable: false),
                    money_change = table.Column<decimal>(nullable: false),
                    position_value = table.Column<decimal>(nullable: false),
                    report_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_account_daily_report", x => x.report_id);
                    table.ForeignKey(
                        name: "FK_mw_account_daily_report_mw_account_account_id",
                        column: x => x.account_id,
                        principalTable: "mw_account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mw_account_operation_log",
                columns: table => new
                {
                    log_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<long>(nullable: false),
                    accounting_date = table.Column<DateTime>(nullable: false),
                    after_account_balance = table.Column<decimal>(nullable: false),
                    before_account_balance = table.Column<decimal>(nullable: false),
                    operation_desc = table.Column<string>(nullable: true),
                    operation_money = table.Column<decimal>(nullable: false),
                    operation_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_account_operation_log", x => x.log_id);
                    table.ForeignKey(
                        name: "FK_mw_account_operation_log_mw_account_account_id",
                        column: x => x.account_id,
                        principalTable: "mw_account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mw_daily_summary",
                columns: table => new
                {
                    daily_summary_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<long>(nullable: false),
                    close_price = table.Column<decimal>(nullable: false),
                    daily_summary_date = table.Column<DateTime>(nullable: false),
                    position_quantity = table.Column<int>(nullable: false),
                    position_value = table.Column<decimal>(nullable: false),
                    stock_code = table.Column<string>(maxLength: 16, nullable: false),
                    stop_loss_price = table.Column<decimal>(nullable: false),
                    todo = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_daily_summary", x => x.daily_summary_id);
                    table.ForeignKey(
                        name: "FK_mw_daily_summary_mw_account_account_id",
                        column: x => x.account_id,
                        principalTable: "mw_account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mw_daily_summary_mw_stock_info_stock_code",
                        column: x => x.stock_code,
                        principalTable: "mw_stock_info",
                        principalColumn: "stock_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mw_position",
                columns: table => new
                {
                    position_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<long>(nullable: false),
                    cost = table.Column<decimal>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user = table.Column<string>(maxLength: 32, nullable: true),
                    last_update_time = table.Column<DateTime>(nullable: false),
                    last_update_user = table.Column<string>(maxLength: 32, nullable: true),
                    quantity = table.Column<int>(nullable: false),
                    status = table.Column<string>(maxLength: 16, nullable: true),
                    stock_code = table.Column<string>(maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_position", x => x.position_id);
                    table.ForeignKey(
                        name: "FK_mw_position_mw_account_account_id",
                        column: x => x.account_id,
                        principalTable: "mw_account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mw_position_mw_stock_info_stock_code",
                        column: x => x.stock_code,
                        principalTable: "mw_stock_info",
                        principalColumn: "stock_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mw_trading",
                columns: table => new
                {
                    trading_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<long>(nullable: false),
                    trading_fees = table.Column<decimal>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    stock_code = table.Column<string>(maxLength: 16, nullable: false),
                    trading_datetime = table.Column<DateTime>(nullable: false),
                    unit_price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_trading", x => x.trading_id);
                    table.ForeignKey(
                        name: "FK_mw_trading_mw_account_account_id",
                        column: x => x.account_id,
                        principalTable: "mw_account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mw_trading_mw_stock_info_stock_code",
                        column: x => x.stock_code,
                        principalTable: "mw_stock_info",
                        principalColumn: "stock_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mw_stock_pool_detail",
                columns: table => new
                {
                    stock_pool_id = table.Column<long>(nullable: false),
                    stock_code = table.Column<string>(maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mw_stock_pool_detail", x => new { x.stock_pool_id, x.stock_code });
                    table.ForeignKey(
                        name: "FK_mw_stock_pool_detail_mw_stock_info_stock_code",
                        column: x => x.stock_code,
                        principalTable: "mw_stock_info",
                        principalColumn: "stock_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mw_stock_pool_detail_mw_stock_pool_stock_pool_id",
                        column: x => x.stock_pool_id,
                        principalTable: "mw_stock_pool",
                        principalColumn: "stock_pool_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mw_account_daily_report_account_id",
                table: "mw_account_daily_report",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_mw_account_operation_log_account_id",
                table: "mw_account_operation_log",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_mw_daily_summary_account_id",
                table: "mw_daily_summary",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_mw_daily_summary_stock_code",
                table: "mw_daily_summary",
                column: "stock_code");

            migrationBuilder.CreateIndex(
                name: "IX_mw_position_account_id",
                table: "mw_position",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_mw_position_stock_code",
                table: "mw_position",
                column: "stock_code");

            migrationBuilder.CreateIndex(
                name: "IX_mw_stock_pool_detail_stock_code",
                table: "mw_stock_pool_detail",
                column: "stock_code");

            migrationBuilder.CreateIndex(
                name: "IX_mw_trading_account_id",
                table: "mw_trading",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_mw_trading_stock_code",
                table: "mw_trading",
                column: "stock_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mw_account_daily_report");

            migrationBuilder.DropTable(
                name: "mw_account_operation_log");

            migrationBuilder.DropTable(
                name: "mw_daily_summary");

            migrationBuilder.DropTable(
                name: "mw_position");

            migrationBuilder.DropTable(
                name: "mw_stock_pool_detail");

            migrationBuilder.DropTable(
                name: "mw_trading");

            migrationBuilder.DropTable(
                name: "mw_stock_pool");

            migrationBuilder.DropTable(
                name: "mw_account");

            migrationBuilder.DropTable(
                name: "mw_stock_info");
        }
    }
}
