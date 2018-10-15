using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models.CGData;
using MyCore.Models.Store;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace MyCore.Controllers.CGMT
{
    public class CGReportController : BaseController
    {
        private MyCoreContext conn;
        public CGReportController(MyCoreContext _conn)
        {
            conn = _conn;
        }

        public IActionResult CGReportIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InStoreList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<InStoreBill> bills = conn.InStoreBill;

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
                            bills = bills.Where(b => b.SupName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();
            return lists.GetJson<InStoreBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<InStoreBill>());
        }

        [HttpPost]
        public async Task<IActionResult> InStore_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.InStoreBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            return bills.GetJson<InStoreBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<InStoreBill_MX>());

        }

        [HttpPost]
        public async Task<IActionResult> GetFile(string StrSearchType, string StrSearch)
        {

            IQueryable<InStoreBill> bills = conn.InStoreBill;

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
                            bills = bills.Where(b => b.SupName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<InStoreBill>(lists, "采购报表", "采购报表", SysTool.GetPropertyNameArray<InStoreBill>()).GetBuffer();


            var fileName = "采购报表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }


    }
}