﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyCore.DAL;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace MyCore.Migrations
{
    [DbContext(typeof(MyCoreContext))]
    [Migration("20180726133225_Goodinfos")]
    partial class Goodinfos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("MyCore.Models.BaseData.Goodinfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<string>("ClassID")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<string>("DW")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("EditDate");

                    b.Property<string>("EditName")
                        .HasMaxLength(45);

                    b.Property<string>("ForType")
                        .HasMaxLength(45);

                    b.Property<string>("GGType")
                        .HasMaxLength(45);

                    b.Property<string>("GoodID")
                        .HasMaxLength(45);

                    b.Property<string>("GoodName")
                        .HasMaxLength(145);

                    b.Property<string>("ModelType")
                        .HasMaxLength(45);

                    b.Property<string>("PYM")
                        .HasMaxLength(145);

                    b.Property<string>("SCCJ")
                        .HasMaxLength(145);

                    b.Property<decimal>("ShopPrice");

                    b.Property<string>("TXM")
                        .HasMaxLength(100);

                    b.Property<string>("TYName")
                        .HasMaxLength(145);

                    b.Property<string>("XKZ")
                        .HasMaxLength(145);

                    b.HasKey("id");

                    b.ToTable("Goodinfo");
                });

            modelBuilder.Entity("MyCore.Models.Menu", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<string>("MenuID")
                        .HasMaxLength(45);

                    b.Property<string>("MenuIcon")
                        .HasMaxLength(100);

                    b.Property<string>("MenuName")
                        .HasMaxLength(45);

                    b.Property<string>("MenuNameCN")
                        .HasMaxLength(45);

                    b.Property<string>("MenuParentID")
                        .HasMaxLength(45);

                    b.Property<int?>("MenuSort");

                    b.Property<int?>("MenuType");

                    b.Property<string>("MenuUrl")
                        .HasMaxLength(100);

                    b.HasKey("id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("MyCore.Models.Office", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<string>("EditName")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("EditeDate");

                    b.Property<string>("FZName")
                        .HasMaxLength(45);

                    b.Property<string>("OfficeID")
                        .HasMaxLength(45);

                    b.Property<string>("OfficeName")
                        .HasMaxLength(45);

                    b.Property<string>("ParentID")
                        .HasMaxLength(45);

                    b.Property<string>("Status")
                        .HasMaxLength(45);

                    b.HasKey("id");

                    b.ToTable("Office");
                });

            modelBuilder.Entity("MyCore.Models.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("EditDate");

                    b.Property<string>("EditName")
                        .HasMaxLength(45);

                    b.Property<string>("RoleID")
                        .HasMaxLength(45);

                    b.Property<string>("RoleName")
                        .HasMaxLength(45);

                    b.Property<string>("RoleType")
                        .HasMaxLength(45);

                    b.Property<string>("Status")
                        .HasMaxLength(45);

                    b.HasKey("id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("MyCore.Models.RoleAuthorize", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<string>("MenuID")
                        .HasMaxLength(45);

                    b.Property<int>("RoleID");

                    b.Property<int?>("Sort");

                    b.HasKey("id");

                    b.ToTable("RoleAuthorize");
                });

            modelBuilder.Entity("MyCore.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(145);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("EditDate");

                    b.Property<string>("EditName")
                        .HasMaxLength(45);

                    b.Property<string>("LoginName")
                        .HasMaxLength(45);

                    b.Property<string>("LoginPWD")
                        .HasMaxLength(45);

                    b.Property<int?>("OID");

                    b.Property<string>("Phone")
                        .HasMaxLength(45);

                    b.Property<int?>("RID");

                    b.Property<string>("Sex")
                        .HasMaxLength(45);

                    b.Property<string>("Status")
                        .HasMaxLength(45);

                    b.Property<string>("UserID")
                        .HasMaxLength(45);

                    b.Property<string>("UserName")
                        .HasMaxLength(45);

                    b.HasKey("id");

                    b.HasIndex("OID");

                    b.HasIndex("RID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MyCore.Models.User", b =>
                {
                    b.HasOne("MyCore.Models.Office", "Office")
                        .WithMany("Users")
                        .HasForeignKey("OID");

                    b.HasOne("MyCore.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RID");
                });
#pragma warning restore 612, 618
        }
    }
}
