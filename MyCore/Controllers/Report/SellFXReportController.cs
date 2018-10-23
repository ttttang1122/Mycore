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
    public class SellFXReportController : BaseController
    {
        private MyCoreContext conn;
        public SellFXReportController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult SellFXReportIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetDatas(int years)
        {
            //取得全年数据
            var yearbeginDate = new DateTime(years, 1, 1);
            var yearendDate = new DateTime(years, 12, 31);
            IQueryable<SellBill> bills = conn.SellBill.Where(b => b.BillDate >= yearbeginDate && b.BillDate <= yearendDate&&b.Status==1);
            await bills.ForEachAsync(x => {
                if (x.BillType == "SR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();


            List<decimal?> intList = new List<decimal?>();
            for (int i = 1; i <= 12; i++)
            {

                var beginDate = new DateTime(years, i, 1);
                var endDate = beginDate.AddMonths(1).AddDays(-1);
                var moneySum = lists.Where(b => b.BillDate >= beginDate && b.BillDate <= endDate).Sum(x => x.Sum);
                if (moneySum == null)
                {
                    moneySum = 0;
                }
                intList.Add(moneySum);
            }
            decimal?[] CGData = intList.ToArray();

            var FXData = new
            {
                datas = CGData,
            };

            return Json(FXData);
        }
    }
}