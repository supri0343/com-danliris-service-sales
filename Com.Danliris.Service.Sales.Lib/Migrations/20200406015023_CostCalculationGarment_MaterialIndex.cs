using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class CostCalculationGarment_MaterialIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaterialIndex",
                table: "CostCalculationGarment_Materials",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialIndex",
                table: "CostCalculationGarment_Materials");
        }
    }
}
