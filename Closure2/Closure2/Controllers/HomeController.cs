using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Closure2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Stam text about the project";
            return View();
        }

        public ActionResult Order()
        {
            ViewBag.Message = "This page will contain ability to order the product";

            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "This page will contain the pictures from different projects";
            string imageLocations = "~/Images/Gallery";
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath(imageLocations))
                              .Select(fn => imageLocations + "/" + Path.GetFileName(fn));
            return View();
        }
    }
}
