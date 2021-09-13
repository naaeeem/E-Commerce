using DhakaPlaza.Data;
using DhakaPlaza.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DhakaPlaza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;
        //private IHostingEnvironment _he; // previos
        public ProductsController(ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }


        public IActionResult Index()
        {
            // a, b are variable, we can use anything
            // bacause of relation database, using include
            var data = _db.Products.Include(a=>a.ProductTypes).Include(b=>b.SpecialTags).ToList();
            return View(data);
        }

        // Get Create action Method
        public ActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["tagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "SpecialTag");
            return View();
        }
        // Post Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (ModelState.IsValid) // check, if it can fullfill my assigned Annotations
            {
                if(image != null) // image address saving
                {
                    string __path = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(__path, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }
                if(image == null)
                {
                    products.Image = "Images/no_image.jpg";
                }
                _db.Products.Add(products); // Adding in DB
                await _db.SaveChangesAsync();
                TempData["save"] = "Saved";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }


        // Get Edit action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = _db.Products.Include(a => a.ProductTypes).Include(b => b.SpecialTags)
                .FirstOrDefault(c=>c.Id == id);
            // it will return data, if value is matched against id
            if (products == null)
            {
                return NotFound();
            }
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["tagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "SpecialTag");
            return View(products);
        }

        // Post Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        // same as create post, but we have to catch the id by a hidden input type from Edit-view
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid) // check, if it can fullfill my assigned Annotations
            {
                if (image != null) // image address saving
                {
                    string __path = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(__path, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "Images/no_image.jpg";
                }
                _db.Products.Update(products); // Updating in DB
                await _db.SaveChangesAsync();
                TempData["save"] = "Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }


        // Get Details action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = _db.Products.Include(a => a.ProductTypes).Include(b => b.SpecialTags)
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

        // Get Delete action Method
        
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c=>c.SpecialTags).Include(c=>c.ProductTypes).Where(c => c.Id == id).FirstOrDefault();
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Post Delete Action Method
        [HttpPost]
        [ActionName("Delete")] // to call DeleteConfirm as Delete
        public async Task<IActionResult> DeleteConfirm(int? id) // we cant call same type method twice, so that changed the method name
        {
            if(id == null)
            {
                return NotFound();
            }
            var products = _db.Products.FirstOrDefault(c => c.Id == id);
            if(products == null)
            {
                return NotFound();
            }
            _db.Products.Remove(products);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}