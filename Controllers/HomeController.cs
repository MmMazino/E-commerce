using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models.db;
using Ecommerce.Models;
using Ecommerce.Helpers;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        DBONLINESHOPINGContext dop = new DBONLINESHOPINGContext();
        


        public IActionResult Index()
        {
            var ps = from p in dop.TblProducts select p;
            return View(ps);
        }

        public IActionResult SearchProduct(string CheckSearch)
        {
            if (string.IsNullOrEmpty(CheckSearch))
            {
                return View("SearchProduct", dop.TblProducts.ToList());
            }
            else
            {
                return View("SearchProduct", dop.TblProducts.Where(p => p.ProductName.Contains(CheckSearch)).ToList());
            }
        }

        public IActionResult Meat()
        {
            var meats = dop.TblProducts.Where(p => p.CategoryId == 2).ToList();
            return View(meats);
        }

        public IActionResult Vegetable()
        {
            var vet = dop.TblProducts.Where(p => p.CategoryId == 1).ToList();
            return View(vet);
        }

        public IActionResult Register2()
        {
            return Redirect("/Home/Register");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(TblMember tblMember)
        {
            if (ModelState.IsValid)
            {
                dop.Add(tblMember);
                await dop.SaveChangesAsync();
                return Redirect("/");
            }
            return View(tblMember);
        }

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = dop.TblMembers.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);
            if (user == null)
            {
                return Redirect("/Login");
            }
            else
            {
                // TODO :: if have product in cart go to shipping address else go to home page
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", user);

                List<TblCart> cart = dop.TblCarts.Where(x => x.MemberId == null).ToList();
                if (cart.Count > 0)
                {
                    // adjust cart with current member
                    cart.ForEach(x => x.MemberId = user.MemberId);
                    await dop.SaveChangesAsync();

                    return Redirect("/AddAddress/AddShipping");
                } else
                {
                    return Redirect("/");
                }

                
            }
        }

        public IActionResult CheckShipping()
        {
            var member = SessionHelper.GetObjectFromJson<TblMember>(HttpContext.Session, "user");
            if (member != null)
            {
                return Redirect("/AddAddress/AddShipping");
            } else
            {
                return Redirect("/Home/Login");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
