using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class EnhanceSalesReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginBankName",
                table: "SalesReceipts");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "SalesReceipts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "SalesReceipts");

            migrationBuilder.AddColumn<string>(
                name: "OriginBankName",
                table: "SalesReceipts",
                maxLength: 255,
                nullable: true);
        }
    }
}
