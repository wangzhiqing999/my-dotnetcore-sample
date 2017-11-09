using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyMiniTradingSystem.DataAccess.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mts_commodity",
                columns: table => new
                {
                    commodity_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    commodity_name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    deposit_ratio = table.Column<int>(type: "INTEGER", nullable: false),
                    num_of_one_hand = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mts_commodity", x => x.commodity_code);
                });

            migrationBuilder.CreateTable(
                name: "mts_user_account",
                columns: table => new
                {
                    user_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    user_name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mts_user_account", x => x.user_code);
                });

            migrationBuilder.CreateTable(
                name: "mts_commodity_price",
                columns: table => new
                {
                    commodity_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    trading_start_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    atr = table.Column<decimal>(type: "decimal(10, 3)", nullable: false),
                    close_price = table.Column<decimal>(type: "decimal(10, 3)", nullable: false),
                    highest_price = table.Column<decimal>(type: "decimal(10, 3)", nullable: false),
                    lowest_price = table.Column<decimal>(type: "decimal(10, 3)", nullable: false),
                    open_price = table.Column<decimal>(type: "decimal(10, 3)", nullable: false),
                    tr = table.Column<decimal>(type: "decimal(10, 3)", nullable: false),
                    trading_finish_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    volume = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mts_commodity_price", x => new { x.commodity_code, x.trading_start_date });
                    table.ForeignKey(
                        name: "FK_mts_commodity_price_mts_commodity_commodity_code",
                        column: x => x.commodity_code,
                        principalTable: "mts_commodity",
                        principalColumn: "commodity_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mts_daily_summary",
                columns: table => new
                {
                    daily_summary_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    close_price = table.Column<decimal>(type: "decimal(10, 3)", nullable: false),
                    daily_summary_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    position_commodity_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    position_quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    position_value = table.Column<decimal>(type: "decimal(10, 3)", nullable: false),
                    stop_loss_price = table.Column<decimal>(type: "TEXT", nullable: false),
                    todo = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    user_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mts_daily_summary", x => x.daily_summary_id);
                    table.ForeignKey(
                        name: "FK_mts_daily_summary_mts_commodity_position_commodity_code",
                        column: x => x.position_commodity_code,
                        principalTable: "mts_commodity",
                        principalColumn: "commodity_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mts_daily_summary_mts_user_account_user_code",
                        column: x => x.user_code,
                        principalTable: "mts_user_account",
                        principalColumn: "user_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mts_position",
                columns: table => new
                {
                    position_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    commodity_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    is_long = table.Column<bool>(type: "INTEGER", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    user_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mts_position", x => x.position_id);
                    table.ForeignKey(
                        name: "FK_mts_position_mts_commodity_commodity_code",
                        column: x => x.commodity_code,
                        principalTable: "mts_commodity",
                        principalColumn: "commodity_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mts_position_mts_user_account_user_code",
                        column: x => x.user_code,
                        principalTable: "mts_user_account",
                        principalColumn: "user_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mts_daily_summary_position_commodity_code",
                table: "mts_daily_summary",
                column: "position_commodity_code");

            migrationBuilder.CreateIndex(
                name: "IX_mts_daily_summary_user_code",
                table: "mts_daily_summary",
                column: "user_code");

            migrationBuilder.CreateIndex(
                name: "IX_mts_position_commodity_code",
                table: "mts_position",
                column: "commodity_code");

            migrationBuilder.CreateIndex(
                name: "IX_mts_position_user_code",
                table: "mts_position",
                column: "user_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mts_commodity_price");

            migrationBuilder.DropTable(
                name: "mts_daily_summary");

            migrationBuilder.DropTable(
                name: "mts_position");

            migrationBuilder.DropTable(
                name: "mts_commodity");

            migrationBuilder.DropTable(
                name: "mts_user_account");
        }
    }
}
