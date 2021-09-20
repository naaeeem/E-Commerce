using DhakaPlaza.Data;
using DhakaPlaza.Models;
using DhakaPlaza.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DhakaPlaza.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        // GET Checkout action Method
        public IActionResult Checkout()
        {
            return View();
        }

        // POST Checkout action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order anOrder)
        {
            List<Products> session_list = HttpContext.Session.Get<List<Products>>("session_list");
            if(session_list != null)
            {
                foreach (var item in session_list )
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = item.Id;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }
            anOrder.OrderNo = GetOrderNo();
            _db.Orders.Add(anOrder);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("session_list", new List<Products>()); // clearing session data
            return View();
        }

        public string GetOrderNo()
        {
            int rowCount = _db.Orders.ToList().Count + 1;
            return rowCount.ToString();
        }

    }
}
