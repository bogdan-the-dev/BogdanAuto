using Microsoft.AspNetCore.Mvc;

namespace Bogdan_Auto.Areas.Info.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
