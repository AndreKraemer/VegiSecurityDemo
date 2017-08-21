using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Ak.ElVegetarianoFurio.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        public HomeController()
        {
            
        }
        private ApplicationUserManager _userManager;
        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            var users = UserManager.Users.ToList();
            return View(users);
        }



        // POST: Admin/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {

            var user = UserManager.FindById(id);
            UserManager.Delete(user);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult MakeAdmin(string id)
        {

            UserManager.AddToRole(id, "Admin");
            return RedirectToAction("Index");

        }
    }
}
