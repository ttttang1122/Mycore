using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.Search;
using MyCore.Models.CGData;
using Microsoft.EntityFrameworkCore;
namespace MyCore.Controllers.Report
{
    public class CGFXReportController : BaseController
    {
        private MyCoreContext conn;
        public CGFXReportController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult CGFXReportIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetDatas(int years)
        {
            //取得全年数据
            var  yearbeginDate = new DateTime(years, 1, 1);
            var  yearendDate = new DateTime(years, 12, 31);
            IQueryable<InStoreBill> bills = conn.InStoreBill.Where(b=>b.BillDate>= yearbeginDate && b.BillDate<= yearendDate&&b.SHStatus==1);
            await bills.ForEachAsync(x => {
                if (x.BillType == "BR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();

           
            List<decimal?> intList = new List<decimal?>();
            for (int i = 1; i <= 12; i++)
            {
                
                var beginDate = new DateTime(years, i, 1);
                var endDate = beginDate.AddMonths(1).AddDays(-1);
                var moneySum = lists.Where(b => b.BillDate >= beginDate && b.BillDate <= endDate).Sum(x=>x.Sum);
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