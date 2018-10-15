using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class createsell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellBill",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    BillDate = table.Column<DateTime>(nullable: true),
                    BillID = table.Column<string>(maxLength: 45, nullable: true),
                    BillType = table.Column<string>(maxLength: 10, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    SellName = table.Column<string>(maxLength: 45, nullable: true),
                    SellNameID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    StoreName = table.Column<string>(maxLength: 45, nullable: true),
                    StroeInfo_id = table.Column<int>(nullable: false),
                    Sum = table.Column<decimal>(nullable: true),
                    SupName = table.Column<string>(maxLength: 145, nullable: true),
                    Sup_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellBill", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SellBill_MX",
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
                    Good_id = table.Column<string>(maxLength: 145, nullable: true),
                    InPrice = table.Column<decimal>(nullable: false),
                    MJPH = table.Column<string>(maxLength: 100, nullable: true),
                    ModelType = table.Column<string>(maxLength: 45, nullable: true),
                    Num = table.Column<decimal>(nullable: false),
                    SCCJ = table.Column<string>(maxLength: 145, nullable: true),
                    SCPH = table.Column<string>(maxLength: 100, nullable: true),
                    SellPrice = table.Column<decimal>(nullable: false),
                    StoreName = table.Column<string>(maxLength: 45, nullable: true),
                    StoreRow = table.Column<int>(nullable: true),
                    StroeInfo_id = table.Column<int>(nullable: true),
                    Sum = table.Column<decimal>(nullable: false),
                    scDate = table.Column<DateTime>(nullable: true),
                    yxqDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellBill_MX", x => x.id);
                    table.ForeignKey(
                        name: "FK_SellBill_MX_SellBill_Bill_id",
                        column: x => x.Bill_id,
                        principalTable: "SellBill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellBill_MX_Bill_id",
                table: "SellBill_MX",
                column: "Bill_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellBill_MX");

            migrationBuilder.DropTable(
                name: "SellBill");
        }
    }
}
