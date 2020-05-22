using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class UpdateDOReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DOReturnDetailModelId",
                table: "DOReturnItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DOReturnItems_DOReturnDetailModelId",
                table: "DOReturnItems",
                column: "DOReturnDetailModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DOReturnItems_DOReturnDetails_DOReturnDetailModelId",
                table: "DOReturnItems",
                column: "DOReturnDetailModelId",
                principalTable: "DOReturnDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOReturnItems_DOReturnDetails_DOReturnDetailModelId",
                table: "DOReturnItems");

            migrationBuilder.DropIndex(
                name: "IX_DOReturnItems_DOReturnDetailModelId",
                table: "DOReturnItems");

            migrationBuilder.DropColumn(
                name: "DOReturnDetailModelId",
                table: "DOReturnItems");
        }
    }
}
