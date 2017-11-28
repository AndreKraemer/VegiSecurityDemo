using System.Collections.Generic;
using System.IO;
using Ak.ElVegetarianoFurio.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace Ak.ElVegetarianoFurio.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ak.ElVegetarianoFurio.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ak.ElVegetarianoFurio.Models.ApplicationDbContext context)
        {

            if (!context.Users.Any())
            {


                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


                userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Konfigurieren der Überprüfungslogik für Kennwörter.
                userManager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };
                roleManager.Create(new IdentityRole("Admin"));

                var admin = new ApplicationUser { UserName = "admin@el-vegi-furio.de", Email = "admin@el-vegi-furio.de" };
                userManager.Create(admin, "G3h4im?");
                context.SaveChanges();
                Console.WriteLine("User Created. Id: {0}", admin.Id);
                userManager.AddToRole(admin.Id, "Admin");
                

                var userToInsert = new ApplicationUser { UserName = "wilhelm@brause.de", Email = "wilhelm@brause.de" };
                userManager.Create(userToInsert, "G3h4im?");

                context.PaymentInfos.Add(new PaymentInfo
                {
                    AccountNumber = "3694-1649-6395-8421",
                    Cardholder = "Wilhelm Brause",
                    ExpirationDate = "12/2019",
                    CCV = "123",
                    CardType = CreditCardType.AmericanExpress,
                    User = userToInsert
                });

                context.PaymentInfos.Add(new PaymentInfo
                {
                    AccountNumber = "1941-8872-8791-1234",
                    Cardholder = "Wilhelm Brause",
                    ExpirationDate = "06/2018",
                    CCV = "321",
                    CardType = CreditCardType.Visa,
                    User = userToInsert
                });

                context.SaveChanges();

                context.Invoices.Add(new Invoice
                {
                    Filename = "Rechnung17001.pdf",
                    InvoiceDate = new DateTime(2017, 08, 21),
                    Total = 9.98,
                    UserId = userToInsert.Id
                });

                context.Invoices.Add(new Invoice
                {
                    Filename = "Rechnung17002.pdf",
                    InvoiceDate = new DateTime(2017, 08, 22),
                    Total = 9.98,
                    UserId = userToInsert.Id
                });

                userToInsert = new ApplicationUser { UserName = "hans@dampf.de", Email = "hans@dampf.de" };
                userManager.Create(userToInsert, "G3h4im?");

                context.PaymentInfos.Add(new PaymentInfo
                {
                    AccountNumber = "9981-5953-1298-7609",
                    Cardholder = "Hans Dampf",
                    ExpirationDate = "05/2017",
                    CCV = "520",
                    CardType = CreditCardType.Master,
                    User = userToInsert
                });

                context.PaymentInfos.Add(new PaymentInfo
                {
                    AccountNumber = "6309-8306-7503-2974",
                    Cardholder = "Hans Dampf",
                    ExpirationDate = "02/2019",
                    CCV = "836",
                    CardType = CreditCardType.Visa,
                    User = userToInsert
                });




                var path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                var dishesJson = File.ReadAllText(Path.Combine(path, "dishes.json"));
                var categoriesJson = File.ReadAllText(Path.Combine(path, "categories.json"));

                var dishes = JsonConvert.DeserializeObject<List<Dish>>(dishesJson);
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

                foreach (var category in categories)
                {
                    foreach (var dish in dishes.Where(x => x.CategoryId == category.Id))
                    {
                        dish.Id = 0;
                        category.Dishes.Add(dish);
                    }
                    category.Id = 0;
                    context.Categories.Add(category);
                }
                context.SaveChanges();
            }
        }
    }
}
