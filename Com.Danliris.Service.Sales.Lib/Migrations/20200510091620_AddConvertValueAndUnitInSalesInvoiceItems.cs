using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddConvertValueAndUnitInSalesInvoiceItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConvertUnit",
                table: "SalesInvoiceItems",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ConvertValue",
                table: "SalesInvoiceItems",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConvertUnit",
                table: "SalesInvoiceItems");

            migrationBuilder.DropColumn(
                name: "ConvertValue",
                table: "SalesInvoiceItems");
        }
    }
}
