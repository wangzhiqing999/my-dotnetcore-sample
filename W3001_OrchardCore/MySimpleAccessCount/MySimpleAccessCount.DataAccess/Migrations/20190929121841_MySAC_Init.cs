using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MySimpleAccessCount.Migrations
{
    public partial class MySAC_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "my_page_access_count",
                columns: table => new
                {
                    page_code = table.Column<string>(maxLength: 16, nullable: false),
                    page_name = table.Column<string>(maxLength: 64, nullable: false),
                    init_access_count = table.Column<int>(nullable: false),
                    real_access_count = table.Column<int>(nullable: false),
                    access_multiple = table.Column<byte>(nullable: false),
                    is_save_daily_count = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_page_access_count", x => x.page_code);
                });

            migrationBuilder.CreateTable(
                name: "my_page_daily_access_count",
                columns: table => new
                {
                    page_code = table.Column<string>(maxLength: 16, nullable: false),
                    access_date = table.Column<DateTime>(nullable: false),
                    real_access_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_page_daily_access_count", x => new { x.page_code, x.access_date });
                    table.ForeignKey(
                        name: "FK_my_page_daily_access_count_my_page_access_count_page_code",
                        column: x => x.page_code,
                        principalTable: "my_page_access_count",
                        principalColumn: "page_code",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "my_page_daily_access_count");

            migrationBuilder.DropTable(
                name: "my_page_access_count");
        }
    }
}
