using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddingSalesAndUnitInSalesInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sales",
                table: "SalesInvoices",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "SalesInvoices",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "SalesInvoices",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "SalesInvoices",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sales",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "SalesInvoices");
        }
    }
}
