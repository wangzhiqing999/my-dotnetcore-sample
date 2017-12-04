using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace A0040_React.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auot_report_master",
                columns: table => new
                {
                    auto_report_master_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    auto_report_days = table.Column<int>(nullable: false),
                    auto_report_type = table.Column<int>(nullable: false),
                    auto_report_mail_body = table.Column<string>(nullable: false),
                    auto_report_mail_subject = table.Column<string>(maxLength: 256, nullable: false),
                    auto_report_hour_min = table.Column<int>(nullable: false),
                    auto_report_mail_cc = table.Column<string>(maxLength: 512, nullable: true),
                    auto_report_mail_to = table.Column<string>(maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auot_report_master", x => x.auto_report_master_id);
                });

            migrationBuilder.CreateTable(
                name: "cr_report",
                columns: table => new
                {
                    report_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    report_file_name = table.Column<string>(maxLength: 64, nullable: false),
                    report_name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cr_report", x => x.report_id);
                });

            migrationBuilder.CreateTable(
                name: "auot_report_detail",
                columns: table => new
                {
                    auto_report_detail_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    auto_report_master_id = table.Column<long>(nullable: false),
                    report_id = table.Column<int>(nullable: false),
                    report_parameter = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auot_report_detail", x => x.auto_report_detail_id);
                    table.ForeignKey(
                        name: "FK_auot_report_detail_auot_report_master_auto_report_master_id",
                        column: x => x.auto_report_master_id,
                        principalTable: "auot_report_master",
                        principalColumn: "auto_report_master_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_auot_report_detail_cr_report_report_id",
                        column: x => x.report_id,
                        principalTable: "cr_report",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "auot_report_detail_file",
                columns: table => new
                {
                    auto_report_detail_file_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    auto_report_detail_id = table.Column<long>(nullable: false),
                    auto_report_format = table.Column<int>(nullable: false),
                    auto_report_file_name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auot_report_detail_file", x => x.auto_report_detail_file_id);
                    table.ForeignKey(
                        name: "FK_auot_report_detail_file_auot_report_detail_auto_report_detail_id",
                        column: x => x.auto_report_detail_id,
                        principalTable: "auot_report_detail",
                        principalColumn: "auto_report_detail_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_auot_report_detail_auto_report_master_id",
                table: "auot_report_detail",
                column: "auto_report_master_id");

            migrationBuilder.CreateIndex(
                name: "IX_auot_report_detail_report_id",
                table: "auot_report_detail",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_auot_report_detail_file_auto_report_detail_id",
                table: "auot_report_detail_file",
                column: "auto_report_detail_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auot_report_detail_file");

            migrationBuilder.DropTable(
                name: "auot_report_detail");

            migrationBuilder.DropTable(
                name: "auot_report_master");

            migrationBuilder.DropTable(
                name: "cr_report");
        }
    }
}
