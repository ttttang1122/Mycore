using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyCore.Controllers
{
    [Produces("application/json")]
    [Route("api/BaseApi")]
    public class BaseApiController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        
            var rts = context.HttpContext.Request.Headers["token"];
            if (rts == string.Empty)
            {
                context.Result = NotFound();
            }
            else
            {
                if (rts.Contains("a"))
                {

                }
                //不通过跳转错误提示
                else
                {

                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new UnauthorizedResult();
                }
            }
            base.OnActionExecuted(context);
        }
    }
}