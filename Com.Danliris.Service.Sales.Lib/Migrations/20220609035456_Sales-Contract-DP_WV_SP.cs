using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class SalesContractDP_WV_SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerJob",
                table: "WeavingSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DownPayments",
                table: "WeavingSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                table: "WeavingSalesContract",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialID",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "WeavingSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaterialPrice",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MaterialTags",
                table: "WeavingSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethods",
                table: "WeavingSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceDP",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeCode",
                table: "WeavingSalesContract",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "WeavingSalesContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VatId",
                table: "WeavingSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VatRate",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "precentageDP",
                table: "WeavingSalesContract",
                nullable: false,
                defaultValue: 0.0);

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

            migrationBuilder.AddColumn<string>(
                name: "VatId",
                table: "SpinningSalesContract",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VatRate",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "precentageDP",
                table: "SpinningSalesContract",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "BuyerJob",
                table: "FinishingPrintingSalesContracts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DownPayments",
                table: "FinishingPrintingSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethods",
                table: "FinishingPrintingSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceDP",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeCode",
                table: "FinishingPrintingSalesContracts",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "FinishingPrintingSalesContracts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VatId",
                table: "FinishingPrintingSalesContracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VatRate",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "precentageDP",
                table: "FinishingPrintingSalesContracts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerJob",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "DownPayments",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialID",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "MaterialTags",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "PaymentMethods",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "PriceDP",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductTypeCode",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "VatId",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "WeavingSalesContract");

            migrationBuilder.DropColumn(
                name: "precentageDP",
                table: "WeavingSalesContract");

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
                name: "VatId",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "precentageDP",
                table: "SpinningSalesContract");

            migrationBuilder.DropColumn(
                name: "BuyerJob",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "DownPayments",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "PaymentMethods",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "PriceDP",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ProductTypeCode",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "VatId",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "FinishingPrintingSalesContracts");

            migrationBuilder.DropColumn(
                name: "precentageDP",
                table: "FinishingPrintingSalesContracts");
        }
    }
}
