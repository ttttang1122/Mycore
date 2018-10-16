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

namespace MyCore.Controllers.Store
{
    public class MoreLoseReportController : Controller
    {
        private MyCoreContext conn;

        public MoreLoseReportController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult MoreLoseReportIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MoreLoseList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<MoreLoseBill> bills = conn.MoreLoseBill;

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
                            bills = bills.Where(b => b.YSName.Contains(StrSearch));

                            break;
                        case "2":
                            bills = bills.Where(b => b.CreateName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();
            return lists.GetJson<MoreLoseBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<MoreLoseBill>());
        }

        [HttpPost]
        public async Task<IActionResult> MoreLose_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.MoreLoseBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            return bills.GetJson<MoreLoseBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<MoreLoseBill_MX>());

        }


        [HttpPost]
        public async Task<IActionResult> GetFile(string StrSearchType, string StrSearch)
        {


            IQueryable<MoreLoseBill> bills = conn.MoreLoseBill;

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
                            bills = bills.Where(b => b.YSName.Contains(StrSearch));

                            break;
                        case "2":
                            bills = bills.Where(b => b.CreateName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<MoreLoseBill>(lists, "报损报溢单", "报损报溢单", SysTool.GetPropertyNameArray<MoreLoseBill>()).GetBuffer();


            var fileName = "报损报溢单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }


    }
}