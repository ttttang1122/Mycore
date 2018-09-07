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
    public class MainController : BaseController
    {
        public IActionResult main()
        {
            var LoginName = HttpContext.Session.GetString("LoginName");
            ViewBag.LoginName = LoginName;
            return View();
        }

        public IActionResult Defalut()
        {
            return View();
        }

        private MyCoreContext conn;
        public MainController(MyCoreContext _conn)
        {
            conn = _conn;
        }

        // GET: ClientsData
        public IActionResult GetMenuData()
        {
            //var a = this.GetMenuList();
            var data =  new
            {
                UseMenuDatas = this.GetMenuList(),
            };

            return Content(data.ToJson());

        }

        public async Task< ActionResult> GetBtn(string urls)
        {
            var UserID = HttpContext.Session.GetString("UserID");


            var users = await conn.User.Where(b => b.UserID == UserID).FirstOrDefaultAsync();
            var RoleID = users.RID;
            if (RoleID != null)
            {             
                List<Menu> Btns;
                if (UserID == "US-0001")
                {
                    var Menus = await conn.Menu.Where(b => b.MenuUrl == urls).FirstOrDefaultAsync();
                    var ParentID = Menus.MenuID;
                    Btns = await conn.Menu.Where(b => b.MenuParentID == ParentID).ToListAsync();
                }
                else
                {
                  
                    var Menus = await conn.Menu.Where(b => b.MenuUrl == urls).FirstOrDefaultAsync();
                    var ParentID = Menus.MenuID;
                    var btnMenus = await conn.Menu.Where(b => b.MenuParentID == ParentID).ToListAsync();
                    Btns = btnMenus.Where(b => conn.RoleAuthorize.Where(a => a.RoleID == RoleID).Select(a => a.MenuID).Contains(b.MenuID) && b.MenuType == 2).ToList();
                }
                var data = new
                {
                    UseBtnData = Btns,
                };
                return Content(data.ToJson());
            }else
            {
                if (UserID == "US-0001")
                {
                    var Menus = await conn.Menu.Where(b => b.MenuUrl == urls).FirstOrDefaultAsync();
                    var ParentID = Menus.MenuID;
                    var Btns = await conn.Menu.Where(b => b.MenuParentID == ParentID).ToListAsync();
                    var datas = new
                    {
                        UseBtnData = Btns,                     
                    };
                    return Content(datas.ToJson());
                }
                var data = new
                {
                };
                return Content(data.ToJson());
            }
        
        }
        private async Task<object> GetMenuList()
        {
            string userID= HttpContext.Session.GetString("UserID");
          
            var  users = await conn.User.FirstOrDefaultAsync(b => b.UserID == userID);
            var RoleID = users.RID;
           
            string userName = users.UserName;
            if (userID == "US-0001")
            {
                var menus =await conn.Menu.Where(b=>b.MenuType==1).OrderBy(u => u.MenuSort).ToListAsync();
                return ToMenuJson(menus, "0");
            }
            else
            {
                if (RoleID == null)
                {
                    var json = new { };
                    return Json(json);
                }

                var Rolemenus = await conn.RoleAuthorize.Where(b => b.RoleID == RoleID).ToListAsync();
                var menus = await conn.Menu.Where(b => conn.RoleAuthorize.Where(a => a.RoleID == RoleID).Select(a => a.MenuID).Contains(b.MenuID) &&b.MenuType==1 ).OrderBy(u => u.MenuSort).ToListAsync();
                return ToMenuJson(menus, "0");
            }

        }

      
        private string ToMenuJson(List<Menu> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<Menu> entitys = data.FindAll(t => t.MenuParentID == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.MenuID) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }

     
        //public string GetMenuList(string pid)
        //{
        //    var menulist = conn.Menu.Where(s => s.MenuParentID == pid).ToList();//查出所有父级id
        //    StringBuilder sbJson = new StringBuilder();
        //    sbJson.Append("[");
        //    //string[] arry;
        //    if (menulist != null)
        //    {
        //        foreach (var item in menulist)//遍历，item只能点出属性来
        //        {
        //            //string s = "ID:" + item.ID 

        //            //+",MenuName:" + item.MenuName + "";
        //            //sbJson.Append(s);
        //            string strJson = item.ToJson();
        //            //ChildNodes子节点数组
        //            strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + GetMenuList(item.MenuID) + "");
        //            sbJson.Append(strJson + ",");
        //        }
        //        sbJson = sbJson.Remove(sbJson.Length - 1, 1);
        //    }
        //    sbJson.Append("]");
        //    return sbJson.ToString();
        //    //return JsonConvert.SerializeObject(menulist);        
        //}

    }
}