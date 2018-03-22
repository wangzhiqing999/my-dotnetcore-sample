using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace W1010_ABP_NetCode2.Migrations
{
    public partial class AddOthers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppOthers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creation_user = table.Column<long>(nullable: true),
                    deleter_user = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    lastmodification_time = table.Column<DateTime>(nullable: true),
                    lastmodifier_user = table.Column<long>(nullable: true),
                    other_name = table.Column<string>(maxLength: 64, nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOthers", x => x.Id);
                });

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
                name: "AppOthers");

            migrationBuilder.DropTable(
                name: "AppTestUserType");
        }
    }
}
