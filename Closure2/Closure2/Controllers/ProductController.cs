using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Closure2.Models;

namespace Closure2.Controllers
{
    public class ProductController : Controller
    {
        private ClosureDbContext db = new ClosureDbContext();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            if (db.Products == null)
                return View();
            return View(db.Products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            ProductModels productmodels = db.Products.Find(id);
            if (productmodels == null)
            {
                return HttpNotFound();
            }
            return View(productmodels);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(ProductModels productmodels)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(productmodels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productmodels);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductModels productmodels = db.Products.Find(id);
            if (productmodels == null)
            {
                return HttpNotFound();
            }
            return View(productmodels);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductModels productmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productmodels);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductModels productmodels = db.Products.Find(id);
            if (productmodels == null)
            {
                return HttpNotFound();
            }
            return View(productmodels);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModels productmodels = db.Products.Find(id);
            db.Products.Remove(productmodels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}