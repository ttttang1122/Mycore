using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class createtake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TakeStockBill",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    BillDate = table.Column<DateTime>(nullable: true),
                    BillID = table.Column<string>(maxLength: 45, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    StoreName = table.Column<string>(maxLength: 45, nullable: true),
                    StroeInfo_id = table.Column<int>(nullable: false),
                    YSName = table.Column<string>(maxLength: 45, nullable: true),
                    YSNameID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakeStockBill", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TakeStockBill_MX",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    Bill_id = table.Column<int>(nullable: false),
                    DW = table.Column<string>(maxLength: 45, nullable: true),
                    GGType = table.Column<string>(maxLength: 45, nullable: true),
                    GoodID = table.Column<string>(maxLength: 45, nullable: true),
                    GoodName = table.Column<string>(maxLength: 145, nullable: true),
                    Good_id = table.Column<int>(nullable: false),
                    HowNum = table.Column<decimal>(nullable: false),
                    MJPH = table.Column<string>(maxLength: 100, nullable: true),
                    ModelType = table.Column<string>(maxLength: 45, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    SCCJ = table.Column<string>(maxLength: 145, nullable: true),
                    SCPH = table.Column<string>(maxLength: 100, nullable: true),
                    StockNum = table.Column<decimal>(nullable: false),
                    StoreName = table.Column<string>(maxLength: 45, nullable: true),
                    StoreRow = table.Column<int>(nullable: true),
                    StroeInfo_id = table.Column<int>(nullable: true),
                    TakeNum = table.Column<decimal>(nullable: false),
                    scDate = table.Column<DateTime>(nullable: true),
                    yxqDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakeStockBill_MX", x => x.id);
                    table.ForeignKey(
                        name: "FK_TakeStockBill_MX_TakeStockBill_Bill_id",
                        column: x => x.Bill_id,
                        principalTable: "TakeStockBill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TakeStockBill_MX_Bill_id",
                table: "TakeStockBill_MX",
                column: "Bill_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TakeStockBill_MX");

            migrationBuilder.DropTable(
                name: "TakeStockBill");
        }
    }
}
