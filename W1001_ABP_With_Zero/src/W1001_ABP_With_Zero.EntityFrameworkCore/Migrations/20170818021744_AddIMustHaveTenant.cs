using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace W1001_ABP_With_Zero.Migrations
{
    public partial class AddIMustHaveTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "AppTasks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "AppTasks",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
