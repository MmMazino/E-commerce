﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class Graph : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
