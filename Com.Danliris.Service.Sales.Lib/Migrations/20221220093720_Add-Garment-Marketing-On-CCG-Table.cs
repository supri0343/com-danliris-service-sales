using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddGarmentMarketingOnCCGTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedMktBy",
                table: "CostCalculationGarments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedMktDate",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedMkt",
                table: "CostCalculationGarments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MarketingName",
                table: "CostCalculationGarments",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleName",
                table: "CostCalculationGarments",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedMktBy",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ApprovedMktDate",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "IsApprovedMkt",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "MarketingName",
                table: "CostCalculationGarments");

            migrationBuilder.DropColumn(
                name: "ResponsibleName",
                table: "CostCalculationGarments");
        }
    }
}
