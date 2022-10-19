using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockInfoApi.Migrations
{
    public partial class removedsharepriceanddatesfromstock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SharePrice",
                table: "Stocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Stocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Stocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "SharePrice",
                table: "Stocks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
