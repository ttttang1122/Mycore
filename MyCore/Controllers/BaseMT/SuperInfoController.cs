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
    public class SuperInfoController : BaseController
    {
        private MyCoreContext conn;
        public SuperInfoController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult SupIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SupInfoList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<SupperInfo> sups = conn.SupperInfo.Where(b => b.Status == 1);

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            sups = sups.Where(b => b.SupName.Contains(StrSearch));
                            break;
                        case "1":
                            sups = sups.Where(b => b.dq.Contains(StrSearch));

                            break;
                      
                        default:

                            break;
                    }

                }
            }

            var lists = await sups.ToListAsync();
            return lists.GetJson<SupperInfo>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<SupperInfo>());
        }

        public IActionResult AddIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSup(SupperInfo sups)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.Session.GetString("UserID");
                SupperInfo m = new SupperInfo();
                m.SupID = sups.SupID;
                m.SupName = sups.SupName;
                m.PYM = sups.PYM;
                m.Address = sups.Address;
                m.FZName = sups.FZName;
                m.Phone = sups.Phone;
                m.WeiXin = sups.WeiXin;
                m.dq = sups.dq;
                m.SupType = sups.SupType;              
                m.BZ = sups.BZ;
                m.Status = 1;
                m.CreateDate = DateTime.Now;
                m.CreateName = UserID;
                conn.SupperInfo.Add(m);

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
            var sups = await conn.SupperInfo.FirstOrDefaultAsync(a => a.id == ids);
            return Content(sups.ToJson());
        }
        public IActionResult EditIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditSup(SupperInfo sups)
        {
            if (ModelState.IsValid)
            {
                if (sups != null)
                {
                    var m = await conn.SupperInfo.FirstOrDefaultAsync(a => a.id == sups.id);
                    if (m != null)
                    {
                        string UserID = HttpContext.Session.GetString("UserID");
                        m.SupID = sups.SupID;
                        m.SupName = sups.SupName;
                        m.PYM = sups.PYM;
                        m.Address = sups.Address;
                        m.FZName = sups.FZName;
                        m.Phone = sups.Phone;
                        m.WeiXin = sups.WeiXin;
                        m.dq = sups.dq;
                        m.SupType = sups.SupType;
                        m.BZ = sups.BZ;
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
        public async Task<IActionResult> DeleteSup(int ids)
        {
            var m = await conn.SupperInfo.FirstOrDefaultAsync(u => u.id == ids);
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


            var sups = await conn.SupperInfo.Where(b => b.Status == 1).ToListAsync(); ;

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            sups = sups.Where(b => b.SupName.Contains(StrSearch)).ToList();
                            break;
                        case "1":
                            sups = sups.Where(b => b.dq.Contains(StrSearch)).ToList();

                            break;

                        default:

                            break;
                    }

                }
            }

            byte[] buffer = ExcelHelp.Export<SupperInfo>(sups, "客商信息", "客商信息", SysTool.GetPropertyNameArray<SupperInfo>()).GetBuffer();


            var fileName = "客商信息" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
    }
}