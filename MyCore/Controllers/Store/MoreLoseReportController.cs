using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models.SellData;
using MyCore.Models.Store;
using MyCore.Models.Search;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyCore.Controllers.Store
{
    public class MoreLoseReportController : BaseController
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
        public async Task<IActionResult> MoreLoseList(string sidx, string sord, int page, int rows, BillSearch Search)
        {


            Expression<Func<MoreLoseBill, bool>> predicate = ExpressionBuilder.True<MoreLoseBill>();
            if (Search.StartDate != null)
            {
                predicate = predicate.And(b => b.BillDate >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                predicate = predicate.And(b => b.BillDate <= Search.EndDate);
            }
            if (!string.IsNullOrWhiteSpace(Search.BillID))
            {
                predicate = predicate.And(b => b.BillID.Contains(Search.BillID));
            }

            if (!string.IsNullOrWhiteSpace(Search.YSName))
            {
                predicate = predicate.And(b => b.YSName.Contains(Search.YSName));
            }
            if (!string.IsNullOrWhiteSpace(Search.StoreName))
            {
                predicate = predicate.And(b => b.StoreName.Contains(Search.StoreName));
            }
            IQueryable<MoreLoseBill> bills = conn.MoreLoseBill.Where(predicate);

            await bills.ForEachAsync(x => {
                if (x.BillType == "LS") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();
            return lists.GetJson<MoreLoseBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<MoreLoseBill>());
        }

        [HttpPost]
        public async Task<IActionResult> MoreLose_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.MoreLoseBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            bills.ForEach(x => {
                conn.Entry(x).Reference(p => p.MoreLoseBill).Query().Load();
                if (x.MoreLoseBill.BillType == "LS") { x.Sum = x.Sum * -1; x.Num = x.Num * -1; }
            });
            return bills.GetJson<MoreLoseBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<MoreLoseBill_MX>());

        }


        [HttpPost]
        public async Task<IActionResult> GetFile(string JsonSearch)
        {
            //json字符串转模型
            var Search = SysTool.JsonToModel<BillSearch>(JsonSearch);

            Expression<Func<MoreLoseBill, bool>> predicate = ExpressionBuilder.True<MoreLoseBill>();
            if (Search.StartDate != null)
            {
                predicate = predicate.And(b => b.BillDate >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                predicate = predicate.And(b => b.BillDate <= Search.EndDate);
            }
            if (!string.IsNullOrWhiteSpace(Search.BillID))
            {
                predicate = predicate.And(b => b.BillID.Contains(Search.BillID));
            }

            if (!string.IsNullOrWhiteSpace(Search.YSName))
            {
                predicate = predicate.And(b => b.YSName.Contains(Search.YSName));
            }
            if (!string.IsNullOrWhiteSpace(Search.StoreName))
            {
                predicate = predicate.And(b => b.StoreName.Contains(Search.StoreName));
            }
            IQueryable<MoreLoseBill> bills = conn.MoreLoseBill.Where(predicate);

            await bills.ForEachAsync(x => {
                if (x.BillType == "LS") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<MoreLoseBill>(lists, "报损报溢单", "报损报溢单", SysTool.GetPropertyNameArray<MoreLoseBill>()).GetBuffer();


            var fileName = "报损报溢单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }


    }
}