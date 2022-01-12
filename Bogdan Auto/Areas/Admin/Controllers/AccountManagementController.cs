using Bogdan_Auto.Data;
using Bogdan_Auto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Bogdan_Auto.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AdminUser> _userManagerAdmin;
        private readonly UserManager<DeliveryStaffUser> _userManagerDelivery;
        private readonly IUserStore<AdminUser> _userStoreAdmin;
        private readonly IUserStore<DeliveryStaffUser> _userStoreDelivery;
        private readonly IUserEmailStore<AdminUser> _emailStoreAdmin;
        private readonly IUserEmailStore<DeliveryStaffUser> _emailStoreDelivery;

        public AccountManagementController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<IdentityUser> users = new List<IdentityUser>();
            var admins = _context.AdminUsers.ToList();
            foreach (var user in admins)
            {
                users.Append(user);
            }
            var deliveryStaff = _context.DeliveryStaffUsers.ToList();
            foreach (var delivery in deliveryStaff)
            {
                users.Append(delivery);
            }

            return View(users);
        }

        //GET Create Admin Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Admin Ation Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountRegisterViewModel accountModel)
        {
            return View(accountModel);
        }
    }

}
