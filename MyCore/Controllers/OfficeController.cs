using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyCore.Controllers
{
    public class OfficeController : BaseController
    {

        MyCoreContext conn;
        public OfficeController(MyCoreContext _conn)
        {
            conn = _conn;
        }

        public IActionResult OfficeIndex()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTreeSelectJson()
        {
            var data = await conn.Office.ToListAsync();
            var treeList = new List<TreeSelectModel>();
            foreach (Office item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.OfficeID;
                treeModel.text = item.OfficeName;
                treeModel.parentId = item.ParentID;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        private void GetChilds(List<object> treelist, IList<Office> oldData, string parentID, int level = 0)
        {
            IList<Office> list = oldData.Where(n => n.ParentID == parentID).ToList();

            foreach (var item in list)
            {
                Dictionary<string, object> results = new Dictionary<string, object>();
                results.Add("level", level);
                results.Add("isLeaf", oldData.Where(n => n.ParentID == item.OfficeID).Count() > 0 ? false : true);
                results.Add("expanded", true);
                results.Add("loaded", true);
                results.Add("parent", parentID);
                results.Add("id", item.id);
                results.Add("OfficeID", item.OfficeID);
                results.Add("ParentID", item.ParentID);
                results.Add("OfficeName", item.OfficeName);
                results.Add("FZName", item.FZName);
                results.Add("BZ", item.BZ);
                results.Add("Status", item.Status);
                results.Add("CreateDate", item.CreateDate);
                results.Add("CreateName", item.CreateName);
                results.Add("EditeDate", item.EditeDate);
                results.Add("EditName", item.EditName);
                treelist.Add(results);
                this.GetChilds(treelist, oldData, item.OfficeID, level + 1);
            }
        }
        [HttpPost]
        public async Task<IActionResult> OfficeList( string StrSearch)
        {
            var Offices = await conn.Office.ToListAsync();
            var treeList = new List<object>();
            GetChilds(treeList, Offices, "0");
            return Content(treeList.ToJson());
        }

        public IActionResult AddIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice(Office offices)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.Session.GetString("UserID");
                string strOfficeName = offices.OfficeName;
                if (OnlyOfficeName(strOfficeName) == true)
                {
                    var json = new
                    {
                        errorMsg = "部门重复！,添加失败!"
                    };
                    return Json(json);
                }
                Office m = new Office();
                string cID = SysOfficeID().ToString();                
                m.OfficeID = cID;
                m.ParentID = offices.ParentID;
                m.OfficeName = offices.OfficeName;
                m.FZName = offices.FZName;
                m.Status = "正常";
                m.BZ = offices.BZ;
                m.CreateDate = DateTime.Now;
                m.CreateName = UserID;
                conn.Office.Add(m);

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

        public bool OnlyOfficeName(string OfficeName)
        {

            var data = conn.Office.Where(b => b.OfficeName == OfficeName).ToList();
            if (data.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string SysOfficeID()
        {
            string DjNO = "";
            List<string> OldNum = new List<string>();

            var offices = conn.Office.OrderByDescending(b => b.OfficeID).FirstOrDefault();
            if (offices != null)
            {
                OldNum = offices.OfficeID.ToString().Split('-').ToList<string>();
            }
            if (!(OldNum.Count > 0))
            {
                DjNO = string.Format("{0}-{1}", "OF", "0001");
            }
            else
            {
                DjNO = string.Format("{0}-{1}", "OF", (Convert.ToInt32(OldNum[OldNum.Count - 1]) + 1).ToString().PadLeft(4, '0'));

            }
            return DjNO;

        }

        public async Task<ActionResult> GetFormList(int ids)
        {
            var offices = await conn.Office.FirstOrDefaultAsync(a => a.id == ids);
            return Content(offices.ToJson());
        }
        public IActionResult EditIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditOffice(Office offices)
        {
            if (ModelState.IsValid)
            {
                if (offices != null)
                {
                    var m = await conn.Office.FirstOrDefaultAsync(a => a.OfficeID == offices.OfficeID);
                    if (m != null)
                    {
                        string UserID = HttpContext.Session.GetString("UserID");
                        m.OfficeName = offices.OfficeName;
                        m.ParentID = offices.ParentID;
                        m.FZName = offices.FZName;
                        m.BZ = offices.BZ;
                        m.EditeDate = DateTime.Now;
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
        public async Task<ActionResult> DeleteOffice(int ids)
        {
            var m = await conn.Office.FirstOrDefaultAsync(u => u.id == ids);
            if (m != null)
            {
                var OfficeID = m.OfficeID;
                var child = conn.Office.Where(b => b.ParentID == OfficeID).ToList();
                if (child.Count > 0)
                {
                    var json = new
                    {
                        errorMsg = "存在子项,删除失败!"
                    };
                    return Json(json);
                }

                conn.Office.Remove(m);
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
    }
}