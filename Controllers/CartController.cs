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
	public class CartController : Controller
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
			//List<TblProduct> cart = SessionHelper.GetObjectFromJson<List<TblProduct>>(HttpContext.Session, "cart");
			ViewBag.cart = cart;
			ViewBag.total = cart.Sum(item => item.GetProduct().Price * item.Quantity);
			return View();
		}

		private int isExit(int id)
		{
			List<TblProduct> cart = SessionHelper.GetObjectFromJson<List<TblProduct>>(HttpContext.Session, "cart");
			for (int i=0; i< cart.Count;i++)
			{
				if(cart[i].ProductId.Equals(id))
				{
					return i;
				}
			}
			return -1;
		}
		public async Task<IActionResult> Buy(int id)
		{
			// TODO :: apply user to cart if login success
			TblMember user = SessionHelper.GetObjectFromJson<TblMember>(HttpContext.Session, "user");
			TblCart cartExit = dop.TblCarts.Where(x => x.ProductId == id && x.OrderId == null).FirstOrDefault();
			if (cartExit == null)
            {
				TblCart tblCart = new TblCart();
				tblCart.ProductId = id;
				tblCart.Quantity = 1;
				if (user != null)
				{
					tblCart.MemberId = user.MemberId;
				}

				//tblCart.Product = productModel.find(id);
				dop.Add(tblCart);
				await dop.SaveChangesAsync();
			}

			/*ProductModel productModel = new ProductModel();
			if(SessionHelper.GetObjectFromJson<List<TblProduct>>(HttpContext.Session,"cart")==null)
			{
				List<TblProduct> cart = new List<TblProduct>();
				var product = productModel.find(id);
				product.Quantity = 1;
				cart.Add(product);

				dop.Add(new TblCart(cart));

				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			}
			else
			{
				List<TblProduct> cart = SessionHelper.GetObjectFromJson<List<TblProduct>>(HttpContext.Session, "cart");
				int index = isExit(id);
				if(index != -1)
				{
					cart[index].Quantity++;
				}
				else
				{
					var product = productModel.find(id);
					product.Quantity = 1;
					cart.Add(product);
				}
				SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			}*/


			return Redirect("/Cart");
		}

		public async Task<IActionResult> Increase(int id)
		{
			TblCart tblCart = dop.TblCarts.FirstOrDefault(x => x.CartId == id);
			tblCart.Quantity = tblCart.Quantity + 1;
			dop.Update(tblCart);
			await dop.SaveChangesAsync();
			/*List<TblProduct> cart = SessionHelper.GetObjectFromJson<List<TblProduct>>(HttpContext.Session, "cart");
			int index = isExit(id);
			var product = cart[index];
			product.Quantity = product.Quantity+1;

			SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);*/
			return Redirect("/Cart");
		}

		public async Task<IActionResult> Decrease(int id)
		{
			TblCart tblCart = dop.TblCarts.FirstOrDefault(x => x.CartId == id);
			if (tblCart.Quantity > 1)
			{
				tblCart.Quantity = tblCart.Quantity - 1;
			}
			dop.Update(tblCart);
			await dop.SaveChangesAsync();
			/*List<TblProduct> cart = SessionHelper.GetObjectFromJson<List<TblProduct>>(HttpContext.Session, "cart");
			int index = isExit(id);
			var product = cart[index];
			if (product.Quantity > 1)
            {
				product.Quantity = product.Quantity - 1;
			}

			SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);*/
			return Redirect("/Cart");
		}

		public async Task<IActionResult> Remove(int id)
		{
			TblCart tblCart = dop.TblCarts.FirstOrDefault(x => x.CartId == id);
			dop.Remove(tblCart);
			await dop.SaveChangesAsync();

			/*List<TblProduct> cart = SessionHelper.GetObjectFromJson<List<TblProduct>>(HttpContext.Session, "cart");
			int index = isExit(id);
			cart.RemoveAt(index);
			SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);*/
			return RedirectToAction("Index");

		}

		public async Task<IActionResult> RemoveAll()
		{
			List<TblCart> cart = dop.TblCarts.Where(x => x.OrderId == null).ToList();
			cart.ForEach(x =>
			{
				dop.Remove(x);
			});
			await dop.SaveChangesAsync();
			//	List<TblProduct> cart = SessionHelper.GetObjectFromJson<List<TblProduct>>(HttpContext.Session, "cart");
			//	TblProduct.RemoveAll(cart);
			//	SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			return Redirect("/");
		}
	}
}
