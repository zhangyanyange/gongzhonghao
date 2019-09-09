using Microsoft.AspNetCore.Mvc;
using MQ.WeChat.Models;

namespace MQ.WeChat.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Authorization(AuthorizationModel authorizationModel)
        {
            ViewData["openId"] = authorizationModel.OpenId;
            return View(authorizationModel);
        }
    }
}