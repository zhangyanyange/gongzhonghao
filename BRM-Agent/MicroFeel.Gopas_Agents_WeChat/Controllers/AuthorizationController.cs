using MicroFeel.Pramy.Agent.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class AuthorizationController : Controller
    {
        public ViewResult Authorization(AuthorizationModel authorizationModel)
        {
            ViewData["openId"] = authorizationModel.OpenId;
            return View(authorizationModel);
        }
    }
}