using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyCore.Controllers
{
    public class TestController : Controller
    {
        public IActionResult RealName()
        {
            return View();
        }
    }
}