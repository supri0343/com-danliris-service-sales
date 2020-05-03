using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class DeleteSalesReceiptModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesReceiptDetails");

            migrationBuilder.DropTable(
                name: "SalesReceipts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesReceipts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(maxLength: 255, nullable: true),
                    AccountNumber = table.Column<string>(maxLength: 255, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    AdministrationFee = table.Column<double>(nullable: false),
                    AutoIncreament = table.Column<long>(nullable: false),
                    BankCode = table.Column<string>(maxLength: 255, nullable: true),
                    BankId = table.Column<int>(nullable: false),
                    BankName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 255, nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyRate = table.Column<double>(nullable: false),
                    CurrencySymbol = table.Column<string>(maxLength: 255, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    OriginAccountNumber = table.Column<string>(maxLength: 255, nullable: true),
                    SalesReceiptDate = table.Column<DateTimeOffset>(nullable: false),
                    SalesReceiptNo = table.Column<string>(maxLength: 255, nullable: true),
                    TotalPaid = table.Column<double>(nullable: false),
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReceipts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesReceiptDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 255, nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyRate = table.Column<double>(nullable: false),
                    CurrencySymbol = table.Column<string>(maxLength: 255, nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTimeOffset>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPaidOff = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    Nominal = table.Column<double>(nullable: false),
                    OverPaid = table.Column<double>(nullable: false),
                    Paid = table.Column<double>(nullable: false),
                    SalesInvoiceId = table.Column<int>(nullable: false),
                    SalesInvoiceNo = table.Column<string>(maxLength: 255, nullable: true),
                    SalesReceiptModelId = table.Column<long>(nullable: true),
                    Tempo = table.Column<double>(nullable: false),
                    TotalPaid = table.Column<double>(nullable: false),
                    TotalPayment = table.Column<double>(nullable: false),
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    Unpaid = table.Column<double>(nullable: false),
                    VatType = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReceiptDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesReceiptDetails_SalesReceipts_SalesReceiptModelId",
                        column: x => x.SalesReceiptModelId,
                        principalTable: "SalesReceipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceiptDetails_SalesReceiptModelId",
                table: "SalesReceiptDetails",
                column: "SalesReceiptModelId");
        }
    }
}
