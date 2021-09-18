using DhakaPlaza.Data;
using DhakaPlaza.Models;
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
        // Post Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Products products)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
