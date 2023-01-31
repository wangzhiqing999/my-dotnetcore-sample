using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace P0002_MyGrid.Migrations
{
    public partial class MyGridInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "my_grid");

            migrationBuilder.CreateTable(
                name: "grid",
                schema: "my_grid",
                columns: table => new
                {
                    item_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    grid_no = table.Column<int>(type: "integer", nullable: false),
                    buy_price = table.Column<decimal>(type: "numeric", nullable: false),
                    sell_price = table.Column<decimal>(type: "numeric", nullable: false),
                    hold = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grid", x => new { x.item_code, x.grid_no });
                });

            migrationBuilder.CreateTable(
                name: "grid_transaction",
                schema: "my_grid",
                columns: table => new
                {
                    transaction_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    grid_no = table.Column<int>(type: "integer", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    transaction_price = table.Column<decimal>(type: "numeric", nullable: false),
                    transaction_quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grid_transaction", x => x.transaction_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grid",
                schema: "my_grid");

            migrationBuilder.DropTable(
                name: "grid_transaction",
                schema: "my_grid");
        }
    }
}
