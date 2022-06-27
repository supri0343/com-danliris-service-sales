using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddtablesalescontractDP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Claim",
                table: "FinishingPrintingSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LatePayment",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LateReturn",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claim",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "LatePayment",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "LateReturn",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
