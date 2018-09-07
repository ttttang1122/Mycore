using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class StoreInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreInfo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Address = table.Column<string>(maxLength: 145, nullable: true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    EditDate = table.Column<DateTime>(nullable: true),
                    EditName = table.Column<string>(maxLength: 45, nullable: true),
                    Sizes = table.Column<string>(maxLength: 145, nullable: true),
                    Status = table.Column<int>(nullable: true),
                    StoreName = table.Column<string>(maxLength: 45, nullable: true),
                    UseSay = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInfo", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreInfo");
        }
    }
}
