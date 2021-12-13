using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D0010_MyTodo.Migrations
{
    public partial class MyInitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "my_todo_list",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    detail = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true),
                    importance = table.Column<byte>(type: "INTEGER", nullable: false),
                    closing_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    is_done = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_todo_list", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "my_todo_list");
        }
    }
}
