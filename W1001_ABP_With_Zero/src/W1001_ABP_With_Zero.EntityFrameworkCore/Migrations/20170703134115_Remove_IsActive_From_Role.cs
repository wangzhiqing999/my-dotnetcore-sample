using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace W1001_ABP_With_Zero.Migrations
{
    public partial class Remove_IsActive_From_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AbpRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AbpRoles",
                nullable: false,
                defaultValue: false);
        }
    }
}
