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
    public class CommentController : Controller
    {
        private ClosureDbContext db = new ClosureDbContext();

        //
        // GET: /Comment/

        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        //
        // GET: /Comment/Details/5

        public ActionResult Details(int id = 0)
        {
            CommentModels commentmodels = db.Comments.Find(id);
            if (commentmodels == null)
            {
                return HttpNotFound();
            }
            return View(commentmodels);
        }

        //
        // GET: /Comment/Create

        public ActionResult Create(int id = -1)
        {
            if (id == -1)
                return HttpNotFound();
            PostModels post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();
            CommentModels comment = new CommentModels();
            comment.postId = id;
            return View(comment);
        }

        //
        // POST: /Comment/Create

        [HttpPost]
        public ActionResult Create(CommentModels commentmodels)
        {
            if (db.Posts.Find(commentmodels.postId) == null)
                return HttpNotFound();
            commentmodels.commentDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comments.Add(commentmodels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("../Posts/Index");
            //return View(commentmodels);
        }

        //
        // GET: /Comment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CommentModels commentmodels = db.Comments.Find(id);
            if (commentmodels == null)
            {
                return HttpNotFound();
            }
            return View(commentmodels);
        }

        public ActionResult Search(DateTime? date, string text = "")
        {
            var postsres = from m in db.Comments select m;
            if (!String.IsNullOrEmpty(text))
                postsres = postsres.Where(s => s.text.Contains(text));
            if (date != null)
                postsres = postsres.Where(s => s.commentDate >= date);

            return View(postsres);
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        public ActionResult Edit(CommentModels commentmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commentmodels);
        }

        //
        // GET: /Comment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CommentModels commentmodels = db.Comments.Find(id);
            if (commentmodels == null)
            {
                return HttpNotFound();
            }
            return View(commentmodels);
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModels commentmodels = db.Comments.Find(id);
            db.Comments.Remove(commentmodels);
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