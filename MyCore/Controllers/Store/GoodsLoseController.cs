using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models.SellData;
using MyCore.Models.Store;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace MyCore.Controllers.Store
{
    public class GoodsLoseController : Controller
    {
        private MyCoreContext conn;

        public GoodsLoseController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult GoodsLoseIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GoodsLoseList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<MoreLoseBill> bills = conn.MoreLoseBill.Where(b => b.BillType == "LS");

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
                            bills = bills.Where(b => b.CreateName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();
            return lists.GetJson<MoreLoseBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<MoreLoseBill>());
        }

        [HttpPost]
        public async Task<IActionResult> GoodsLose_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.MoreLoseBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            return bills.GetJson<MoreLoseBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<MoreLoseBill_MX>());

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

        public IActionResult AddEditGoodsLoseIndex()
        {
            return View();
        }

        public IActionResult AddRowIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GoodsStoreList(string sidx, string sord, int page, int rows, int Store_id, string StrSearchType, string StrSearch)
        {
            IQueryable<GoodsStore> bills = conn.GoodsStore.Where(b => b.Num > 0 && b.Store_id == Store_id);
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
        public async Task<IActionResult> SaveBill(string SHType, MoreLoseBill MoreLoseBills, List<MoreLoseBill_MX> MoreLoseBills_MX)
        {
            if (MoreLoseBills == null)
            {
                var jsons = new
                {
                    errorMsg = "保存失败,无数据!"
                };
                return Json(jsons);
            }

            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            MoreLoseBills.BillID = string.Concat("LS.", now.ToString("yyyyMMddHHmmsss"));
            MoreLoseBills.BillType = "LS";
            MoreLoseBills.CreateName = UserID;
            MoreLoseBills.CreateDate = DateTime.Now;
            MoreLoseBills.Status = 0;
            MoreLoseBills.MoreLoseBill_MX = MoreLoseBills_MX;
            if (SHType == "yes")
            {
                MoreLoseBills.Status = 1;
                MoreLoseBills.SHName = UserID;
                MoreLoseBills.SHDate = now;
            }
            foreach (var item in MoreLoseBills_MX)
            {
                if (item.Num == 0)
                {
                    var json = new
                    {
                        errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                    };
                    return Json(json);
                }
                item.StroeInfo_id = MoreLoseBills.StroeInfo_id;
                item.StoreName = MoreLoseBills.StoreName;
                item.Sum = item.Num * item.Price;
                //审核
                if (SHType == "yes")
                {
                    //处理库存
                    var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.StoreRow);
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
            MoreLoseBills.Sum = MoreLoseBills_MX.Sum(b => b.Sum);

            conn.MoreLoseBill.Add(MoreLoseBills);
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
        public async Task<IActionResult> SHBill(int ids)
        {

            var Bills = await conn.MoreLoseBill.FirstOrDefaultAsync(b => b.id == ids);

            var Bills_MX = conn.MoreLoseBill_MX.Where(b => b.Bill_id == ids);
            if (Bills == null)
            {
                var jsons = new
                {
                    errorMsg = "审核失败,单据不存在!"
                };
                return Json(jsons);
            }
            if (Bills.Status == 1)
            {
                var jsons = new
                {
                    errorMsg = "审核失败,单据已审核!"
                };
                return Json(jsons);
            }
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            Bills.Status = 1;
            Bills.SHName = UserID;
            Bills.SHDate = now;

            foreach (var item in Bills_MX)
            {
                //处理库存
                var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.StoreRow);
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
        public async Task<IActionResult> DeleteBill(int ids)
        {
            var Bills = await conn.MoreLoseBill.FirstOrDefaultAsync(b => b.id == ids);
            if (Bills == null)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据不存在!"
                };
                return Json(jsons);
            }
            if (Bills.Status == 1)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据已审核!"
                };
                return Json(jsons);
            }
            var Bills_MX = conn.MoreLoseBill_MX.Where(b => b.Bill_id == ids);
            conn.Remove(Bills);
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
        public async Task<IActionResult> EditBill(int ids, string SHType, MoreLoseBill MoreLoseBills, List<MoreLoseBill_MX> MoreLoseBills_MX)
        {
            if (MoreLoseBills == null)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,无数据!"
                };
                return Json(jsons);
            }
            var EditBills = await conn.MoreLoseBill.FirstOrDefaultAsync(b => b.id == ids);
            var EditBills_MX = conn.MoreLoseBill_MX.Where(b => b.Bill_id == ids);
            if (EditBills == null)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,该单据不存在!"
                };
                return Json(jsons);
            }
            if (EditBills.Status == 1)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,该单据已审核不可修改!"
                };
                return Json(jsons);
            }
            //更新主表
            EditBills.BillDate = MoreLoseBills.BillDate;
            EditBills.YSName = MoreLoseBills.YSName;
            EditBills.YSNameID = MoreLoseBills.YSNameID;
            EditBills.StroeInfo_id = MoreLoseBills.StroeInfo_id;
            EditBills.StoreName = MoreLoseBills.StoreName;
            EditBills.BZ = MoreLoseBills.BZ;
            //删除原来明细
            foreach (var item in EditBills_MX)
            {
                conn.MoreLoseBill_MX.Remove(item);
            }
            EditBills.MoreLoseBill_MX = MoreLoseBills_MX;
            //对明细表进行处理
            foreach (var item in MoreLoseBills_MX)
            {
                if (item.Num == 0)
                {
                    var json = new
                    {
                        errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                    };
                    return Json(json);
                }
                item.StroeInfo_id = MoreLoseBills.StroeInfo_id;
                item.StoreName = MoreLoseBills.StoreName;
                item.Sum = item.Num * item.Price;
                //审核
                if (SHType == "yes")
                {
                    //处理库存
                    var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.StoreRow);
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
            //添加目前的明细


            EditBills.Sum = MoreLoseBills_MX.Sum(b => b.Sum);
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            if (SHType == "yes")
            {
                EditBills.Status = 1;
                EditBills.SHName = UserID;
                EditBills.SHDate = now;
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


            IQueryable<MoreLoseBill> bills = conn.MoreLoseBill.Where(b => b.BillType == "LS");

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
                            bills = bills.Where(b => b.CreateName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<MoreLoseBill>(lists, "报损单", "报损单", SysTool.GetPropertyNameArray<MoreLoseBill>()).GetBuffer();


            var fileName = "报损单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
        public async Task<IActionResult> GetBillList(int ids)
        {
            var bill = await conn.MoreLoseBill.FirstOrDefaultAsync(b => b.id == ids);
            conn.Entry(bill).Collection(p => p.MoreLoseBill_MX).Query().Load();
            var data = new
            {
                bills = bill
            };
            return Json(data);
        }

    }
}