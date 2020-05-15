using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class EnhanceDOReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DOReturnItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "DOReturnItems");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentId",
                table: "DOReturnDetailItems",
                newName: "DOSalesId");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentCode",
                table: "DOReturnDetailItems",
                newName: "DOSalesNo");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDocumentCode",
                table: "DOReturnItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentDocumentId",
                table: "DOReturnItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentDocumentCode",
                table: "DOReturnItems");

            migrationBuilder.DropColumn(
                name: "ShipmentDocumentId",
                table: "DOReturnItems");

            migrationBuilder.RenameColumn(
                name: "DOSalesNo",
                table: "DOReturnDetailItems",
                newName: "ShipmentDocumentCode");

            migrationBuilder.RenameColumn(
                name: "DOSalesId",
                table: "DOReturnDetailItems",
                newName: "ShipmentDocumentId");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "DOReturnItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "DOReturnItems",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
