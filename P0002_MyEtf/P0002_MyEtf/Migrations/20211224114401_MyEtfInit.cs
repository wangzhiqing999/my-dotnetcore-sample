using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P0002_MyEtf.Migrations
{
    public partial class MyEtfInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "my_etf");

            migrationBuilder.CreateTable(
                name: "etf_master",
                schema: "my_etf",
                columns: table => new
                {
                    etf_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    etf_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etf_master", x => x.etf_code);
                });

            migrationBuilder.CreateTable(
                name: "etf_day_line",
                schema: "my_etf",
                columns: table => new
                {
                    etf_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    open_price = table.Column<decimal>(type: "numeric", nullable: false),
                    close_price = table.Column<decimal>(type: "numeric", nullable: false),
                    highest_price = table.Column<decimal>(type: "numeric", nullable: false),
                    lowest_price = table.Column<decimal>(type: "numeric", nullable: false),
                    volume = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etf_day_line", x => new { x.etf_code, x.trading_date });
                    table.ForeignKey(
                        name: "FK_etf_day_line_etf_master_etf_code",
                        column: x => x.etf_code,
                        principalSchema: "my_etf",
                        principalTable: "etf_master",
                        principalColumn: "etf_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "etf_day_tr",
                schema: "my_etf",
                columns: table => new
                {
                    etf_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tr = table.Column<decimal>(type: "numeric", nullable: false),
                    atr = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etf_day_tr", x => new { x.etf_code, x.trading_date });
                    table.ForeignKey(
                        name: "FK_etf_day_tr_etf_master_etf_code",
                        column: x => x.etf_code,
                        principalSchema: "my_etf",
                        principalTable: "etf_master",
                        principalColumn: "etf_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "etf_week_line",
                schema: "my_etf",
                columns: table => new
                {
                    etf_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    trading_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    open_price = table.Column<decimal>(type: "numeric", nullable: false),
                    close_price = table.Column<decimal>(type: "numeric", nullable: false),
                    highest_price = table.Column<decimal>(type: "numeric", nullable: false),
                    lowest_price = table.Column<decimal>(type: "numeric", nullable: false),
                    volume = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etf_week_line", x => new { x.etf_code, x.trading_date });
                    table.ForeignKey(
                        name: "FK_etf_week_line_etf_master_etf_code",
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
                name: "etf_day_line",
                schema: "my_etf");

            migrationBuilder.DropTable(
                name: "etf_day_tr",
                schema: "my_etf");

            migrationBuilder.DropTable(
                name: "etf_week_line",
                schema: "my_etf");

            migrationBuilder.DropTable(
                name: "etf_master",
                schema: "my_etf");
        }
    }
}
