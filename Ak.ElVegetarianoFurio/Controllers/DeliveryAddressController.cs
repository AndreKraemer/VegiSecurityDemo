using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ak.ElVegetarianoFurio.Models;
using Microsoft.AspNet.Identity;

namespace Ak.ElVegetarianoFurio.Controllers
{
    public class DeliverAddressController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var address = _db.DeliveryAddresses
                .FirstOrDefault(x => x.UserId == userId);
            return View(address);
        }

        [Authorize]
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var address = _db.DeliveryAddresses
                .FirstOrDefault(x => x.UserId == userId);
            return View(address);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(DeliveryAddress model)
        {

            var address = _db.DeliveryAddresses.Find(model.Id);
            
            if (address == null)
            {
                return HttpNotFound();
            }
            address.FirstName = model.FirstName;
            address.LastName = model.LastName;
            address.Street = model.Street;
            address.Zip = model.Zip;
            address.City = model.City;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Create()
        {
            var address = new DeliveryAddress();
            return View(address);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Create(DeliveryAddress model)
        {
            var userId = User.Identity.GetUserId();
            model.UserId = userId;
            _db.DeliveryAddresses.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
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