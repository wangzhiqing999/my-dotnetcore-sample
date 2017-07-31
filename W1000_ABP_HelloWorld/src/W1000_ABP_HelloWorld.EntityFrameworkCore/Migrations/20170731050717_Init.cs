using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace W1000_ABP_HelloWorld.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sc_system_config_type",
                columns: table => new
                {
                    config_type_code = table.Column<string>(maxLength: 32, nullable: false),
                    config_type_class_name = table.Column<string>(maxLength: 128, nullable: true),
                    config_type_name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sc_system_config_type", x => x.config_type_code);
                });

            migrationBuilder.CreateTable(
                name: "sc_system_config_property",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    config_type_code = table.Column<string>(maxLength: 32, nullable: false),
                    display_order = table.Column<int>(nullable: false),
                    property_datatype = table.Column<string>(maxLength: 64, nullable: true),
                    property_desc = table.Column<string>(maxLength: 64, nullable: true),
                    property_name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sc_system_config_property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sc_system_config_property_sc_system_config_type_config_type_code",
                        column: x => x.config_type_code,
                        principalTable: "sc_system_config_type",
                        principalColumn: "config_type_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sc_system_config_value",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    config_code = table.Column<string>(maxLength: 32, nullable: true),
                    config_name = table.Column<string>(maxLength: 64, nullable: true),
                    config_type_code = table.Column<string>(maxLength: 32, nullable: false),
                    config_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sc_system_config_value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sc_system_config_value_sc_system_config_type_config_type_code",
                        column: x => x.config_type_code,
                        principalTable: "sc_system_config_type",
                        principalColumn: "config_type_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sc_system_config_property_config_type_code",
                table: "sc_system_config_property",
                column: "config_type_code");

            migrationBuilder.CreateIndex(
                name: "IX_sc_system_config_value_config_type_code",
                table: "sc_system_config_value",
                column: "config_type_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sc_system_config_property");

            migrationBuilder.DropTable(
                name: "sc_system_config_value");

            migrationBuilder.DropTable(
                name: "sc_system_config_type");
        }
    }
}
