
using Microsoft.AspNetCore.Mvc;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class OrderTypeController : Controller
    {
        public ViewResult OrderType(string openId)
        {
            ViewData["openId"] = openId;
            return View();
        }
    }
}