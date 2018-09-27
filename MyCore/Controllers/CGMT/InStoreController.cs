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
    public class InStoreController : Controller
    {
        private MyCoreContext conn;
        public InStoreController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult InStoreIndex()
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

        public IActionResult AddEditOrderIndex()
        {
            return View();
        }
        public IActionResult ChooseOrderIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OrderBillList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<OrderBill> bills = conn.OrderBill.Where(b=>b.SHStatus==1 &&b.Status==0);

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
                            bills = bills.Where(b => b.CGName.Contains(StrSearch));

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
            return lists.GetJson<OrderBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<OrderBill>());
        }

        [HttpPost]
        public async Task<IActionResult> GetOrder(int ids)
        {
            var orderBills = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == ids);
            conn.Entry(orderBills).Collection(p => p.OrderBill_MX).Query().Load();
            var data = new
            {
                bills = orderBills
            };
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetStoreInfo()
        {
            var stores = await conn.StoreInfo.ToListAsync();
            var data = stores.Select(p => new { p.id, p.StoreName });
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> SaveBill( string SHType, InStoreBill InStoreBills ,List<InStoreBill_MX> InStoreBills_MX)
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
            InStoreBills.BillID = string.Concat("IS.", now.ToString("yyyyMMddHHmmsss"));
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
            //关联订单明细
            var ordermxlist = conn.OrderBill_MX.Where(b => b.Bill_id == InStoreBills.OrderBill_id);
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
                    var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.Store_id == item.StroeInfo_id && b.Good_id == item.Good_id && b.SCPH == item.SCPH && b.Price == item.Price && b.MJPH == item.MJPH && b.scDate == item.scDate && b.yxqDate == item.yxqDate);
                    if (stores != null)
                    {
                        stores.Num = stores.Num + item.Num;
                    }
                    else
                    {
                        var store = new GoodsStore();
                        store.Store_id = InStoreBills.StroeInfo_id;
                        store.StoreName = InStoreBills.StoreName;
                        store.Good_id = item.Good_id;
                        store.GoodID = item.GoodID;
                        store.GoodName = item.GoodName;
                        store.DW = item.DW;
                        store.GGType = item.GGType;
                        store.ModelType = item.ModelType;
                        store.SCCJ = item.SCCJ;
                        store.Price = item.Price;
                        store.Num = item.Num;
                        store.SCPH = item.SCPH;
                        store.MJPH = item.MJPH;
                        store.scDate = item.scDate;
                        store.yxqDate = item.yxqDate;
                        conn.GoodsStore.Add(store);
                    }
                    //处理关联订单
                    var ordermx = await ordermxlist.FirstOrDefaultAsync(b => b.id == item.OrderRow);
                    if (ordermx != null)
                    {
                        ordermx.EndNum = ordermx.EndNum + item.Num;
                      
                        if (ordermx.EndNum > ordermx.Num)
                        {
                            var json = new
                            {
                                errorMsg = item.GoodName+ "的入库超过订单数量!"
                            };
                            return Json(json);
                        }
                        if(ordermx.EndNum == ordermx.Num)
                        {
                            ordermx.Status = 1;
                        }
                    }


                }


            }
            //判断订单是否全部完成
            if (SHType == "yes")
            {
                var orderflag = true;
               
                foreach (var item in ordermxlist)
                {
                    if (item.Status == 0)
                    {
                        orderflag = false;
                        break;
                    }
                }
                if (orderflag == true)
                {
                    var orderbill = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == InStoreBills.OrderBill_id);
                    if (orderbill != null)
                    {
                        orderbill.Status = 1;
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
        [HttpPost]
        public async Task<IActionResult> DeleteBill(int ids)
        {

            var insrore = await conn.InStoreBill.FirstOrDefaultAsync(b => b.id == ids);
            conn.Entry(insrore).Collection(p => p.InStoreBill_MX).Query().Load();
            if (insrore == null)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据不存在!"
                };
                return Json(jsons);
            }
            if (insrore.SHStatus == 1)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据已审核!"
                };
                return Json(jsons);
            }
            conn.Remove(insrore);

            try
            {
                await conn.SaveChangesAsync();
                var json = new
                {
                    okMsg = "删除成功！"
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