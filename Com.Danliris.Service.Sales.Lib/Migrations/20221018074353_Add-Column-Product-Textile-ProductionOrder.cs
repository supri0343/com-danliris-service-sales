using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddColumnProductTextileProductionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductTextileCode",
                table: "ProductionOrder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTextileId",
                table: "ProductionOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTextileName",
                table: "ProductionOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTextileCode",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "ProductTextileId",
                table: "ProductionOrder");

            migrationBuilder.DropColumn(
                name: "ProductTextileName",
                table: "ProductionOrder");
        }
    }
}
