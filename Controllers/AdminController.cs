using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Models.db;
namespace Ecommerce.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkadmin()
        {
            string checkuser = HttpContext.Request.Form["Username"];
            string checkpass = HttpContext.Request.Form["Password"];
            if (checkuser == "Petch" && checkpass == "1234")
            {
                return RedirectToAction("Admin");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Admin()
        {
            return View();
        }

    }
}
