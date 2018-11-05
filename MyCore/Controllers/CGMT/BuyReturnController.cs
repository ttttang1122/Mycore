using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models.CGData;
using MyCore.Models.Store;
using MyCore.Models.Search;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyCore.Controllers.CGMT
{
    public class BuyReturnController : BaseController
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
        public async Task<IActionResult> InStoreList(string sidx, string sord, int page, int rows, Search_CGBill Search)
        {

    
            Expression<Func<InStoreBill, bool>> predicate = ExpressionBuilder.True<InStoreBill>();
            predicate = predicate.And(b => b.BillType == "BR");
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
            if (!string.IsNullOrWhiteSpace(Search.SupName))
            {
                predicate = predicate.And(b => b.SupName.Contains(Search.SupName));
            }
            if (!string.IsNullOrWhiteSpace(Search.JSName))
            {
                predicate = predicate.And(b => b.YSName.Contains(Search.JSName));
            }
            if (!string.IsNullOrWhiteSpace(Search.StoreName))
            {
                predicate = predicate.And(b => b.StoreName.Contains(Search.StoreName));
            }

            IQueryable<InStoreBill> bills = conn.InStoreBill.Where(predicate);
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
            if (InStoreBills == null)
            {
                var jsons = new
                {
                    errorMsg = "审核失败,单据不存在!"
                };
                return Json(jsons);
            }
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

            foreach (var item in InStoreBills_MX)
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
            if (InStoreBills == null)
            {
                var jsons = new
                {
                    errorMsg = "反审失败,单据不存在!"
                };
                return Json(jsons);
            }
            if (InStoreBills.SHStatus == 0)
            {
                var jsons = new
                {
                    errorMsg = "反审失败,单据未审核不可反审核!"
                };
                return Json(jsons);
            }


            foreach (var item in InStoreBills_MX)
            {
                //处理库存
                var stores = await conn.GoodsStore.FirstOrDefaultAsync(b => b.id == item.OrderRow);
                if (stores != null)
                {
                    stores.Num = stores.Num + item.Num;                  
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
            if (EditInStoreBills == null)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,该单据不存在!"
                };
                return Json(jsons);
            }
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
            EditInStoreBills.Sup_id = InStoreBills.Sup_id;
            EditInStoreBills.SupName = InStoreBills.SupName;
            EditInStoreBills.BZ = InStoreBills.BZ;
            //删除原来明细
            foreach (var item in EditInStoreBills_MX)
            {
                conn.InStoreBill_MX.Remove(item);
            }
            EditInStoreBills.InStoreBill_MX = InStoreBills_MX;
            //对明细表进行处理
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
            //添加目前的明细
          

            EditInStoreBills.Sum = InStoreBills_MX.Sum(b => b.Sum);
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            if (SHType == "yes")
            {
                EditInStoreBills.SHStatus = 1;
                EditInStoreBills.SHName = UserID;
                EditInStoreBills.SHDate = now;
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
            var Search = SysTool.JsonToModel<Search_CGBill>(JsonSearch);
            Expression<Func<InStoreBill, bool>> predicate = ExpressionBuilder.True<InStoreBill>();
            predicate = predicate.And(b => b.BillType == "BR");
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
            if (!string.IsNullOrWhiteSpace(Search.SupName))
            {
                predicate = predicate.And(b => b.SupName.Contains(Search.SupName));
            }
            if (!string.IsNullOrWhiteSpace(Search.JSName))
            {
                predicate = predicate.And(b => b.YSName.Contains(Search.JSName));
            }
            if (!string.IsNullOrWhiteSpace(Search.StoreName))
            {
                predicate = predicate.And(b => b.StoreName.Contains(Search.StoreName));
            }

            IQueryable<InStoreBill> bills = conn.InStoreBill.Where(predicate);

            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<InStoreBill>(lists, "采购退库单", "采购退库单", SysTool.GetPropertyNameArray<InStoreBill>()).GetBuffer();


            var fileName = "采购退库单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

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