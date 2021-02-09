using Ecommerce.Models.db;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class AddProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        DBONLINESHOPINGContext dop = new DBONLINESHOPINGContext();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(TblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                dop.Add(tblProduct);
                await dop.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblProduct);
        }

        public IActionResult ProductDetail()
        {
            var ps = from p in dop.TblProducts select p;
            return View(ps);
        }
    }
}
