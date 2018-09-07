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

            var bills = await conn.OrderBill.FirstOrDefaultAsync(b=>b.id==id);   
            var lists = bills.OrderBill_MX;
            return lists.GetJson<OrderBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<OrderBill_MX>());

        }
        public IActionResult AddOrderIndex()
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


    }
}