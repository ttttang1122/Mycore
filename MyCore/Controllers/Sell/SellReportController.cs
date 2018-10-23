using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models.SellData;
using MyCore.Models.Store;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyCore.Controllers.Sell
{
    public class SellReportController : BaseController
    {
        private MyCoreContext conn;

        public SellReportController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult SellReportIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SellList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<SellBill> bills = conn.SellBill;

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
                            bills = bills.Where(b => b.SellName.Contains(StrSearch));

                            break;
                        case "2":
                            bills = bills.Where(b => b.SupName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }
            await bills.ForEachAsync(x => {
                if (x.BillType == "SR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();
            return lists.GetJson<SellBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<SellBill>());
        }

        [HttpPost]
        public async Task<IActionResult> SellBill_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.SellBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            bills.ForEach(x => {
                conn.Entry(x).Reference(p => p.SellBill).Query().Load();
                if (x.SellBill.BillType == "SR") { x.Sum = x.Sum * -1; x.Num = x.Num * -1; }
            });
            return bills.GetJson<SellBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<SellBill_MX>());

        }
        [HttpPost]
        public async Task<IActionResult> GetFile(string StrSearchType, string StrSearch)
        {

            IQueryable<SellBill> bills = conn.SellBill;

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
                            bills = bills.Where(b => b.SellName.Contains(StrSearch));

                            break;
                        case "2":
                            bills = bills.Where(b => b.SupName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }
            await bills.ForEachAsync(x => {
                if (x.BillType == "SR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<SellBill>(lists, "销售统计报表", "销售统计报表", SysTool.GetPropertyNameArray<SellBill>()).GetBuffer();


            var fileName = "销售统计报表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
    }
}