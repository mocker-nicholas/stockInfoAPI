using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockInfoApi.Migrations
{
    public partial class changestockfktoguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Accounts_AccountId1",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_AccountId1",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "AccountId1",
                table: "Stocks",
                newName: "AccountId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Stocks",
                newName: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_AccountId1",
                table: "Stocks",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Accounts_AccountId1",
                table: "Stocks",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
