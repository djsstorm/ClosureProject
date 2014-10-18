using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Closure2.Models;
using System.Collections;

namespace Closure2.Controllers
{
    public class ProductController : Controller
    {
        private ClosureDbContext db = new ClosureDbContext();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            var branchList = (from m in db.Branches select m.Name).ToList();
            ViewBag.Branches = new SelectList(branchList, "Name");
            if (TempData["FilteredBranches"] != null)
                return View(TempData["FilteredBranches"]);
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

        public ActionResult PostsAndComments(int id = 0)
        {
            var postsres = from posts in db.Posts
                           join comments in db.Comments on posts.ID equals comments.postId into Inners
                           from i in Inners.DefaultIfEmpty()
                           select new PostsAndComments { postText = posts.text, commentText = i.text, prodId = posts.prodId };
            postsres = postsres.Where(s => s.prodId == id);
            if (postsres == null)
            {
                return HttpNotFound();
            }
            return View(postsres);
        }

        public ActionResult NumberOfComments(int id = 0)
        {
            /*  var postsres = from posts in db.Posts
                             join comments in db.Comments on posts.ID equals comments.postId into t
                             from x in t group x by new { posts.ID, x.text, posts.prodId } into g
                             select new CommentsAmounts { postText = g.Key.text, commentsAmount = g.Count(), prodId = g.Key.prodId };
              */

            var query = from posts in db.Posts
                        join comments in db.Comments on posts.ID equals comments.postId into gj
                        from subc in gj.DefaultIfEmpty()
                        group subc by new { posts.ID, posts.text, posts.prodId, subc.postId } into g
                        select new CommentsAmounts { postText = g.Key.text, commentsAmount = g.Key.postId == null ? 0 : g.Count(), prodId = g.Key.prodId };

            query = query.Where(p => p.prodId == id);
            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
        }

        //
        // GET: /Product/Create
        [Authorize(Roles = "Administrators")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create
        [Authorize(Roles = "Administrators")]
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
        // Post: /Produt/Search
        public ActionResult Search(int id = -1, string name = "", string desc = "", string branchName = "All")
        {
            var productRes = from m in db.Products select m;
            if (!String.IsNullOrEmpty(branchName) && !branchName.Equals("All"))
            {
                var branchRes = from b in db.Branches where b.Name.Equals(branchName) select b;
                int[] productIDs = null;
                foreach (Branch b in branchRes.ToList())
                {
                    // The filter above is using case-senstivie for some reason.
                    if (!b.Name.Equals(branchName))
                        continue;
                    productIDs = new int[b.Products.Count()];
                    for (int i = 0; i < productIDs.Count(); i++)
                        productIDs[i] = b.Products.ElementAt(i).ID;
                }
                if (productIDs != null)
                    productRes = from p in db.Products where productIDs.Contains(p.ID) select p;
            }
            if (id != -1)
                productRes = productRes.Where(s => s.ID == id);
            if (!String.IsNullOrEmpty(name))
                productRes = productRes.Where(s => s.Name.Equals(name));
            if (!String.IsNullOrEmpty(desc))
                productRes = productRes.Where(s => s.Description.Contains(desc));
            TempData["FilteredBranches"] = productRes.ToList();
            return RedirectToAction("Index");
        }

        //
        // GET: /Product/Edit/5
        [Authorize(Roles = "Administrators")]
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
        [Authorize(Roles = "Administrators")]
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
        [Authorize(Roles = "Administrators")]
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
        [Authorize(Roles = "Administrators")]
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