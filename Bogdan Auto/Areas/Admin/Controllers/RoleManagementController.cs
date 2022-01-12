using Microsoft.AspNetCore.Mvc;
using Bogdan_Auto.Data;
namespace Bogdan_Auto.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleManagementController : Controller
    {
        private ApplicationDbContext _context;
        public RoleManagementController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
