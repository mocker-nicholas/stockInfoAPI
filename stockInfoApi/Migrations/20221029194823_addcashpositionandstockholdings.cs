using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockInfoApi.Migrations
{
    public partial class addcashpositionandstockholdings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Accounts",
                newName: "StockHoldings");

            migrationBuilder.AddColumn<double>(
                name: "Cash",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cash",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "StockHoldings",
                table: "Accounts",
                newName: "Balance");
        }
    }
}
