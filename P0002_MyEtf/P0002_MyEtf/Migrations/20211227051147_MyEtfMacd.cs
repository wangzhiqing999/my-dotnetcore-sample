using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P0002_MyEtf.Migrations
{
    public partial class MyEtfMacd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "etf_day_macd",
                schema: "my_etf",
                columns: table => new
                {
                    etf_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    diff = table.Column<decimal>(type: "numeric", nullable: false),
                    dea = table.Column<decimal>(type: "numeric", nullable: false),
                    macd = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etf_day_macd", x => new { x.etf_code, x.trading_date });
                    table.ForeignKey(
                        name: "FK_etf_day_macd_etf_master_etf_code",
                        column: x => x.etf_code,
                        principalSchema: "my_etf",
                        principalTable: "etf_master",
                        principalColumn: "etf_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "etf_week_macd",
                schema: "my_etf",
                columns: table => new
                {
                    etf_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    diff = table.Column<decimal>(type: "numeric", nullable: false),
                    dea = table.Column<decimal>(type: "numeric", nullable: false),
                    macd = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etf_week_macd", x => new { x.etf_code, x.trading_date });
                    table.ForeignKey(
                        name: "FK_etf_week_macd_etf_master_etf_code",
                        column: x => x.etf_code,
                        principalSchema: "my_etf",
                        principalTable: "etf_master",
                        principalColumn: "etf_code",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "etf_day_macd",
                schema: "my_etf");

            migrationBuilder.DropTable(
                name: "etf_week_macd",
                schema: "my_etf");
        }
    }
}
