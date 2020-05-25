using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Sales.Lib.Migrations
{
    public partial class ChangeReturnFromInDOReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnFrom",
                table: "DOReturns",
                newName: "ReturnFromName");

            migrationBuilder.AddColumn<long>(
                name: "ReturnFromId",
                table: "DOReturns",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnFromId",
                table: "DOReturns");

            migrationBuilder.RenameColumn(
                name: "ReturnFromName",
                table: "DOReturns",
                newName: "ReturnFrom");
        }
    }
}
