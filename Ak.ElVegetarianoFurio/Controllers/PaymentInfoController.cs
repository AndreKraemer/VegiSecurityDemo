using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ak.ElVegetarianoFurio.Models;
using Microsoft.AspNet.Identity;

namespace Ak.ElVegetarianoFurio.Controllers
{
    public class PaymentInfoController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var paymentInfos = _db.PaymentInfos
                .Where(x => x.UserId == userId)
                .ToList();
            return View(paymentInfos);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var paymentInfo = _db.PaymentInfos.Find(id);

            if (paymentInfo == null)
            {
                return HttpNotFound();
            }

            return View(paymentInfo);
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