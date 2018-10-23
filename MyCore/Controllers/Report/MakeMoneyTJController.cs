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
    public class MakeMoneyTJController : BaseController
    {
        private MyCoreContext conn;
        public MakeMoneyTJController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult MakeMoneyTJIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetDatas(int years)
        {
            //取得全年数据
            var yearbeginDate = new DateTime(years, 1, 1);
            var yearendDate = new DateTime(years, 12, 31);
            IQueryable<SellBill> bills = conn.SellBill.Where(b => b.BillDate >= yearbeginDate && b.BillDate <= yearendDate && b.Status == 1);
            await bills.ForEachAsync(x => {
                if (x.BillType == "SR") { x.Sum = x.Sum * -1; }
            });
            var lists = await bills.ToListAsync();


            var mxbills = await conn.SellBill_MX.ToListAsync();
            mxbills.ForEach(x => {
                conn.Entry(x).Reference(p => p.SellBill).Query().Load();
                if (x.SellBill.BillType == "SR") { x.Sum = x.Sum * -1; x.Num = x.Num * -1; }
            });

            List<TakeMoneyTJBillcs> mlbills = mxbills.Select(p => new TakeMoneyTJBillcs { id = p.id, Bill_id = p.Bill_id, StroeInfo_id = p.StroeInfo_id, StoreName = p.StoreName, OrderRow = p.StoreRow, Good_id = p.Good_id, GoodID = p.GoodID, GoodName = p.GoodName, DW = p.DW, GGType = p.GGType, ModelType = p.ModelType, SCCJ = p.SCCJ,InPrice=p.InPrice,  SellPrice = p.SellPrice, Num = p.Num, SellSum = p.Sum,InSum=p.Num *p.InPrice, SCPH = p.SCPH, MJPH = p.MJPH, scDate = p.scDate, yxqDate = p.yxqDate, BZ = p.BZ, BillID = p.SellBill.BillID, BillDate = p.SellBill.BillDate, YSNameID = p.SellBill.SellNameID, YSName = p.SellBill.SellName, Sup_id = p.SellBill.Sup_id, SupName = p.SellBill.SupName, BillType = p.SellBill.BillType, Status = p.SellBill.Status }).ToList();
            mlbills = mlbills.Where(b => b.BillDate >= yearbeginDate && b.BillDate <= yearendDate && b.Status == 1).ToList();

            List<decimal?> intList = new List<decimal?>();
            for (int i = 1; i <= 12; i++)
            {

                var beginDate = new DateTime(years, i, 1);
                var endDate = beginDate.AddMonths(1).AddDays(-1);
                //销售额
                var SellSum = lists.Where(b => b.BillDate >= beginDate && b.BillDate <= endDate).Sum(x => x.Sum);
                //快递费
                var GiveSum= lists.Where(b => b.BillDate >= beginDate && b.BillDate <= endDate).Sum(x => x.GiveSum);
                //成本
                var InSum= mlbills.Where(b => b.BillDate >= beginDate && b.BillDate <= endDate).Sum(x => x.InSum);

                var moneySum = SellSum - GiveSum - InSum;

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