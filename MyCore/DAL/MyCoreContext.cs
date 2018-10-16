using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCore.Models;
using MyCore.Models.BaseData;
using MyCore.Models.CGData;
using MyCore.Models.Store;
using MyCore.Models.SellData;
namespace MyCore.DAL
{
    public class MyCoreContext:DbContext
    {
        public MyCoreContext(DbContextOptions<MyCoreContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<RoleAuthorize> RoleAuthorize { get; set; }
        public DbSet<Goodinfo> Goodinfo { get; set; }
        public DbSet<SupperInfo> SupperInfo { get; set; }
        public DbSet<StoreInfo> StoreInfo { get; set; }
        public DbSet<OrderBill> OrderBill { get; set; }
        public DbSet<OrderBill_MX> OrderBill_MX { get; set; }
        public DbSet<InStoreBill> InStoreBill { get; set; }
        public DbSet<InStoreBill_MX> InStoreBill_MX { get; set; }
        public DbSet<GoodsStore> GoodsStore { get; set; }
        public DbSet<SellBill> SellBill { get; set; }
        public DbSet<SellBill_MX> SellBill_MX { get; set; }
        public DbSet<MoreLoseBill> MoreLoseBill { get; set; }
        public DbSet<MoreLoseBill_MX> MoreLoseBill_MX { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Menu>();
            modelBuilder.Entity<Office>();
            modelBuilder.Entity<RoleAuthorize>();
            modelBuilder.Entity<Goodinfo>();
            modelBuilder.Entity<SupperInfo>();
            modelBuilder.Entity<StoreInfo>();
            modelBuilder.Entity<OrderBill_MX>();
            modelBuilder.Entity<OrderBill>();
            modelBuilder.Entity<InStoreBill>();
            modelBuilder.Entity<InStoreBill_MX>();
            modelBuilder.Entity<GoodsStore>();
            modelBuilder.Entity<SellBill>();
            modelBuilder.Entity<SellBill_MX>();
            modelBuilder.Entity<MoreLoseBill>();
            modelBuilder.Entity<MoreLoseBill_MX>();
        }
    }
}
