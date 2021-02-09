using Ecommerce.Models.db;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ecommerce.Controllers
{
    public class Chart : Controller
    {
        DBONLINESHOPINGContext dop = new DBONLINESHOPINGContext();
        public IActionResult ShowChart()
        {
            List<int> data = new List<int>();
            List<TblProduct> products = dop.TblProducts.ToList();
            products.ForEach(product =>
            {
                int sumProductSellQuantity = dop.TblCarts.Where(cart => cart.ProductId == product.ProductId).Sum(x => x.Quantity);
                data.Add(sumProductSellQuantity);
            });
            // ลิสชื่อสินค้า
            ViewBag.label = products.Select(x => x.ProductName).ToArray();
            // ลิสจำนวนการขายสินค้า
            ViewBag.data = data.ToArray();

            //List<Chartdata> chartData = new List<Chartdata>
            //{
            // new Chartdata { x= ViewBag.label[] , y= ViewBag.data[]},
            //};
            //ViewBag.dataSource = chartData;
            return View();

        }
    }

    public class Chartdata
    {
        public string x;
        public int y;
    }
}