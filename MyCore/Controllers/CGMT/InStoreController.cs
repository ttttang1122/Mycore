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


            IQueryable<InStoreBill> bills = conn.InStoreBill.Where(b=>b.BillType=="IS");

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
            InStoreBills.BillType = "IS";
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
                            errorMsg = item.GoodName + "的入库超过订单数量!"
                        };
                        return Json(json);
                    }
                    if (ordermx.EndNum == ordermx.Num)
                    {
                        ordermx.Status = 1;
                    }
                }

            }
            //判断订单是否全部完成
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
            var InStoreBills = await conn.InStoreBill.FirstOrDefaultAsync(b => b.id == ids);
            if (InStoreBills == null)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据不存在!"
                };
                return Json(jsons);
            }
            if (InStoreBills.SHStatus == 1)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据已审核!"
                };
                return Json(jsons);
            }
            var InStoreBills_MX = conn.InStoreBill_MX.Where(b => b.Bill_id == ids);
            var ordermxlist = conn.OrderBill_MX.Where(b => b.Bill_id == InStoreBills.OrderBill_id);
            var orderbill = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == InStoreBills.OrderBill_id);
            //处理关联订单
            foreach (var item in InStoreBills_MX)
            {
                var ordermx = await ordermxlist.FirstOrDefaultAsync(b => b.id == item.OrderRow);
                if (ordermx != null)
                {
                    ordermx.EndNum = ordermx.EndNum - item.Num;

                    if (ordermx.EndNum < 0)
                    {
                        var json = new
                        {
                            errorMsg = item.GoodName + "更新订单数量错误!删除失败"
                        };
                        return Json(json);
                    }
                   ordermx.Status = 0;

                }
                else
                {
                    var json = new
                    {
                        errorMsg = item.GoodName + "未找到对应的订单明细!删除失败"
                    };
                    return Json(json);
                }
            }
            //判断订单是否全部完成
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

                if (orderbill != null)
                {
                    orderbill.Status = 1;
                }
            }
            else
            {
                if (orderbill != null)
                {
                    orderbill.Status = 0;
                }
            }

            conn.Remove(InStoreBills);

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

        [HttpPost]
        public async Task<IActionResult> SHBill(int ids)
        {

            var InStoreBills = await conn.InStoreBill.FirstOrDefaultAsync(b => b.id == ids);
            var InStoreBills_MX = conn.InStoreBill_MX.Where(b => b.Bill_id == ids);
            if (InStoreBills.SHStatus == 1)
            {
                var jsons = new
                {
                    errorMsg = "审核失败,单据已审核!"
                };
                return Json(jsons);
            }
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            InStoreBills.SHStatus = 1;
            InStoreBills.SHName = UserID;
            InStoreBills.SHDate = now;
            var ordermxlist = conn.OrderBill_MX.Where(b => b.Bill_id == InStoreBills.OrderBill_id);         
            foreach (var item in InStoreBills_MX)
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
                    ////处理关联订单
                    //var ordermx = await ordermxlist.FirstOrDefaultAsync(b => b.id == item.OrderRow);
                    //if (ordermx != null)
                    //{
                    //    ordermx.EndNum = ordermx.EndNum + item.Num;

                    //    if (ordermx.EndNum > ordermx.Num)
                    //    {
                    //        var json = new
                    //        {
                    //            errorMsg = item.GoodName + "的入库超过订单数量!"
                    //        };
                    //        return Json(json);
                    //    }
                    //    if (ordermx.EndNum == ordermx.Num)
                    //    {
                    //        ordermx.Status = 1;
                    //    }
                    //}


               
            }
            ////判断订单是否全部完成           
            //    var orderflag = true;
            //    foreach (var item in ordermxlist)
            //    {
            //        if (item.Status == 0)
            //        {
            //            orderflag = false;
            //            break;
            //        }
            //    }
            //    if (orderflag == true)
            //    {
            //        var orderbill = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == InStoreBills.OrderBill_id);
            //        if (orderbill != null)
            //        {
            //            orderbill.Status = 1;
            //        }
            //    }
           
            try
            {
                await conn.SaveChangesAsync();
                var json = new
                {
                    okMsg = "审核成功！"
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
        public async Task<IActionResult> NOSHBill(int ids)
        {
            //
            var InStoreBills = await conn.InStoreBill.FirstOrDefaultAsync(b => b.id == ids);
            var InStoreBills_MX = conn.InStoreBill_MX.Where(b => b.Bill_id == ids);
            if (InStoreBills.SHStatus == 0)
            {
                var jsons = new
                {
                    errorMsg = "审核失败,单据未审核不可反审核!"
                };
                return Json(jsons);
            }
          
           
            foreach (var item in InStoreBills_MX)
            {
                //处理库存
                var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.Store_id == item.StroeInfo_id && b.Good_id == item.Good_id && b.SCPH == item.SCPH && b.Price == item.Price && b.MJPH == item.MJPH && b.scDate == item.scDate && b.yxqDate == item.yxqDate);
                if (stores != null)
                {
                    stores.Num = stores.Num - item.Num;
                    if (stores.Num < 0)
                    {
                        var json = new
                        {
                            errorMsg = item.GoodName + "库存不足!反审失败"
                        };
                        return Json(json);
                    }
                }
                else
                {
                    var json = new
                    {
                        errorMsg = item.GoodName + "未找到库存信息!反审失败"
                    };
                    return Json(json);
                }            
            }
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            InStoreBills.SHStatus = 0;
            InStoreBills.SHName = string.Empty;
            InStoreBills.SHDate = DateTime.MinValue;
            try
            {
                await conn.SaveChangesAsync();
                var json = new
                {
                    okMsg = "反审核成功！"
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
        public async Task<IActionResult> EditBill(int ids, string SHType, InStoreBill InStoreBills, List<InStoreBill_MX> InStoreBills_MX)
        {
            if (InStoreBills == null)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,无数据!"
                };
                return Json(jsons);
            }
            var EditInStoreBills = await conn.InStoreBill.FirstOrDefaultAsync(b => b.id == ids);
          

            var EditInStoreBills_MX = conn.InStoreBill_MX.Where(b => b.Bill_id == ids);
         
            if (EditInStoreBills.SHStatus == 1)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,该单据已审核不可修改!"
                };
                return Json(jsons);
            }
            //更新主表
            EditInStoreBills.BillDate = InStoreBills.BillDate;
            EditInStoreBills.YSName = InStoreBills.YSName;
            EditInStoreBills.YSNameID = InStoreBills.YSNameID;
            EditInStoreBills.StroeInfo_id = InStoreBills.StroeInfo_id;
            EditInStoreBills.StoreName = InStoreBills.StoreName;
            //更新明细
            foreach (var item in EditInStoreBills_MX)
            {
                item.StroeInfo_id = InStoreBills.StroeInfo_id;
                item.StoreName = InStoreBills.StoreName;
            }

            //关联的订单明细
            var ordermxlist = conn.OrderBill_MX.Where(b => b.Bill_id == EditInStoreBills.OrderBill_id);
            //循环判断原来的明细和现在的明细比较
            foreach (var item in EditInStoreBills_MX)
            {
                //当前明细是否存在原明细里
                var billmx = InStoreBills_MX.FirstOrDefault(b => b.id == item.id);
                if(billmx != null)
                {
                    var ordermx = await ordermxlist.FirstOrDefaultAsync(b => b.id == item.OrderRow);
                    if (ordermx != null)
                    {
                        ordermx.EndNum = ordermx.EndNum - item.Num +billmx.Num;
                        if (ordermx.EndNum < 0)
                        {
                            var json = new
                            {
                                errorMsg = item.GoodName + "更新订单数量错误!删除失败"
                            };
                            return Json(json);
                        }
                        if (ordermx.EndNum==ordermx.Num)
                        {
                            ordermx.Status = 1;
                        }
                        else
                        {
                            ordermx.Status =0;
                        }
                      
                    }
                    else
                    {
                        var json = new
                        {
                            errorMsg = item.GoodName + "未找到对应的订单明细!删除失败"
                        };
                        return Json(json);
                    }
                    item.Price = billmx.Price;
                    item.Num = billmx.Num;
                    item.Sum = billmx.Sum;
                    item.SCPH = billmx.SCPH;
                    item.MJPH = billmx.MJPH;
                    item.scDate = billmx.scDate;
                    item.yxqDate = billmx.yxqDate;
                    item.BZ = billmx.BZ;

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



                    }

                }
                else
                {
                    var ordermx = await ordermxlist.FirstOrDefaultAsync(b => b.id == item.OrderRow);
                    if (ordermx != null)
                    {
                        ordermx.EndNum = ordermx.EndNum - item.Num;

                        if (ordermx.EndNum < 0)
                        {
                            var json = new
                            {
                                errorMsg = item.GoodName + "更新订单数量错误!删除失败"
                            };
                            return Json(json);
                        }
                        ordermx.Status = 0;

                    }
                    else
                    {
                        var json = new
                        {
                            errorMsg = item.GoodName + "未找到对应的订单明细!删除失败"
                        };
                        return Json(json);
                    }
                    conn.InStoreBill_MX.Remove(item);
                }
            }



            EditInStoreBills.Sum = InStoreBills_MX.Sum(b => b.Sum);
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            if (SHType == "yes")
            {
                EditInStoreBills.SHStatus = 1;
                EditInStoreBills.SHName = UserID;
                EditInStoreBills.SHDate = now;
            }
         
            //判断订单是否全部完成
            var orderflag = true;
            foreach (var item in ordermxlist)
            {
                if (item.Status == 0)
                {
                    orderflag = false;
                    break;
                }
            }
            var orderbill = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == EditInStoreBills.OrderBill_id);
            if (orderflag == true)
            {
               
                if (orderbill != null)
                {
                    orderbill.Status = 1;
                }
            }
            else
            {
                if (orderbill != null)
                {
                    orderbill.Status = 0;
                }
            }

            try
            {
                await conn.SaveChangesAsync();
                var json = new
                {
                    okMsg = "修改成功！"
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
        public async Task<IActionResult> GetFile(string StrSearchType, string StrSearch)
        {

            IQueryable<InStoreBill> bills = conn.InStoreBill.Where(b => b.BillType == "IS");

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

            byte[] buffer = ExcelHelp.Export<InStoreBill>(lists, "采购入库单", "采购入库单", SysTool.GetPropertyNameArray<InStoreBill>()).GetBuffer();


            var fileName = "采购入库单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
        public async Task<IActionResult> GetBillList(int ids)
        {
            var instorebill = await conn.InStoreBill.FirstOrDefaultAsync(b => b.id == ids);
            conn.Entry(instorebill).Collection(p => p.InStoreBill_MX).Query().Load();
            var data = new
            {
                bills = instorebill
            };
            return Json(data);
        }
    }
}