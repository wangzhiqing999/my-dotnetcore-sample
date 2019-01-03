using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MySSO.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sso_user_category",
                columns: table => new
                {
                    user_category_code = table.Column<string>(maxLength: 32, nullable: false),
                    user_category_name = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sso_user_category", x => x.user_category_code);
                });

            migrationBuilder.CreateTable(
                name: "sso_user",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    user_category_code = table.Column<string>(maxLength: 32, nullable: true),
                    user_name = table.Column<string>(maxLength: 32, nullable: true),
                    user_password = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sso_user", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_sso_user_sso_user_category_user_category_code",
                        column: x => x.user_category_code,
                        principalTable: "sso_user_category",
                        principalColumn: "user_category_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sso_user_token",
                columns: table => new
                {
                    token_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    start_time = table.Column<DateTime>(nullable: false),
                    expired_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sso_user_token", x => x.token_id);
                    table.ForeignKey(
                        name: "FK_sso_user_token_sso_user_user_id",
                        column: x => x.user_id,
                        principalTable: "sso_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sso_user_user_category_code",
                table: "sso_user",
                column: "user_category_code");

            migrationBuilder.CreateIndex(
                name: "IX_sso_user_token_user_id",
                table: "sso_user_token",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sso_user_token");

            migrationBuilder.DropTable(
                name: "sso_user");

            migrationBuilder.DropTable(
                name: "sso_user_category");
        }
    }
}
