using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class DeleteIdFieldInDOSalesDOReturnSalesInvoiceSalesReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesReceiptId",
                table: "SalesReceiptDetails");

            migrationBuilder.DropColumn(
                name: "SalesInvoiceDetailId",
                table: "SalesInvoiceItems");

            migrationBuilder.DropColumn(
                name: "SalesInvoiceId",
                table: "SalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "DOSalesId",
                table: "DOSalesLocalItems");

            migrationBuilder.DropColumn(
                name: "DOReturnDetailItemId",
                table: "DOReturnItemModel");

            migrationBuilder.DropColumn(
                name: "DOReturnId",
                table: "DOReturnDetailModel");

            migrationBuilder.DropColumn(
                name: "DOReturnDetailId",
                table: "DOReturnDetailItemModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesReceiptId",
                table: "SalesReceiptDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesInvoiceDetailId",
                table: "SalesInvoiceItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesInvoiceId",
                table: "SalesInvoiceDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOSalesId",
                table: "DOSalesLocalItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOReturnDetailItemId",
                table: "DOReturnItemModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOReturnId",
                table: "DOReturnDetailModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DOReturnDetailId",
                table: "DOReturnDetailItemModel",
                nullable: false,
                defaultValue: 0);
        }
    }
}
