using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.Models.db;
using Ecommerce.ViewModels;

namespace Ecommerce.Controllers
{
    public class AddAddress : Controller
    {
        DBONLINESHOPINGContext dop = new DBONLINESHOPINGContext();
        public IActionResult AddShipping()
        {
            TblMember user = SessionHelper.GetObjectFromJson<TblMember>(HttpContext.Session, "user");
            SessionHelper.SetObjectAsJson(HttpContext.Session, "order_id", DateTimeOffset.Now.ToUnixTimeSeconds());

            TblShippingDetail address = new TblShippingDetail();
            address.MemberId = user.MemberId;
            address.OrderId = SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "order_id");
            ViewBag.address = address;
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShipping(TblShippingDetail tblShippingDetail)
        {
            if (ModelState.IsValid)
            {
                
                TblMember user = SessionHelper.GetObjectFromJson<TblMember>(HttpContext.Session, "user");
                List<TblCart> cart = dop.TblCarts.ToList().Where(x => x.MemberId == user.MemberId && x.OrderId == null).ToList();
                ViewBag.total = cart.Sum(item => item.GetProduct().Price * item.Quantity);
                dop.Add(tblShippingDetail);
                cart.ForEach(x => x.OrderId = tblShippingDetail.OrderId);

                await dop.SaveChangesAsync();

                return RedirectToAction("AddAddressSuccess");
            }
            return View(tblShippingDetail);
        }
        public IActionResult AddAddressSuccess()
        {
            TblMember user = SessionHelper.GetObjectFromJson<TblMember>(HttpContext.Session, "user");
            List<TblCart> cart = dop.TblCarts.ToList().Where(x => x.MemberId == user.MemberId && x.OrderId == SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "order_id")).ToList();
            TblShippingDetail shipping = dop.TblShippingDetails.FirstOrDefault(x => x.OrderId == SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "order_id"));

            ViewBag.shipping = shipping;
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.GetProduct().Price * item.Quantity);
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}

