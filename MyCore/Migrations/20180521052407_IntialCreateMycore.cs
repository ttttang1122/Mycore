using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCore.Migrations
{
    public partial class IntialCreateMycore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 45, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    MenuID = table.Column<string>(maxLength: 45, nullable: true),
                    MenuIcon = table.Column<string>(maxLength: 100, nullable: true),
                    MenuName = table.Column<string>(maxLength: 45, nullable: true),
                    MenuNameCN = table.Column<string>(maxLength: 45, nullable: true),
                    MenuParentID = table.Column<string>(maxLength: 45, nullable: true),
                    MenuSort = table.Column<int>(nullable: true),
                    MenuType = table.Column<int>(nullable: true),
                    MenuUrl = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    EditName = table.Column<string>(maxLength: 45, nullable: true),
                    EditeDate = table.Column<DateTime>(nullable: true),
                    FZName = table.Column<string>(maxLength: 45, nullable: true),
                    OfficeID = table.Column<string>(maxLength: 45, nullable: true),
                    OfficeName = table.Column<string>(maxLength: 45, nullable: true),
                    ParentID = table.Column<string>(maxLength: 45, nullable: true),
                    Status = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    EditDate = table.Column<DateTime>(nullable: true),
                    EditName = table.Column<string>(maxLength: 45, nullable: true),
                    RoleID = table.Column<string>(maxLength: 45, nullable: true),
                    RoleName = table.Column<string>(maxLength: 45, nullable: true),
                    RoleType = table.Column<string>(maxLength: 45, nullable: true),
                    Status = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoleAuthorize",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    MenuID = table.Column<string>(maxLength: 45, nullable: true),
                    RoleID = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAuthorize", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BZ = table.Column<string>(maxLength: 145, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateName = table.Column<string>(maxLength: 45, nullable: true),
                    EditDate = table.Column<DateTime>(nullable: true),
                    EditName = table.Column<string>(maxLength: 45, nullable: true),
                    LoginName = table.Column<string>(maxLength: 45, nullable: true),
                    LoginPWD = table.Column<string>(maxLength: 45, nullable: true),
                    OID = table.Column<int>(nullable: true),
                    Phone = table.Column<string>(maxLength: 45, nullable: true),
                    RID = table.Column<int>(nullable: true),
                    Sex = table.Column<string>(maxLength: 45, nullable: true),
                    Status = table.Column<string>(maxLength: 45, nullable: true),
                    UserID = table.Column<string>(maxLength: 45, nullable: true),
                    UserName = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Office_OID",
                        column: x => x.OID,
                        principalTable: "Office",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Role_RID",
                        column: x => x.RID,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_OID",
                table: "User",
                column: "OID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RID",
                table: "User",
                column: "RID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "RoleAuthorize");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
