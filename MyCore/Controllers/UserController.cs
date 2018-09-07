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
    public class UserController : BaseController
    {
        public IActionResult UserIndex()
        {
            return View();
        }
        private MyCoreContext conn ;
        public UserController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        [HttpPost]
        public async Task<IActionResult> UserList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {


            IQueryable<User> Users = conn.User;
            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            Users = Users.Where(b => b.UserName.Contains(StrSearch));
                            break;
                        case "1":
                            Users = Users.Where(b => b.LoginName.Contains(StrSearch));

                            break;
                        case "2":
                            Users = Users.Where(b => b.Office.OfficeName.Contains(StrSearch));
                            break;
                        case "3":
                            Users = Users.Where(b => b.Phone.Contains(StrSearch));
                            break;
                        default:

                            break;
                    }

                }
            }
            List<View_User>Viewusers = await Users.Select(p=>new View_User{id=p.id, UserID=p.UserID, UserName=p.UserName, LoginName=p.LoginName, LoginPWD=p.LoginPWD, RID=p.RID, OID=p.OID, Sex=p.Sex, Phone=p.Phone, BZ=p.BZ, Status=p.Status, CreateDate=p.CreateDate, CreateName=p.CreateName, EditDate=p.EditDate, EditName=p.EditName, RoleName=p.Role.RoleName, RoleType=p.Role.RoleType, OfficeName=p.Office.OfficeName, FZName=p.Office.FZName }).ToListAsync();
           
            return Viewusers.GetJson<View_User>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<View_User>());
        }

        public IActionResult AddIndex()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetRole()
        {
            var data = await conn.Role.ToListAsync();
            return Content(data.ToJson());
        }
        [HttpGet]
        public async Task<IActionResult> GetOffice()
        {
            var data = await conn.Office.ToListAsync();
            return Content(data.ToJson());
        }
        [HttpPost]
        public async Task< IActionResult> AddUser(User users)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.Session.GetString("UserID");

                string strLoginName = users.LoginName;

                if (OnlyLoginName(strLoginName) ==true)
                {
                    var json = new
                    {
                        errorMsg = "登入名重复！,添加失败!"
                    };
                    return Json(json);
                }
                User m = new User();
                string cID = SysUserID().ToString();
                m.UserID = cID;
                m.UserName = users.UserName;
                m.LoginName = users.LoginName;
                m.LoginPWD = users.LoginPWD;
                m.RID = users.RID;
                m.OID = users.OID;
                m.Sex = users.Sex;
                m.Phone = users.Phone;
                m.Status = "正常";
                m.BZ = users.BZ;
                m.CreateDate = DateTime.Now;
                m.CreateName = UserID;
                conn.User.Add(m);

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

        public  bool OnlyLoginName(string LoginName)
        {
          
                var data = conn.User.Where(b => b.LoginName == LoginName).ToList();
                if (data.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
         
        }

        public  string SysUserID()
        {
            string DjNO = "";
            List<string> OldNum = new List<string>();

            var roles = conn.User.OrderByDescending(b => b.UserID).FirstOrDefault();
            if (roles != null)
            {
                OldNum = roles.UserID.ToString().Split('-').ToList<string>();
            }
            if (!(OldNum.Count > 0))
            {
                DjNO = string.Format("{0}-{1}", "US", "0001");
            }
            else
            {
                DjNO = string.Format("{0}-{1}", "US", (Convert.ToInt32(OldNum[OldNum.Count - 1]) + 1).ToString().PadLeft(4, '0'));

            }
            return DjNO;

        }

        public async Task<ActionResult> GetFormList(int ids)
        {
            var users = await conn.User.FirstOrDefaultAsync(a => a.id == ids);
            return Content(users.ToJson());
        }
        public IActionResult EditIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User users)
        {
            if (ModelState.IsValid)
            {
                if (users != null)
                {
                    var m = await conn.User.FirstOrDefaultAsync(a => a.UserID == users.UserID);
                    if (m != null)
                    {
                        string UserID = HttpContext.Session.GetString("UserID");
                        m.UserName = users.UserName;
                        m.LoginName = users.LoginName;
                        m.RID = users.RID;
                        m.OID = users.OID;
                        m.Sex = users.Sex;
                        m.Phone = users.Phone;
                        m.BZ = users.BZ;
                        m.EditDate = DateTime.Now;
                        m.EditName = UserID;
                    }

                    try
                    {
                      await  conn.SaveChangesAsync();
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
        public async Task<IActionResult> DeleteUser(int ids)
        {
            var m = await conn.User.FirstOrDefaultAsync(u => u.id == ids);
            if (m != null)
            {
                conn.User.Remove(m);
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

            List<View_User> Viewusers = await conn.User.Select(p => new View_User { id = p.id, UserID = p.UserID, UserName = p.UserName, LoginName = p.LoginName, LoginPWD = p.LoginPWD, RID = p.RID, OID = p.OID, Sex = p.Sex, Phone = p.Phone, BZ = p.BZ, Status = p.Status, CreateDate = p.CreateDate, CreateName = p.CreateName, EditDate = p.EditDate, EditName = p.EditName, RoleName = p.Role.RoleName, RoleType = p.Role.RoleType, OfficeName = p.Office.OfficeName, FZName = p.Office.FZName }).ToListAsync();
            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            Viewusers = Viewusers.Where(b => b.UserName.Contains(StrSearch)).ToList();
                            break;
                        case "1":
                            Viewusers = Viewusers.Where(b => b.LoginName.Contains(StrSearch)).ToList();

                            break;
                        case "2":
                            Viewusers = Viewusers.Where(b => b.OfficeName.Contains(StrSearch)).ToList();
                            break;
                        case "3":
                            Viewusers = Viewusers.Where(b => b.Phone.Contains(StrSearch)).ToList();
                            break;
                        default:

                            break;
                    }

                }
            }


            byte[] buffer = ExcelHelp.Export<View_User>(Viewusers,"用户信息","用户信息", SysTool.GetPropertyNameArray<View_User>()).GetBuffer();


            var fileName = "用户信息" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }

    }
}