using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class Add_ProductType_PaymentMethod_DownPayment_SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DownPayments",
                table: "SpinningSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                table: "SpinningSalesContract",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionCode",
                table: "SpinningSalesContract",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialConstructionId",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialConstructionName",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialID",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaterialPrice",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialTags",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethods",
                table: "SpinningSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceDP",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeCode",
                table: "SpinningSalesContract",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "SpinningSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "precentageDP",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "DownPayments",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionCode",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionId",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialConstructionName",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialID",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialTags",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "PaymentMethods",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "PriceDP",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductTypeCode",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "precentageDP",
                table: "SpinningSalesContract");
        }
    }
}
