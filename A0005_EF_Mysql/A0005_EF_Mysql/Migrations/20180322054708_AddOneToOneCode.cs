using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace A0005_EF_Mysql.Migrations
{
    public partial class AddOneToOneCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mr_user_exp",
                columns: table => new
                {
                    user_code = table.Column<string>(maxLength: 32, nullable: false),
                    user_exp_data = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_user_exp", x => x.user_code);
                    table.ForeignKey(
                        name: "FK_mr_user_exp_mr_user_user_code",
                        column: x => x.user_code,
                        principalTable: "mr_user",
                        principalColumn: "user_code",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mr_user_exp");
        }
    }
}
