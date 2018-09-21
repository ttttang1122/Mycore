using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class createInstore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsStore",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    DW = table.Column<string>(maxLength: 45, nullable: true),
                    GGType = table.Column<string>(maxLength: 45, nullable: true),
                    GoodID = table.Column<string>(maxLength: 45, nullable: true),
                    GoodName = table.Column<string>(maxLength: 145, nullable: true),
                    Good_id = table.Column<string>(maxLength: 145, nullable: true),
                    MJPH = table.Column<string>(maxLength: 100, nullable: true),
                    ModelType = table.Column<string>(maxLength: 45, nullable: true),
                    Num = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    SCCJ = table.Column<string>(maxLength: 145, nullable: true),
                    SCPH = table.Column<string>(maxLength: 100, nullable: true),
                    StoreName = table.Column<string>(maxLength: 45, nullable: true),
                    Store_id = table.Column<int>(nullable: false),
                    scDate = table.Column<DateTime>(nullable: true),
                    yxqDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsStore", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InStoreBill",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    BillDate = table.Column<DateTime>(nullable: true),
                    BillID = table.Column<string>(maxLength: 45, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    OrderBillID = table.Column<string>(maxLength: 45, nullable: true),
                    OrderBill_id = table.Column<int>(nullable: true),
                    SHDate = table.Column<DateTime>(nullable: true),
                    SHName = table.Column<string>(maxLength: 45, nullable: true),
                    SHStatus = table.Column<int>(nullable: true),
                    Sum = table.Column<decimal>(nullable: true),
                    SupName = table.Column<string>(maxLength: 145, nullable: true),
                    Sup_id = table.Column<int>(nullable: false),
                    YSName = table.Column<string>(maxLength: 45, nullable: true),
                    YSNameID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InStoreBill", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InStoreBill_MX",
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
                    MJPH = table.Column<string>(maxLength: 100, nullable: true),
                    ModelType = table.Column<string>(maxLength: 45, nullable: true),
                    Num = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    SCCJ = table.Column<string>(maxLength: 145, nullable: true),
                    SCPH = table.Column<string>(maxLength: 100, nullable: true),
                    Sum = table.Column<decimal>(nullable: false),
                    scDate = table.Column<DateTime>(nullable: true),
                    yxqDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InStoreBill_MX", x => x.id);
                    table.ForeignKey(
                        name: "FK_InStoreBill_MX_InStoreBill_Bill_id",
                        column: x => x.Bill_id,
                        principalTable: "InStoreBill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InStoreBill_MX_Bill_id",
                table: "InStoreBill_MX",
                column: "Bill_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsStore");

            migrationBuilder.DropTable(
                name: "InStoreBill_MX");

            migrationBuilder.DropTable(
                name: "InStoreBill");
        }
    }
}
