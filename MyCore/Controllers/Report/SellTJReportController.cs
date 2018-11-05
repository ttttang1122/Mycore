using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.Search;
using MyCore.Models.SellData;
using Microsoft.EntityFrameworkCore;
namespace MyCore.Controllers.Report
{
    public class SellTJReportController : BaseController
    {
        private MyCoreContext conn;
        public SellTJReportController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult SellTJReportIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SellList(string sidx, string sord, int page, int rows, BillSearch Search)
        {
            IQueryable<SellBill> bills = conn.SellBill.Where(b=>b.Status==1);

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
                bills = bills.Where(b => b.SellName.Contains(Search.YSName));
            }

            await bills.ForEachAsync(x => {
                if (x.BillType == "SR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();
            var sums = lists.Sum(b => b.Sum);
            var gives = lists.Sum(b => b.GiveSum);
            var userData = new
            {
                Sums = sums,
                givesums= gives,
            };
            return lists.GetJson<SellBill>(sidx, sord, page, rows, userData, SysTool.GetPropertyNameArray<SellBill>());
        }

        [HttpPost]
        public async Task<IActionResult> Sell_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.SellBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            bills.ForEach(x => {
                conn.Entry(x).Reference(p => p.SellBill).Query().Load();
                if (x.SellBill.BillType == "SR") { x.Sum = x.Sum * -1; x.Num = x.Num * -1; }
            });

            return bills.GetJson<SellBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<SellBill_MX>());

        }

        [HttpPost]
        public async Task<IActionResult> GetFile(string JsonSearch)
        {
            var Search = SysTool.JsonToModel<BillSearch>(JsonSearch);

            IQueryable<SellBill> bills = conn.SellBill.Where(b => b.Status == 1);

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
                bills = bills.Where(b => b.SellName.Contains(Search.YSName));
            }

            await bills.ForEachAsync(x => {
                if (x.BillType == "SR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<SellBill>(lists, "销售报表", "销售报表", SysTool.GetPropertyNameArray<SellBill>()).GetBuffer();


            var fileName = "销售报表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
    }
}