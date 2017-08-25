using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace W1001_ABP_With_Zero.Migrations
{
    public partial class AddTestUserTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTestUserType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    user_type_name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTestUserType", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTestUserType");
        }
    }
}
