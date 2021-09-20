using DhakaPlaza.Data;
using DhakaPlaza.Models;
using DhakaPlaza.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DhakaPlaza.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var data = _db.Products.Include(a => a.ProductTypes).Include(b => b.SpecialTags).ToList();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        // Get Products Details action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = _db.Products.Include(a => a.ProductTypes)
                .FirstOrDefault(c => c.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST Products Details action Method
        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductDetails(int? id)
        {
            List<Products> session_list = new List<Products>();
            
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(a => a.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            session_list = HttpContext.Session.Get<List<Products>>("session_list"); // without this only one data will get everytime
            if(session_list == null)
            {
                session_list = new List<Products>();
            }
            session_list.Add(product);
            HttpContext.Session.Set("session_list", session_list);
            return View(product);
        }

        // GET Remove from cart action Method
        public IActionResult Remove(int? id)
        {
            List<Products> session_list = HttpContext.Session.Get<List<Products>>("session_list");
            if (session_list != null)
            {
                var cur_session_list = session_list.FirstOrDefault(c => c.Id == id);
                if (cur_session_list != null)
                {
                    session_list.Remove(cur_session_list);
                    HttpContext.Session.Set("session_list", session_list);
                }
            }
            return RedirectToAction(nameof(Cart));
        }
        // POST Remove from cart action Method
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemoveFromCart(int? id)
        {
            List<Products> session_list = HttpContext.Session.Get<List<Products>>("session_list"); 
            if (session_list != null)
            {
                var cur_session_list = session_list.FirstOrDefault(c => c.Id == id);
                if(cur_session_list != null)
                {
                    session_list.Remove(cur_session_list);
                    HttpContext.Session.Set("session_list", session_list);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // Get product cart action method
        public IActionResult Cart()
        {
            List<Products> session_list = HttpContext.Session.Get<List<Products>>("session_list"); 
            if (session_list == null)
            {
                session_list = new List<Products>();
            }

            return View(session_list);
        }
    }
}
