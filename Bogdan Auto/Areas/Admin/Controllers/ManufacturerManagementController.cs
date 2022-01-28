using Microsoft.AspNetCore.Mvc;
using Bogdan_Auto.Data;
using Bogdan_Auto.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bogdan_Auto.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ManufacturerManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManufacturerManagementController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Create Get action method
        public IActionResult Index()
        {
            return View(_context.Manufacturers.ToList());
        }

        //Create Get action method
        public IActionResult Create()
        {
            return View();
        }


        //Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                if (NameExists(manufacturer.Name))
                {
                    ModelState.AddModelError(string.Empty, "Manufacturer already exists");
                    return View(manufacturer);
                }
                _context.Manufacturers.Add(manufacturer);
                await _context.SaveChangesAsync();
                TempData["save"] = "Manufacturer created";
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        //Get Edit action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var selectedManufacturer = _context.Manufacturers.Find(id);
            if (selectedManufacturer == null)
            {
                return NotFound();
            }
            return View(selectedManufacturer);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                if (NameExists(manufacturer.Name))
                {
                    ModelState.AddModelError(string.Empty, "Manufacturer already exists");
                    return View(manufacturer);
                }
                _context.Update(manufacturer);
                await _context.SaveChangesAsync();
                TempData["edit"] = "Manufacturer name changed";
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        //GET Delete Action
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var selectedManufacturer = _context.Manufacturers.Find(id);
            if (selectedManufacturer == null)
            {
                return NotFound();
            }
            return View(selectedManufacturer);
        }


        //POST Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Manufacturer manufacturer)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != manufacturer.Id)
            {
                return NotFound();
            }
            var manufacturerForDeletion = _context.Manufacturers.Find(id);
            if (manufacturerForDeletion == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Remove(manufacturerForDeletion);
                await _context.SaveChangesAsync();
                TempData["delete"] = "The manufacurer has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool NameExists(string name)
        {
            return _context.Manufacturers.Any(m => m.Name == name);
        }

    }

}
