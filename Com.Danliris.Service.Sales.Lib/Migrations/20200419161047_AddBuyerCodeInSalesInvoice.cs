using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class AddBuyerCodeInSalesInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerCode",
                table: "SalesInvoices",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DOReturnModel",
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
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    AutoIncreament = table.Column<long>(nullable: false),
                    DOReturnNo = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<string>(maxLength: 255, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    LKTPNo = table.Column<string>(maxLength: 255, nullable: true),
                    HeadOfStorage = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOReturnModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOReturnDetailModel",
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
                    SalesInvoiceId = table.Column<int>(nullable: false),
                    SalesInvoiceNo = table.Column<string>(maxLength: 255, nullable: true),
                    DOReturnId = table.Column<int>(nullable: false),
                    DOReturnModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOReturnDetailModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOReturnDetailModel_DOReturnModel_DOReturnModelId",
                        column: x => x.DOReturnModelId,
                        principalTable: "DOReturnModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DOReturnDetailItemModel",
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
                    ShipmentDocumentId = table.Column<int>(nullable: false),
                    ShipmentDocumentCode = table.Column<string>(maxLength: 255, nullable: true),
                    DOReturnDetailId = table.Column<int>(nullable: false),
                    DOReturnDetailModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOReturnDetailItemModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOReturnDetailItemModel_DOReturnDetailModel_DOReturnDetailModelId",
                        column: x => x.DOReturnDetailModelId,
                        principalTable: "DOReturnDetailModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DOReturnItemModel",
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
                    DOReturnDetailItemId = table.Column<int>(nullable: false),
                    DOReturnDetailItemModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOReturnItemModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOReturnItemModel_DOReturnDetailItemModel_DOReturnDetailItemModelId",
                        column: x => x.DOReturnDetailItemModelId,
                        principalTable: "DOReturnDetailItemModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DOReturnDetailItemModel_DOReturnDetailModelId",
                table: "DOReturnDetailItemModel",
                column: "DOReturnDetailModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DOReturnDetailModel_DOReturnModelId",
                table: "DOReturnDetailModel",
                column: "DOReturnModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DOReturnItemModel_DOReturnDetailItemModelId",
                table: "DOReturnItemModel",
                column: "DOReturnDetailItemModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOReturnItemModel");

            migrationBuilder.DropTable(
                name: "DOReturnDetailItemModel");

            migrationBuilder.DropTable(
                name: "DOReturnDetailModel");

            migrationBuilder.DropTable(
                name: "DOReturnModel");

            migrationBuilder.DropColumn(
                name: "BuyerCode",
                table: "SalesInvoices");
        }
    }
}
