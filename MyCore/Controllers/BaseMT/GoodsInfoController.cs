using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyCore.Controllers
{
    public class GoodsInfoController : BaseController
    {
        private MyCoreContext conn;
        public GoodsInfoController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult GoodIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GoodsInfoList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<Goodinfo> goods = conn.Goodinfo.Where(b=>b.Status==0);

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            goods = goods.Where(b => b.GoodName.Contains(StrSearch));
                            break;
                        case "1":
                            goods = goods.Where(b => b.TYName.Contains(StrSearch));

                            break;
                        case "2":
                            goods = goods.Where(b => b.SCCJ.Contains(StrSearch));
                            break;
                        case "3":
                            goods = goods.Where(b => b.ForType.Contains(StrSearch));
                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await goods.ToListAsync();
            return lists.GetJson<Goodinfo>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<Goodinfo>());
        }

        public IActionResult AddIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGoods(Goodinfo good)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.Session.GetString("UserID");
                Goodinfo m = new Goodinfo();
                m.GoodID = good.GoodID;
                m.GoodName = good.GoodName;
                m.TYName = good.TYName;
                m.PYM = good.PYM;
                m.TXM = good.TXM;
                m.DW = good.DW;
                m.GGType = good.GGType;
                m.ModelType = good.ModelType;
                m.SCCJ = good.SCCJ;
                m.XKZ = good.XKZ;
                m.ClassID = good.ClassID;
                m.ForType = good.ForType;
                m.ShopPrice = good.ShopPrice;
                m.BZ = good.BZ;
                m.Status = 0;
                m.CreateDate = DateTime.Now;
                m.CreateName = UserID;
                conn.Goodinfo.Add(m);

                try
                {
                    await conn.SaveChangesAsync();
                    var json = new
                    {
                        okMsg = "添加成功！"
                    };
                    return Json(json);
                }
                catch
                {
                    var json = new
                    {
                        errorMsg = "插入数据错误,添加失败!"
                    };
                    return Json(json);
                }
            }
            else
            {
                var json = new
                {
                    errorMsg = "传输错误，添加失败！"
                };
                return Json(json);
            }

        }

        public async Task<ActionResult> GetFormList(int ids)
        {
            var good = await conn.Goodinfo.FirstOrDefaultAsync(a => a.id == ids);
            return Content(good.ToJson());
        }
        public IActionResult EditIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditGoods(Goodinfo good)
        {
            if (ModelState.IsValid)
            {
                if (good != null)
                {
                    var m = await conn.Goodinfo.FirstOrDefaultAsync(a => a.id == good.id);
                    if (m != null)
                    {
                        string UserID = HttpContext.Session.GetString("UserID");
                        m.GoodID = good.GoodID;
                        m.TYName = good.TYName;
                        m.PYM = good.PYM;
                        m.TXM = good.TXM;
                        m.DW = good.DW;
                        m.GGType = good.GGType;
                        m.ModelType = good.ModelType;
                        m.SCCJ = good.SCCJ;
                        m.XKZ = good.XKZ;
                        m.ClassID = good.ClassID;
                        m.ForType = good.ForType;
                        m.ShopPrice = good.ShopPrice;
                        m.BZ = good.BZ;
                        m.EditDate = DateTime.Now;
                        m.EditName = UserID;
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
                    catch
                    {
                        var json = new
                        {
                            errorMsg = "插入数据错误,修改失败!"
                        };
                        return Json(json);
                    }
                }
                else
                {
                    var json = new
                    {
                        errorMsg = "未知错误，修改失败"
                    };
                    return Json(json);
                }
            }
            else
            {
                var json = new
                {
                    errorMsg = "传输错误，修改失败！"
                };
                return Json(json);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGoods(int ids)
        {
            var m = await conn.Goodinfo.FirstOrDefaultAsync(u => u.id == ids);
            if (m != null)
            {
                m.Status = 1;
            }
            //2.更新对象数据                   
            try
            {
                await conn.SaveChangesAsync();
                var json = new
                {
                    okMsg = "删除成功"
                };
                return Json(json);
            }
            catch
            {
                var json = new
                {
                    errorMsg = "删除失败"
                };
                return Json(json);
            }
        }

        public async Task<IActionResult> GetFile(string StrSearchType, string StrSearch)
        {

           
            List<Goodinfo> good = await conn.Goodinfo.Where(b => b.Status == 1).ToListAsync();
            IQueryable<Goodinfo> goods = conn.Goodinfo.Where(b => b.Status == 1);

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            goods = goods.Where(b => b.GoodName.Contains(StrSearch));
                            break;
                        case "1":
                            goods = goods.Where(b => b.TYName.Contains(StrSearch));

                            break;
                        case "2":
                            goods = goods.Where(b => b.SCCJ.Contains(StrSearch));
                            break;
                        case "3":
                            goods = goods.Where(b => b.ForType.Contains(StrSearch));
                            break;
                        default:

                            break;
                    }

                }
            }

            byte[] buffer = ExcelHelp.Export<Goodinfo>(good, "商品信息", "商品信息", SysTool.GetPropertyNameArray<Goodinfo>()).GetBuffer();


            var fileName = "商品信息" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
    }
}