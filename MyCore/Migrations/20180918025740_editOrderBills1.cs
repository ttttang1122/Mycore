using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class editOrderBills1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateNameID",
                table: "OrderBill");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "OrderBill",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "OrderBill");

            migrationBuilder.AddColumn<int>(
                name: "CreateNameID",
                table: "OrderBill",
                nullable: false,
                defaultValue: 0);
        }
    }
}
