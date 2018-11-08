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
    public class StoreMoveController : BaseController
    {
        //连接数据库字符
        MyCoreContext conn;
        //依赖
        public StoreMoveController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        //仓库调拨猎豹视图
        public IActionResult StoreMoveIndex()
        {
            return View();
        }
        //仓库调拨主表数据
        [HttpPost]
        public async Task<IActionResult> StoreMoveList(string sidx, string sord, int page, int rows, BillSearch Search)
        {

            Expression<Func<StoreMoveBill, bool>> predicate = ExpressionBuilder.True<StoreMoveBill>();
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

            IQueryable<StoreMoveBill> bills = conn.StoreMoveBill.Where(predicate);

            var lists = await bills.ToListAsync();
            return lists.GetJson<StoreMoveBill>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<StoreMoveBill>());
        }
        //仓库调拨明细表数据
        [HttpPost]
        public async Task<IActionResult> StoreMove_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.StoreMoveBill_MX.Where(b => b.Bill_id == id).ToListAsync();
            return bills.GetJson<StoreMoveBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<StoreMoveBill_MX>());

        }
        //取得经手人信息
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await conn.User.ToListAsync();
            var data = users.Select(p => new { p.id, p.UserName });
            return Content(data.ToJson());
        }
        //取得仓库信息
        [HttpGet]
        public async Task<IActionResult> GetStoreInfo()
        {
            var stores = await conn.StoreInfo.ToListAsync();
            var data = stores.Select(p => new { p.id, p.StoreName });
            return Content(data.ToJson());
        }
        //新增修改调拨单的视图
        public IActionResult AddEditStoreMoveIndex()
        {
            return View();
        }
        //取得仓库信息表的视图
        public IActionResult AddRowIndex()
        {
            return View();
        }
        //仓库信息表数据
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
        //取得仓库调拨主分表
        public async Task<IActionResult> GetBillList(int ids)
        {
            var Bills = await conn.StoreMoveBill.FirstOrDefaultAsync(b => b.id == ids);
            conn.Entry(Bills).Collection(p => p.StoreMoveBill_MX).Query().Load();
            var data = new
            {
                bills = Bills
            };
            return Json(data);
        }
        //保存仓库调拨
        [HttpPost]
        public async Task<IActionResult> SaveBill(string SHType, StoreMoveBill Bills, List<StoreMoveBill_MX> Bills_MX)
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
            Bills.BillID = string.Concat("MV.", now.ToString("yyyyMMddHHmmsss"));
            Bills.CreateName = UserID;
            Bills.CreateDate = DateTime.Now;
            Bills.Status = 0;
            Bills.StoreMoveBill_MX = Bills_MX;
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
                item.OutStroeInfo_id = Bills.OutStroeInfo_id;
                item.OutStoreName = Bills.OutStoreName;
                item.InStroeInfo_id = Bills.InStroeInfo_id;
                item.InStoreName = Bills.InStoreName;
                //审核
                if (SHType == "yes")
                {
                    //处理原库存
                    var Outstores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.StoreRow);
                    var instores = await conn.GoodsStore.FirstOrDefaultAsync(b=>b.Good_id==item.Good_id&&b.Price==item.Price&&b.SCPH==item.SCPH&&b.MJPH==item.MJPH&&b.scDate==item.scDate&&b.yxqDate==b.yxqDate&&b.Store_id==item.InStroeInfo_id);
                    if (Outstores != null)
                    {

                        Outstores.Num = Outstores.Num - item.Num;
                        //判断仓库库存不足
                        if (Outstores.Num < 0)
                        {
                            var json = new
                            {
                                errorMsg = "保存失败," + item.GoodName + " 库存不足!"
                            };
                            return Json(json);
                        }
                        //调入仓库判断是否存在 存在就库存数量加起来不存在就创建新的一行库存数据
                        if (instores != null)
                        {
                            instores.Num=instores.Num + item.Num;
                        }else
                        {
                           
                            var store = new GoodsStore();
                            store.Store_id =SysTool.ObjToInt(item.InStroeInfo_id);
                            store.StoreName = item.InStoreName;
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
         

            conn.StoreMoveBill.Add(Bills);
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
        //审核单据
        [HttpPost]
        public async Task<IActionResult> SHBill(int ids)
        {

            var Bills = await conn.StoreMoveBill.FirstOrDefaultAsync(b => b.id == ids);

            var Bills_MX = conn.StoreMoveBill_MX.Where(b => b.Bill_id == ids);
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
                    //处理原库存
                    var Outstores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.StoreRow);
                    var instores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.Good_id == item.Good_id && b.Price == item.Price && b.SCPH == item.SCPH && b.MJPH == item.MJPH && b.scDate == item.scDate && b.yxqDate == b.yxqDate && b.Store_id == item.InStroeInfo_id);
                    if (Outstores != null)
                    {

                        Outstores.Num = Outstores.Num - item.Num;
                        //判断仓库库存不足
                        if (Outstores.Num < 0)
                        {
                            var json = new
                            {
                                errorMsg = "修改失败," + item.GoodName + " 库存不足!"
                            };
                            return Json(json);
                        }
                        //调入仓库判断是否存在 存在就库存数量加起来不存在就创建新的一行库存数据
                        if (instores != null)
                        {
                            instores.Num = instores.Num + item.Num;
                        }
                        else
                        {

                            var store = new GoodsStore();
                            store.Store_id = SysTool.ObjToInt(item.InStroeInfo_id);
                            store.StoreName = item.InStoreName;
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
                    else
                    {
                        var json = new
                        {
                            errorMsg = "修改失败," + item.GoodName + " 未找到库存!"
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
            var Bills = await conn.StoreMoveBill.FirstOrDefaultAsync(b => b.id == ids);
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
            var Bills_MX = conn.StoreMoveBill_MX.Where(b => b.Bill_id == ids);
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
        public async Task<IActionResult> EditBill(int ids, string SHType, StoreMoveBill Bills, List<StoreMoveBill_MX> Bills_MX)
        {
            if (Bills == null)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,无数据!"
                };
                return Json(jsons);
            }
            //取得修改前的主分表
            var EditBills = await conn.StoreMoveBill.FirstOrDefaultAsync(b => b.id == ids);
            var EditBills_MX = conn.StoreMoveBill_MX.Where(b => b.Bill_id == ids);
            //判断这张表是否可修改
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
            EditBills.InStroeInfo_id = Bills.InStroeInfo_id;
            EditBills.InStoreName = Bills.InStoreName;
            EditBills.OutStroeInfo_id = Bills.OutStroeInfo_id;
            EditBills.OutStoreName = Bills.OutStoreName;
            EditBills.BZ = Bills.BZ;
            //删除原来明细
            foreach (var item in EditBills_MX)
            {
                conn.StoreMoveBill_MX.Remove(item);
            }
            EditBills.StoreMoveBill_MX = Bills_MX;
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
                item.OutStroeInfo_id = Bills.OutStroeInfo_id;
                item.OutStoreName = Bills.OutStoreName;
                item.InStroeInfo_id = Bills.InStroeInfo_id;
                item.InStoreName = Bills.InStoreName;
                //审核
               
                if (SHType == "yes")
                {
                    //处理原库存
                    var Outstores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.StoreRow);
                    var instores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.Good_id == item.Good_id && b.Price == item.Price && b.SCPH == item.SCPH && b.MJPH == item.MJPH && b.scDate == item.scDate && b.yxqDate == b.yxqDate && b.Store_id == item.InStroeInfo_id);
                    if (Outstores != null)
                    {

                        Outstores.Num = Outstores.Num - item.Num;
                        //判断仓库库存不足
                        if (Outstores.Num < 0)
                        {
                            var json = new
                            {
                                errorMsg = "修改失败," + item.GoodName + " 库存不足!"
                            };
                            return Json(json);
                        }
                        //调入仓库判断是否存在 存在就库存数量加起来不存在就创建新的一行库存数据
                        if (instores != null)
                        {
                            instores.Num = instores.Num + item.Num;
                        }
                        else
                        {

                            var store = new GoodsStore();
                            store.Store_id = SysTool.ObjToInt(item.InStroeInfo_id);
                            store.StoreName = item.InStoreName;
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
                    else
                    {
                        var json = new
                        {
                            errorMsg = "修改失败," + item.GoodName + " 未找到库存!"
                        };
                        return Json(json);
                    }



                }


            }
            //添加目前的明细


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

            Expression<Func<StoreMoveBill, bool>> predicate = ExpressionBuilder.True<StoreMoveBill>();
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

            IQueryable<StoreMoveBill> bills = conn.StoreMoveBill.Where(predicate);

            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<StoreMoveBill>(lists, "仓库调拨单", "仓库调拨单", SysTool.GetPropertyNameArray<StoreMoveBill>()).GetBuffer();


            var fileName = "仓库调拨单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }

    
    }
}