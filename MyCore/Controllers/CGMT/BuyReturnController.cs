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
    public class BuyReturnController : Controller
    {
        private MyCoreContext conn;
        public BuyReturnController(MyCoreContext _conn)
        {
            conn = _conn;
        }

        public IActionResult BuyReturnIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InStoreList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<InStoreBill> bills = conn.InStoreBill.Where(b => b.BillType == "BR");

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

        public IActionResult AddBuyReturnIndex()
        {
            return View();
        }

        public IActionResult AddRowIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GoodsStoreList(string sidx, string sord, int page, int rows,int Store_id, string StrSearchType, string StrSearch)
        {
            IQueryable<GoodsStore> bills = conn.GoodsStore.Where(b => b.Num > 0&&b.Store_id== Store_id);
            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            bills = bills.Where(b => b.GoodName.Contains(StrSearch));
                            break;
                        case "1":
                            bills = bills.Where(b => b.GoodID.Contains(StrSearch));
                            break;

                        default:

                            break;
                    }

                }
            }
            var lisbills = await bills.ToListAsync();

            return lisbills.GetJson<GoodsStore>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<GoodsStore>());
        }
        [HttpPost]
        public async Task<IActionResult> SaveBill(string SHType, InStoreBill InStoreBills, List<InStoreBill_MX> InStoreBills_MX)
        {
            if (InStoreBills == null)
            {
                var jsons = new
                {
                    errorMsg = "保存失败,无数据!"
                };
                return Json(jsons);
            }

            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            InStoreBills.BillID = string.Concat("BR.", now.ToString("yyyyMMddHHmmsss"));
            InStoreBills.BillType = "BR";
            InStoreBills.CreateName = UserID;
            InStoreBills.CreateDate = DateTime.Now;
            InStoreBills.SHStatus = 0;
            InStoreBills.InStoreBill_MX = InStoreBills_MX;
            if (SHType == "yes")
            {
                InStoreBills.SHStatus = 1;
                InStoreBills.SHName = UserID;
                InStoreBills.SHDate = now;
            }
            foreach (var item in InStoreBills_MX)
            {
                if (item.Num == 0)
                {
                    var json = new
                    {
                        errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                    };
                    return Json(json);
                }
                item.StroeInfo_id = InStoreBills.StroeInfo_id;
                item.StoreName = InStoreBills.StoreName;
                item.Sum = item.Num * item.Price;
                //审核
                if (SHType == "yes")
                {
                    //处理库存
                    var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.OrderRow);
                    if (stores != null)
                    {
                        stores.Num = stores.Num - item.Num;

                        if (stores.Num < 0)
                        {
                            var json = new
                            {
                                errorMsg = "保存失败," + item.GoodName + " 库存不足!"
                            };
                            return Json(json);
                        }
                    }
                    else
                    {
                        var json = new
                        {
                            errorMsg = "保存失败," + item.GoodName + " 未找到库存!"
                        };
                        return Json(json);
                    }



                }
      
      

            }          
            InStoreBills.Sum = InStoreBills_MX.Sum(b => b.Sum);

            conn.InStoreBill.Add(InStoreBills);
            try
            {
                await conn.SaveChangesAsync();
                var json = new
                {
                    okMsg = "保存成功！"
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new
                {
                    errorMsg = ex.ToString()
                };
                return Json(json);
            }

        }
    }
}