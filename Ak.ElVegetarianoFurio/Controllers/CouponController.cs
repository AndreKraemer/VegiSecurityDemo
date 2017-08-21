using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ak.ElVegetarianoFurio.Models;
using Microsoft.AspNet.Identity;

namespace Ak.ElVegetarianoFurio.Controllers
{
    [Authorize]
    public class CouponController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Coupon
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var coupons = _db.Coupons.Where(x => x.UserId == userId).OrderByDescending(x => x.Date).ToList();
            ViewBag.Sum = coupons.Sum(x => x.Ammount);
            return View(coupons);
        }



        // GET: Coupon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coupon/Create
        [HttpPost]
        public ActionResult Create(Coupon coupon)
        {
            try
            {
                coupon.UserId = User.Identity.GetUserId();
                coupon.Date = DateTime.Now;
                _db.Coupons.Add(coupon);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(coupon);
            }
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
