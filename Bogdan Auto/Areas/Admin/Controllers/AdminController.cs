using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bogdan_Auto.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        //Admin main page
        [Authorize(Roles = "Admin")]

        public IActionResult Index()
        {
            return View();
        }


    }
}
