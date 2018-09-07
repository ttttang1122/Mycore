using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class orderbill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderBill",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    BillDate = table.Column<DateTime>(nullable: true),
                    BillID = table.Column<string>(maxLength: 45, nullable: true),
                    CGNameID = table.Column<int>(nullable: false),
                    CreateNameID = table.Column<int>(nullable: false),
                    SHDate = table.Column<DateTime>(nullable: true),
                    SHName = table.Column<string>(maxLength: 45, nullable: true),
                    SHStatus = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    Sup_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBill", x => x.id);                 
                });

            migrationBuilder.CreateTable(
                name: "OrderBill_MX",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    Bill_id = table.Column<int>(nullable: false),
                    DW = table.Column<string>(maxLength: 45, nullable: true),
                    EndNum = table.Column<decimal>(nullable: false),
                    GGType = table.Column<string>(maxLength: 45, nullable: true),
                    GoodID = table.Column<string>(maxLength: 45, nullable: true),
                    GoodName = table.Column<string>(maxLength: 145, nullable: true),
                    Good_id = table.Column<string>(maxLength: 145, nullable: true),
                    ModelType = table.Column<string>(maxLength: 45, nullable: true),
                    Num = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    SCCJ = table.Column<string>(maxLength: 145, nullable: true),
                    Status = table.Column<int>(nullable: true),
                    Sum = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBill_MX", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderBill_MX_OrderBill_Bill_id",
                        column: x => x.Bill_id,
                        principalTable: "OrderBill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderBill_MX_Bill_id",
                table: "OrderBill_MX",
                column: "Bill_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBill_MX");

            migrationBuilder.DropTable(
                name: "OrderBill");
        }
    }
}
