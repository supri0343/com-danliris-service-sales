using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class EnhancementSalesInvoiceUpdateDetailandItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentDocumentCode",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "ShipmentDocumentId",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "SalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SalesInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "SalesInvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "UomUnit",
                table: "SalesInvoiceDetails",
                newName: "ShipmentDocumentCode");

            migrationBuilder.RenameColumn(
                name: "UomId",
                table: "SalesInvoiceDetails",
                newName: "ShipmentDocumentId");

            migrationBuilder.CreateTable(
                name: "SalesInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    ProductCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(maxLength: 255, nullable: true),
                    Quantity = table.Column<string>(maxLength: 255, nullable: true),
                    UomId = table.Column<int>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true),
                    Total = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    SalesInvoiceDetailId = table.Column<int>(nullable: false),
                    SalesInvoiceDetailModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceItems_SalesInvoiceDetails_SalesInvoiceDetailModelId",
                        column: x => x.SalesInvoiceDetailModelId,
                        principalTable: "SalesInvoiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceItems_SalesInvoiceDetailModelId",
                table: "SalesInvoiceItems",
                column: "SalesInvoiceDetailModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesInvoiceItems");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentId",
                table: "SalesInvoiceDetails",
                newName: "UomId");

            migrationBuilder.RenameColumn(
                name: "ShipmentDocumentCode",
                table: "SalesInvoiceDetails",
                newName: "UomUnit");

            migrationBuilder.AddColumn<string>(
                name: "ShipmentDocumentCode",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentDocumentId",
                table: "SalesInvoices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "SalesInvoiceDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "SalesInvoiceDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "SalesInvoiceDetails",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SalesInvoiceDetails",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "SalesInvoiceDetails",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "SalesInvoiceDetails",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
