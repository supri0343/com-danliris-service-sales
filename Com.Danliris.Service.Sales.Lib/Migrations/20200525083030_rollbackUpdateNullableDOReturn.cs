using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class rollbackUpdateNullableDOReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "DOSalesCategory",
            //    table: "DOSales",
            //    maxLength: 255,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "StorageCode",
            //    table: "DOSales",
            //    maxLength: 255,
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "StorageId",
            //    table: "DOSales",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<string>(
            //    name: "StorageName",
            //    table: "DOSales",
            //    maxLength: 255,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "StorageUnit",
            //    table: "DOSales",
            //    maxLength: 255,
            //    nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReturnFromId",
                table: "DOReturns",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AutoIncreament",
                table: "DOReturns",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShipmentDocumentId",
                table: "DOReturnItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SalesInvoiceId",
                table: "DOReturnDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DOSalesId",
                table: "DOReturnDetailItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "DOSalesCategory",
            //    table: "DOSales");

            //migrationBuilder.DropColumn(
            //    name: "StorageCode",
            //    table: "DOSales");

            //migrationBuilder.DropColumn(
            //    name: "StorageId",
            //    table: "DOSales");

            //migrationBuilder.DropColumn(
            //    name: "StorageName",
            //    table: "DOSales");

            //migrationBuilder.DropColumn(
            //    name: "StorageUnit",
            //    table: "DOSales");

            migrationBuilder.AlterColumn<int>(
                name: "ReturnFromId",
                table: "DOReturns",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "AutoIncreament",
                table: "DOReturns",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "ShipmentDocumentId",
                table: "DOReturnItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SalesInvoiceId",
                table: "DOReturnDetails",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DOSalesId",
                table: "DOReturnDetailItems",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
