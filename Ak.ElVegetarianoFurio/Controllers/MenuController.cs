using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ak.ElVegetarianoFurio.Models;

namespace Ak.ElVegetarianoFurio.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menu
        public ActionResult Index()
        {
            return View(db.Categories.Include(x => x.Dishes).ToList());
        }

        public ActionResult Find(string searchTerm)

        {
            ViewBag.SearchTerm = searchTerm;
            var dishes =
                db.Database.SqlQuery<Dish>("FindDishes @SearchTerm", new SqlParameter("@SearchTerm", searchTerm)).ToList();
            return View(dishes);
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
