using Microsoft.AspNetCore.Mvc;
using Bogdan_Auto.Data;
using Bogdan_Auto.Models;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Bogdan_Auto.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ProductManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _he;
        public ProductManagementController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(p=>p.Category).Include(p=>p.Manufacturer).ToList());
        }

        //POST Index Action method
        [HttpPost]
        public IActionResult Index(string? productName)
        {
            if(productName == null)
            {
                return View(_context.Products.Include(p => p.Category).Include(p => p.Manufacturer).ToList());
            }
            var products = _context.Products.Include(p => p.Category).Include( p => p.Manufacturer).Where(p => p.Name.Contains(productName)).ToList();
            return View(products);
        }


        //GET method for Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers.ToList(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }

        //POST method for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile PhotoPath)
        {
            ModelState.Remove("orderProducts");
            if (ModelState.IsValid)
            {
                if (IsNameUnique(product))
                {
                    ModelState.AddModelError(string.Empty, "Product already exists");
                    ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers.ToList(), "Id", "Name");
                    ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
                    return View(product);
                }
                if(PhotoPath != null)
                {
                    var folderPath = Path.Combine(_he.WebRootPath, "WebSite Resources" , product.Name);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    var imageName = Path.Combine(folderPath, PhotoPath.FileName);
                    var fileStream = new FileStream(imageName, FileMode.Create);
                    await PhotoPath.CopyToAsync(fileStream);
                    fileStream.Close();
                    product.PhotoPath = Path.Combine("WebSite Resources", product.Name, PhotoPath.FileName);
                    Console.WriteLine(Path.Combine("WebSite Resources", product.Name, PhotoPath.FileName));
                }
                if (PhotoPath == null)
                {
                    product.PhotoPath = Path.Combine("Default", "noimage.PNG");
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                TempData["save"] = "Product created";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers.ToList(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(product);
        }

        //GET Edit Action Method
        public ActionResult Edit(int? id)
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers.ToList(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
            if(id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Include(p=>p.Category).Include(p=>p.Manufacturer).FirstOrDefault(p=>p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile PhotoPath)
        {
            ModelState.Remove("orderProducts");
            var photoUnchanged = false;
            var productForValidation = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
          //  if (productForValidation.PhotoPath.Contains(PhotoPath.FileName))
            //    {
         // //      photoUnchanged = true;
//ModelState.Remove("PhotoPath");
        //    }
            if (ModelState.IsValid)
            {
                if (IsNameUnique(product))
                {
                    
                    if (productForValidation != null && productForValidation.Name != product.Name)
                    { 
                        ModelState.AddModelError(string.Empty, "Product already exists");
                        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers.ToList(), "Id", "Name");
                        ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
                        return View(product);
                    }
                }
                if (!photoUnchanged)
                {
                    if (PhotoPath != null)
                    {
                        var folderPath = Path.Combine(_he.WebRootPath, "WebSite Resources", product.Name);
                        folderPath.Replace(" ", "_");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        } 
                        var imageName = Path.Combine(folderPath, PhotoPath.FileName);
                        var fileStream = new FileStream(imageName, FileMode.Create);
                        await PhotoPath.CopyToAsync(fileStream);
                        fileStream.Close();
                        product.PhotoPath = Path.Combine("WebSite Resources", product.Name, PhotoPath.FileName);
                    }
                    if (PhotoPath == null)
                    {
                        product.PhotoPath = Path.Combine("Default", "noimage.PNG");
                    }
                }
                TempData["edit"] = "Changes were saved";
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //GET Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Include(p=>p.Category).Include(p=>p.Manufacturer).FirstOrDefault(p=>p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers.ToList(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(product);
        }

        //GET IncrementStock Action Method
        public IActionResult IncrementStock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Include(p => p.Category).Include(p => p.Manufacturer).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST IncrementStock Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IncrementStock(Product product)
        {
            ModelState.Remove("orderProducts");

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //GET Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(p => p.Category).Include(p => p.Manufacturer).Where(p => p.Id == id).FirstOrDefault();
            if(product == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers.ToList(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(product);
        }

        //POST Delete Action Method
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            TempData["delete"] = "Product deleted";
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool IsNameUnique(Product product)
        {
            return _context.Products.Any(p=>p.Name == product.Name);
        }
    }
}
