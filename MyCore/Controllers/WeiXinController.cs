using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyCore.Controllers
{
    [Produces("application/json")]
    [Route("api/WeiXin")]
    public class WeiXinController : BaseApiController
    {

        [Route("test"),HttpGet]
        public object test(string name)
        {
            var names = name + 1;
            return names;
        }

    }
}