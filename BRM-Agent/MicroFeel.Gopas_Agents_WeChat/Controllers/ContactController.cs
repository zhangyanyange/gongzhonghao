using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroFeel.Pramy.Agent.Controllers
{
    public class ContactController : Controller
    {
        // GET: /<controller>/
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Feedback()
        {
            return View();
        }
        public ViewResult Opinion()
        {
            return View();
        }
    }
}
