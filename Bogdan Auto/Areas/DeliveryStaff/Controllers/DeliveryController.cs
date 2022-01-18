using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bogdan_Auto.Data;
using Bogdan_Auto.Models;

namespace Bogdan_Auto.Areas.DeliveryStaff.Controllers
{
    [Area("DeliveryStaff")]
    [Authorize(Roles = "DeliveryStaff")]
    public class DeliveryController : Controller
    {
        private ApplicationDbContext _context;
        public DeliveryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Order.ToList());
        }

        //GET Action Method for details
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            ViewData["order"] = _context.Order.FirstOrDefault(o => o.Id == id);
            return View(_context.OrderProduct.Where(o => o.OrderId == id).ToList());
        }

        //GET Action Method for update status
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(_context.Order.FirstOrDefault(o => o.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.Order order)
        {
            if(order.Status == null)
            {
                return NotFound();
            }
            var orderFromDb = _context.Order.FirstOrDefault(o => o.Id == order.Id);
            if(orderFromDb == null)
            {
                return NotFound();
            }
            orderFromDb.Status = order.Status;
            orderFromDb.TrackingNumber = order.TrackingNumber;
            if (orderFromDb.TrackingNumber == null)
            {
                orderFromDb.TrackingNumber = "";
            }
            _context.Order.Update(orderFromDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
