using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace W1001_ABP_With_Zero.Migrations
{
    public partial class AddIMayHaveTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AppTasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AppOthers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppTasks");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppOthers");
        }
    }
}
