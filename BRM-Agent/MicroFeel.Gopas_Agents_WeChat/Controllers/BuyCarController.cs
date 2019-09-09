using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class BuyCarController : Controller
    {
        public ViewResult BuyCar(string openId)
        {
            ViewData["openId"] = openId;
            return View();
        }
    }
}