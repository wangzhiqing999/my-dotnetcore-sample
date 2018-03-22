using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace A0005_EF_Mysql.Migrations
{
    public partial class AddManyToManyCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mr_role",
                columns: table => new
                {
                    role_code = table.Column<string>(maxLength: 32, nullable: false),
                    role_name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_role", x => x.role_code);
                });

            migrationBuilder.CreateTable(
                name: "mr_user",
                columns: table => new
                {
                    user_code = table.Column<string>(maxLength: 32, nullable: false),
                    user_name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_user", x => x.user_code);
                });

            migrationBuilder.CreateTable(
                name: "mr_user_role",
                columns: table => new
                {
                    user_code = table.Column<string>(maxLength: 32, nullable: false),
                    role_code = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_user_role", x => new { x.user_code, x.role_code });
                    table.ForeignKey(
                        name: "FK_mr_user_role_mr_role_role_code",
                        column: x => x.role_code,
                        principalTable: "mr_role",
                        principalColumn: "role_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mr_user_role_mr_user_user_code",
                        column: x => x.user_code,
                        principalTable: "mr_user",
                        principalColumn: "user_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mr_user_role_role_code",
                table: "mr_user_role",
                column: "role_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mr_user_role");

            migrationBuilder.DropTable(
                name: "mr_role");

            migrationBuilder.DropTable(
                name: "mr_user");
        }
    }
}
