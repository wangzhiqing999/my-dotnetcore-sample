using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyWork.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_mw_stock_pool_detail_stock_code",
                table: "mw_stock_pool_detail",
                column: "stock_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mw_stock_pool_detail");

            migrationBuilder.DropTable(
                name: "mw_stock_info");

            migrationBuilder.DropTable(
                name: "mw_stock_pool");
        }
    }
}
