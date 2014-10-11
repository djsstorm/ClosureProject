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
                        group subc by new { posts.ID, posts.text, posts.prodId, subc.postId} into g
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
        // Post: /Produt/Search
        public ActionResult Search(int id = -1, string name = "", string desc = "")
        {
            var productRes = from m in db.Products select m;
            if (id != -1)
                productRes = productRes.Where(s => s.ID == id);
            if (String.IsNullOrEmpty(name))
                productRes = productRes.Where(s => s.Name.Equals(name));
            if (String.IsNullOrEmpty(desc))
                productRes = productRes.Where(s => s.Description.Contains(desc));
            return View(productRes);
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