﻿using System;
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
    public class SellMXReportController : BaseController
    {
        private MyCoreContext conn;
        public SellMXReportController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult SellMXReportIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SellMXList(string sidx, string sord, int page, int rows, MXBillSearch Search)
        {
            var mxbills = await conn.SellBill_MX.ToListAsync();
            mxbills.ForEach(x => {
                conn.Entry(x).Reference(p => p.SellBill).Query().Load();
                if (x.SellBill.BillType == "SR") { x.Sum = x.Sum * -1; x.Num = x.Num * -1; }
            });

            var bills = mxbills.Select(p => new CGMXReport { id = p.id, Bill_id = p.Bill_id, StroeInfo_id = p.StroeInfo_id, StoreName = p.StoreName, OrderRow = p.StoreRow, Good_id = p.Good_id, GoodID = p.GoodID, GoodName = p.GoodName, DW = p.DW, GGType = p.GGType, ModelType = p.ModelType, SCCJ = p.SCCJ, Price = p.SellPrice, Num = p.Num, Sum = p.Sum, SCPH = p.SCPH, MJPH = p.MJPH, scDate = p.scDate, yxqDate = p.yxqDate, BZ = p.BZ, BillID = p.SellBill.BillID, BillDate = p.SellBill.BillDate, YSNameID = p.SellBill.SellNameID, YSName = p.SellBill.SellName, Sup_id = p.SellBill.Sup_id, SupName = p.SellBill.SupName, BillType = p.SellBill.BillType,Status=p.SellBill.Status });
            bills = bills.Where(b => b.Status == 1);
            if (Search.StartDate != null)
            {
                bills = bills.Where(b => b.BillDate >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                bills = bills.Where(b => b.BillDate <= Search.EndDate);
            }
            if (!string.IsNullOrWhiteSpace(Search.GoodName))
            {
                bills = bills.Where(b => b.GoodName.Contains(Search.GoodName));
            }

            if (!string.IsNullOrWhiteSpace(Search.SupName))
            {
                bills = bills.Where(b => b.SupName.Contains(Search.SupName));
            }

            if (!string.IsNullOrWhiteSpace(Search.GGType))
            {
                bills = bills.Where(b => b.GGType.Contains(Search.GGType));
            }
            if (!string.IsNullOrWhiteSpace(Search.ModelType))
            {
                bills = bills.Where(b => b.ModelType.Contains(Search.ModelType));
            }
            if (!string.IsNullOrWhiteSpace(Search.SCPH))
            {
                bills = bills.Where(b => b.SCPH.Contains(Search.SCPH));
            }
            if (!string.IsNullOrWhiteSpace(Search.MJPH))
            {
                bills = bills.Where(b => b.MJPH.Contains(Search.MJPH));
            }
            if (!string.IsNullOrWhiteSpace(Search.SCCJ))
            {
                bills = bills.Where(b => b.SCCJ.Contains(Search.SCCJ));
            }

            var lists = bills.ToList();
            var sums = lists.Sum(b => b.Sum);
            var nums = lists.Sum(b => b.Num);
            var userData = new
            {
                Nums = nums,
                Sums = sums,
            };
            return lists.GetJson<CGMXReport>(sidx, sord, page, rows, userData, SysTool.GetPropertyNameArray<CGMXReport>());
        }



        [HttpPost]
        public async Task<IActionResult> GetFile(string JosnSearch)
        {
            var Search = SysTool.JsonToModel<MXBillSearch>(JosnSearch);

            var mxbills = await conn.SellBill_MX.ToListAsync();
            mxbills.ForEach(x => {
                conn.Entry(x).Reference(p => p.SellBill).Query().Load();
                if (x.SellBill.BillType == "SR") { x.Sum = x.Sum * -1; x.Num = x.Num * -1; }
            });

            var bills = mxbills.Select(p => new CGMXReport { id = p.id, Bill_id = p.Bill_id, StroeInfo_id = p.StroeInfo_id, StoreName = p.StoreName, OrderRow = p.StoreRow, Good_id = p.Good_id, GoodID = p.GoodID, GoodName = p.GoodName, DW = p.DW, GGType = p.GGType, ModelType = p.ModelType, SCCJ = p.SCCJ, Price = p.SellPrice, Num = p.Num, Sum = p.Sum, SCPH = p.SCPH, MJPH = p.MJPH, scDate = p.scDate, yxqDate = p.yxqDate, BZ = p.BZ, BillID = p.SellBill.BillID, BillDate = p.SellBill.BillDate, YSNameID = p.SellBill.SellNameID, YSName = p.SellBill.SellName, Sup_id = p.SellBill.Sup_id, SupName = p.SellBill.SupName, BillType = p.SellBill.BillType, Status = p.SellBill.Status });
            bills = bills.Where(b => b.Status == 1);
            if (Search.StartDate != null)
            {
                bills = bills.Where(b => b.BillDate >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                bills = bills.Where(b => b.BillDate <= Search.EndDate);
            }
            if (!string.IsNullOrWhiteSpace(Search.GoodName))
            {
                bills = bills.Where(b => b.GoodName.Contains(Search.GoodName));
            }

            if (!string.IsNullOrWhiteSpace(Search.SupName))
            {
                bills = bills.Where(b => b.SupName.Contains(Search.SupName));
            }

            if (!string.IsNullOrWhiteSpace(Search.GGType))
            {
                bills = bills.Where(b => b.GGType.Contains(Search.GGType));
            }
            if (!string.IsNullOrWhiteSpace(Search.ModelType))
            {
                bills = bills.Where(b => b.ModelType.Contains(Search.ModelType));
            }
            if (!string.IsNullOrWhiteSpace(Search.SCPH))
            {
                bills = bills.Where(b => b.SCPH.Contains(Search.SCPH));
            }
            if (!string.IsNullOrWhiteSpace(Search.MJPH))
            {
                bills = bills.Where(b => b.MJPH.Contains(Search.MJPH));
            }
            if (!string.IsNullOrWhiteSpace(Search.SCCJ))
            {
                bills = bills.Where(b => b.SCCJ.Contains(Search.SCCJ));
            }

            var lists = bills.ToList();
            byte[] buffer = ExcelHelp.Export<CGMXReport>(lists, "销售明细报表", "销售明细报表", SysTool.GetPropertyNameArray<CGMXReport>()).GetBuffer();


            var fileName = "销售明细报表" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }

    }
}