﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace A0009_EF_Postgres_V8.Migrations
{
    /// <inheritdoc />
    public partial class MyA0009_EF_Postgres_V8_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "document_type",
                columns: table => new
                {
                    document_type_code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    document_type_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type", x => x.document_type_code);
                });

            migrationBuilder.CreateTable(
                name: "mr_role",
                columns: table => new
                {
                    role_code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    role_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_role", x => x.role_code);
                });

            migrationBuilder.CreateTable(
                name: "mr_user",
                columns: table => new
                {
                    user_code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    user_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_user", x => x.user_code);
                });

            migrationBuilder.CreateTable(
                name: "test_data",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    address = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    document_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    document_type_code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    document_title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    document_content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.document_id);
                    table.ForeignKey(
                        name: "FK_document_document_type_document_type_code",
                        column: x => x.document_type_code,
                        principalTable: "document_type",
                        principalColumn: "document_type_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mr_user_exp",
                columns: table => new
                {
                    user_code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    user_exp_data = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "mr_user_role",
                columns: table => new
                {
                    user_code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    role_code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
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
                name: "IX_document_document_type_code",
                table: "document",
                column: "document_type_code");

            migrationBuilder.CreateIndex(
                name: "IX_mr_user_role_role_code",
                table: "mr_user_role",
                column: "role_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "mr_user_exp");

            migrationBuilder.DropTable(
                name: "mr_user_role");

            migrationBuilder.DropTable(
                name: "test_data");

            migrationBuilder.DropTable(
                name: "document_type");

            migrationBuilder.DropTable(
                name: "mr_role");

            migrationBuilder.DropTable(
                name: "mr_user");
        }
    }
}
