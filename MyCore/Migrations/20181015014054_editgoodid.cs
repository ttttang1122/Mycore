using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class editgoodid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Good_id",
                table: "SellBill_MX",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 145,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Good_id",
                table: "OrderBill_MX",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 145,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Good_id",
                table: "InStoreBill_MX",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 145,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Good_id",
                table: "GoodsStore",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 145,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Good_id",
                table: "SellBill_MX",
                maxLength: 145,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Good_id",
                table: "OrderBill_MX",
                maxLength: 145,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Good_id",
                table: "InStoreBill_MX",
                maxLength: 145,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Good_id",
                table: "GoodsStore",
                maxLength: 145,
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
