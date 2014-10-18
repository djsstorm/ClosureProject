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

        public ActionResult Index(int id = -1)
        {
            ViewBag.ID = id;
            var res = from m in db.Comments.Where(s => s.postId == id) select m;
            return View(res.ToList());
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

        [Authorize(Roles = "Administrators,Users")]
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
        [Authorize(Roles = "Administrators,Users")]
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
        // Post: /Comment/Search
        // 

        public ActionResult Search(DateTime? date, string text = "", int id = -1)
        {
            var commentRes = from m in db.Comments select m;
            if (id != -1)
                commentRes = commentRes.Where(s => s.postId == id);
            if (!String.IsNullOrEmpty(text))
                commentRes = commentRes.Where(s => s.text.Contains(text));
            if (date != null)
                commentRes = commentRes.Where(s => s.commentDate >= date);
            return View(commentRes);
        }

        //
        // GET: /Comment/Edit/5
        [Authorize(Roles = "Administrators,Users")]
        public ActionResult Edit(int id = 0)
        {
            CommentModels commentmodels = db.Comments.Find(id);
            if (commentmodels == null)
            {
                return HttpNotFound();
            }
            return View(commentmodels);
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        [Authorize(Roles = "Administrators,Users")]
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
        [Authorize(Roles = "Administrators,Users")]
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
        [Authorize(Roles = "Administrators,Users")]
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