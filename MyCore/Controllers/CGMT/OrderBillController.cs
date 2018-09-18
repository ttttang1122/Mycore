using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models.CGData;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyCore.Controllers.CGGL
{
    public class OrderBillController : BaseController
    {
        private MyCoreContext conn;
        public OrderBillController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult OrderBillIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OrderBillList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<OrderBill> bills = conn.OrderBill;

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            bills = bills.Where(b => b.BillID.Contains(StrSearch));
                            break;
                        case "1":
                            bills = bills.Where(b => b.CGName.Contains(StrSearch));

                            break;
                        case "2":
                            bills = bills.Where(b => b.SupName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();
            return lists.GetJson<OrderBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<OrderBill>());
        }

        [HttpPost]
        public async Task<IActionResult> OrderBill_MXList(string sidx, string sord, int page, int rows, int id)
        {

            var bills = await conn.OrderBill_MX.Where(b=>b.Bill_id==id).ToListAsync();   
            return bills.GetJson<OrderBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<OrderBill_MX>());

        }
        public IActionResult AddOrderIndex()
        {
            return View();
        }
        public IActionResult AddRowIndex()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await conn.User.ToListAsync();
             var data = users.Select(p=>new {p.id,p.UserName });
            return Content(data.ToJson());
        }
        [HttpGet]
        public async Task<IActionResult> GetSup()
        {
            var sups = await conn.SupperInfo.ToListAsync();
            var data = sups.Select(p => new { p.id, p.SupName });
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> GoodsList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {
            IQueryable<Goodinfo> goods = conn.Goodinfo;
            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            goods = goods.Where(b => b.PYM.Contains(StrSearch));
                            break;
                        case "1":
                            goods = goods.Where(b => b.GoodName.Contains(StrSearch));
                            break;
                        case "2":
                            goods = goods.Where(b => b.TXM.Contains(StrSearch));
                            break;

                        default:

                            break;
                    }

                }
            }
            var lisbills = await goods.ToListAsync();

            return lisbills.GetJson<Goodinfo>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<Goodinfo>());
        }


        [HttpPost]
        public async Task<IActionResult> SaveOrderBill(OrderBill OrderBiils, List<OrderBill_MX> OrderBiils_MX)
        {
            if (OrderBiils == null)
            {
                var jsons = new
                {
                    errorMsg = "收款失败,无数据!"
                };
                return Json(jsons);
            }

            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            OrderBiils.BillID = string.Concat("OR.", now.ToString("yyyyMMddHHmmsss"));
            OrderBiils.CreateName = UserID;
            OrderBiils.CreateDate = DateTime.Now;
            OrderBiils.Status =0;
            OrderBiils.SHStatus = 0;
            OrderBiils.SHName= UserID;
            OrderBiils.SHDate= DateTime.Now;
            OrderBiils.OrderBill_MX = OrderBiils_MX;
            foreach (var item in OrderBiils_MX)
                {
                    if (item.Num == 0)
                    {
                        var json = new
                        {
                            errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                        };
                        return Json(json);
                    }
                }
           

            conn.OrderBill.Add(OrderBiils);
            try
            {
               await conn.SaveChangesAsync();
                var json = new
                {
                    okMsg = "保存成功！"
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new
                {
                    errorMsg = ex.ToString()
                };
                return Json(json);
            }


        }

    }
}