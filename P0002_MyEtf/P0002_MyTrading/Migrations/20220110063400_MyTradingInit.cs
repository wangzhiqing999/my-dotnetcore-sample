using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace P0002_MyTrading.Migrations
{
    public partial class MyTradingInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "my_trading");

            migrationBuilder.CreateTable(
                name: "simple_trading",
                schema: "my_trading",
                columns: table => new
                {
                    trading_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    trading_item_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_quantity = table.Column<int>(type: "integer", nullable: false),
                    open_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    open_price = table.Column<decimal>(type: "numeric", nullable: false),
                    close_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    close_price = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simple_trading", x => x.trading_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "simple_trading",
                schema: "my_trading");
        }
    }
}
