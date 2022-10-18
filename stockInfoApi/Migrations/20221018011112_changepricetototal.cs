using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockInfoApi.Migrations
{
    public partial class changepricetototal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchasePrice",
                table: "Stocks",
                newName: "TotalHoldings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalHoldings",
                table: "Stocks",
                newName: "PurchasePrice");
        }
    }
}
