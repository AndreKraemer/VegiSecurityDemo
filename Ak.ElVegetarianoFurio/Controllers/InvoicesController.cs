using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ak.ElVegetarianoFurio.Models;
using Microsoft.AspNet.Identity;

namespace Ak.ElVegetarianoFurio.Controllers
{
    public class InvoicesController : Controller
    {

        ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Invoices
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var invoices = _db.Invoices.Where(x => x.UserId == userId);
            return View(invoices);
        }

        public ActionResult Download(string fileName)
        {
            var path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var filePath = Path.Combine(path, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return HttpNotFound($"Datei '{filePath}' nicht gefunden.");
            }
            byte[] filedata = System.IO.File.ReadAllBytes(filePath);
            string contentType = MimeMapping.GetMimeMapping(filePath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
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