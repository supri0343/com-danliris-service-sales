using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class RegisterToSalesDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOReturnDetailItemModel_DOReturnDetailModel_DOReturnDetailModelId",
                table: "DOReturnDetailItemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_DOReturnDetailModel_DOReturnModel_DOReturnModelId",
                table: "DOReturnDetailModel");

            migrationBuilder.DropForeignKey(
                name: "FK_DOReturnItemModel_DOReturnDetailItemModel_DOReturnDetailItemModelId",
                table: "DOReturnItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOReturnModel",
                table: "DOReturnModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOReturnItemModel",
                table: "DOReturnItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOReturnDetailModel",
                table: "DOReturnDetailModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOReturnDetailItemModel",
                table: "DOReturnDetailItemModel");

            migrationBuilder.RenameTable(
                name: "DOReturnModel",
                newName: "DOReturns");

            migrationBuilder.RenameTable(
                name: "DOReturnItemModel",
                newName: "DOReturnItems");

            migrationBuilder.RenameTable(
                name: "DOReturnDetailModel",
                newName: "DOReturnDetails");

            migrationBuilder.RenameTable(
                name: "DOReturnDetailItemModel",
                newName: "DOReturnDetailItems");

            migrationBuilder.RenameIndex(
                name: "IX_DOReturnItemModel_DOReturnDetailItemModelId",
                table: "DOReturnItems",
                newName: "IX_DOReturnItems_DOReturnDetailItemModelId");

            migrationBuilder.RenameIndex(
                name: "IX_DOReturnDetailModel_DOReturnModelId",
                table: "DOReturnDetails",
                newName: "IX_DOReturnDetails_DOReturnModelId");

            migrationBuilder.RenameIndex(
                name: "IX_DOReturnDetailItemModel_DOReturnDetailModelId",
                table: "DOReturnDetailItems",
                newName: "IX_DOReturnDetailItems_DOReturnDetailModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOReturns",
                table: "DOReturns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOReturnItems",
                table: "DOReturnItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOReturnDetails",
                table: "DOReturnDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOReturnDetailItems",
                table: "DOReturnDetailItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DOReturnDetailItems_DOReturnDetails_DOReturnDetailModelId",
                table: "DOReturnDetailItems",
                column: "DOReturnDetailModelId",
                principalTable: "DOReturnDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DOReturnDetails_DOReturns_DOReturnModelId",
                table: "DOReturnDetails",
                column: "DOReturnModelId",
                principalTable: "DOReturns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DOReturnItems_DOReturnDetailItems_DOReturnDetailItemModelId",
                table: "DOReturnItems",
                column: "DOReturnDetailItemModelId",
                principalTable: "DOReturnDetailItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOReturnDetailItems_DOReturnDetails_DOReturnDetailModelId",
                table: "DOReturnDetailItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DOReturnDetails_DOReturns_DOReturnModelId",
                table: "DOReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DOReturnItems_DOReturnDetailItems_DOReturnDetailItemModelId",
                table: "DOReturnItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOReturns",
                table: "DOReturns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOReturnItems",
                table: "DOReturnItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOReturnDetails",
                table: "DOReturnDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOReturnDetailItems",
                table: "DOReturnDetailItems");

            migrationBuilder.RenameTable(
                name: "DOReturns",
                newName: "DOReturnModel");

            migrationBuilder.RenameTable(
                name: "DOReturnItems",
                newName: "DOReturnItemModel");

            migrationBuilder.RenameTable(
                name: "DOReturnDetails",
                newName: "DOReturnDetailModel");

            migrationBuilder.RenameTable(
                name: "DOReturnDetailItems",
                newName: "DOReturnDetailItemModel");

            migrationBuilder.RenameIndex(
                name: "IX_DOReturnItems_DOReturnDetailItemModelId",
                table: "DOReturnItemModel",
                newName: "IX_DOReturnItemModel_DOReturnDetailItemModelId");

            migrationBuilder.RenameIndex(
                name: "IX_DOReturnDetails_DOReturnModelId",
                table: "DOReturnDetailModel",
                newName: "IX_DOReturnDetailModel_DOReturnModelId");

            migrationBuilder.RenameIndex(
                name: "IX_DOReturnDetailItems_DOReturnDetailModelId",
                table: "DOReturnDetailItemModel",
                newName: "IX_DOReturnDetailItemModel_DOReturnDetailModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOReturnModel",
                table: "DOReturnModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOReturnItemModel",
                table: "DOReturnItemModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOReturnDetailModel",
                table: "DOReturnDetailModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOReturnDetailItemModel",
                table: "DOReturnDetailItemModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DOReturnDetailItemModel_DOReturnDetailModel_DOReturnDetailModelId",
                table: "DOReturnDetailItemModel",
                column: "DOReturnDetailModelId",
                principalTable: "DOReturnDetailModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DOReturnDetailModel_DOReturnModel_DOReturnModelId",
                table: "DOReturnDetailModel",
                column: "DOReturnModelId",
                principalTable: "DOReturnModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DOReturnItemModel_DOReturnDetailItemModel_DOReturnDetailItemModelId",
                table: "DOReturnItemModel",
                column: "DOReturnDetailItemModelId",
                principalTable: "DOReturnDetailItemModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
