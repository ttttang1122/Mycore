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
    public class MenuController : BaseController
    {
        MyCoreContext conn;
        public MenuController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        [HttpGet]
        public async Task< IActionResult> GetTreeSelectJson()
        {
            var data = await conn.Menu.Where(b=>b.MenuType==1).OrderBy(u => u.MenuSort).ToListAsync();
            var treeList = new List<TreeSelectModel>();
            foreach (Menu item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.MenuID;
                treeModel.text = item.MenuNameCN;
                treeModel.parentId = item.MenuParentID;
                treeModel.data = item;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        private void GetChilds(List<object> treelist, IList<Menu> oldData, string parentID, int level = 0)
        {
            IList<Menu> list = oldData.Where(n => n.MenuParentID == parentID).ToList();

            foreach (var item in list)
            {
                Dictionary<string, object> results = new Dictionary<string, object>();
                results.Add("level", level);
                results.Add("isLeaf", oldData.Where(n => n.MenuParentID == item.MenuID).Count() > 0 ? false : true);
                results.Add("expanded", true);
                results.Add("loaded", true);
                results.Add("parent", parentID);
                results.Add("id", item.id);
                results.Add("MenuID", item.MenuID);
                results.Add("MenuParentID", item.MenuParentID);
                results.Add("MenuName", item.MenuName);              
                results.Add("MenuNameCN", item.MenuNameCN);
                results.Add("MenuType", item.MenuType);
                results.Add("MenuUrl", item.MenuUrl);
                results.Add("MenuSort", item.MenuSort);
                results.Add("MenuIcon", item.MenuIcon);
                results.Add("BZ", item.BZ);
                results.Add("CreateDate", item.CreateDate);
                results.Add("CreateName", item.CreateName);

                treelist.Add(results);
                this.GetChilds(treelist, oldData, item.MenuID, level + 1);
            }
        }
        public IActionResult MenuIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> MenuList()
        {

            var menus = await conn.Menu.OrderBy(u => u.MenuSort).ToListAsync();
            var treeList = new List<object>();
            GetChilds(treeList, menus, "0");
            return Content(treeList.ToJson());
        }

        public IActionResult AddIndex(string MenuID)
        {
            ViewBag.MenuID = MenuID;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMenu(Menu menus)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.Session.GetString("UserID");
                Menu m = new Menu();
                string cID = SysMenuID();
                m.MenuID = cID;
                m.MenuParentID = menus.MenuParentID;
                m.MenuName = menus.MenuName;
                m.MenuNameCN = menus.MenuNameCN;
                m.MenuType = menus.MenuType;
                m.MenuUrl = menus.MenuUrl;
                m.MenuSort = menus.MenuSort;
                m.MenuIcon = menus.MenuIcon;
                m.BZ = menus.BZ;
                m.CreateDate = DateTime.Now;
                m.CreateName = UserID;
                conn.Menu.Add(m);

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
        public string SysMenuID()
        {
            string DjNO = "";
            List<string> OldNum = new List<string>();

            var menus = conn.Menu.OrderByDescending(b => b.MenuID).FirstOrDefault();
            if (menus != null)
            {
                OldNum = menus.MenuID.ToString().Split('-').ToList<string>();
            }
            if (!(OldNum.Count > 0))
            {
                DjNO = string.Format("{0}-{1}", "ME", "0001");
            }
            else
            {
                DjNO = string.Format("{0}-{1}", "ME", (Convert.ToInt32(OldNum[OldNum.Count - 1]) + 1).ToString().PadLeft(4, '0'));

            }
            return DjNO;

        }
        [HttpPost]
        public async Task<IActionResult> EditMenu(Menu menus)
        {
            if (ModelState.IsValid)
            {
                if (menus != null)
                {

                    var m = conn.Menu.FirstOrDefault(a => a.MenuID == menus.MenuID);
                    if (m != null)
                    {
                        m.MenuParentID = menus.MenuParentID;
                        m.MenuName = menus.MenuName;
                        m.MenuNameCN= menus.MenuNameCN;
                        m.MenuUrl = menus.MenuUrl;
                        m.MenuSort = menus.MenuSort;
                        m.MenuIcon = menus.MenuIcon;
                        m.BZ = menus.BZ;
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
        public IActionResult EditIndex()
        {
            return View();
        }
        public async Task<IActionResult> GetFormList(int ids)
        {
            var roles = await conn.Menu.FirstOrDefaultAsync(a => a.id == ids);
            return Content(roles.ToJson());
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMenu(int ids)
        {
            var m = await conn.Menu.FirstOrDefaultAsync(u => u.id == ids);
            if (m != null)
            {
                var MenuID = m.MenuID;
                var child = conn.Menu.Where(b => b.MenuParentID == MenuID).ToList();
                if (child.Count > 0)
                {
                    var json = new
                    {
                        errorMsg = "存在子项,删除失败!"
                    };
                    return Json(json);
                }


                conn.Menu.Remove(m);
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