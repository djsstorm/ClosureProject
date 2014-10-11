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
    public class BranchController : Controller
    {
        private ClosureDbContext db = new ClosureDbContext();

        //
        // GET: /Branch/

        public ActionResult Index()
        {
            var productList = (from m in db.Products select m.Name).ToList();
            ViewBag.Products = new SelectList(productList, "Name");
            if (TempData["FilteredBranches"] != null)
                return View(TempData["FilteredBranches"]);
            return View(db.Branches.ToList());
        }

        //
        // Post: /Branch/Search

        public ActionResult Search(DateTime? date, string name = "", int id = -1, string product = "", string street = "")
        {
            var branchesRes = from m in db.Branches select m;
            if (id != -1)
                branchesRes = branchesRes.Where(s => s.ID == id);
            if (id != -1)
                branchesRes = branchesRes.Where(s => s.Name.Equals(name));
            if (!String.IsNullOrEmpty(product))
            {
                foreach (Branch b in branchesRes.ToList())
                {
                    bool remove = true;
                    foreach (ProductModels p in b.Products)
                    {
                        if (p.Name.Equals(product))
                        {
                            remove = false;
                            break;
                        }
                    }
                    if (remove)
                    {
                        branchesRes = branchesRes.Where(s => s.ID != b.ID);
                    }
                }
            }
            if (!String.IsNullOrEmpty(street))
                branchesRes = branchesRes.Where(s => s.Street.Contains(street));
            TempData["FilteredBranches"] = branchesRes.ToList();
            return RedirectToAction("Index");
        }

        //
        // GET: /Branch/Details/5

        public ActionResult Details(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // GET: /Branch/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Branch/Create

        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        //
        // GET: /Branch/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // POST: /Branch/Edit/5

        [HttpPost]
        public ActionResult Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        //
        // GET: /Branch/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // POST: /Branch/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
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