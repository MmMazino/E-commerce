using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			ProductModel productModel = new ProductModel();
			ViewBag.products = productModel.findAll();
			return View();
		}
	}
}
     