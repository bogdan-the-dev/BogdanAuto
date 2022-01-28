using Microsoft.AspNetCore.Mvc;
using Bogdan_Auto.Data;
using Bogdan_Auto.Models;
using Microsoft.AspNetCore.Identity;
using Bogdan_Auto.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bogdan_Auto.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleManagementController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<CustomerUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public RoleManagementController(ApplicationDbContext context, UserManager<CustomerUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(_context.Roles.ToList());
        }

        //GET for Create
        public IActionResult Create()
        {
            return View();
        }

        //POST for create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(RoleExists(model.RoleName))
                {
                    ModelState.AddModelError(string.Empty, "Role name already exists");
                }
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    TempData["save"] = "Role created";
                    return RedirectToAction(nameof(Index));
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(model);
        }

        
        private bool RoleExists(string name)
        {
            return _context.Roles.Any(m => m.Name == name);
        }

    }
}
