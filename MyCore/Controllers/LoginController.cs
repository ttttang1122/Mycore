using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.Models;
using MyCore.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace MyCore.Controllers
{
    public class LoginController : Controller
    {
   
        private MyCoreContext conn;     
        public LoginController(MyCoreContext _conn)
        {
            conn = _conn;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginIn(string loginName, string password)
        {
            var users = await conn.User.FirstOrDefaultAsync(b => b.LoginName == loginName && b.LoginPWD == password);
            //var users = await _IUser.CheckUser(loginName, password);
            if (users!=null)
            {
                HttpContext.Session.SetString("LoginName", users.LoginName);
                HttpContext.Session.SetString("LoginPWD", users.LoginPWD);
                HttpContext.Session.SetString("UserID", users.UserID);
                HttpContext.Session.SetString("UserName", users.UserName);    
                
                return Json(new { status = 1, jumpurl = "/Main/main/" });
            }
            return Json(new { status = 2, errorMessage = "用户不存在或密码错误" });
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(LoginController.Index), "Index");
            }
        }



    }
}