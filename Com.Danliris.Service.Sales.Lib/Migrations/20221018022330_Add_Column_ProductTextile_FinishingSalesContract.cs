using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Add_Column_ProductTextile_FinishingSalesContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductTextileCode",
                table: "FinishingPrintingSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTextileId",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTextileName",
                table: "FinishingPrintingSalesContracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTextileCode",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ProductTextileId",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ProductTextileName",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
