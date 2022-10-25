using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockInfoApi.Migrations
{
    public partial class addforiegnkeytransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Accounts_AccountId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_AccountId",
                table: "Stocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stocks_AccountId",
                table: "Stocks",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Accounts_AccountId",
                table: "Stocks",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
