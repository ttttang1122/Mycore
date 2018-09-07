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
    public class RoleController : BaseController
    {
        MyCoreContext conn;

       public RoleController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        [HttpPost]
        public async Task<IActionResult> RoleList(string sidx, string sord, int page, int rows, string StrSearch)
        {
            IQueryable<Role> ViewRole = conn.Role;
            if (!string.IsNullOrWhiteSpace(StrSearch))
            {
                ViewRole = ViewRole.Where(b => b.RoleID.Contains(StrSearch) || b.RoleName.Contains(StrSearch) || b.RoleType.Contains(StrSearch) );
            }
            var listroles = await ViewRole.ToListAsync();
            return listroles.GetJson<Role>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<Role>());
        }

        public IActionResult RoleIndex()
        {
            return View();
        }

        public IActionResult AddIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(Role roles)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.Session.GetString("UserID");

                string strRoleName = roles.RoleName;

                if (OnlyRoleName(strRoleName) == true)
                {
                    var json = new
                    {
                        errorMsg = "角色名重复！,添加失败!"
                    };
                    return Json(json);
                }
                Role m = new Role();
                string cID = SysRoleID().ToString();
                m.RoleID = cID;
                m.RoleName = roles.RoleName;
                m.RoleType = roles.RoleType;           
                m.Status = "正常";
                m.BZ = roles.BZ;
                m.CreateDate = DateTime.Now;
                m.CreateName = UserID;
                conn.Role.Add(m);

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


        public bool OnlyRoleName(string RoleName)
        {

            var data = conn.Role.Where(b => b.RoleName == RoleName).ToList();
            if (data.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string SysRoleID()
        {
            string DjNO = "";
            List<string> OldNum = new List<string>();

            var roles = conn.Role.OrderByDescending(b => b.RoleID).FirstOrDefault();
            if (roles != null)
            {
                OldNum = roles.RoleID.ToString().Split('-').ToList<string>();
            }
            if (!(OldNum.Count > 0))
            {
                DjNO = string.Format("{0}-{1}", "RL", "0001");
            }
            else
            {
                DjNO = string.Format("{0}-{1}", "RL", (Convert.ToInt32(OldNum[OldNum.Count - 1]) + 1).ToString().PadLeft(4, '0'));

            }
            return DjNO;

        }

        public async Task<ActionResult> GetFormList(int ids)
        {
            var roles = await conn.Role.FirstOrDefaultAsync(a => a.id == ids);
            return Content(roles.ToJson());
        }
        public IActionResult EditIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(Role roles)
        {
            if (ModelState.IsValid)
            {
                if (roles != null)
                {
                    var m = await conn.Role.FirstOrDefaultAsync(a => a.RoleID == roles.RoleID);
                    if (m != null)
                    {
                        string UserID = HttpContext.Session.GetString("UserID");
                        m.RoleName = roles.RoleName;
                        m.RoleType = roles.RoleType;
                        m.BZ = roles.BZ;
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
        public async Task<ActionResult> DeleteRole(int ids)
        {
            var m = await conn.Role.FirstOrDefaultAsync(u => u.id == ids);
            if (m != null)
            {
                conn.Role.Remove(m);
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

        public IActionResult AuthorizeIndex()
        {
            return View();
        }

        private RoleAuthorize roleAuthorizeApp = new RoleAuthorize();
        private Menu moduleApp = new Menu();

        public async Task< IActionResult> GetPermissionTree(int ids)
        {
            var MenuData = await conn.Menu.ToListAsync();
            var RoleAuthorizeData = new List<RoleAuthorize>();
                var roles = await conn.Role.Where(b=>b.id==ids).FirstOrDefaultAsync();
                var RoleID = roles.id;
                RoleAuthorizeData =await conn.RoleAuthorize.Where(b => b.RoleID == RoleID).ToListAsync();
            var treeList = new List<TreeViewModel>();
            foreach (Menu item in MenuData)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = MenuData.Count(t => t.MenuParentID == item.MenuID) == 0 ? false : true;
                tree.id = item.MenuID;
                tree.text = item.MenuNameCN;
                tree.value = item.MenuID;
                tree.parentId = item.MenuParentID;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = RoleAuthorizeData.Count(t => t.MenuID == item.MenuID);
                tree.hasChildren = true;
                tree.img = item.MenuIcon == "" ? "" : item.MenuIcon;
                treeList.Add(tree);
            }
           
            return Content(treeList.TreeViewJson());
        }
        [HttpPost]
        public async Task<IActionResult> RoleAuthorize(string permissionIds, int ids)
        {
            if (string.IsNullOrEmpty(permissionIds))
            {
                var json = new
                {
                    errorMsg = "传输错误！,授权失败!"
                };
                return Json(json);
            }
            else
            {
                string[] ParmID = permissionIds.Split(',');
                var MenuData = await conn.Menu.ToListAsync();
                var roles = await conn.Role.Where(b => b.id == ids).FirstOrDefaultAsync();
                var RoleID = roles.id;
                var RoleAuthorizeData = await conn.RoleAuthorize.Where(b => b.RoleID == RoleID).ToListAsync();
                foreach (var RA in RoleAuthorizeData)
                {
                    conn.RoleAuthorize.Remove(RA);
                }
                foreach (var itemId in ParmID)
                {
                    RoleAuthorize roleAuthorizeEntity = new RoleAuthorize();
                    roleAuthorizeEntity.RoleID = RoleID;
                    roleAuthorizeEntity.MenuID = itemId;                 
                    conn.RoleAuthorize.Add(roleAuthorizeEntity);
                }

                try
                {
                   await conn.SaveChangesAsync();
                   
                    var json = new
                    {
                        okMsg = "授权成功！"
                    };
                    return Json(json);
                }
                catch
                {
                    var json = new
                    {
                        errorMsg = "授权错误,修改失败!"
                    };
                    return Json(json);
                }

            }
        }

    }
}