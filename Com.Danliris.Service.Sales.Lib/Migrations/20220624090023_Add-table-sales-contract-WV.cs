using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddtablesalescontractWV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Claim",
                table: "WeavingSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LatePayment",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LateReturn",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claim",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "LatePayment",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "LateReturn",
                table: "WeavingSalesContract");
        }
    }
}
