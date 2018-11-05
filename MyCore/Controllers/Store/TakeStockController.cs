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
    public class TakeStockController : BaseController
    {
        private MyCoreContext conn;

        public TakeStockController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult TakeStockIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TakeStockList(string sidx, string sord, int page, int rows, BillSearch Search)
        {

            Expression<Func<TakeStockBill, bool>> predicate = ExpressionBuilder.True<TakeStockBill>();
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

            IQueryable<TakeStockBill> bills = conn.TakeStockBill.Where(predicate);

            var lists = await bills.ToListAsync();
            return lists.GetJson<TakeStockBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<TakeStockBill>());
        }

        [HttpPost]
        public async Task<IActionResult> TakeStock_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.TakeStockBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            return bills.GetJson<TakeStockBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<TakeStockBill_MX>());

        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await conn.User.ToListAsync();
            var data = users.Select(p => new { p.id, p.UserName });
            return Content(data.ToJson());
        }

        [HttpGet]
        public async Task<IActionResult> GetStoreInfo()
        {
            var stores = await conn.StoreInfo.ToListAsync();
            var data = stores.Select(p => new { p.id, p.StoreName });
            return Content(data.ToJson());
        }

        public IActionResult AddTakeStockIndex()
        {
            return View();
        }

        public IActionResult AddRowIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GoodsStoreList(string sidx, string sord, int page, int rows, int Store_id, string StrSearchType, string StrSearch, string Check)
        {
            IQueryable<GoodsStore> bills = conn.GoodsStore.Where(b => b.Store_id == Store_id);
            if (!string.IsNullOrWhiteSpace(Check))
            {
                if (Check == "是")
                {
                    bills = bills.Where(b => b.Num >= 0);
                }
                else if (Check == "否")
                {
                    bills = bills.Where(b => b.Num > 0);
                }
            }
            else
            {
                bills = bills.Where(b => b.Num > 0);
            }
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
        public async Task<IActionResult> SaveBill(string SHType, TakeStockBill Bills, List<TakeStockBill_MX> Bills_MX)
        {
            if (Bills == null)
            {
                var jsons = new
                {
                    errorMsg = "盘点失败,无数据!"
                };
                return Json(jsons);
            }
            //盘点主表
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            Bills.BillID = string.Concat("PD.", now.ToString("yyyyMMddHHmmsss"));
            Bills.CreateName = UserID;
            Bills.CreateDate = DateTime.Now;
            Bills.TakeStockBill_MX = Bills_MX;
            //加载盘点单
            conn.TakeStockBill.Add(Bills);

            List<TakeStockBill_MX> loseTake = new List<TakeStockBill_MX>();
            List<TakeStockBill_MX> moretake = new List<TakeStockBill_MX>();
            //组装报损报溢单
            foreach (var item in Bills_MX)
            {

                //判断盘点数量不小于0
                if (item.TakeNum < 0)
                {
                    var json = new
                    {
                        errorMsg = "盘点失败," + item.GoodName + " 请输入正确数量!"
                    };
                    return Json(json);
                }
                item.StroeInfo_id = Bills.StroeInfo_id;
                item.StoreName = Bills.StoreName;

                if (item.HowNum > 0)
                {
                    moretake.Add(item);
                }
                else if (item.HowNum < 0)
                {
                    loseTake.Add(item);
                }

            }
            //判断报损报溢明细表是否大于一条记录
            if ((loseTake.Count + moretake.Count) > 0)
            {
                //报损单
                if (loseTake.Count > 0)
                {
                    //主表
                    MoreLoseBill loseBill = new MoreLoseBill();
                    List<MoreLoseBill_MX> loseBill_MX = new List<MoreLoseBill_MX>();
                    loseBill.BillID = string.Concat("LS.", now.ToString("yyyyMMddHHmmsss"));
                    loseBill.BillType = "LS";
                    loseBill.BillDate = DateTime.Now;
                    loseBill.YSName = Bills.YSName;
                    loseBill.YSNameID = Bills.YSNameID;
                    loseBill.StroeInfo_id = Bills.StroeInfo_id;
                    loseBill.StoreName = Bills.StoreName;
                    loseBill.CreateName = UserID;
                    loseBill.CreateDate = DateTime.Now;
                    loseBill.Status = 1;
                    loseBill.SHName = UserID;
                    loseBill.SHDate = now;
                    //明细表
                    foreach (var item in loseTake)
                    {
                        var m = new MoreLoseBill_MX();
                        m.StroeInfo_id = Bills.StroeInfo_id;
                        m.StoreName = Bills.StoreName;
                        m.StoreRow = item.StoreRow;
                        m.Good_id = item.Good_id;
                        m.GoodID = item.GoodID;
                        m.GoodName = item.GoodName;
                        m.DW = item.DW;
                        m.GGType = item.GGType;
                        m.ModelType = item.ModelType;
                        m.SCCJ = item.SCCJ;
                        m.Num = item.HowNum * -1;
                        m.Price = item.Price;
                        m.Sum = m.Num * m.Price;
                        m.SCPH = item.SCPH;
                        m.MJPH = item.MJPH;
                        m.scDate = item.scDate;
                        m.yxqDate = item.yxqDate;
                        loseBill_MX.Add(m);
                    }
                    loseBill.MoreLoseBill_MX = loseBill_MX;
                    loseBill.Sum = loseBill_MX.Sum(b => b.Sum);
                    //处理明细表改变库存数量
                    foreach (var item in loseBill_MX)
                    {
                        var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.StoreRow);
                        if (stores != null)
                        {
                            stores.Num = stores.Num - item.Num;

                            if (stores.Num < 0)
                            {
                                var json = new
                                {
                                    errorMsg = "盘点失败,生成报损单" + item.GoodName + " 库存不足!"
                                };
                                return Json(json);
                            }
                        }
                        else
                        {
                            var json = new
                            {
                                errorMsg = "盘点失败,生成报损单" + item.GoodName + " 未找到库存!"
                            };
                            return Json(json);
                        }
                    }
                    conn.MoreLoseBill.Add(loseBill);
                }

                //报溢单
                if (moretake.Count > 0)
                {
                    //主表
                    MoreLoseBill moreBill = new MoreLoseBill();
                    List<MoreLoseBill_MX> moreBill_MX = new List<MoreLoseBill_MX>();
                    moreBill.BillID = string.Concat("MR.", now.ToString("yyyyMMddHHmmsss"));
                    moreBill.BillType = "MR";
                    moreBill.BillDate = DateTime.Now;
                    moreBill.YSName = Bills.YSName;
                    moreBill.YSNameID = Bills.YSNameID;
                    moreBill.StroeInfo_id = Bills.StroeInfo_id;
                    moreBill.StoreName = Bills.StoreName;
                    moreBill.CreateName = UserID;
                    moreBill.CreateDate = DateTime.Now;
                    moreBill.Status = 1;
                    moreBill.SHName = UserID;
                    moreBill.SHDate = now;
                    //明细表
                    foreach (var item in moretake)
                    {
                        var m = new MoreLoseBill_MX();
                        m.StroeInfo_id = Bills.StroeInfo_id;
                        m.StoreName = Bills.StoreName;
                        m.StoreRow = item.StoreRow;
                        m.Good_id = item.Good_id;
                        m.GoodID = item.GoodID;
                        m.GoodName = item.GoodName;
                        m.DW = item.DW;
                        m.GGType = item.GGType;
                        m.ModelType = item.ModelType;
                        m.SCCJ = item.SCCJ;
                        m.Num = item.HowNum;
                        m.Price = item.Price;
                        m.Sum = m.Num * m.Price;
                        m.SCPH = item.SCPH;
                        m.MJPH = item.MJPH;
                        m.scDate = item.scDate;
                        m.yxqDate = item.yxqDate;
                        moreBill_MX.Add(m);
                    }
                    moreBill.MoreLoseBill_MX = moreBill_MX;
                    moreBill.Sum = moreBill_MX.Sum(b => b.Sum);
                    //处理明细表改变库存数量
                    foreach (var item in moreBill_MX)
                    {
                        //处理库存
                        var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.StoreRow);
                        if (stores != null)
                        {
                            stores.Num = stores.Num + item.Num;


                        }
                        else
                        {
                            var json = new
                            {
                                errorMsg = "盘点失败,生成报溢单" + item.GoodName + " 未找到库存!"
                            };
                            return Json(json);
                        }
                    }
                    conn.MoreLoseBill.Add(moreBill);
                }

            }
            else
            {
                var json = new
                {
                    errorMsg = "盘点失败,盘点的商品信息无数据改变"
                };
                return Json(json);
            }

            try
            {
                await conn.SaveChangesAsync();
                var json = new
                {
                    okMsg = "盘点成功！"
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

        [HttpPost]
        public async Task<IActionResult> GetFile(string JsonSearch)
        {
            //json字符串转模型
            var Search = SysTool.JsonToModel<BillSearch>(JsonSearch);

            Expression<Func<TakeStockBill, bool>> predicate = ExpressionBuilder.True<TakeStockBill>();
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

            IQueryable<TakeStockBill> bills = conn.TakeStockBill.Where(predicate);

            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<TakeStockBill>(lists, "盘点单", "盘点单", SysTool.GetPropertyNameArray<TakeStockBill>()).GetBuffer();


            var fileName = "盘点单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }

    }
}