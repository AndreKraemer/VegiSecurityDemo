using System.Web.Mvc;

namespace Ak.ElVegetarianoFurio.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
            namespaces: new string[] { "Ak.ElVegetarianoFurio.Areas.Admin.Controllers" }
            );
        }
    }
}