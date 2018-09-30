using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models.CGData;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyCore.Controllers.CGGL
{
    public class OrderBillController : BaseController
    {
      
        private MyCoreContext conn;
        public OrderBillController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult OrderBillIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OrderBillList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<OrderBill> bills = conn.OrderBill;

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
        public async Task<IActionResult> OrderBill_MXList(string sidx, string sord, int page, int rows, int id)
        {
            var bills = await conn.OrderBill_MX.Where(b=>b.Bill_id==id).ToListAsync();           
            return bills.GetJson<OrderBill_MX>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<OrderBill_MX>());

        }
        public IActionResult AddOrderIndex()
        {
            return View();
        }
        public IActionResult EditOrderIndex()
        {
            return View();
        }
        public IActionResult AddRowIndex()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await conn.User.ToListAsync();
             var data = users.Select(p=>new {p.id,p.UserName });
            return Content(data.ToJson());
        }
        [HttpGet]
        public async Task<IActionResult> GetSup()
        {
            var sups = await conn.SupperInfo.ToListAsync();
            var data = sups.Select(p => new { p.id, p.SupName });
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> GoodsList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {
            IQueryable<Goodinfo> goods = conn.Goodinfo.Where(b=>b.Status==0);
            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            goods = goods.Where(b => b.PYM.Contains(StrSearch));
                            break;
                        case "1":
                            goods = goods.Where(b => b.GoodName.Contains(StrSearch));
                            break;
                        case "2":
                            goods = goods.Where(b => b.TXM.Contains(StrSearch));
                            break;

                        default:

                            break;
                    }

                }
            }
            var lisbills = await goods.ToListAsync();

            return lisbills.GetJson<Goodinfo>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<Goodinfo>());
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrderBill(string SHTypes, OrderBill OrderBiils, List<OrderBill_MX> OrderBiils_MX)
        {
            if (OrderBiils == null)
            {
                var jsons = new
                {
                    errorMsg = "保存失败,无数据!"
                };
                return Json(jsons);
            }

            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            OrderBiils.BillID = string.Concat("OR.", now.ToString("yyyyMMddHHmmsss"));
            OrderBiils.CreateName = UserID;
            OrderBiils.CreateDate = DateTime.Now;
            OrderBiils.Status =0;
            OrderBiils.SHStatus = 0;
            OrderBiils.OrderBill_MX = OrderBiils_MX;
            if (SHTypes =="yes")
            {
                OrderBiils.SHStatus = 1;
                OrderBiils.SHName = UserID;
                OrderBiils.SHDate = now;
            }

            foreach (var item in OrderBiils_MX)
                {
                   item.Status = 0;
                    if (item.Num == 0)
                    {
                        var json = new
                        {
                            errorMsg = "保存失败," + item.GoodName + " 请输入数量!"
                        };
                        return Json(json);
                    }
                }
           

            conn.OrderBill.Add(OrderBiils);
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
        public async Task<IActionResult> GetBillList(int ids)
        {
            var orderBills = await conn.OrderBill.FirstOrDefaultAsync(b=>b.id==ids);
            conn.Entry(orderBills).Collection(p => p.OrderBill_MX).Query().Load();         
            var data = new
            {
                bills = orderBills
            };
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditOrderBill(int id, OrderBill OrderBiils, List<OrderBill_MX> OrderBiils_MX)
        {
            if (OrderBiils == null)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,无数据!"
                };
                return Json(jsons);
            }
            var editBills = await conn.OrderBill.FirstOrDefaultAsync(b=>b.id==id);

            if (editBills.SHStatus == 1)
            {
                var jsons = new
                {
                    errorMsg = "修改失败,单据已审核!"
                };
                return Json(jsons);
            }

            var editBill_mx = conn.OrderBill_MX.Where(b => b.Bill_id == id);
            foreach (var item in editBill_mx)
            {
                conn.OrderBill_MX.Remove(item);
            }
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            editBills.BillDate = OrderBiils.BillDate;
            editBills.CGName = OrderBiils.CGName;
            editBills.CGNameID = OrderBiils.CGNameID;
            editBills.SupName = OrderBiils.SupName;
            editBills.Sup_id = OrderBiils.Sup_id;
            editBills.BZ = OrderBiils.BZ;
            editBills.OrderBill_MX = OrderBiils_MX;
            foreach (var item in OrderBiils_MX)
            {
                if (item.Num == 0)
                {
                    var json = new
                    {
                        errorMsg = "修改失败," + item.GoodName + " 请输入数量!"
                    };
                    return Json(json);
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
        public async Task<IActionResult> DeleteBill(int ids)
        {

            var orderBills = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == ids);
            conn.Entry(orderBills).Collection(p => p.OrderBill_MX).Query().Load();
            if (orderBills == null)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据不存在!"
                };
                return Json(jsons);
            }
            if (orderBills.SHStatus == 1)
            {
                var jsons = new
                {
                    errorMsg = "删除失败,单据已审核!"
                };
                return Json(jsons);
            }
            conn.Remove(orderBills);

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

            var orderBills = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == ids);
            if (orderBills.SHStatus == 1)
            {
                var jsons = new
                {
                    errorMsg = "审核失败,单据已审核!"
                };
                return Json(jsons);
            }
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            orderBills.SHStatus = 1;
            orderBills.SHName = UserID;
            orderBills.SHDate = now;
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

            var orderBills = await conn.OrderBill.FirstOrDefaultAsync(b => b.id == ids);
            if (orderBills.SHStatus == 0)
            {
                var jsons = new
                {
                    errorMsg = "反审核失败,单据未审核不可反审核!"
                };
                return Json(jsons);
            }
            var instores = await conn.InStoreBill.Where(b => b.OrderBill_id == ids).ToListAsync();
            if (instores.Count > 0)
            {
                var jsons = new
                {
                    errorMsg = "反审核失败,单据已生成入库单不可反审核!"
                };
                return Json(jsons);
            }
            string UserID = HttpContext.Session.GetString("UserID");
            DateTime now = DateTime.Now;
            orderBills.SHStatus = 0;
            orderBills.SHName = string.Empty;
            orderBills.SHDate = DateTime.MinValue;
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
        public async Task<IActionResult> GetFile(string StrSearchType, string StrSearch)
        {

            IQueryable<OrderBill> bills = conn.OrderBill;

           
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

            byte[] buffer = ExcelHelp.Export<OrderBill>(lists, "采购订单", "采购订单", SysTool.GetPropertyNameArray<OrderBill>()).GetBuffer();


            var fileName = "采购订单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }

    }
}