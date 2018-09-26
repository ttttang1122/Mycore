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

namespace MyCore.Controllers.CGMT
{
    public class InStoreController : Controller
    {
        private MyCoreContext conn;
        public InStoreController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult InStoreIndex()
        {
            return View();
        }
        public IActionResult AddEditOrderIndex()
        {
            return View();
        }
        public IActionResult ChooseOrderIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OrderBillList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<OrderBill> bills = conn.OrderBill.Where(b=>b.SHStatus==1 &&b.Status==0);

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
        public async Task<IActionResult> GetOrder(int ids)
        {
            var orderBills = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == ids);
            conn.Entry(orderBills).Collection(p => p.OrderBill_MX).Query().Load();
            var data = new
            {
                bills = orderBills
            };
            return Json(data);
        }
    }
}