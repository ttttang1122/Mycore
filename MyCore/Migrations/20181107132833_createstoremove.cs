using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class createstoremove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreMoveBill",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    BillDate = table.Column<DateTime>(nullable: true),
                    BillID = table.Column<string>(maxLength: 45, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    InStoreName = table.Column<string>(maxLength: 45, nullable: true),
                    InStroeInfo_id = table.Column<int>(nullable: false),
                    OutStoreName = table.Column<string>(maxLength: 45, nullable: true),
                    OutStroeInfo_id = table.Column<int>(nullable: false),
                    SHDate = table.Column<DateTime>(nullable: true),
                    SHName = table.Column<string>(maxLength: 45, nullable: true),
                    Status = table.Column<int>(nullable: true),
                    YSName = table.Column<string>(maxLength: 45, nullable: true),
                    YSNameID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMoveBill", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StoreMoveBill_MX",
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
                    InStoreName = table.Column<string>(maxLength: 45, nullable: true),
                    InStroeInfo_id = table.Column<int>(nullable: true),
                    MJPH = table.Column<string>(maxLength: 100, nullable: true),
                    ModelType = table.Column<string>(maxLength: 45, nullable: true),
                    Num = table.Column<decimal>(nullable: false),
                    OutStoreName = table.Column<string>(maxLength: 45, nullable: true),
                    OutStroeInfo_id = table.Column<int>(nullable: true),
                    SCCJ = table.Column<string>(maxLength: 145, nullable: true),
                    SCPH = table.Column<string>(maxLength: 100, nullable: true),
                    StoreRow = table.Column<int>(nullable: true),
                    scDate = table.Column<DateTime>(nullable: true),
                    yxqDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMoveBill_MX", x => x.id);
                    table.ForeignKey(
                        name: "FK_StoreMoveBill_MX_StoreMoveBill_Bill_id",
                        column: x => x.Bill_id,
                        principalTable: "StoreMoveBill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreMoveBill_MX_Bill_id",
                table: "StoreMoveBill_MX",
                column: "Bill_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreMoveBill_MX");

            migrationBuilder.DropTable(
                name: "StoreMoveBill");
        }
    }
}
