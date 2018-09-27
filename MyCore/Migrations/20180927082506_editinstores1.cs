using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class editinstores1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "InStoreBill_MX",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StroeInfo_id",
                table: "InStoreBill_MX",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "InStoreBill",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StroeInfo_id",
                table: "InStoreBill",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "InStoreBill_MX");

            migrationBuilder.DropColumn(
                name: "StroeInfo_id",
                table: "InStoreBill_MX");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "InStoreBill");

            migrationBuilder.DropColumn(
                name: "StroeInfo_id",
                table: "InStoreBill");
        }
    }
}
