using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class UpdateSalesInvoiceItemChangeUom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UomId",
                table: "SalesInvoiceItems");

            migrationBuilder.RenameColumn(
                name: "UomUnit",
                table: "SalesInvoiceItems",
                newName: "Uom");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "SalesInvoiceItems",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uom",
                table: "SalesInvoiceItems",
                newName: "UomUnit");

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "SalesInvoiceItems",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(double),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "UomId",
                table: "SalesInvoiceItems",
                nullable: false,
                defaultValue: 0);
        }
    }
}
