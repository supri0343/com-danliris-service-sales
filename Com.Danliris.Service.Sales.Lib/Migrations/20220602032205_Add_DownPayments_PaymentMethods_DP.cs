using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Add_DownPayments_PaymentMethods_DP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DownPayments",
                table: "FinishingPrintingSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethods",
                table: "FinishingPrintingSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceDP",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "DownPayments",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "PaymentMethods",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "PriceDP",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
