using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace src.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(string password)
        {
            HttpContext.Session.SetString("dwp", password);
            return RedirectToAction("Index", "Tree");
        }
    }
}
