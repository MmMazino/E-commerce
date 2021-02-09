using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.Models.db;

namespace Ecommerce.Controllers
{
	public class CheckoutController : Controller
	{
		DBONLINESHOPINGContext dop = new DBONLINESHOPINGContext();
		public IActionResult Index()
		{
			List<TblCart> cart = dop.TblCarts.Where(x => x.OrderId == null).ToList();
			TblMember user = SessionHelper.GetObjectFromJson<TblMember>(HttpContext.Session, "user");
			if (user != null)
			{
				cart = cart.Where(x => x.MemberId == user.MemberId).ToList();
			}
			else
			{
				cart = cart.Where(x => x.MemberId == null).ToList();
			}

			ViewBag.cart = cart;
			if (cart.Count != 0)
            {
				ViewBag.total = cart.Sum(item => item.GetProduct().Price * item.Quantity);
			} else
            {
				return Redirect("/");
			}
			return View();
		}
	}
}
