using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddtablesalescontractSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Claim",
                table: "SpinningSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LatePayment",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LateReturn",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claim",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "LatePayment",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "LateReturn",
                table: "SpinningSalesContract");
        }
    }
}
