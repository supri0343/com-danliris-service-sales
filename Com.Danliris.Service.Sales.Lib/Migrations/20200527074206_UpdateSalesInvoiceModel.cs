using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class UpdateSalesInvoiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uom",
                table: "SalesInvoiceItems",
                newName: "ItemUom");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "SalesInvoiceItems",
                newName: "QuantityItem");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "SalesInvoiceItems",
                newName: "QuantityPacking");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentId",
                table: "SalesInvoiceDetails",
                newName: "ShippingOutId");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentCode",
                table: "SalesInvoiceDetails",
                newName: "BonNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityPacking",
                table: "SalesInvoiceItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "QuantityItem",
                table: "SalesInvoiceItems",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "ItemUom",
                table: "SalesInvoiceItems",
                newName: "Uom");

            migrationBuilder.RenameColumn(
                name: "ShippingOutId",
                table: "SalesInvoiceDetails",
                newName: "ShipmentDocumentId");

            migrationBuilder.RenameColumn(
                name: "BonNo",
                table: "SalesInvoiceDetails",
                newName: "ShipmentDocumentCode");
        }
    }
}
