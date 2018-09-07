using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class Orderbilledit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<string>(
                name: "CGName",
                table: "OrderBill",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateName",
                table: "OrderBill",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupName",
                table: "OrderBill",
                maxLength: 145,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CGName",
                table: "OrderBill");

            migrationBuilder.DropColumn(
                name: "CreateName",
                table: "OrderBill");

            migrationBuilder.DropColumn(
                name: "SupName",
                table: "OrderBill");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBill_CGNameID",
                table: "OrderBill",
                column: "CGNameID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBill_CreateNameID",
                table: "OrderBill",
                column: "CreateNameID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBill_Sup_id",
                table: "OrderBill",
                column: "Sup_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBill_User_CGNameID",
                table: "OrderBill",
                column: "CGNameID",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBill_User_CreateNameID",
                table: "OrderBill",
                column: "CreateNameID",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBill_SupperInfo_Sup_id",
                table: "OrderBill",
                column: "Sup_id",
                principalTable: "SupperInfo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
