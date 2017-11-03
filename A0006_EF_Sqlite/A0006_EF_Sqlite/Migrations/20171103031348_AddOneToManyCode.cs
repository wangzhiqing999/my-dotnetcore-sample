using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace A0006_EF_Sqlite.Migrations
{
    public partial class AddOneToManyCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "document_type",
                columns: table => new
                {
                    document_type_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    document_type_name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type", x => x.document_type_code);
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    document_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    document_content = table.Column<string>(type: "TEXT", nullable: true),
                    document_title = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    document_type_code = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true)
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
                name: "document_type");
        }
    }
}
