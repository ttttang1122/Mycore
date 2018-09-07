using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class Goodinfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goodinfo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    ClassID = table.Column<string>(maxLength: 45, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    DW = table.Column<string>(maxLength: 45, nullable: true),
                    EditDate = table.Column<DateTime>(nullable: true),
                    EditName = table.Column<string>(maxLength: 45, nullable: true),
                    ForType = table.Column<string>(maxLength: 45, nullable: true),
                    GGType = table.Column<string>(maxLength: 45, nullable: true),
                    GoodID = table.Column<string>(maxLength: 45, nullable: true),
                    GoodName = table.Column<string>(maxLength: 145, nullable: true),
                    ModelType = table.Column<string>(maxLength: 45, nullable: true),
                    PYM = table.Column<string>(maxLength: 145, nullable: true),
                    SCCJ = table.Column<string>(maxLength: 145, nullable: true),
                    ShopPrice = table.Column<decimal>(nullable: false),
                    TXM = table.Column<string>(maxLength: 100, nullable: true),
                    TYName = table.Column<string>(maxLength: 145, nullable: true),
                    XKZ = table.Column<string>(maxLength: 145, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goodinfo", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goodinfo");
        }
    }
}
