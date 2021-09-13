using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DhakaPlaza.Data;
using DhakaPlaza.Models;

namespace DhakaPlaza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var data = _db.ProductTypes.ToList();
            return View(data);
        }

        // Get Create action Method
        public ActionResult Create()
        {
            return View();
        }
        // Post Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes) 
        {
            if(ModelState.IsValid) // check, if it can fullfill my assigned Annotations
            {
                _db.ProductTypes.Add(productTypes); // Adding in DB
                await _db.SaveChangesAsync();
                TempData["save"] = "Saved";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }


        // Get Edit action Method
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id); // finding data by id
            if(productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        // Post Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid) // check, if it can fullfill my assigned Annotations
            {
                _db.Update(productTypes); // Updating in DB
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        // Get Details action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id); // finding data by id
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        // Post Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        }


        // Get Delete action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id); // finding data by id
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        // Post Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {

            if(id == null)
            {
                return NotFound();
            }
            if (id != productTypes.Id)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid) // check, if it can fullfill my assigned Annotations
            {
                _db.Remove(productType); // Delete in DB
                await _db.SaveChangesAsync();
                TempData["save"] = "Deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
    }
}
