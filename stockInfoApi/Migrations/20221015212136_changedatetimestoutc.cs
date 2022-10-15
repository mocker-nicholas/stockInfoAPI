using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockInfoApi.Migrations
{
    public partial class changedatetimestoutc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountDboAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountDboAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountDboAccountId",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountDboAccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountDboAccountId",
                table: "Transactions",
                column: "AccountDboAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountDboAccountId",
                table: "Transactions",
                column: "AccountDboAccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }
    }
}
