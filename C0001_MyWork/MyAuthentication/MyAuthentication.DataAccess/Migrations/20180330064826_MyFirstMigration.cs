using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyAuthentication.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "my_module_type",
                columns: table => new
                {
                    module_type_code = table.Column<string>(maxLength: 32, nullable: false),
                    module_type_name = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_module_type", x => x.module_type_code);
                });

            migrationBuilder.CreateTable(
                name: "my_organization",
                columns: table => new
                {
                    organization_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user = table.Column<string>(maxLength: 32, nullable: true),
                    last_update_time = table.Column<DateTime>(nullable: false),
                    last_update_user = table.Column<string>(maxLength: 32, nullable: true),
                    login_organization_code = table.Column<string>(maxLength: 32, nullable: false),
                    organization_name = table.Column<string>(maxLength: 32, nullable: false),
                    status = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_organization", x => x.organization_id);
                });

            migrationBuilder.CreateTable(
                name: "my_system",
                columns: table => new
                {
                    system_code = table.Column<string>(maxLength: 32, nullable: false),
                    system_name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_system", x => x.system_code);
                });

            migrationBuilder.CreateTable(
                name: "my_user",
                columns: table => new
                {
                    user_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user = table.Column<string>(maxLength: 32, nullable: true),
                    last_update_time = table.Column<DateTime>(nullable: false),
                    last_update_user = table.Column<string>(maxLength: 32, nullable: true),
                    login_user_code = table.Column<string>(maxLength: 32, nullable: false),
                    organization_id = table.Column<long>(nullable: false),
                    status = table.Column<string>(maxLength: 16, nullable: true),
                    user_name = table.Column<string>(maxLength: 32, nullable: false),
                    user_password = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_user", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_my_user_my_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "my_organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "my_module",
                columns: table => new
                {
                    module_code = table.Column<string>(maxLength: 32, nullable: false),
                    module_expand = table.Column<string>(maxLength: 256, nullable: true),
                    module_name = table.Column<string>(maxLength: 32, nullable: false),
                    module_type_code = table.Column<string>(maxLength: 32, nullable: false),
                    system_code = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_module", x => x.module_code);
                    table.ForeignKey(
                        name: "FK_my_module_my_module_type_module_type_code",
                        column: x => x.module_type_code,
                        principalTable: "my_module_type",
                        principalColumn: "module_type_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_my_module_my_system_system_code",
                        column: x => x.system_code,
                        principalTable: "my_system",
                        principalColumn: "system_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "my_role",
                columns: table => new
                {
                    role_code = table.Column<string>(maxLength: 32, nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user = table.Column<string>(maxLength: 32, nullable: true),
                    last_update_time = table.Column<DateTime>(nullable: false),
                    last_update_user = table.Column<string>(maxLength: 32, nullable: true),
                    role_name = table.Column<string>(maxLength: 32, nullable: false),
                    status = table.Column<string>(maxLength: 16, nullable: true),
                    system_code = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_role", x => x.role_code);
                    table.ForeignKey(
                        name: "FK_my_role_my_system_system_code",
                        column: x => x.system_code,
                        principalTable: "my_system",
                        principalColumn: "system_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "my_system_user",
                columns: table => new
                {
                    system_code = table.Column<string>(maxLength: 32, nullable: false),
                    user_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_system_user", x => new { x.system_code, x.user_id });
                    table.ForeignKey(
                        name: "FK_my_system_user_my_system_system_code",
                        column: x => x.system_code,
                        principalTable: "my_system",
                        principalColumn: "system_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_my_system_user_my_user_user_id",
                        column: x => x.user_id,
                        principalTable: "my_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "my_action",
                columns: table => new
                {
                    action_code = table.Column<string>(maxLength: 32, nullable: false),
                    action_expand = table.Column<string>(maxLength: 256, nullable: false),
                    action_name = table.Column<string>(maxLength: 32, nullable: false),
                    default_useable = table.Column<bool>(nullable: false),
                    module_code = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_action", x => x.action_code);
                    table.ForeignKey(
                        name: "FK_my_action_my_module_module_code",
                        column: x => x.module_code,
                        principalTable: "my_module",
                        principalColumn: "module_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "my_role_module",
                columns: table => new
                {
                    role_code = table.Column<string>(maxLength: 32, nullable: false),
                    module_code = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_role_module", x => new { x.role_code, x.module_code });
                    table.ForeignKey(
                        name: "FK_my_role_module_my_module_module_code",
                        column: x => x.module_code,
                        principalTable: "my_module",
                        principalColumn: "module_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_my_role_module_my_role_role_code",
                        column: x => x.role_code,
                        principalTable: "my_role",
                        principalColumn: "role_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "my_user_role",
                columns: table => new
                {
                    user_id = table.Column<long>(nullable: false),
                    role_code = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_user_role", x => new { x.user_id, x.role_code });
                    table.ForeignKey(
                        name: "FK_my_user_role_my_role_role_code",
                        column: x => x.role_code,
                        principalTable: "my_role",
                        principalColumn: "role_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_my_user_role_my_user_user_id",
                        column: x => x.user_id,
                        principalTable: "my_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "my_role_action",
                columns: table => new
                {
                    role_code = table.Column<string>(maxLength: 32, nullable: false),
                    action_code = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_role_action", x => new { x.role_code, x.action_code });
                    table.ForeignKey(
                        name: "FK_my_role_action_my_action_action_code",
                        column: x => x.action_code,
                        principalTable: "my_action",
                        principalColumn: "action_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_my_role_action_my_role_role_code",
                        column: x => x.role_code,
                        principalTable: "my_role",
                        principalColumn: "role_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_my_action_module_code",
                table: "my_action",
                column: "module_code");

            migrationBuilder.CreateIndex(
                name: "IX_my_module_module_type_code",
                table: "my_module",
                column: "module_type_code");

            migrationBuilder.CreateIndex(
                name: "IX_my_module_system_code",
                table: "my_module",
                column: "system_code");

            migrationBuilder.CreateIndex(
                name: "IX_my_role_system_code",
                table: "my_role",
                column: "system_code");

            migrationBuilder.CreateIndex(
                name: "IX_my_role_action_action_code",
                table: "my_role_action",
                column: "action_code");

            migrationBuilder.CreateIndex(
                name: "IX_my_role_module_module_code",
                table: "my_role_module",
                column: "module_code");

            migrationBuilder.CreateIndex(
                name: "IX_my_system_user_user_id",
                table: "my_system_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_my_user_organization_id",
                table: "my_user",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_my_user_role_role_code",
                table: "my_user_role",
                column: "role_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "my_role_action");

            migrationBuilder.DropTable(
                name: "my_role_module");

            migrationBuilder.DropTable(
                name: "my_system_user");

            migrationBuilder.DropTable(
                name: "my_user_role");

            migrationBuilder.DropTable(
                name: "my_action");

            migrationBuilder.DropTable(
                name: "my_role");

            migrationBuilder.DropTable(
                name: "my_user");

            migrationBuilder.DropTable(
                name: "my_module");

            migrationBuilder.DropTable(
                name: "my_organization");

            migrationBuilder.DropTable(
                name: "my_module_type");

            migrationBuilder.DropTable(
                name: "my_system");
        }
    }
}
