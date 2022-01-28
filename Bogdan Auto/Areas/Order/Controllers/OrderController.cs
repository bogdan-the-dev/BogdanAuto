using Bogdan_Auto.Data;
using Bogdan_Auto.Models;
using Bogdan_Auto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bogdan_Auto.Areas.Order.Controllers
{
    [Area("Order")]
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }   

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Models.Order anOrder)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            double totalPrice = 0;
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderProduct orderDetails = new OrderProduct();
                    orderDetails.ProductId = product.Id;
                    totalPrice += product.Price;
                    orderDetails.ProductIndividualPrice = product.Price;
                    anOrder.Products.Add(orderDetails);
                }
            }
            var userID = User.Identity.Name;
            var currentUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.UserName == userID);
            anOrder.Email = currentUser.Email;
            anOrder.ClientId = currentUser.Id;
            anOrder.DateTime = DateTime.Now;
            anOrder.TrackingNumber = "";
            anOrder.TotalCost = totalPrice;
            _context.Order.Add(anOrder);
            await _context.SaveChangesAsync();

            if (products != null)
            {
                foreach (var product in products)
                {
                    var localProd = _context.Products.FirstOrDefault(p => p.Id == product.Id);
                    localProd.AvailableQuantity--;
                    _context.Update(localProd);
                    await _context.SaveChangesAsync();
                }
            }

            HttpContext.Session.Set("products", new List<Product>());
            return Redirect("https://localhost:49153");
        }


    }
}
