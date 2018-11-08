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
    public class GoodsMoreController : BaseController
    {
        private MyCoreContext conn;

        public GoodsMoreController(MyCoreContext _conn)
        {
            conn = _conn;
        }

        public IActionResult GoodsMoreIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GoodsMoreList(string sidx, string sord, int page, int rows, BillSearch Search)
        {

            Expression<Func<MoreLoseBill, bool>> predicate = ExpressionBuilder.True<MoreLoseBill>();
            predicate = predicate.And(b => b.BillType == "MR");
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

            var lists = await bills.ToListAsync();
            return lists.GetJson<MoreLoseBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<MoreLoseBill>());
        }

        [HttpPost]
        public async Task<IActionResult> GoodsMore_MXList(string sidx, string sord, int page, int rows, int id)
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

        public IActionResult AddEditGoodsMoreIndex()
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
        public async Task<IActionResult> SaveBill(string SHType, MoreLoseBill Bills, List<MoreLoseBill_MX> Bills_MX)
        {
            if (Bills == null)
            {
                var jsons = new
                {
                    errorMsg = "保存失败,无数据!"
                };
                return Json(jsons);
            }

            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            Bills.BillID = string.Concat("MR.", now.ToString("yyyyMMddHHmmsss"));
            Bills.BillType = "MR";
            Bills.CreateName = UserID;
            Bills.CreateDate = DateTime.Now;
            Bills.Status = 0;
            Bills.MoreLoseBill_MX = Bills_MX;
            if (SHType == "yes")
            {
                Bills.Status = 1;
                Bills.SHName = UserID;
                Bills.SHDate = now;
            }
            foreach (var item in Bills_MX)
            {
                if (item.Num == 0)
                {
                    var json = new
                    {
                        errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                    };
                    return Json(json);
                }
                item.StroeInfo_id = Bills.StroeInfo_id;
                item.StoreName = Bills.StoreName;
                item.Sum = item.Num * item.Price;
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
            Bills.Sum = Bills_MX.Sum(b => b.Sum);

            conn.MoreLoseBill.Add(Bills);
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
        public async Task<IActionResult> EditBill(int ids, string SHType, MoreLoseBill Bills, List<MoreLoseBill_MX> Bills_MX)
        {
            if (Bills == null)
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
            EditBills.BillDate = Bills.BillDate;
            EditBills.YSName = Bills.YSName;
            EditBills.YSNameID = Bills.YSNameID;
            EditBills.StroeInfo_id = Bills.StroeInfo_id;
            EditBills.StoreName = Bills.StoreName;
            EditBills.BZ = Bills.BZ;
            //删除原来明细
            foreach (var item in EditBills_MX)
            {
                conn.MoreLoseBill_MX.Remove(item);
            }
            EditBills.MoreLoseBill_MX = Bills_MX;
            //对明细表进行处理
            foreach (var item in Bills_MX)
            {
                if (item.Num == 0)
                {
                    var json = new
                    {
                        errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                    };
                    return Json(json);
                }
                item.StroeInfo_id = Bills.StroeInfo_id;
                item.StoreName = Bills.StoreName;
                item.Sum = item.Num * item.Price;
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


            EditBills.Sum = Bills_MX.Sum(b => b.Sum);
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
        public async Task<IActionResult> GetFile(string JsonSearch)
        {
            //json字符串转模型
            var Search = SysTool.JsonToModel<BillSearch>(JsonSearch);

            Expression<Func<MoreLoseBill, bool>> predicate = ExpressionBuilder.True<MoreLoseBill>();
            predicate = predicate.And(b => b.BillType == "MR");
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
            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<MoreLoseBill>(lists, "报溢单", "报溢单", SysTool.GetPropertyNameArray<MoreLoseBill>()).GetBuffer();


            var fileName = "报溢单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

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