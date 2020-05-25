using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class UpdateDoSalesStorageCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DOSalesCategory",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageCode",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "DOSales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StorageName",
                table: "DOSales",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageUnit",
                table: "DOSales",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOSalesCategory",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "StorageCode",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "StorageName",
                table: "DOSales");

            migrationBuilder.DropColumn(
                name: "StorageUnit",
                table: "DOSales");
        }
    }
}
