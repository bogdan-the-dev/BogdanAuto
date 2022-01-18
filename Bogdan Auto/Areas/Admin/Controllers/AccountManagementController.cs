using Bogdan_Auto.Data;
using Bogdan_Auto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Bogdan_Auto.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CustomerUser> _userManager;
        private readonly IUserStore<CustomerUser> _userStore;

        public AccountManagementController(ApplicationDbContext context, UserManager<CustomerUser> user, IUserStore<CustomerUser> userStore)
        {
            _context = context;
            _userManager = user;
            _userStore = userStore;

        }
        public IActionResult Index()
        {
            List<CustomerUser> users = new List<CustomerUser>();
            var allUsers = _context.Users.ToList();
            foreach (var user in allUsers)
            {
                Console.WriteLine(user.UserName);
                Console.WriteLine(user.isAdmin);
                if(user.isAdmin || user.isDelivery)
                    users.Add(user);
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
            if (ModelState.IsValid)
            {
                if (isemailTaken(accountModel.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email already registered");
                    return View(accountModel);
                }

                var user = new CustomerUser()
                {
                    FirstName = accountModel.FirstName,
                    LastName = accountModel.LastName,
                    EmailConfirmed = true
                };
                user.isDelivery = false;
                user.isAdmin = false;
                if (accountModel.AccountType == "Admin")
                {
                    user.isAdmin = true;
                }
                if (accountModel.AccountType == "Delivery")
                {
                    user.isDelivery = true;
                }

                await _userStore.SetUserNameAsync(user, accountModel.Email, CancellationToken.None);
                await _userManager.SetEmailAsync(user, accountModel.Email);
                var result = await _userManager.CreateAsync(user, accountModel.Password);

                if (result.Succeeded)
                {

                    if (user.isAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (user.isDelivery)
                    {
                        await _userManager.AddToRoleAsync(user, "DeliveryStaff");
                    }

                    var createdUser = _context.Users.FirstOrDefault(u => u.Email == accountModel.Email);
                    if (createdUser != null) 
                    {
                        createdUser.EmailConfirmed = true;
                        _context.Users.Update(createdUser);
                        _context.SaveChanges();
                    }

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(accountModel);
        }

        //GET DELETE action
        public IActionResult Delete(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            if(user.isAdmin)
            {
                ViewData["option"] = "Admin";
            }
            else if(user.isDelivery)
            {
                ViewData["option"] = "Delivery";
            }
            var viewModel = new AccountRegisterViewModel();
            viewModel.Email = user.Email;
            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            return View(viewModel);
        }


        //POST DELETE Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string? id, AccountRegisterViewModel user)
        {
            if(id == null)
            {
                return NotFound();
            }

            var userDeletion = _context.Users.Find(id);
            if(userDeletion == null)
            {
                return NotFound();
            }
          
                _context.Remove(userDeletion);
                await _context.SaveChangesAsync();
                TempData["delete"] = "The accound was deleted";
                return RedirectToAction(nameof(Index));
            
        }


        private bool isemailTaken(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
