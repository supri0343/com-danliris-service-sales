using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Add_ProductType_JobBuyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerJob",
                table: "FinishingPrintingSalesContracts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeCode",
                table: "FinishingPrintingSalesContracts",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "FinishingPrintingSalesContracts",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerJob",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ProductTypeCode",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
