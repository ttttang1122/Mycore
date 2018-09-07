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
    public class StoreInfoController : BaseController
    {
        private MyCoreContext conn;
        public StoreInfoController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult StoreIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StoreInfoList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<StoreInfo> sto = conn.StoreInfo.Where(b => b.Status == 1);

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            sto = sto.Where(b => b.StoreName.Contains(StrSearch));
                            break;
                    

                        default:

                            break;
                    }

                }
            }

            var lists = await sto.ToListAsync();
            return lists.GetJson<StoreInfo>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<StoreInfo>());
        }

        public IActionResult AddIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStore(StoreInfo sto)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.Session.GetString("UserID");
                StoreInfo m = new StoreInfo();
                m.StoreName = sto.StoreName;
                m.Address = sto.Address;
                m.Sizes = sto.Sizes;
                m.UseSay = sto.UseSay;
                m.BZ = sto.BZ;
                m.Status = 1;
                m.CreateDate = DateTime.Now;
                m.CreateName = UserID;
                conn.StoreInfo.Add(m);

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
            var sto = await conn.StoreInfo.FirstOrDefaultAsync(a => a.id == ids);
            return Content(sto.ToJson());
        }
        public IActionResult EditIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(StoreInfo sto)
        {
            if (ModelState.IsValid)
            {
                if (sto != null)
                {
                    var m = await conn.StoreInfo.FirstOrDefaultAsync(a => a.id == sto.id);
                    if (m != null)
                    {
                        string UserID = HttpContext.Session.GetString("UserID");
                        m.StoreName = sto.StoreName;
                        m.Address = sto.Address;
                        m.Sizes = sto.Sizes;
                        m.UseSay = sto.UseSay;
                        m.BZ = sto.BZ;
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
        public async Task<IActionResult> DeleteStore(int ids)
        {
            var m = await conn.StoreInfo.FirstOrDefaultAsync(u => u.id == ids);
            if (m != null)
            {
                m.Status = 2;
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

           var sto = await conn.StoreInfo.Where(b => b.Status == 1).ToListAsync();

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            sto = sto.Where(b => b.StoreName.Contains(StrSearch)).ToList();
                            break;


                        default:

                            break;
                    }

                }
            }

            byte[] buffer = ExcelHelp.Export<StoreInfo>(sto, "仓库信息", "仓库信息", SysTool.GetPropertyNameArray<StoreInfo>()).GetBuffer();


            var fileName = "仓库信息" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
    }
}