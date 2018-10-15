using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class editshname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "SHDate",
                table: "SellBill",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHName",
                table: "SellBill",
                maxLength: 45,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SHDate",
                table: "SellBill");

            migrationBuilder.DropColumn(
                name: "SHName",
                table: "SellBill");

        }
    }
}
