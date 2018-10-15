using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyCore.Controllers
{
    public class BaseController : Controller
    {
        protected string UserID
        {
            set
            {
       
                HttpContext.Session.GetString("UserID");
            }

            get
            {
                if (HttpContext.Session.GetString("UserID") == null)
                {
                    return null;
                }
                else
                {
                    return HttpContext.Session.GetString("UserID");
                }
            }
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            byte[] result;
            filterContext.HttpContext.Session.TryGetValue("UserID", out result);
            if (result == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            base.OnActionExecuted(filterContext);
        }
        
    }
}