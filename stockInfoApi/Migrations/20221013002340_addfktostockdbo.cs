using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockInfoApi.Migrations
{
    public partial class addfktostockdbo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Accounts_AccountDboAccountId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_AccountDboAccountId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "AccountDboAccountId",
                table: "Stocks");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Accounts_AccountId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_AccountId",
                table: "Stocks");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountDboAccountId",
                table: "Stocks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_AccountDboAccountId",
                table: "Stocks",
                column: "AccountDboAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Accounts_AccountDboAccountId",
                table: "Stocks",
                column: "AccountDboAccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }
    }
}
