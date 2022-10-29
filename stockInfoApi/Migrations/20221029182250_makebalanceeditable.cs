﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stockInfoApi.Migrations
{
    public partial class makebalanceeditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Accounts");
        }
    }
}
