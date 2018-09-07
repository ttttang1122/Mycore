using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class SupperAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupperInfo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    EditDate = table.Column<DateTime>(nullable: true),
                    EditName = table.Column<string>(maxLength: 45, nullable: true),
                    FZName = table.Column<string>(maxLength: 45, nullable: true),
                    PYM = table.Column<string>(maxLength: 145, nullable: true),
                    Phone = table.Column<string>(maxLength: 45, nullable: true),
                    Status = table.Column<int>(nullable: true),
                    SupID = table.Column<string>(maxLength: 45, nullable: true),
                    SupName = table.Column<string>(maxLength: 145, nullable: true),
                    SupType = table.Column<int>(nullable: true),
                    WeiXin = table.Column<string>(maxLength: 45, nullable: true),
                    dq = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupperInfo", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupperInfo");
        }
    }
}
