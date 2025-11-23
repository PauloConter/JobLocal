using Microsoft.AspNetCore.Mvc;

namespace JobLocal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ComoFunciona()
        {
            return View();
        }
    }
}