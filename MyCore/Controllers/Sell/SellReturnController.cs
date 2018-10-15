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

namespace MyCore.Controllers.Sell
{
    public class SellReturnController : Controller
    {
        private MyCoreContext conn;

        public SellReturnController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult SellReturnIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<SellBill> bills = conn.SellBill.Where(b => b.BillType == "SR");

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
                            bills = bills.Where(b => b.SellName.Contains(StrSearch));

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
            return lists.GetJson<SellBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<SellBill>());
        }

        [HttpPost]
        public async Task<IActionResult> SellBill_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.SellBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            return bills.GetJson<SellBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<SellBill_MX>());

        }

        [HttpPost]
        public async Task<IActionResult> SellChoose(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<SellBill> bills = conn.SellBill.Where(b => b.BillType == "SE");

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
                            bills = bills.Where(b => b.SellName.Contains(StrSearch));

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
            return lists.GetJson<SellBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<SellBill>());
        }

        [HttpPost]
        public async Task<IActionResult> SellChoose_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.SellBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            return bills.GetJson<SellBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<SellBill_MX>());

        }


        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await conn.User.ToListAsync();
            var data = users.Select(p => new { p.id, p.UserName });
            return Content(data.ToJson());
        }
        [HttpGet]
        public async Task<IActionResult> GetSup()
        {
            var sups = await conn.SupperInfo.Where(b => b.SupType == 0 || b.SupType == 2 && b.Status == 1).ToListAsync();
            var data = sups.Select(p => new { p.id, p.SupName });
            return Content(data.ToJson());
        }
        [HttpGet]
        public async Task<IActionResult> GetStoreInfo()
        {
            var stores = await conn.StoreInfo.ToListAsync();
            var data = stores.Select(p => new { p.id, p.StoreName });
            return Content(data.ToJson());
        }

        public IActionResult AddEditSellReturnIndex()
        {
            return View();
        }

        public IActionResult AddRowIndex()
        {
            return View();
        }

        public IActionResult ChooseIndex()
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

            List<StoreAll> sells = bills.Select(b => new StoreAll { id = b.id, Store_id = b.Store_id, StoreName = b.StoreName, Good_id = b.Good_id, GoodID = b.GoodID, GoodName = b.GoodName, DW = b.DW, GGType = b.GGType, ModelType = b.ModelType, SCCJ = b.SCCJ, InPrice = b.Price, Num = b.Num, SCPH = b.SCPH, MJPH = b.MJPH, scDate = b.scDate, yxqDate = b.yxqDate, BZ = b.BZ }).ToList();
            var goods = conn.Goodinfo;
            sells.ForEach(x => { x.SellPrice = goods.FirstOrDefault(b => b.id == x.Good_id).ShopPrice; });

            return sells.GetJson<StoreAll>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<StoreAll>());
        }

        [HttpPost]
        public async Task<IActionResult> SaveBill(string SHType, SellBill SellBills, List<SellBill_MX> SellBills_MX)
        {
            if (SellBills == null)
            {
                var jsons = new
                {
                    errorMsg = "保存失败,无数据!"
                };
                return Json(jsons);
            }

            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            SellBills.BillID = string.Concat("SR.", now.ToString("yyyyMMddHHmmsss"));
            SellBills.BillType = "SR";
            SellBills.CreateName = UserID;
            SellBills.CreateDate = DateTime.Now;
            SellBills.Status = 0;
            SellBills.SellBill_MX = SellBills_MX;
            if (SHType == "yes")
            {
                SellBills.Status = 1;
                SellBills.SHName = UserID;
                SellBills.SHDate = now;
            }
            foreach (var item in SellBills_MX)
            {
                if (item.Num == 0)
                {
                    var json = new
                    {
                        errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                    };
                    return Json(json);
                }
                item.StroeInfo_id = SellBills.StroeInfo_id;
                item.StoreName = SellBills.StoreName;
                item.Sum = item.Num * item.SellPrice;
                //审核
                if (SHType == "yes")
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
                            errorMsg = "保存失败," + item.GoodName + " 未找到库存!"
                        };
                        return Json(json);
                    }



                }



            }
            SellBills.Sum = SellBills_MX.Sum(b => b.Sum);

            conn.SellBill.Add(SellBills);
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

            var SellBills = await conn.SellBill.FirstOrDefaultAsync(b => b.id == ids);

            var SellBills_MX = conn.SellBill_MX.Where(b => b.Bill_id == ids);
            if (SellBills == null)
            {
                var jsons = new
                {
                    errorMsg = "审核失败,单据不存在!"
                };
                return Json(jsons);
            }
            if (SellBills.Status == 1)
            {
                var jsons = new
                {
                    errorMsg = "审核失败,单据已审核!"
                };
                return Json(jsons);
            }
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            SellBills.Status = 1;
            SellBills.SHName = UserID;
            SellBills.SHDate = now;

            foreach (var item in SellBills_MX)
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
            var SellBills = await conn.SellBill.FirstOrDefaultAsync(b => b.id == ids);
            if (SellBills == null)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据不存在!"
                };
                return Json(jsons);
            }
            if (SellBills.Status == 1)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据已审核!"
                };
                return Json(jsons);
            }
            var SellBills_MX = conn.SellBill_MX.Where(b => b.Bill_id == ids);
            conn.Remove(SellBills);
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
        public async Task<IActionResult> EditBill(int ids, string SHType, SellBill SellBills, List<SellBill_MX> SellBills_MX)
        {
            if (SellBills == null)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,无数据!"
                };
                return Json(jsons);
            }
            var EditSellBills = await conn.SellBill.FirstOrDefaultAsync(b => b.id == ids);
            var EditSellBills_MX = conn.SellBill_MX.Where(b => b.Bill_id == ids);
            if (EditSellBills == null)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,该单据不存在!"
                };
                return Json(jsons);
            }
            if (EditSellBills.Status == 1)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,该单据已审核不可修改!"
                };
                return Json(jsons);
            }
            //更新主表
            EditSellBills.BillDate = SellBills.BillDate;
            EditSellBills.SellName = SellBills.SellName;
            EditSellBills.SellNameID = SellBills.SellNameID;
            EditSellBills.StroeInfo_id = SellBills.StroeInfo_id;
            EditSellBills.StoreName = SellBills.StoreName;
            EditSellBills.GiveSum = SellBills.GiveSum;
            EditSellBills.Sup_id = SellBills.Sup_id;
            EditSellBills.SupName = SellBills.SupName;
            EditSellBills.BZ = SellBills.BZ;
            //删除原来明细
            foreach (var item in EditSellBills_MX)
            {
                conn.SellBill_MX.Remove(item);
            }
            EditSellBills.SellBill_MX = SellBills_MX;
            //对明细表进行处理
            foreach (var item in SellBills_MX)
            {
                if (item.Num == 0)
                {
                    var json = new
                    {
                        errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                    };
                    return Json(json);
                }
                item.StroeInfo_id = SellBills.StroeInfo_id;
                item.StoreName = SellBills.StoreName;
                item.Sum = item.Num * item.SellPrice;
                //审核
                if (SHType == "yes")
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
                            errorMsg = "保存失败," + item.GoodName + " 未找到库存!"
                        };
                        return Json(json);
                    }



                }



            }
            //添加目前的明细


            EditSellBills.Sum = SellBills_MX.Sum(b => b.Sum);
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            if (SHType == "yes")
            {
                EditSellBills.Status = 1;
                EditSellBills.SHName = UserID;
                EditSellBills.SHDate = now;
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

            IQueryable<SellBill> bills = conn.SellBill.Where(b => b.BillType == "SE");

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
                            bills = bills.Where(b => b.SellName.Contains(StrSearch));

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

            byte[] buffer = ExcelHelp.Export<SellBill>(lists, "销售退回单", "销售退回单", SysTool.GetPropertyNameArray<SellBill>()).GetBuffer();


            var fileName = "销售退回单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
        public async Task<IActionResult> GetBillList(int ids)
        {
            var bill = await conn.SellBill.FirstOrDefaultAsync(b => b.id == ids);
            conn.Entry(bill).Collection(p => p.SellBill_MX).Query().Load();
            var data = new
            {
                bills = bill
            };
            return Json(data);
        }





    }
}