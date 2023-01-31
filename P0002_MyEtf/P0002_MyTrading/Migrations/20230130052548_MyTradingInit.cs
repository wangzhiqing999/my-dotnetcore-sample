using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace P0002_MyTrading.Migrations
{
    public partial class MyTradingInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "my_trading");

            migrationBuilder.CreateTable(
                name: "holding",
                schema: "my_trading",
                columns: table => new
                {
                    item_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    item_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    source_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    reader_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_holding", x => x.item_code);
                });

            migrationBuilder.CreateTable(
                name: "simple_trading",
                schema: "my_trading",
                columns: table => new
                {
                    trading_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    trading_item_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_quantity = table.Column<int>(type: "integer", nullable: false),
                    open_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    open_price = table.Column<decimal>(type: "numeric", nullable: false),
                    close_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    close_price = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simple_trading", x => x.trading_id);
                });

            migrationBuilder.CreateTable(
                name: "holding_log",
                schema: "my_trading",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    log_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_holding_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_holding_log_holding_item_code",
                        column: x => x.item_code,
                        principalSchema: "my_trading",
                        principalTable: "holding",
                        principalColumn: "item_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_holding_log_item_code",
                schema: "my_trading",
                table: "holding_log",
                column: "item_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "holding_log",
                schema: "my_trading");

            migrationBuilder.DropTable(
                name: "simple_trading",
                schema: "my_trading");

            migrationBuilder.DropTable(
                name: "holding",
                schema: "my_trading");
        }
    }
}
