using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A0006_EF_Sqlite_V8.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "test_data",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    phone = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    address = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    detail_data = table.Column<string>(type: "TEXT", nullable: false),
                    created_time = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_data", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test_data");
        }
    }
}
