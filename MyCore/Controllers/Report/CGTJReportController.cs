using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.Search;
using MyCore.Models.CGData;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace MyCore.Controllers.Report
{
    public class CGTJReportController : Controller
    {
        private MyCoreContext conn;
        public CGTJReportController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult CGTJReportIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InStoreList(string sidx, string sord, int page, int rows,BillSearch Search)
        {
            IQueryable<InStoreBill> bills = conn.InStoreBill;

            if (Search.StartDate!=null)
            {
                bills = bills.Where(b => b.BillDate >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                bills = bills.Where(b => b.BillDate <= Search.EndDate);
            }
            if (!string.IsNullOrWhiteSpace(Search.BillID))
            {
                bills = bills.Where(b => b.BillID.Contains(Search.BillID));
            }

            if (!string.IsNullOrWhiteSpace(Search.SupName))
            {
                bills = bills.Where(b => b.SupName.Contains(Search.SupName));
            }

            if (!string.IsNullOrWhiteSpace(Search.StoreName))
            {
                bills = bills.Where(b => b.StoreName.Contains(Search.StoreName));
            }
            if (!string.IsNullOrWhiteSpace(Search.YSName))
            {
                bills = bills.Where(b => b.YSName.Contains(Search.YSName));
            }

            await bills.ForEachAsync(x => {
                if (x.BillType == "BR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();
            var sums = lists.Sum(b => b.Sum);
            var userData = new
            {
                Sums= sums,
            };
            return lists.GetJson<InStoreBill>(sidx, sord, page, rows, userData, SysTool.GetPropertyNameArray<InStoreBill>());
        }

        [HttpPost]
        public async Task<IActionResult> InStore_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.InStoreBill_MX.Where(b => b.Bill_id == id).ToListAsync();
             bills.ForEach(x => {
                 conn.Entry(x).Reference(p=>p.InStoreBill).Query().Load();
                 if (x.InStoreBill.BillType == "BR") { x.Sum = x.Sum * -1;x.Num = x.Num * -1; }
            });

            return bills.GetJson<InStoreBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<InStoreBill_MX>());

        }

        [HttpPost]
        public async Task<IActionResult> GetFile(BillSearch Search)
        {

            IQueryable<InStoreBill> bills = conn.InStoreBill;

            if (Search.StartDate != null)
            {
                bills = bills.Where(b => b.BillDate >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                bills = bills.Where(b => b.BillDate <= Search.EndDate);
            }
            if (!string.IsNullOrWhiteSpace(Search.BillID))
            {
                bills = bills.Where(b => b.BillID.Contains(Search.BillID));
            }

            if (!string.IsNullOrWhiteSpace(Search.SupName))
            {
                bills = bills.Where(b => b.SupName.Contains(Search.SupName));
            }

            if (!string.IsNullOrWhiteSpace(Search.StoreName))
            {
                bills = bills.Where(b => b.StoreName.Contains(Search.StoreName));
            }
            if (!string.IsNullOrWhiteSpace(Search.YSName))
            {
                bills = bills.Where(b => b.YSName.Contains(Search.YSName));
            }

            await bills.ForEachAsync(x => {
                if (x.BillType == "BR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<InStoreBill>(lists, "采购报表", "采购报表", SysTool.GetPropertyNameArray<InStoreBill>()).GetBuffer();


            var fileName = "采购报表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }

    }
}