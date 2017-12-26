using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace W1100_Page.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinanceDatas",
                columns: table => new
                {
                    finance_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(maxLength: 32, nullable: false),
                    CountryName = table.Column<string>(maxLength: 32, nullable: false),
                    CurrentValue = table.Column<string>(maxLength: 32, nullable: true),
                    FinanceDateTime = table.Column<DateTime>(nullable: false),
                    more_url = table.Column<string>(maxLength: 64, nullable: true),
                    Predict = table.Column<string>(maxLength: 32, nullable: true),
                    Previous = table.Column<string>(maxLength: 32, nullable: true),
                    Profitable = table.Column<string>(maxLength: 32, nullable: true),
                    Unprofitable = table.Column<string>(maxLength: 32, nullable: true),
                    Weightiness = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceDatas", x => x.finance_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinanceDatas");
        }
    }
}
