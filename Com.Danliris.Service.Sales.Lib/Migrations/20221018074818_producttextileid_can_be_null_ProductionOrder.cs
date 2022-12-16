using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class producttextileid_can_be_null_ProductionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductTextileId",
                table: "ProductionOrder",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductTextileId",
                table: "ProductionOrder",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
