using Bogdan_Auto.Data;
using Bogdan_Auto.Models;
using Bogdan_Auto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
namespace Bogdan_Auto.Controllers
{
    [Authorize(Roles = "Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index(int? page, string? name)
        {
            if (name != null)
            {
                return View(_context.Products.Include(c => c.Manufacturer).Include(c => c.Category).Where(c => c.Name.Contains(name)).ToList().ToPagedList(page ?? 1, 9));
            }
            return View(_context.Products.Include(c => c.Manufacturer).Include(c => c.Category).ToList().ToPagedList(page ?? 1, 9));
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult AboutUs()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET product detail action method
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.Category).Include(p => p.Manufacturer).FirstOrDefault(c => c.Id == id);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers.ToList(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "Id", "Name");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST product detail acation method
        [HttpPost]
        [AllowAnonymous]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<Product> products = new List<Product>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.Category).Include(p => p.Manufacturer).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }
        //GET Remove action methdo
        [AllowAnonymous]
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Cart));
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //GET product Cart action method
        [AllowAnonymous]
        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            return View(products);
        }

        //GET action method for viewing the orders
        public IActionResult MyOrders()
        {
            return View(_context.Order.Where(o => o.Email == User.Identity.Name).ToList());
        }
        
        public IActionResult OrderDetail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var order = _context.Order.FirstOrDefault(o => o.Id == id);
            if(order == null)
            {
                return NotFound();
            }
            ViewData["order"] = order;
            return View(_context.OrderProduct.Where(p => p.OrderId == id).ToList());
        }



    }
}