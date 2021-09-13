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
    public class SpecialTagsController : Controller
    {

        private ApplicationDbContext _db;
        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.SpecialTags.ToList());
        }

        // Get Create action Method
        public ActionResult Create()
        {
            return View();
        }
        // Post Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if (ModelState.IsValid) // check Annotations, server-site, if it can fullfill my assigned Annotations
            {
                _db.SpecialTags.Add(specialTags); // Adding in DB
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        // Get Edit action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id); // finding data by id
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }
        // Post Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTags specialTags)
        {
            if (ModelState.IsValid) // check, if it can fullfill my assigned Annotations
            {
                _db.Update(specialTags); // Updating in DB
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }


        // Get Details action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id); // finding data by id
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }
        // Post Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTags specialTags)
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
            var specialTag = _db.SpecialTags.Find(id); // finding data by id
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }
        // Post Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTags specialTags)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (id != specialTags.Id)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id); // finding data by id
            if (specialTag == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid) // check, if it can fullfill my assigned Annotations
            {
                _db.Remove(specialTag); // Delete in DB
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }
    }
}
