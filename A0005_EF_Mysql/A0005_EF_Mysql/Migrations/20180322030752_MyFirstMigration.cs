using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace A0005_EF_Mysql.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "test_data",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    address = table.Column<string>(maxLength: 64, nullable: true),
                    name = table.Column<string>(maxLength: 16, nullable: true),
                    phone = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_data", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test_data");
        }
    }
}
