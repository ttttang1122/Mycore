using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCore.Models;
using MyCore.DAL;

namespace MyCore.Data
{
    public class DbInitializer
    {
        public static void Initialize(MyCoreContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                //用户
            new User{UserID="US-0001",UserName="系统管理员",LoginName="Admin", LoginPWD="123",BZ="one"},
            new User{UserID="US-0002",UserName="基姆",LoginName="Jim", LoginPWD="123",BZ="one"},

            };
            var Menus = new Menu[]
           {
               //菜单
            new Menu{MenuID="ME-0001",MenuIcon="layui-icon layui-icon-home",MenuName="Home", MenuParentID="4",MenuSort=1,MenuType=1,MenuNameCN="系统设置"},
            new Menu{MenuID="ME-0002",MenuIcon="layui-icon layui-icon-component",MenuName="component", MenuParentID="0",MenuSort=2,MenuType=1,MenuNameCN="综合业务"},
            new Menu{MenuID="ME-0003",MenuIcon="layui-icon layui-icon-app",MenuName="App", MenuParentID="0",MenuSort=3,MenuType=1,MenuNameCN="财务报表"},
            new Menu{MenuID="ME-0004",MenuIcon="layui-icon layui-icon-user",MenuName="Home", MenuParentID="ME-0001",MenuSort=1,MenuType=1,MenuUrl="/User/UserIndex", MenuNameCN="用户管理"},
            new Menu{MenuID="ME-0005",MenuIcon="layui-icon layui-icon-smile",MenuName="Role", MenuParentID="ME-0001",MenuSort=2,MenuType=1,MenuUrl="/Role/RoleIndex",MenuNameCN="角色管理"},
            new Menu{MenuID="ME-0006",MenuIcon="layui-icon layui-icon-group",MenuName="Office", MenuParentID="ME-0001",MenuSort=3,MenuType=1,MenuUrl="/Office/OfficeIndex",MenuNameCN="部门管理"},
            new Menu{MenuID="ME-0007",MenuIcon="layui-icon layui-icon-list",MenuName="Menu", MenuParentID="ME-0001",MenuSort=4,MenuType=1,MenuUrl="/Menu/MenuIndex",MenuNameCN="菜单管理"},

            new Menu{MenuID="ME-0008",MenuIcon="layui-icon layui-icon-cart",MenuName="Buy", MenuParentID="ME-0002",MenuSort=1,MenuType=1,MenuNameCN="采购管理"},
            new Menu{MenuID="ME-0009",MenuIcon="layui-icon layui-icon-form",MenuName="Order", MenuParentID="ME-0008",MenuSort=1,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="采购订单"},
            new Menu{MenuID="ME-0010",MenuIcon="layui-icon layui-icon-next",MenuName="PutSto", MenuParentID="ME-0008",MenuSort=2,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="采购入库"},
            new Menu{MenuID="ME-0011",MenuIcon="layui-icon layui-icon-set-fill",MenuName="Shopping", MenuParentID="ME-0002",MenuSort=2,MenuType=1,MenuNameCN="销售管理"},
            new Menu{MenuID="ME-0012",MenuIcon="layui-icon layui-icon-form",MenuName="ShopBill", MenuParentID="ME-0011",MenuSort=1,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="销售开单"},
            new Menu{MenuID="ME-0013",MenuIcon="layui-icon layui-icon-table",MenuName="ShopReport", MenuParentID="ME-0011",MenuSort=2,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="销售统计报表"},
            //按钮
            new Menu{MenuID="ME-0014",MenuName="Add", MenuParentID="ME-0004",MenuSort=1,MenuType=2,MenuNameCN="新增"},
            new Menu{MenuID="ME-0015",MenuName="Edit", MenuParentID="ME-0004",MenuSort=2,MenuType=2,MenuNameCN="修改"},
            new Menu{MenuID="ME-0016",MenuName="Del", MenuParentID="ME-0004",MenuSort=3,MenuType=2,MenuNameCN="删除"},
            new Menu{MenuID="ME-0017",MenuName="Search", MenuParentID="ME-0004",MenuSort=4,MenuType=2,MenuNameCN="查询"},

            new Menu{MenuID="ME-0018",MenuName="Add", MenuParentID="ME-0005",MenuSort=1,MenuType=2,MenuNameCN="新增"},
            new Menu{MenuID="ME-0019",MenuName="Edit", MenuParentID="ME-0005",MenuSort=2,MenuType=2,MenuNameCN="修改"},
            new Menu{MenuID="ME-0020",MenuName="Del", MenuParentID="ME-0005",MenuSort=3,MenuType=2,MenuNameCN="删除"},
            new Menu{MenuID="ME-0021",MenuName="Search", MenuParentID="ME-0005",MenuSort=4,MenuType=2,MenuNameCN="查询"},
            new Menu{MenuID="ME-0022",MenuName="Authorize", MenuParentID="ME-0005",MenuSort=4,MenuType=2,MenuNameCN="授权"},

            new Menu{MenuID="ME-0023",MenuName="Add", MenuParentID="ME-0006",MenuSort=1,MenuType=2,MenuNameCN="新增"},
            new Menu{MenuID="ME-0024",MenuName="Edit", MenuParentID="ME-0006",MenuSort=2,MenuType=2,MenuNameCN="修改"},
            new Menu{MenuID="ME-0025",MenuName="Del", MenuParentID="ME-0006",MenuSort=3,MenuType=2,MenuNameCN="删除"},


            new Menu{MenuID="ME-0026",MenuName="Add", MenuParentID="ME-0007",MenuSort=1,MenuType=2,MenuNameCN="新增"},
            new Menu{MenuID="ME-0027",MenuName="Edit", MenuParentID="ME-0007",MenuSort=2,MenuType=2,MenuNameCN="修改"},
            new Menu{MenuID="ME-0028",MenuName="Del", MenuParentID="ME-0007",MenuSort=3,MenuType=2,MenuNameCN="删除"},

            new Menu{MenuID="ME-0029",MenuIcon="layui-icon layui-icon-table",MenuName="BuyReport", MenuParentID="ME-0008",MenuSort=3,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="采购统计报表"},
            new Menu{MenuID="ME-0030",MenuName="Excel", MenuParentID="ME-0004",MenuSort=5,MenuType=2,MenuNameCN="导出"},
            new Menu{MenuID="ME-0031",MenuIcon="layui-icon layui-icon-form",MenuName="Base", MenuParentID="0",MenuSort=1,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="基础档案"},
            new Menu{MenuID="ME-0032",MenuIcon="layui-icon layui-icon-cart",MenuName="Goods", MenuParentID="ME-0031",MenuSort=1,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="商品信息"},
            new Menu{MenuID="ME-0033",MenuIcon="layui-icon layui-icon-friends",MenuName="Supply", MenuParentID="ME-0031",MenuSort=2,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="供应商信息"},
            new Menu{MenuID="ME-0034",MenuIcon="layui-icon layui-icon-friends",MenuName="Customer", MenuParentID="ME-0031",MenuSort=3,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="客户信息"},
            new Menu{MenuID="ME-0035",MenuIcon="layui-icon layui-icon-friends",MenuName="Made", MenuParentID="ME-0031",MenuSort=3,MenuType=4,MenuUrl="/Main/Defalut",MenuNameCN="生产厂家"},

            new Menu{MenuID="ME-0036",MenuIcon="layui-icon layui-icon-table",MenuName="BuyMonthRepot", MenuParentID="ME-0003",MenuSort=2,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="采购月统计报表"},
            new Menu{MenuID="ME-0037",MenuIcon="layui-icon layui-icon-table",MenuName="ShopReport", MenuParentID="ME-0003",MenuSort=1,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="采购统计报表"},
            new Menu{MenuID="ME-0038",MenuIcon="layui-icon layui-icon-table",MenuName="ShopAnalyse", MenuParentID="ME-0003",MenuSort=3,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="采购分析报表"},
            new Menu{MenuID="ME-0039",MenuIcon="layui-icon layui-icon-table",MenuName="ShopReport", MenuParentID="ME-0003",MenuSort=4,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="销售统计报表"},
            new Menu{MenuID="ME-0040",MenuIcon="layui-icon layui-icon-table",MenuName="MonthShopReport", MenuParentID="ME-0003",MenuSort=5,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="销售月报表"},
            new Menu{MenuID="ME-0041",MenuIcon="layui-icon layui-icon-table",MenuName="ShopAnalyse", MenuParentID="ME-0003",MenuSort=6,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="销售分析报表"},
            new Menu{MenuID="ME-0042",MenuIcon="layui-icon layui-icon-table",MenuName="ProfitReport", MenuParentID="ME-0003",MenuSort=7,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="毛利统计报表"},
            new Menu{MenuID="ME-0043",MenuIcon="layui-icon layui-icon-table",MenuName="MonthPrefitRport", MenuParentID="ME-0003",MenuSort=8,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="毛利月报表"},
            new Menu{MenuID="ME-0044",MenuIcon="layui-icon layui-icon-table",MenuName="PrefitAnasly", MenuParentID="ME-0003",MenuSort=9,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="毛利分析报表"},

            new Menu{MenuID="ME-0047",MenuIcon="layui-icon layui-icon-home",MenuName="StockManage", MenuParentID="ME-0002",MenuSort=3,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="库存管理"},
            new Menu{MenuID="ME-0048",MenuIcon="layui-icon layui-icon-component",MenuName="BadManage", MenuParentID="ME-0047",MenuSort=1,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="报损单"},
            new Menu{MenuID="ME-0049",MenuIcon="layui-icon layui-icon-add-circle-fine",MenuName="MoreBill", MenuParentID="ME-0047",MenuSort=2,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="报溢单"},
            new Menu{MenuID="ME-0050",MenuIcon="layui-icon layui-icon-list",MenuName="StockTest", MenuParentID="ME-0047",MenuSort=4,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="库存盘点"},
            new Menu{MenuID="ME-0051",MenuIcon="layui-icon layui-icon-list",MenuName="Stock", MenuParentID="ME-0047",MenuSort=3,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="库存信息"},
            new Menu{MenuID="ME-0052",MenuIcon="layui-icon layui-icon-table",MenuName="BadReport", MenuParentID="ME-0047",MenuSort=5,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="报损报表"},
            new Menu{MenuID="ME-0053",MenuIcon="layui-icon layui-icon-table",MenuName="MoreReport", MenuParentID="ME-0047",MenuSort=6,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="报溢报表"},
            new Menu{MenuID="ME-0054",MenuIcon="layui-icon layui-icon-table",MenuName="StockReport", MenuParentID="ME-0047",MenuSort=7,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="盘点报表"},
            new Menu{MenuID="ME-0055",MenuIcon="layui-icon layui-icon-home",MenuName="StockManage", MenuParentID="ME-0031",MenuSort=5,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="仓库管理"},
            new Menu{MenuID="ME-0056",MenuIcon="layui-icon layui-icon-share",MenuName="ClassManage", MenuParentID="ME-0031",MenuSort=6,MenuType=1,MenuUrl="/Main/Defalut",MenuNameCN="类别信息管理"},
           };
           
            foreach (Menu s in Menus)
            {
                context.Menu.Add(s);
            }
            foreach (User s in users)
            {
                context.User.Add(s);
            }
            context.SaveChanges();

        }
    }
}
