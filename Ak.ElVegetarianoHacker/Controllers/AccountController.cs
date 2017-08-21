using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ak.ElVegetarianoHacker.Models;

namespace Ak.ElVegetarianoHacker.Controllers
{
    public class AccountController : Controller
    {

        private HackerContext _db = new HackerContext();

        public ActionResult Index()
        {
            var users = _db.Users.ToList();
            return View(users);
        }
        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return Redirect(returnUrl);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}