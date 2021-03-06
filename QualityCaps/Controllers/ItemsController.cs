﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using QualityCaps.Models;

namespace QualityCaps.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index(string sortOrder, string currentFilter, string searchString,string currentCatagory, int? page)
        {

            var catagoryId = string.IsNullOrWhiteSpace(currentCatagory)
                ? 0
                : Convert.ToInt32(currentCatagory.Substring(0, 1));
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            var catagories = new List<string>();
            foreach (var catagory in db.Catagories)
            {
                catagories.Add(catagory.ID + catagory.Name);
            }
            ViewBag.Catagories = catagories;
            
            if (searchString != null)
            {
                page = 1;
            }
            
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var items = from i in db.Items
                        select i;
            if (catagoryId != 0)
            {
                items = items.Where(s => s.CatagorieId == catagoryId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                       || s.Catagorie.Name.ToUpper().Contains(searchString.ToUpper()));
                items = items.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                       || s.Supplier.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.Price);
                    break;
                default:  // Name ascending 
                    items = items.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View( items.ToPagedList(pageNumber, pageSize));


            //var items = db.Items.Include(i => i.Catagorie);
            //return View(await items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var colors = new List<string>();
            foreach (var color in db.Colors)
            {
                colors.Add(color.Name);
            }
            ViewBag.Colors = colors;
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CatagorieId = new SelectList(db.Catagories, "ID", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "ID", "Name");

            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CatagorieId = new SelectList(db.Catagories, "ID", "Name", item.CatagorieId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "ID", "Name", item.SupplierId);
            return View(item);
        }

        // GET: Items/Edit/5
         [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatagorieId = new SelectList(db.Catagories, "ID", "Name", item.CatagorieId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "ID", "Name", item.SupplierId);
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id,Item newItem)
        {
            Item item = await db.Items.FindAsync(id);
            if (id != null)
            {
                item.Name = newItem.Name;
                item.CatagorieId = newItem.CatagorieId;
                item.Price = newItem.Price;
                if (newItem.Image != null)
                {
                    item.Image = newItem.Image;
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CatagorieId = new SelectList(db.Catagories, "ID", "Name", item.CatagorieId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "ID", "Name", item.SupplierId);
            return View(item);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Items.FindAsync(id);
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            Item item = await db.Items.FindAsync(id);

            byte[] photoBack = item.Image;

            return File(photoBack, "image/png");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
