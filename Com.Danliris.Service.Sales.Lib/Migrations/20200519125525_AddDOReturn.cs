using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddDOReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOReturnItems_DOReturnDetailItems_DOReturnDetailItemModelId",
                table: "DOReturnItems");

            migrationBuilder.DropIndex(
                name: "IX_DOReturnItems_DOReturnDetailItemModelId",
                table: "DOReturnItems");

            //migrationBuilder.DropColumn(
            //    name: "Amount",
            //    table: "DOReturnItems");

            migrationBuilder.DropColumn(
                name: "DOReturnDetailItemModelId",
                table: "DOReturnItems");

            //migrationBuilder.DropColumn(
            //    name: "Price",
            //    table: "DOReturnItems");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "DOReturns",
                newName: "LTKPNo");

            migrationBuilder.RenameColumn(
                name: "LKTPNo",
                table: "DOReturns",
                newName: "DOReturnType");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DOReturns",
                newName: "DOReturnDate");

            //migrationBuilder.RenameColumn(
            //    name: "ShipmentDocumentId",
            //    table: "DOReturnDetailItems",
            //    newName: "DOSalesId");

            //migrationBuilder.RenameColumn(
            //    name: "ShipmentDocumentCode",
            //    table: "DOReturnDetailItems",
            //    newName: "DOSalesNo");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryOrderType",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "DOReturnItems",
                nullable: true,
                oldClrType: typeof(double));

            //migrationBuilder.AddColumn<string>(
            //    name: "ShipmentDocumentCode",
            //    table: "DOReturnItems",
            //    maxLength: 255,
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "ShipmentDocumentId",
            //    table: "DOReturnItems",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOrderType",
                table: "SalesInvoices");

            //migrationBuilder.DropColumn(
            //    name: "ShipmentDocumentCode",
            //    table: "DOReturnItems");

            //migrationBuilder.DropColumn(
            //    name: "ShipmentDocumentId",
            //    table: "DOReturnItems");

            migrationBuilder.RenameColumn(
                name: "LTKPNo",
                table: "DOReturns",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "DOReturnType",
                table: "DOReturns",
                newName: "LKTPNo");

            migrationBuilder.RenameColumn(
                name: "DOReturnDate",
                table: "DOReturns",
                newName: "Date");

            //migrationBuilder.RenameColumn(
            //    name: "DOSalesNo",
            //    table: "DOReturnDetailItems",
            //    newName: "ShipmentDocumentCode");

            //migrationBuilder.RenameColumn(
            //    name: "DOSalesId",
            //    table: "DOReturnDetailItems",
            //    newName: "ShipmentDocumentId");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "DOReturnItems",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            //migrationBuilder.AddColumn<double>(
            //    name: "Amount",
            //    table: "DOReturnItems",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "DOReturnDetailItemModelId",
                table: "DOReturnItems",
                nullable: true);

            //migrationBuilder.AddColumn<double>(
            //    name: "Price",
            //    table: "DOReturnItems",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_DOReturnItems_DOReturnDetailItemModelId",
                table: "DOReturnItems",
                column: "DOReturnDetailItemModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DOReturnItems_DOReturnDetailItems_DOReturnDetailItemModelId",
                table: "DOReturnItems",
                column: "DOReturnDetailItemModelId",
                principalTable: "DOReturnDetailItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
