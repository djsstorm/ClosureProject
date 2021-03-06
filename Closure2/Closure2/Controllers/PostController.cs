﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Closure2.Models;

namespace Closure2.Controllers
{
    public class PostController : Controller
    {
        private ClosureDbContext db = new ClosureDbContext();

        //
        // GET: /Post/

        public ActionResult Index()
        {   
            var GenreLst = new List<int>();
            for (int i = 1; i < 6; i++)
                GenreLst.Add(i);
            ViewBag.rating = new SelectList(GenreLst);
            if (TempData["FilteredPosts"] != null)
                return View(TempData["FilteredPosts"]);
            if (db.Posts == null)
                return View();
            return View(db.Posts.ToList());
        }

        //
        // GET: /Post/Details/5

        public ActionResult Details(int id = 0)
        {
            PostModels postmodels = db.Posts.Find(id);
            if (postmodels == null)
            {
                return HttpNotFound();
            }
            return View(postmodels);
        }

        //
        // GET: /Post/Create
        [Authorize(Roles = "Administrators,Users")]
        public ActionResult Create(int id = -1)
        {
            if (id == -1)
                return HttpNotFound();
            PostModels post = new PostModels();
            ProductModels prod = db.Products.Find(id);
            if (prod == null)
                return HttpNotFound();
            post.prodId = id;
            return View(post);
        }

        //
        // POST: /Post/Create
        [Authorize(Roles = "Administrators,Users")]
        [HttpPost]
        public ActionResult Create(PostModels postmodels)
        {
            ProductModels prod = db.Products.Find(postmodels.prodId);
            if (prod == null)
                return HttpNotFound();
            postmodels.postDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Posts.Add(postmodels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("../Product/Index");
            // return View(postmodels);
        }

        //
        // GET: /Post/Edit/5
        [Authorize(Roles = "Administrators,Users")]
        public ActionResult Edit(int id = 0)
        {
            PostModels postmodels = db.Posts.Find(id);
            if (postmodels == null)
            {
                return HttpNotFound();
            }
            return View(postmodels);
        }

        //
        // POST: /Post/Edit/5
        [Authorize(Roles = "Administrators,Users")]
        [HttpPost]
        public ActionResult Edit(PostModels postmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postmodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postmodels);
        }

        //
        // Post: /Post/Search

        public ActionResult Search(DateTime? date, int rating = 0, string text = "")
        {          

            var postsres = from m in db.Posts select m;
            if (!String.IsNullOrEmpty(text))
                postsres = postsres.Where(s => s.text.Contains(text));
            if (date != null)
                postsres = postsres.Where(s => s.postDate >= date);
            if (rating >= 1 && rating <= 5)
                postsres = postsres.Where(s => s.rating >= rating);
            TempData["FilteredPosts"] = postsres.ToList();
            return RedirectToAction("Index");
        }

        public ActionResult First(int id = 0)
        {
            if (id == 0)
                return View();
            var min = from m in db.Comments.OrderBy(cv => cv.commentDate) 
                      where m.postId == id
                      select m;
            return View(min.FirstOrDefault());
        }

        //
        // GET: /Post/Delete/5
        [Authorize(Roles = "Administrators,Users")]
        public ActionResult Delete(int id = 0)
        {
            PostModels postmodels = db.Posts.Find(id);
            if (postmodels == null)
            {
                return HttpNotFound();
            }
            return View(postmodels);
        }

        //
        // POST: /Post/Delete/5
        [Authorize(Roles = "Administrators,Users")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PostModels postmodels = db.Posts.Find(id);
            db.Posts.Remove(postmodels);
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