using Microsoft.EntityFrameworkCore.Migrations;

namespace MySample.Migrations
{
    public partial class MySample_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "my_sample_test_school",
                columns: table => new
                {
                    school_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    school_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_sample_test_school", x => x.school_id);
                });

            migrationBuilder.CreateTable(
                name: "my_sample_test_teacher",
                columns: table => new
                {
                    teacher_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    school_id = table.Column<int>(nullable: false),
                    teacher_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_sample_test_teacher", x => x.teacher_id);
                    table.ForeignKey(
                        name: "FK_my_sample_test_teacher_my_sample_test_school_school_id",
                        column: x => x.school_id,
                        principalTable: "my_sample_test_school",
                        principalColumn: "school_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_my_sample_test_teacher_school_id",
                table: "my_sample_test_teacher",
                column: "school_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "my_sample_test_teacher");

            migrationBuilder.DropTable(
                name: "my_sample_test_school");
        }
    }
}
