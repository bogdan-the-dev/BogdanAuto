using Microsoft.AspNetCore.Mvc;
using Bogdan_Auto.Data;
using Bogdan_Auto.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bogdan_Auto.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryManagementController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }

        //Create Get action method
        public ActionResult Create()
        {
            return View();
        }

        //Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (NameExists(category.Name))
                {
                    ModelState.AddModelError(string.Empty, "Category already exists");
                    return View(category);
                }
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["save"] = "Category created";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //Get Edit action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var selectedCategory = _context.Categories.Find(id);
            if (selectedCategory == null)
            {
                return NotFound();
            }
            return View(selectedCategory);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (NameExists(category.Name))
                {
                    ModelState.AddModelError(string.Empty, "Category already exists");
                    return View(category);
                }
                _context.Update(category);
                await _context.SaveChangesAsync();
                TempData["edit"] = "Category name changed";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET Delete Action
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var selectedCategory = _context.Categories.Find(id);
            if (selectedCategory == null)
            {
                return NotFound();
            }
            return View(selectedCategory);
        }


        //POST Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Category category)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != category.Id)
            {
                return NotFound();
            }
            var categoryForDeletion = _context.Categories.Find(id);
            if (categoryForDeletion == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Remove(categoryForDeletion);
                await _context.SaveChangesAsync();
                TempData["delete"] = "The category has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool NameExists(string name)
        {
            return _context.Categories.Any(c => c.Name == name);
        }

    }

}
