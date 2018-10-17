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
    partial class MyCoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("Status");

                    b.Property<string>("TXM")
                        .HasMaxLength(100);

                    b.Property<string>("TYName")
                        .HasMaxLength(145);

                    b.Property<string>("XKZ")
                        .HasMaxLength(145);

                    b.HasKey("id");

                    b.ToTable("Goodinfo");
                });

            modelBuilder.Entity("MyCore.Models.BaseData.StoreInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(145);

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("EditDate");

                    b.Property<string>("EditName")
                        .HasMaxLength(45);

                    b.Property<string>("Sizes")
                        .HasMaxLength(145);

                    b.Property<int?>("Status");

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<string>("UseSay")
                        .HasMaxLength(500);

                    b.HasKey("id");

                    b.ToTable("StoreInfo");
                });

            modelBuilder.Entity("MyCore.Models.BaseData.SupperInfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("EditDate");

                    b.Property<string>("EditName")
                        .HasMaxLength(45);

                    b.Property<string>("FZName")
                        .HasMaxLength(45);

                    b.Property<string>("PYM")
                        .HasMaxLength(145);

                    b.Property<string>("Phone")
                        .HasMaxLength(45);

                    b.Property<int?>("Status");

                    b.Property<string>("SupID")
                        .HasMaxLength(45);

                    b.Property<string>("SupName")
                        .HasMaxLength(145);

                    b.Property<int?>("SupType");

                    b.Property<string>("WeiXin")
                        .HasMaxLength(45);

                    b.Property<string>("dq")
                        .HasMaxLength(45);

                    b.HasKey("id");

                    b.ToTable("SupperInfo");
                });

            modelBuilder.Entity("MyCore.Models.CGData.InStoreBill", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("BillDate");

                    b.Property<string>("BillID")
                        .HasMaxLength(45);

                    b.Property<string>("BillType")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<string>("OrderBillID")
                        .HasMaxLength(45);

                    b.Property<int?>("OrderBill_id");

                    b.Property<DateTime?>("SHDate");

                    b.Property<string>("SHName")
                        .HasMaxLength(45);

                    b.Property<int?>("SHStatus");

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int>("StroeInfo_id");

                    b.Property<decimal?>("Sum");

                    b.Property<string>("SupName")
                        .HasMaxLength(145);

                    b.Property<int>("Sup_id");

                    b.Property<string>("YSName")
                        .HasMaxLength(45);

                    b.Property<int>("YSNameID");

                    b.HasKey("id");

                    b.ToTable("InStoreBill");
                });

            modelBuilder.Entity("MyCore.Models.CGData.InStoreBill_MX", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<int>("Bill_id");

                    b.Property<string>("DW")
                        .HasMaxLength(45);

                    b.Property<string>("GGType")
                        .HasMaxLength(45);

                    b.Property<string>("GoodID")
                        .HasMaxLength(45);

                    b.Property<string>("GoodName")
                        .HasMaxLength(145);

                    b.Property<int>("Good_id");

                    b.Property<string>("MJPH")
                        .HasMaxLength(100);

                    b.Property<string>("ModelType")
                        .HasMaxLength(45);

                    b.Property<decimal>("Num");

                    b.Property<int?>("OrderRow");

                    b.Property<decimal>("Price");

                    b.Property<string>("SCCJ")
                        .HasMaxLength(145);

                    b.Property<string>("SCPH")
                        .HasMaxLength(100);

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int?>("StroeInfo_id");

                    b.Property<decimal>("Sum");

                    b.Property<DateTime?>("scDate");

                    b.Property<DateTime?>("yxqDate");

                    b.HasKey("id");

                    b.HasIndex("Bill_id");

                    b.ToTable("InStoreBill_MX");
                });

            modelBuilder.Entity("MyCore.Models.CGData.OrderBill", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("BillDate");

                    b.Property<string>("BillID")
                        .HasMaxLength(45);

                    b.Property<string>("CGName")
                        .HasMaxLength(45);

                    b.Property<int>("CGNameID");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("SHDate");

                    b.Property<string>("SHName")
                        .HasMaxLength(45);

                    b.Property<int?>("SHStatus");

                    b.Property<int?>("Status");

                    b.Property<string>("SupName")
                        .HasMaxLength(145);

                    b.Property<int>("Sup_id");

                    b.HasKey("id");

                    b.ToTable("OrderBill");
                });

            modelBuilder.Entity("MyCore.Models.CGData.OrderBill_MX", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<int>("Bill_id");

                    b.Property<string>("DW")
                        .HasMaxLength(45);

                    b.Property<decimal>("EndNum");

                    b.Property<string>("GGType")
                        .HasMaxLength(45);

                    b.Property<string>("GoodID")
                        .HasMaxLength(45);

                    b.Property<string>("GoodName")
                        .HasMaxLength(145);

                    b.Property<int>("Good_id");

                    b.Property<string>("ModelType")
                        .HasMaxLength(45);

                    b.Property<decimal>("Num");

                    b.Property<decimal>("Price");

                    b.Property<string>("SCCJ")
                        .HasMaxLength(145);

                    b.Property<int?>("Status");

                    b.Property<decimal>("Sum");

                    b.HasKey("id");

                    b.HasIndex("Bill_id");

                    b.ToTable("OrderBill_MX");
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

            modelBuilder.Entity("MyCore.Models.SellData.SellBill", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("BillDate");

                    b.Property<string>("BillID")
                        .HasMaxLength(45);

                    b.Property<string>("BillType")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<decimal?>("GiveSum");

                    b.Property<DateTime?>("SHDate");

                    b.Property<string>("SHName")
                        .HasMaxLength(45);

                    b.Property<string>("SellName")
                        .HasMaxLength(45);

                    b.Property<int>("SellNameID");

                    b.Property<int?>("Status");

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int>("StroeInfo_id");

                    b.Property<decimal?>("Sum");

                    b.Property<string>("SupName")
                        .HasMaxLength(145);

                    b.Property<int>("Sup_id");

                    b.HasKey("id");

                    b.ToTable("SellBill");
                });

            modelBuilder.Entity("MyCore.Models.SellData.SellBill_MX", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<int>("Bill_id");

                    b.Property<string>("DW")
                        .HasMaxLength(45);

                    b.Property<string>("GGType")
                        .HasMaxLength(45);

                    b.Property<string>("GoodID")
                        .HasMaxLength(45);

                    b.Property<string>("GoodName")
                        .HasMaxLength(145);

                    b.Property<int>("Good_id");

                    b.Property<decimal>("InPrice");

                    b.Property<string>("MJPH")
                        .HasMaxLength(100);

                    b.Property<string>("ModelType")
                        .HasMaxLength(45);

                    b.Property<decimal>("Num");

                    b.Property<string>("SCCJ")
                        .HasMaxLength(145);

                    b.Property<string>("SCPH")
                        .HasMaxLength(100);

                    b.Property<decimal>("SellPrice");

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int?>("StoreRow");

                    b.Property<int?>("StroeInfo_id");

                    b.Property<decimal>("Sum");

                    b.Property<DateTime?>("scDate");

                    b.Property<DateTime?>("yxqDate");

                    b.HasKey("id");

                    b.HasIndex("Bill_id");

                    b.ToTable("SellBill_MX");
                });

            modelBuilder.Entity("MyCore.Models.Store.GoodsStore", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<string>("DW")
                        .HasMaxLength(45);

                    b.Property<string>("GGType")
                        .HasMaxLength(45);

                    b.Property<string>("GoodID")
                        .HasMaxLength(45);

                    b.Property<string>("GoodName")
                        .HasMaxLength(145);

                    b.Property<int>("Good_id");

                    b.Property<string>("MJPH")
                        .HasMaxLength(100);

                    b.Property<string>("ModelType")
                        .HasMaxLength(45);

                    b.Property<decimal>("Num");

                    b.Property<decimal>("Price");

                    b.Property<string>("SCCJ")
                        .HasMaxLength(145);

                    b.Property<string>("SCPH")
                        .HasMaxLength(100);

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int>("Store_id");

                    b.Property<DateTime?>("scDate");

                    b.Property<DateTime?>("yxqDate");

                    b.HasKey("id");

                    b.ToTable("GoodsStore");
                });

            modelBuilder.Entity("MyCore.Models.Store.MoreLoseBill", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("BillDate");

                    b.Property<string>("BillID")
                        .HasMaxLength(45);

                    b.Property<string>("BillType")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("SHDate");

                    b.Property<string>("SHName")
                        .HasMaxLength(45);

                    b.Property<int?>("Status");

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int>("StroeInfo_id");

                    b.Property<decimal?>("Sum");

                    b.Property<string>("YSName")
                        .HasMaxLength(45);

                    b.Property<int>("YSNameID");

                    b.HasKey("id");

                    b.ToTable("MoreLoseBill");
                });

            modelBuilder.Entity("MyCore.Models.Store.MoreLoseBill_MX", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<int>("Bill_id");

                    b.Property<string>("DW")
                        .HasMaxLength(45);

                    b.Property<string>("GGType")
                        .HasMaxLength(45);

                    b.Property<string>("GoodID")
                        .HasMaxLength(45);

                    b.Property<string>("GoodName")
                        .HasMaxLength(145);

                    b.Property<int>("Good_id");

                    b.Property<string>("MJPH")
                        .HasMaxLength(100);

                    b.Property<string>("ModelType")
                        .HasMaxLength(45);

                    b.Property<decimal>("Num");

                    b.Property<decimal>("Price");

                    b.Property<string>("SCCJ")
                        .HasMaxLength(145);

                    b.Property<string>("SCPH")
                        .HasMaxLength(100);

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int?>("StoreRow");

                    b.Property<int?>("StroeInfo_id");

                    b.Property<decimal>("Sum");

                    b.Property<DateTime?>("scDate");

                    b.Property<DateTime?>("yxqDate");

                    b.HasKey("id");

                    b.HasIndex("Bill_id");

                    b.ToTable("MoreLoseBill_MX");
                });

            modelBuilder.Entity("MyCore.Models.Store.TakeStockBill", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("BillDate");

                    b.Property<string>("BillID")
                        .HasMaxLength(45);

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateName")
                        .HasMaxLength(45);

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int>("StroeInfo_id");

                    b.Property<string>("YSName")
                        .HasMaxLength(45);

                    b.Property<int>("YSNameID");

                    b.HasKey("id");

                    b.ToTable("TakeStockBill");
                });

            modelBuilder.Entity("MyCore.Models.Store.TakeStockBill_MX", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BZ")
                        .HasMaxLength(200);

                    b.Property<int>("Bill_id");

                    b.Property<string>("DW")
                        .HasMaxLength(45);

                    b.Property<string>("GGType")
                        .HasMaxLength(45);

                    b.Property<string>("GoodID")
                        .HasMaxLength(45);

                    b.Property<string>("GoodName")
                        .HasMaxLength(145);

                    b.Property<int>("Good_id");

                    b.Property<decimal>("HowNum");

                    b.Property<string>("MJPH")
                        .HasMaxLength(100);

                    b.Property<string>("ModelType")
                        .HasMaxLength(45);

                    b.Property<decimal>("Price");

                    b.Property<string>("SCCJ")
                        .HasMaxLength(145);

                    b.Property<string>("SCPH")
                        .HasMaxLength(100);

                    b.Property<decimal>("StockNum");

                    b.Property<string>("StoreName")
                        .HasMaxLength(45);

                    b.Property<int?>("StoreRow");

                    b.Property<int?>("StroeInfo_id");

                    b.Property<decimal>("TakeNum");

                    b.Property<DateTime?>("scDate");

                    b.Property<DateTime?>("yxqDate");

                    b.HasKey("id");

                    b.HasIndex("Bill_id");

                    b.ToTable("TakeStockBill_MX");
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

            modelBuilder.Entity("MyCore.Models.CGData.InStoreBill_MX", b =>
                {
                    b.HasOne("MyCore.Models.CGData.InStoreBill", "InStoreBill")
                        .WithMany("InStoreBill_MX")
                        .HasForeignKey("Bill_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCore.Models.CGData.OrderBill_MX", b =>
                {
                    b.HasOne("MyCore.Models.CGData.OrderBill", "OrderBill")
                        .WithMany("OrderBill_MX")
                        .HasForeignKey("Bill_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCore.Models.SellData.SellBill_MX", b =>
                {
                    b.HasOne("MyCore.Models.SellData.SellBill", "SellBill")
                        .WithMany("SellBill_MX")
                        .HasForeignKey("Bill_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCore.Models.Store.MoreLoseBill_MX", b =>
                {
                    b.HasOne("MyCore.Models.Store.MoreLoseBill", "MoreLoseBill")
                        .WithMany("MoreLoseBill_MX")
                        .HasForeignKey("Bill_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCore.Models.Store.TakeStockBill_MX", b =>
                {
                    b.HasOne("MyCore.Models.Store.TakeStockBill", "TakeStockBill")
                        .WithMany("TakeStockBill_MX")
                        .HasForeignKey("Bill_id")
                        .OnDelete(DeleteBehavior.Cascade);
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
