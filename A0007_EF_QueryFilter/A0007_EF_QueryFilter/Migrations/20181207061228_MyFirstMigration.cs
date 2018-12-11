using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace A0007_EF_QueryFilter.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "document_type",
                columns: table => new
                {
                    document_type_code = table.Column<string>(maxLength: 32, nullable: false),
                    status = table.Column<string>(maxLength: 16, nullable: true),
                    create_user = table.Column<string>(maxLength: 32, nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    last_update_user = table.Column<string>(maxLength: 32, nullable: true),
                    last_update_time = table.Column<DateTime>(nullable: false),
                    document_type_name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type", x => x.document_type_code);
                });

            migrationBuilder.CreateTable(
                name: "test_data",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 16, nullable: true),
                    phone = table.Column<string>(maxLength: 16, nullable: true),
                    address = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    document_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    status = table.Column<string>(maxLength: 16, nullable: true),
                    create_user = table.Column<string>(maxLength: 32, nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    last_update_user = table.Column<string>(maxLength: 32, nullable: true),
                    last_update_time = table.Column<DateTime>(nullable: false),
                    document_type_code = table.Column<string>(maxLength: 32, nullable: true),
                    document_title = table.Column<string>(maxLength: 64, nullable: false),
                    document_content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.document_id);
                    table.ForeignKey(
                        name: "FK_document_document_type_document_type_code",
                        column: x => x.document_type_code,
                        principalTable: "document_type",
                        principalColumn: "document_type_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_document_document_type_code",
                table: "document",
                column: "document_type_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "test_data");

            migrationBuilder.DropTable(
                name: "document_type");
        }
    }
}
