using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P0002_MyEtf.Migrations
{
    public partial class MyEtfEma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "etf_day_ema",
                schema: "my_etf",
                columns: table => new
                {
                    etf_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ema12 = table.Column<decimal>(type: "numeric", nullable: false),
                    ema26 = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etf_day_ema", x => new { x.etf_code, x.trading_date });
                    table.ForeignKey(
                        name: "FK_etf_day_ema_etf_master_etf_code",
                        column: x => x.etf_code,
                        principalSchema: "my_etf",
                        principalTable: "etf_master",
                        principalColumn: "etf_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "etf_week_ema",
                schema: "my_etf",
                columns: table => new
                {
                    etf_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ema12 = table.Column<decimal>(type: "numeric", nullable: false),
                    ema26 = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etf_week_ema", x => new { x.etf_code, x.trading_date });
                    table.ForeignKey(
                        name: "FK_etf_week_ema_etf_master_etf_code",
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
                name: "etf_day_ema",
                schema: "my_etf");

            migrationBuilder.DropTable(
                name: "etf_week_ema",
                schema: "my_etf");
        }
    }
}
